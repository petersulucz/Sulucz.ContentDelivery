namespace Sulucz.ContentDelivery.ApiTestApp.Filters
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc.Filters;

    using Sulucz.ContentDelivery.Common.Exceptions;

    /// <summary>
    /// The model validation filter.
    /// </summary>
    public class ModelValidationFilter : ActionFilterAttribute
    {
        /// <summary>
        /// The on action executing.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <exception cref="InvalidModelException">
        /// </exception>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (false == context.ModelState.IsValid)
            {
                var messages =
                    context.ModelState.Keys.SelectMany(k => context.ModelState[k].Errors.Select(e => e.ErrorMessage));
                var flat = string.Join(",", messages);
                throw new InvalidModelException(flat);
            }
        }
    }
}
