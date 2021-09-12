using cohesionPractice.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cohesionPractice.Contexts
{
    public class ServiceRequestContext : DbContext
    {
        public DbSet<ServiceRequest> ServiceRequests { get; set; }

        public ServiceRequestContext(DbContextOptions<ServiceRequestContext> options): base(options)
        {
            //Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ServiceRequest>().HasData(
                 new ServiceRequest()
                 {
                     Id = new Guid("727b376b-79ae-498e-9cff-a9f51b848ea4"),
                     BuildingCode = "COH",
                     Description = "Please turn up the AC in suite 1200D. It is too hot here.",
                     CurrentStatus = (CurrentStatusDTO)1,
                     CreatedBy = "Ricky Martin",
                     CreatedDate = new DateTime(2019, 08, 01, 14, 01, 23, 1),
                     LastModifiedBy = "Eric Iglesias",
                     LastModifiedDate = new DateTime(2019, 09, 01, 14, 01, 23, 1)
                 }
                ,
                new ServiceRequest()
                {
                    Id = new Guid("727b376b-79ae-498e-9cff-a9f51b848ea5"),
                    BuildingCode = "ACH",
                    Description = "Take me home country roads cause It is freezing cold in here! So turn down the AC!",
                    CurrentStatus = (CurrentStatusDTO)1,
                    CreatedBy = "John Denver",
                    CreatedDate = new DateTime(2020, 09, 01, 14, 01, 23, 122),
                    LastModifiedBy = "Jane Tech support",
                    LastModifiedDate = new DateTime(2020, 12, 01, 14, 01, 23, 1)
                },
                new ServiceRequest()
                {
                    Id = new Guid("727b376b-79ae-498e-9cff-a9f51b848ea6"),
                    BuildingCode = "ATC",
                    Description = "Music Box is broken",
                    CurrentStatus = (CurrentStatusDTO)1,
                    CreatedBy = "Mick Jager",
                    CreatedDate = new DateTime(2019, 08, 01, 14, 01, 23, 1),
                    LastModifiedBy = "Matt White",
                    LastModifiedDate = new DateTime(2019, 09, 01, 14, 01, 23, 1)
                }
                ,
                new ServiceRequest()
                {
                    Id = new Guid("727b376b-79ae-498e-9cff-a9f51b848ea7"),
                    BuildingCode = "AFC",
                    Description = "I can feel something in the air",
                    CurrentStatus = (CurrentStatusDTO)2,
                    CreatedBy = "Elton John",
                    CreatedDate = new DateTime(2020, 09, 01, 14, 01, 23, 122),
                    LastModifiedBy = "Jack Black",
                    LastModifiedDate = new DateTime(2020, 12, 01, 14, 01, 23, 1)
                }





                );
            base.OnModelCreating(modelBuilder);
        }
    }
}
