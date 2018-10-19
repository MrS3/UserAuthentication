using System;

namespace UserAuth.API.Helpers
{
    public class AppException: Exception
    {
        public AppException() : base() {}
        public AppException(string message) : base(message) {}
    }
}