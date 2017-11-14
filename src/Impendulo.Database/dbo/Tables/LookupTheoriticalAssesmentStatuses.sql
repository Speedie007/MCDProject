CREATE TABLE [dbo].[LookupTheoriticalAssesmentStatuses] (
    [TheoriticalAssessmentStatusID]   INT          IDENTITY (1, 1) NOT NULL,
    [TheoriticalAssesmentStatus]      VARCHAR (50) CONSTRAINT [DF_LoolupTheoriticalAssesmentStatuses_TheoriticalAssesmentStatus] DEFAULT ('Compentent') NOT NULL,
    [TheoriticalAssessmentStatusCode] VARCHAR (5)  NOT NULL,
    CONSTRAINT [PK_LoolupTheoriticalAssesmentStatuses] PRIMARY KEY CLUSTERED ([TheoriticalAssessmentStatusID] ASC)
);

