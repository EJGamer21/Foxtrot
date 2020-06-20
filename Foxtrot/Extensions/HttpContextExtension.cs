using System;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Foxtrot.Extensions
{
    public static class HttpContextExtension
    {
        public static Guid GetLoggedUserId(this HttpContext httpContext)
        {
            /*return httpContext.User == null
                ? Guid.Empty
                : new Guid(httpContext.User.Claims
                    .Single(claim => claim.Type == "sid").Value);*/
            
            Guid userId = new Guid(httpContext.User.Claims
                .FirstOrDefault(claim => claim.Type == "sid")?.Value ?? string.Empty);

            return new Guid("9F917B76-5B52-4DF2-9A9E-D99875777AC4");
        }
    }
}