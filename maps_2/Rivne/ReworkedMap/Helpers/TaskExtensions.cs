using System;
using System.Threading.Tasks;

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
        internal static Task CatchErrorOrCancel(this Task task, Services.ILogger logger)
        {
            if (logger == null)
            {
                throw new ArgumentNullException("logger");
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
                        logger.Log(ex);
                    }
                }
                else if (result.IsFaulted)
                {
                    logger.Log(result.Exception);
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
        internal static Task<TResult> CatchErrorOrCancel<T, TResult>(this Task<T> task, Services.ILogger logger, 
                                                                     Func<T, TResult> resultFunc)
        {
            if (logger == null)
            {
                throw new ArgumentNullException("logger");
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
                        logger.Log(ex);
                    }

                    return resultFunc != null ? resultFunc(default) : default;
                }
                else if (result.IsFaulted)
                {
                    logger.Log(result.Exception);
                    return resultFunc != null ? resultFunc(default) : default;
                }
                else
                {
                    return resultFunc != null ? resultFunc(result.Result) : default;
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
