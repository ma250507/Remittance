USE [BMRem]
GO

/****** Object:  Table [dbo].[NewTransactions]    Script Date: 4/7/2020 1:16:24 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[NewTransactions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TransactionCode] [nchar](50) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[NationalID] [nchar](50) NOT NULL,
	[Amount] [int] NOT NULL,
	[ATMId] [varchar](20) NULL,
	[ATMDateTime] [datetime] NULL,
	[ATMTransactionSequence] [int] NULL,
	[WithdrawalStatus] [varchar](50) NULL,
	[WithdrawalDateTime] [datetime] NULL,
	[CommissionAmount] [int] NULL,
	[CassettesDispensedNotes] [int] NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[NewTransactions] ADD  CONSTRAINT [DF_NewTransactions_WithdrawalStatus]  DEFAULT ('') FOR [WithdrawalStatus]
GO


