USE [SMSMoneyFer]
GO
/****** Object:  StoredProcedure [dbo].[sp_add_customer]    Script Date: 8/5/2018 7:43:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_add_customer]
	-- Add the parameters for the stored procedure here
	 (
    @action_todo CHAR(1) = 'A',
	@MobileNo nvarchar(20),
    @ID nvarchar(20),
    @Name nvarchar(50),
    @Address nvarchar(100),
    @Staff bit,
    @BankCustomer bit,
    @result INT OUT -- 0 -success, 1- Failed, 2- Record is already existing
  )
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

 DECLARE @error_message NVARCHAR(MAX)
     SELECT  @error_message = '', @result = 0   
     BEGIN TRY  
  
 IF @action_todo = 'A'   --Add     
        BEGIN
        IF NOT EXISTS 
				  ( SELECT  TOP 1   
							MobileNumber
					FROM    RegisteredCustomer 
					WHERE   ID = @ID
				  )
		    BEGIN
				INSERT INTO RegisteredCustomer(MobileNumber,ID, Address,Name, bankcustomer,Staff,RegisteringDate)
                VALUES(@MobileNo,@ID,@Address,@Name,@BankCustomer,@Staff,getdate())
             END
		 ELSE		   
			 BEGIN
				--record already exists
			 SET @result = 2
			 END
         END
   
    IF @action_todo = 'E'   --Edit
        BEGIN	
        UPDATE RegisteredCustomer
        SET MobileNumber=@MobileNo
        ,Address=@Address
        ,bankcustomer=@BankCustomer
        ,Staff=@Staff
        ,Name=@Name
        WHERE ID = @ID
        END
 END TRY
 BEGIN CATCH
  SET @result = 1
 END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[sp_addedit_ATM]    Script Date: 8/5/2018 7:43:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_addedit_ATM]  
  -- Add the parameters for the stored procedure here
   (
    @action_todo CHAR(1) = 'A',
  @ATMID nvarchar(20),
  @ATMTerminalID nvarchar(20),
  @ATMName nvarchar(50),
  @CountryCode nvarchar(20),
  @BankCode nvarchar(20),
  @Branch nvarchar(20),
  @IP nvarchar(20),
  @ATMLocation nvarchar(256)='',
  @IsTeller bit,
  @Cassette1 int,
  @Cassette2 int,
  @Cassette3 int,
  @Cassette4 int,
    @result INT OUT -- 0 -success, 1- Failed, 2- Record is already existing
  )
AS
BEGIN
  -- SET NOCOUNT ON added to prevent extra result sets from
  -- interfering with SELECT statements.
  SET NOCOUNT ON;

    DECLARE @error_message NVARCHAR(MAX)
     SELECT  @error_message = '', @result = 0   
     BEGIN TRY  
  
 IF @action_todo = 'A'   --Add
        BEGIN
        IF NOT EXISTS 
          ( SELECT  TOP 1   
              ATMID
          FROM    ATM
          WHERE   ATMID = @ATMID
          )
        BEGIN
        INSERT INTO ATM
				(TERMINALID,CountryCode,BankCode,ATMID,Branch,Cassitte1Value,Cassitte2Value,Cassitte3Value,Cassitte4Value,ATMIPADDRESS,ATMLOCATION,ISTELLER,ATMNAME)
        VALUES(@ATMTerminalID,@CountryCode,@BankCode,@ATMID,@BRANCH,@Cassette1,@Cassette2,@Cassette3,@Cassette4,@IP,@ATMLocation,@IsTeller,@ATMName)
             END
     ELSE      
       BEGIN
        --record already exists
       SET @result = 2
       END
         END
   
    IF @action_todo = 'E'   --Edit
        BEGIN 
        
        UPDATE ATM
        SET ATMName = @ATMName 
           ,ATMIPADDRESS = @IP
           ,ATMLocation=@ATMLocation
           ,[ISTELLER]=@IsTeller
           ,Cassitte1Value=@Cassette1
           ,Cassitte2Value=@Cassette2
           ,Cassitte3Value=@Cassette3
           ,Cassitte4Value=@Cassette4
        WHERE ATMID = @ATMID AND TERMINALID=@ATMTerminalID
        END
 END TRY
 BEGIN CATCH
  SET @result = 1
 END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[sp_block_customer]    Script Date: 8/5/2018 7:43:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_block_customer]
	-- Add the parameters for the stored procedure here
	(
    @ID nvarchar(20),
    @BState bit,
	@UserType bit,
	@BlockReason text,
    @result INT OUT -- 0 -success, 1- Failed, 2- Record is already existing
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	 DECLARE @error_message NVARCHAR(MAX)
     SELECT  @error_message = '', @result = 0   
    
	 BEGIN TRY  

    -- Insert statements for procedure here
	If @BState = 'True'
		Declare @MobNo nvarchar(20)
		Begin
		IF NOT EXISTS
			(SELECT  TOP 1 ID
				FROM    BlockedCustomers
				WHERE   UserId = @ID
				)
		    BEGIN
				SELECT @MobNo = (SELECT MobileNumber FROM RegisteredCustomer  WHERE ID = @ID)
				INSERT INTO BlockedCustomers(MobileNumber,DepositorOrBeneficiary,BlockDateTime,BlockReason,UnBlocked,UserId)
					VALUES(@MobNo,@UserType,getdate(),@BlockReason,1,@ID)
					SET @result = 0
             END
		ELSE
			BEGIN
			SELECT @MobNo = (SELECT MobileNumber FROM RegisteredCustomer  WHERE ID = @ID)
			UPDATE BlockedCustomers SET MobileNumber = @MobNo, DepositorOrBeneficiary = @UserType,
					BlockDateTime = getdate(),
					BlockReason = @BlockReason,
					UnBlocked = 1
					Where UserId=@ID
					SET @result = 0
			END
		END

	If @BState = 'False'
		Begin
		IF NOT EXISTS
			(SELECT  TOP 1 ID
				FROM    BlockedCustomers
				WHERE   UserId = @ID
				)
		    BEGIN
				SELECT @MobNo = (SELECT MobileNumber FROM RegisteredCustomer  WHERE ID = @ID)
				INSERT INTO BlockedCustomers(MobileNumber,DepositorOrBeneficiary,BlockDateTime,BlockReason,UnBlocked,UserId)
					VALUES(@MobNo,@UserType,getdate(),@BlockReason,0,@ID)
					SET @result = 0
             END
		ELSE
		Begin
			SELECT @MobNo = (SELECT MobileNumber FROM RegisteredCustomer  WHERE ID = @ID)
			UPDATE BlockedCustomers SET MobileNumber = @MobNo, DepositorOrBeneficiary = @UserType,
					UnBlocked = 0,
					UnBlockDateTime = getdate()
					Where UserId=@ID
					SET @result = 0
		END
		END
		END TRY
		BEGIN CATCH
		SET @result = 1
		END CATCH

		END
			



GO
/****** Object:  StoredProcedure [dbo].[sp_del_customer]    Script Date: 8/5/2018 7:43:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_del_customer] 
	(
	@ID nvarchar(100),
	@result INT OUT -- 0 -success, 1- Failed, 2- Record in use
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

     DECLARE @error_message NVARCHAR(MAX)
     SELECT  @error_message = '', @result = 0 
     BEGIN TRY
		DELETE FROM RegisteredCustomer WHERE ID=@ID
     END TRY
 
 BEGIN CATCH
  SET @result = 1
 END CATCH
END



GO
/****** Object:  StoredProcedure [dbo].[sp_get_ATMs]    Script Date: 8/5/2018 7:43:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_get_ATMs]
  -- Add the parameters for the stored procedure here
  (
  @ATMID nvarchar(20),
  @ATMTerminalID nvarchar(20),
  @ATMName nvarchar(50),
  @BranchID nvarchar(20)
  )
AS
BEGIN
  -- SET NOCOUNT ON added to prevent extra result sets from
  -- interfering with SELECT statements.
  SET NOCOUNT ON;
DECLARE @error_message NVARCHAR(MAX)
  BEGIN TRY    
   SELECT  @error_message = ''
       IF ( @ATMID IS NULL ) 
        BEGIN 
         set @ATMID='%%'
        END
      IF ( @ATMTerminalID IS NULL ) 
        BEGIN 
         set @ATMTerminalID='%%'
        END
        IF ( @ATMName IS NULL ) 
        BEGIN 
         set @ATMName='%%'
        END
        IF ( @BranchID IS NULL ) 
        BEGIN 
         set @BranchID='%%'
        END
--SELECT a.*,b.BranchName from ATMs a, Branches b WHERE  a.BranchID=b.BranchCode AND ATMTerminalID LIKE @ATMTerminalID AND ATMName LIKE @ATMName AND a.BranchID LIKE @BranchID
 
 SELECT * FROM ATM WHERE ATMID LIKE @ATMID AND TERMINALID LIKE @ATMTerminalID AND ATMName LIKE @ATMName
 
  END TRY
    BEGIN CATCH
      SELECT  @error_message = ISNULL(OBJECT_NAME(@@PROCID),
                                      'Procedure Name Not Obtained') + ':'
              + ISNULL(ERROR_MESSAGE(), 'Error message not captured')
    RAISERROR(@error_message, 18, 1)  
    END CATCH
END





GO
/****** Object:  StoredProcedure [dbo].[sp_get_User]    Script Date: 8/5/2018 7:43:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_get_User]
	-- Add the parameters for the stored procedure here
	(
	@MobileNo nvarchar(50),
	@NationalID nvarchar(50),
	@FullName nvarchar(100)
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   DECLARE @error_message NVARCHAR(MAX)
  BEGIN TRY    
  SELECT  @error_message = ''
      
       IF ( @MobileNo IS NULL ) 
        BEGIN 
         set @MobileNo='%%'
        END
        IF ( @NationalID IS NULL ) 
        BEGIN 
         set @NationalID='%%'
        END
        IF ( @FullName IS NULL ) 
        BEGIN 
         set @FullName='%%'
        END
		select r.*, b.UnBlocked, b.BlockDateTime,b.BlockReason,b.DepositorOrBeneficiary from RegisteredCustomer R left join BlockedCustomers b on r.MobileNumber = b.MobileNumber
		where r.MobileNumber LIKE @MobileNo
AND r.ID like @NationalID AND r.Name like @FullName
  END TRY
    BEGIN CATCH
      SELECT  @error_message = ISNULL(OBJECT_NAME(@@PROCID),
                                      'Procedure Name Not Obtained') + ':'
              + ISNULL(ERROR_MESSAGE(), 'Error message not captured')
    RAISERROR(@error_message, 18, 1)	
    END CATCH
END


GO
/****** Object:  StoredProcedure [dbo].[sp_Report_ATMTotals]    Script Date: 8/5/2018 7:43:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_Report_ATMTotals]
  -- Add the parameters for the stored procedure here
  (
@ATMTerminalID nvarchar(20),
@FromDate nvarchar(20),
@ToDate nvarchar(20),
@MobileNumber  nvarchar(20)
)
AS
BEGIN
  -- SET NOCOUNT ON added to prevent extra result sets from
  -- interfering with SELECT statements.
  SET NOCOUNT ON;
DECLARE @error_message NVARCHAR(MAX)
BEGIN TRY
   SELECT  @error_message = ''
     
        IF (@ATMTerminalID IS NULL ) 
        BEGIN 
         set @ATMTerminalID='%%'
        END
        IF (@MobileNumber IS NULL ) 
        BEGIN 
         set @MobileNumber='%%'
        END
        ELSE
        BEGIN 
         set @MobileNumber='%' + @MobileNumber
        END
        IF (@FromDate IS NOT NULL)
        Begin
        IF isdate(@FromDate) =0 
        BEGIN
      raiserror('Wrong From date',16,-1)
      return
        END
        set @FromDate=cast(@FromDate as datetime)
        END
        
        IF (@ToDate IS NOT NULL)
        Begin
        IF isdate(@ToDate) =0 
        BEGIN
      raiserror('Wrong To date',16,-1)
      return
        END
        set @ToDate=cast(@ToDate as datetime)
        END
        
      IF (@FromDate IS NOT NULL) AND (@ToDate IS NOT NULL) 
        BEGIN
		  SELECT A.TERMINALID,A.ATMID,A.ATMNAME,D.WDAmount
		  FROM ATM A
		  LEFT OUTER JOIN 
		  (SELECT SUM (b.Amount) WDAmount, b.ATMTerminalID TID FROM IRSTransactions b WHERE b.WithdrawalStatus='CONFIRMED' AND TrxDatetime BETWEEN @FromDate AND @ToDate Group by b.ATMTerminalID
		  )D ON A.TERMINALID= D.TID
		  WHERE A.TERMINALID LIKE @ATMTerminalID
		END
	ELSE
	BEGIN
		  SELECT A.TERMINALID,A.ATMID,A.ATMNAME,D.WDAmount
		  FROM ATM A
		  LEFT OUTER JOIN 
		  (SELECT SUM (b.Amount) WDAmount, b.ATMTerminalID TID FROM IRSTransactions b WHERE b.WithdrawalStatus='CONFIRMED' Group by b.ATMTerminalID
		  )D ON A.TERMINALID= D.TID
		  WHERE A.TERMINALID LIKE @ATMTerminalID
	END
  END TRY
    BEGIN CATCH
      SELECT  @error_message = ISNULL(OBJECT_NAME(@@PROCID),
                                      'Procedure Name Not Obtained') + ':'
              + ISNULL(ERROR_MESSAGE(), 'Error message not captured')
    RAISERROR(@error_message, 18, 1)  
    END CATCH
END


GO
/****** Object:  StoredProcedure [dbo].[sp_Report_IRSTrxStatus]    Script Date: 8/5/2018 7:43:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_Report_IRSTrxStatus]
  -- Add the parameters for the stored procedure here
  (
@FromDate nvarchar(20),
@ToDate nvarchar(20),
@Status nvarchar(20),
@MobileNumber  nvarchar(20)
)
AS
BEGIN
  -- SET NOCOUNT ON added to prevent extra result sets from
  -- interfering with SELECT statements.
  SET NOCOUNT ON;
DECLARE @error_message NVARCHAR(MAX)
BEGIN TRY
   SELECT  @error_message = ''
		IF (@MobileNumber IS NULL ) 
        BEGIN 
         set @MobileNumber='%%'
        END
        ELSE
        BEGIN 
         set @MobileNumber='%' + @MobileNumber
        END
		IF (@Status IS NULL ) 
        BEGIN 
         set @Status='%%'
        END
        ELSE
        BEGIN 
         set @Status=@Status
        END

        IF (@FromDate IS NOT NULL)
        Begin
        IF isdate(@FromDate) =0 
        BEGIN
      raiserror('Wrong From date',16,-1)
      return
        END
        set @FromDate=cast(@FromDate as datetime)
        END
        
        IF (@ToDate IS NOT NULL)
        Begin
        IF isdate(@ToDate) =0 
        BEGIN
      raiserror('Wrong To date',16,-1)
      return
        END
        set @ToDate=cast(@ToDate as datetime)
        END
        
      IF (@FromDate IS NOT NULL) AND (@ToDate IS NOT NULL) AND (@Status IS NOT NULL)
        BEGIN
		  select TransactionCode, DepositStatus , WithdrawalStatus , NationalID,
				CancelStatus , WSendingStatus , ResendSMSFlag, Amount , TrxDatetime, WithdrawalDateTime, 
				RemitterID, BeneficiaryMobile, BeneficiaryMobile from irstransactions
				WHERE  ((WithdrawalDateTime between @FromDate and @ToDate) OR (TrxDatetime between @FromDate and @ToDate))
				AND  (Depositstatus=@Status OR WithdrawalStatus=@Status) 
				AND  (BeneficiaryMobile LIKE @MobileNumber or NationalID likE @MobileNumber)
		END
	ELSE IF (@FromDate IS NOT NULL) AND (@ToDate IS NOT NULL) AND (@Status IS NULL)
	BEGIN
		  select TransactionCode, DepositStatus , WithdrawalStatus,
				CancelStatus , WSendingStatus , ResendSMSFlag, Amount , TrxDatetime, WithdrawalDateTime, 
				RemitterID, BeneficiaryMobile,NationalID from irstransactions
				WHERE  ((WithdrawalDateTime between @FromDate and @ToDate) OR (TrxDatetime between @FromDate and @ToDate))
				AND  (BeneficiaryMobile LIKE @MobileNumber or NationalID likE @MobileNumber)
	END
	ELSE
	BEGIN
		  select TransactionCode, DepositStatus , WithdrawalStatus,
				CancelStatus , WSendingStatus , ResendSMSFlag, Amount , TrxDatetime, WithdrawalDateTime, 
				RemitterID, BeneficiaryMobile,NationalID from irstransactions
				WHERE  (BeneficiaryMobile LIKE @MobileNumber or NationalID likE @MobileNumber)
	END
  END TRY
    BEGIN CATCH
      SELECT  @error_message = ISNULL(OBJECT_NAME(@@PROCID),
                                      'Procedure Name Not Obtained') + ':'
              + ISNULL(ERROR_MESSAGE(), 'Error message not captured')
    RAISERROR(@error_message, 18, 1)  
    END CATCH
END



GO
/****** Object:  Table [dbo].[IRSAdvice]    Script Date: 8/5/2018 7:43:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IRSAdvice](
	[IRSTransactionID] [nvarchar](50) NOT NULL,
	[NCRTXID] [nvarchar](50) NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
	[LiquidationDate] [datetime] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[IRSMessages]    Script Date: 8/5/2018 7:43:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IRSMessages](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[requestType] [nvarchar](20) NOT NULL,
	[requestDateTime] [datetime] NULL,
	[beneficiaryMobile] [nvarchar](50) NULL,
	[beneficiaryName] [nvarchar](50) NULL,
	[currencyID] [int] NULL,
	[accountStatus] [int] NULL,
	[beneficiaryBranch] [nvarchar](50) NULL,
	[amount] [decimal](18, 4) NULL,
	[IRS_ID] [nvarchar](50) NULL,
	[transactionID] [nvarchar](50) NULL,
	[responseCode] [nvarchar](50) NULL,
	[errorDescription] [nvarchar](max) NULL,
 CONSTRAINT [PK_IRSMessages] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[IRSTransactions]    Script Date: 8/5/2018 7:43:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[IRSTransactions](
	[TransactionCode] [varchar](12) NOT NULL,
	[TrxDatetime] [datetime] NOT NULL,
	[IRSTransactionID] [varchar](10) NOT NULL,
	[RemitterName] [nvarchar](50) NOT NULL,
	[RemitterID] [nvarchar](50) NOT NULL,
	[BeneficiaryName] [varchar](50) NOT NULL,
	[BeneficiaryMobile] [varchar](20) NOT NULL,
	[BeneficiaryPIN] [varchar](256) NOT NULL,
	[Amount] [numeric](18, 0) NOT NULL,
	[CurrencyCode] [varchar](10) NOT NULL,
	[CancelStatus] [varchar](20) NULL,
	[CancelDateTime] [datetime] NULL,
	[WSendingStatus] [varchar](20) NULL,
	[WSentDateTime] [datetime] NULL,
	[NationalID] [varchar](512) NOT NULL,
	[WithdrawalStatus] [varchar](20) NULL,
	[ResendSMSFlag] [int] NOT NULL,
	[WithdrawalDateTime] [datetime] NULL,
	[Registered] [bit] NULL,
	[SMSLanguage] [nchar](10) NULL,
	[DepositStatus] [varchar](20) NULL,
	[ATMId] [varchar](20) NULL,
	[ATMTrxSequence] [varchar](10) NULL,
	[SMSSendingStatus] [varchar](20) NULL,
	[ResendTo] [int] NOT NULL,
	[ATMTerminalID] [varchar](20) NULL,
 CONSTRAINT [PK_IRSTransactions] PRIMARY KEY CLUSTERED 
(
	[TransactionCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[IRSMessages] ADD  CONSTRAINT [DF_IRSMessages_requestDateTime]  DEFAULT (getdate()) FOR [requestDateTime]
GO
ALTER TABLE [dbo].[IRSTransactions] ADD  CONSTRAINT [DF_IRSTransactions_TrxDatetime]  DEFAULT (getdate()) FOR [TrxDatetime]
GO
ALTER TABLE [dbo].[IRSTransactions] ADD  CONSTRAINT [DF_IRSTransactions_ResendSMSFlag]  DEFAULT ((0)) FOR [ResendSMSFlag]
GO
ALTER TABLE [dbo].[IRSTransactions] ADD  CONSTRAINT [DF_IRSTransactions_Registered]  DEFAULT ((1)) FOR [Registered]
GO
ALTER TABLE [dbo].[IRSTransactions] ADD  CONSTRAINT [DF_IRSTransactions_SMSLanguage]  DEFAULT ('E') FOR [SMSLanguage]
GO
ALTER TABLE [dbo].[IRSTransactions] ADD  CONSTRAINT [DF_IRSTransactions_DepositStatus]  DEFAULT ('CONFIRMED') FOR [DepositStatus]
GO
ALTER TABLE [dbo].[IRSTransactions] ADD  CONSTRAINT [DF_IRSTransactions_ATMId]  DEFAULT (NULL) FOR [ATMId]
GO
ALTER TABLE [dbo].[IRSTransactions] ADD  CONSTRAINT [DF_IRSTransactions_ATMTrxSequence]  DEFAULT (NULL) FOR [ATMTrxSequence]
GO
ALTER TABLE [dbo].[IRSTransactions] ADD  CONSTRAINT [DF_IRSTransactions_SMSSendingStatus]  DEFAULT (NULL) FOR [SMSSendingStatus]
GO
ALTER TABLE [dbo].[IRSTransactions] ADD  CONSTRAINT [DF_IRSTransactions_ResendTo]  DEFAULT ((3)) FOR [ResendTo]
GO
ALTER TABLE [dbo].[IRSTransactions]  WITH NOCHECK ADD  CONSTRAINT [FK_IRSTransactions_Currency] FOREIGN KEY([CurrencyCode])
REFERENCES [dbo].[Currency] ([CurrencyCode])
GO
ALTER TABLE [dbo].[IRSTransactions] CHECK CONSTRAINT [FK_IRSTransactions_Currency]
GO
