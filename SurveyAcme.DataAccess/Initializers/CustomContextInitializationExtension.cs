using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SurveyAcme.DataAccess.Context;

namespace SurveyAcme.DataAccess.Initializers
{
    public static class CustomContextInitializationExtension
    {
        public static void AddCustomDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            //string conectionString = configuration["ConnectionStrings:CadenaConexion"].DecryptByAES();
            string conectionString = configuration["ConnectionStrings:CadenaConexion"];
            string tipoBaseDatos = configuration["ConnectionStrings:TipoBaseDatos"];

            try
            {
                switch (tipoBaseDatos)
                {
                    case "SQLServer":
                        services.AddDbContext<IAppDbContext, ContextSQLServer>(options => options.UseSqlServer(conectionString), ServiceLifetime.Scoped);
                        break;
                    case "Postgres":
                        break;
                    case "MySQL":
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static void AddAutomaticMigrate(this IServiceScope scope, IConfiguration configuration)
        {
            string tipoBaseDatos = configuration["ConnectionStrings:TipoBaseDatos"];

            try
            {
                IAppDbContext context = null;
                switch (tipoBaseDatos)
                {
                    case "SQLServer":
                        context = scope.ServiceProvider.GetService<ContextSQLServer>();
                        break;
                    case "Postgres":
                        break;
                    case "MySQL":
                        break;
                }

                context?.Database.Migrate();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
