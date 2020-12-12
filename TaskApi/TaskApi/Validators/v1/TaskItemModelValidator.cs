using FluentValidation;
using TaskApi.Models.v1;

namespace TaskApi.Validators.v1
{
    public class TaskItemModelValidator : AbstractValidator<TaskItemModel>
    {
        public TaskItemModelValidator()
        {
            RuleFor(x => x.Title)
                .NotNull();
            RuleFor(x => x.Description )
                .NotNull();
         
        }
    }
}