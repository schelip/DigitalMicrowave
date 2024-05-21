using DigitalMicrowave.Web.Model.InputModel;
using FluentValidation;

namespace DigitalMicrowave.Web.Model.Validators
{
    public class StartHeatingInputModelValidator : AbstractValidator<StartHeatingInputModel>
    {
        public StartHeatingInputModelValidator()
        {
            RuleFor(inputModel => inputModel.Time)
                .InclusiveBetween(1, 120)
                .WithMessage("Tempo precisa ser um valor entre 1 e 120 segundos");

            RuleFor(inputModel => inputModel.PowerLevel)
                .InclusiveBetween(1, 10)
                .WithMessage("Porência precisa ser um valor entre 1 e 10"); ;
        }
    }
}