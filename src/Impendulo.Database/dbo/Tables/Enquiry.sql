CREATE TABLE [dbo].[Enquiry] (
    [EnquiryID]                                 INT      IDENTITY (1, 1) NOT NULL,
    [ProgressFileID]                            INT      CONSTRAINT [DF_Enquiry_ProgressFileID] DEFAULT ((1)) NOT NULL,
    [EnquiryDate]                               DATE     CONSTRAINT [DF_Enquiry_EnquiryDate] DEFAULT (getdate()) NOT NULL,
    [EnquiryStatusID]                           INT      CONSTRAINT [DF_Enquiry_EnquiryStatusID] DEFAULT ((1)) NOT NULL,
    [InitialConsultationComplete]               BIT      CONSTRAINT [DF_Enquiry_InitialConsultationComplete] DEFAULT ((0)) NOT NULL,
    [InitialCurriculumEnquiryDocumentationSent] BIT      CONSTRAINT [DF_Enquiry_InitialCurriculumEnquiryDocumentationSent] DEFAULT ((0)) NOT NULL,
    [CurriculumID]                              INT      CONSTRAINT [DF_Enquiry_CurriculumID] DEFAULT ((1)) NOT NULL,
    [LastUpdated]                               DATETIME CONSTRAINT [DF_Enquiry_LastUpdated] DEFAULT (getdate()) NOT NULL,
    [EnrollmentQuanity]                         INT      CONSTRAINT [DF_Enquiry_EnrollmentQuanity] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_Enquiry] PRIMARY KEY CLUSTERED ([EnquiryID] ASC),
    CONSTRAINT [FK_Enquiry_Curriculums] FOREIGN KEY ([CurriculumID]) REFERENCES [dbo].[Curriculums] ([CurriculumID]),
    CONSTRAINT [FK_Enquiry_LookupEnquiryStatuses] FOREIGN KEY ([EnquiryStatusID]) REFERENCES [dbo].[LookupEnquiryStatuses] ([EnquiryStatusID]),
    CONSTRAINT [FK_Enquiry_ProgressFiles] FOREIGN KEY ([ProgressFileID]) REFERENCES [dbo].[ProgressFiles] ([ProgressFileID])
);













