using MajorTestTask.DataBase;
using MajorTestTask.DataBase.Entities;
using Microsoft.Extensions.Logging;

namespace MajorTestTask;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		InitDatabase();
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
	private static async void InitDatabase()
	{
        var cancellationToken = new CancellationToken();
        try
		{
			using (var db = new AppDbContext())
			{
				if(db.Database.EnsureCreated())
				{
					await AddInfoAsync(db, cancellationToken);
				}
			}
		}
		catch (Exception ex) 
		{

		}
	}
	private static async Task AddInfoAsync(AppDbContext db, CancellationToken cancellationToken)
	{
		var applicationInfo = new ApplicationEntity()
		{
			ReceiverAddress = "г.Москва",
			SenderAddress = "г.Брянск",
			TakingDate = new DateTime(2024,10,10),
			Weight = 30,
			Length=10,
			Width=10,
			Height=10,
			Status="Новая",
		};
		await db.Applications.AddAsync(applicationInfo, cancellationToken);
		await db.SaveChangesAsync();
	}
}
