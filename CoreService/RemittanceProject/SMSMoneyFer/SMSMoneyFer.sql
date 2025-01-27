if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Transactions_ATM]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Transactions] DROP CONSTRAINT FK_Transactions_ATM
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ATM_Bank]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ATM] DROP CONSTRAINT FK_ATM_Bank
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Bank_Country]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Bank] DROP CONSTRAINT FK_Bank_Country
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Country_Currency]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Country] DROP CONSTRAINT FK_Country_Currency
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Transactions_Currency]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Transactions] DROP CONSTRAINT FK_Transactions_Currency
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_TransactionNestedActions_RequestType]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[TransactionNestedActions] DROP CONSTRAINT FK_TransactionNestedActions_RequestType
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_TransactionNestedActions_Transactions]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[TransactionNestedActions] DROP CONSTRAINT FK_TransactionNestedActions_Transactions
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ATM]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ATM]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Bank]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Bank]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Country]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Country]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Currency]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Currency]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[RequestType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[RequestType]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[TransactionNestedActions]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[TransactionNestedActions]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Transactions]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Transactions]
GO

CREATE TABLE [dbo].[ATM] (
	[ATMId] [varchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[ATMLocation] [varchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[CountryCode] [varchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[BankCode] [varchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Cassitte1Value] [tinyint] NOT NULL ,
	[Cassitte2Value] [tinyint] NOT NULL ,
	[Cassitte3Value] [tinyint] NOT NULL ,
	[Cassitte4Value] [tinyint] NOT NULL ,
	[ATMIPAddress] [varchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[IsTeller] [bit] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Bank] (
	[BankCode] [varchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[BankName] [varchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[CountryCode] [varchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Country] (
	[CountryCode] [varchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[CountryName] [varchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[LocalCurrencyCode] [varchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[MaxAmountLimit] [numeric](18, 0) NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Currency] (
	[CurrencyCode] [varchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[CurrencyName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[CurrencySymbole] [varchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[RequestType] (
	[RequestTypeCode] [varchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[RequestTypeDescription] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[TransactionNestedActions] (
	[TransactionCode] [varchar] (12) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Action] [varchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[ActionDateTime] [datetime] NOT NULL ,
	[ActionReason] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[ActionStatus] [varchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[DispensedNotes] [varchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Transactions] (
	[TransactionCode] [varchar] (12) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[CountryCode] [varchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[BankCode] [varchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[ATMId] [varchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[RequestType] [varchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[ATMDate] [varchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[ATMTime] [varchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[ATMTrxSequence] [varchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[DepositorMobile] [varchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[DepositorPIN] [varchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[BeneficiaryMobile] [varchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[BeneficiaryPIN] [varchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Amount] [numeric](18, 0) NULL ,
	[CurrencyCode] [varchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[DepositDateTime] [datetime] NULL ,
	[DepositActionReason] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[DepositStatus] [varchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[WithdrawalStatus] [varchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[WithdrawalDateTime] [datetime] NULL ,
	[CancelStatus] [varchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[CancelDateTime] [datetime] NULL ,
	[OverallStatus] [varchar] (1024) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[SMSSendingStatus] [varchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[SMSSentDateTime] [datetime] NULL 
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ATM] ADD 
	CONSTRAINT [DF_ATM_IsTeller] DEFAULT (0) FOR [IsTeller],
	CONSTRAINT [PK_ATM] PRIMARY KEY  CLUSTERED 
	(
		[ATMId],
		[CountryCode],
		[BankCode]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Bank] ADD 
	CONSTRAINT [PK_Bank] PRIMARY KEY  CLUSTERED 
	(
		[BankCode],
		[CountryCode]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Country] ADD 
	CONSTRAINT [DF_Country_MaxAmountLimit] DEFAULT (10000) FOR [MaxAmountLimit],
	CONSTRAINT [PK_Country] PRIMARY KEY  CLUSTERED 
	(
		[CountryCode]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Currency] ADD 
	CONSTRAINT [PK_Currency] PRIMARY KEY  CLUSTERED 
	(
		[CurrencyCode]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[RequestType] ADD 
	CONSTRAINT [PK_RequestType] PRIMARY KEY  CLUSTERED 
	(
		[RequestTypeCode]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[TransactionNestedActions] ADD 
	CONSTRAINT [DF_TransactionNestedActions_ActionDateTime] DEFAULT (getdate()) FOR [ActionDateTime],
	CONSTRAINT [PK_TransactionNestedActions] PRIMARY KEY  CLUSTERED 
	(
		[TransactionCode],
		[Action],
		[ActionDateTime]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Transactions] ADD 
	CONSTRAINT [DF_Transactions_DepositDateTime] DEFAULT (getdate()) FOR [DepositDateTime],
	CONSTRAINT [PK_Transactions] PRIMARY KEY  CLUSTERED 
	(
		[TransactionCode]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ATM] ADD 
	CONSTRAINT [FK_ATM_Bank] FOREIGN KEY 
	(
		[BankCode],
		[CountryCode]
	) REFERENCES [dbo].[Bank] (
		[BankCode],
		[CountryCode]
	)
GO

ALTER TABLE [dbo].[Bank] ADD 
	CONSTRAINT [FK_Bank_Country] FOREIGN KEY 
	(
		[CountryCode]
	) REFERENCES [dbo].[Country] (
		[CountryCode]
	)
GO

ALTER TABLE [dbo].[Country] ADD 
	CONSTRAINT [FK_Country_Currency] FOREIGN KEY 
	(
		[LocalCurrencyCode]
	) REFERENCES [dbo].[Currency] (
		[CurrencyCode]
	)
GO

ALTER TABLE [dbo].[TransactionNestedActions] ADD 
	CONSTRAINT [FK_TransactionNestedActions_RequestType] FOREIGN KEY 
	(
		[Action]
	) REFERENCES [dbo].[RequestType] (
		[RequestTypeCode]
	),
	CONSTRAINT [FK_TransactionNestedActions_Transactions] FOREIGN KEY 
	(
		[TransactionCode]
	) REFERENCES [dbo].[Transactions] (
		[TransactionCode]
	)
GO

ALTER TABLE [dbo].[Transactions] ADD 
	CONSTRAINT [FK_Transactions_ATM] FOREIGN KEY 
	(
		[ATMId],
		[CountryCode],
		[BankCode]
	) REFERENCES [dbo].[ATM] (
		[ATMId],
		[CountryCode],
		[BankCode]
	),
	CONSTRAINT [FK_Transactions_Currency] FOREIGN KEY 
	(
		[CurrencyCode]
	) REFERENCES [dbo].[Currency] (
		[CurrencyCode]
	)
GO

