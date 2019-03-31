using System.Data.Entity.Migrations;
using DDD.Infrastructure.Data.Context;

namespace EP.CursoMvc.Infrastructure.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<EFContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(EFContext context)
        {
        }
    }
}
