using CommunityToolkit.Maui.Core.Extensions;
using Compras.MVVM.Models;
using SQLite;
using System.Collections.ObjectModel;

namespace Compras.Services {
    public class Database {
        SQLiteAsyncConnection _itemsConnection;
        public Database() {
            var itemsPath = Path.Combine(FileSystem.AppDataDirectory, "items.db3");
            _itemsConnection = new SQLiteAsyncConnection(itemsPath);
        }
        public async Task InitializeAsync() {
            await _itemsConnection.CreateTableAsync<Item>();
            await _itemsConnection.CreateTableAsync<SelectedItem>();
            await _itemsConnection.CreateTableAsync<Category>();
            await _itemsConnection.CreateTableAsync<Unit>();

            await PopulateDatabaseAsync();
        }

        private async Task PopulateDatabaseAsync() {
            bool isPopulated = Preferences.Get("DatabasePopulated", false);
            if (isPopulated) return;


            Unit kg = new Unit { Name = "kg" };
            Unit g = new Unit { Name = "g" };
            Unit und = new Unit { Name = "und" };
            await InsertUnitAsync(kg);
            await InsertUnitAsync(g);
            await InsertUnitAsync(und);

            string legumes = "legumes";
            string hortalicas = "hortaliças";
            string temperos = "Temperos";
            string frutas = "frutas";
            string outros = "outros";
            await InsertCategoryAsync(legumes);
            await InsertCategoryAsync(hortalicas);
            await InsertCategoryAsync(temperos);
            await InsertCategoryAsync(frutas);
            await InsertCategoryAsync(outros);


            ObservableCollection<Item> appDefaultItems = new ObservableCollection<Item> {
                Item.CreateItem("Tomate italiano", 40, "tomate_italiano.png", kg, legumes),
                Item.CreateItem("Cebola branca", 4, "cebola_branca.png", kg, hortalicas),
                Item.CreateItem("Cebola roxa", 3, "cebola_roxa.png", kg, hortalicas),
                Item.CreateItem("Manjericão", 5, "manjericao.png", und, temperos),
                Item.CreateItem("Pimentão vermelho", 1, "pimentao_vermelho.png", kg, legumes),
                Item.CreateItem("Pimentão amarelo", 1, "pimentao_amarelo.png", kg, legumes),
                Item.CreateItem("Pimenta dedo de moça", 3, "pimenta_dedo_de_moca.png", und, legumes),
                Item.CreateItem("Maça", 2, "maca.png", kg, frutas),
                Item.CreateItem("Alface", 5, "alface.png", und, hortalicas),
                Item.CreateItem("Rúcula", 5, "rucula.png", und, hortalicas),
                Item.CreateItem("Farinha", 10, "farinha.png", kg, outros),
                Item.CreateItem("Tomate cereja", 2, "tomate_cereja.png", kg, legumes),
                Item.CreateItem("Batata", 3, "batata.png", kg, hortalicas),
                Item.CreateItem("Batata baroa", 10, "batata_baroa.png", kg, hortalicas),
                Item.CreateItem("Flores", 10, "flores.png", und, outros),
                Item.CreateItem("Salsão", 10, "salsao.png", und, legumes),
                Item.CreateItem("Limão", 30, "limao.png", kg, frutas),
                Item.CreateItem("Tomilho", 8, "tomilho.png", und, temperos),
                Item.CreateItem("Alecrim", 8, "alecrim.png", und, temperos),
                Item.CreateItem("Laranja Bahia", 4, "laranja_bahia.png", und, frutas),
                Item.CreateItem("Limão siciliano", 6, "limao_siciliano.png", und, frutas),
                Item.CreateItem("Hortelã", 2, "hortela.png", und, temperos),
                Item.CreateItem("Abacaxi", 4, "abacaxi.png", und, frutas),
            };

            await _itemsConnection.InsertAllAsync(appDefaultItems);
            Preferences.Set("DatabasePopulated", true);
        }
        #region CATEGORIES E UNITS
        private async Task InsertCategoryAsync(string categoryName) => await _itemsConnection.InsertAsync(new Category { Name = categoryName });
        private async Task InsertUnitAsync(Unit unit) => await _itemsConnection.InsertAsync(unit);
        public async Task<List<Unit>> GetUnitsAsync() => await _itemsConnection.Table<Unit>().ToListAsync(); 
        #endregion

        public async Task<ObservableCollection<ItemGroup>> GetAllAppItemsGroupsAsync() {
            var allAppItems = await _itemsConnection.Table<Item>().ToListAsync();
            List<ItemGroup> groups = allAppItems
                .GroupBy(i => i.Category)
                .Select(g => new ItemGroup {
                    CategoryName = g.Key.ToString(),
                    AllCategoryItems = g.ToList().ToObservableCollection()
                })
                .ToList();
            return new ObservableCollection<ItemGroup>(groups);
        }
        public async Task InsertItem(Item item) => await _itemsConnection.InsertAsync(item);

        #region SELECTED ITEMS
        public async Task<ObservableCollection<SelectedItem>> GetSelectedItemsAsync() {
            List<SelectedItem> selectedItems = await _itemsConnection.Table<SelectedItem>().ToListAsync();
            return new ObservableCollection<SelectedItem>(selectedItems);
        }
        public async Task<SelectedItem> GetSelectedItemAsync(int id) {
            return await _itemsConnection.Table<SelectedItem>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }
        public async Task InsertSelectedItem(SelectedItem item) {
            await _itemsConnection.InsertAsync(item);
        }
        public async Task RemoveSelectedItem(SelectedItem item) {
            await _itemsConnection.DeleteAsync(item);
        }
        public async Task RemoveAllSelectedItem() {
            await _itemsConnection.DeleteAllAsync<SelectedItem>();
        }
        public async Task UpdateSelectedItem(SelectedItem item) {
            await _itemsConnection.UpdateAsync(item);
        } 
        #endregion
    }
}

