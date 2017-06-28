FCM Push Notifications Service (v 1.0.0)
===

Injectable service library to send push notifications using FCM from an ASP.Net Core MVC Application.

## API

The `IPushNotificationService` interface defines 3 methods:

```
Task<List<NotificationResult>> NotifyAsync(NotificationRequest request);

Task RegisterUserAsync(string userId, string regToken, DevicePlatform platform);

Task UnregisterUserAsync(string userId, string regToken);
```

## Installation

* Add [the NuGet package](https://www.nuget.org/packages/ACB.FCMPushNotifications) to your project

## Setup

- [Setup your Firebase project](https://firebase.google.com/docs/cloud-messaging/)

- In your Startup.cs file, add the below to the `ConfigureServices` method:

	```
	public void ConfigureServices(IServiceCollection services)
	{
		...

		services.AddDbContext<NotifServerDbContext>(builder =>
			builder.UseSqlServer(CONNECTION_STRING, options =>
				options.MigrationsAssembly(ASSEMBLY_NAME))
		);

		services.AddFCMPushNotificationService(options => {
			options.FCMServerToken = FCM_SERVER_TOKEN;
		});

		...
	}
	```

	3 things to note in the above code:

	- *_CONNECTION_STRING_*: The connection string to your database. A good practice is to store it in your `appsettings.json` file and then access it using the `Configuration` object. ex: `Configuration.GetConnectionString("DefaultConnection")`

	- *_ASSEMBLY_NAME_*: This library will create a UserDeviceTokens table in your database using code migrations. Since your project and the library each belong to a different assembly, you need to pass your assembly name for it to work. You can use `typeof(Startup).GetTypeInfo().Assembly.GetName().Name` to avoid hardcoding it.

	- *_FCM_SERVER_TOKEN_*: After creating your Firebase project, you can find your server token by going to Project Settings > Cloud Messaging in the console.

- Create the database migration using `dotnet ef migrations add devices --context NotifServerDbContext`. See `dotnet ef migrations add --help` for more options.

- Apply the database migration using `dotnet ef database update --context NotifServerDbContext`.

## Usage

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

		public async Task MyMethod()
		{
			try
			{
				var results = await _notifService.NotifyAsync(new NotificationRequest
				{
					...
				});

				// Inspect the results list here
			}
			catch (Exception e)
			{
				// The library throws an exception when FCM returns
				// a 400, 401, or 50x HttpStatusCode
			}
		}
	}
	```

Inspect the list of `NotificationResult` to check if the message was sent successfully. 

_Note_: Since a single user might have more than one device registered (ex. android phone + iPad), the list returned does NOT necessarily have the same length of the userId's list passed to NotificationRequest.

When FCM sends an updated device token, the service will automatically handle removing the old token and saving the new one. Likewise, if a token is no longer valid, it will be automatically removed.

## Release Notes

**v 1.0.1**

- Added a check in `RegisterUserAsync` to avoid a duplicate key exception.

**v 1.0.0**

- This is a breaking change. If you had v 0.9.x betas installed, create a new migration and apply it to the database (this can potentially cause you to lose some data)

- The `NotifyAsync` method now returns a list of result objects
