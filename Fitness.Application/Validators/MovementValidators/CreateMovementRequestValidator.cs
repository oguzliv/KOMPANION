using Fitness.Application.Models.MovementModels.MovementRequests;
using FluentValidation;

namespace Fitness.Application.Validators.UserValidators
{
    public class CreateMovementRequestValidator : AbstractValidator<CreateMovementRequest>
    {
        public CreateMovementRequestValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(255);

            RuleFor(x => x.MuscleGroup)
                .IsInEnum();

        }
    }
}