using Compras.Services;

namespace Compras
{
    public partial class App : Application
    {
        public App(IServiceProvider services)
        {
            InitializeComponent();
            Task.Run(async () =>
            {
                var db = services.GetService<Database>();
                if (db != null) {
                    await db.InitializeAsync();
                }
            });
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}