using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyCollection.Domain.Entities;

namespace MyCollection.Data.Seeders
{
    public static class DataSeeders
    {
        public static async Task ApplySeeders(IServiceProvider serviceProvider)
        {

            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<MyCollectionContext>();
                if (context.Database.IsRelational())
                {
                    var connections = context.Database.GetDbConnection();
                    Console.WriteLine("======= CRIANDO BANCO DE DADOS: {0} =======", connections.ConnectionString);
                }

                bool criado = context.Database.EnsureCreated();
                Console.WriteLine("======= BANCO DE DADOS CRIADO: {0} =======", criado);
                var itemsList = new List<CollectionItem>();
                Console.WriteLine("====== APLICANDO SEEDERS =========");

                Random rnd = new Random();
                itemsList.Add(new CollectionItem("O Programador Apaixonado", "Chad Fowler", rnd.Next(1, 10), "Professional", EType.BOOK));
                itemsList.Add(new CollectionItem("Domain Driven Design: Atacando as complexidades no coração do software", "Eric Evans", rnd.Next(1, 10), "Professional", EType.BOOK));
                itemsList.Add(new CollectionItem("Código Limpo: Habilidades práticas do Agile Software", "Robert Cecil", rnd.Next(1, 10), "Professional", EType.BOOK));
                itemsList.Add(new CollectionItem("O Programador Pragmático: De Aprendiz a Mestre", "Dave Thomas e Andy Hunt", rnd.Next(1, 10), "Professional", EType.BOOK));
                itemsList.Add(new CollectionItem("Design Patterns: Elements of Reusable Object-Oriented Software", "Erich Gamma", rnd.Next(1, 10), "Professional", EType.BOOK));

                itemsList.Add(new CollectionItem("Pink Floyd", "The Piper at the Gates of Dawn", rnd.Next(1, 10), "1967", EType.CD));
                itemsList.Add(new CollectionItem("Black Sabbath", "Sabotage", rnd.Next(1, 10), "1975", EType.CD));
                itemsList.Add(new CollectionItem("Red Hot Chili Peppers", "Blood Sugar Sex Magik", rnd.Next(1, 10), "1991", EType.CD));
                itemsList.Add(new CollectionItem("Kiss", "Destroyer", rnd.Next(1, 10), "1976", EType.CD));
                itemsList.Add(new CollectionItem("Bob Dylan", "Highway 61 Revisited", rnd.Next(1, 10), "1965", EType.CD));

                itemsList.Add(new CollectionItem("Deep Purple", "Burn ", rnd.Next(1, 10), "1974", EType.DVD));
                itemsList.Add(new CollectionItem("Radiohed ", "OK Computer", rnd.Next(1, 10), "1997", EType.DVD));
                itemsList.Add(new CollectionItem("Iron Maiden", "Seventh Son of a Seventh Son", rnd.Next(1, 10), "1986", EType.DVD));
                itemsList.Add(new CollectionItem("AC/DC", "Powerage", rnd.Next(1, 10), "1976", EType.DVD));
                itemsList.Add(new CollectionItem("U2", "The Joshua Tree", rnd.Next(1, 10), "1987", EType.DVD));

                foreach (var item in itemsList)
                    if (!await context.CollectionItems!.AnyAsync(x => x.Title == item.Title))
                        await context.CollectionItems!.AddAsync(item);

                var locationsList = new List<Location>
                {
                    new Location("PRT 1", "Prateleira 1", null, 0),
                    new Location("PRT 2", "Prateleira 2", null, 0),
                    new Location("PRT 3", "Prateleira 3", null, 0),
                    new Location("PRT 4", "Prateleira 4", null, 0),
                    new Location("PRT 5", "Prateleira 5", null, 0)
                };

                foreach (var item in locationsList)
                    if (!await context.Locations!.AnyAsync(x => x.Description == item.Description))
                        await context.Locations!.AddAsync(item);

                await context.SaveChangesAsync();
            }

            Console.WriteLine("====== SEEDERS FINALIZADOS =========");
        }
    }
}
