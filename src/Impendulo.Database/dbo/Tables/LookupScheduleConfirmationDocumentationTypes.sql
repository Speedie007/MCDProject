CREATE TABLE [dbo].[LookupScheduleConfirmationDocumentationTypes] (
    [ScheduleConfirmationDocumentationTypeID] INT          IDENTITY (1, 1) NOT NULL,
    [ScheduleConfirmationDocumentationType]   VARCHAR (50) NULL,
    CONSTRAINT [PK_LookupScheduleConfirmationDocumentationTypes] PRIMARY KEY CLUSTERED ([ScheduleConfirmationDocumentationTypeID] ASC)
);

