/****** Object:  Table [dbo].[Sector]    Script Date: 04/28/2014 11:35:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sector](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SectorName] [nvarchar](40) NOT NULL,
 CONSTRAINT [PK_Sector] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Industry]    Script Date: 04/28/2014 11:35:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Industry](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IndustryName] [nvarchar](40) NOT NULL,
	[SectorId] [int] NOT NULL,
 CONSTRAINT [PK_Industry] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Company]    Script Date: 04/28/2014 11:35:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyName] [nvarchar](40) NOT NULL,
	[CompanySymbol] [nvarchar](20) NOT NULL,
	[IndustryId] [int] NOT NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Quote]    Script Date: 2014.05.09 23:30:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Quote](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [int] NOT NULL,
	[LastUpdate] [smalldatetime] NOT NULL,
	[AskRealtime] [decimal](18, 4) NULL,
	[Ask] [decimal](18, 4) NULL,
	[BidRealtime] [decimal](18, 4) NULL,
	[Bid] [decimal](18, 4) NULL,
	[AverageDailyVolume] [decimal](18, 4) NULL,
	[BookValue] [decimal](18, 4) NULL,
	[Change] [decimal](18, 4) NULL,
	[ChangeRealtime] [decimal](18, 4) NULL,
	[DividendShare] [decimal](18, 4) NULL,
	[LastTradeDate] [date] NULL,
	[EarningsShare] [decimal](18, 4) NULL,
	[EPSEstimateCurrentYear] [decimal](18, 4) NULL,
	[EPSEstimateNextYear] [decimal](18, 4) NULL,
	[EPSEstimateNextQuarter] [decimal](18, 4) NULL,
	[DailyLow] [decimal](18, 4) NULL,
	[DailyHigh] [decimal](18, 4) NULL,
	[YearlyLow] [decimal](18, 4) NULL,
	[YearlyHigh] [decimal](18, 4) NULL,
	[MarketCapitalization] [decimal](18, 4) NULL,
	[EBITDA] [decimal](18, 4) NULL,
	[ChangeFromYearLow] [decimal](18, 4) NULL,
	[PercentChangeFromYearLow] [decimal](18, 4) NULL,
	[ChangeFromYearHigh] [decimal](18, 4) NULL,
	[LastTradePrice] [decimal](18, 4) NULL,
	[PercentChangeFromYearHigh] [decimal](18, 4) NULL,
	[FiftydayMovingAverage] [decimal](18, 4) NULL,
	[TwoHunderedDayMovingAverage] [decimal](18, 4) NULL,
	[ChangeFromTwoHundreddayMovingAverage] [decimal](18, 4) NULL,
	[PercentChangeFromTwoHundreddayMovingAverage] [decimal](18, 4) NULL,
	[PercentChangeFromFiftydayMovingAverage] [decimal](18, 4) NULL,
	[PreviousClose] [decimal](18, 4) NULL,
	[ChangeinPercent] [decimal](18, 4) NULL,
	[PriceSales] [decimal](18, 4) NULL,
	[PriceBook] [decimal](18, 4) NULL,
	[ExDividendDate] [date] NULL,
	[PERatio] [decimal](18, 4) NULL,
	[DividendPayDate] [date] NULL,
	[PEGRatio] [decimal](18, 4) NULL,
	[PriceEPSEstimateCurrentYear] [decimal](18, 4) NULL,
	[PriceEPSEstimateNextYear] [decimal](18, 4) NULL,
	[ShortRatio] [decimal](18, 4) NULL,
	[OneYearPriceTarget] [decimal](18, 4) NULL,
	[Volume] [decimal](18, 4) NULL,
	[StockExchange] [nvarchar](30) NULL,
 CONSTRAINT [PK_Quote] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Default [DF_DataTime]    Script Date: 04/28/2014 11:35:28 ******/
ALTER TABLE [dbo].[Quote] ADD  CONSTRAINT [DF_DataTime_AddData]  DEFAULT (getdate()) FOR [LastUpdate]
GO
/****** Object:  Constrain [CS_Sector]    Script Date: 04/28/2014 11:35:28 ******/
ALTER TABLE [dbo].[Sector] 
ADD CONSTRAINT uniq UNIQUE NONCLUSTERED ([SectorName])
GO
/****** Object:  ForeignKey [FK_Sector_Industry]    Script Date: 04/28/2014 11:35:28 ******/
ALTER TABLE [dbo].[Industry]  WITH CHECK ADD  CONSTRAINT [FK_Sector_Industry] FOREIGN KEY([SectorId])
REFERENCES [dbo].[Sector] ([Id])
GO
ALTER TABLE [dbo].[Industry] CHECK CONSTRAINT [FK_Sector_Industry]
GO
ALTER TABLE [dbo].[Industry] 
ADD CONSTRAINT uniq1 UNIQUE NONCLUSTERED ([IndustryName])
GO
/****** Object:  ForeignKey [FK_Industry_Company]    Script Date: 04/28/2014 11:35:28 ******/
ALTER TABLE [dbo].[Company]  WITH CHECK ADD CONSTRAINT [FK_Industry_Company] FOREIGN KEY([IndustryId])
REFERENCES [dbo].[Industry] ([Id])
GO
ALTER TABLE [dbo].[Company] CHECK CONSTRAINT [FK_Industry_Company]
GO
ALTER TABLE [dbo].[Company] 
ADD CONSTRAINT uniq2 UNIQUE NONCLUSTERED ([CompanySymbol])
GO
/****** Object:  ForeignKey [FK_Company_DataValue]    Script Date: 04/28/2014 11:35:28 ******/
ALTER TABLE [dbo].[Quote]  WITH CHECK ADD  CONSTRAINT [FK_Company_Quote] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Company] ([Id])
GO
ALTER TABLE [dbo].[Quote] CHECK CONSTRAINT [FK_Company_Quote]
GO