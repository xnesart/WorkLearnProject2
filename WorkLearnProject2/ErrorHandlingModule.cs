using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Owin;
using OwinInterface = System.Func<System.Collections.Generic.IDictionary<string, object>, System.Threading.Tasks.Task>;

namespace WorkLearnProject2
{
    public class ErrorHandlingModule
    {
        private readonly OwinInterface _next;

        public ErrorHandlingModule(OwinInterface next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(IDictionary<string, object> env)
        {
            try
            {
                await _next(env);
            }
            catch (Exception e)
            {
                LogError(e);

                var response = new OwinResponse(env)
                {
                    StatusCode = 500,
                    ReasonPhrase = "InternalServerError"
                };

                await response.WriteAsync("Unexpected error, please try again later");
            }
        }

        private void LogError(Exception e)
        {
            var logMessage = $"{DateTime.Now}: Error occurred - {e.Message}{Environment.NewLine}{e.StackTrace}";


            var logFilePath = "ErrorHandlingLog,txt";

            File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
        }
    }
}