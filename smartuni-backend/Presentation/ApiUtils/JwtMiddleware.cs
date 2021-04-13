using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;
using FirebaseAdmin.Auth;

namespace Presentation.ApiUtils
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                if(token != null)
                {
                    FirebaseToken decodedJwtToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(token);
                    object isAdmin;
                    if (decodedJwtToken.Claims.TryGetValue("student", out isAdmin))
                    {
                        if ((bool)isAdmin)
                        {
                            context.Items["roles"] = new string[] { "student" };
                            context.Items["userId"] = decodedJwtToken.Uid;
                        }
                    }
                }
            }
            finally
            {

            await _next(context);
            }
        }
    }
}
