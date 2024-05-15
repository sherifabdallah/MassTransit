using Microsoft.EntityFrameworkCore;
using Contracts;

namespace SenderApi.Data
{
    public class MessageDbContext : DbContext
    {
        public string connectionString = "Server=SherifAbdullah\\MSSQLSERVER01;Database=DotNetCourseDatabase;TrustServerCertificate=true;Trusted_Connection=true;";



        private readonly IConfiguration _config;

        public MessageDbContext(IConfiguration config)
        {
            _config = config;
        }

        public DbSet<RequestResponseMessage> RequestResponseMessages { get; set; }

        // Model Creation
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.HasDefaultSchema("SchemaName"); // Schema name
            modelBuilder.Entity<RequestResponseMessage>().ToTable("RequestResponseMessage").HasKey(u => u.Id);

        }

        // Configurations
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // If optionsBuilder is not Configured.
            if(!optionsBuilder.IsConfigured) {
                    optionsBuilder.UseSqlServer(connectionString, optionsBuilder => optionsBuilder.EnableRetryOnFailure());
            }
            
        }
    }
}