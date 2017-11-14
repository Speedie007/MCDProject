CREATE TABLE [dbo].[StudentEnrollments] (
    [EnrollmentID] INT  NOT NULL,
    [IndividualID] INT  NOT NULL,
    [DateUpdated]  DATE CONSTRAINT [DF_StudentEnrollments_DateUpdated] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_StudentEnrollments] PRIMARY KEY CLUSTERED ([EnrollmentID] ASC),
    CONSTRAINT [FK_StudentEnrollments_Enrollments] FOREIGN KEY ([EnrollmentID]) REFERENCES [dbo].[Enrollments] ([EnrollmentID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_StudentEnrollments_Students] FOREIGN KEY ([IndividualID]) REFERENCES [dbo].[Students] ([IndividualID])
);



