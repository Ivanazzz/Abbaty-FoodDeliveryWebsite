using System.Security.Claims;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using FoodDeliveryWebsite.Models.Enums;

namespace FoodDeliveryWebsite.Repositories.ValidatorContainer
{
    public class ValidatorRepository
    {
        public class AuthorizedAdminAttribute : Attribute, IAsyncActionFilter
        {
            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                var role = context.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;

                if (role != UserRole.Admin.ToString())
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }
                await next();
            }
        }

        public class AuthorizedClientAttribute : Attribute, IAsyncActionFilter
        {
            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                var role = context.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;

                if (role != UserRole.Admin.ToString() && role != UserRole.Client.ToString())
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }
                await next();
            }
        }
    }
}
