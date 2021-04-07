using AtividadeXunit.Api.Models;
using AtividadeXunit.Api.Controllers;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;
using System.Collections.Generic;
using System.Linq;

namespace AtividadeXunit.Test
{
    public class AtividadeUnitTest
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
                context.Atividade.Add(new Atividade { Id = 1, Descricao = "Correr", DataInicio = DateTime.Now, DataFim = DateTime.Now, Responsavel = new Responsavel { Id = 1, Nome = "Saulo" } });
                context.Atividade.Add(new Atividade { Id = 2, Descricao = "Andar", DataInicio = DateTime.Now, DataFim = DateTime.Now, Responsavel = new Responsavel { Id = 2, Nome = "Hugo" } });
                context.Atividade.Add(new Atividade { Id = 3, Descricao = "Nadar", DataInicio = DateTime.Now, DataFim = DateTime.Now, Responsavel = new Responsavel { Id = 3, Nome = "Saulo" } });
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
                AtividadeController atividadeController = new AtividadeController(context);
                IEnumerable<Atividade> atividades = atividadeController.GetAtividade().Result.Value;

                Assert.Equal(3, atividades.Count());
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
                AtividadeController atividadeController = new AtividadeController(context);
                Atividade atividade = atividadeController.GetAtividade(ativiId).Result.Value;
                Assert.Equal(2, atividade.Id);
            }
        }

        [Fact]
        public void Create()
        {
            InitializeDataBase();

            Atividade atividade = new Atividade()
            {
                Id = 4,
                Descricao = "Pedalar",
                DataInicio = DateTime.Now,
                DataFim = DateTime.Now,
                Responsavel = new Responsavel { 
                    Id = 4, 
                    Nome = "Bola" 
                }
            
            };

            // Use a clean instance of the context to run the test
            using (var context = new AtividadeContext(options))
            {
                AtividadeController atividadeController = new AtividadeController(context);
                Atividade at = atividadeController.PostAtividade(atividade).Result.Value;
                Assert.Equal(4, atividade.Id);
            }
        }

        [Fact]
        public void Update()
        {
            InitializeDataBase();

            Atividade atividade = new Atividade()
            {
                Id = 1,
                Descricao = "Pedalar",
                DataInicio = DateTime.Now,
                DataFim = DateTime.Now,
                Responsavel = new Responsavel { Id = 1, Nome = "Saulo" }

            };

            // Use a clean instance of the context to run the test
            using (var context = new AtividadeContext(options))
            {
                AtividadeController atividadeController = new AtividadeController(context);
                Atividade at = atividadeController.PutAtividade(4, atividade).Result.Value;
                Assert.Equal("Pedalar", atividade.Descricao);
            }
        }
        [Fact]
        public void Delete()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new AtividadeContext(options))
            {
                AtividadeController atividadeController = new AtividadeController(context);
                Atividade atividade = atividadeController.DeleteAtividade(2).Result.Value;
                Assert.Null(atividade);
            }
        }
    }
}
