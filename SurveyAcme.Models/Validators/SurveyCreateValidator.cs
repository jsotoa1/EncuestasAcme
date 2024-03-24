using FluentValidation;
using SurveyAcme.Models.Inputs;

namespace SurveyAcme.Models.Validators
{
    public  class SurveyCreateValidator : AbstractValidator<SurveyCreateIn>
    {
        public SurveyCreateValidator()
        {
            //NameRules();
            //EmailRules();
            //UsernameRules();
            //PasswordRules();
            //GreaterThanRule();
        }

        public void NameRules()
        {
            int min = 3, max = 50;

            RuleFor(validator => validator.SurveyName)
                .NotNull().WithMessage("Debe ingresar un nombre")
                .Length(min, max).WithMessage($"El nombre debe contener entre {min} y {max} caracteres")
                .Matches("^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$").WithMessage("El formato del nombre es incorrecto, considerar usar letras y tildes");

            RuleFor(validator => validator.SurveyDescription)
                .NotNull().WithMessage("Debe ingresar un apellido")
                .Length(min, max).WithMessage($"El nombre debe contener entre {min} y {max} caracteres")
                .Matches("^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$").WithMessage("El formato del nombre es incorrecto, considerar usar letras y tildes");
        }

        //public void EmailRules()
        //{
        //    int min = 0, max = 50;

        //    RuleFor(user => user.Email)
        //        .NotNull().WithMessage("Debe ingresar el correo electrónico")
        //        .Length(min, max).WithMessage($"El correo electrónico debe contener máximo {max} caracteres")
        //        .EmailAddress().WithMessage("El correo electrónico no cumple con el formato");
        //}

        //public void UsernameRules()
        //{
        //    int min = 8, max = 50;

        //    RuleFor(user => user.Username)
        //        .NotNull().WithMessage("Debe ingresar el usuario")
        //        .Length(min, max).WithMessage($"El usuario debe contener entre {min} y {max} caracteres")
        //        .Matches("^(?=.*[a-zA-Z0-9%$#@]).{8,}$").WithMessage("El formato del usuario es incorrecto, considerar usar letras mayúsculas, minúsculas y números");
        //}

        //public void PasswordRules()
        //{
        //    int min = 8, max = 50;

        //    RuleFor(user => user.Password)
        //        .NotNull().WithMessage("Debe ingresar una contraseña")
        //        .Length(min, max).WithMessage($"La contraseña debe contener entre {min} y {max} caracteres")
        //        .Matches("^(?=.*[A-Z])(?=.*[a-z])(?=.*[\\d!@#$%^&*()_+|~\\-]).{8,}$")
        //        .WithMessage("La contraseña debe contener mínimo una mayúscula, una minúscula y un carácter especial o número");
        //}

        //public void GreaterThanRule()
        //{
        //    RuleFor(user => user.ExpirationTime)
        //        .NotNull().WithMessage("Debe ingresar la cantidad de días de vencimiento de la contraseña")
        //        .GreaterThan(0).WithMessage("La cantidad de días de vencimiento de la contraseña debe ser mayor a cero");

        //    RuleFor(user => user.CompanyId)
        //        .NotNull().WithMessage("Debe ingresar el Id de la compañía o empresa")
        //        .GreaterThan(0).WithMessage("Debe ingresar el Id de la compañía o empresa");

        //    RuleFor(user => user.RoleUserId)
        //        .NotNull().WithMessage("Debe ingresar el Id del rol del usuario")
        //        .GreaterThan(0).WithMessage("Debe ingresar el Id del rol del usuario");

        //    RuleFor(user => user.RoleCompanyId)
        //        .NotNull().WithMessage("Debe ingresar el Id del rol que ocupa en la compañía o empresa")
        //        .GreaterThan(0).WithMessage("Debe ingresar el Id del rol que ocupa en la compañía o empresa");
        //}
    }
}
