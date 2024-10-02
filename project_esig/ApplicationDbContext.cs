using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using project_esig.Models;

namespace project_esig
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("name=DbConnection") // substitua pelo nome da sua connection string
        {
        }

        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<PessoaSalario> PessoaSalarios { get; set; }
    }
}