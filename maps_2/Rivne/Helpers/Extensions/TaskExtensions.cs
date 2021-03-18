using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserMap.Helpers;

namespace UserMap.Helpers
{
    public static class TaskExtensions
    {
        public static Task CatchErrorOrCancel(this Task task, Action<Exception> exceptionHandle)
        {
            if (exceptionHandle == null)
            {
                throw new ArgumentNullException("exceptionHandle");
            }

            return task.ContinueWith(result => 
            {
                if (result.IsCanceled)
                {
                    try
                    {
                        result.GetAwaiter().GetResult();
                    }
                    catch (Exception ex)
                    {
                        exceptionHandle(ex);
                    }
                }
                else if (result.IsFaulted)
                {
                    exceptionHandle(result.Exception);
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        public static Task<TResult> CatchErrorOrCancel<T, TResult>(this Task<T> task, Action<Exception> exceptionHandle, 
                                                                   Func<T, TResult> resultFunc)
        {
            if (exceptionHandle == null)
            {
                throw new ArgumentNullException("exceptionHandle");
            }

            return task.ContinueWith(result =>
            {
                if (result.IsCanceled)
                {
                    try
                    {
                        _ = result.Result;
                    }
                    catch (Exception ex)
                    {
                        exceptionHandle(ex);
                    }

                    return resultFunc != null ? resultFunc(default) : default;
                }
                else if (result.IsFaulted)
                {
                    exceptionHandle(result.Exception);
                    return resultFunc != null ? resultFunc(default) : default;
                }
                else
                {
                    return resultFunc != null ? resultFunc(result.Result) : default;
                } 
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        internal static Task CatchAndLog(this Task task, Services.ILogger logger)
        {
            return task.CatchErrorOrCancel(ex =>
             {
                 var innerException = ex.GetInnerestException();
                 if (innerException is SocketException socketException)
                 {
                     if (socketException.SocketErrorCode == SocketError.TimedOut)
                     {
                         MessageBox.Show("Сталась помилка під час фільтрації статистики. " +
                                         "Відсутнє підключення до інтернету.",
                                         "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     }
                 }
                 else
                 {
                     MessageBox.Show("Виникла помилка під час фільтрації статистики.",
                                     "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 }
#if DEBUG
                 System.Diagnostics.Debug.WriteLine(ex.Message);
#else
                 logger.Log(ex);
#endif
             });
        }
        internal static Task<TResult> CatchAndLog<TResult>(this Task<TResult> task, Services.ILogger logger)
        {
            return task.CatchErrorOrCancel(ex =>
            {
                var innerException = ex.GetInnerestException();
                if (innerException is SocketException socketException)
                {
                    if (socketException.SocketErrorCode == SocketError.TimedOut)
                    {
                        MessageBox.Show("Сталась помилка під час фільтрації статистики. " +
                                        "Відсутнє підключення до інтернету.",
                                        "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Виникла помилка під час фільтрації статистики.",
                                    "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
#if DEBUG
                System.Diagnostics.Debug.WriteLine(ex.Message);
#else
                logger.Log(ex);
#endif
            }, data => data);
        }
    }
}
