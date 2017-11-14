CREATE TABLE [dbo].[Assessments] (
    [AssessmentID]                 INT  IDENTITY (1, 1) NOT NULL,
    [CurriculumCourseEnrollmentID] INT  NOT NULL,
    [AssessmentDateCompleted]      DATE CONSTRAINT [DF_Assessments_AssessmentDateCompleted] DEFAULT (getdate()) NOT NULL,
    [AssessmentDateStarted]        DATE CONSTRAINT [DF_Assessments_AssessmentDateStarted] DEFAULT (getdate()) NOT NULL,
    [AssessmentDateSubmitted]      DATE CONSTRAINT [DF_Assessments_AssessmentDateSubmitted] DEFAULT (getdate()) NOT NULL,
    [AssessmentRecommendationID]   INT  CONSTRAINT [DF_Assessments_AssessmentRecommendationID] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_Assessments] PRIMARY KEY CLUSTERED ([AssessmentID] ASC),
    CONSTRAINT [FK_Assessments_CurriculumCourseEnrollments] FOREIGN KEY ([CurriculumCourseEnrollmentID]) REFERENCES [dbo].[CurriculumCourseEnrollments] ([CurriculumCourseEnrollmentID]),
    CONSTRAINT [FK_Assessments_LookupAssessmentRecommendations] FOREIGN KEY ([AssessmentRecommendationID]) REFERENCES [dbo].[LookupAssessmentRecommendations] ([AssessmentRecommendationID])
);

