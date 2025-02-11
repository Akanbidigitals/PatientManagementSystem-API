using FluentValidation;
using PatientManagementSystem_API.Domain.DTos.Record;

namespace PatientManagementSystem_API.Domain.Validators.Record
{
    public class RecordDtoValidation : AbstractValidator<CreateRecordDTO>
    {
        public RecordDtoValidation()
        {
            RuleFor(dto => dto.Diagnosis)
                .NotEmpty().WithMessage("Diagnosis is required.");

            RuleFor(dto => dto.Treatment)
                .NotEmpty().WithMessage("Treatment is required");


        }
     

    }
}
