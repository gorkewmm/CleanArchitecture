using FluentValidation.Results;

namespace HR.LeaveManagement.Application.Excepitons
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base()
        {

        }
        public BadRequestException(string message, ValidationResult validationResult) : base(message)
        {
            ValidationErrors = validationResult.ToDictionary();
        }

        public IDictionary<string, string[]> ValidationErrors { get; set; }
    }

}
