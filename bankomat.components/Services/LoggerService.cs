using Microsoft.JSInterop;

namespace bankomat.components
{
    public class LoggerService : ILoggerService
    {
        private readonly IJSRuntime _jsRuntime;
        public LoggerService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public void WriteConsole(string message)
        {
            _jsRuntime.InvokeVoidAsync("console.log", message);
        }
    }
}