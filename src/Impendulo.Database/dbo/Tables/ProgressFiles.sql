CREATE TABLE [dbo].[ProgressFiles] (
    [ProgressFileID]     INT  IDENTITY (1, 1) NOT NULL,
    [ProgressFileTypeID] INT  NOT NULL,
    [DateCreated]        DATE CONSTRAINT [DF_ProgressFiles_DateCreated] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_ProgressFiles] PRIMARY KEY CLUSTERED ([ProgressFileID] ASC),
    CONSTRAINT [FK_ProgressFiles_LookupProgessFileTypes] FOREIGN KEY ([ProgressFileTypeID]) REFERENCES [dbo].[LookupProgessFileTypes] ([ProgressFileTypeID])
);

