CREATE TABLE [dbo].[IndividualNotifications] (
    [IndividualNotifactionID] INT IDENTITY (1, 1) NOT NULL,
    [IndividualID]            INT NOT NULL,
    [NotificationID]          INT NOT NULL,
    CONSTRAINT [PK_IndividualNotifications] PRIMARY KEY CLUSTERED ([IndividualNotifactionID] ASC),
    CONSTRAINT [FK_IndividualNotifications_Individuals] FOREIGN KEY ([IndividualID]) REFERENCES [dbo].[Individuals] ([IndividualID]),
    CONSTRAINT [FK_IndividualNotifications_Notifications] FOREIGN KEY ([NotificationID]) REFERENCES [dbo].[Notifications] ([NotificationID])
);

