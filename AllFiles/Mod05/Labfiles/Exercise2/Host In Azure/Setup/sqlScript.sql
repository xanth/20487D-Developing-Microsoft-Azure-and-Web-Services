CREATE TABLE [dbo].[Flights](
[Id] [int] IDENTITY(1,1) NOT NULL,
[Origin] [varchar](50) NOT NULL,
[Destination] [varchar](50) NOT NULL,
[FlightNumber] [varchar](50) NOT NULL,
[DepartureTime] [date] NOT NULL,

PRIMARY KEY CLUSTERED 
(
    [Id] ASC
))


INSERT [Flights] ([Origin], [Destination], [FlightNumber],[DepartureTime]) VALUES('New York', 'Paris','204837D','10/10/2018')
INSERT [Flights] ([Origin], [Destination], [FlightNumber],[DepartureTime]) VALUES('Paris', 'Rome','204837C','10/10/2017')

