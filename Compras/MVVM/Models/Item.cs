﻿using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using System.Text.Json;

namespace Compras.MVVM.Models;

public partial class Item : ObservableObject {
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Icon { get; set; } = "no_picture_icon.png";
    public string Category { get; set; } = string.Empty;

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



    public static Item CreateItem(string name, float amount, string icon, Unit unit, string category, string? description = null) {
        return new Item {
            Name = name,
            Amount = amount,
            Icon = icon,
            Unit = unit,
            Category = category,
            Description = description ?? string.Empty
        };
    }
}
