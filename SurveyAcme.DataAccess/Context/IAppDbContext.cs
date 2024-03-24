using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using SurveyAcme.DataAccess.Entities;

namespace SurveyAcme.DataAccess.Context
{
    public interface IAppDbContext
    {
        #region Propiedades DBContext
        DatabaseFacade Database { get; }
        #endregion

        #region Funciones DBContext

        // Guardar Registros
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        // Consultas SQL crudas
        IQueryable<TEntity> FromSqlRaw<TEntity>(string sql, params object[] parameters) where TEntity : class;
        IQueryable<TEntity> FromSqlInterpolated<TEntity>(FormattableString sql) where TEntity : class;

        // Consultas SQL crudas sin seguimiento
        IQueryable<TEntity> FromSqlRawNoTracking<TEntity>(string sql, params object[] parameters) where TEntity : class;
        IQueryable<TEntity> FromSqlInterpolatedNoTracking<TEntity>(FormattableString sql) where TEntity : class;

        // Ejecución de comandos SQL
        Task<int> ExecuteSqlCommand(string sql, params object[] parameters);

        //Obtener el contexto
        DbContext GetDbContext();
        #endregion

        #region Entidades de Base de Datos
        public DbSet<Survey> Survey { get; set; }
        public DbSet<SurveyField> SurveyField { get; set; }
        public DbSet<SurveyFieldData> SurveyFieldData { get; set; }
        #endregion

    }
}
