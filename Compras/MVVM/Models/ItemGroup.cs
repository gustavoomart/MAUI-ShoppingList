using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using System.Collections.ObjectModel;

namespace Compras.MVVM.Models;

public partial class ItemGroup : ObservableObject {
    [PrimaryKey]
    public string CategoryName { get => _categoryName; set => SetProperty(ref _categoryName, value); }
    public string _categoryName = string.Empty;
    public ObservableCollection<Item> AllCategoryItems = new();

    public bool IsExpanded {
        get => isExpanded;
        set {
            OnPropertyChanged(nameof(VisibleItems));
            SetProperty(ref isExpanded, value);
        }
    }
    private bool isExpanded = true;

    public ObservableCollection<Item> VisibleItems => IsExpanded
        ? AllCategoryItems
        : new ObservableCollection<Item>();
}
