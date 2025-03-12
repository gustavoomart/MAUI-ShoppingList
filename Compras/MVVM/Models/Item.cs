using CommunityToolkit.Mvvm.ComponentModel;
using Compras.MVVM.Models;

namespace Compras.MVVM.Models;
public partial class Item : ObservableObject {
    public Item(string name, float amount, string icon, ItemsInfos.UnitTypes unit, ItemsInfos.Categories category) {
        Id = Guid.NewGuid();
        Name = name;
        Amount = amount;
        Icon = icon;
        Unit = unit;
        Category = category;
    }
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Icon { get; set; }
    public ItemsInfos.Categories Category { get; set; }

    public float Amount { get => _amount; set => SetProperty(ref _amount, value); }
    private float _amount;
    public ItemsInfos.UnitTypes Unit { get => _unit; set => SetProperty(ref _unit, value); }
    private ItemsInfos.UnitTypes _unit;

}
