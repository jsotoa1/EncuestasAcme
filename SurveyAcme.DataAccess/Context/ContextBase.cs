using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SurveyAcme.DataAccess.Entities;

namespace SurveyAcme.DataAccess.Context
{
    public class ContextBase<TContext> : DbContext where TContext : DbContext
    {
        #region Propiedades DBContext
        public override DatabaseFacade Database => base.Database;
        #endregion

        public ContextBase(DbContextOptions<TContext> options) : base(options)
        {
        }

        #region Funciones DBContext
        public IQueryable<TEntity> FromSqlRaw<TEntity>(string sql, params object[] parameters) where TEntity : class
        {
            return Set<TEntity>().FromSqlRaw(sql, parameters);
        }

        public IQueryable<TEntity> FromSqlInterpolated<TEntity>(FormattableString sql) where TEntity : class
        {
            return Set<TEntity>().FromSqlInterpolated(sql);
        }

        public IQueryable<TEntity> FromSqlRawNoTracking<TEntity>(string sql, params object[] parameters) where TEntity : class
        {
            return Set<TEntity>().FromSqlRaw(sql, parameters).AsNoTracking();
        }

        public IQueryable<TEntity> FromSqlInterpolatedNoTracking<TEntity>(FormattableString sql) where TEntity : class
        {
            return Set<TEntity>().FromSqlInterpolated(sql).AsNoTracking();
        }

        public async Task<int> ExecuteSqlCommand(string sql, params object[] parameters)
        {
            return await Database.ExecuteSqlRawAsync(sql, parameters);
        }

        #endregion

        #region Entidades de Base de Datos
        public DbSet<Survey> Survey { get; set; }  
        public DbSet<SurveyField> SurveyField { get; set; }
        public DbSet<SurveyFieldData> SurveyFieldData { get; set; }
        #endregion
    }
}
