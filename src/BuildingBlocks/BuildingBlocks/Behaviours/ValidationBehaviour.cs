using BuildingBlocks.CQRS;
using FluentValidation;
using MediatR;
using System.Windows.Input;

namespace BuildingBlocks.Behaviours;

public class ValidationBehaviour<TRequest, TResponse>
    (IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommand<IRequest>
{
    public async Task<TResponse> Handle
        (TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        // Validation contect comes from incoming request
        var context = new ValidationContext<TRequest>(request);
        // Complete all validation operation injected from primary constructor
        // run all validation and accumalte result into validator till completed all validation
        var validationResults = await Task.WhenAll(validators.Select (validator => validator.ValidateAsync(context, cancellationToken)));
        //if any error then throw validation exception
        var failures = validationResults
                        .Where(result => result.Errors.Any())
                        .SelectMany(error =>  error.Errors)
                        .ToList();
        //throw validation error
        if (failures.Any())
        {
            throw new ValidationException(failures);
        }
        //run next pipeline behaviour - actual command handler.
        return await next();
    }
}
