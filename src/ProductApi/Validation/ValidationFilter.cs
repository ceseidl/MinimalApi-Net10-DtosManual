using FluentValidation;

namespace ProductApi.Validation;

public sealed class ValidationFilter<T> : IEndpointFilter where T : class
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var validator = context.HttpContext.RequestServices.GetService(typeof(IValidator<T>)) as IValidator<T>;
        if (validator is null)
            return await next(context);

        var arg = context.Arguments.FirstOrDefault(a => a is T) as T;
        if (arg is null)
            return Results.BadRequest(new { message = "Payload inválido." });

        var result = await validator.ValidateAsync(arg);
        if (!result.IsValid)
        {
            var errors = result.Errors.Select(e => new { field = e.PropertyName, error = e.ErrorMessage });
            return Results.ValidationProblem(errors.ToDictionary(e => e.field, e => new[] { e.error }));
        }

        return await next(context);
    }
}
