using FluentValidation;
using MediatR;

namespace LEGO4IDControl.Infrastructure;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);
        
        var validationFailures = await Task.WhenAll(
            validators.Select(validator => validator.ValidateAsync(context, cancellationToken)));

        var isValid = validationFailures.Select(validation => validation.IsValid).Single();
        
        if (!isValid)
        {
            throw new Exception("Validation error");
        }

        var response = await next();

        return response;
    }
}