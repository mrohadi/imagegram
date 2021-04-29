using System;
using System.Linq;
using ImageGram.API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ImageGram.API.Services
{
    public class AuthenticationFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var tokenManager = (ITokenManager)context.HttpContext.RequestServices.GetService(typeof(ITokenManager));

            var result = true;
            if(!context.HttpContext.Request.Headers.ContainsKey("Authorization"))
                result = false;
            
            string token = string.Empty;
            if(result)
            {
                token = context.HttpContext.Request.Headers.First(x => x.Key == "Authorization").Value;
                if(!tokenManager.VerifyToken(token))
                    result = false;
            }

            if(!result)
            {
                context.ModelState.AddModelError("Unauthorized", "You are unauthorized!");
                context.Result = new UnauthorizedObjectResult(context.ModelState);
            }
        }
    }
}