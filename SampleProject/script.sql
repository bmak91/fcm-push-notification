IF OBJECT_ID(N'__EFMigrationsHistory') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171121204807_devices')
BEGIN
    CREATE TABLE [UserDeviceTokens] (
        [Token] nvarchar(450) NOT NULL,
        [Platform] int NOT NULL,
        [UserId] nvarchar(max) NULL,
        CONSTRAINT [PK_UserDeviceTokens] PRIMARY KEY ([Token])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171121204807_devices')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20171121204807_devices', N'2.0.0-rtm-26452');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171121205522_devices2')
BEGIN
    ALTER TABLE [UserDeviceTokens] DROP CONSTRAINT [PK_UserDeviceTokens];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171121205522_devices2')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'UserDeviceTokens') AND [c].[name] = N'UserId');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [UserDeviceTokens] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [UserDeviceTokens] ALTER COLUMN [UserId] nvarchar(450) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171121205522_devices2')
BEGIN
    ALTER TABLE [UserDeviceTokens] ADD CONSTRAINT [PK_UserDeviceTokens] PRIMARY KEY ([UserId], [Token]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171121205522_devices2')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20171121205522_devices2', N'2.0.0-rtm-26452');
END;

GO

