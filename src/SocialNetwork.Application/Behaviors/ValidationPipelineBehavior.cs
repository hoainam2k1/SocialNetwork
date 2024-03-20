using MediatR;
using SocialNetwork.Contract.Abstractions.Shared;
using FluentValidation;


namespace SocialNetwork.Application.Behaviors
{
    public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
        where TRequest : IRequest<TResponse>
        where TResponse : Result
    {
        private readonly IEnumerable<IValidator<TRequest>> _validatiors;

        public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validatiors)
        {
            _validatiors = validatiors;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if(!_validatiors.Any())
            {
                return await next();
            }

            Error[] errors = _validatiors
                .Select(validator => validator.Validate(request))
                .SelectMany(validationResult => validationResult.Errors)
                .Where(validationFailure => validationFailure is not null)
                .Select(failure => new Error( failure.PropertyName, failure.ErrorMessage))
                .Distinct()
                .ToArray();

            if(errors.Any())
            {
                return CreateValidationResult<TResponse>(errors);
            }
            return await next();
                
        }

        private static TResult CreateValidationResult<TResult>(Error[] errors) where TResult : Result
        {
            if(typeof(TResult) == typeof(Result))
            {
                return (ValidationResult.WithErrors(errors) as TResult)!;
            }
            object validationResult = typeof(ValidationResult<>)
                .GetGenericTypeDefinition()
                .MakeGenericType(typeof(TResult)).GenericTypeArguments[0]
                .GetMethod(nameof(ValidationResult.WithErrors)) !
                .Invoke(null, new object?[] { errors}) !;
            return (TResult)validationResult;
        }
    }
}
