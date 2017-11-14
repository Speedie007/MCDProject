CREATE TABLE [dbo].[LookupScheduleLocations] (
    [LookupScheduleLocationID] INT           IDENTITY (1, 1) NOT NULL,
    [ScheduleLocation]         VARCHAR (100) NOT NULL,
    CONSTRAINT [PK_LookupScheduleLocations] PRIMARY KEY CLUSTERED ([LookupScheduleLocationID] ASC)
);

