using Microsoft.EntityFrameworkCore;
using SimbirSoft_Latfullin.Domain.Entities;
using System;

namespace SimbirSoft_Latfullin.Domain
{
    public class ApplicationContext : DbContext
    {
        public DbSet<UniqueResult> UniqueResults { get; set; }


        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        { }
    }
}
