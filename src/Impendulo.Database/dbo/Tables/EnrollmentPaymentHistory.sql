CREATE TABLE [dbo].[EnrollmentPaymentHistory] (
    [EnrollmentPaymentHistoryID] INT      IDENTITY (1, 1) NOT NULL,
    [EnrollmentID]               INT      NOT NULL,
    [AmountPaid]                 MONEY    CONSTRAINT [DF_EnrollmentPaymentHistory_AmountPaid] DEFAULT ((0.00)) NOT NULL,
    [DatePaymentMade]            DATETIME CONSTRAINT [DF_EnrollmentPaymentHistory_DatePaymentMade] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_EnrollmentPaymentHistory] PRIMARY KEY CLUSTERED ([EnrollmentPaymentHistoryID] ASC),
    CONSTRAINT [FK_EnrollmentPaymentHistory_Enrollments] FOREIGN KEY ([EnrollmentID]) REFERENCES [dbo].[Enrollments] ([EnrollmentID])
);

