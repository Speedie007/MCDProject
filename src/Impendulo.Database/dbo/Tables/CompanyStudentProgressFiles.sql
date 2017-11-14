CREATE TABLE [dbo].[CompanyStudentProgressFiles] (
    [CompanyStudentProgressFileID] INT IDENTITY (1, 1) NOT NULL,
    [CompanyProgressFileID]        INT NOT NULL,
    [StudentProgressFileID]        INT NOT NULL,
    CONSTRAINT [PK_CompanyStudentProgressFiles] PRIMARY KEY CLUSTERED ([CompanyStudentProgressFileID] ASC),
    CONSTRAINT [FK_CompanyStudentProgressFiles_CompanyProgressFiles] FOREIGN KEY ([CompanyProgressFileID]) REFERENCES [dbo].[CompanyProgressFiles] ([CompanyProgressFileID]),
    CONSTRAINT [FK_CompanyStudentProgressFiles_StudentProgressFiles] FOREIGN KEY ([StudentProgressFileID]) REFERENCES [dbo].[StudentProgressFiles] ([StudentProgressFileID])
);

