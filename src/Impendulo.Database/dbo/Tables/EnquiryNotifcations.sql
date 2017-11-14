CREATE TABLE [dbo].[EnquiryNotifcations] (
    [EnquiryID]      INT NOT NULL,
    [NotificationID] INT NOT NULL,
    CONSTRAINT [PK_EnquiryNotifcations] PRIMARY KEY CLUSTERED ([EnquiryID] ASC, [NotificationID] ASC),
    CONSTRAINT [FK_EnquiryNotifcations_Enquiry] FOREIGN KEY ([EnquiryID]) REFERENCES [dbo].[Enquiry] ([EnquiryID]),
    CONSTRAINT [FK_EnquiryNotifcations_Notifications] FOREIGN KEY ([NotificationID]) REFERENCES [dbo].[Notifications] ([NotificationID])
);

