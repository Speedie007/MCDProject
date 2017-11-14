CREATE TABLE [dbo].[OnSiteSchedules] (
    [ScheduleID]       INT      NOT NULL,
    [VenueID]          INT      NOT NULL,
    [DateLastModified] DATETIME CONSTRAINT [DF_OnSiteSchedules_DateLastModified] DEFAULT (((1)-(1))-(1)) NOT NULL,
    CONSTRAINT [PK_OnSiteSchedules] PRIMARY KEY CLUSTERED ([ScheduleID] ASC),
    CONSTRAINT [FK_OnSiteSchedules_Schedules] FOREIGN KEY ([ScheduleID]) REFERENCES [dbo].[Schedules] ([ScheduleID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_OnSiteSchedules_Venues] FOREIGN KEY ([VenueID]) REFERENCES [dbo].[Venues] ([VenueID])
);





