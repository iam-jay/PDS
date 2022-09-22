using Microsoft.EntityFrameworkCore;

namespace PDS.Data
{
    public class DataContext: DbContext
    {
        public DbSet<PatientData> PatientData { get; set; }
        public DbSet<ClientData> ClientData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseCosmos("https://patient-db.documents.azure.com:443/",
                "iPlWH61I3Waut6Vlivt02gGFUpK6Jwv7bjkCtJc7dIR9wGJ85sYgtBMaIADUYcrsm3AzpNW3p4RVS8fby2c4Fg==",
                "patient-db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PatientData>().ToContainer("PatientData").HasPartitionKey(x => x.GUID);
            modelBuilder.Entity<ClientData>().ToContainer("ClientData").HasPartitionKey(x => x.GUID);
        }
    }
    
}
