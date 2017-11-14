CREATE TABLE [dbo].[AssessmentModules] (
    [AssessmentModuleID]            INT           IDENTITY (1, 1) NOT NULL,
    [AssessmentID]                  INT           NOT NULL,
    [CurriculumCourseModuleID]      INT           NOT NULL,
    [PracticalAssessmentStatusID]   INT           NOT NULL,
    [TheoriticalAssessmentStatusID] INT           NOT NULL,
    [Notes]                         VARCHAR (MAX) NULL,
    CONSTRAINT [PK_AssessmentModules] PRIMARY KEY CLUSTERED ([AssessmentModuleID] ASC),
    CONSTRAINT [FK_AssessmentModules_Assessments] FOREIGN KEY ([AssessmentID]) REFERENCES [dbo].[Assessments] ([AssessmentID]) ON DELETE CASCADE,
    CONSTRAINT [FK_AssessmentModules_CurriculumCourseModules] FOREIGN KEY ([CurriculumCourseModuleID]) REFERENCES [dbo].[CurriculumCourseModules] ([CurriculumCourseModuleID]) ON DELETE CASCADE,
    CONSTRAINT [FK_AssessmentModules_LookupPracticalAssessmentStatuses] FOREIGN KEY ([PracticalAssessmentStatusID]) REFERENCES [dbo].[LookupPracticalAssessmentStatuses] ([PracticalAssessmentStatusID]),
    CONSTRAINT [FK_AssessmentModules_LoolupTheoriticalAssesmentStatuses] FOREIGN KEY ([TheoriticalAssessmentStatusID]) REFERENCES [dbo].[LookupTheoriticalAssesmentStatuses] ([TheoriticalAssessmentStatusID])
);

