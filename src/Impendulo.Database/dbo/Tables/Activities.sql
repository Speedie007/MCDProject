CREATE TABLE [dbo].[Activities] (
    [ActivityID]           INT            IDENTITY (1, 1) NOT NULL,
    [ActivityCode]         VARCHAR (20)   CONSTRAINT [DF_Activities_ActivityCode] DEFAULT ((0)) NOT NULL,
    [ActvityCategoryID]    INT            CONSTRAINT [DF_Activities_ActvityCategoryID] DEFAULT ((1)) NOT NULL,
    [ActivitiyDescription] VARCHAR (2000) CONSTRAINT [DF_Activities_ActivitiyDescription] DEFAULT ('None') NOT NULL,
    CONSTRAINT [PK_Activities] PRIMARY KEY CLUSTERED ([ActivityID] ASC),
    CONSTRAINT [FK_Activities_LookupActivityCategories] FOREIGN KEY ([ActvityCategoryID]) REFERENCES [dbo].[LookupActivityCategories] ([ActvityCategoryID])
);








GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_ActivitiesCode]
    ON [dbo].[Activities]([ActivityCode] ASC);

