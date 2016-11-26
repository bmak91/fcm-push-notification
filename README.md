FCM Push Notifications Service
===

**DISCLAIMER:** This is still a pre-release version that doesn't support all features of FCM.

Injectable service library to send push notifications using FCM from an ASP.Net Core MVC Application.

## Setup

* Download the project and include it as a reference in your ASP.Net Core MVC app.

## Usage

- [Setup your Firebase project](https://firebase.google.com/docs/cloud-messaging/)

- In your Startup.cs file, add the below to the `ConfigureServices` method:

	```
	public void ConfigureServices(IServiceCollection services)
	{
		...

		services.AddDbContext<NotifServerDbContext>(options =>
			{
				options.UseSqlServer(CONNECTION_STRING, builder =>
				{
					builder.MigrationsAssembly(ASSEMBLY_NAME);
				});
			});

		services.AddFCMPushNotificationService(options => {
			options.FCMServerToken = FCM_SERVER_TOKEN;
		});

		...
	}
	```

	3 things to note in the above code:

	- *_CONNECTION_STRING_*: The connection string to your database. A good practice is to store it in your `appsettings.json` file and then access it using the `Configuration` object. ex: `Configuration.GetConnectionString("DefaultConnection")`

	- *_ASSEMBLY_NAME_*: This library will create a UserDeviceTokens table in your database using code migrations. Since your project and the library each belong to a different assembly, you need to pass your assembly name for it to work. You can use `nameof(_SOLUTION_NAME_)`	if you don't want to hardcode it.

	- *_FCM_SERVER_TOKEN_*: After creating your Firebase project, you can find your server token by going to Project Settings > Cloud Messaging in the console.

- After making sure your project compiles, apply the migrations using `dotnet ef database update --context NotifServerDbContext`

- In your controllers, you can take advantage of DI:

	```
	public class MyController : Controller
	{
		private readonly IPushNotificationService _notifService;

		public MyController(IPushNotificationService notifService)
		{
			_notifService = notifService;
			...
		}

		...
	}
	```

## API

The `IPushNotificationService` interface defines 3 methods:

```
Task NotifyAsync(NotificationRequest request);

Task RegisterUserAsync(string userId, string regToken, DevicePlatform platform);

Task UnregisterUserAsync(string userId, string regToken);
```

