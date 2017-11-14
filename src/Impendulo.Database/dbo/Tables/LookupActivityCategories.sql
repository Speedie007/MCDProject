CREATE TABLE [dbo].[LookupActivityCategories] (
    [ActvityCategoryID]    INT          IDENTITY (1, 1) NOT NULL,
    [ActivityCategory]     VARCHAR (50) NOT NULL,
    [ActivityCategoryCode] VARCHAR (20) CONSTRAINT [DF_LookupActivityCategories_ActivityCode] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_LookupActivityCategories] PRIMARY KEY CLUSTERED ([ActvityCategoryID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_LookupActivityCategories]
    ON [dbo].[LookupActivityCategories]([ActivityCategory] ASC);

