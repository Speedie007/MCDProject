CREATE TABLE [dbo].[StudentProgressFiles] (
    [StudentProgressFileID] INT  NOT NULL,
    [StudentID]             INT  NOT NULL,
    [DateLastUpdated]       DATE NOT NULL,
    CONSTRAINT [PK_StudentProgressFiles] PRIMARY KEY CLUSTERED ([StudentProgressFileID] ASC),
    CONSTRAINT [FK_StudentProgressFiles_ProgressFiles] FOREIGN KEY ([StudentProgressFileID]) REFERENCES [dbo].[ProgressFiles] ([ProgressFileID]),
    CONSTRAINT [FK_StudentProgressFiles_Students] FOREIGN KEY ([StudentID]) REFERENCES [dbo].[Students] ([IndividualID])
);

