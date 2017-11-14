CREATE TABLE [dbo].[OffSiteSchedule] (
    [ScheduleID]       INT      NOT NULL,
    [AddressID]        INT      NOT NULL,
    [DateLastModified] DATETIME CONSTRAINT [DF_OffSiteSchedule_DateLastModified] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_OffSiteSchedule] PRIMARY KEY CLUSTERED ([ScheduleID] ASC),
    CONSTRAINT [FK_OffSiteSchedule_Addresses] FOREIGN KEY ([AddressID]) REFERENCES [dbo].[Addresses] ([AddressID]),
    CONSTRAINT [FK_OffSiteSchedule_Schedules] FOREIGN KEY ([ScheduleID]) REFERENCES [dbo].[Schedules] ([ScheduleID]) ON DELETE CASCADE ON UPDATE CASCADE
);







