using Compras.MVVM.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace Compras.Services {
    public class Database {
        private ObservableCollection<ItemGroup> itemGroups = new();

        public Database() {
            SeedDatabase();
        }

        private void SeedDatabase() {
            var allItems = GetAllItems();
            var groups = allItems
                .GroupBy(i => i.Category)
                .Select(g => new ItemGroup(g.Key.ToString(), g.ToList()))
                .ToList();

            foreach (var group in groups)
                itemGroups.Add(group);
        }

        private ObservableCollection<Item> GetAllItems() {
            return new ObservableCollection<Item>
            {
                new Item("Maça", 10, "apple.svg", ItemsInfos.UnitTypes.und, ItemsInfos.Categories.Frutas),
                new Item("Uva", 10, "grape.svg", ItemsInfos.UnitTypes.und, ItemsInfos.Categories.Frutas),
                new Item("Abacaxi", 10, "pineapple.svg", ItemsInfos.UnitTypes.und, ItemsInfos.Categories.Frutas),
                new Item("Morango", 10, "strawberry.svg", ItemsInfos.UnitTypes.und, ItemsInfos.Categories.Frutas),
                new Item("Melância", 10, "watermelon.svg", ItemsInfos.UnitTypes.und, ItemsInfos.Categories.Frutas),
                new Item("Alho", 10, "garlic.svg", ItemsInfos.UnitTypes.und, ItemsInfos.Categories.Hortículas),
                new Item("Alface", 10, "lettuce.svg", ItemsInfos.UnitTypes.und, ItemsInfos.Categories.Hortículas),
                new Item("Pão", 40, "bread.svg", ItemsInfos.UnitTypes.und, ItemsInfos.Categories.Padaria),
                new Item("Farinha", 5, "flour.svg", ItemsInfos.UnitTypes.kg, ItemsInfos.Categories.Padaria)
            };
        }

        public ObservableCollection<ItemGroup> GetAll() => itemGroups;
    }
}
