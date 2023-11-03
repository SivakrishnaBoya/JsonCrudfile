using System.Text;
namespace TaskCrud_20_10_23.LoggerDetails
{
    public class LoggerDetailsclass
    {
        private readonly RequestDelegate _next;
        public LoggerDetailsclass( RequestDelegate next)
        {
            _next=next;
        }
        public async Task Invoke(HttpContext context)
        {
            // Create a log file name with a timestamp

            string FileName = $"RequestLogFile_{DateTime.Now:yyyyMMddHHmmss}.txt";
            string FilePath = Path.Combine("LoggerDetails", FileName);

            // Capture request information
            var requestInfo = new StringBuilder();
            requestInfo.AppendLine($"Received Request at: {DateTime.Now}");
            requestInfo.AppendLine($"Request Path: {context.Request.Path}");
            requestInfo.AppendLine($"Request Method: {context.Request.Method}");
            requestInfo.AppendLine($"Client IP: {context.Connection.RemoteIpAddress}");

            // Write the request information to the log file
            File.WriteAllText(FilePath, requestInfo.ToString());

            // Call the next middleware in the pipeline
            await _next(context);
        }
    }
}
