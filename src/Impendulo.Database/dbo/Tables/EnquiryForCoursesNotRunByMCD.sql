CREATE TABLE [dbo].[EnquiryForCoursesNotRunByMCD] (
    [EnquiryForCoursesNotRunByMCDID] INT           IDENTITY (1, 1) NOT NULL,
    [EnquiryForCoursesNotRunByMCD]   VARCHAR (250) NOT NULL,
    [EnquiryID]                      INT           NOT NULL,
    CONSTRAINT [FK_EnquiryForCoursesNotRunByMCD_Enquiry] FOREIGN KEY ([EnquiryID]) REFERENCES [dbo].[Enquiry] ([EnquiryID])
);

