using Compras.MVVM.ViewModels;

namespace Compras.MVVM.Views;

public partial class CreateListPage : ContentPage {
    public CreateListPage(CreateListViewModel vm) {
        InitializeComponent();
        BindingContext = vm;
    }

    private void bottonButtons_SizeChanged(object sender, EventArgs e) {
        if (sender is Grid grid) cv_ItemsGroupsFooter.HeightRequest = grid.Height;
    }
    override protected void OnAppearing() {
        base.OnAppearing();
        var vm = (CreateListViewModel)BindingContext;
        vm.LoadItems();
    }
}
