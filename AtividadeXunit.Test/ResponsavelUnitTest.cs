using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using AtividadeXunit.Api.Models;
using AtividadeXunit.Api.Controllers;

namespace ResponsavelXunit.Test
{
    public class ResponsavelUnitTest
    {
        private DbContextOptions<AtividadeContext> options;

        private void InitializeDataBase()
        {
            // Create a Temporary Database
            options = new DbContextOptionsBuilder<AtividadeContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            // Insert data into the database using one instance of the context
            using (var context = new AtividadeContext(options))
            {
                context.Responsavel.Add(new Responsavel { Id = 1, Nome = "Saulo" });
                context.Responsavel.Add(new Responsavel { Id = 2, Nome = "Hugo" });
                context.Responsavel.Add(new Responsavel { Id = 3, Nome = "Thiago" });
                context.SaveChanges();
            }
        }

        [Fact]
        public void GetAll()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new AtividadeContext(options))
            {
                ResponsavelController responsavelController = new ResponsavelController(context);
                IEnumerable<Responsavel> responsavels = responsavelController.GetResponsavel().Result.Value;

                Assert.Equal(3, responsavels.Count());
            }
        }

        [Fact]
        public void GetbyId()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new AtividadeContext(options))
            {
                int ativiId = 2;
                ResponsavelController responsavelController = new ResponsavelController(context);
                Responsavel responsavel = responsavelController.GetResponsavel(ativiId).Result.Value;
                Assert.Equal(2, responsavel.Id);
            }
        }

        [Fact]
        public void Create()
        {
            InitializeDataBase();

            Responsavel responsavel = new Responsavel()
            {
                Id = 4,
                Nome = "Saulo"

            };

            // Use a clean instance of the context to run the test
            using (var context = new AtividadeContext(options))
            {
                ResponsavelController responsavelController = new ResponsavelController(context);
                Responsavel at = responsavelController.PostResponsavel(responsavel).Result.Value;
                Assert.Equal(4, responsavel.Id);
            }
        }

        [Fact]
        public void Update()
        {
            InitializeDataBase();

            Responsavel responsavel = new Responsavel()
            {
                Id = 1,
                Nome = "Saulo"

            };

            // Use a clean instance of the context to run the test
            using (var context = new AtividadeContext(options))
            {
                ResponsavelController responsavelController = new ResponsavelController(context);
                Responsavel at = responsavelController.PutResponsavel(4, responsavel).Result.Value;
                Assert.Equal("Saulo", responsavel.Nome);
            }
        }
        [Fact]
        public void Delete()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new AtividadeContext(options))
            {
                ResponsavelController responsavelController = new ResponsavelController(context);
                Responsavel responsavel = responsavelController.DeleteResponsavel(2).Result.Value;
                Assert.Null(responsavel);
            }
        }
    }
}
