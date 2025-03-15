using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Compras.MVVM.Models;
using Compras.MVVM.Views;
using Compras.Services;
using System.Collections.ObjectModel;

namespace Compras.MVVM.ViewModels;

public partial class CreateListViewModel : ObservableObject {
    public ObservableCollection<SelectedItem> SelectedItems { get; } = [];

    public DateTime Date { get => _date; set => SetProperty(ref _date, value); }
    private DateTime _date;
    private readonly double bottonMenuOpenedHeight = 500;
    private ObservableCollection<ItemGroup> itemGroups = new();
    public ObservableCollection<ItemGroup> ItemGroups { get => itemGroups; set => SetProperty(ref itemGroups, value); }
    Database _db;
    //public bool ListItemsIsExpanded { get => _listItemsIsExpanded; set => SetProperty(ref _listItemsIsExpanded, value);}
    //private bool _listItemsIsExpanded = false;
    public CreateListViewModel(Database database) {
        Date = DateTime.Now;
        _db = database;
    }
    public async void LoadItems() {
        ItemGroups = await _db.GetAllAppItemsGroupsAsync();

        var selectedItems = await _db.GetSelectedItemsAsync();
        SelectedItems.Clear();
        foreach (SelectedItem item in selectedItems) {
            SelectedItems.Add(item);
        }
    }

    [RelayCommand] private static void ToggleGroup(ItemGroup group) => group.IsExpanded = !group.IsExpanded;

    [RelayCommand] private async Task ItemClicked(Item item) {
        SelectedItem sModel = new SelectedItem {
            Name = item.Name,
            Amount = item.Amount,
            Description = item.Description,
            Unit = item.Unit
        };
        var popUp = new ItemPopupView(_db, sModel);

        var mainWindow = Application.Current?.Windows[0];
        if (mainWindow?.Page is not Page page)
            return;

        var result = await page.ShowPopupAsync(popUp);
        if (result is SelectedItem newItem) {
            SelectedItems.Add(newItem);
            await _db.InsertSelectedItem(newItem);
        }
    }
    [RelayCommand] private async Task EditItemFromList(SelectedItem item) {

        var popUp = new ItemPopupView(_db, item);

        var mainWindow = Application.Current?.Windows[0];
        if (mainWindow?.Page is not Page page)
            return;

        var result = await page.ShowPopupAsync(popUp);
        if (result is SelectedItem newItem) {

            if (item != null) {
                item.Amount = newItem.Amount;
                item.Unit = newItem.Unit;
                item.Description = newItem.Description;
                await _db.UpdateSelectedItem(item);
            }
        }
    }
    [RelayCommand] private async Task ShareClicked() {
        string formattedDate = Date.ToString("dd/MM");

        var lines = SelectedItems.Select(item =>
        {
            var descriptionPart = !string.IsNullOrWhiteSpace(item.Description)
                ? $" ({item.Description})"
                : string.Empty;

            return $"• {item.Name}{descriptionPart} - {item.Amount} {item.Unit.Name}";
        });

        var text = $"{formattedDate}\n\n{string.Join(Environment.NewLine, lines)}";

        await Share.Default.RequestAsync(new ShareTextRequest {
            Title = "Minha lista de compras",
            Text = text
        });
    }
    [RelayCommand] private async Task DeleteListItem(SelectedItem? item) {
        if (item == null) return;
        SelectedItems.Remove(item);
        await _db.RemoveSelectedItem(item);
    }
    [RelayCommand] private static void AdjustItemSize(Grid grid) {
        grid.HeightRequest = grid.Width;
    }
    [RelayCommand] private void ToggleItemsListPanel(CollectionView vs_itensList) {
        double targetHeight = vs_itensList.HeightRequest == 0 ? bottonMenuOpenedHeight : 0;
   //     ListItemsIsExpanded = !ListItemsIsExpanded;
        var animation = new Animation(
            d => vs_itensList.HeightRequest = d,
            vs_itensList.HeightRequest,
            targetHeight,
            Easing.SpringIn);

        animation.Commit(vs_itensList, "ToggleHeightRequest", 16, 500);
    }
}
