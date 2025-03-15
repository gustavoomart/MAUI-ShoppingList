using CommunityToolkit.Maui.Views;
using Compras.MVVM.Models;
using Compras.MVVM.ViewModels;
using Compras.Services;

namespace Compras.MVVM.Views;

public partial class ItemPopupView : Popup
{
    public ItemPopupView(Database database, SelectedItem item)
	{
		InitializeComponent();

        BindingContext = new ItemPopupViewModel(database, this, item);
    }

    private async void Entry_Focused(object sender, FocusEventArgs e) {
        if (sender is Entry entry) {
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                await Task.Delay(200);

                entry.CursorPosition = 0;
                entry.SelectionLength = entry.Text?.Length ?? 0;
            });
        }
    }

}