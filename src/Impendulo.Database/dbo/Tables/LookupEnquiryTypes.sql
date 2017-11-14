CREATE TABLE [dbo].[LookupEnquiryTypes] (
    [EquiryTypeID] INT          IDENTITY (1, 1) NOT NULL,
    [EnquiryType]  VARCHAR (50) CONSTRAINT [DF_LookupEnquiryTypes_EnquiryType] DEFAULT ('Student') NOT NULL,
    CONSTRAINT [PK_LookupEnquiryTypes] PRIMARY KEY CLUSTERED ([EquiryTypeID] ASC)
);




GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_LookupEnquiryTypes]
    ON [dbo].[LookupEnquiryTypes]([EnquiryType] ASC);

