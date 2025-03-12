using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Compras.MVVM.Models;

namespace Compras.MVVM.Models;

public partial class ItemGroup : ObservableObject {
    public string CategoryName { get; }
    private ObservableCollection<Item> _allItems;

    [ObservableProperty]
    private bool isExpanded = true;

    public ObservableCollection<Item> VisibleItems => IsExpanded
        ? _allItems
        : new ObservableCollection<Item>();

    public ItemGroup(string categoryName, IEnumerable<Item> items) {
        CategoryName = categoryName;
        _allItems = new ObservableCollection<Item>(items);
    }

    partial void OnIsExpandedChanged(bool value) {
        OnPropertyChanged(nameof(VisibleItems));
    }
}
