CREATE TABLE [dbo].[Members] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [Address]        NVARCHAR (MAX) NULL,
    [Email]          NVARCHAR (MAX) NULL,
    [FullName]      NVARCHAR (MAX) NULL,
    [MembershipType] INT            NOT NULL,
    [MobileNumber]   NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Members] PRIMARY KEY CLUSTERED ([Id] ASC)
);

