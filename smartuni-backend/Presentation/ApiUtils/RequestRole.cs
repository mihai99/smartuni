using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace Presentation.ApiUtils
{
    public class RequestRole: Attribute, IResourceFilter
    { 
        public string[] RequiredRoles { get; set; }

        public RequestRole(string roles)
        {
            RequiredRoles = roles.Split(" ");
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            string[] presentRole = (string[])context.HttpContext.Items["roles"];
            if((RequiredRoles != null && presentRole == null) || !RequiredRoles.All(role => presentRole.Contains(role))) {
                byte[] data = Encoding.UTF8.GetBytes("You do not have the role to perform this request");
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                context.HttpContext.Response.Body.WriteAsync(data, 0, data.Length);
            }
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
        }
    }
}
