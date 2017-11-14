CREATE TABLE [dbo].[Schedules] (
    [ScheduleID]                   INT  IDENTITY (1, 1) NOT NULL,
    [CurriculumCourseEnrollmentID] INT  NOT NULL,
    [IndividualID]                 INT  NOT NULL,
    [ScheduleStartDate]            DATE CONSTRAINT [DF_Schedules_ScheduleStartDate] DEFAULT (getdate()) NOT NULL,
    [ScheduleCompletionDate]       DATE CONSTRAINT [DF_Schedules_ScheduleStartDate1] DEFAULT (getdate()) NOT NULL,
    [ScheduleStatusID]             INT  NOT NULL,
    [EnrollmentID]                 INT  NOT NULL,
    [LookupScheduleLocationID]     INT  NOT NULL,
    [ScheduleConfirmed]            BIT  CONSTRAINT [DF_Schedules_ScheduleConfirmed] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Schedules] PRIMARY KEY CLUSTERED ([ScheduleID] ASC),
    CONSTRAINT [FK_Schedules_CurriculumCourseEnrollments] FOREIGN KEY ([CurriculumCourseEnrollmentID]) REFERENCES [dbo].[CurriculumCourseEnrollments] ([CurriculumCourseEnrollmentID]) ON DELETE CASCADE,
    CONSTRAINT [FK_Schedules_Enrollments] FOREIGN KEY ([EnrollmentID]) REFERENCES [dbo].[Enrollments] ([EnrollmentID]),
    CONSTRAINT [FK_Schedules_Facilitators] FOREIGN KEY ([IndividualID]) REFERENCES [dbo].[Facilitators] ([IndividualID]),
    CONSTRAINT [FK_Schedules_LookupScheduleLocations] FOREIGN KEY ([LookupScheduleLocationID]) REFERENCES [dbo].[LookupScheduleLocations] ([LookupScheduleLocationID]),
    CONSTRAINT [FK_Schedules_LookupScheduleStatuses] FOREIGN KEY ([ScheduleStatusID]) REFERENCES [dbo].[LookupScheduleStatuses] ([ScheduleStatusID])
);









