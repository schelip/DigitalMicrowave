using System.Collections.Generic;
using System.Data.Entity.Migrations;
using DigitalMicrowave.Business.Entities;
using DigitalMicrowave.Infrastructure.Data;

namespace DigitalMicrowave.Infrastructure.Data.Migrations
{

    internal sealed class Configuration : DbMigrationsConfiguration<DigitalMicrowaveContext>
    {
        public Configuration()
        {
            MigrationsDirectory = "Infrastructure\\Data\\Migrations";
            MigrationsNamespace = "DigitalMigration.Infrastructure.Data.Migrations";
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DigitalMicrowaveContext context)
        {
            context.HeatingProcedures.AddOrUpdate(hp => hp.Id,
                new HeatingProcedure()
                {
                    Id = 1,
                    Name = "Pipoca",
                    FoodName = "Pipoca (de micro-ondas)",
                    Time = 3 * 60,
                    PowerLevel = 7,
                    HeatingString = "p",
                    Instructions = "Observar o barulho de estouros do milho, caso houver um intervalo de mais de 10 segundos entre um estouro e outro, interrompa o aquecimento."
                },
                new HeatingProcedure()
                {
                    Id = 2,
                    Name = "Leite",
                    FoodName = "Leite",
                    Time = 5 * 60,
                    PowerLevel = 5,
                    HeatingString = "~",
                    Instructions = "Cuidado com aquecimento de líquidos, o choque térmico aliado ao movimento do recipiente pode causar fervura imediata causando risco de queimaduras."
                },
                new HeatingProcedure()
                {
                    Id = 3,
                    Name = "Carnes de boi",
                    FoodName = "Carne em pedaço ou fatias",
                    Time = 14 * 60,
                    PowerLevel = 4,
                    HeatingString = "g",
                    Instructions = "Interrompa o processo na metade e vire o conteúdo com a parte de baixo para cima para o descongelamento uniforme."
                },
                new HeatingProcedure()
                {
                    Id = 4,
                    Name = "Frango",
                    FoodName = "Frango (qualquer corte)",
                    Time = 8 * 60,
                    PowerLevel = 7,
                    HeatingString = "f",
                    Instructions = "Interrompa o processo na metade e vire o conteúdo com a parte de baixo para cima para o descongelamento uniforme."
                },
                new HeatingProcedure()
                {
                    Id = 4,
                    Name = "Feijão",
                    FoodName = "Feijão congelado",
                    Time = 8 * 60,
                    PowerLevel = 9,
                    HeatingString = "o",
                    Instructions = "Deixe o recipiente destampado e em casos de plástico, cuidado ao retirar o recipiente pois o mesmo pode perder resistência em altas temperaturas."
                });
        }
    }
}
