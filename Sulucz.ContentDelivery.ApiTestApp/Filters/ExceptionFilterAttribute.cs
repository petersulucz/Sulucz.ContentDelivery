namespace Sulucz.ContentDelivery.ApiTestApp.Filters
{
    using System.Net;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    using Sulucz.ContentDelivery.Common.Exceptions;

    /// <summary>
    /// The sulucz exception filter attribute.
    /// </summary>
    public class SuluczExceptionFilterAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// The on exception.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public override void OnException(ExceptionContext context)
        {
            var shortMessage = "SERVER ERRROR";
            var message = context.HttpContext.Request.IsLocal() ? context.Exception.Message : "An error has occured";
            var code = HttpStatusCode.InternalServerError;

            if (context.Exception is GenericFailureException)
            {
                var generic = (GenericFailureException)context.Exception;

                message = generic.Message;
                shortMessage = generic.ShortMessage;
                code = generic.StatusCode;
            }

            context.Result = new ContentResult()
                                 {
                                     Content = $"{{ Message = \"{message}\"}}",
                                     ContentType = "text/json",
                                     StatusCode = (int)code
                                 };
        }
    }
}
