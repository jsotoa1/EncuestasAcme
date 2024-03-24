using Microsoft.EntityFrameworkCore;
using SurveyAcme.DataAccess.Context;
using SurveyAcme.Models.Inputs;
using SurveyAcme.Models.Outputs;
using SurveyAcme.Models.Utilities;
using SurveyAcme.Utilities.Helpers;
using System.Net;
using System.Xml.Linq;

namespace SurveyAcme.Api.Data
{
    public class SurveyCrudData
    {
        private readonly IAppDbContext _context;

        public SurveyCrudData(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<SurveyOut>> ListSurvey()
        {
            var survey = await _context.Survey.Include(c => c.SurveyField)

                .Select(c => new SurveyOut()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    Link = c.Link,
                    Fields = c.SurveyField.Select(sf => new Field
                    {
                        Id = sf.Id,
                        Name = sf.Name,
                        Title = sf.Title,
                        Required = sf.Required.ToString(),
                        Type = sf.Type,
                        Data = sf.SurveyFieldData.Select(sd => new FieldData
                        {
                           Id = sd.Id,
                           Name = sd.Name
                        }).ToList()
                    }).ToList()
                })
                .ToListAsync();
            return survey;
        }

        public async Task<ResultOut> RegisterSurvey(SurveyCreateIn request)
        {
            DataAccess.Entities.Survey survey = SetSurvey(request);

            await _context.Survey.AddAsync(survey);

            int rows = await _context.SaveChangesAsync();
            if (rows == 0) throw new CustomException("No fue posible registrar.");

            string surveyLink = await GenerateSurveyLink(survey.Id, survey);

            return new(survey.Id, true, surveyLink, string.Empty);
        }

        private async Task<string> GenerateSurveyLink(int surveyId, DataAccess.Entities.Survey surveyFields)
        {
            string baseUrl = "https://localhost:7006/api/SurveyCrud/InsertSurvey";
            string queryParams = $"surveyId={surveyId}";

            int index = 0;
            foreach (var field in surveyFields.SurveyField)
            {
                queryParams += $"&Fields[{index}].Id={field.Id}";
                queryParams += $"&Fields[{index}].Name={field.Name}";
                queryParams += $"&Fields[{index}].Value={string.Empty}";
                queryParams += $"&Fields[{index}].Title={field.Title}";
                queryParams += $"&Fields[{index}].Required={field.Required}";
                queryParams += $"&Fields[{index}].Type={field.Type}";
                index++;
            }

            Uri uri = new Uri($"{baseUrl}?{queryParams}");
            return uri.AbsoluteUri;
        }

        public async Task<bool> UpdateSurvey(SurveyCreateIn request)
        {
            try
            {
                DataAccess.Entities.Survey surveyFind = await _context.Survey.FirstOrDefaultAsync(c => c.Id == request.Id && !c.Deleted);

                if (surveyFind is null) throw new CustomException("La encuesta que intenta actualizar no existe.", HttpStatusCode.NotFound);

               SetSurveyUpdate(surveyFind, request);
               _context.Survey.Update(surveyFind);

                int rows = await _context.SaveChangesAsync();
                if (rows == 0) throw new CustomException("No fue posible insertar el link.");

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ResultOut> InsertSurvey(List<SurveyFieldInsert> fields)
        {
            foreach (var field in fields)
            {
                DataAccess.Entities.SurveyField surveyFieldFind = await _context.SurveyField.FirstOrDefaultAsync(c => c.Id == int.Parse(field.Id) && !c.Deleted);
                if (surveyFieldFind is null) throw new CustomException("No se encontraron datos.", HttpStatusCode.NotFound);

                SetSurveyFieldData(surveyFieldFind, field);
                _context.SurveyField.Update(surveyFieldFind);

                int rows = await _context.SaveChangesAsync();
                if (rows == 0) throw new CustomException("No fue posible registrar.");
            }
            return new(0, true, "Datos almacenados con exito.", string.Empty);
        }


        #region Set
        private static DataAccess.Entities.Survey SetSurvey(SurveyCreateIn request)
        {
            return new()
            {
                Name = request.SurveyName,
                Description = request.SurveyDescription,
                SurveyField = SetSurveyField(request.ListSurveyFields),
                CreationUser = "REGISTER"
            };
        }

        private void SetSurveyFieldData(DataAccess.Entities.SurveyField surveyFieldFind, SurveyFieldInsert data)
        {
            List<DataAccess.Entities.SurveyFieldData> surveyFielDataList = new();
            DataAccess.Entities.SurveyFieldData surveyFielData = new();          
            surveyFielData.Name = data.Value;
            surveyFielData.CreationUser = "REGISTER";
            surveyFielDataList.Add(surveyFielData);

            surveyFieldFind.SurveyFieldData = surveyFielDataList;
        }

        private static void SetSurveyUpdate(DataAccess.Entities.Survey surveyFind, SurveyCreateIn request)
        {
            surveyFind.Name = surveyFind.Name;
            surveyFind.Description = surveyFind.Description;
            surveyFind.Link = request.Link;
            surveyFind.SurveyField = surveyFind.SurveyField;
            surveyFind.ModificationUser = "REGISTER";
            surveyFind.ModificationDate = TimeZoneHelper.Now;
        }

        private static List<DataAccess.Entities.SurveyField> SetSurveyField(List<SurveyField> surveyFields)
        {
            return surveyFields.Select(surveyField => new DataAccess.Entities.SurveyField
            {
                Name = surveyField.Name,
                Title = surveyField.Title,
                Required = surveyField.Required,
                Type = surveyField.Type,
                CreationUser = "REGISTER"
            }).ToList();
        }
        #endregion Set

    }
}
