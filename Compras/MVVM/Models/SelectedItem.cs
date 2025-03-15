using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using System.Text.Json;

namespace Compras.MVVM.Models;

public partial class SelectedItem : ObservableObject {
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public float Amount {
        get => _amount;
        set => SetProperty(ref _amount, value);
    }
    private float _amount;

    public string Description {
        get => _description;
        set => SetProperty(ref _description, value);
    }
    private string _description = string.Empty;

    public string UnitJson { get; set; } = string.Empty;

    [Ignore]
    public Unit Unit {
        get {
            if (string.IsNullOrWhiteSpace(UnitJson))
                return new Unit { Name = "Unknown" };

            try {
                return JsonSerializer.Deserialize<Unit>(UnitJson) ?? new Unit { Name = "Unknown" };
            }
            catch {
                return new Unit { Name = "Unknown" };
            }
        }

        set {
            UnitJson = JsonSerializer.Serialize(value);
            OnPropertyChanged(nameof(Unit));
        }
    }

}
