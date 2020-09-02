﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Exceptions
{
    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (!(context.Exception is HttpResponseException exception)) return;
            context.Result = new ObjectResult(exception.Value) {StatusCode = exception.Status};
            context.ExceptionHandled = true;
        }

        public int Order { get; set; } = int.MaxValue - 10;
    }
}