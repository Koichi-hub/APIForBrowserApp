using APIForBrowserApp.Models;

namespace APIForBrowserApp.Helpers
{
    public static class AppResultFactory
    {
        public static AppResult<object> Create()
        {
            return new AppResult<object>()
            {
                Status = StatusCodes.Status200OK,
                Message = "OK",
            };
        }

        public static AppResult<T> Create<T>()
        {
            return new AppResult<T>()
            {
                Status = StatusCodes.Status200OK,
                Message = "OK",
            };
        }

        public static AppResult<T> Create<T>(int statusCode, string message)
        {
            return new AppResult<T>()
            {
                Status = statusCode,
                Message = message,
            };
        }
    }
}
