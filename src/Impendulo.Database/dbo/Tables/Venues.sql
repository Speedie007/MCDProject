CREATE TABLE [dbo].[Venues] (
    [VenueID]          INT          IDENTITY (1, 1) NOT NULL,
    [VenueName]        VARCHAR (50) CONSTRAINT [DF_Venues_VenueName] DEFAULT ('') NOT NULL,
    [VenueMaxCapacity] INT          CONSTRAINT [DF_Venues_VenueMaxCapacity] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Venues] PRIMARY KEY CLUSTERED ([VenueID] ASC)
);





