using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
namespace ReturnTypeAndStatusCodes.Models
{
    public class HttpMethodFilter : IActionFilter
    {
        private readonly List<string> _allowedMethods;
        public HttpMethodFilter(List<string> allowedMethods)
        {
            _allowedMethods = allowedMethods;  
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!_allowedMethods.Contains(context.HttpContext.Request.Method))
            {
                var customResponse = new
                {
                    Code = 400,
                    Message = "HTTP Method not allowed"
                };

                context.Result = new ObjectResult(customResponse)
                {
                    StatusCode = 400
                };
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }
    }
}
