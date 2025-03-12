﻿using System.Globalization;

namespace Compras.Converters;

public class BoolToExpandIconConverter : IValueConverter {
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {
        return (value is bool b && b) ? "▼" : "▶";
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) {
        throw new NotImplementedException();
    }
}
