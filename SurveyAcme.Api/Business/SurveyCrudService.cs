using SurveyAcme.Api.Data;
using SurveyAcme.DataAccess.Context;
using SurveyAcme.Models.Inputs;
using SurveyAcme.Models.Outputs;
using SurveyAcme.Models.Utilities;
using System.Net;

namespace SurveyAcme.Api.Business
{
    public class SurveyCrudService
    {
        private readonly IAppDbContext _context;
        private readonly SurveyCrudData _surveyCrudData;

        public SurveyCrudService(IAppDbContext context)
        {
            _context = context;
            _surveyCrudData = new(context);
        }

        public async Task<ResultOut> RegisterSurvey(SurveyCreateIn request)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var survey = await _surveyCrudData.RegisterSurvey(request);
                await transaction.CommitAsync();
                return survey;
            }
            catch (CustomException)
            {
                await transaction.RollbackAsync();
                throw;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new CustomException("Ocurrió un problema al intentar registrar la encuesta", ex.ToString(), HttpStatusCode.InternalServerError);
            }
        }

        public async Task<List<SurveyOut>> ListSurvey()
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var survey = await _surveyCrudData.ListSurvey();
                await transaction.CommitAsync();
                return survey;
            }
            catch (CustomException)
            {
                await transaction.RollbackAsync();
                throw;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new CustomException("Ocurrió un problema al intentar listar.", ex.ToString(), HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ResultOut> InsertSurvey(List<SurveyFieldInsert> fields)
        {
            TypeValid(fields);
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var survey = await _surveyCrudData.InsertSurvey(fields);
                await transaction.CommitAsync();
                return survey;
            }
            catch (CustomException)
            {
                await transaction.RollbackAsync();
                throw;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new CustomException("Ocurrió un problema al intentar registrar los datos de la encuesta", ex.ToString(), HttpStatusCode.InternalServerError);
            }
        }

        public bool TypeValid(List<SurveyFieldInsert> fields)
        {
            bool result = false;
            try
            {
                fields.ForEach(field =>
                {
                    result = !ValueValid((DataType)Enum.Parse(typeof(DataType), field.Type), field.Value) ? throw new CustomException("Ocurrió un problema al intentar validar el tipo de dato, Unicos disponibles Texto, Numero y Fecha", HttpStatusCode.InternalServerError) : true;
                });
            }
            catch (Exception ex)
            {
                throw new CustomException("Ocurrió un problema al intentar validar el tipo de dato, Unicos disponibles Texto, Numero y Fecha", ex.ToString(), HttpStatusCode.InternalServerError);
            }
            return result;
        }

        public bool ValueValid(DataType type, string value)
        {
            switch (type)
            {
                case DataType.Texto:
                    return !string.IsNullOrWhiteSpace(value) && value.All(char.IsLetter);
                case DataType.Numero:
                    return decimal.TryParse(value, out _);
                case DataType.Fecha:
                    return DateTime.TryParse(value, out _);
                default: return false;
            }
        }

        //public async Task<bool> UpdateSurvey(SurveyCreateIn request)
        //{
        //        return await _surveyCrudData.UpdateSurvey(request);
        //}

        public TokenOut GenerateJwtToken()
        {
            JwtService jwtService = new();
            TokenOut token = jwtService.GenerateToken(20, "");
            return token;
        }

    }
}
