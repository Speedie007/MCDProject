CREATE TABLE [dbo].[AssessmentFeedBackForms] (
    [AssessmentID] INT NOT NULL,
    CONSTRAINT [PK_AssessmentFeedBackForms] PRIMARY KEY CLUSTERED ([AssessmentID] ASC),
    CONSTRAINT [FK_AssessmentFeedBackForms_Assessments] FOREIGN KEY ([AssessmentID]) REFERENCES [dbo].[Assessments] ([AssessmentID])
);

