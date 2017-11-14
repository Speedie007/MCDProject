CREATE TABLE [dbo].[LookupAssessmentRecommendations] (
    [AssessmentRecommendationID] INT          IDENTITY (1, 1) NOT NULL,
    [AssessmentRecommendation]   VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_LookupAssessmentRecommendations] PRIMARY KEY CLUSTERED ([AssessmentRecommendationID] ASC)
);




GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_LookupAssessmentRecommendations]
    ON [dbo].[LookupAssessmentRecommendations]([AssessmentRecommendation] ASC);

