CREATE TABLE [dbo].[LookupProgessFileTypes] (
    [ProgressFileTypeID] INT          IDENTITY (1, 1) NOT NULL,
    [ProgressFileType]   VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_LookupProgessFileTypes] PRIMARY KEY CLUSTERED ([ProgressFileTypeID] ASC)
);

