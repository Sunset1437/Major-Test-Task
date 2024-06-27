using MajorTestTask.Views;

namespace MajorTestTask;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		RegisterRoutes();
    }
	private void RegisterRoutes()
	{
		Routing.RegisterRoute(nameof(ApplicationPage), typeof(ApplicationPage));
        Routing.RegisterRoute(nameof(NewApplicationPage), typeof(NewApplicationPage));
    }
}
