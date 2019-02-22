USE [inforerpdb]
GO
/****** Object:  StoredProcedure [dbo].[sppakERPRecHInsert]    Script Date: 22/02/2019 10:05:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sppakERPRecHInsert]
  @t_rcno VarChar(9),
  @t_revn VarChar(20),
  @t_cprj VarChar(9),
  @t_item VarChar(47),
  @t_bpid VarChar(9),
  @t_nama VarChar(35),
  @t_stat Int,
  @t_user VarChar(16),
  @t_date DateTime,
  @t_sent_1 Int,
  @t_sent_2 Int,
  @t_sent_3 Int,
  @t_sent_4 Int,
  @t_sent_5 Int,
  @t_sent_6 Int,
  @t_sent_7 Int,
  @t_rece_1 Int,
  @t_rece_2 Int,
  @t_rece_3 Int,
  @t_rece_4 Int,
  @t_rece_5 Int,
  @t_rece_6 Int,
  @t_rece_7 Int,
  @t_suer VarChar(16),
  @t_sdat DateTime,
  @t_appr VarChar(16),
  @t_adat DateTime,
  @t_subm_1 Int,
  @t_subm_2 Int,
  @t_subm_3 Int,
  @t_subm_4 Int,
  @t_subm_5 Int,
  @t_subm_6 Int,
  @t_subm_7 Int,
  @t_orno VarChar(9),
  @t_pono Int,
  @t_trno VarChar(9),
  @t_Refcntd Int,
  @t_Refcntu Int,
  @t_docn VarChar(3),
  @t_eunt VarChar(6),
  @Return_t_rcno VarChar(9) = null OUTPUT, 
  @Return_t_revn VarChar(20) = null OUTPUT 
  AS
  INSERT [tdmisg134200]
  (
   [t_rcno]
  ,[t_revn]
  ,[t_cprj]
  ,[t_item]
  ,[t_bpid]
  ,[t_nama]
  ,[t_stat]
  ,[t_user]
  ,[t_date]
  ,[t_sent_1]
  ,[t_sent_2]
  ,[t_sent_3]
  ,[t_sent_4]
  ,[t_sent_5]
  ,[t_sent_6]
  ,[t_sent_7]
  ,[t_rece_1]
  ,[t_rece_2]
  ,[t_rece_3]
  ,[t_rece_4]
  ,[t_rece_5]
  ,[t_rece_6]
  ,[t_rece_7]
  ,[t_suer]
  ,[t_sdat]
  ,[t_appr]
  ,[t_adat]
  ,[t_subm_1]
  ,[t_subm_2]
  ,[t_subm_3]
  ,[t_subm_4]
  ,[t_subm_5]
  ,[t_subm_6]
  ,[t_subm_7]
  ,[t_orno]
  ,[t_pono]
  ,[t_trno]
  ,[t_Refcntd]
  ,[t_Refcntu]
  ,[t_docn]
  ,[t_eunt]
  ,[t_atch]
  ,[t_rqno]
  ,[t_rqln]
  ,[t_pwfd]
  ,[t_wfid]
  ,[t_apid_1]
  ,[t_apid_2]
  ,[t_apid_3]
  ,[t_apid_4]
  ,[t_apid_5]
  ,[t_apid_6]
  ,[t_apid_7]
  )
  VALUES
  (
   UPPER(@t_rcno)
  ,UPPER(@t_revn)
  ,@t_cprj
  ,@t_item
  ,@t_bpid
  ,@t_nama
  ,@t_stat
  ,@t_user
  ,@t_date
  ,@t_sent_1
  ,@t_sent_2
  ,@t_sent_3
  ,@t_sent_4
  ,@t_sent_5
  ,@t_sent_6
  ,@t_sent_7
  ,@t_rece_1
  ,@t_rece_2
  ,@t_rece_3
  ,@t_rece_4
  ,@t_rece_5
  ,@t_rece_6
  ,@t_rece_7
  ,@t_suer
  ,@t_sdat
  ,@t_appr
  ,@t_adat
  ,@t_subm_1
  ,@t_subm_2
  ,@t_subm_3
  ,@t_subm_4
  ,@t_subm_5
  ,@t_subm_6
  ,@t_subm_7
  ,@t_orno
  ,@t_pono
  ,@t_trno
  ,@t_Refcntd
  ,@t_Refcntu
  ,@t_docn
  ,@t_eunt
  ,' ' 
  ,'ZZZZZZZZZ'
  ,0
  ,0
  ,0
  ,' '
  ,' '
  ,' '
  ,' '
  ,' '
  ,' '
  ,' '
  )
  SET @Return_t_rcno = @t_rcno
  SET @Return_t_revn = @t_revn
