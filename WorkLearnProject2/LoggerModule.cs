using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using OwinInterface = System.Func<System.Collections.Generic.IDictionary<string, object>, System.Threading.Tasks.Task>;

namespace WorkLearnProject2
{
    public class LoggerModule
    {
        private readonly OwinInterface _next;
        private readonly string _prefix;

        public LoggerModule(OwinInterface next, string prefix)
        {
            if (string.IsNullOrEmpty(prefix)) throw new ArgumentException("prefix cant be null or empty");

            this._next = next ?? throw new ArgumentException("next");
            this._prefix = prefix;
        }

        public Task Invoke(IDictionary<string, object> env)
        {
            try
            {
                Debug.WriteLine("{0} Request: {1}", this._prefix, env["owin.RequestPath"]);
                string requestPath = env.ContainsKey("owin.RequestPath")
                    ? env["owin.RequestPath"].ToString()
                    : "Unknown path";
                string logMessage = string.Format("{0} [{1}] Request: {2}",
                    this._prefix,
                    DateTime.Now,
                    requestPath);

                WriteToFile(logMessage);
            }
            catch (Exception ex)
            {
                var tcs = new TaskCompletionSource<object>();
                tcs.SetException(ex);
                return tcs.Task;
            }

            return this._next(env);
        }

        public void WriteToFile(string textToWrite)
        {
            try
            {
                // Используем StreamWriter для записи в файл. Если файла нет, он будет создан.
                using (StreamWriter writer = new StreamWriter("log.txt", true)) // true добавляет запись в конец файла
                {
                    string logEntry = $"{DateTime.Now}: {textToWrite}"; // Добавляем метку времени
                    writer.WriteLine(logEntry);
                }
            }
            catch (Exception ex)
            {
                // Если произошла ошибка при записи, можно залогировать это отдельно или выбросить исключение
                Console.WriteLine($"Ошибка при записи в файл: {ex.Message}");
            }
        }
    }
}