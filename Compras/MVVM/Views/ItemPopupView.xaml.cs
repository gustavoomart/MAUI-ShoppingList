using CommunityToolkit.Maui.Views;
using Compras.MVVM.Models;
using Compras.MVVM.ViewModels;

namespace Compras.MVVM.Views;

public partial class ItemPopupView : Popup
{
	public ItemPopupView(Item item)
	{
		InitializeComponent();

        BindingContext = new ItemPopupViewModel(this, item);
    }
}