using FluentValidation.Results;
using System.Net;

namespace SurveyAcme.Models.Utilities
{
    public class CustomException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public string TechnicalError { get; set; }
        public List<string> FieldValidations { get; set; }

        public CustomException(string message) : base(message)
        {
            StatusCode = HttpStatusCode.BadRequest;
            TechnicalError = string.Empty;
        }

        public CustomException(string message, string technicalError) : base(message)
        {
            StatusCode = HttpStatusCode.BadRequest;
            TechnicalError = technicalError;
        }

        public CustomException(string message, HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
            TechnicalError = string.Empty;
        }

        public CustomException(string message, string technicalError, HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
            TechnicalError = technicalError;
        }

        public CustomException(List<ValidationFailure> errors) : base(GenerateMessageFromErrors())
        {
            FieldValidations = errors.Select(error => error.ErrorMessage).ToList();
            StatusCode = HttpStatusCode.UnprocessableEntity;
            TechnicalError = string.Empty;
        }

        private static string GenerateMessageFromErrors()
        {
            string errorMessage = "Oops, parece que hay algunos problemas en tu formulario. Te sugerimos corregirlos";
            return errorMessage;
        }
    }
}
