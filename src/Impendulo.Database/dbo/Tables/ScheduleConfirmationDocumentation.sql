CREATE TABLE [dbo].[ScheduleConfirmationDocumentation] (
    [ScheduleConfirmationDocumentationID]     INT IDENTITY (1, 1) NOT NULL,
    [ScheduleID]                              INT NOT NULL,
    [ImageID]                                 INT NOT NULL,
    [ScheduleConfirmationDocumentationTypeID] INT NOT NULL,
    CONSTRAINT [PK_ScheduleConfirmationDocumentation] PRIMARY KEY CLUSTERED ([ScheduleConfirmationDocumentationID] ASC),
    CONSTRAINT [FK_ScheduleConfirmationDocumentation_Files] FOREIGN KEY ([ImageID]) REFERENCES [dbo].[Files] ([ImageID]) ON DELETE CASCADE,
    CONSTRAINT [FK_ScheduleConfirmationDocumentation_LookupScheduleConfirmationDocumentationTypes] FOREIGN KEY ([ScheduleConfirmationDocumentationTypeID]) REFERENCES [dbo].[LookupScheduleConfirmationDocumentationTypes] ([ScheduleConfirmationDocumentationTypeID]),
    CONSTRAINT [FK_ScheduleConfirmationDocumentation_Schedules] FOREIGN KEY ([ScheduleID]) REFERENCES [dbo].[Schedules] ([ScheduleID]) ON DELETE CASCADE
);

