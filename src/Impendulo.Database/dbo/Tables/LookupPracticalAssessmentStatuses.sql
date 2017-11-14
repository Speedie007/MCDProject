CREATE TABLE [dbo].[LookupPracticalAssessmentStatuses] (
    [PracticalAssessmentStatusID]   INT          IDENTITY (1, 1) NOT NULL,
    [PracticalAssessmentStatus]     VARCHAR (50) NOT NULL,
    [PracticalAssessmentStatusCode] VARCHAR (5)  NOT NULL,
    CONSTRAINT [PK_LookupPracticalAssessmentStatuses] PRIMARY KEY CLUSTERED ([PracticalAssessmentStatusID] ASC)
);

