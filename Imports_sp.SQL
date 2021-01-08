SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sppakBPLoginMapSelectByID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sppakBPLoginMapSelectByID]
GO
 
CREATE PROCEDURE [dbo].[sppakBPLoginMapSelectByID]
  @LoginID NVarChar(8),
  @LoginID NVarChar(8),
  @BPID NVarChar(9),
  @Comp NVarChar(10) 
  AS
  SELECT
    [SYS_BPLoginMap].* ,
    [aspnet_Users1].[UserFullName] AS aspnet_Users1_UserFullName 
  FROM [SYS_BPLoginMap] 
  INNER JOIN [aspnet_users] AS [aspnet_users1]
    ON [SYS_BPLoginMap].[LoginID] = [aspnet_users1].[LoginID]
  WHERE
  [SYS_BPLoginMap].[LoginID] = @LoginID
  AND [SYS_BPLoginMap].[BPID] = @BPID
  AND [SYS_BPLoginMap].[Comp] = @Comp
  GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sppakBPLoginMapSelectListSearch]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sppakBPLoginMapSelectListSearch]
GO
 
CREATE PROCEDURE [dbo].[sppakBPLoginMapSelectListSearch]
  @LoginID NVarChar(8),
  @StartRowIndex int,
  @MaximumRows int,
  @KeyWord VarChar(250),
  @OrderBy NVarChar(50),
  @RecordCount Int = 0 OUTPUT
  AS
  BEGIN
    DECLARE @KeyWord1 VarChar(260)
    SET @KeyWord1 = '%' + LOWER(@KeyWord) + '%'
  CREATE TABLE #PageIndex (
  IndexID INT IDENTITY (1, 1) NOT NULL
 ,LoginID NVarChar(8) NOT NULL
 ,BPID NVarChar(9) NOT NULL
 ,Comp NVarChar(10) NOT NULL
  )
  INSERT INTO #PageIndex (LoginID, BPID, Comp)
  SELECT [SYS_BPLoginMap].[LoginID], [SYS_BPLoginMap].[BPID], [SYS_BPLoginMap].[Comp] FROM [SYS_BPLoginMap]
  INNER JOIN [aspnet_users] AS [aspnet_users1]
    ON [SYS_BPLoginMap].[LoginID] = [aspnet_users1].[LoginID]
 WHERE  
   ( 
         LOWER(ISNULL([SYS_BPLoginMap].[LoginID],'')) LIKE @KeyWord1
     OR LOWER(ISNULL([SYS_BPLoginMap].[BPID],'')) LIKE @KeyWord1
     OR LOWER(ISNULL([SYS_BPLoginMap].[Comp],'')) LIKE @KeyWord1
   ) 
  ORDER BY (Select @OrderBy)

    SET @RecordCount = @@RowCount

  SELECT
    [SYS_BPLoginMap].* ,
    [aspnet_Users1].[UserFullName] AS aspnet_Users1_UserFullName 
  FROM [SYS_BPLoginMap] 
      INNER JOIN #PageIndex
          ON [SYS_BPLoginMap].[LoginID] = #PageIndex.LoginID
          AND [SYS_BPLoginMap].[BPID] = #PageIndex.BPID
          AND [SYS_BPLoginMap].[Comp] = #PageIndex.Comp
  INNER JOIN [aspnet_users] AS [aspnet_users1]
    ON [SYS_BPLoginMap].[LoginID] = [aspnet_users1].[LoginID]
  WHERE
        #PageIndex.IndexID > @StartRowIndex
        AND #PageIndex.IndexID < (@StartRowIndex + @MaximumRows + 1)
  ORDER BY
    #PageIndex.IndexID
  END
  GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sppakBPLoginMapSelectListFilteres]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sppakBPLoginMapSelectListFilteres]
GO
 
