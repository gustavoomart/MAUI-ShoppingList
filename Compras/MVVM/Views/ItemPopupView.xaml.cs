using CommunityToolkit.Maui.Views;
using Compras.MVVM.Models;
using Compras.MVVM.ViewModels;
using System.Diagnostics;

namespace Compras.MVVM.Views;

public partial class ItemPopupView : Popup
{
	public ItemPopupView(Item item)
	{
		InitializeComponent();

        BindingContext = new ItemPopupViewModel(this, item);
    }

    private async void Entry_Focused(object sender, FocusEventArgs e) {
        if (sender is Entry entry) {
            Debug.WriteLine($"_____________________{entry.Text}___________________________");

            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                await Task.Delay(200); // Um pequeno delay costuma ser suficiente (100~300ms)

                entry.CursorPosition = 0;
                entry.SelectionLength = entry.Text?.Length ?? 0;
            });
        }
    }

}