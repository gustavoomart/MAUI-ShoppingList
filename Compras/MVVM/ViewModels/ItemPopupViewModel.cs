using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Compras.MVVM.Models;
using Compras.Services;
using System.Collections.ObjectModel;

namespace Compras.MVVM.ViewModels;

public partial class ItemPopupViewModel : ObservableObject
{
    SelectedItem _item;
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
    public string Description { get => _description; set => SetProperty(ref _description, value); }
    private string _description = string.Empty;
    public Unit SelectedUnit {
        get => _selectedUnit;
        set => SetProperty(ref _selectedUnit, value);
    }
    private Unit _selectedUnit = new();
    public ObservableCollection<Unit> UnitTypes { get => _unitTypes; set => SetProperty(ref _unitTypes, value); } 
    ObservableCollection<Unit> _unitTypes = new();
    private Database? _db;

    public string Name {
        get => _item.Name;
    }
    public ItemPopupViewModel(Database database, Popup popup, SelectedItem item) {
        _db = database;
        _item = item;
        _popup = popup;
        _amount = item.Amount;
        Description = item.Description;
        LoadUnitTypes();
    }
    private async void LoadUnitTypes() {
        if (_db != null) {
            UnitTypes = new ObservableCollection<Unit>(await _db.GetUnitsAsync());

            // Após carregar as unidades, forçar match da instância do SelectedUnit
            var unitMatch = UnitTypes.FirstOrDefault(u => u.Id == _item.Unit.Id);
            if (unitMatch != null) {
                SelectedUnit = unitMatch;
            }
        }
    }

    [RelayCommand]
    private async Task AddItem() {
        SelectedItem item = new SelectedItem {
            Name = _item.Name,
            Amount = _amount,
            Description = Description,
            Unit = SelectedUnit
        };
        await _popup.CloseAsync(item);
    }
}