CREATE PROCEDURE [dbo].[sppakBPLoginMapSelectListFilteres]
  @Filter_BPID NVarChar(9),
  @Filter_Comp NVarChar(10),
  @LoginID NVarChar(8),
  @StartRowIndex int,
  @MaximumRows int,
  @OrderBy NVarChar(50),
  @RecordCount Int = 0 OUTPUT
  AS
  BEGIN
  DECLARE @LGSQL VarChar(8000)
  IF (@OrderBy = '')
    SET @OrderBy = 'LoginID, BPID, Comp'
  CREATE TABLE #PageIndex (
  IndexID INT IDENTITY (1, 1) NOT NULL
 ,LoginID NVarChar(8) NOT NULL
 ,BPID NVarChar(9) NOT NULL
 ,Comp NVarChar(10) NOT NULL
  )
  SET @LGSQL = 'INSERT INTO #PageIndex (' 
  SET @LGSQL +=  'LoginID'
  SET @LGSQL +=  ', BPID'
  SET @LGSQL +=  ', Comp'
  SET @LGSQL +=  ')'
  SET @LGSQL +=  ' SELECT '
  SET @LGSQL +=  '[SYS_BPLoginMap].[LoginID]'
  SET @LGSQL +=  ', [SYS_BPLoginMap].[BPID]'
  SET @LGSQL +=  ', [SYS_BPLoginMap].[Comp]'
  SET @LGSQL +=  ' FROM [SYS_BPLoginMap] '
  SET @LGSQL +=  '  INNER JOIN [aspnet_users] AS [aspnet_users1]'
  SET @LGSQL +=  '    ON [SYS_BPLoginMap].[LoginID] = [aspnet_users1].[LoginID]'
  SET @LGSQL +=  '  WHERE 1 = 1 '
  IF (@Filter_BPID > '') 
    SET @LGSQL +=  ' AND [SYS_BPLoginMap].[BPID] = ''' + @Filter_BPID + ''''
  IF (@Filter_Comp > '') 
    SET @LGSQL +=  ' AND [SYS_BPLoginMap].[Comp] = ''' + @Filter_Comp + ''''
  SET @LGSQL +=  '  ORDER BY ' + @OrderBy
 
 
  EXEC (@LGSQL)

  SET @RecordCount = @@RowCount

  SELECT
    [SYS_BPLoginMap].* ,
    [aspnet_Users1].[UserFullName] AS aspnet_Users1_UserFullName 
  FROM [SYS_BPLoginMap] 
      INNER JOIN #PageIndex
          ON [SYS_BPLoginMap].[LoginID] = #PageIndex.LoginID
          AND [SYS_BPLoginMap].[BPID] = #PageIndex.BPID
          AND [SYS_BPLoginMap].[Comp] = #PageIndex.Comp
  INNER JOIN [aspnet_users] AS [aspnet_users1]
    ON [SYS_BPLoginMap].[LoginID] = [aspnet_users1].[LoginID]
  WHERE
        #PageIndex.IndexID > @StartRowIndex
        AND #PageIndex.IndexID < (@StartRowIndex + @MaximumRows + 1)
  ORDER BY
    #PageIndex.IndexID
  END
  GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sppakBPLoginMapInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sppakBPLoginMapInsert]
GO
 
CREATE PROCEDURE [dbo].[sppakBPLoginMapInsert]
  @LoginID NVarChar(8),
  @BPID NVarChar(9),
  @Comp NVarChar(10),
  @Return_LoginID NVarChar(8) = null OUTPUT, 
  @Return_BPID NVarChar(9) = null OUTPUT, 
  @Return_Comp NVarChar(10) = null OUTPUT 
  AS
  INSERT [SYS_BPLoginMap]
  (
   [LoginID]
  ,[BPID]
  ,[Comp]
  )
  VALUES
  (
   UPPER(@LoginID)
  ,UPPER(@BPID)
  ,UPPER(@Comp)
  )
  SET @Return_LoginID = @LoginID
  SET @Return_BPID = @BPID
  SET @Return_Comp = @Comp
  GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sppakBPLoginMapUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sppakBPLoginMapUpdate]
GO
 
CREATE PROCEDURE [dbo].[sppakBPLoginMapUpdate]
  @Original_LoginID NVarChar(8), 
  @Original_BPID NVarChar(9), 
  @Original_Comp NVarChar(10), 
  @LoginID NVarChar(8),
  @BPID NVarChar(9),
  @Comp NVarChar(10),
  @RowCount int = null OUTPUT
  AS
  UPDATE [SYS_BPLoginMap] SET 
   [LoginID] = @LoginID
  ,[BPID] = @BPID
  ,[Comp] = @Comp
  WHERE
  [LoginID] = @Original_LoginID
  AND [BPID] = @Original_BPID
  AND [Comp] = @Original_Comp
  SET @RowCount = @@RowCount
  GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sppakBPLoginMapDelete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sppakBPLoginMapDelete]
GO
 
CREATE PROCEDURE [dbo].[sppakBPLoginMapDelete]
  @Original_LoginID NVarChar(8),
  @Original_BPID NVarChar(9),
  @Original_Comp NVarChar(10),
  @RowCount int = null OUTPUT
  AS
  DELETE [SYS_BPLoginMap]
  WHERE
  [SYS_BPLoginMap].[LoginID] = @Original_LoginID
  AND [SYS_BPLoginMap].[BPID] = @Original_BPID
  AND [SYS_BPLoginMap].[Comp] = @Original_Comp
  SET @RowCount = @@RowCount
  GO

SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO
