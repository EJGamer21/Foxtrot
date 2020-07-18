using System;
using Microsoft.AspNetCore.Http;

namespace Foxtrot.Extensions
{
    public static class HttpContextExtension
    {
        public static Guid GetLoggedUserId(this HttpContext httpContext)
        {
            var userId = httpContext.Session.GetString("UserId");
            return string.IsNullOrWhiteSpace(userId)
                ? Guid.Empty
                : new Guid(userId);
        }

        public static bool IsUserLoggedIn(this HttpContext httpContext) =>
            GetLoggedUserId(httpContext) != Guid.Empty;
    }
}