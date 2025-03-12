using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Compras.MVVM.Models;
using Compras.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Compras.MVVM.ViewModels;

public partial class ItemPopupViewModel : ObservableObject
{
    Item _item;
    Popup _popup;
    public string Amount {
        get {
            return _amount.ToString();
        }

        set {
            if (float.TryParse(value, out float result))
                SetProperty(ref _amount, result);
        }
    }
    private float _amount;

    public ItemsInfos.UnitTypes SelectedUnit {
        get => _selectedUnit;
        set => SetProperty(ref _selectedUnit, value);
    }
    private ItemsInfos.UnitTypes _selectedUnit;
    public ObservableCollection<ItemsInfos.UnitTypes> UnitTypes { get; } =
            new ObservableCollection<ItemsInfos.UnitTypes>(
                Enum.GetValues(typeof(ItemsInfos.UnitTypes)).Cast<ItemsInfos.UnitTypes>());


    public string Name {
        get => _item.Name;
    }
    public ItemPopupViewModel(Popup popup, Item item) {
        _item = item;
        _popup = popup;
        _amount = item.Amount;
        _selectedUnit = item.Unit;
    }

    [RelayCommand]
    private async Task AddItem() {
        Item item = new Item (
            _item.Name,
            _amount,
            _item.Icon,
            SelectedUnit,
            _item.Category
        );
        await _popup.CloseAsync(item);
    }
}