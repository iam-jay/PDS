using Microsoft.EntityFrameworkCore;
using PDS.Models;
using ConfigurationManager = PDS.Helpers.ConfigurationManager;

namespace PDS.Data
{
    public class DataContext: DbContext
    {
        public DbSet<PatientData> PatientData { get; set; }
        public DbSet<ClientData> ClientData { get; set; }
        public DbSet<ConsentResponseData> ConsentResponseData { get; set; }
        public DbSet<AuthenticationData> AuthenticationData { get; set; }
        public DbSet<AuthorisedData> AuthorisedData { get; set; }

        public DbSet<ConsultationData> ConsultationData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseCosmos(ConfigurationManager.AppSetting["ConnectionStrings:Uri"],
                ConfigurationManager.AppSetting["ConnectionStrings:Key"],
                ConfigurationManager.AppSetting["ConnectionStrings:DB"]);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PatientData>().ToContainer("PatientData").HasPartitionKey(x => x.GUID);
            modelBuilder.Entity<ClientData>().ToContainer("ClientData").HasPartitionKey(x => x.GUID);
            modelBuilder.Entity<ConsultationData>().ToContainer("ConsultationData").HasPartitionKey(x => x.GUID);
            modelBuilder.Entity<ConsentResponseData>().ToContainer("ConsultationData").HasPartitionKey(x => x.GUID);
            modelBuilder.Entity<AuthenticationData>().ToContainer("AuthenticationData").HasPartitionKey(x => x.GUID);
            modelBuilder.Entity<AuthorisedData>().ToContainer("AuthorisedData").HasPartitionKey(x => x.GUID);
        }
    }
    
}
