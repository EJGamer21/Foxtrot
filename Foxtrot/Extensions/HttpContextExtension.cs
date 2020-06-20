using System;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Foxtrot.Extensions
{
    public static class HttpContextExtension
    {
        public static Guid GetLoggedUserId(this HttpContext httpContext)
        {
            return httpContext.User == null
                ? Guid.Empty
                : new Guid(httpContext.User.Claims
                    .Single(claim => claim.Type == "sid").Value);
        }
    }
}