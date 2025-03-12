using Compras.MVVM.ViewModels;

namespace Compras.MVVM.Views;

public partial class CreateListPage : ContentPage {
    public CreateListPage() {
        InitializeComponent();
        BindingContext = new CreateListViewModel();
    }

    private void bottonButtons_SizeChanged(object sender, EventArgs e) {
        if (sender is Grid grid) {
            cv_ItemsGroupsFooter.HeightRequest = grid.Height;

        }
    }
}
