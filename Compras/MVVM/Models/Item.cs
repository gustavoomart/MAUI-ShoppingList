using CommunityToolkit.Mvvm.ComponentModel;

namespace Compras.MVVM.Models;
public partial class Item : ObservableObject {
    public Item(string name, float amount, string icon, ItemsInfos.UnitTypes unit, ItemsInfos.Categories category, string? description = null) {
        Id = Guid.NewGuid();
        Name = name;
        Amount = amount;
        Icon = icon;
        Unit = unit;
        Category = category;
        if (description != null) Description = description;
    }
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Icon { get; set; }
    public ItemsInfos.Categories Category { get; set; }

    public float Amount { get => _amount; set => SetProperty(ref _amount, value); }
    private float _amount;
    public string Description { get => _description; set => SetProperty(ref _description, value); }
    private string _description = string.Empty;
    public ItemsInfos.UnitTypes Unit { get => _unit; set => SetProperty(ref _unit, value); }
    private ItemsInfos.UnitTypes _unit;

}
