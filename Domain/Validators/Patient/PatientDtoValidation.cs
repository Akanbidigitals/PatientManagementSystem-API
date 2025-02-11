using FluentValidation;
using PatientManagementSystem_API.Domain.DTos.Patient;
using System.Globalization;

namespace PatientManagementSystem_API.Domain.Validators.Patient
{
    public class PatientDtoValidation : AbstractValidator<CreatePatientDTO>
    {
        public PatientDtoValidation()
        {
            RuleFor(dto => dto.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(20).WithMessage("First name cannot exceed 20 chracters.");
                
            RuleFor(dto => dto.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(20).WithMessage("Last name cannot exceed 20 chracters.");

            RuleFor(dto => dto.DateOfBirth)
              .NotEmpty().WithMessage("Date of birth is required.")
              .Matches(@"^\d{4}-\d{2}-\d{2}$").WithMessage("Date must be in YYYY-MM-DD format.")
              .Must(BeAValidDate).WithMessage("Invalid date.")
              .Must(NotBeInFuture).WithMessage("Date of birth cannot be in the future.");

            RuleFor(dto => dto.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("A valid email address is required");

            RuleFor(dto => dto.Address)
               .NotEmpty().WithMessage("Address is required.");
               

            RuleFor(dto => dto.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .MinimumLength(11).WithMessage("Phone number cant be less than 11 digit.");

        }


        private bool BeAValidDate(string date)
        {
            return DateTime.TryParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);   //Valid Date
        }

        private bool NotBeInFuture(string date)
        {
            if (DateTime.TryParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
            {
                return parsedDate <= DateTime.UtcNow.Date;
            }
            return false; // Invalid date format future date
        }
    }
}
