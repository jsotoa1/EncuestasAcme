namespace SurveyAcme.Models.Outputs
{
    public class ResultOut
    {
        public ResultOut(int id, bool result, string message, string technicalError, List<string> fieldValidations)
        {
            ID = id;
            Result = result;
            Message = message;
            TechnicalError = technicalError;
            FieldValidations = fieldValidations;
        }

        public ResultOut(int id, bool result, string message, string technicalError)
        {
            ID = id;
            Result = result;
            Message = message;
            TechnicalError = technicalError;
            FieldValidations = null;
        }

        public ResultOut(string message, string technicalError, List<string> fieldValidations)
        {
            ID = 0;
            Result = false;
            Message = message;
            TechnicalError = technicalError;
            FieldValidations = fieldValidations;
        }

        public int ID { get; set; }
        public bool Result { get; set; }
        public string Message { get; set; }
        public string TechnicalError { get; set; }
        public List<string> FieldValidations { get; set; }
    }
}
