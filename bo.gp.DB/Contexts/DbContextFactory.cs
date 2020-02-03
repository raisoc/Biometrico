using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace bo.gp.DB.Contexts
{
    public class DbContextFactory : IDesignTimeDbContextFactory<DBContexto>
    {


        private  string connectionString;
        Conector conector { get; set; }
        public DBContexto CreateDbContext(Conector conector)
        {
            this.conector = conector;
            return CreateDbContext(null);
        }

        public DBContexto CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<DBContexto>();
            switch (conector)
            {
                case Conector.SqlServer:
                    {
                        LoadConnectionString();
                        builder.UseSqlServer(connectionString);

                    }
                    break;
                case Conector.MySql:
                    {
                        LoadConnectionString();
                        builder.UseMySql(connectionString);

                    }
                    break;
                default:
                    break;
            }
            return new DBContexto(builder.Options);
        }
        
     
        private  void LoadConnectionString()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: false);

            var configuration = builder.Build();
            switch (conector)
            {
                case Conector.SqlServer:
                    {
                        connectionString = configuration.GetConnectionString("DBSQLServer");
                    }
                    break;
                case Conector.MySql:
                    {
                        connectionString = configuration.GetConnectionString("DBMySQL");
                    }
                    break;
                default:
                    break;
            }


        }
    }
}
