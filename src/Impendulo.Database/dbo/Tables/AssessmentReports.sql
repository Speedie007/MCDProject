CREATE TABLE [dbo].[AssessmentReports] (
    [AssessmentReportID] INT      IDENTITY (1, 1) NOT NULL,
    [AssessmentID]       INT      NOT NULL,
    [ImageID]            INT      NOT NULL,
    [DateReportUploaded] DATETIME CONSTRAINT [DF_AssessmentReports_DateReportUploaded] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_AssessmentReports] PRIMARY KEY CLUSTERED ([AssessmentReportID] ASC),
    CONSTRAINT [FK_AssessmentReports_Assessments] FOREIGN KEY ([AssessmentID]) REFERENCES [dbo].[Assessments] ([AssessmentID]) ON DELETE CASCADE,
    CONSTRAINT [FK_AssessmentReports_Files] FOREIGN KEY ([ImageID]) REFERENCES [dbo].[Files] ([ImageID]) ON DELETE CASCADE
);

