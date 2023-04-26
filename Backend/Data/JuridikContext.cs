using JuridikApp.Models;
using Microsoft.EntityFrameworkCore;

namespace JuridikApp.Data
{
    public class JuridikContext : DbContext
    {
        public JuridikContext(DbContextOptions<JuridikContext> options) : base(options)
        {
        }

        public virtual DbSet<Query> Queries { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("JuridikContext"));
        }
    }
}