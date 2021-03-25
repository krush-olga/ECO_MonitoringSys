using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserMap.Helpers
{
    /// <include file='Docs/Helpers/TaskExtensionsDoc.xml' path='docs/members[@name="taks_extensions"]/TaskExtension/*'/>
    public static class TaskExtensions
    {
        /// <include file='Docs/Helpers/TaskExtensionsDoc.xml' path='docs/members[@name="taks_extensions"]/CatchErrorOrCancel/*'/>
        public static Task CatchErrorOrCancel(this Task task, Action<Exception> exceptionHandler)
        {
            if (exceptionHandler == null)
                throw new ArgumentNullException("exceptionHandle");
            if (task == null)
                throw new ArgumentNullException("task");

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
                        exceptionHandler(ex);
                    }
                }
                else if (result.IsFaulted)
                {
                    exceptionHandler(result.Exception);
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <include file='Docs/Helpers/TaskExtensionsDoc.xml' path='docs/members[@name="taks_extensions"]/CatchErrorOrCancelGeneric/*'/>
        public static Task<TResult> CatchErrorOrCancel<T, TResult>(this Task<T> task, Action<Exception> exceptionHandler, 
                                                                   Func<T, TResult> resultFunc)
        {
            if (exceptionHandler == null)
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
                        exceptionHandler(ex);
                    }

                    return resultFunc != null ? resultFunc(default) : default;
                }
                else if (result.IsFaulted)
                {
                    exceptionHandler(result.Exception);
                    return resultFunc != null ? resultFunc(default) : default;
                }
                else
                {
                    return resultFunc != null ? resultFunc(result.Result) : default;
                } 
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <include file='Docs/Helpers/TaskExtensionsDoc.xml' path='docs/members[@name="taks_extensions"]/CatchAndLog/*'/>
        internal static Task CatchAndLog(this Task task, Services.ILogger logger, string errorMessage = "")
        {
            return task.CatchErrorOrCancel(ex =>
             {
                 if (string.IsNullOrEmpty(errorMessage))
                     errorMessage = "Сталась помилка під час виконання запиту. ";

                 var innerException = ex.GetInnerestException();
                 if (innerException is SocketException socketException)
                 {
                     if (socketException.SocketErrorCode == SocketError.TimedOut)
                     {
                         MessageBox.Show(errorMessage +
                                         "Відсутнє підключення до інтернету.",
                                         "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     }
                 }
                 else
                 {
                     MessageBox.Show(errorMessage,
                                     "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 }
#if DEBUG
                 System.Diagnostics.Debug.WriteLine(ex.Message);
#else
                 logger.Log(ex);
#endif
             });
        }
        /// <include file='Docs/Helpers/TaskExtensionsDoc.xml' path='docs/members[@name="taks_extensions"]/CatchAndLogGeneric/*'/>
        internal static Task<TResult> CatchAndLog<TResult>(this Task<TResult> task, Services.ILogger logger, string errorMessage = "")
        {
            return task.CatchErrorOrCancel(ex =>
            {
                if (string.IsNullOrEmpty(errorMessage))
                    errorMessage = "Сталась помилка під час виконання запиту. ";

                var innerException = ex.GetInnerestException();
                if (innerException is SocketException socketException)
                {
                    if (socketException.SocketErrorCode == SocketError.TimedOut)
                    {
                        MessageBox.Show(errorMessage +
                                        "Відсутнє підключення до інтернету.",
                                        "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show(errorMessage,
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
