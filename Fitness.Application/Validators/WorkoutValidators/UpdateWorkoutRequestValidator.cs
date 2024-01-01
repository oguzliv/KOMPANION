using Fitness.Application.Models.WorkoutModels.WorkoutRequests;
using FluentValidation;

namespace Fitness.Application.Validators.UserValidators
{
    public class UpdateWorkoutRequestValidator : AbstractValidator<UpdateWorkoutRequest>
    {
        public UpdateWorkoutRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(255);
            RuleFor(x => x.Level).IsInEnum();
            RuleFor(x => x.Duration).IsInEnum();
            RuleFor(x => x.Movements).NotEmpty().ForEach(item => item.NotEqual(Guid.Empty));

        }
    }
}