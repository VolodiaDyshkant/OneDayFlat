CREATE TABLE [dbo].[Day] (
    [DayID]      INT IDENTITY (1, 1) NOT NULL,
    [Booked]     BIT NOT NULL,
    [CalendarID] INT NULL, 
    [UserID]     INT NULL,
    CONSTRAINT [PK_Day] PRIMARY KEY CLUSTERED ([DayID] ASC),
    CONSTRAINT [FK_Day_Calendar_CalendarID] FOREIGN KEY ([CalendarID]) REFERENCES [dbo].[Calendar] ([CalendarID]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Day_CalendarID]
    ON [dbo].[Day]([CalendarID] ASC);

