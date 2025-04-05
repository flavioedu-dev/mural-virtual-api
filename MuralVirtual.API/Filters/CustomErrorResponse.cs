using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MuralVirtual.API.Resources;

namespace MuralVirtual.API.Filters;

public class CustomErrorResponse : Attribute, IActionFilter
{
    private Dictionary<string, IEnumerable<string>> ListModelErros(ActionContext context) =>
        context.ModelState
        .ToDictionary(x => x.Key, y => y.Value?.Errors.Select(z => z.ErrorMessage))!;

    public void OnActionExecuted(ActionExecutedContext context) { }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            Dictionary<string, IEnumerable<string>> errors = ListModelErros(context);

            var result = new
            {
                message = ApiMessages.Request_Failure,
                errors
            };

            context.Result = new BadRequestObjectResult(result);

            return;
        }

        return;
    }
}