CREATE TABLE [dbo].[LookupScheduleStatuses] (
    [ScheduleStatusID] INT          IDENTITY (1, 1) NOT NULL,
    [ScheduleStatus]   VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_LookupScheduleStatuses] PRIMARY KEY CLUSTERED ([ScheduleStatusID] ASC)
);

