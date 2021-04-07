using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AtividadeXunit.Api.Models;

    public class AtividadeContext : DbContext
    {
        public AtividadeContext (DbContextOptions<AtividadeContext> options) : base(options)
        {
        }

        public DbSet<AtividadeXunit.Api.Models.Atividade> Atividade { get; set; }

        public DbSet<AtividadeXunit.Api.Models.Responsavel> Responsavel { get; set; }
    }
