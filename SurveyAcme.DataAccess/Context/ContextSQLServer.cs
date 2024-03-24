using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SurveyAcme.DataAccess.Entities;

namespace SurveyAcme.DataAccess.Context
{
    public class ContextSQLServer : ContextBase<ContextSQLServer>, IAppDbContext
    {

        public ContextSQLServer(DbContextOptions<ContextSQLServer> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DbContext GetDbContext()
        {
            return this;
        }
    }
}
