using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Compras.MVVM.Models;
using Compras.Services;
using System.Collections.ObjectModel;
using Compras.MVVM.Views;
using System.Diagnostics;
using Microsoft.Maui.Controls;

namespace Compras.MVVM.ViewModels;

public partial class CreateListViewModel : ObservableObject {
    public ObservableCollection<Item> ItemsList { get; } = [];

    public DateTime Date { get => _date; set => SetProperty(ref _date, value); }
    private DateTime _date;
    private readonly double bottonMenuOpenedHeight = 500;
    private ObservableCollection<ItemGroup> itemGroups;
    public ObservableCollection<ItemGroup> ItemGroups {
        get => itemGroups;
        set => SetProperty(ref itemGroups, value);
    }

    public CreateListViewModel() {
        Date = DateTime.Now;
        var db = new Database();
        itemGroups = db.GetAll();
    }

    [RelayCommand] private static void ToggleGroup(ItemGroup group) => group.IsExpanded = !group.IsExpanded;

    [RelayCommand] private async Task ItemClicked(Item item) {
        if (item == null) return;

        var popUp = new ItemPopupView(item);

        var mainWindow = Application.Current?.Windows[0];
        if (mainWindow?.Page is not Page page)
            return;

        var result = await page.ShowPopupAsync(popUp);
        if (result is Item newItem) {
            ItemsList.Add(newItem);
        }
    }
    [RelayCommand] private async Task EditItemFromList(Item? item) {
        if (item == null) return;

        var popUp = new ItemPopupView(item);

        var mainWindow = Application.Current?.Windows[0];
        if (mainWindow?.Page is not Page page)
            return;

        var result = await page.ShowPopupAsync(popUp);
        if (result is Item newItem) {
            var itemToEdit = ItemsList.FirstOrDefault(i => i.Id == item.Id);

            if (itemToEdit != null) {
                itemToEdit.Amount = newItem.Amount;
                itemToEdit.Unit = newItem.Unit;
                itemToEdit.Description = newItem.Description;
            }

        }
    }
    [RelayCommand] private async Task ShareClicked() {
        string formattedDate = Date.ToString("dd/MM");

        var lines = ItemsList.Select(item =>
        {
            var descriptionPart = !string.IsNullOrWhiteSpace(item.Description)
                ? $" ({item.Description})"
                : string.Empty;

            return $"• {item.Name}{descriptionPart} - {item.Amount} {item.Unit}";
        });

        var text = $"{formattedDate}\n\n{string.Join(Environment.NewLine, lines)}";

        await Share.Default.RequestAsync(new ShareTextRequest {
            Title = "Minha lista de compras",
            Text = text
        });
    }

    [RelayCommand] private void DeleteListItem(Item? item) {
        if (item == null) return;
        ItemsList.Remove(item);
    }
    [RelayCommand] private static void AdjustItemSize(Grid grid) {
        grid.HeightRequest = grid.Width;
    }
    [RelayCommand] private void ToggleItemsListPanel(CollectionView vs_itensList) {
        double targetHeight = vs_itensList.HeightRequest == 0 ? bottonMenuOpenedHeight : 0;

        var animation = new Animation(
            d => vs_itensList.HeightRequest = d,
            vs_itensList.HeightRequest,
            targetHeight,
            Easing.SpringIn);

        animation.Commit(vs_itensList, "ToggleHeightRequest", 16, 500);
    }
}
