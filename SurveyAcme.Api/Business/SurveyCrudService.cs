using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SurveyAcme.Api.Data;
using SurveyAcme.DataAccess.Context;
using SurveyAcme.Models;
using SurveyAcme.Models.Inputs;
using SurveyAcme.Models.Outputs;
using SurveyAcme.Models.Utilities;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;

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

        public async Task<bool> UpdateSurvey(SurveyCreateIn request)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var update = await _surveyCrudData.UpdateSurvey(request);
                await transaction.CommitAsync();
                return true;
            }
            catch (CustomException)
            {
                await transaction.RollbackAsync();
                throw;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        public TokenOut GenerateJwtToken()
        {
            JwtService jwtService = new();
            TokenOut token = jwtService.GenerateToken( 20, "");
            return token;
        }

    }
}
