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
                new Item("Tomate italiano", 40, "tomate_italiano.png", ItemsInfos.UnitTypes.kg, ItemsInfos.Categories.Legumes),
                new Item("Cebola branca", 4, "cebola_branca.png", ItemsInfos.UnitTypes.kg, ItemsInfos.Categories.Hortaliças),
                new Item("Cebola roxa", 3, "cebola_roxa.png", ItemsInfos.UnitTypes.kg, ItemsInfos.Categories.Hortaliças),
                new Item("Manjericão", 5, "manjericao.png", ItemsInfos.UnitTypes.und, ItemsInfos.Categories.Temperos),
                new Item("Pimentão vermelho", 1, "pimentao_vermelho.png", ItemsInfos.UnitTypes.kg, ItemsInfos.Categories.Legumes),
                new Item("Pimentão amarelo", 1, "pimentao_amarelo.png", ItemsInfos.UnitTypes.kg, ItemsInfos.Categories.Legumes),
                new Item("Pimenta dedo de moça", 3, "pimenta_dedo_de_moca.png", ItemsInfos.UnitTypes.und, ItemsInfos.Categories.Legumes),
                new Item("Maça", 2, "maca.png", ItemsInfos.UnitTypes.kg, ItemsInfos.Categories.Frutas),
                new Item("Alface", 5, "alface.png", ItemsInfos.UnitTypes.und, ItemsInfos.Categories.Hortaliças),
                new Item("Rúcula", 5, "rucula.png", ItemsInfos.UnitTypes.und, ItemsInfos.Categories.Hortaliças),
                new Item("Farinha", 10, "farinha.png", ItemsInfos.UnitTypes.kg, ItemsInfos.Categories.Outros),
                new Item("Tomate cereja", 2, "tomate_cereja.png", ItemsInfos.UnitTypes.kg, ItemsInfos.Categories.Legumes),
                new Item("Batata", 3, "batata.png", ItemsInfos.UnitTypes.kg, ItemsInfos.Categories.Hortaliças),
                new Item("Batata baroa", 10, "batata_baroa.png", ItemsInfos.UnitTypes.kg, ItemsInfos.Categories.Hortaliças),
                new Item("Flores", 10, "flores.png", ItemsInfos.UnitTypes.und, ItemsInfos.Categories.Outros),
                new Item("Salsão", 10, "salsao.png", ItemsInfos.UnitTypes.und, ItemsInfos.Categories.Legumes),
                new Item("Limão", 30, "limao.png", ItemsInfos.UnitTypes.kg, ItemsInfos.Categories.Frutas),
                new Item("Tomilho", 8, "tomilho.png", ItemsInfos.UnitTypes.und, ItemsInfos.Categories.Temperos),
                new Item("Alecrim", 8, "alecrim.png", ItemsInfos.UnitTypes.und, ItemsInfos.Categories.Temperos),
                new Item("Laranja Bahia", 4, "laranja_bahia.png", ItemsInfos.UnitTypes.und, ItemsInfos.Categories.Frutas),
                new Item("Limão siciliano", 6, "limao_siciliano.png", ItemsInfos.UnitTypes.und, ItemsInfos.Categories.Frutas),
                new Item("Hortelã", 2, "hortela.png", ItemsInfos.UnitTypes.und, ItemsInfos.Categories.Temperos),
                new Item("Abacaxi", 4, "abacaxi.png", ItemsInfos.UnitTypes.und, ItemsInfos.Categories.Frutas),

            };
        }

        public ObservableCollection<ItemGroup> GetAll() => itemGroups;
    }
}
