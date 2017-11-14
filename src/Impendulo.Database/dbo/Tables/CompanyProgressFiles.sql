CREATE TABLE [dbo].[CompanyProgressFiles] (
    [CompanyProgressFileID] INT  NOT NULL,
    [CompanyID]             INT  NOT NULL,
    [DateLastUpdated]       DATE NOT NULL,
    CONSTRAINT [PK_CompanyProgressFiles] PRIMARY KEY CLUSTERED ([CompanyProgressFileID] ASC),
    CONSTRAINT [FK_CompanyProgressFiles_Companies] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[Companies] ([CompanyID]),
    CONSTRAINT [FK_CompanyProgressFiles_ProgressFiles] FOREIGN KEY ([CompanyProgressFileID]) REFERENCES [dbo].[ProgressFiles] ([ProgressFileID])
);

