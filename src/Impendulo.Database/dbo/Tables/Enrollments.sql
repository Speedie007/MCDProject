CREATE TABLE [dbo].[Enrollments] (
    [EnrollmentID]                    INT  IDENTITY (1, 1) NOT NULL,
    [EnrolmentParentID]               INT  CONSTRAINT [DF_Enrollments_EnrolmentParentID_1] DEFAULT ((0)) NOT NULL,
    [EnquiryID]                       INT  CONSTRAINT [DF_Enrollments_EnquiryID] DEFAULT ((9197)) NOT NULL,
    [ProgressFileID]                  INT  CONSTRAINT [DF_Enrollments_ProgressFileID] DEFAULT ((1)) NOT NULL,
    [EnrollmentExcempt]               BIT  CONSTRAINT [DF_Enrollments_EnrollmentExcempt] DEFAULT ((0)) NULL,
    [LookupEnrollmentProgressStateID] INT  NOT NULL,
    [CurriculumID]                    INT  NOT NULL,
    [DateIntitiated]                  DATE CONSTRAINT [DF_Enrollments_DateIntitiated] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Enrollments] PRIMARY KEY CLUSTERED ([EnrollmentID] ASC),
    CONSTRAINT [FK_Enrollments_Curriculums] FOREIGN KEY ([CurriculumID]) REFERENCES [dbo].[Curriculums] ([CurriculumID]),
    CONSTRAINT [FK_Enrollments_Enquiry] FOREIGN KEY ([EnquiryID]) REFERENCES [dbo].[Enquiry] ([EnquiryID]),
    CONSTRAINT [FK_Enrollments_LookupEnrollmentProgressStates] FOREIGN KEY ([LookupEnrollmentProgressStateID]) REFERENCES [dbo].[LookupEnrollmentProgressStates] ([LookupEnrollmentProgressStateID]),
    CONSTRAINT [FK_Enrollments_ProgressFiles] FOREIGN KEY ([ProgressFileID]) REFERENCES [dbo].[ProgressFiles] ([ProgressFileID])
);















