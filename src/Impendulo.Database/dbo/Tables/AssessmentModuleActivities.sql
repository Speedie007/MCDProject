CREATE TABLE [dbo].[AssessmentModuleActivities] (
    [AssessmentModuleActivityID] INT IDENTITY (1, 1) NOT NULL,
    [AssessmentModuleID]         INT NOT NULL,
    [ActivityID]                 INT NOT NULL,
    CONSTRAINT [PK_AssessmentModuleActivities] PRIMARY KEY CLUSTERED ([AssessmentModuleActivityID] ASC),
    CONSTRAINT [FK_AssessmentModuleActivities_Activities] FOREIGN KEY ([ActivityID]) REFERENCES [dbo].[Activities] ([ActivityID]),
    CONSTRAINT [FK_AssessmentModuleActivities_AssessmentModules] FOREIGN KEY ([AssessmentModuleID]) REFERENCES [dbo].[AssessmentModules] ([AssessmentModuleID])
);

