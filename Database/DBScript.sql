USE [SMSMoneyFer]
GO
/****** Object:  Table [dbo].[ATM]    Script Date: 2/5/2017 10:15:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ATM](
	[ATMId] [varchar](20) NOT NULL,
	[ATMLocation] [varchar](256) NOT NULL,
	[CountryCode] [varchar](10) NOT NULL,
	[BankCode] [varchar](10) NOT NULL,
	[Cassitte1Value] [tinyint] NOT NULL,
	[Cassitte2Value] [tinyint] NOT NULL,
	[Cassitte3Value] [tinyint] NOT NULL,
	[Cassitte4Value] [tinyint] NOT NULL,
	[ATMIPAddress] [varchar](20) NULL,
	[IsTeller] [bit] NOT NULL,
	[ATMName] [varchar](50) NULL,
	[TerminalId] [varchar](20) NULL,
 CONSTRAINT [PK_ATM] PRIMARY KEY CLUSTERED 
(
	[ATMId] ASC,
	[CountryCode] ASC,
	[BankCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Bank]    Script Date: 2/5/2017 10:15:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Bank](
	[BankCode] [varchar](10) NOT NULL,
	[BankName] [varchar](256) NOT NULL,
	[CountryCode] [varchar](10) NOT NULL,
	[MaxNotesCount] [int] NOT NULL,
	[MaximumAmount] [int] NOT NULL,
	[MinimumAmount] [int] NOT NULL,
	[ReceiptLine1] [varchar](40) NULL,
	[ReceiptLine2] [varchar](40) NULL,
	[ReceiptLine3] [varchar](40) NULL,
	[StartAmount1] [int] NOT NULL,
	[EndAmount1] [int] NOT NULL,
	[CommissionAmount1] [int] NOT NULL,
	[StartAmount2] [int] NOT NULL,
	[EndAmount2] [int] NOT NULL,
	[CommissionAmount2] [int] NOT NULL,
	[MaximumDailyAmount] [int] NOT NULL,
	[MaintenanceATM] [varchar](20) NULL,
	[RemittanceServicePort] [varchar](20) NULL,
	[RemittanceServiceIPAddress] [varchar](50) NULL,
	[MaximumKeyTrials] [int] NULL,
	[MaxreActivateTimes] [int] NULL,
	[DepositTransactionExpirationDays] [int] NOT NULL,
	[MaximumMonthlyAmount] [int] NOT NULL,
	[MaximumDailyCount] [int] NOT NULL,
 CONSTRAINT [PK_Bank] PRIMARY KEY CLUSTERED 
(
	[BankCode] ASC,
	[CountryCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BlockedCustomers]    Script Date: 2/5/2017 10:15:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BlockedCustomers](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[MobileNumber] [varchar](20) NOT NULL,
	[DepositorOrBeneficiary] [bit] NOT NULL,
	[BlockDateTime] [datetime] NOT NULL,
	[BlockReason] [text] NOT NULL,
	[UnBlocked] [bit] NOT NULL,
	[UnBlockDateTime] [datetime] NULL,
	[UserId] [varchar](20) NOT NULL,
 CONSTRAINT [PK_BlockedCustomers] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Branches]    Script Date: 2/5/2017 10:15:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Branches](
	[BankCode] [varchar](10) NOT NULL,
	[CountryCode] [varchar](10) NOT NULL,
	[BranchName] [varchar](50) NOT NULL,
	[Address] [varchar](50) NULL,
 CONSTRAINT [PK_Branches] PRIMARY KEY CLUSTERED 
(
	[BankCode] ASC,
	[CountryCode] ASC,
	[BranchName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Country]    Script Date: 2/5/2017 10:15:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Country](
	[CountryCode] [varchar](10) NOT NULL,
	[CountryName] [varchar](128) NOT NULL,
	[LocalCurrencyCode] [varchar](10) NOT NULL,
	[MaxAmountLimit] [numeric](18, 0) NOT NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[CountryCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Currency]    Script Date: 2/5/2017 10:15:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Currency](
	[CurrencyCode] [varchar](10) NOT NULL,
	[CurrencyName] [varchar](50) NOT NULL,
	[CurrencySymbole] [varchar](10) NOT NULL,
 CONSTRAINT [PK_Currency] PRIMARY KEY CLUSTERED 
(
	[CurrencyCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Groups]    Script Date: 2/5/2017 10:15:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Groups](
	[ID] [varchar](20) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Reports] [bit] NOT NULL,
	[Maintenance] [bit] NOT NULL,
	[Administration] [bit] NOT NULL,
	[Users] [bit] NOT NULL,
	[Teller] [bit] NOT NULL,
 CONSTRAINT [PK_Groups] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RegisteredCustomer]    Script Date: 2/5/2017 10:15:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RegisteredCustomer](
	[MobileNumber] [varchar](20) NOT NULL,
	[Name] [varchar](50) NULL,
	[RegisteringDate] [datetime] NOT NULL,
	[Address] [varchar](100) NULL,
 CONSTRAINT [PK_RegisteredCustomer] PRIMARY KEY CLUSTERED 
(
	[MobileNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RequestType]    Script Date: 2/5/2017 10:15:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RequestType](
	[RequestTypeCode] [varchar](10) NOT NULL,
	[RequestTypeDescription] [varchar](50) NULL,
 CONSTRAINT [PK_RequestType] PRIMARY KEY CLUSTERED 
(
	[RequestTypeCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SMSLanguages]    Script Date: 2/5/2017 10:15:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SMSLanguages](
	[SMSCode] [varchar](10) NOT NULL,
	[SMSDescription] [varchar](100) NOT NULL,
 CONSTRAINT [PK_SMSLanguages] PRIMARY KEY CLUSTERED 
(
	[SMSCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TransactionExceptionActions]    Script Date: 2/5/2017 10:15:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TransactionExceptionActions](
	[TransactionCode] [varchar](12) NULL,
	[Action] [varchar](10) NOT NULL,
	[ActionDateTime] [datetime] NOT NULL,
	[ActionStatus] [varchar](20) NULL,
	[CountryCode] [varchar](10) NULL,
	[BankCode] [varchar](10) NULL,
	[ATMId] [varchar](20) NULL,
	[ATMDate] [varchar](20) NULL,
	[ATMTime] [varchar](20) NULL,
	[DepositorMobile] [varchar](20) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TransactionKeyCheckTrials]    Script Date: 2/5/2017 10:15:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TransactionKeyCheckTrials](
	[TransactionCode] [varchar](20) NOT NULL,
	[KeyCheckDateTime] [datetime] NOT NULL,
	[TrialFlag] [bit] NOT NULL,
 CONSTRAINT [PK_TransactionKeyCheckTrials] PRIMARY KEY CLUSTERED 
(
	[TransactionCode] ASC,
	[KeyCheckDateTime] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TransactionNestedActions]    Script Date: 2/5/2017 10:15:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TransactionNestedActions](
	[TransactionCode] [varchar](12) NOT NULL,
	[Action] [varchar](10) NOT NULL,
	[ActionDateTime] [datetime] NOT NULL,
	[ActionReason] [varchar](50) NULL,
	[ActionStatus] [varchar](20) NULL,
	[DispensedNotes] [varchar](10) NULL,
	[DispensedAmount] [int] NULL,
	[CommissionAmount] [int] NULL,
	[Cassette1] [int] NULL,
	[Cassette2] [int] NULL,
	[Cassette3] [int] NULL,
	[Cassette4] [int] NULL,
	[CountryCode] [varchar](10) NULL,
	[BankCode] [varchar](10) NULL,
	[ATMId] [varchar](20) NULL,
	[ATMTrxSequence] [varchar](10) NULL,
	[DispensedCurrencyCode] [varchar](10) NULL,
	[DispensedRate] [numeric](13, 5) NULL,
 CONSTRAINT [PK_TransactionNestedActions] PRIMARY KEY CLUSTERED 
(
	[TransactionCode] ASC,
	[Action] ASC,
	[ActionDateTime] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Transactions]    Script Date: 2/5/2017 10:15:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Transactions](
	[TransactionCode] [varchar](12) NOT NULL,
	[CountryCode] [varchar](10) NOT NULL,
	[BankCode] [varchar](10) NOT NULL,
	[ATMId] [varchar](20) NOT NULL,
	[RequestType] [varchar](10) NOT NULL,
	[ATMDate] [varchar](20) NOT NULL,
	[ATMTime] [varchar](20) NOT NULL,
	[ATMTrxSequence] [varchar](10) NOT NULL,
	[DepositorMobile] [varchar](20) NULL,
	[DepositorPIN] [varchar](256) NULL,
	[BeneficiaryMobile] [varchar](20) NULL,
	[BeneficiaryPIN] [varchar](256) NULL,
	[Amount] [numeric](18, 0) NULL,
	[CurrencyCode] [varchar](10) NULL,
	[DepositDateTime] [datetime] NULL,
	[DepositActionReason] [varchar](50) NULL,
	[DepositStatus] [varchar](20) NULL,
	[WithdrawalStatus] [varchar](20) NULL,
	[WithdrawalDateTime] [datetime] NULL,
	[CancelStatus] [varchar](20) NULL,
	[CancelDateTime] [datetime] NULL,
	[OverallStatus] [varchar](1024) NULL,
	[SMSSendingStatus] [varchar](20) NULL,
	[SMSSentDateTime] [datetime] NULL,
	[WSendingStatus] [varchar](20) NULL,
	[WSentDateTime] [datetime] NULL,
	[ResendSMSFlag] [int] NOT NULL,
	[ResendTo] [int] NOT NULL,
	[ResendSMSDateTime] [datetime] NULL,
	[SMSLanguage] [varchar](10) NULL,
	[RedemptionPIN] [varchar](256) NULL,
	[DepositHostFlag] [int] NULL,
	[WithdrawalHostFlag] [int] NULL,
	[DepositorID] [varchar](512) NULL,
	[BeneficiaryID] [varchar](512) NULL,
	[HostDResponse] [varchar](10) NULL,
	[HostWResponse] [varchar](10) NULL,
	[HostDUpdateTime] [datetime] NULL,
	[HostWUpdateTime] [datetime] NULL,
	[ReActivationCounter] [int] NOT NULL,
	[PaymentType] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Transactions] PRIMARY KEY CLUSTERED 
(
	[TransactionCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserActions]    Script Date: 2/5/2017 10:15:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserActions](
	[UserId] [varchar](20) NOT NULL,
	[Action] [varchar](50) NULL,
	[ActionDateTime] [datetime] NULL,
	[TransactionCode] [varchar](50) NULL,
	[UserName] [varchar](100) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserPermissions]    Script Date: 2/5/2017 10:15:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserPermissions](
	[Userid] [varchar](20) NOT NULL,
	[Activate_Perm] [bit] NULL,
	[Hold_Perm] [bit] NULL,
	[Unhold_Perm] [bit] NULL,
	[Resend_Perm] [bit] NULL,
	[Unblock_Perm] [bit] NULL,
 CONSTRAINT [PK_UserPermissions] PRIMARY KEY CLUSTERED 
(
	[Userid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Users]    Script Date: 2/5/2017 10:15:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [varchar](20) NOT NULL,
	[UserName] [varchar](250) NOT NULL,
	[Password] [varchar](300) NOT NULL,
	[Group_ID] [varchar](20) NOT NULL,
	[ATM_ID] [varchar](20) NULL,
	[CountryCode] [varchar](10) NULL,
	[BankCode] [varchar](10) NULL,
	[Branch] [varchar](200) NULL,
	[FirstTime] [bit] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  View [dbo].[AllUsers]    Script Date: 2/5/2017 10:15:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[AllUsers]
AS
SELECT     u.UserId, u.UserName,
                          (SELECT     name
                            FROM          groups
                            WHERE      id = u.group_ID) AS [Group], u.ATM_ID, u.CountryCode, u.BankCode, a.IsTeller, u.Branch
FROM         dbo.Users u LEFT OUTER JOIN
                      dbo.ATM a ON u.ATM_ID = a.ATMId AND u.BankCode = a.BankCode AND u.CountryCode = a.CountryCode

GO
/****** Object:  View [dbo].[BankATMView]    Script Date: 2/5/2017 10:15:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[BankATMView]
AS
SELECT     a.ATMId, a.BankCode, a.CountryCode, a.Cassitte1Value, a.Cassitte2Value, a.Cassitte3Value, a.Cassitte4Value, b.MaxNotesCount, b.MaximumAmount, 
                      b.MinimumAmount, b.StartAmount1, b.EndAmount1, b.CommissionAmount1, b.StartAmount2, b.EndAmount2, b.CommissionAmount2, b.ReceiptLine1, 
                      b.ReceiptLine2, b.ReceiptLine3, b.MaximumDailyAmount, b.MaximumKeyTrials, b.MaxreActivateTimes, b.DepositTransactionExpirationDays,b.MaximumMonthlyAmount,b.MaximumDailyCount 
FROM         dbo.ATM a INNER JOIN
                      dbo.Bank b ON a.BankCode = b.BankCode AND a.CountryCode = b.CountryCode




GO
/****** Object:  View [dbo].[HostUpdateView]    Script Date: 2/5/2017 10:15:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[HostUpdateView] as

select   t.TransactionCode,t.DepositorMobile ,t.BeneficiaryMobile ,t.Amount,t.CurrencyCode,a.isTeller , t.DepositStatus,t.withdrawalStatus,t.DepositHostFlag, t.withdrawalHostFlag,
         tn.CountryCode,      tn.BankCode ,      tn.ATMId, 
         TN.ACTION,convert(varchar(19),tn.actiondatetime,121) ActionDateTime,convert(varchar(8),tn.actiondatetime,112) ActionDate,         convert(varchar(8),tn.actiondatetime,108) ActionTime,  tn.ATMTrxSequence,   
         tn.DispensedAmount,tn.CommissionAmount,tn.dispensedcurrencycode,tn.dispensedrate

from transactions t, atm a,transactionnestedactions tn
where 
     tn.transactioncode=t.transactioncode
and (
     (tn.action='11'  and tn.actionstatus='CONFIRMED' and  (t.DepositStatus    ='CONFIRMED' or t.DepositStatus ='EXPIRED')    and  ( t.DepositHostFlag=0  or  t.DepositHostFlag is null))
      or
     (tn.action='12'  and tn.actionstatus='CONFIRMED' and   t.withdrawalstatus ='CONFIRMED' and  ( t.withdrawalHostFlag=0  or  t.withdrawalHostFlag is null))
     or
     (tn.action='17'  and tn.actionstatus='CONFIRMED' and   t.withdrawalstatus ='CONFIRMED' and  ( t.withdrawalHostFlag=0  or  t.withdrawalHostFlag is null))

    )

and a.countrycode=tn.countrycode and a.bankcode=tn.bankcode and a.atmid=tn.atmid


GO
/****** Object:  View [dbo].[NBEHostUpdateView]    Script Date: 2/5/2017 10:15:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[NBEHostUpdateView]
as

SELECT     t.TransactionCode, t.DepositorMobile, t.BeneficiaryMobile, tn.ActionStatus, t.Amount, t.CurrencyCode, a.IsTeller, t.DepositStatus, t.WithdrawalStatus, 

                      t.DepositHostFlag, t.WithdrawalHostFlag, tn.CountryCode, tn.BankCode, tn.ATMId, tn.Action, tn.ActionDateTime, CONVERT(varchar(8), 

                      tn.ActionDateTime, 112) AS ActionDate, CONVERT(varchar(8), tn.ActionDateTime, 108) AS ActionTime, t.WithdrawalDateTime, tn.ATMTrxSequence, 

                      tn.DispensedAmount, tn.CommissionAmount, tn.DispensedCurrencyCode, tn.DispensedRate, tn.DispensedNotes, a.TerminalID

FROM         dbo.Transactions AS t INNER JOIN

                      dbo.TransactionNestedActions AS tn ON t.TransactionCode = tn.TransactionCode INNER JOIN

                      dbo.ATM AS a ON tn.CountryCode = a.CountryCode AND tn.BankCode = a.BankCode AND tn.ATMId = a.ATMId

WHERE     (tn.Action = '11') AND (tn.ActionStatus = 'CONFIRMED') AND (t.DepositStatus = 'CONFIRMED' OR  t.DepositStatus = 'EXPIRED') AND (t.DepositHostFlag = 0 OR  t.DepositHostFlag IS NULL)

      OR

          (tn.Action = '12') AND (tn.ActionStatus = 'CONFIRMED') AND (t.WithdrawalStatus = 'CONFIRMED') AND (t.WithdrawalHostFlag %2 = 0 OR t.WithdrawalHostFlag IS NULL)                 

      OR

          (tn.Action = '17') AND (tn.ActionStatus = 'CONFIRMED') AND (t.WithdrawalStatus = 'CONFIRMED') AND (t.WithdrawalHostFlag %2 = 0 OR t.WithdrawalHostFlag IS NULL) 

      OR

          (tn.Action = '02') AND (tn.ActionStatus = 'AUTHORIZED') AND (t.WithdrawalStatus = 'AUTHORIZED') AND (t.WithdrawalHostFlag %2 = 0 OR  t.WithdrawalHostFlag IS NULL)  and datediff(minute,t.Withdrawaldatetime, getdate()) >=5

 

 

and a.countrycode=tn.countrycode and a.bankcode=tn.bankcode and a.atmid=tn.atmid

 



GO
/****** Object:  View [dbo].[UserATM]    Script Date: 2/5/2017 10:15:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[UserATM]
AS
SELECT     u.UserId, u.UserName, u.Password,
                          (SELECT     name
                            FROM          groups
                            WHERE      id = u.group_ID) AS [Group], u.ATM_ID, u.CountryCode, u.BankCode, a.IsTeller
FROM         dbo.Users u INNER JOIN
                      dbo.ATM a ON u.ATM_ID = a.ATMId AND u.BankCode = a.BankCode AND u.CountryCode = a.CountryCode

GO
ALTER TABLE [dbo].[ATM] ADD  CONSTRAINT [DF_ATM_IsTeller]  DEFAULT (0) FOR [IsTeller]
GO
ALTER TABLE [dbo].[Bank] ADD  CONSTRAINT [DF_Bank_MaxNotesCount]  DEFAULT (40) FOR [MaxNotesCount]
GO
ALTER TABLE [dbo].[Bank] ADD  CONSTRAINT [DF_Bank_MaximumAmount]  DEFAULT (999999) FOR [MaximumAmount]
GO
ALTER TABLE [dbo].[Bank] ADD  CONSTRAINT [DF_Bank_MinimumAmount]  DEFAULT (0) FOR [MinimumAmount]
GO
ALTER TABLE [dbo].[Bank] ADD  CONSTRAINT [DF_Bank_StartAmount1]  DEFAULT (1) FOR [StartAmount1]
GO
ALTER TABLE [dbo].[Bank] ADD  CONSTRAINT [DF_Bank_EndAmount1]  DEFAULT (500) FOR [EndAmount1]
GO
ALTER TABLE [dbo].[Bank] ADD  CONSTRAINT [DF_Bank_CommissionAmount1]  DEFAULT (10) FOR [CommissionAmount1]
GO
ALTER TABLE [dbo].[Bank] ADD  CONSTRAINT [DF_Bank_StartAmount11]  DEFAULT (501) FOR [StartAmount2]
GO
ALTER TABLE [dbo].[Bank] ADD  CONSTRAINT [DF_Bank_EndAmount11]  DEFAULT (3000) FOR [EndAmount2]
GO
ALTER TABLE [dbo].[Bank] ADD  CONSTRAINT [DF_Bank_CommissionAmount11]  DEFAULT (10) FOR [CommissionAmount2]
GO
ALTER TABLE [dbo].[Bank] ADD  CONSTRAINT [DF_Bank_MaximumDailyAmount]  DEFAULT (5000) FOR [MaximumDailyAmount]
GO
ALTER TABLE [dbo].[Bank] ADD  CONSTRAINT [DF_Bank_MaintenanceATM]  DEFAULT ('TEL01') FOR [MaintenanceATM]
GO
ALTER TABLE [dbo].[Bank] ADD  CONSTRAINT [DF_Bank_RemittanceServicePort]  DEFAULT (1009) FOR [RemittanceServicePort]
GO
ALTER TABLE [dbo].[Bank] ADD  CONSTRAINT [DF_Bank_MaximumKeyTrials]  DEFAULT (3) FOR [MaximumKeyTrials]
GO
ALTER TABLE [dbo].[Bank] ADD  CONSTRAINT [DF_Bank_MaxreActivateTimes]  DEFAULT (3) FOR [MaxreActivateTimes]
GO
ALTER TABLE [dbo].[Bank] ADD  CONSTRAINT [DF_Bank_DepositTransactionExpirationDays]  DEFAULT (3) FOR [DepositTransactionExpirationDays]
GO
ALTER TABLE [dbo].[Bank] ADD  CONSTRAINT [DF_Bank_MaximumMonthlyAmount]  DEFAULT (1000000) FOR [MaximumMonthlyAmount]
GO
ALTER TABLE [dbo].[Bank] ADD  CONSTRAINT [DF_Bank_MaximumMonthlyAmount1]  DEFAULT (5) FOR [MaximumDailyCount]
GO
ALTER TABLE [dbo].[BlockedCustomers] ADD  CONSTRAINT [DF_BlockedCustomers_BlockDateTime]  DEFAULT (getdate()) FOR [BlockDateTime]
GO
ALTER TABLE [dbo].[BlockedCustomers] ADD  CONSTRAINT [DF_BlockedCustomers_UnBlocked]  DEFAULT (0) FOR [UnBlocked]
GO
ALTER TABLE [dbo].[Country] ADD  CONSTRAINT [DF_Country_MaxAmountLimit]  DEFAULT (10000) FOR [MaxAmountLimit]
GO
ALTER TABLE [dbo].[RegisteredCustomer] ADD  CONSTRAINT [DF_RegisteredCustomer_RegisteringDate]  DEFAULT (getdate()) FOR [RegisteringDate]
GO
ALTER TABLE [dbo].[TransactionKeyCheckTrials] ADD  CONSTRAINT [DF_TransactionKeyCheckTrials_KeyCheckDateTime]  DEFAULT (getdate()) FOR [KeyCheckDateTime]
GO
ALTER TABLE [dbo].[TransactionKeyCheckTrials] ADD  CONSTRAINT [DF_TransactionKeyCheckTrials_TrialFlag]  DEFAULT (0) FOR [TrialFlag]
GO
ALTER TABLE [dbo].[TransactionNestedActions] ADD  CONSTRAINT [DF_TransactionNestedActions_ActionDateTime]  DEFAULT (getdate()) FOR [ActionDateTime]
GO
ALTER TABLE [dbo].[Transactions] ADD  CONSTRAINT [DF_Transactions_DepositDateTime]  DEFAULT (getdate()) FOR [DepositDateTime]
GO
ALTER TABLE [dbo].[Transactions] ADD  CONSTRAINT [DF_Transactions_ResendSMSFlag]  DEFAULT (0) FOR [ResendSMSFlag]
GO
ALTER TABLE [dbo].[Transactions] ADD  CONSTRAINT [DF_Transactions_ResendTo]  DEFAULT (3) FOR [ResendTo]
GO
ALTER TABLE [dbo].[Transactions] ADD  CONSTRAINT [DF_Transactions_SMSLanguage]  DEFAULT ('E') FOR [SMSLanguage]
GO
ALTER TABLE [dbo].[Transactions] ADD  CONSTRAINT [DF_Transactions_DepositHostFalg]  DEFAULT (0) FOR [DepositHostFlag]
GO
ALTER TABLE [dbo].[Transactions] ADD  CONSTRAINT [DF_Transactions_WithdrawalHostFlag]  DEFAULT (0) FOR [WithdrawalHostFlag]
GO
ALTER TABLE [dbo].[Transactions] ADD  CONSTRAINT [DF_Transactions_ReActivationCounter]  DEFAULT (0) FOR [ReActivationCounter]
GO
ALTER TABLE [dbo].[Transactions] ADD  CONSTRAINT [DF_Transactions_PaymentType]  DEFAULT ('CASH') FOR [PaymentType]
GO
ALTER TABLE [dbo].[UserActions] ADD  CONSTRAINT [DF_UserActions_ActionDateTime]  DEFAULT (getdate()) FOR [ActionDateTime]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_FirstTime]  DEFAULT (0) FOR [FirstTime]
GO
ALTER TABLE [dbo].[ATM]  WITH NOCHECK ADD  CONSTRAINT [FK_ATM_Bank] FOREIGN KEY([BankCode], [CountryCode])
REFERENCES [dbo].[Bank] ([BankCode], [CountryCode])
GO
ALTER TABLE [dbo].[ATM] CHECK CONSTRAINT [FK_ATM_Bank]
GO
ALTER TABLE [dbo].[Bank]  WITH NOCHECK ADD  CONSTRAINT [FK_Bank_Country] FOREIGN KEY([CountryCode])
REFERENCES [dbo].[Country] ([CountryCode])
GO
ALTER TABLE [dbo].[Bank] CHECK CONSTRAINT [FK_Bank_Country]
GO
ALTER TABLE [dbo].[Country]  WITH CHECK ADD  CONSTRAINT [FK_Country_Currency] FOREIGN KEY([LocalCurrencyCode])
REFERENCES [dbo].[Currency] ([CurrencyCode])
GO
ALTER TABLE [dbo].[Country] CHECK CONSTRAINT [FK_Country_Currency]
GO
ALTER TABLE [dbo].[TransactionNestedActions]  WITH NOCHECK ADD  CONSTRAINT [FK_TransactionNestedActions_RequestType] FOREIGN KEY([Action])
REFERENCES [dbo].[RequestType] ([RequestTypeCode])
GO
ALTER TABLE [dbo].[TransactionNestedActions] CHECK CONSTRAINT [FK_TransactionNestedActions_RequestType]
GO
ALTER TABLE [dbo].[TransactionNestedActions]  WITH NOCHECK ADD  CONSTRAINT [FK_TransactionNestedActions_Transactions] FOREIGN KEY([TransactionCode])
REFERENCES [dbo].[Transactions] ([TransactionCode])
GO
ALTER TABLE [dbo].[TransactionNestedActions] CHECK CONSTRAINT [FK_TransactionNestedActions_Transactions]
GO
ALTER TABLE [dbo].[Transactions]  WITH NOCHECK ADD  CONSTRAINT [FK_Transactions_ATM] FOREIGN KEY([ATMId], [CountryCode], [BankCode])
REFERENCES [dbo].[ATM] ([ATMId], [CountryCode], [BankCode])
GO
ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [FK_Transactions_ATM]
GO
ALTER TABLE [dbo].[Transactions]  WITH NOCHECK ADD  CONSTRAINT [FK_Transactions_Currency] FOREIGN KEY([CurrencyCode])
REFERENCES [dbo].[Currency] ([CurrencyCode])
GO
ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [FK_Transactions_Currency]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Groups] FOREIGN KEY([Group_ID])
REFERENCES [dbo].[Groups] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Groups]
GO
