Option Strict Off
Option Explicit On
Imports bp3ft.bp3ft
Imports bp3doc.bp3doc
Imports bp3qry.bp3qry
Imports bp3dic.bp3dic
Public Class Generator
    Dim m As bp3doc.bp3doc.Application
    Dim dt_ot As DataTable
    Dim man As BP3.Manager
    Dim fTypes As bp3ft.bp3ft.Application

    Dim o As BP3Generator.Response
    Dim tid As String
    Dim log As String
    Dim ftmap As Collection

    Private mTables As Boolean
    Private mKernel As Boolean
    Private mViews As Boolean

    Private mInit As Boolean
    Private mProcs As Boolean
    Private mMethod As Boolean
    Private mManual As Boolean
    Private mRights As Boolean



    Public Map As Collection

    Public Sub Setup()
        Dim f As frmOptions
        f = New frmOptions
        f.ShowDialog()
        f.Close()
        f = Nothing
    End Sub



    Public Property OptTables() As Boolean
        Get
            OptTables = mTables
        End Get
        Set(ByVal Value As Boolean)
            mTables = Value
        End Set
    End Property


   


    Public Property OptManual() As Boolean
        Get
            OptManual = mManual
        End Get
        Set(ByVal Value As Boolean)
            mManual = Value
        End Set
    End Property



    Public Property OptRights() As Boolean
        Get
            OptRights = mRights
        End Get
        Set(ByVal Value As Boolean)
            mRights = Value
        End Set
    End Property


    Public Property OptMethod() As Boolean
        Get
            OptMethod = mMethod
        End Get
        Set(ByVal Value As Boolean)
            mMethod = Value
        End Set
    End Property


    Public Property OptProcs() As Boolean
        Get
            OptProcs = mProcs
        End Get
        Set(ByVal Value As Boolean)
            mProcs = Value
        End Set
    End Property



    Public Property OptInit() As Boolean
        Get
            OptInit = mInit
        End Get
        Set(ByVal Value As Boolean)
            mInit = Value
        End Set
    End Property


    Public Property OptViews() As Boolean
        Get
            OptViews = mViews
        End Get
        Set(ByVal Value As Boolean)
            mViews = Value
        End Set
    End Property


    Public Property OptKernel() As Boolean
        Get
            OptKernel = mKernel
        End Get
        Set(ByVal Value As Boolean)
            mKernel = Value
        End Set
    End Property

    Private Sub CreateInstBriefHdr()
        ' создаем первую табличку в индексе
        Dim Sql As Writer
        Sql = Nothing
        Sql = New Writer


        Sql = Nothing
        Sql = New Writer
        Sql.putBufLC(funcDropSQL("INSTANCE_BRIEF_F"))
        Sql.putBufLC("  create function INSTANCE_BRIEF_F  (")

        Sql.putBufLC(" ainstanceid varchar(38)")
        'MLF
        Sql.putBufLC(" ,aLang varchar(25)")
        'EMLF
        Sql.putBufLC(")returns varchar(255) DETERMINISTIC begin")
        Sql.putBufLC(" declare aBRIEF varchar(255);")
        Sql.putBufLC("  set aBRIEF=' to do';")
        Sql.putBufLC("  return aBRIEF;")
        Sql.putBufLC("End")
        Sql.putBufLC("GO")

        o.ModuleName = "--FunctionsHeader"
        o.Block = "--TableProc"
        o.OutNL(Sql.getBuf)


    End Sub

    Private Sub CreateInstBrief()
        Dim Sql As Writer

        Sql = Nothing
        Sql = New Writer
        Sql.putBufLC(funcDropSQL("INSTANCE_BRIEF_F"))
        Sql.putBufLC("  create function INSTANCE_BRIEF_F  (")
        'Sql.putBufLC(" ainstanceid varchar(38)")
        Sql.putBufLC(" ainstanceid binary(16)")
        Sql.putBufLC(" ,aLang varchar(25)")
        Sql.putBufLC(")returns varchar(255)")
        Sql.putBufLC("READS SQL DATA")
        Sql.putBufLC("begin")
        Sql.putBufLC(" declare aBRIEF varchar(255);")
        Sql.putBufLC(" declare aEC int;")
        Sql.putBufLC("if ainstanceid is null then set aBRIEF=''; return aBRIEF; end if;")
        Sql.putBufLC("select count(*) into aEC from instance where instanceID=ainstanceID;")
        Sql.putBufLC("if aEC=0 then")
        Sql.putBufLC("  set aBRIEF='';")
        Sql.putBufLC("  select concat(aBRIEF")
        Sql.putBufLC("  ,  IFNULL(Name,''))")
        Sql.putBufLC(" into aBRIEF from instance  where  instanceID = ainstanceID;")
        Sql.putBufLC("else")
        Sql.putBufLC("  set aBRIEF= '';")
        Sql.putBufLC("End if;")
        Sql.putBufLC("set aBRIEF=left(aBRIEF,255);")
        Sql.putBufLC("  return aBRIEF;")
        Sql.putBufLC("End")
        Sql.putBufLC("GO")

        o.ModuleName = "--Functions"
        o.Block = "--TableProc"
        o.OutNL(Sql.getBuf)
        o.OutNL("GO")
    End Sub

    Private Sub KernelProcs()

        Dim sql As Writer
        sql = New Writer

        System.Windows.Forms.Application.DoEvents()
        On Error GoTo bye



        sql.putBufLC(funcDropSQL("B2G"))
        sql.putBufLC("        CREATE  FUNCTION `B2G`(")
        sql.putBufLC("    $Data BINARY(16)")
        sql.putBufLC(") RETURNS char(38) ")
        sql.putBufLC("    DETERMINISTIC")
        sql.putBufLC("BEGIN")
        sql.putBufLC("    DECLARE $Result CHAR(38) DEFAULT NULL;")
        sql.putBufLC("    IF $Data IS NOT NULL THEN")
        sql.putBufLC("        SET $Result = CONCAT('{',HEX(SUBSTRING($Data,4,1)),HEX(SUBSTRING($Data,3,1)),HEX(SUBSTRING($Data,2,1)), HEX(SUBSTRING($Data,1,1)) , '-', ")
        sql.putBufLC("                HEX(SUBSTRING($Data,6,1)),HEX(SUBSTRING($Data,5,1)),'-',")
        sql.putBufLC("                HEX(SUBSTRING($Data,8,1)) , HEX(SUBSTRING($Data,7,1)),'-',")
        sql.putBufLC("                HEX(SUBSTRING($Data,9,2)),'-',HEX(SUBSTRING($Data,11,6)) ,'}');")
        sql.putBufLC("        SET $Result = UCASE($Result);")
        sql.putBufLC("    END IF;")
        sql.putBufLC("    RETURN $Result;")
        sql.putBufLC("END")
        sql.putBufLC("GO")

        sql.putBufLC(funcDropSQL("G2B"))
        sql.putBufLC("CREATE  FUNCTION `G2B`(")
        sql.putBufLC("    $Data VARCHAR(38)")
        sql.putBufLC(") RETURNS binary(16)")
        sql.putBufLC("    DETERMINISTIC")
        sql.putBufLC("BEGIN")
        sql.putBufLC("    DECLARE $Result BINARY(16) DEFAULT NULL;")
        sql.putBufLC("    IF $Data IS NOT NULL THEN")

        sql.putBufLC("        SET $Data = REPLACE($Data,'-','');")
        sql.putBufLC("        SET $Data = REPLACE($Data,'{','');")
        sql.putBufLC("        SET $Data = REPLACE($Data,'}','');")
        sql.putBufLC("        SET $Result = CONCAT(UNHEX(SUBSTRING($Data,7,2)),UNHEX(SUBSTRING($Data,5,2)),UNHEX(SUBSTRING($Data,3,2)), UNHEX(SUBSTRING($Data,1,2)),")
        sql.putBufLC("                UNHEX(SUBSTRING($Data,11,2)),UNHEX(SUBSTRING($Data,9,2)),UNHEX(SUBSTRING($Data,15,2)) , UNHEX(SUBSTRING($Data,13,2)),")
        sql.putBufLC("                UNHEX(SUBSTRING($Data,17,16)));")
        sql.putBufLC("    END IF;")
        sql.putBufLC("    RETURN $Result;")
        sql.putBufLC("END")
        sql.putBufLC("GO")

        sql.putBufLC(funcDropSQL("CheckOperation"))
        sql.putBufLC("CREATE  FUNCTION `CheckOperation`(")
        sql.putBufLC("    aCURSESSION varchar(38)")
        sql.putBufLC("    ,aOpName varchar(255)")
        sql.putBufLC(") RETURNS TINYINT(1)")
        sql.putBufLC("    READS SQL DATA")
        sql.putBufLC("BEGIN")
        sql.putBufLC("  declare IsOK int;")
        sql.putBufLC("  select 1 into IsOK;")
        sql.putBufLC(" /* select roles_operations.allowaction into IsOK from   roles_operations  ")
        sql.putBufLC("      join roles_def on roles_def.instanceid=roles_operations.instanceid")
        sql.putBufLC("      join the_Session on the_Session.userrole=roles_def.roles_defid")
        sql.putBufLC("      where the_Session.the_Sessionid=g2b(aCURSESSION) ")
        sql.putBufLC("      and roles_operations.name=aOpName; */")

        sql.putBufLC("  if IsOK<>0 then")
        sql.putBufLC("      return 1;")
        sql.putBufLC("  else")
        sql.putBufLC("      return 0;")
        sql.putBufLC("  end if;")
        sql.putBufLC("END")
        sql.putBufLC("GO")


        sql.putBufLC(procDropSQL("Login"))
        sql.putBufLC("CREATE  procedure `Login`(")
        sql.putBufLC(" out aTHE_SESSION varchar(38)/* Идентификатор новой сессии */")
        sql.putBufLC(",aPWD VARCHAR(80)/* Пароль */")
        sql.putBufLC(",aUSR VARCHAR (64)/* Имя пользователя */")
        sql.putBufLC(")")
        sql.putBufLC("body:   begin")
        sql.putBufLC("    declare aID binary(16); ")
        sql.putBufLC("    declare aUSERSID binary(16); ")
        sql.putBufLC("    declare asysid binary(16); ")
        sql.putBufLC("    declare existsCnt int;  ")
        sql.putBufLC("    declare aStatus varchar(38);")
        sql.putBufLC("set asysid = null; ")
        sql.putBufLC("select instanceid into asysid from instance where objtype = 'MTZSYSTEM'; ")
        sql.putBufLC("set athe_session=null  ; ")
        sql.putBufLC("set aUSERSID=null;")
        sql.putBufLC(" if aPWD is null then ")
        sql.putBufLC("    set athe_session=null  ; ")
        sql.putBufLC(" else ")
        sql.putBufLC("	 select USERSID into ausersid from users where Login=ausr and Password =MD5(aPWD); ")
        sql.putBufLC("	 set  aID=G2B(UUID())  ; ")
        sql.putBufLC("	 if not aUSERSID  is null then")
        sql.putBufLC("		 if asysid is null then ")
        sql.putBufLC("				insert into the_session(the_sessionid,lastaccess,usersid,closed,startAt,changestamp)")
        sql.putBufLC("				values(aid,sysdate,ausersid,0,now(),now()); ")
        sql.putBufLC("		else ")
        sql.putBufLC("				insert into the_session(instanceid,the_sessionid,lastaccess,usersid,closed,startAt,changestamp)")
        sql.putBufLC("				values(asysid,aid,now(),ausersid,0,now(),now()); ")
        sql.putBufLC("		end if; ")
        sql.putBufLC("		set athe_session=b2g(aid); ")
        sql.putBufLC("	 end if;")
        sql.putBufLC("	-- call build_usercache(b2g(aid));")
        sql.putBufLC(" end if;")
        sql.putBufLC("End")
        sql.putBufLC("GO")


        sql.putBufLC(procDropSQL("Logout"))
        sql.putBufLC("CREATE  PROCEDURE `Logout`(aCURSESSION varchar(38))/* Идентификатор сессии */")
        sql.putBufLC("body:   begin")
        sql.putBufLC("declare")
        sql.putBufLC("existsCnt integer;")
        sql.putBufLC("select count(*) into existsCnt from the_session where the_sessionid=g2b(acursession) and closed=0;")
        sql.putBufLC("if existsCnt >0 then")
        sql.putBufLC("    update INSTANCE set LockSessionID =null where LockSessionID=acursession ;")
        sql.putBufLC("    update the_session set closed=1,closedAt=now(), changeStamp=now() where ")
        sql.putBufLC("    the_sessionid=g2b(acursession);")
        sql.putBufLC("     delete from SysRefCache where sessionid= g2b(acursession);")
        sql.putBufLC(" End if;")
        sql.putBufLC("            End")
        sql.putBufLC("GO")

        sql.putBufLC(funcDropSQL("GetBriefFromXML"))
        sql.putBufLC("create function GetBriefFromXML (axmlSource varchar(255))")
        sql.putBufLC("RETURNS VarChar(255)")
        sql.putBufLC("  DETERMINISTIC ")
        sql.putBufLC("body:BEGIN")
        sql.putBufLC("declare aoutstr  varchar(255);")
        sql.putBufLC("declare afrom int;")
        sql.putBufLC("declare ato int;")
        sql.putBufLC("set afrom = CHARINDEX('<Brief>', axmlSource);")
        sql.putBufLC("set ato = CHARINDEX('</Brief>', axmlSource);")
        sql.putBufLC("if (afrom > 0 AND ato > 0) then")
        sql.putBufLC("  set aoutstr = substring(axmlSource, afrom + 7, ato - afrom - 7);")
        sql.putBufLC("else")
        sql.putBufLC("  set aoutstr = '';")
        sql.putBufLC("End if;")
        sql.putBufLC("return aoutstr;")
        sql.putBufLC("End")
        sql.putBufLC("GO")





        sql.putBufLC(funcDropSQL("GetIDFromXML"))
        sql.putBufLC("create function GetIDFromXML (axmlSource varchar(255))")
        sql.putBufLC("RETURNS VarChar(255)")
        sql.putBufLC(" DETERMINISTIC ")
        sql.putBufLC("BEGIN")
        sql.putBufLC("declare aoutstr  varchar(255);")
        sql.putBufLC("declare afrom  int;")
        sql.putBufLC("declare ato  int;")
        sql.putBufLC("")
        sql.putBufLC("set afrom = CHARINDEX('<ID>', axmlSource);")
        sql.putBufLC("set ato = CHARINDEX('</ID>', axmlSource);")
        sql.putBufLC("if (afrom > 0 AND ato > 0) then")
        sql.putBufLC("  set aoutstr = substring(axmlSource, afrom + 4, ato - afrom - 4);")
        sql.putBufLC(" else ")
        sql.putBufLC("  set aoutstr = '';")
        sql.putBufLC("End if;")
        sql.putBufLC("")
        sql.putBufLC("return aoutstr;")
        sql.putBufLC("End")
        sql.putBufLC("GO")

        CreateInstBriefHdr()
        CreateInstBrief()
        sql.putBufLC(procDropSQL("INSTANCE_OWNER"))
        sql.putBufLC("create procedure INSTANCE_OWNER (  acursession varchar(38) ,ainstanceid varchar(38), aOwnerPartName varchar(255), aOwnerRowID varchar(38))")
        sql.putBufLC("body:begin")
        sql.putBufLC("declare aEC  int;")
        sql.putBufLC("select count(*) into aEC from the_session where the_sessionid=g2b(acursession) and closed=0 ;")
        sql.putBufLC("if aEC=0  then")
        sql.putBufLC("    leave body;")
        sql.putBufLC("end if;")
        sql.putBufLC("select count(*) into aEC from instance where instanceid=g2b(ainstanceid);")
        sql.putBufLC("if aEC>0 then")
        sql.putBufLC("  if aOwnerPartName is null or aownerRowID is null then")
        sql.putBufLC("     update instance set OwnerPartName=null, OwnerRowid = null where instanceid=g2b(ainstanceid);")
        sql.putBufLC("  Else")
        sql.putBufLC("     update instance set OwnerPartName=aOwnerPartName, OwnerRowid = g2b(aOwnerRowID) where instanceid=g2b(ainstanceid);")
        sql.putBufLC("  End if;")
        sql.putBufLC("End if;")
        sql.putBufLC("End")
        sql.putBufLC("GO")
        sql.putBufLC("")
        '
        sql.putBufLC(procDropSQL("SysOptions_Save"))
        sql.putBufLC("create procedure SysOptions_Save ( aSysOptionsid varchar(38), aName varchar(255),aValue varchar (255), aOptionType varchar(255)) ")
        sql.putBufLC("begin")
        sql.putBufLC("declare aEC  int;")
        sql.putBufLC("select count(*) into aEC from sysoptions where sysoptionsid=g2b(asysoptionsid);")
        sql.putBufLC("if aEC>0 then")
        sql.putBufLC("  update sysoptions set Name=aName, Value=aValue, OptionType=aOptionType where sysoptionsid=g2b(asysoptionsid);")
        sql.putBufLC("Else")
        sql.putBufLC("  insert into sysoptions (sysoptionsid, Name, Value, OptionType)values(g2b(asysoptionsid),aName,aValue,aOptionType);")
        sql.putBufLC("End if;")
        sql.putBufLC("end")
        sql.putBufLC("GO")
        sql.putBufLC("")


        sql.putBufLC(procDropSQL("Instance_Save"))
        sql.putBufLC("create procedure Instance_Save (")
        sql.putBufLC("acursession varchar(38),")
        sql.putBufLC("aInstanceID varchar(38),")
        sql.putBufLC("aObjType varchar(255),")
        sql.putBufLC("aName varchar(255)")
        sql.putBufLC(") ")
        sql.putBufLC("body:begin")
        sql.putBufLC(" declare atmpStr varchar(255);")
        sql.putBufLC(" declare aSSID BINARY(16);")
        sql.putBufLC(" declare atmpID BINARY(16);")
        sql.putBufLC(" declare aSysLogID BINARY(16);")
        sql.putBufLC(" declare aaccess int;")
        sql.putBufLC(" declare aSysInstID BINARY(16);")
        sql.putBufLC(" declare aStatusID BINARY(16);")
        sql.putBufLC("declare aEC  int;")
        sql.putBufLC("select count(*) into aEC from the_session where the_sessionid=g2b(acursession) and closed=0 ;")
        sql.putBufLC("if aEC=0  then")
        sql.putBufLC("    leave body;")
        sql.putBufLC("end if;")
        sql.putBufLC("select count(*) into aEC from instance where instanceid=g2b(ainstanceid);")
        sql.putBufLC("if aEC>0 then")
        sql.putBufLC("   update instance set name = aname where  instanceid=g2b(ainstanceid);")
        sql.putBufLC("Else")
        sql.putBufLC("    select count(*) into aEC from typelist where name = aobjtype;")
        sql.putBufLC("    if aEC then")
        sql.putBufLC("      start transaction;")
        sql.putBufLC("      set astatusid=null;")
        sql.putBufLC("      select objstatusid into astatusid from objstatus join bp3doc_def on")
        sql.putBufLC("      bp3doc_def.objecttypeid=objstatus.parentstructrowid and bp3doc_def.name=aobjtype and isStartup<>0;")
        sql.putBufLC("      if not astatusid is null then")
        sql.putBufLC("        insert into instance(instanceid,name,objtype,STATUS) values(g2b(ainstanceid),aname,aobjtype,g2b(aSTATUSID));")
        sql.putBufLC("      else ")
        sql.putBufLC("        insert into instance(instanceid,name,objtype) values(g2b(ainstanceid),aname,aobjtype);")
        sql.putBufLC("      end if; ")
        sql.putBufLC("      commit;")
        sql.putBufLC("    End if;")
        sql.putBufLC("End if;")
        sql.putBufLC("select 'OK' result;")
        sql.putBufLC("End")
        sql.putBufLC("GO")
        '

        sql.putBufLC(procDropSQL("Instance_DELETE"))
        sql.putBufLC("create procedure Instance_DELETE (")
        sql.putBufLC("acursession varchar(38),")
        sql.putBufLC("aInstanceID varchar(38)")
        sql.putBufLC(")")
        sql.putBufLC("body:begin")
        sql.putBufLC("   declare aEC  int;")
        sql.putBufLC("select count(*) into aEC from the_session where the_sessionid=g2b(acursession) and closed=0 ;")
        sql.putBufLC("if aEC=0  then")
        sql.putBufLC("    leave body;")
        sql.putBufLC("end if;")
        sql.putBufLC("   select count(*) into aEC from instance where instanceid=g2b(ainstanceid);")
        sql.putBufLC("    if aEC>0 then")
        sql.putBufLC("         delete from instance where instanceid=g2b(ainstanceid);")
        sql.putBufLC("        End if;")
        sql.putBufLC("End")
        sql.putBufLC("GO")

        sql.putBufLC(procDropSQL("Instance_ARCHIVE"))
        sql.putBufLC("create procedure Instance_ARCHIVE (")
        sql.putBufLC("acursession varchar(38),")
        sql.putBufLC("aInstanceID varchar(38)")
        sql.putBufLC(")")
        sql.putBufLC("body:begin")
        sql.putBufLC("   declare aEC  int;")
        sql.putBufLC("select count(*) into aEC from the_session where the_sessionid=g2b(acursession) and closed=0 ;")
        sql.putBufLC("if aEC=0  then")
        sql.putBufLC("    leave body;")
        sql.putBufLC("end if;")
        sql.putBufLC("   select count(*) into aEC from instance where instanceid=g2b(ainstanceid);")
        sql.putBufLC("    if aEC>0 then")
        sql.putBufLC("         update instance set archived=1 where instanceid=g2b(ainstanceid);")
        sql.putBufLC("    End if;")
        sql.putBufLC("End")
        sql.putBufLC("GO")

        sql.putBufLC(procDropSQL("Instance_REARCHIVE"))
        sql.putBufLC("create procedure Instance_REARCHIVE (")
        sql.putBufLC("acursession varchar(38),")
        sql.putBufLC("aInstanceID varchar(38)")
        sql.putBufLC(")")
        sql.putBufLC("body:begin")
        sql.putBufLC("   declare aEC  int;")
        sql.putBufLC("select count(*) into aEC from the_session where the_sessionid=g2b(acursession) and closed=0 ;")
        sql.putBufLC("if aEC=0  then")
        sql.putBufLC("    leave body;")
        sql.putBufLC("end if;")
        sql.putBufLC("   select count(*) into aEC from instance where instanceid=g2b(ainstanceid);")
        sql.putBufLC("    if aEC>0 then")
        sql.putBufLC("         update instance set archived=0 where instanceid=g2b(ainstanceid);")
        sql.putBufLC("    End if;")
        sql.putBufLC("End")
        sql.putBufLC("GO")

        sql.putBufLC(procDropSQL("Instance_HCL"))
        sql.putBufLC("create procedure Instance_HCL (")
        sql.putBufLC("acursession varchar(38),")
        sql.putBufLC("aRowID varchar(38),")
        sql.putBufLC("out aIsLocked int")
        sql.putBufLC(") ")
        sql.putBufLC("body:begin")
        sql.putBufLC("declare atmpStr varchar(255);")
        sql.putBufLC("declare aObjType varchar(255);")
        sql.putBufLC("   declare aEC  int;")
        sql.putBufLC("select count(*) into aEC from the_session where the_sessionid=g2b(acursession) and closed=0 ;")
        sql.putBufLC("if aEC=0  then")
        sql.putBufLC("    leave body;")
        sql.putBufLC("end if;")
        sql.putBufLC("   select count(*) into aEC from instance where instanceid=g2b(ainstanceid);")
        sql.putBufLC("    if aEC>0 then")
        'sql.putBufLC("      select aobjtype = objtype from instance where instanceid=arowid;")
        'sql.putBufLC("      select atmpstr =HCLProc from typelist where name = aobjtype")
        'sql.putBufLC("      if not atmpstr is null")
        'sql.putBufLC("        call atmpstr acursession = acursession, aROWid =arowid,aIsLocked=aIsLocked out")
        sql.putBufLC("    set aisLocked=0;")
        sql.putBufLC("    End if;")
        sql.putBufLC("End")
        sql.putBufLC("GO")




        sql.putBufLC(procDropSQL("INSTANCE_ISLOCKED"))
        sql.putBufLC(" create  procedure INSTANCE_ISLOCKED (")
        sql.putBufLC(" acursession varchar(38),")
        sql.putBufLC(" arowid varchar(38) ,")
        sql.putBufLC(" out aIsLocked integer ")
        sql.putBufLC(") body:begin")
        sql.putBufLC(" declare aUserID BINARY(16);")
        sql.putBufLC(" declare aLockUserID BINARY(16);")
        sql.putBufLC(" declare aLockSessionID BINARY(16);")
        sql.putBufLC("   declare aEC  int;")
        sql.putBufLC(" set aisLocked = 0;")
        sql.putBufLC("select count(*) into aEC from the_session where the_sessionid=g2b(acursession) and closed=0 ;")
        sql.putBufLC("if aEC=0  then")
        sql.putBufLC("    leave body;")
        sql.putBufLC("end if;")
        sql.putBufLC("   select count(*) into aEC from instance where instanceid=g2b(arowid);")
        sql.putBufLC("   if aEC>0 then")
        sql.putBufLC("        select auserID = usersid  from the_session where the_sessionid=g2b(acursession);")
        sql.putBufLC("        select aLockUserID = LockUserID,aLockSessionID = LockSessionID from INSTANCE where INSTANCEid=g2b(arowid);")

        sql.putBufLC("        /* verify this row */")
        sql.putBufLC("   if not aLockUserID is null then")

        sql.putBufLC("    if  aLockUserID <> auserID then")
        sql.putBufLC("      set aisLocked = 4; /* CheckOut by another user */")
        sql.putBufLC("      LEAVE body;")
        sql.putBufLC("    else")

        sql.putBufLC("     set aisLocked = 2; /* CheckOut by caller */")
        sql.putBufLC("     LEAVE body;")
        sql.putBufLC("    End if;")
        sql.putBufLC("   End if;")
        sql.putBufLC("   if not aLockSessionID is null then")
        sql.putBufLC("        if  aLockSessionID <> aCURSESSION then")
        sql.putBufLC("            set aisLocked = 3; /* Lockes by another user */")
        sql.putBufLC("            LEAVE body;")
        sql.putBufLC("        else")
        sql.putBufLC("            set aisLocked = 1; /* Locked by caller */")
        sql.putBufLC("            LEAVE body;")
        sql.putBufLC("        End if;")
        sql.putBufLC("   End if;")
        sql.putBufLC("  End if;")
        sql.putBufLC(" End")
        sql.putBufLC("GO")

        sql.putBufLC(procDropSQL("QR_or_QR"))
        sql.putBufLC("create procedure QR_or_QR( aid1 varchar(38), aid2 varchar(38),aidout varchar(38),out acnt integer )")
        sql.putBufLC("body:begin")
        sql.putBufLC("delete from QueryResult where QueryResultid=g2b(aidout);")
        sql.putBufLC("insert into QueryResult(QueryResultid,result)")
        sql.putBufLC("select distinct aidout, result from QueryResult where QueryResultid in (aid1,aid2);")
        sql.putBufLC("select acnt=count(*) from QueryResult where QueryResultid=g2b(aidout);")
        sql.putBufLC("end")
        sql.putBufLC("GO")



        sql.putBufLC(procDropSQL("Instance_LOCK"))
        sql.putBufLC("create  procedure Instance_LOCK  (")
        sql.putBufLC(" acursession varchar(38),")
        sql.putBufLC(" aRowID varchar(38) ,")
        sql.putBufLC(" aLockMode integer")
        sql.putBufLC(") body:begin")
        sql.putBufLC("")
        sql.putBufLC(" declare aUserID BINARY(16);")
        sql.putBufLC(" declare atmpID BINARY(16);")
        sql.putBufLC(" declare aaccess integer;")
        sql.putBufLC(" declare aIsLocked integer;")
        sql.putBufLC(" declare aEC  int;")
        sql.putBufLC(" select count(*) into aEC from the_session where the_sessionid=g2b(acursession) and closed=0 ;")
        sql.putBufLC(" if aEC=0  then")
        sql.putBufLC("    leave body;")
        sql.putBufLC(" end if;")
        sql.putBufLC(" select count(*) into aEC from instance where instanceid=g2b(arowid);")
        sql.putBufLC(" if aEC>0 then")
        sql.putBufLC("  select auserID = usersid  from the_session where the_sessionid=g2b(acursession);")
        sql.putBufLC("  call Instance_ISLOCKED( aCURSESSION,aROWID,aISLocked);")
        sql.putBufLC("  if aIsLocked >=3 then")
        sql.putBufLC("    LEAVE body;")
        sql.putBufLC("  End if;")
        'sql.putBufLC(" if aIsLocked =0")
        'sql.putBufLC(" begin")
        'sql.putBufLC("  call Instance_HCL acursession,aRowID,aisLocked out")
        'sql.putBufLC("  if aIsLocked >=3")
        'sql.putBufLC("   begin")
        'sql.putBufLC("     raiserror('У данного объекта имеются дочерние строки, которые заблокированы другим пользователем',16,1)")
        'sql.putBufLC("     LEAVE body;")
        'sql.putBufLC("   End")
        'sql.putBufLC(" End")


        sql.putBufLC("   if  aLockMode =2 then")
        sql.putBufLC("    update INSTANCE  set LockUserID =g2b(auserID ),LockSessionID=null  where Instanceid=g2b(aRowID);")
        sql.putBufLC("     LEAVE body;")
        sql.putBufLC("   End if;")
        sql.putBufLC("   if  aLockMode =1 then")
        sql.putBufLC("    update INSTANCE  set LockUserID=null ,LockSessionID =g2b(aCURSESSION)  where Instanceid=g2b(aRowID);")
        sql.putBufLC("     LEAVE body;")
        sql.putBufLC("   End if;")
        sql.putBufLC("End if;")
        sql.putBufLC(" End")
        sql.putBufLC("")
        sql.putBufLC("GO")


        sql.putBufLC(procDropSQL("Instance_UNLOCK"))
        sql.putBufLC("create  procedure Instance_UNLOCK /*Пользователи системы*/ (")
        sql.putBufLC(" acursession varchar(38),")
        sql.putBufLC(" aRowID varchar(38)")
        sql.putBufLC(") body:begin")
        sql.putBufLC(" declare aParentID BINARY(16);")
        sql.putBufLC(" declare aUserID BINARY(16);")
        sql.putBufLC(" declare aIsLocked int;")
        sql.putBufLC(" declare aParentTable varchar(255);")
        sql.putBufLC(" declare aEC  int;")
        sql.putBufLC(" select count(*) into aEC from the_session where the_sessionid=g2b(acursession) and closed=0 ;")
        sql.putBufLC(" if aEC=0  then")
        sql.putBufLC("    leave body;")
        sql.putBufLC(" end if;")
        sql.putBufLC(" select count(*) into aEC from instance where instanceid=g2b(arowid);")
        sql.putBufLC(" if aEC>0 then")
        sql.putBufLC(" call Instance_ISLOCKED( aCURSESSION,aROWID,aISLocked );")
        sql.putBufLC(" if aIsLocked >=3 then")
        sql.putBufLC("    LEAVE body;")
        sql.putBufLC("  End if;")
        sql.putBufLC("   if  aIsLocked =2 then")
        sql.putBufLC("    update INSTANCE set LockUserID=null   where Instanceid=g2b(aRowID);")
        sql.putBufLC("     LEAVE body;")
        sql.putBufLC("   End if;")
        sql.putBufLC("   if  aIsLocked =1 then")
        sql.putBufLC("    update INSTANCE set LockSessionID=null   where Instanceid=g2b(aRowID);")
        sql.putBufLC("     LEAVE body;")
        sql.putBufLC("   End if;")
        sql.putBufLC("End if;")
        sql.putBufLC(" End")
        sql.putBufLC("")
        sql.putBufLC("GO")

        sql.putBufLC(procDropSQL("instance_BRIEF"))
        sql.putBufLC("  create procedure instance_BRIEF  (")
        sql.putBufLC(" acursession varchar(38),")
        sql.putBufLC(" ainstanceid varchar(38),")
        sql.putBufLC(" out aBRIEF varchar(255)")
        sql.putBufLC(") body:begin")
        sql.putBufLC(" declare atmpStr varchar(255);")
        sql.putBufLC(" declare aaccess int;")
        sql.putBufLC(" declare atmpBrief varchar(255);")
        sql.putBufLC(" declare atmpID BINARY(16);")
        sql.putBufLC(" declare aEC  int;")
        sql.putBufLC(" select count(*) into aEC from the_session where the_sessionid=g2b(acursession) and closed=0 ;")
        sql.putBufLC(" if aEC=0  then")
        sql.putBufLC("    leave body;")
        sql.putBufLC(" end if;")
        sql.putBufLC(" if ainstanceid is null then set aBRIEF=''; LEAVE body; end if;")

        sql.putBufLC(" select count(*) into aEC from instance where instanceid=g2b(ainstanceid);")
        sql.putBufLC(" if aEC>0 then")
        sql.putBufLC("   select concat(IFNULL(Name,''),'; ',IFNULL(objtype,'')) into aBRIEF ")
        sql.putBufLC("   from instance  where  instanceID = g2b(ainstanceID);")
        sql.putBufLC(" else ")
        sql.putBufLC("   set aBRIEF= 'неверный идентификатор';")
        sql.putBufLC("End if;")
        sql.putBufLC("End")
        sql.putBufLC("")
        sql.putBufLC("GO")


        sql.putBufLC(procDropSQL("RowParents"))
        sql.putBufLC("create  procedure RowParents")
        sql.putBufLC("        (aQueryID varchar(38)")
        sql.putBufLC("        ,aRowID varchar(38)")
        sql.putBufLC("        ,aTable VARCHAR (255)")
        sql.putBufLC("        ,aCURSESSION varchar(38)")
        sql.putBufLC("        )")
        sql.putBufLC("body:   begin")
        sql.putBufLC("declare aplevel integer;")
        sql.putBufLC("declare aparent varchar(255);")
        sql.putBufLC("declare aprev varchar(255);")
        sql.putBufLC("declare aec int;")
        sql.putBufLC(" select count(*) into aec from the_session where the_sessionid=g2b(acursession) and closed=0 ;")
        sql.putBufLC(" if aec=0  then")
        sql.putBufLC("    leave body;")
        sql.putBufLC("  end if;")
        sql.putBufLC("set aparent =atable;")
        sql.putBufLC("set @_tmpID = g2b(aROWID);")
        sql.putBufLC("set aplevel =0;")
        sql.putBufLC("delete from RPRESULT where RPRESULTID  =g2b(aQUERYID);")
        sql.putBufLC("insert into RPRESULT(RPRESULTID,PARENTLEVEL,PARTNAME,ROWID)values(g2b(aQUERYID),aPLEVEL,atable,g2b(aRowID));")
        sql.putBufLC("again:loop")
        sql.putBufLC("set aplevel =aplevel + 1;")
        sql.putBufLC("set aprev = aparent;")
        sql.putBufLC("set aparent = null;")
        sql.putBufLC("select value into aparent from sysoptions where optiontype ='parent' and  name=aprev;")
        sql.putBufLC(" if aparent is null then")
        sql.putBufLC("    set @_ss = concat('select InstanceID into @_tmpRowID from ' , aprev , ' where ' ,aprev ,'id=?');")
        sql.putBufLC("    PREPARE stmt FROM @_ss;")
        sql.putBufLC("    EXECUTE stmt USING @_tmpid;")
        sql.putBufLC("    DEALLOCATE PREPARE stmt;")
        sql.putBufLC("   insert into RPRESULT(RPRESULTID,PARENTLEVEL,PARTNAME,ROWID)values(g2b(aQUERYID),aPLEVEL,'INSTANCE',@_tmpRowID);")
        sql.putBufLC("   leave again;")
        sql.putBufLC(" Else ")
        sql.putBufLC("    set @_ss = concat('select ParentStructRowID  into @_tmpRowID from ' , aprev , ' where ' ,aprev ,'id=?');")
        sql.putBufLC("    PREPARE stmt FROM @_ss;")
        sql.putBufLC("    EXECUTE stmt USING @_tmpid;")
        sql.putBufLC("    DEALLOCATE PREPARE stmt;")
        sql.putBufLC("    set @_tmpID = @_tmpROWID;")
        sql.putBufLC("   insert into RPRESULT(RPRESULTID,PARENTLEVEL,PARTNAME,ROWID)")
        sql.putBufLC("   values(g2b(aQUERYID),aPLEVEL,aparent,@_tmpRowID);")
        sql.putBufLC(" End if;")
        sql.putBufLC("End loop again;")
        sql.putBufLC("End ")
        sql.putBufLC("GO")


        o.ModuleName = "--Kernel procs"
        o.Block = "--body"
        o.OutNL(sql.getBuf)


        sql = Nothing

        CreateInstMultirefFunc()


        Exit Sub
bye:
        Console.WriteLine("ERROR-" & Err.Description & "<--ERROR")
        sql = Nothing
    End Sub

    Private Sub Kernel()
        Dim sql As Writer
        sql = New Writer

        System.Windows.Forms.Application.DoEvents()
        On Error GoTo bye
        'MTZUtilUtility_definst.DebugOutput("MYSQLGEN.KERNEL:start")


        sql.putBufLC("-- Kernel Tables --")


        sql.putBufLC("create table IF NOT EXISTS sysoptions(")
        sql.putBufLC("sysoptionsID BINARY(16) primary key ,")
        sql.putBufLC("Name varchar(255) null,")
        sql.putBufLC("Value varchar(255) null,")
        sql.putBufLC("OptionType VarChar(255) null")
        sql.putBufLC(")")
        sql.putBufLC("GO")



        sql.putBufLC("create table IF NOT EXISTS typelist(")
        sql.putBufLC("typelistID BINARY(16) primary key ,")
        sql.putBufLC("Name varchar(255) not null,")
        sql.putBufLC("SecurityStyleID BINARY(16) null, /* default security style for type */")
        sql.putBufLC("RegisterProc varchar(255) null,")
        sql.putBufLC("DeleteProc VarChar(255) null,")
        sql.putBufLC("HCLProc varchar(255) null /* has children locked */,")
        sql.putBufLC("PropagateProc varchar(255) null /* propagate secrity styleto children */")
        sql.putBufLC(")")
        sql.putBufLC("GO")



        sql.putBufLC("create table IF NOT EXISTS Instance(")
        sql.putBufLC("InstanceID BINARY(16) PRIMARY KEY ,")
        sql.putBufLC("LockUserID BINARY(16) null, ")
        sql.putBufLC("LockSessionID BINARY(16) null, ")
        sql.putBufLC("SecurityStyleID BINARY(16) null, /* default security style for document */")
        sql.putBufLC("changestamp date null,")
        sql.putBufLC("Name varchar(255) null,")
        sql.putBufLC("ObjType VarChar(255) null,")
        sql.putBufLC("ExportCounter Integer null, ")
        sql.putBufLC(" OwnerPartName varchar(255) null")
        sql.putBufLC(",OwnerRowID BINARY(16) null")
        sql.putBufLC(", status BINARY(16) null")
        sql.putBufLC(", archived int null default 0")
        sql.putBufLC(")")
        sql.putBufLC("GO")


        sql.putBufLC("create table IF NOT EXISTS QueryResult (")
        sql.putBufLC("  QueryResultid BINARY(16) NOT NULL ,")
        sql.putBufLC("  result BINARY(16) NULL ")
        sql.putBufLC(")")
        sql.putBufLC("GO")


        sql.putBufLC("create table IF NOT EXISTS RPRESULT (")
        sql.putBufLC("  RPRESULTID BINARY(16) NOT NULL ,")
        sql.putBufLC("  PARENTLEVEL int NOT NULL ,")
        sql.putBufLC("  PARTNAME varchar (255) NULL ,")
        sql.putBufLC("  ROWID BINARY(16) NULL )")
        sql.putBufLC("GO")

        Dim s As Writer
        s = Nothing
        s = New Writer

        s.putBufLC(procDropSQL("alter_pk"))
        s.putBufLC("create procedure alter_pk() begin")
        s.putBufLC("    IF not EXISTS (SELECT 1 FROM information_schema.TABLE_CONSTRAINTS WHERE")
        s.putBufLC("           CONSTRAINT_SCHEMA = DATABASE() AND ")
        s.putBufLC("           TABLE_NAME   = 'RPRESULT' AND ")
        s.putBufLC("           CONSTRAINT_TYPE   = 'PRIMARY KEY') THEN")
        s.putBufLC("            alter table RPRESULT add constraint pk_RPRESULT primary key (RPRESULTID,PARENTLEVEL);")
        s.putBufLC("    End If;")
        s.putBufLC("End ")
        s.putBufLC("GO")
        s.putBufLC("call alter_pk(); ")
        s.putBufLC("GO")
        s.putBufLC(procDropSQL("alter_pk"))



        o.ModuleName = "--Kernel tables"
        o.Block = "--body"
        o.OutNL(sql.getBuf)


        sql = New Writer
        sql.putBufLC(viewDropSQL("v_INSTANCE"))
        sql.putBufLC("create view v_INSTANCE as")
        sql.putBufLC("select B2G(`instance`.`InstanceID`) AS `InstanceID`,`instance`.`LockUserID` AS `LockUserID`,`instance`.`LockSessionID` AS `LockSessionID`,`instance`.`SecurityStyleID` AS `SecurityStyleID`,`instance`.`Name` AS `Name`,`instance`.`ObjType` AS `ObjType`,`instance`.`OwnerPartName` AS `OwnerPartName`,`instance`.`OwnerRowID` AS `OwnerRowID`,`instance`.`status` AS `status`,`instance`.`archived` AS `archived`,`objstatus`.`name` AS `statusname`,`objstatus`.`IsArchive` AS `IsArchive` from (`instance` left join `objstatus` on((`instance`.`status` = `objstatus`.`OBJSTATUSid`)));")
        sql.putBufLC("GO")
        o.ModuleName = "--Kernel views"
        o.Block = "--body"
        o.OutNL(sql.getBuf)




        sql = Nothing
        CreateInstMultirefFunc()
        Exit Sub
bye:
        Console.WriteLine("ERROR-" & Err.Description & "<--ERROR")
        sql = Nothing
    End Sub


    Public Function Run(ByRef Manager As Object, ByRef Output As Object, ByRef targetid As String) As String
        man = Manager
        o = Output
        tid = targetid


        OptInit = CBool(GetSetting(My.Application.Info.Title, "MYSQLGEN", "INIT", "True"))
        OptKernel = CBool(GetSetting(My.Application.Info.Title, "MYSQLGEN", "KERNEL", "True"))
        OptMethod = CBool(GetSetting(My.Application.Info.Title, "MYSQLGEN", "METHODS", "True"))
        OptProcs = CBool(GetSetting(My.Application.Info.Title, "MYSQLGEN", "PROCS", "True"))
        OptTables = CBool(GetSetting(My.Application.Info.Title, "MYSQLGEN", "TABLES", "True"))
        OptViews = CBool(GetSetting(My.Application.Info.Title, "MYSQLGEN", "VIEW", "True"))
        OptManual = CBool(GetSetting(My.Application.Info.Title, "MYSQLGEN", "MANUAL", "True"))
        OptRights = CBool(GetSetting(My.Application.Info.Title, "MYSQLGEN", "RIGHTS", "True"))



        o.ModuleName = "--Kernel tables"
        o.Block = "--body"
        o.OutNL(" ")

        o.ModuleName = "--Tables"
        o.Block = "--body"
        o.OutNL(" ")

        o.ModuleName = "--Tables"
        o.Block = "--Index"
        o.OutNL(" ")

        o.ModuleName = "--Tables"
        o.Block = "--ForeignKey"
        o.OutNL("SET @@foreign_key_checks = 0;")
        o.OutNL("GO")



        o.ModuleName = "--FunctionsHeader"
        o.Block = "--TableProc"
        o.OutNL(" ")

        o.ModuleName = "--Functions"
        o.Block = "--TableProc"
        o.OutNL(" ")

        o.ModuleName = "--Kernel procs"
        o.Block = "--body"
        o.OutNL(" ")

        o.ModuleName = "--Procedures"
        o.Block = "--TableProc"
        o.OutNL(" ")



        o.ModuleName = "--Procedures"
        o.Block = "--Methods"
        o.OutNL(" ")

        o.ModuleName = "--Kernel views"
        o.Block = "--body"
        o.OutNL(" ")

        o.ModuleName = "--Views--"
        o.Block = "--Views--"
        o.OutNL(" ")

        o.ModuleName = "--Procs"
        o.Block = "--body"
        o.OutNL(" ")


        o.ModuleName = "--ManualCode"
        o.Block = "--body"
        o.OutNL(" ")


        o.ModuleName = "--Maintains"
        o.Block = "--create"
        o.OutNL(" ")


        o.ModuleName = "--Options"
        o.Block = "--Load"
        o.OutNL(" ")


        If OptKernel Then
            Kernel()
            KernelProcs()
        ElseIf OptViews Then
            CreateInstMultirefFunc()
            CreateInstMultirefFuncHdr()
            CreateInstBriefHdr()
            CreateInstBrief()
        End If




        Dim j, i, k As Integer
        Dim os As bp3doc.bp3doc.bp3doc_store

        On Error GoTo bye

        dt_ot = man.Session.GetData("select " + man.Session.GetProvider().ID2Base("instanceid", "instanceid") + " from bp3doc_def ")


        For i = 0 To dt_ot.Rows.Count - 1
            m = man.GetInstanceObject(New Guid(dt_ot.Rows(i)("instanceid").ToString()))

            Console.WriteLine("Create code for type " & m.bp3doc_def.Item(1).Name)

            o.ModuleName = "--Tables"
            o.Block = "--body"
            o.OutNL("/* TYPE=" & m.bp3doc_def.Item(1).Name & " (" & m.bp3doc_def.Item(1).TheComment & ") */")
            o.OutNL("GO")

            If OptProcs Then
                CreateTypeCopyProc(m)
                CreateTypeProcs(m)

                ' CommitFullObject
                If m.bp3doc_def.Item(1).CommitFullObject = bp3doc.bp3doc.enumBoolean.Boolean_Da Then
                    CreateTypeGetToTempProc(m)
                    CreateTypeCommitFromTempProc(m)
                    CreateTypeDropTempDataProc(m)
                End If
            End If




            For j = 1 To m.bp3doc_store.Count
                os = m.bp3doc_store.Item(j)
                If OptTables Then
                    CreateStruct(os)
                    If os.UseChangeLog = bp3doc.bp3doc.enumBoolean.Boolean_Da Then CreateStructLog(os)
                    ' CommitFullObject
                    If m.bp3doc_def.Item(1).CommitFullObject = bp3doc.bp3doc.enumBoolean.Boolean_Da Then
                        CreateStructTemp(os)
                    End If

                End If

                If OptProcs Then
                    If m.bp3doc_def.Item(1).CommitFullObject = bp3doc.bp3doc.enumBoolean.Boolean_Da Then
                        CreatePartGetToTempProc(os)
                        CreatePartCommitFromTempProc(os)
                        CreatePartDropTempDataProc(os)
                    End If

                    CreateProc(os)
                    CreatePartCopyProc(os)
                    CreateLoggerProc(os)
                    CreateV2Proc(os)

                End If


            Next
            o.Status(m.bp3doc_def.Item(1).TheComment & " done", i)
        Next

        If OptViews Then
            MakeAllViews()
        End If



        If OptInit Then LoadOptions()


        o.ModuleName = "--Tables"
        o.Block = "--ForeignKey"
        o.OutNL("SET @@foreign_key_checks = 1;")
        o.OutNL("GO")




        Run = log
        'MTZUtilUtility_definst.DebugOutput("MYSQLGEN.Run:done")
        Exit Function
bye:
        Console.WriteLine("ERROR-" & Err.Description & "<--ERROR")
        'MsgBox Err.Description
        Resume
        'MTZUtilUtility_definst.DebugOutput("MYSQLGEN.Run:" & Err.Description)
        Run = log

    End Function

    Private Sub CreateTypeCopyProc(ByRef app As bp3doc.bp3doc.Application)
        Dim j As Integer
        Dim os As bp3doc.bp3doc.bp3doc_store
        Dim ot As bp3doc_def
        ot = app.bp3doc_def.Item(1)
        Dim s As Writer
        s = New Writer
        System.Windows.Forms.Application.DoEvents()
        s.putBufLC(procDropSQL(ot.Name & "_copy"))
        s.putBufLC(Delimiter())
        s.putBufLC("create procedure " & ot.Name & "_copy (")
        s.putBufLC(" acursession varchar(38),")
        s.putBufLC(" aInstanceID varchar(38), ")
        s.putBufLC(" aNewInstanceID varchar(38) ")
        s.putBufLC(")  body: begin  ")
        s.putBufLC("DECLARE aaction varchar(38);")

        s.putBufLC(" declare aEC int;")
        s.putBufLC(" select count(*) into aEC from the_session where the_sessionid=g2b(acursession) and closed=0 ;")
        s.putBufLC("if aEC=0  then")
        s.putBufLC("    select 'Сессия уже завершена.' result;")
        s.putBufLC("    leave body;")
        s.putBufLC(" end if;")


        s.putBufLC("select UUID() into aaction;")
        s.putBufLC("insert into CopyMapData(actionid,inpid,outid) values(g2b(aaction),g2b(aInstanceID),g2b(aNewInstanceID));")
        s.putBufLC("insert into instance(instanceid,name,objtype,status,archived)  select g2b(aNewInstanceID),name,objtype,status,archived from instance where instanceid=g2b(aInstanceID); ")



        For j = 1 To app.bp3doc_store.Count
            os = app.bp3doc_store.Item(j)
            If os.PartType = bp3doc.bp3doc.enumPartType.PartType_Rassirenie Then
                Continue For
            End If
            s.putBufLC("call " + os.Name + "_copy(acursession,aaction,aInstanceID);")
        Next

        s.putBufLC("delete from CopyMapData where actionid=g2b(aaction);")
        s.putBufLC("select 'OK' result;")
        s.putBufLC(" end ")

        '
        '
        o.ModuleName = "--Procs"
        o.Block = "--body"
        o.OutNL(s.getBuf)
        o.OutNL("GO")
        s = Nothing
    End Sub



    Private Sub CreateTypeGetToTempProc(ByRef app As bp3doc.bp3doc.Application)
        Dim j As Integer
        Dim os As bp3doc.bp3doc.bp3doc_store
        Dim ot As bp3doc.bp3doc.bp3doc_def
        ot = app.bp3doc_def.Item(1)
        Dim s As Writer
        s = New Writer
        System.Windows.Forms.Application.DoEvents()
        s.putBufLC(procDropSQL(ot.Name & "_GetToTemp"))
        s.putBufLC(Delimiter())
        s.putBufLC("create procedure " & ot.Name & "_GetToTemp (")
        s.putBufLC(" acursession varchar(38),")
        s.putBufLC(" aInstanceID varchar(38) ")
        s.putBufLC(")  body: begin  ")
        s.putBufLC("DECLARE aaction varchar(38);")

        s.putBufLC(" declare aEC int;")
        s.putBufLC(" select count(*) into aEC from the_session where the_sessionid=g2b(acursession) and closed=0 ;")
        s.putBufLC("if aEC=0  then")
        s.putBufLC("    select 'Сессия уже завершена.' result;")
        s.putBufLC("    leave body;")
        s.putBufLC(" end if;")




        For j = 1 To app.bp3doc_store.Count
            os = app.bp3doc_store.Item(j)
            If os.PartType = bp3doc.bp3doc.enumPartType.PartType_Rassirenie Then
                Continue For
            End If
            s.putBufLC("call " + os.Name + "_GetToTemp(acursession,aInstanceID);")
        Next


        s.putBufLC("select 'OK' result;")
        s.putBufLC(" end ")

        '
        '
        o.ModuleName = "--Procs"
        o.Block = "--body"
        o.OutNL(s.getBuf)
        o.OutNL("GO")
        s = Nothing
    End Sub




    Private Sub CreateTypeCommitFromTempProc(ByRef app As bp3doc.bp3doc.Application)
        Dim j As Integer
        Dim os As bp3doc.bp3doc.bp3doc_store
        Dim ot As bp3doc.bp3doc.bp3doc_def
        ot = app.bp3doc_def.Item(1)
        Dim s As Writer
        s = New Writer
        System.Windows.Forms.Application.DoEvents()
        s.putBufLC(procDropSQL(ot.Name & "_CommitFromTemp"))
        s.putBufLC(Delimiter())
        s.putBufLC("create procedure " & ot.Name & "_CommitFromTemp (")
        s.putBufLC(" acursession varchar(38),")
        s.putBufLC(" aInstanceID varchar(38) ")
        s.putBufLC(")  body: begin  ")
        s.putBufLC("DECLARE aaction varchar(38);")

        s.putBufLC(" declare aEC int;")
        s.putBufLC(" select count(*) into aEC from the_session where the_sessionid=g2b(acursession) and closed=0 ;")
        s.putBufLC("if aEC=0  then")
        s.putBufLC("    select 'Сессия уже завершена.' result;")
        s.putBufLC("    leave body;")
        s.putBufLC(" end if;")




        For j = 1 To app.bp3doc_store.Count
            os = app.bp3doc_store.Item(j)
            If os.PartType = bp3doc.bp3doc.enumPartType.PartType_Rassirenie Then
                Continue For
            End If
            s.putBufLC("call " + os.Name + "_CommitFromTemp(acursession,aInstanceID);")
        Next


        s.putBufLC("select 'OK' result;")
        s.putBufLC(" end ")

        '
        '
        o.ModuleName = "--Procs"
        o.Block = "--body"
        o.OutNL(s.getBuf)
        o.OutNL("GO")
        s = Nothing
    End Sub

    Private Sub CreateTypeDropTempDataProc(ByRef app As bp3doc.bp3doc.Application)
        Dim j As Integer
        Dim os As bp3doc.bp3doc.bp3doc_store
        Dim ot As bp3doc.bp3doc.bp3doc_def
        ot = app.bp3doc_def.Item(1)
        Dim s As Writer
        s = New Writer
        System.Windows.Forms.Application.DoEvents()
        s.putBufLC(procDropSQL(ot.Name & "_DropTempData"))
        s.putBufLC(Delimiter())
        s.putBufLC("create procedure " & ot.Name & "_DropTempData (")
        s.putBufLC(" acursession varchar(38),")
        s.putBufLC(" aInstanceID varchar(38) ")
        s.putBufLC(")  body: begin  ")
        s.putBufLC("DECLARE aaction varchar(38);")

        s.putBufLC(" declare aEC int;")
        s.putBufLC(" select count(*) into aEC from the_session where the_sessionid=g2b(acursession) and closed=0 ;")
        s.putBufLC("if aEC=0  then")
        s.putBufLC("    select 'Сессия уже завершена.' result;")
        s.putBufLC("    leave body;")
        s.putBufLC(" end if;")




        For j = 1 To app.bp3doc_store.Count
            os = app.bp3doc_store.Item(j)
            If os.PartType = bp3doc.bp3doc.enumPartType.PartType_Rassirenie Then
                Continue For
            End If
            s.putBufLC("call " + os.Name + "_DropTempData(acursession,aInstanceID);")
        Next


        s.putBufLC("select 'OK' result;")
        s.putBufLC(" end ")

        '
        '
        o.ModuleName = "--Procs"
        o.Block = "--body"
        o.OutNL(s.getBuf)
        o.OutNL("GO")
        s = Nothing
    End Sub


    Private Sub CreatePartCopyProc(ByRef os As bp3doc.bp3doc.bp3doc_store)
        Dim ft As bp3ft.bp3ft.bp3ft_def
        Dim fld As bp3doc.bp3doc.bp3doc_field
        Dim chos As bp3doc.bp3doc.bp3doc_store
        Dim i As Integer
        Dim s As Writer
        s = New Writer

        s.putBufLC(procDropSQL(os.Name & "_copy"))
        s.putBufLC(Delimiter())
        s.putBufLC("create procedure " & os.Name & "_copy (")
        s.putBufLC(" acursession varchar(38),")
        s.putBufLC(" acopyaction varchar(38),")
        s.putBufLC(" aInstanceID varchar(38) ")
        If TypeName(os.Parent.Parent) <> "Application" Then
            s.putBufLC(", aParentStructRowID varchar(38) ")
        End If


        s.putBufLC(")  body: begin  ")
        s.putBufLC(" declare aEC int;")

        If os.bp3doc_store.Count > 0 Then
            s.putBufLC("DECLARE aID binary(16);")
            s.putBufLC("DECLARE fetch_done INT DEFAULT FALSE;")

            s.putBufLC("declare copy_cursor_" + os.Name + " cursor for")

            s.putBufLC("select " + os.Name + "id from " + os.Name + " where ")
            If TypeName(os.Parent.Parent) = "Application" Then
                s.putBufLC("InstanceID =aInstanceID;")
            Else
                s.putBufLC("ParentStructRowID=aParentStructRowID;")
            End If

            s.putBufLC(" DECLARE CONTINUE HANDLER FOR NOT FOUND SET fetch_done = TRUE;")
        End If


        s.putBufLC(" select count(*) into aEC from the_session where the_sessionid=g2b(acursession) and closed=0 ;")
        s.putBufLC("if aEC=0  then")
        ' s.putBufLC("    select 'Сессия уже завершена.' result;")
        s.putBufLC("    leave body;")
        s.putBufLC(" end if;")


        s.putBufLC(" insert into " & os.Name & " ")

        s.putBufLC(" ( " & os.Name & "ID ")


        If TypeName(os.Parent.Parent) = "Application" Then
            s.putBufLC(",InstanceID")
        Else
            s.putBufLC(",ParentStructRowID")
        End If

        If os.PartType = 2 Then
            s.putBufLC(",ParentRowid")
        End If

        If os.UseArchiving = bp3doc.bp3doc.enumBoolean.Boolean_Da Then
            s.putBufLC(",archived")
        End If

        os.bp3doc_field.Sort = "sequence"
        For i = 1 To os.bp3doc_field.Count
            fld = os.bp3doc_field.Item(i)
            ft = fld.FieldType
            If Not ft Is Nothing Then
                If (ft.TypeStyle <> bp3doc.bp3doc.enumTypeStyle.TypeStyle_Element_oformleniy) Then
                    If ft.DelayedSave = bp3doc.bp3doc.enumBoolean.Boolean_Net Then
                        s.putBufLC("," & fld.Name & vbCrLf)


                        If UCase(ft.Name) = "FILE" Then
                            s.putBufLC("," & fld.Name & "_EXT")
                        End If
                    End If
                End If
            Else
                Console.WriteLine("Field Error " + fld.Name + " " + fld.Caption + " Table " + os.Name)
            End If

        Next

        s.putBufLC(" ) select ")

        s.putBufLC("CopyMap(acopyaction,b2g(" & os.Name & "ID)) ")



        If TypeName(os.Parent.Parent) = "Application" Then
            s.putBufLC(",CopyMap(acopyaction,b2g(InstanceID))")
        Else
            s.putBufLC(",CopyMap(acopyaction,b2g(ParentStructRowID))")
        End If

        If os.PartType = 2 Then
            s.putBufLC(",CopyMap(acopyaction,b2g(ParentRowid))")
        End If

        If os.UseArchiving = bp3doc.bp3doc.enumBoolean.Boolean_Da Then
            s.putBufLC(",archived")
        End If

        os.bp3doc_field.Sort = "sequence"
        For i = 1 To os.bp3doc_field.Count
            fld = os.bp3doc_field.Item(i)
            ft = fld.fieldType
            If Not ft Is Nothing Then
                If (ft.TypeStyle <> bp3doc.bp3doc.enumTypeStyle.TypeStyle_Element_oformleniy) Then
                    If ft.DelayedSave = bp3doc.bp3doc.enumBoolean.Boolean_Net Then
                        s.putBufLC("," & fld.Name & vbCrLf)


                        If UCase(ft.Name) = "FILE" Then
                            s.putBufLC("," & fld.Name & "_EXT")
                        End If
                    End If
                End If
            End If

        Next

        s.putBufLC(" from " & os.Name)
        If TypeName(os.Parent.Parent) = "Application" Then
            s.putBufLC(" where instanceid =g2b( aInstanceID); ")
        Else
            s.putBufLC(" where ParentStructRowID =g2b(aParentStructRowID); ")
        End If


        If os.bp3doc_store.Count > 0 Then
            s.putBufLC("open copy_cursor_" + os.Name + ";")
            s.putBufLC("  set  fetch_done=false;")
            s.putBufLC("fetch copy_cursor_" + os.Name + " into aId;")
            s.putBufLC("while not fetch_done DO ")
            For i = 1 To os.bp3doc_store.Count
                chos = os.bp3doc_store.Item(i)
                CreatePartCopyProc(chos)
                s.putBufLC("    call " + chos.Name + "_copy(acursession,acopyaction,aInstanceID,aID);")
            Next
            s.putBufLC("  set  fetch_done=false;")
            s.putBufLC("    fetch copy_cursor_" + os.Name + " into aId;")
            s.putBufLC("End while;")
            s.putBufLC("")
            s.putBufLC("Close copy_cursor_" + os.Name + ";")
        End If



        's.putBufLC("-- select 'OK' result;")
        s.putBufLC(" end ")
        s.putBufLC("GO")
        '
        '
        o.ModuleName = "--Procedures"
        o.Block = "--TableProc"
        o.OutNL(s.getBuf)
        o.OutNL("GO")
        s = Nothing

    End Sub


    Private Sub CreatePartGetToTempProc(ByRef os As bp3doc.bp3doc.bp3doc_store)
        Dim ft As bp3ft.bp3ft.bp3ft_def
        Dim fld As bp3doc.bp3doc.bp3doc_field
        Dim chos As bp3doc.bp3doc.bp3doc_store
        Dim i As Integer
        Dim s As Writer
        s = New Writer

        s.putBufLC(procDropSQL(os.Name & "_GetToTemp"))
        s.putBufLC(Delimiter())
        s.putBufLC("create procedure " & os.Name & "_GetToTemp (")
        s.putBufLC(" acursession varchar(38),")
        s.putBufLC(" aInstanceID varchar(38) ")
        If TypeName(os.Parent.Parent) <> "Application" Then
            s.putBufLC(", aParentStructRowID varchar(38) ")
        End If


        s.putBufLC(")  body: begin  ")
        s.putBufLC(" declare aEC int;")

        If os.bp3doc_store.Count > 0 Then
            s.putBufLC("DECLARE aID binary(16);")
            s.putBufLC("DECLARE fetch_done INT DEFAULT FALSE;")

            s.putBufLC("declare GetToTemp_cursor_" + os.Name + " cursor for")

            s.putBufLC("select " + os.Name + "id from " + os.Name + " where ")
            If TypeName(os.Parent.Parent) = "Application" Then
                s.putBufLC("InstanceID =aInstanceID;")
            Else
                s.putBufLC("ParentStructRowID=aParentStructRowID;")
            End If

            s.putBufLC(" DECLARE CONTINUE HANDLER FOR NOT FOUND SET fetch_done = TRUE;")
        End If


        s.putBufLC(" select count(*) into aEC from the_session where the_sessionid=g2b(acursession) and closed=0 ;")
        s.putBufLC("if aEC=0  then")
        ' s.putBufLC("    select 'Сессия уже завершена.' result;")
        s.putBufLC("    leave body;")
        s.putBufLC(" end if;")

        s.putBufLC("delete from " & os.Name & "_temp")
        If TypeName(os.Parent.Parent) = "Application" Then
            s.putBufLC(" where  sessionid=g2b(acursession) and instanceid =g2b(aInstanceID); ")
        Else
            s.putBufLC(" where  sessionid=g2b(acursession) and ParentStructRowID =g2b(aParentStructRowID); ")
        End If


        s.putBufLC(" insert into " & os.Name & "_temp ")

        s.putBufLC(" (timestamp,changestamp,sessionID, " & os.Name & "ID ")


        If TypeName(os.Parent.Parent) = "Application" Then
            s.putBufLC(",InstanceID")
        Else
            s.putBufLC(",ParentStructRowID")
        End If

        If os.PartType = 2 Then
            s.putBufLC(",ParentRowid")
        End If

        If os.UseArchiving = bp3doc.bp3doc.enumBoolean.Boolean_Da Then
            s.putBufLC(",archived")
        End If

        os.bp3doc_field.Sort = "sequence"
        For i = 1 To os.bp3doc_field.Count
            fld = os.bp3doc_field.Item(i)
            ft = fld.fieldType
            If (ft.TypeStyle <> bp3doc.bp3doc.enumTypeStyle.TypeStyle_Element_oformleniy) Then
                If ft.DelayedSave = bp3doc.bp3doc.enumBoolean.Boolean_Net Then
                    s.putBufLC("," & fld.Name & vbCrLf)


                    If UCase(ft.Name) = "FILE" Then
                        s.putBufLC("," & fld.Name & "_EXT")
                    End If
                End If
            End If
        Next

        s.putBufLC(" ) select ")

        s.putBufLC("timestamp,changestamp,g2b(acursession)," & os.Name & "ID ")



        If TypeName(os.Parent.Parent) = "Application" Then
            s.putBufLC(",InstanceID")
        Else
            s.putBufLC(",ParentStructRowID")
        End If

        If os.PartType = 2 Then
            s.putBufLC(",ParentRowid")
        End If

        If os.UseArchiving = bp3doc.bp3doc.enumBoolean.Boolean_Da Then
            s.putBufLC(",archived")
        End If

        os.bp3doc_field.Sort = "sequence"
        For i = 1 To os.bp3doc_field.Count
            fld = os.bp3doc_field.Item(i)
            ft = fld.fieldType

            If (ft.TypeStyle <> bp3doc.bp3doc.enumTypeStyle.TypeStyle_Element_oformleniy) Then
                If ft.DelayedSave = bp3doc.bp3doc.enumBoolean.Boolean_Net Then
                    s.putBufLC("," & fld.Name & vbCrLf)


                    If UCase(ft.Name) = "FILE" Then
                        s.putBufLC("," & fld.Name & "_EXT")
                    End If
                End If
            End If
        Next

        s.putBufLC(" from " & os.Name)
        If TypeName(os.Parent.Parent) = "Application" Then
            s.putBufLC(" where instanceid =g2b(aInstanceID); ")
        Else
            s.putBufLC(" where ParentStructRowID =g2b(aParentStructRowID); ")
        End If


        If os.bp3doc_store.Count > 0 Then
            s.putBufLC("open GetToTemp_cursor_" + os.Name + ";")
            s.putBufLC("  set  fetch_done=false;")
            s.putBufLC("fetch GetToTemp_cursor_" + os.Name + " into aId;")
            s.putBufLC("while not fetch_done DO ")
            For i = 1 To os.bp3doc_store.Count
                chos = os.bp3doc_store.Item(i)
                If chos.PartType <> bp3doc.bp3doc.enumPartType.PartType_Rassirenie Then
                    CreatePartGetToTempProc(chos)
                    s.putBufLC("    call " + chos.Name + "_GetToTemp(acursession,aInstanceID,aID);")
                End If

            Next
            s.putBufLC("  set  fetch_done=false;")
            s.putBufLC("    fetch GetToTemp_cursor_" + os.Name + " into aId;")
            s.putBufLC("End while;")
            s.putBufLC("")
            s.putBufLC("Close GetToTemp_cursor_" + os.Name + ";")
        End If



        's.putBufLC("-- select 'OK' result;")
        s.putBufLC(" end ")
        s.putBufLC("GO")
        '
        '
        o.ModuleName = "--Procedures"
        o.Block = "--TableProc"
        o.OutNL(s.getBuf)
        o.OutNL("GO")
        s = Nothing

    End Sub



    Private Sub CreatePartDropTempDataProc(ByRef os As bp3doc.bp3doc.bp3doc_store)
        Dim chos As bp3doc.bp3doc.bp3doc_store
        Dim i As Integer
        Dim s As Writer
        s = New Writer

        s.putBufLC(procDropSQL(os.Name & "_DropTempData"))
        s.putBufLC(Delimiter())
        s.putBufLC("create procedure " & os.Name & "_DropTempData (")
        s.putBufLC(" acursession varchar(38),")
        s.putBufLC(" aInstanceID varchar(38) ")
        If TypeName(os.Parent.Parent) <> "Application" Then
            s.putBufLC(", aParentStructRowID varchar(38) ")
        End If


        s.putBufLC(")  body: begin  ")
        s.putBufLC(" declare aEC int;")

        If os.bp3doc_store.Count > 0 Then
            s.putBufLC("DECLARE aID binary(16);")
            s.putBufLC("DECLARE fetch_done INT DEFAULT FALSE;")

            s.putBufLC("declare DropTempData_cursor_" + os.Name + " cursor for")

            s.putBufLC("select " + os.Name + "id from " + os.Name + "_temp where sessionid=g2b(acursession) and ")
            If TypeName(os.Parent.Parent) = "Application" Then
                s.putBufLC(" InstanceID =aInstanceID;")
            Else
                s.putBufLC(" ParentStructRowID=aParentStructRowID;")
            End If

            s.putBufLC(" DECLARE CONTINUE HANDLER FOR NOT FOUND SET fetch_done = TRUE;")
        End If


        s.putBufLC(" select count(*) into aEC from the_session where the_sessionid=g2b(acursession) and closed=0 ;")
        s.putBufLC("if aEC=0  then")
        ' s.putBufLC("    select 'Сессия уже завершена.' result;")
        s.putBufLC("    leave body;")
        s.putBufLC(" end if;")


        If os.bp3doc_store.Count > 0 Then
            s.putBufLC("open DropTempData_cursor_" + os.Name + ";")
            s.putBufLC("  set  fetch_done=false;")
            s.putBufLC("fetch DropTempData_cursor_" + os.Name + " into aId;")
            s.putBufLC("while not fetch_done DO ")
            For i = 1 To os.bp3doc_store.Count
                chos = os.bp3doc_store.Item(i)
                If chos.PartType <> bp3doc.bp3doc.enumPartType.PartType_Rassirenie Then
                    CreatePartDropTempDataProc(chos)
                    s.putBufLC("    call " + chos.Name + "_DropTempData(acursession,aInstanceID,aID);")
                End If
            Next
            s.putBufLC("  set  fetch_done=false;")
            s.putBufLC("    fetch DropTempData_cursor_" + os.Name + " into aId;")
            s.putBufLC("End while;")
            s.putBufLC("")
            s.putBufLC("Close DropTempData_cursor_" + os.Name + ";")
        End If

        s.putBufLC("delete from " & os.Name & "_temp")
        If TypeName(os.Parent.Parent) = "Application" Then
            s.putBufLC(" where  sessionid=g2b(acursession) and instanceid =g2b(aInstanceID); ")
        Else
            s.putBufLC(" where  sessionid=g2b(acursession) and ParentStructRowID =g2b(aParentStructRowID); ")
        End If


        's.putBufLC("-- select 'OK' result;")
        s.putBufLC(" end ")
        s.putBufLC("GO")
        '
        '
        o.ModuleName = "--Procedures"
        o.Block = "--TableProc"
        o.OutNL(s.getBuf)
        o.OutNL("GO")
        s = Nothing

    End Sub



    Private Sub CreatePartCommitFromTempProc(ByRef os As bp3doc.bp3doc.bp3doc_store)
        Dim ft As bp3ft.bp3ft.bp3ft_def
        Dim fld As bp3doc.bp3doc.bp3doc_field
        Dim chos As bp3doc.bp3doc.bp3doc_store
        Dim i As Integer
        Dim s As Writer
        s = New Writer

        s.putBufLC(procDropSQL(os.Name & "_CommitFromTemp"))
        s.putBufLC(Delimiter())
        s.putBufLC("create procedure " & os.Name & "_CommitFromTemp (")
        s.putBufLC(" acursession varchar(38),")
        s.putBufLC(" aInstanceID varchar(38) ")
        If TypeName(os.Parent.Parent) <> "Application" Then
            s.putBufLC(", aParentStructRowID varchar(38) ")
        End If


        s.putBufLC(")  body: begin  ")
        s.putBufLC(" declare aEC int;")

        If os.bp3doc_store.Count > 0 Then
            s.putBufLC("DECLARE aID binary(16);")
            s.putBufLC("DECLARE fetch_done INT DEFAULT FALSE;")

            s.putBufLC("declare CommitFromTemp_cursor_" + os.Name + " cursor for")

            s.putBufLC("select " + os.Name + "id from " + os.Name + "_temp where sessionid=g2b(acursession) and ")
            If TypeName(os.Parent.Parent) = "Application" Then
                s.putBufLC(" InstanceID =aInstanceID;")
            Else
                s.putBufLC(" ParentStructRowID=aParentStructRowID;")
            End If

            s.putBufLC(" DECLARE CONTINUE HANDLER FOR NOT FOUND SET fetch_done = TRUE;")
        End If


        s.putBufLC(" select count(*) into aEC from the_session where the_sessionid=g2b(acursession) and closed=0 ;")
        s.putBufLC("if aEC=0  then")
        ' s.putBufLC("    select 'Сессия уже завершена.' result;")
        s.putBufLC("    leave body;")
        s.putBufLC(" end if;")


        s.putBufLC("delete from " & os.Name)
        If TypeName(os.Parent.Parent) = "Application" Then
            s.putBufLC(" where   instanceid =g2b(aInstanceID); ")
        Else
            s.putBufLC(" where   ParentStructRowID =g2b(aParentStructRowID); ")
        End If


        s.putBufLC(" insert into " & os.Name & " ")

        s.putBufLC(" (timestamp, changestamp," & os.Name & "ID ")


        If TypeName(os.Parent.Parent) = "Application" Then
            s.putBufLC(",InstanceID")
        Else
            s.putBufLC(",ParentStructRowID")
        End If

        If os.PartType = 2 Then
            s.putBufLC(",ParentRowid")
        End If

        If os.UseArchiving = bp3doc.bp3doc.enumBoolean.Boolean_Da Then
            s.putBufLC(",archived")
        End If

        os.bp3doc_field.Sort = "sequence"
        For i = 1 To os.bp3doc_field.Count
            fld = os.bp3doc_field.Item(i)
            ft = fld.fieldType
            If (ft.TypeStyle <> bp3doc.bp3doc.enumTypeStyle.TypeStyle_Element_oformleniy) Then
                If ft.DelayedSave = bp3doc.bp3doc.enumBoolean.Boolean_Net Then
                    s.putBufLC("," & fld.Name & vbCrLf)


                    If UCase(ft.Name) = "FILE" Then
                        s.putBufLC("," & fld.Name & "_EXT")
                    End If
                End If
            End If
        Next

        s.putBufLC(" ) select ")

        s.putBufLC("timestamp, changestamp," & os.Name & "ID ")



        If TypeName(os.Parent.Parent) = "Application" Then
            s.putBufLC(",InstanceID")
        Else
            s.putBufLC(",ParentStructRowID")
        End If

        If os.PartType = 2 Then
            s.putBufLC(",ParentRowid")
        End If

        If os.UseArchiving = bp3doc.bp3doc.enumBoolean.Boolean_Da Then
            s.putBufLC(",archived")
        End If

        os.bp3doc_field.Sort = "sequence"
        For i = 1 To os.bp3doc_field.Count
            fld = os.bp3doc_field.Item(i)
            ft = fld.FieldType

            If (ft.TypeStyle <> bp3doc.bp3doc.enumTypeStyle.TypeStyle_Element_oformleniy) Then
                If ft.DelayedSave = bp3doc.bp3doc.enumBoolean.Boolean_Net Then
                    s.putBufLC("," & fld.Name & vbCrLf)


                    If UCase(ft.Name) = "FILE" Then
                        s.putBufLC("," & fld.Name & "_EXT")
                    End If
                End If
            End If
        Next

        s.putBufLC(" from " & os.Name & "_temp")
        If TypeName(os.Parent.Parent) = "Application" Then
            s.putBufLC(" where  sessionid=g2b(acursession) and instanceid =g2b(aInstanceID); ")
        Else
            s.putBufLC(" where  sessionid=g2b(acursession) and ParentStructRowID =g2b(aParentStructRowID); ")
        End If


        If os.bp3doc_store.Count > 0 Then
            s.putBufLC("open CommitFromTemp_cursor_" + os.Name + ";")
            s.putBufLC("  set  fetch_done=false;")
            s.putBufLC("fetch CommitFromTemp_cursor_" + os.Name + " into aId;")
            s.putBufLC("while not fetch_done DO ")
            For i = 1 To os.bp3doc_store.Count
                chos = os.bp3doc_store.Item(i)
                If chos.PartType <> bp3doc.bp3doc.enumPartType.PartType_Rassirenie Then
                    CreatePartCommitFromTempProc(chos)
                    s.putBufLC("    call " + chos.Name + "_CommitFromTemp(acursession,aInstanceID,aID);")
                End If
            Next
            s.putBufLC("  set  fetch_done=false;")
            s.putBufLC("    fetch CommitFromTemp_cursor_" + os.Name + " into aId;")
            s.putBufLC("End while;")
            s.putBufLC("")
            s.putBufLC("Close CommitFromTemp_cursor_" + os.Name + ";")
        End If




        's.putBufLC("-- select 'OK' result;")
        s.putBufLC(" end ")
        s.putBufLC("GO")
        '
        '
        o.ModuleName = "--Procedures"
        o.Block = "--TableProc"
        o.OutNL(s.getBuf)
        o.OutNL("GO")
        s = Nothing

    End Sub


    Private Sub CreateStruct(ByRef os As bp3doc.bp3doc.bp3doc_store)
        'MTZUtilUtility_definst.DebugOutput("MYSQLGEN.CreateStruct:start " & os.Caption)
        '''' If os.PartType = 3 Then
        If os.PartType = bp3doc.bp3doc.enumPartType.PartType_Rassirenie Then
            'MTZUtilUtility_definst.DebugOutput("MYSQLGEN.CreateStruct:skipped " & os.Caption)
            Exit Sub
        End If
        Dim st As bp3doc.bp3doc.bp3doc_store
        st = os
        System.Windows.Forms.Application.DoEvents()
        Dim chos As bp3doc.bp3doc.bp3doc_store
        Dim i As Short
        Dim s As Writer
        s = New Writer
        Dim collist As String

        'Строка
        'Колекция
        'Дерево
        'Граф
        'CheckPartMLF(st, log)

        Console.WriteLine("-CreateStruct " & os.Name)


        On Error GoTo bye
        s.putBufLC("/*" & os.Caption & "*/")


        s.putBufLC("create table IF NOT EXISTS " & os.Name & "/*" & os.the_Comment & "*/ (")
        collist = ""

        If TypeName(os.Parent.Parent) = "Application" Then
            s.putBufLC("InstanceID BINARY(16) ,")
            collist = collist & "'InstanceID'"
        Else
            s.putBufLC("ParentStructRowID BINARY(16) not null,")
            collist = collist & "'ParentStructRowID'"
        End If

        s.putBufLC(os.Name & "id BINARY(16) not null  ")


        collist = collist & ",'" & os.Name & "ID'"

        s.putBufLC(",ChangeStamp datetime not null  /* Время последнего изменения */")
        collist = collist & ",'ChangeStamp'"

        s.putBufLC(",TimeStamp timestamp not null  /* для организации инкрементального индексирования полнотекстовой информации */")
        collist = collist & ",'TimeStamp'"


        s.putBufLC(",LockSessionID BINARY(16) null  /* temporary lock */")
        collist = collist & ",'LockSessionID'"
        s.putBufLC(",LockUserID BINARY(16) null /* checkout lock */")
        collist = collist & ",'LockUserID'"
        s.putBufLC(",SecurityStyleID BINARY(16) null /* security formula */")
        collist = collist & ",'SecurityStyleID'"

        ' дерево
        If os.PartType = 2 Then


            s.putBufLC(",ParentRowid BINARY(16) ")


            collist = collist & ",'ParentRowid'"
        End If

        s.putBufLC(")")
        s.putBufLC("GO")


        If os.UseArchiving = bp3doc.bp3doc.enumBoolean.Boolean_Da Then

            s.putBufLC(procDropSQL("alter_col"))
            s.putBufLC("create procedure alter_col() begin")
            s.putBufLC("        IF not EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE")
            s.putBufLC("           TABLE_SCHEMA = DATABASE() AND")
            s.putBufLC("           TABLE_NAME   = '" + os.Name + "' AND ")
            s.putBufLC("           column_name   = 'archived') THEN")
            s.putBufLC("                alter table " & os.Name & " add ")
            s.putBufLC("                archived int null default 0;")
            s.putBufLC("        End If;")
            s.putBufLC("End ")
            s.putBufLC("GO")
            s.putBufLC("call alter_col(); ")
            s.putBufLC("GO")
            s.putBufLC(procDropSQL("alter_col"))
            collist = collist & ",'archived'"
        End If


        If (OptRights) Then
            's.putBufLC("revoke all on " & os.Name & " to public")
            's.putBufLC("GO")
            's.putBufLC("grant select on " & os.Name & " to public")
            's.putBufLC("GO")
        End If
        Dim ft As bp3ft.bp3ft.bp3ft_def

        st.bp3doc_field.Sort = "sequence"
        For i = 1 To st.bp3doc_field.Count
            ft = st.bp3doc_field.Item(i).FieldType


            If (ft.TypeStyle <> bp3doc.bp3doc.enumTypeStyle.TypeStyle_Element_oformleniy) Then
                'If i > 1 Then

                's.putbuf ","

                'support extention .bp3doc_field if file type used

                If UCase(ft.Name) = "FILE" Then



                    s.putBufLC(procDropSQL("alter_col"))
                    s.putBufLC("create procedure alter_col() begin")
                    s.putBufLC("        IF not EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE")
                    s.putBufLC("           TABLE_SCHEMA = DATABASE() AND")
                    s.putBufLC("           TABLE_NAME   = '" + st.Name + "' AND ")
                    s.putBufLC("           column_name   = '" + st.bp3doc_field.Item(i).Name & "_EXT" + "') THEN")
                    s.putBufLC("        alter table " & st.Name & " add ")
                    s.putBufLC("            " & st.bp3doc_field.Item(i).Name & "_EXT nvarchar(4) null;")
                    s.putBufLC("        End If;")
                    s.putBufLC("End ")
                    s.putBufLC("GO")
                    s.putBufLC("call alter_col(); ")
                    s.putBufLC("GO")
                    s.putBufLC(procDropSQL("alter_col"))
                    collist = collist & ",'" & st.bp3doc_field.Item(i).Name & "_EXT'"


                End If



                s.putBufLC(procDropSQL("alter_col"))
                s.putBufLC("create procedure alter_col() begin")
                s.putBufLC("        IF not EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE")
                s.putBufLC("           TABLE_SCHEMA = DATABASE() AND")
                s.putBufLC("           TABLE_NAME   = '" + st.Name + "' AND ")
                s.putBufLC("           column_name   = '" + st.bp3doc_field.Item(i).Name + "') THEN")
                s.putBufLC("                alter table " & st.Name & " add ")
                s.putBufLC("                " + FieldForCreate(st.bp3doc_field.Item(i)) + ";")
                s.putBufLC("        End If;")
                s.putBufLC("End ")
                s.putBufLC("GO")
                s.putBufLC("call alter_col(); ")
                s.putBufLC("GO")
                s.putBufLC(procDropSQL("alter_col"))

                collist = collist & ",'" & st.bp3doc_field.Item(i).Name & "'"

            End If
        Next

        s.putBufLC(ColumnDropSQL((st.Name), collist))

        o.ModuleName = "--Tables"
        o.Block = "--body"
        o.OutNL(s.getBuf)

        s = Nothing
        s = New Writer

        s.putBufLC(procDropSQL("alter_pk"))
        s.putBufLC("create procedure alter_pk() begin")
        s.putBufLC("    IF not EXISTS (SELECT 1 FROM information_schema.TABLE_CONSTRAINTS WHERE")
        s.putBufLC("           CONSTRAINT_SCHEMA = DATABASE() AND ")
        s.putBufLC("           TABLE_NAME   = '" + os.Name + "' AND ")
        s.putBufLC("           CONSTRAINT_TYPE   = 'PRIMARY KEY') THEN")
        s.putBufLC("            alter table " & os.Name & " add constraint pk_" & os.Name & " primary key (" & os.Name & "ID);")
        s.putBufLC("    End If;")
        s.putBufLC("End ")
        s.putBufLC("GO")
        s.putBufLC("call alter_pk(); ")
        s.putBufLC("GO")
        s.putBufLC(procDropSQL("alter_pk"))


        o.ModuleName = "--Tables"
        o.Block = "--body"
        o.OutNL(s.getBuf)
        o.OutNL("GO")



        If TypeName(os.Parent.Parent) <> "Application" Then

            s = Nothing
            s = New Writer

            s.putBufLC(procDropSQL("alter_idx"))
            s.putBufLC("create procedure alter_idx() begin")
            s.putBufLC("if not exists (SELECT 1 ")
            s.putBufLC("  FROM(INFORMATION_SCHEMA.STATISTICS)")
            s.putBufLC("  WHERE(table_schema = DATABASE()) ")
            s.putBufLC("  AND   table_name   = '" + os.Name + "' ")
            s.putBufLC("  AND   index_name   = 'parent_" & os.Name + "' ) THEN")
            s.putBufLC("create index parent_" & os.Name & " on " & os.Name & "(ParentStructRowID);")
            s.putBufLC("End If;")
            s.putBufLC("End ")
            s.putBufLC("GO")
            s.putBufLC("call alter_idx(); ")
            s.putBufLC("GO")
            s.putBufLC(procDropSQL("alter_idx"))


            o.ModuleName = "--Tables"
            o.Block = "--Index"
            o.OutNL(s.getBuf)
            o.OutNL("GO")

            s = Nothing
            s = New Writer



            s.putBufLC(procDropSQL("alter_fk"))
            s.putBufLC("create procedure alter_fk() begin")
            s.putBufLC("IF not EXISTS (SELECT 1 FROM information_schema.TABLE_CONSTRAINTS WHERE")
            s.putBufLC("           CONSTRAINT_SCHEMA = DATABASE() AND")
            s.putBufLC("           TABLE_NAME   = '" & os.Name & "' AND ")
            s.putBufLC("           CONSTRAINT_TYPE   = 'FOREIGN KEY') THEN")
            s.putBufLC("        alter table " & os.Name & " add constraint fk_" & MakeName(os.ID.ToString()) & " foreign key(ParentStructRowID) references " & CType(os.Parent.Parent, bp3doc_store).Name & " (" & CType(os.Parent.Parent, bp3doc_store).Name & "ID)   ON DELETE CASCADE;")
            s.putBufLC("End If;")
            s.putBufLC("End ")
            s.putBufLC("GO")
            s.putBufLC("call alter_fk(); ")
            s.putBufLC("GO")
            s.putBufLC(procDropSQL("alter_fk"))


            o.ModuleName = "--Tables"
            o.Block = "--ForeignKey"
            o.OutNL(s.getBuf)
            o.OutNL("GO")



        Else

            s = Nothing
            s = New Writer
            s.putBufLC(procDropSQL("alter_idx"))
            s.putBufLC("create procedure alter_idx() begin")
            s.putBufLC("if not exists (SELECT 1 ")
            s.putBufLC("  FROM(INFORMATION_SCHEMA.STATISTICS)")
            s.putBufLC("  WHERE(table_schema = DATABASE()) ")
            s.putBufLC("  AND   table_name   = '" + os.Name + "' ")
            s.putBufLC("  AND   index_name   = 'parent_" & os.Name + "' ) THEN")
            s.putBufLC("   create index parent_" & os.Name & " on " & os.Name & "(INSTANCEID);")
            s.putBufLC("End If;")
            s.putBufLC("End ")
            s.putBufLC("GO")
            s.putBufLC("call alter_idx(); ")
            s.putBufLC("GO")
            s.putBufLC(procDropSQL("alter_idx"))

            o.ModuleName = "--Tables"
            o.Block = "--Index"
            o.OutNL(s.getBuf)
            o.OutNL("GO")

            s = Nothing
            s = New Writer
            s.putBufLC(procDropSQL("alter_fk"))
            s.putBufLC("create procedure alter_fk() begin")
            s.putBufLC("IF not EXISTS (SELECT 1 FROM information_schema.TABLE_CONSTRAINTS WHERE")
            s.putBufLC("           CONSTRAINT_SCHEMA = DATABASE() AND")
            s.putBufLC("           TABLE_NAME   = '" & os.Name & "' AND ")
            s.putBufLC("           CONSTRAINT_TYPE   = 'FOREIGN KEY') THEN")
            s.putBufLC("alter table " & os.Name & " add constraint fk_" & MakeName(os.ID.ToString()) & " foreign key(INSTANCEID) references INSTANCE (INSTANCEID) ON DELETE CASCADE;")
            s.putBufLC("End If;")
            s.putBufLC("End ")
            s.putBufLC("GO")
            s.putBufLC("call alter_fk(); ")
            s.putBufLC("GO")
            s.putBufLC(procDropSQL("alter_fk"))

            o.ModuleName = "--Tables"
            o.Block = "--ForeignKey"
            o.OutNL(s.getBuf)
            o.OutNL("GO")



        End If





        For i = 1 To os.bp3doc_store.Count
            chos = os.bp3doc_store.Item(i)
            CreateStruct(chos)
        Next

        s = Nothing

        Exit Sub
bye:
        Console.WriteLine("ERROR-" & Err.Description & "<--ERROR")
        s = Nothing

    End Sub

    Private Sub CreateStructLog(ByRef os As bp3doc.bp3doc.bp3doc_store)


        Dim logName As String
        logName = os.Name & "_LOG"


        If os.PartType = bp3doc.bp3doc.enumPartType.PartType_Rassirenie Then

            Exit Sub
        End If

        Dim st As bp3doc.bp3doc.bp3doc_store
        st = os
        System.Windows.Forms.Application.DoEvents()
        Dim chos As bp3doc.bp3doc.bp3doc_store
        Dim i As Short
        Dim s As Writer
        s = New Writer
        Dim collist As String

        Console.WriteLine("-CreateStruct " & logName)

        On Error GoTo bye
        s.putBufLC("/*" & os.Caption & "*/")


        s.putBufLC("create table IF NOT EXISTS " & logName & "/*" & os.the_Comment & "*/ (")
        collist = ""


        If TypeName(os.Parent.Parent) = "Application" Then
            s.putBufLC("InstanceID BINARY(16) ,")
            collist = collist & "'InstanceID'"
        Else
            s.putBufLC("ParentStructRowID BINARY(16) not null,")
            collist = collist & "'ParentStructRowID'"
        End If


        s.putBufLC(logName & "id BINARY(16) not null  ")



        collist = collist & ",'" & logName & "ID'"

        s.putBufLC(",ChangeStamp datetime not null /* Время последнего изменения */")
        collist = collist & ",'ChangeStamp'"

        s.putBufLC(",TimeStamp timestamp not null  /* для организации инкрементального индексирования полнотекстовой информации */")
        collist = collist & ",'TimeStamp'"


        s.putBufLC(",LockSessionID BINARY(16) null  /* temporary lock */")
        collist = collist & ",'LockSessionID'"
        s.putBufLC(",LockUserID BINARY(16) null /* checkout lock */")
        collist = collist & ",'LockUserID'"
        s.putBufLC(",SecurityStyleID BINARY(16) null /* security formula */")
        collist = collist & ",'SecurityStyleID'"


        s.putBufLC(",DateTimeLog datetime not null  /* time when this record changet to new one */")
        collist = collist & ",'DateTimeLog'"
        s.putBufLC(",UserLog BINARY(16) null /* Session id for modifier */")
        collist = collist & ",'UserLog'"

        ' дерево
        If os.PartType = 2 Then

            s.putBufLC(",ParentRowid BINARY(16) ")


            collist = collist & ",'ParentRowid'"
        End If

        s.putBufLC(")")
        s.putBufLC("GO")


        If os.UseArchiving = bp3doc.bp3doc.enumBoolean.Boolean_Da Then
            s.putBufLC(procDropSQL("alter_col"))
            s.putBufLC("create procedure alter_col() begin")
            s.putBufLC("        IF not EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE")
            s.putBufLC("           TABLE_SCHEMA = DATABASE() AND")
            s.putBufLC("           TABLE_NAME   = '" + logName + "' AND ")
            s.putBufLC("           column_name   = 'archived') THEN")
            s.putBufLC("                alter table " & logName & " add ")
            s.putBufLC("                archived int null default 0;")
            s.putBufLC("        End If;")
            s.putBufLC("End ")
            s.putBufLC("GO")
            s.putBufLC("call alter_col(); ")
            s.putBufLC("GO")
            s.putBufLC(procDropSQL("alter_col"))
            collist = collist & ",'archived'"
        End If

        If (OptRights) Then
            's.putBufLC("revoke all on " & logName & " to public")
            's.putBufLC("GO")
            's.putBufLC("grant select on " & logName & " to public")
            's.putBufLC("GO")
        End If

        Dim ft As bp3ft.bp3ft.bp3ft_def
        st.bp3doc_field.Sort = "sequence"
        For i = 1 To st.bp3doc_field.Count
            ft = st.bp3doc_field.Item(i).FieldType


            If (ft.TypeStyle <> bp3doc.bp3doc.enumTypeStyle.TypeStyle_Element_oformleniy) Then

                s.putBufLC(procDropSQL("alter_col"))
                s.putBufLC("create procedure alter_col() begin")
                s.putBufLC("        IF not EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE")
                s.putBufLC("           TABLE_SCHEMA = DATABASE() AND")
                s.putBufLC("           TABLE_NAME   = '" + logName + "' AND ")
                s.putBufLC("           column_name   = '" + st.bp3doc_field.Item(i).Name + "') THEN")
                s.putBufLC("                alter table " & logName & " add ")
                s.putBufLC("                " + FieldForCreate(st.bp3doc_field.Item(i)) + ";")
                s.putBufLC("        End If;")
                s.putBufLC("End ")
                s.putBufLC("GO")
                s.putBufLC("call alter_col(); ")
                s.putBufLC("GO")
                s.putBufLC(procDropSQL("alter_col"))

                If UCase(ft.Name) = "FILE" Then

                    s.putBufLC(procDropSQL("alter_col"))
                    s.putBufLC("create procedure alter_col() begin")
                    s.putBufLC("        IF not EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE")
                    s.putBufLC("           TABLE_SCHEMA = DATABASE() AND")
                    s.putBufLC("           TABLE_NAME   = '" + logName + "' AND ")
                    s.putBufLC("           column_name   = '" + st.bp3doc_field.Item(i).Name + "_ext" + "') THEN")
                    s.putBufLC("            alter table " & logName & " add ")
                    s.putBufLC("            " & st.bp3doc_field.Item(i).Name & "_EXT nvarchar(4) null;")
                    s.putBufLC("        End If;")
                    s.putBufLC("End ")
                    s.putBufLC("GO")
                    s.putBufLC("call alter_col(); ")
                    s.putBufLC("GO")
                    s.putBufLC(procDropSQL("alter_col"))


                    collist = collist & ",'" & st.bp3doc_field.Item(i).Name & "_EXT'"
                End If

                collist = collist & ",'" & st.bp3doc_field.Item(i).Name & "'"
                s.putBufLC("GO")
            End If
        Next

        s.putBufLC(ColumnDropSQL(logName, collist))

        o.ModuleName = "--Tables"
        o.Block = "--body"
        o.OutNL(s.getBuf)



        For i = 1 To os.bp3doc_store.Count
            chos = os.bp3doc_store.Item(i)
            CreateStructLog(chos)
        Next

        s = Nothing

        Exit Sub
bye:
        Console.WriteLine("ERROR-" & Err.Description & "<--ERROR")
        s = Nothing

    End Sub



    Private Sub CreateStructTemp(ByRef os As bp3doc.bp3doc.bp3doc_store)


        Dim logName As String
        logName = os.Name & "_TEMP"


        If os.PartType = bp3doc.bp3doc.enumPartType.PartType_Rassirenie Then

            Exit Sub
        End If

        Dim st As bp3doc.bp3doc.bp3doc_store
        st = os
        System.Windows.Forms.Application.DoEvents()
        Dim chos As bp3doc.bp3doc.bp3doc_store
        Dim i As Short
        Dim s As Writer
        s = New Writer
        Dim collist As String

        Console.WriteLine("-CreateStruct " & logName)

        On Error GoTo bye
        s.putBufLC("/*" & os.Caption & "*/")


        s.putBufLC("create table IF NOT EXISTS " & logName & "/*" & os.the_Comment & " temp table*/ (")
        collist = ""



        s.putBufLC("SessionID BINARY(16) ,/*  id текущей сесии пользователя   */ ")
        collist = collist & "'SessionID'"


        If TypeName(os.Parent.Parent) = "Application" Then
            s.putBufLC("InstanceID BINARY(16) ,")
            collist = collist & "'InstanceID'"
        Else
            s.putBufLC("ParentStructRowID BINARY(16) not null,")
            collist = collist & "'ParentStructRowID'"
        End If


        s.putBufLC(os.Name & "id BINARY(16) not null  ")

        collist = collist & ",'" & os.Name & "ID'"

        s.putBufLC(",ChangeStamp datetime not null /* Время последнего изменения */")
        collist = collist & ",'ChangeStamp'"

        s.putBufLC(",TimeStamp timestamp not null  /* для организации инкрементального индексирования полнотекстовой информации */")
        collist = collist & ",'TimeStamp'"


        s.putBufLC(",LockSessionID BINARY(16) null  /* temporary lock */")
        collist = collist & ",'LockSessionID'"
        s.putBufLC(",LockUserID BINARY(16) null /* checkout lock */")
        collist = collist & ",'LockUserID'"
        s.putBufLC(",SecurityStyleID BINARY(16) null /* security formula */")
        collist = collist & ",'SecurityStyleID'"


        ' дерево
        If os.PartType = 2 Then

            s.putBufLC(",ParentRowid BINARY(16) ")

            collist = collist & ",'ParentRowid'"
        End If

        s.putBufLC(")")
        s.putBufLC("GO")


        If os.UseArchiving = bp3doc.bp3doc.enumBoolean.Boolean_Da Then
            s.putBufLC(procDropSQL("alter_col"))
            s.putBufLC("create procedure alter_col() begin")
            s.putBufLC("        IF not EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE")
            s.putBufLC("           TABLE_SCHEMA = DATABASE() AND")
            s.putBufLC("           TABLE_NAME   = '" + logName + "' AND ")
            s.putBufLC("           column_name   = 'archived') THEN")
            s.putBufLC("                alter table " & logName & " add ")
            s.putBufLC("                archived int null default 0;")
            s.putBufLC("        End If;")
            s.putBufLC("End ")
            s.putBufLC("GO")
            s.putBufLC("call alter_col(); ")
            s.putBufLC("GO")
            s.putBufLC(procDropSQL("alter_col"))
            collist = collist & ",'archived'"
        End If

        If (OptRights) Then
            's.putBufLC("revoke all on " & logName & " to public")
            's.putBufLC("GO")
            's.putBufLC("grant select on " & logName & " to public")
            's.putBufLC("GO")
        End If

        Dim ft As bp3ft.bp3ft.bp3ft_def
        st.bp3doc_field.Sort = "sequence"
        For i = 1 To st.bp3doc_field.Count
            ft = st.bp3doc_field.Item(i).FieldType


            If (ft.TypeStyle <> bp3doc.bp3doc.enumTypeStyle.TypeStyle_Element_oformleniy) Then
                'If i > 1 Then

                's.putbuf ","

                s.putBufLC(procDropSQL("alter_col"))
                s.putBufLC("create procedure alter_col() begin")
                s.putBufLC("        IF not EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE")
                s.putBufLC("           TABLE_SCHEMA = DATABASE() AND")
                s.putBufLC("           TABLE_NAME   = '" + logName + "' AND ")
                s.putBufLC("           column_name   = '" + st.bp3doc_field.Item(i).Name + "') THEN")
                s.putBufLC("                alter table " & logName & " add ")
                s.putBufLC("                " + FieldForCreate(st.bp3doc_field.Item(i)) + ";")
                s.putBufLC("        End If;")
                s.putBufLC("End ")
                s.putBufLC("GO")
                s.putBufLC("call alter_col(); ")
                s.putBufLC("GO")
                s.putBufLC(procDropSQL("alter_col"))

                'support extention .bp3doc_field if file type used

                If UCase(ft.Name) = "FILE" Then

                    s.putBufLC(procDropSQL("alter_col"))
                    s.putBufLC("create procedure alter_col() begin")
                    s.putBufLC("        IF not EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE")
                    s.putBufLC("           TABLE_SCHEMA = DATABASE() AND")
                    s.putBufLC("           TABLE_NAME   = '" + logName + "' AND ")
                    s.putBufLC("           column_name   = '" + st.bp3doc_field.Item(i).Name + "_ext" + "') THEN")
                    s.putBufLC("            alter table " & logName & " add ")
                    s.putBufLC("            " & st.bp3doc_field.Item(i).Name & "_EXT nvarchar(4) null;")
                    s.putBufLC("        End If;")
                    s.putBufLC("End ")
                    s.putBufLC("GO")
                    s.putBufLC("call alter_col(); ")
                    s.putBufLC("GO")
                    s.putBufLC(procDropSQL("alter_col"))
                    collist = collist & ",'" & st.bp3doc_field.Item(i).Name & "_EXT'"
                End If

                collist = collist & ",'" & st.bp3doc_field.Item(i).Name & "'"
                s.putBufLC("GO")
            End If
        Next

        s.putBufLC(ColumnDropSQL(logName, collist))

        o.ModuleName = "--Tables"
        o.Block = "--body"
        o.OutNL(s.getBuf)

        s = Nothing
        s = New Writer

        s.putBufLC(procDropSQL("alter_pk"))
        s.putBufLC("create procedure alter_pk() begin")
        s.putBufLC("    IF not EXISTS (SELECT 1 FROM information_schema.TABLE_CONSTRAINTS WHERE")
        s.putBufLC("           CONSTRAINT_SCHEMA = DATABASE() AND ")
        s.putBufLC("           TABLE_NAME   = '" + os.Name + "_temp' AND ")
        s.putBufLC("           CONSTRAINT_TYPE   = 'PRIMARY KEY') THEN")
        s.putBufLC("            alter table " & os.Name & "_temp add constraint pk_" & os.Name & "_temp primary key (sessionid," & os.Name & "ID);")
        s.putBufLC("    End If;")
        s.putBufLC("End ")
        s.putBufLC("GO")
        s.putBufLC("call alter_pk(); ")
        s.putBufLC("GO")
        s.putBufLC(procDropSQL("alter_pk"))


        o.ModuleName = "--Tables"
        o.Block = "--body"
        o.OutNL(s.getBuf)
        o.OutNL("GO")


        s = Nothing
        s = New Writer


        s.putBufLC(procDropSQL("alter_idx"))
        s.putBufLC("create procedure alter_idx() begin")
        s.putBufLC("if not exists (SELECT 1 ")
        s.putBufLC("  FROM(INFORMATION_SCHEMA.STATISTICS)")
        s.putBufLC("  WHERE(table_schema = DATABASE()) ")
        s.putBufLC("  AND   table_name   = '" + os.Name & "_temp" + "' ")
        s.putBufLC("  AND   index_name   = 'idx_session_" & os.Name & "_temp" + "' ) THEN")
        s.putBufLC("            create index idx_session_" & os.Name & "_temp" & " on " & os.Name & "_temp" & " (sessionID);")
        s.putBufLC("End If;")
        s.putBufLC("End ")
        s.putBufLC("GO")
        s.putBufLC("call alter_idx(); ")
        s.putBufLC("GO")
        s.putBufLC(procDropSQL("alter_idx"))


        o.ModuleName = "--Tables"
        o.Block = "--Index"
        o.OutNL(s.getBuf)
        o.OutNL("GO")


        s = Nothing
        s = New Writer
        s.putBufLC(procDropSQL("alter_fk"))
        s.putBufLC("create procedure alter_fk() begin")
        s.putBufLC("IF not EXISTS (SELECT 1 FROM information_schema.TABLE_CONSTRAINTS WHERE")
        s.putBufLC("           CONSTRAINT_SCHEMA = DATABASE() AND")
        s.putBufLC("           TABLE_NAME   = '" & os.Name & "_temp' AND ")
        s.putBufLC("           CONSTRAINT_TYPE   = 'FOREIGN KEY') THEN")
        s.putBufLC("        alter table " & os.Name & "_temp add constraint fk_" & MakeName(os.ID.ToString()) & "_temp" & " foreign key(sessionID) references the_session ( the_sessionID )   ON DELETE CASCADE;")
        s.putBufLC("End If;")
        s.putBufLC("End ")
        s.putBufLC("GO")
        s.putBufLC("call alter_fk(); ")
        s.putBufLC("GO")
        s.putBufLC(procDropSQL("alter_fk"))


        o.ModuleName = "--Tables"
        o.Block = "--ForeignKey"
        o.OutNL(s.getBuf)
        o.OutNL("GO")


        For i = 1 To os.bp3doc_store.Count
            chos = os.bp3doc_store.Item(i)
            CreateStructTemp(chos)
        Next

        s = Nothing

        Exit Sub
bye:
        Console.WriteLine("ERROR-" & Err.Description & "<--ERROR")
        s = Nothing

    End Sub

    Private Sub CreateDelProc(ByRef os As bp3doc.bp3doc.bp3doc_store)
        If os.PartType = bp3doc.bp3doc.enumPartType.PartType_Rassirenie Then

            Exit Sub
        End If
        Dim ot As bp3doc.bp3doc.bp3doc_def
        ot = TypeForStruct(os)

        Dim st As bp3doc.bp3doc.bp3doc_store
        Dim ft As bp3ft.bp3ft.bp3ft_def
        st = os
        Dim chos As bp3doc.bp3doc.bp3doc_store
        Dim i As Short
        Dim s As Writer
        s = New Writer

        Console.WriteLine("-CreateDelProc " & os.Name)

        On Error GoTo bye


        s.putBufLC(procDropSQL(os.Name & "_DELETE"))
        s.putBufLC(Delimiter)
        s.putBufLC("create procedure " & os.Name & "_DELETE /*" & os.the_Comment & "*/ (")
        s.putBufLC(" acursession varchar(38),")
        s.putBufLC(" a" & os.Name & "id varchar(38),")
        s.putBufLC(" ainstanceid varchar(38) ")
        s.putBufLC(") " & " body: begin  ")
        If Not os.NoLog Then s.putBufLC(" declare aSysLogID BINARY(16);")
        s.putBufLC(" declare aaccess int;")
        s.putBufLC(" declare aSysInstID BINARY(16);")
        s.putBufLC(" declare atmpID BINARY(16);")
        s.putBufLC(" declare aEC int;")
        s.putBufLC(" select Instanceid into aSysInstID from instance where objtype='MTZSYSTEM';")


        s.putBufLC(" select count(*) into aEC from the_session where the_sessionid=g2b(acursession) and closed=0 ;")
        s.putBufLC("if aEC=0 then")
        s.putBufLC("    leave body;")
        s.putBufLC("  end if;")

        If ot.CommitFullObject = bp3doc.bp3doc.enumBoolean.Boolean_Da Then
            s.putBufLC("/*if exists */ select  count(*) into aEC from " & os.Name & "_temp  where sessionid=g2b(acursession) and " & os.Name & "ID = g2b(a" & os.Name & "ID) ;")
        Else
            s.putBufLC("/*if exists */ select  count(*) into aEC from " & os.Name & " where " & os.Name & "ID=g2b(a" & os.Name & "ID);")
        End If

        s.putBufLC("if aEC>0 then")

        s.putBufLC(" select CheckOperation( acursession ,'" + ot.Name + ".edit') into aaccess;")
        s.putBufLC(" if aaccess=0  then")
        s.putBufLC("    select 'Удаление строк не разрешено. Раздел=" & os.Name & "' result;")
        s.putBufLC("    leave body;")
        s.putBufLC("  end if;")




        ' '' Удаляем зависимые документы!!!
        ''s.putBufLC("declare child_inst_" & os.Name & " cursor local for select  instanceid from instance where OwnerPartName ='" & os.Name & "' and OwnerRowID=a" & os.Name & "id")
        ''s.putBufLC("open child_inst_" & os.Name & "")
        ''s.putBufLC("fetch child_inst_" & os.Name & " into achildlistid")
        ''s.putBufLC("while not fetch_done DO  ")
        ''s.putBufLC("begin")
        ''s.putBufLC(" call INSTANCE_OWNER acursession,achildlistid,null,null")
        ''s.putBufLC(" call INSTANCE_DELETE acursession,achildlistid")
        ''s.putBufLC(" if aaerror >0 begin")
        ''s.putBufLC("   close child_inst_" & os.Name & "")
        ' ''s.putBufLC("   deallocate child_inst_" & os.Name & " ")
        ''s.putBufLC("   goto del_error")
        ''s.putBufLC(" end")
        ''s.putBufLC(" fetch child_inst_" & os.Name & " into achildlistid")
        ''s.putBufLC("end")
        ''s.putBufLC("close child_inst_" & os.Name & "")
        ' ''s.putBufLC("deallocate child_inst_" & os.Name & " ")


        s.putBufLC(" call " & os.Name & "_LOGGER(acursession,a" & os.Name & "ID) ; ")


        If ot.CommitFullObject = bp3doc.bp3doc.enumBoolean.Boolean_Da Then
            s.putBufLC("  delete from  " & os.Name & "_temp ")
            s.putBufLC("  where sessionid=g2b(acursession) and " & os.Name & "ID = g2b(a" & os.Name & "ID) ;")
        Else
            s.putBufLC("  delete from  " & os.Name & " ")
            s.putBufLC("  where  " & os.Name & "ID = g2b(a" & os.Name & "ID) ;")
        End If






        s.putBufLC("  delete from num_values where OwnerPartName='" & os.Name & "' and OwnerRowID=g2b(a" & os.Name & "ID);")

        s.putBufLC("  End if;")



        s.putBufLC("    select 'OK' result;")
        s.putBufLC(" end ")

        s.putBufLC("GO")

        o.ModuleName = "--Procedures"
        o.Block = "--TableProc"
        o.OutNL(s.getBuf)
        o.OutNL("GO")



        s = Nothing


        If os.UseArchiving = bp3doc.bp3doc.enumBoolean.Boolean_Da Then
            s = New Writer

            Console.WriteLine("-CreateArchiveProc " & os.Name)

            On Error GoTo bye


            s.putBufLC(procDropSQL(os.Name & "_ARCHIVE"))
            s.putBufLC(Delimiter)
            s.putBufLC("create procedure " & os.Name & "_ARCHIVE /*" & os.the_Comment & "*/ (")
            s.putBufLC(" acursession varchar(38),")
            s.putBufLC(" a" & os.Name & "id varchar(38),")
            s.putBufLC(" ainstanceid varchar(38) ")
            s.putBufLC(") " & " body: begin  ")
            If Not os.NoLog Then s.putBufLC(" declare aSysLogID BINARY(16);")
            s.putBufLC(" declare aaccess int;")
            's.putBufLC(" declare aSysInstID BINARY(16);")
            s.putBufLC(" declare atmpID BINARY(16);")
            s.putBufLC(" declare aEC int;")
            's.putBufLC(" select Instanceid into aSysInstID from instance where objtype='MTZSYSTEM';")


            s.putBufLC(" select count(*) into aEC from the_session where the_sessionid=g2b(acursession) and closed=0 ;")
            s.putBufLC("if aEC=0 then")
            s.putBufLC("    leave body;")
            s.putBufLC("  end if;")


            s.putBufLC("/*if exists */ select  count(*) into aEC from " & os.Name & " where " & os.Name & "ID=g2b(a" & os.Name & "ID);")
            s.putBufLC("if aEC>0 then")

            s.putBufLC(" select CheckOperation( acursession ,'" + ot.Name + ".edit') into aaccess;")
            s.putBufLC(" if aaccess=0  then")
            s.putBufLC("    select 'Удаление строк не разрешено. Раздел=" & os.Name & "' result;")
            s.putBufLC("    leave body;")
            s.putBufLC("  end if;")

            s.putBufLC(" call " & os.Name & "_LOGGER(acursession,a" & os.Name & "ID) ; ")

            If ot.CommitFullObject = bp3doc.bp3doc.enumBoolean.Boolean_Da Then
                s.putBufLC("  update   " & os.Name & "_temp set archived = 1 ")
                s.putBufLC("  where sessionid=g2b(acursession) and " & os.Name & "ID = g2b(a" & os.Name & "ID) ;")
            Else
                s.putBufLC("  update   " & os.Name & " set archived = 1 ")
                s.putBufLC("  where  " & os.Name & "ID = g2b(a" & os.Name & "ID) ;")
            End If

            s.putBufLC("  End if;")



            s.putBufLC("    select 'OK' result;")
            s.putBufLC(" end ")

            s.putBufLC("GO")

            o.ModuleName = "--Procedures"
            o.Block = "--TableProc"
            o.OutNL(s.getBuf)
            o.OutNL("GO")



            s = Nothing
        End If

        Exit Sub
bye:
        Console.WriteLine("ERROR-" & Err.Description & "<--ERROR")
        s = Nothing
    End Sub


    Private Sub CreateLoggerProc(ByRef os As bp3doc.bp3doc.bp3doc_store)

        If os.PartType = bp3doc.bp3doc.enumPartType.PartType_Rassirenie Then
            Exit Sub
        End If
        Dim ot As bp3doc.bp3doc.bp3doc_def
        ot = TypeForStruct(os)

        Dim st As bp3doc.bp3doc.bp3doc_store
        Dim ft As bp3ft.bp3ft.bp3ft_def
        st = os
        Dim chos As bp3doc.bp3doc.bp3doc_store
        Dim i As Short
        Dim s As Writer
        s = New Writer

        Console.WriteLine("-CreateLoggerProc " & os.Name)

        On Error GoTo bye


        s.putBufLC(procDropSQL(os.Name & "_LOGGER"))
        s.putBufLC(Delimiter)
        s.putBufLC("create procedure " & os.Name & "_LOGGER /*" & os.the_Comment & "*/ (")
        s.putBufLC(" acursession varchar(38),")
        s.putBufLC(" a" & os.Name & "id varchar(38)")
        s.putBufLC(") " & " body: begin  ")
        s.putBufLC(" declare aEC int;")

        s.putBufLC(" select count(*) into aEC from the_session where the_sessionid=g2b(acursession) and closed=0 ;")
        s.putBufLC(" if aEC=0 then")
        s.putBufLC("    leave body;")
        s.putBufLC(" end if;")


        If os.UseChangeLog = bp3doc.bp3doc.enumBoolean.Boolean_Da Then
            s.putBufLC(" insert into " & os.Name & "_LOG ")

            s.putBufLC(" ( " & os.Name & "_logID ")


            If TypeName(os.Parent.Parent) = "Application" Then
                s.putBufLC(",InstanceID")
            Else
                s.putBufLC(",ParentStructRowID")
            End If

            If os.PartType = 2 Then
                s.putBufLC(",ParentRowid")
            End If

            s.putBufLC(", UserLog")
            s.putBufLC(", changestamp")
            s.putBufLC(", datetimelog")

            If os.UseArchiving = bp3doc.bp3doc.enumBoolean.Boolean_Da Then
                s.putBufLC(", archived")
            End If



            st.bp3doc_field.Sort = "sequence"
            For i = 1 To st.bp3doc_field.Count
                ft = st.bp3doc_field.Item(i).FieldType
                If (ft.TypeStyle <> bp3doc.bp3doc.enumTypeStyle.TypeStyle_Element_oformleniy) Then
                    If ft.DelayedSave = bp3doc.bp3doc.enumBoolean.Boolean_Net Then
                        s.putBufLC("," & st.bp3doc_field.Item(i).Name & vbCrLf)


                        If UCase(ft.Name) = "FILE" Then
                            s.putBufLC("," & st.bp3doc_field.Item(i).Name & "_EXT")
                        End If
                    End If
                End If
            Next

            s.putBufLC(" ) select ")

            s.putBufLC(os.Name & "ID ")



            If TypeName(os.Parent.Parent) = "Application" Then
                s.putBufLC(",InstanceID")
            Else
                s.putBufLC(",ParentStructRowID")
            End If

            If os.PartType = 2 Then
                s.putBufLC(",ParentRowid")
            End If
            s.putBufLC(", acursession")
            s.putBufLC(", changestamp")
            s.putBufLC(", now()")

            If os.UseArchiving = bp3doc.bp3doc.enumBoolean.Boolean_Da Then
                s.putBufLC(", archived")
            End If

            st.bp3doc_field.Sort = "sequence"
            For i = 1 To st.bp3doc_field.Count
                ft = st.bp3doc_field.Item(i).FieldType

                If (ft.TypeStyle <> bp3doc.bp3doc.enumTypeStyle.TypeStyle_Element_oformleniy) Then
                    If ft.DelayedSave = bp3doc.bp3doc.enumBoolean.Boolean_Net Then
                        s.putBufLC("," & st.bp3doc_field.Item(i).Name & vbCrLf)


                        If UCase(ft.Name) = "FILE" Then
                            s.putBufLC("," & st.bp3doc_field.Item(i).Name & "_EXT")
                        End If
                    End If
                End If
            Next

            s.putBufLC(" from " & os.Name)
            s.putBufLC(" where " & os.Name & "id =g2b( a" & os.Name & "ID); ")

            's.putBufLC("-- end LOG --")
        End If
        s.putBufLC(" end ")

        s.putBufLC("GO")

        o.ModuleName = "--Procedures"
        o.Block = "--TableProc"
        o.OutNL(s.getBuf)
        o.OutNL("GO")



        s = Nothing

        For i = 1 To os.bp3doc_store.Count
            chos = os.bp3doc_store.Item(i)
            CreateLoggerProc(chos)

        Next

        Exit Sub
bye:
        Console.WriteLine("ERROR-" & Err.Description & "<--ERROR")
        s = Nothing
    End Sub



    Private Sub CreateProc(ByRef os As bp3doc.bp3doc.bp3doc_store)

        If os.PartType = bp3doc.bp3doc.enumPartType.PartType_Rassirenie Then
            Exit Sub
        End If

        CreateBriefProc(os)


        Dim st As bp3doc.bp3doc.bp3doc_store
        st = os
        Dim chos As bp3doc.bp3doc.bp3doc_store
        Dim i As Short
        Dim hasownership As Boolean = False
        Dim ot As bp3doc.bp3doc.bp3doc_def = Nothing
        ot = TypeForStruct(os)
        If TypeName(os.Parent.Parent) = "Application" Then

            If ot.UseOwnership = bp3doc.bp3doc.enumBoolean.Boolean_Da Then
                If os.PartType = bp3doc.bp3doc.enumPartType.PartType_Stroka Then
                    hasownership = True
                End If
            End If
        End If



        If OptProcs Then


            CreateDelProc(os)



            Dim s As Writer
            s = New Writer

            Console.WriteLine("-CreateProc " & os.Name)

            On Error GoTo bye

            s.putBufLC("/*" & os.Caption & "*/")
            s.putBufLC(procDropSQL(os.Name & "_SAVE"))
            s.putBufLC(Delimiter)
            s.putBufLC("create procedure " & os.Name & "_SAVE /*" & os.the_Comment & "*/ (")
            s.putBufLC(" acursession varchar(38),")
            s.putBufLC("aInstanceID varchar(38) ,")
            If TypeName(os.Parent.Parent) <> "Application" Then
                s.putBufLC(" aParentStructRowID varchar(38) ,")
            End If

            s.putBufLC(" a" & os.Name & "id varchar(38)")

            ' дерево - родительская связь
            If os.PartType = bp3doc.bp3doc.enumPartType.PartType_Derevo Then
                s.putBufLC(",aParentRowid varchar(38) ")
            End If

            Dim ft As bp3ft.bp3ft.bp3ft_def
            st.bp3doc_field.Sort = "sequence"
            For i = 1 To st.bp3doc_field.Count
                ft = st.bp3doc_field.Item(i).fieldType
                If (ft.TypeStyle <> bp3doc.bp3doc.enumTypeStyle.TypeStyle_Element_oformleniy) Then
                    If ft.DelayedSave = bp3doc.bp3doc.enumBoolean.Boolean_Net Then
                        s.putBufLC("," & fieldForParam(st.bp3doc_field.Item(i)))
                    End If
                End If
            Next

            s.putBufLC(")  body: begin  ")
            ' s.putBufLC("set nocount on")

            If Not os.NoLog Then s.putBufLC("DECLARE aSysLogid BINARY(16);")
            s.putBufLC(" declare aUniqueRowCount integer;")
            s.putBufLC(" declare atmpStr varchar(255);")
            s.putBufLC(" declare atmpID BINARY(16);")
            s.putBufLC(" declare aaccess int;")
            s.putBufLC(" declare aSysInstID BINARY(16);")
            s.putBufLC(" declare aSessUserID BINARY(16);")
            s.putBufLC(" declare aMLF_PartID BINARY(16);")
            s.putBufLC(" declare aSessUserLogin varchar(40);")
            s.putBufLC(" declare aEC int;")

            s.putBufLC(" select UsersID into aSessUserID from the_session where the_sessionid=g2b(acursession);")
            s.putBufLC(" select login into aSessUserLogin from users where usersid=g2b(aSessUserID);")

            s.putBufLC(" select Instanceid into aSysInstID from instance where objtype='MTZSYSTEM';")

            s.putBufLC(" select count(*) into aEC from the_session where the_sessionid=g2b(acursession) and closed=0 ;")
            s.putBufLC("if aEC=0  then")
            s.putBufLC("  select 'Сессия уже завершена.' result;")
            's.putBufLC("    if aatrancount>0 rollback tran")
            s.putBufLC("    leave body;")
            s.putBufLC("  end if;")




            's.putBufLC(" -- Insert / Update body -- ")

            If ot.CommitFullObject = bp3doc.bp3doc.enumBoolean.Boolean_Da Then
                s.putBufLC("/*if exists */ select  count(*) into aEC from " & os.Name & "_temp where sessionid=g2b(acursession) and " & os.Name & "ID=g2b(a" & os.Name & "ID);")

            Else
                s.putBufLC("/*if exists */ select  count(*) into aEC from " & os.Name & " where " & os.Name & "ID=g2b(a" & os.Name & "ID);")

            End If

            s.putBufLC("if aEC >0 then")
            s.putBufLC(" --  UPDATE  --")
            s.putBufLC(" --  verify access  --")

            s.putBufLC(" select CheckOperation( acursession ,'" + ot.Name + ".edit') into aaccess;")
            s.putBufLC(" if aaccess=0  then")
            s.putBufLC("    select 'Изменение строк не разрешено. Раздел=" & os.Name & "' result;")
            s.putBufLC("    leave body;")
            s.putBufLC("  end if;")

            s.putBufLC(" start transaction ; ")

            's.putBufLC(" -- update row  --")

            s.putBufLC(" call " & os.Name & "_LOGGER(acursession,a" & os.Name & "ID) ; ")

            If ot.CommitFullObject = bp3doc.bp3doc.enumBoolean.Boolean_Da Then
                s.putBufLC(" update  " & os.Name & "_temp set ChangeStamp=now()")
            Else
                s.putBufLC(" update  " & os.Name & " set ChangeStamp=now()")
            End If



            ' дерево модификация связи
            If os.PartType = 2 Then
                s.putBufLC(",ParentRowid= g2b(aParentRowid)")
            End If


            Dim usefield As Boolean
            Dim fld As bp3doc.bp3doc.bp3doc_field


            st.bp3doc_field.Sort = "sequence"
            For i = 1 To st.bp3doc_field.Count
                usefield = True
                fld = st.bp3doc_field.Item(i)
                ft = fld.fieldType
                If ft.TypeStyle = bp3doc.bp3doc.enumTypeStyle.TypeStyle_Element_oformleniy Then
                    usefield = False
                End If

                If fld.TheStyle.Contains("noupdate") Then
                    usefield = False
                End If



                If usefield Then

                    If ft.TypeStyle = bp3doc.bp3doc.enumTypeStyle.TypeStyle_Ssilka And ft.Name <> "MULTIREF" Then
                        s.putBufLC(",")
                        s.putBufLC("  " & st.bp3doc_field.Item(i).Name & "=g2b(a" & st.bp3doc_field.Item(i).Name & ")")

                    Else

                        If ft.DelayedSave = bp3doc.bp3doc.enumBoolean.Boolean_Net Then
                            s.putBufLC(",")
                            s.putBufLC("  " & st.bp3doc_field.Item(i).Name & "=a" & st.bp3doc_field.Item(i).Name)


                            If UCase(ft.Name) = "FILE" Then
                                s.putBufLC("," & st.bp3doc_field.Item(i).Name & "_EXT=")
                                s.putBufLC("a" & st.bp3doc_field.Item(i).Name & "_EXT ")
                            End If
                        End If
                    End If

                End If
            Next

            If ot.CommitFullObject = bp3doc.bp3doc.enumBoolean.Boolean_Da Then
                s.putBufLC("  where  sessionid=g2b(acursession) and " & os.Name & "ID = g2b(a" & os.Name & "ID) ;")
            Else
                s.putBufLC("  where  " & os.Name & "ID = g2b(a" & os.Name & "ID) ;")
            End If

            s.putBufLC(UniqueCheck(os) & vbCrLf)

            'If ot.CommitFullObject = bp3doc.bp3doc.enumBoolean.Boolean_Net Then

            s.putBufLC("call " & os.Name & "_client_trigger(acursession,a" & os.Name & "id);" & vbCrLf)

            'End If



            's.putBufLC("  end if;")

            s.putBufLC(" else")

            s.putBufLC(" --  INSERT  --")

            s.putBufLC(" --  verify access  --")

            s.putBufLC(" select CheckOperation( acursession ,'" + ot.Name + ".edit') into aaccess;")
            s.putBufLC(" if aaccess=0  then")
            s.putBufLC("    select 'Добавление строк не разрешено. Раздел=" & os.Name & "' result;")
            s.putBufLC("    leave body;")
            s.putBufLC("  end if;")



            ' check for single row bp3doc_store
            If os.PartType = 0 Then


                If ot.CommitFullObject = bp3doc.bp3doc.enumBoolean.Boolean_Da Then
                    s.putBufLC("select count(*) into aEC from " & os.Name & "_temp where  sessionid=g2b(acursession) and ")
                Else
                    s.putBufLC("select count(*) into aEC from " & os.Name & " where ")
                End If





                If TypeName(os.Parent.Parent) = "Application" Then
                    s.putBufLC("InstanceID=g2b(aInstanceID);")
                Else
                    s.putBufLC("ParentStructRowID=g2b(aParentStructRowID);")
                End If

                s.putBufLC("if aEC >0 then ")
                s.putBufLC("    select 'Невозможно создать вторую строку в однострочной сессии. Раздел: <" & os.Name & ">' result;")
                s.putBufLC("    rollback;")
                s.putBufLC("    leave body;")
                s.putBufLC(" End if;")
            End If

            s.putBufLC(" start transaction;  ")

            If ot.CommitFullObject = bp3doc.bp3doc.enumBoolean.Boolean_Da Then
                s.putBufLC(" insert into   " & os.Name & "_temp" & vbCrLf & " ( sessionid, " & os.Name & "ID ")
            Else
                s.putBufLC(" insert into   " & os.Name & vbCrLf & " (  " & os.Name & "ID ")

            End If



            ' дерево  - поле
            If os.PartType = 2 Then
                s.putBufLC(",ParentRowid")
            End If


            If TypeName(os.Parent.Parent) = "Application" Then
                s.putBufLC(",InstanceID")
            Else
                s.putBufLC(",ParentStructRowID")
            End If


            st.bp3doc_field.Sort = "sequence"
            For i = 1 To st.bp3doc_field.Count
                ft = st.bp3doc_field.Item(i).fieldType
                If (ft.TypeStyle <> bp3doc.bp3doc.enumTypeStyle.TypeStyle_Element_oformleniy) Then
                    If ft.DelayedSave = bp3doc.bp3doc.enumBoolean.Boolean_Net Then
                        s.putBufLC("," & st.bp3doc_field.Item(i).Name & vbCrLf)

                        If UCase(ft.Name) = "FILE" Then
                            s.putBufLC("," & st.bp3doc_field.Item(i).Name & "_EXT")
                        End If
                    End If
                End If
            Next


            If ot.CommitFullObject = bp3doc.bp3doc.enumBoolean.Boolean_Da Then
                s.putBufLC(" ) values " & "( g2b(acursession), g2b(a" & os.Name & "ID) ")
            Else
                s.putBufLC(" ) values " & "( g2b(a" & os.Name & "ID) ")

            End If



            ' дерево  - значение поля
            If os.PartType = 2 Then
                s.putBufLC(",g2b(aParentRowid)")
            End If



            If TypeName(os.Parent.Parent) = "Application" Then
                s.putBufLC(",g2b(aInstanceID)")
            Else
                s.putBufLC(",g2b(aParentStructRowID)")
            End If

            st.bp3doc_field.Sort = "sequence"
            For i = 1 To st.bp3doc_field.Count
                ft = st.bp3doc_field.Item(i).fieldType
                If (ft.TypeStyle <> bp3doc.bp3doc.enumTypeStyle.TypeStyle_Element_oformleniy) Then

                    If ft.TypeStyle = bp3doc.bp3doc.enumTypeStyle.TypeStyle_Ssilka And ft.Name <> "MULTIREF" Then
                        s.putBufLC(",g2b(a" & st.bp3doc_field.Item(i).Name & ")" & vbCrLf)
                    Else

                        If ft.DelayedSave = bp3doc.bp3doc.enumBoolean.Boolean_Net Then
                            s.putBufLC(",a" & st.bp3doc_field.Item(i).Name & vbCrLf)
                            'support extention .bp3doc_field if file type used

                            If UCase(ft.Name) = "FILE" Then
                                s.putBufLC(",a" & st.bp3doc_field.Item(i).Name & "_EXT")
                            End If
                        End If
                    End If

                End If
            Next

            s.putBufLC(" ) ;")




            's.putBufLC(" call " & os.Name & "_SINIT aCURSESSION,a" & os.Name & "id,atmpid;")

            's.putBufLC(" -- checking unique constraints  --")

            'If ot.CommitFullObject = bp3doc.bp3doc.enumBoolean.Boolean_Net Then
            s.putBufLC(UniqueCheck(os) & vbCrLf)

            s.putBufLC("call " & os.Name & "_client_trigger(acursession,a" & os.Name & "id);" & vbCrLf)


            If hasownership Then
                s.putBufLC(" call " & ot.Name & "_INITOWNERSHIP( aCURSESSION,ainstanceid);")
            End If
            'End If

            s.putBufLC(" end if;")


            's.putBufLC(" -- close transaction --")

            's.putBufLC(" if aaerror <>0  then rollback tran;   ")
            s.putBufLC(" commit; ")
            s.putBufLC("select 'OK' result;")
            s.putBufLC(" end ")
            s.putBufLC("GO")
            '
            '
            o.ModuleName = "--Procedures"
            o.Block = "--TableProc"
            o.OutNL(s.getBuf)
            o.OutNL("GO")
            s = Nothing

        End If

        For i = 1 To os.bp3doc_store.Count
            chos = os.bp3doc_store.Item(i)
            CreateProc(chos)
        Next



        Exit Sub
bye:
        Console.WriteLine("ERROR-" & Err.Description & "<--ERROR")

    End Sub






    '    ' make
    '    Private Function ReferenceCheck(ByRef os As bp3doc.bp3doc.bp3doc_store, ByRef f As bp3doc.bp3doc..bp3doc_field) As String
    '        'MTZUtilUtility_definst.DebugOutput("MYSQLGEN.ReferenceCheck:start " & os.Caption & " Filed:" & f.Caption)
    '        On Error GoTo bye
    '        Dim s As Writer
    '        s = New Writer
    '        console.writeline( "-ReferenceCheck " & f.Name)

    '        'не ссылка
    '        'объект
    '        'строка

    '        'RAISERROR   ('The current database ID is:%d, the database name is: %s.',    16, 1, aDBID, aDBNAME)


    '        If f.ReferenceType = bp3doc.bp3doc.enumReferenceType.ReferenceType_Skalyrnoe_pole_OPN_ne_ssilkaCLS Then
    '            s.putBufLC("")
    '        End If

    '        If f.ReferenceType = bp3doc.bp3doc.enumReferenceType.ReferenceType_Na_ob_ekt_ Then
    '            s.putBufLC("if(not exists( select  1 from instance where instanceid=g2b(a" & f.Name & " ))) ")
    '            s.putBufLC("  begin")
    '            s.putBufLC("    raiserror('Не обнаружен объект, на который установлена ссылка. Раздел=" & os.Name & " .bp3doc_field=" & f.Name & "',16,1)")
    '            s.putBufLC("    if aatrancount>0 rollback tran")
    '            s.putBufLC("    return")
    '            s.putBufLC("  end")
    '        End If

    '        Dim refp As bp3doc.bp3doc.bp3doc_store
    '        If f.ReferenceType = bp3doc.bp3doc.enumReferenceType.ReferenceType_Na_stroku_razdela Then
    '            s.putBufLC("if(not a" & f.Name & " is null ) ")
    '            refp = f.RefToPart
    '            s.putBufLC("if(not exists( select  1 from " & refp.Name & " where " & refp.Name & "id=g2b(a" & f.Name & ") )) ")
    '            s.putBufLC("  begin")
    '            s.putBufLC("    raiserror('Отсутствует строка в таблице (" & refp.Name & "), на которую установлена ссылка.  Раздел=" & os.Name & " поле=" & f.Name & "',16,1)")
    '            s.putBufLC("    if aatrancount>0 rollback tran")
    '            s.putBufLC("    return")
    '            s.putBufLC("  end")
    '        End If

    '        ReferenceCheck = s.getBuf
    '        'MTZUtilUtility_definst.DebugOutput("MYSQLGEN.ReferenceCheck:done " & os.Caption & " Filed:" & f.Caption)
    '        s = Nothing
    '        Exit Function
    'bye:
    '        console.writeline( "ERROR-" & Err.Description & "<--ERROR")
    '        s = Nothing
    '    End Function




    '    Private Sub CreateMethod(ByRef m As MTZMetaModel.MTZMetaModel.SHAREDMETHOD)
    '        'MTZUtilUtility_definst.DebugOutput("MYSQLGEN.CreateMethod:start")
    '        On Error GoTo bye
    '        Dim p As MTZMetaModel.MTZMetaModel.PARAMETERS
    '        Dim i As Integer
    '        Dim s, s1 As String
    '        Dim ftm As bp3ft_map
    '        Dim Parameters As MTZMetaModel.MTZMetaModel.PARAMETERS_col
    '        s1 = GetScript(m.SCRIPT)

    '        If s1 = "" Then Exit Sub

    '        console.writeline( "-CreateMethod " & m.Name)
    '        Parameters = GetParameters(m.SCRIPT)
    '        s = "/* " & m.Name & "  " & m.the_Comment & "*/"
    '        If m.ReturnType Is Nothing Then
    '            s = s & vbCrLf & procDropSQL((m.Name))
    '            s = s & vbCrLf & "create procedure " & m.Name & vbCrLf
    '            If Parameters.Count > 0 Then
    '                s = s & vbCrLf & "("
    '            End If
    '        Else
    '            s = s & vbCrLf & funcDropSQL((m.Name))
    '            s = "create function " & m.Name & vbCrLf
    '        End If



    '        Parameters.Sort = "sequence"
    '        For i = 1 To Parameters.Count
    '            p = Parameters.Item(i)
    '            If i > 1 Then s = s & vbCrLf & ","
    '            s = s & MethodParam(p) & vbCrLf
    '        Next

    '        If Not m.ReturnType Is Nothing Then
    '            s = s & vbCrLf & ") "

    '            s = s & vbCrLf & " returns " & MapFTObj(m.ReturnType.ID.ToString()).StoageType & vbCrLf
    '        Else
    '            If Parameters.Count > 0 Then
    '                s = s & vbCrLf & ")"
    '            End If
    '        End If


    '        s = s & vbCrLf & " "

    '        s = s & vbCrLf & "body:begin"


    '        o.ModuleName = "--Procedures"
    '        o.Block = "--Methods"
    '        o.OutNL(s)
    '        s = ""

    '        s1 = GetScript(m.SCRIPT)

    '        If s1 = "" Then
    '            s1 = "print 'to do'"
    '        End If

    '        s = s & s1 & vbCrLf

    '        s = s & vbCrLf & "end"
    '        '

    '        o.ModuleName = "--Procedures"
    '        o.Block = "--Methods"
    '        o.OutNL(s)
    '        o.OutNL("GO")


    '        Exit Sub
    'bye:
    '        console.writeline( "ERROR-" & Err.Description & "<--ERROR")
    '        'Resume
    '    End Sub



    Private Function FieldForCreate(ByRef f As bp3doc.bp3doc.bp3doc_field) As String

        On Error Resume Next

        Console.WriteLine("-FieldForCreate " & f.Name)

        Dim s As String
        Dim ftm As bp3ft.bp3ft.bp3ft_map
        Dim ft As bp3ft.bp3ft.bp3ft_def
        Dim rp As bp3doc.bp3doc.bp3doc_store
        s = f.Name
        ft = f.FieldType
        Dim IntPKey As Boolean = False

        'If ft.TypeStyle = bp3doc.bp3doc.enumTypeStyle.TypeStyle_Ssilka Then
        '    If f.ReferenceType = bp3doc.bp3doc.enumReferenceType.ReferenceType_Na_stroku_razdela Then
        '        rp = f.RefToPart
        '        If Not rp Is Nothing Then
        '            If rp.IntegerPKey = bp3doc.bp3doc.enumBoolean.Boolean_Da Then
        '                IntPKey = True
        '                s = s & " " & " Integer "
        '            End If
        '        End If

        '    End If
        'End If
        'If Not IntPKey Then
        ftm = MapFTObj(ft.ID.ToString())
        If ftm.FixedSize <> 0 Then
            s = s & " " & ftm.StoageType & "(" & ftm.FixedSize & ")"
        Else

            s = s & vbCrLf & " " & ftm.StoageType

            If ft.AllowSize Then
                If f.DataSize <> 0 Then
                    s = s & " (" & f.DataSize & ")"
                Else
                    s = s & " (1)"
                End If
            End If
        End If
        'End If
        ' If F.AllowNull Then
        s = s & " null "
        ' Else
        '  s = s & " not null "
        ' End If



        If ft.TypeStyle = bp3doc.bp3doc.enumTypeStyle.TypeStyle_Interval Then

            's = s & vbCrLf & " check (" & f.Name & " >= " & ft.Minimum & " and " & f.Name & " <= " & ft.Maximum & ")"
        End If


        Dim e As Object
        If ft.TypeStyle = bp3doc.bp3doc.enumTypeStyle.TypeStyle_Perecislenie Then

            'If ft.bp3ft_enums.Count > 0 Then
            '    s = s & vbCrLf & " check (" & f.Name & " in ( "

            '    For e = 1 To ft.bp3ft_enums.Count

            '        If e > 1 Then s = s & vbCrLf & ", "

            '        s = s & ft.bp3ft_enums.Item(e).NameValue & "/* " & ft.bp3ft_enums.Item(e).Name & " */"
            '    Next
            '    s = s & " )) "
            'End If
        End If

        s = s & "/* " & f.Caption & " */"

        '   'support extention .bp3doc_field if file type used
        '   If UCase(F..bp3ft_def.Name) = "FILE" Then
        '     s = s & vbCrLf & "," & F.Name & "_EXT nvarchar(4) null"
        '   End If

        FieldForCreate = s

        Exit Function
bye:
        Console.WriteLine("ERROR-" & Err.Description & "<--ERROR")

    End Function

    Private Function fieldForParam2(ByRef f As bp3doc.bp3doc.bp3doc_field) As String
        On Error GoTo bye


        Console.WriteLine("-.bp3doc_fieldForParam " & f.Name)
        Dim s As String
        Dim ftm As bp3ft_map
        Dim ft As bp3ft.bp3ft.bp3ft_def
        s = "a" & f.Name
        ft = f.FieldType
        Dim IntPKey As Boolean = False
        Dim rp As bp3doc.bp3doc.bp3doc_store


        If ft.TypeStyle = bp3doc.bp3doc.enumTypeStyle.TypeStyle_Ssilka And ft.Name <> "MULTIREF" Then
            s = s & " varchar(38)"
        Else
            ftm = MapFTObj(ft.ID.ToString())
            If ftm.FixedSize <> 0 Then
                s = s & " " & ftm.StoageType & "(" & ftm.FixedSize & ")"
            Else
                s = s & vbCrLf & " " & ftm.StoageType

                If ft.AllowSize Then
                    If f.DataSize <> 0 Then
                        s = s & " (" & f.DataSize & ")"
                    Else
                        s = s & " (1)"
                    End If
                End If
            End If
        End If


        If f.AllowNull Then
            s = s & " = null "
        End If

        's = s & "/* " & f.Caption & " */"

        'support extention .bp3doc_field if file type used

        If UCase(ft.Name) = "FILE" Then
            s = s & vbCrLf & ",a" & f.Name & "_EXT nvarchar(4) = null"
        End If

        fieldForParam2 = s '& "/* " & f.Caption & " */"

        Exit Function
bye:
        Console.WriteLine("ERROR-" & Err.Description & "-ERROR")
        'Resume
    End Function

    Private Function fieldForParam(ByRef f As bp3doc.bp3doc.bp3doc_field) As String
        On Error GoTo bye


        Console.WriteLine("-.bp3doc_fieldForParam " & f.Name)
        Dim s As String
        Dim ftm As bp3ft_map
        Dim ft As bp3ft.bp3ft.bp3ft_def
        ft = f.FieldType
        s = "a" & f.Name

        Dim rp As bp3doc.bp3doc.bp3doc_store


        If ft.TypeStyle = bp3doc.bp3doc.enumTypeStyle.TypeStyle_Ssilka And ft.Name <> "MULTIREF" Then
            s = s & " varchar(38)"
        Else


            ftm = MapFTObj(ft.ID.ToString())
            If ftm.FixedSize <> 0 Then
                s = s & " " & ftm.StoageType & "(" & ftm.FixedSize & ")"
            Else
                s = s & vbCrLf & " " & ftm.StoageType

                If ft.AllowSize Then
                    If f.DataSize <> 0 Then
                        s = s & " (" & f.DataSize & ")"
                    Else
                        s = s & " (1)"
                    End If
                End If
            End If
        End If


        'If f.AllowNull Then
        '    s = s & "  "
        'End If

        s = s & "/* " & f.Caption & " */"

        'support extention .bp3doc_field if file type used

        If UCase(ft.Name) = "FILE" Then
            s = s & vbCrLf & ",a" & f.Name & "_EXT varchar(4) "
        End If

        fieldForParam = s & "/* " & f.Caption & " */"
        Exit Function
bye:
        Console.WriteLine("ERROR-" & Err.Description & "<--ERROR")
        'Resume
    End Function




    '    Private Function MethodParam(ByRef f As MTZMetaModel.MTZMetaModel.PARAMETERS) As String
    '        'MTZUtilUtility_definst.DebugOutput("MYSQLGEN.MethodParam:start")
    '        On Error GoTo bye
    '        console.writeline( "-MethodParam " & f.Name)
    '        Dim s As String
    '        Dim ftm As bp3ft_map
    '        Dim ft As bp3ft.bp3ft.bp3ft_def
    '        s = ""
    '        If f.OutParam Then
    '            s = s & " out "
    '        End If

    '        s = s & "a" & f.Name
    '        ft = f.TypeOfParm
    '        ftm = MapFTObj(f.TypeOfParm.ID.ToString())
    '        If ftm.FixedSize <> 0 Then
    '            s = s & " " & ftm.StoageType & "(" & ftm.FixedSize & ")"
    '        Else
    '            s = s & " " & ftm.StoageType
    '            If ft.AllowSize Then
    '                If f.DataSize <> 0 Then
    '                    s = s & " (" & f.DataSize & ")"
    '                Else
    '                    s = s & " (1)"
    '                End If
    '            End If
    '        End If




    '        MethodParam = s & "/* " & f.Caption & " */"

    '        Exit Function
    'bye:
    '        console.writeline( "ERROR-" & Err.Description & "<--ERROR")

    '    End Function


    Private Function UniqueCheck(ByRef os As bp3doc.bp3doc.bp3doc_store) As String

        Dim ot As bp3doc.bp3doc.bp3doc_def
        Dim doc_app As bp3doc.bp3doc.Application


        If os.PartType = bp3doc.bp3doc.enumPartType.PartType_Rassirenie Then
            Return ""
        End If

        ot = TypeForStruct(os)
        doc_app = os.Application

        System.Windows.Forms.Application.DoEvents()

        Console.WriteLine("-UniqueCheck for " & os.Name)
        On Error GoTo bye
        Dim s As String
        Dim st As bp3doc.bp3doc.bp3doc_store
        Dim uc As bp3doc_uk
        Dim cf As bp3doc_ukfld
        Dim fl As bp3doc.bp3doc.bp3doc_field
        Dim ft As bp3ft.bp3ft.bp3ft_def
        Dim i, j As Integer
        st = os
        s = ""

        Dim z As String


        If doc_app.bp3doc_uk.Count > 0 Then
            s = s & "-- If aSessUserLogin<>'replicator'  then"
            For i = 1 To doc_app.bp3doc_uk.Count
                uc = doc_app.bp3doc_uk.Item(i)
                If uc.UKStore.ID.Equals(os.ID) Then

                    z = ""
                    If uc.bp3doc_ukfld.Count > 0 Then

                        For j = 1 To uc.bp3doc_ukfld.Count
                            cf = uc.bp3doc_ukfld.Item(j)
                            fl = cf.TheField
                            If Not cf.TheField Is Nothing Then
                                ft = fl.FieldType
                                If ft.Name = "ID" Or ft.Name = "Reference" Then
                                    z = z & vbCrLf & " and " & fl.Name & "=g2b(a" & fl.Name & ")"
                                Else
                                    z = z & vbCrLf & " and " & fl.Name & "=a" & fl.Name
                                End If

                            Else
                                Console.WriteLine("WARNING-field not defined in unique constraint Table=" & st.Name & " <--WARNING")
                            End If
                        Next

                        Dim checkTable As String = ""

                        If ot.CommitFullObject = bp3doc.bp3doc.enumBoolean.Boolean_Da Then
                            checkTable = os.Name & "_temp where sessionid = g2b( acursession) and "
                        Else
                            checkTable = os.Name & " where "
                        End If
                        If uc.PerParent Then
                            If os.PartType = bp3doc.bp3doc.enumPartType.PartType_Derevo Then
                                If TypeName(os.Parent.Parent) = "Application" Then
                                    s = s & vbCrLf & " if aParentRowID is null then"
                                    s = s & vbCrLf & "   select Count(*) into  aUniqueRowCount from " & checkTable & " InstanceID=g2b(aInstanceID) and ParentRowID is null " & z & ";"
                                    s = s & vbCrLf & " else "
                                    s = s & vbCrLf & "   select Count(*) into  aUniqueRowCount from " & checkTable & " InstanceID=g2b(aInstanceID) and ParentRowID=g2b(aParentRowID) " & z & ";"
                                    s = s & vbCrLf & " end if;"
                                Else
                                    s = s & vbCrLf & " if aParentRowID is null then"
                                    s = s & vbCrLf & "   select Count(*) into  aUniqueRowCount from " & checkTable & " ParentStructRowID=g2b(aParentStructRowID) and ParentRowID is null " & z & ";"
                                    s = s & vbCrLf & " else "
                                    s = s & vbCrLf & "   select Count(*) into  aUniqueRowCount from " & checkTable & " ParentStructRowID=g2b(aParentStructRowID) and ParentRowID =g2b(aParentRowID) " & z & ";"
                                    s = s & vbCrLf & " end if;"
                                End If
                            Else

                                If TypeName(os.Parent.Parent) = "Application" Then
                                    s = s & vbCrLf & " select Count(*) into  aUniqueRowCount from " & checkTable & " InstanceID=g2b(aInstanceID) " & z & ";"
                                Else
                                    s = s & vbCrLf & "select Count(*) into  aUniqueRowCount from " & checkTable & " ParentStructRowID=g2b(aParentStructRowID) " & z & ";"
                                End If
                            End If
                        Else
                            If ot.CommitFullObject = bp3doc.bp3doc.enumBoolean.Boolean_Net Then
                                s = s & vbCrLf & "select Count(*) into  aUniqueRowCount from " & os.Name & " where 1=1  " & z & ";"
                            Else
                                s = s & vbCrLf & "select 1 into  aUniqueRowCount;"
                            End If
                        End If

                        If uc.TheComment <> "" Then
                            s = s & vbCrLf & "if aUniqueRowCount>=2 then"
                            s = s & vbCrLf & "  select 'Нарущение уникальности сочетания полей.  Раздел=" & os.Caption & " Правило=(" & uc.TheComment & ")'  result;"
                            s = s & vbCrLf & "  rollback;"
                            s = s & vbCrLf & "  leave body;"
                            s = s & vbCrLf & "end if;"
                        ElseIf uc.Name <> "" Then
                            s = s & vbCrLf & "if aUniqueRowCount>=2 then"
                            s = s & vbCrLf & " select 'Нарущение уникальности сочетания полей.  Раздел=" & os.Caption & " Правило=(" & uc.Name & ")' result;"
                            s = s & vbCrLf & "  rollback;"
                            s = s & vbCrLf & "  leave body;"
                            s = s & vbCrLf & "end if;"
                        Else
                            s = s & vbCrLf & "if aUniqueRowCount>=2 then"
                            s = s & vbCrLf & " select 'Нарущение уникальности сочетания полей. Раздел=" & os.Caption & "' result;"
                            s = s & vbCrLf & "  rollback;"
                            s = s & vbCrLf & "  leave body;"
                            s = s & vbCrLf & "end if;"
                        End If

                    End If
                End If
            Next

            s = s & vbCrLf & " -- end if;"
        End If

        UniqueCheck = s

        Exit Function
bye:
        Console.WriteLine("ERROR-" & Err.Description & "<--ERROR")
        'Resume
    End Function






    Private Function MapPartView() As String

        Dim i As Integer
        Dim dt_qry As DataTable
        Dim qry As bp3qry.bp3qry.Application
        Dim os As bp3doc.bp3doc.bp3doc_store
        Dim s As String = ""
        dt_qry = man.Session.GetData("select " + man.Session.GetProvider().ID2Base("instanceid", "instanceid") + " from bp3qry_def ")


        For i = 0 To dt_qry.Rows.Count - 1
            qry = man.GetInstanceObject(New Guid(dt_qry.Rows(i)("instanceid").ToString()))
            Try

                If qry.bp3qry_def.Item(1).ForChoose = bp3doc.bp3doc.enumBoolean.Boolean_Da Then
                    os = qry.bp3qry_def.Item(1).SrcPart
                    s = s & vbCrLf & "set aid = g2b('" & GetMap(os.Name & "_DEFVIEW") & "');"
                    s = s & vbCrLf & "call SysOptions_SAVE ( b2g(aid), '" & os.Name & "', '" & qry.bp3qry_def.Item(1).the_Alias & "', 'DEFVIEW');"
                    Exit For
                End If

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Next






        MapPartView = s

    End Function


    Private Function MapAndParent(ByRef os As bp3doc.bp3doc.bp3doc_store) As String
        If os.PartType = bp3doc.bp3doc.enumPartType.PartType_Rassirenie Then
            Return ""
        End If
        Dim s As String = ""
        Dim tn As String
        tn = TypeForStruct(os).Name
        Try
            s = s & vbCrLf & "set aid = G2B('" & GetMap(os.Name & "_structtype") & "');"
            s = s & vbCrLf & "call SysOptions_SAVE ( b2g(aid), '" & os.Name & "', '" & tn & "', 'STRUCT_TYPE');"


            If TypeName(os.Parent.Parent) <> "Application" Then
                s = s & vbCrLf & "set aid = G2B('" & GetMap(os.Name & "_PARENT") & "');"
                s = s & vbCrLf & "call SysOptions_SAVE(  b2g(aid), '" & os.Name & "', '" & CType(os.Parent.Parent, bp3doc_store).Name & "', 'PARENT');"
            End If
        Catch ex As System.Exception
            MsgBox(ex.Message)
        End Try




        Dim i As Integer


        For i = 1 To os.bp3doc_store.Count
            If Not os.bp3doc_store.Item(i).PartType = bp3doc.bp3doc.enumPartType.PartType_Rassirenie Then
                s = s & vbCrLf & MapAndParent(os.bp3doc_store.Item(i))
            End If
        Next

        MapAndParent = s

    End Function


    Private Function MapViews(ByRef pv As bp3qry.bp3qry.Application) As String
        Dim s As String
        s = ""
        s = s & vbCrLf & "set aid = G2B('" & GetMap(pv.bp3qry_def.Item(1).the_Alias & "_map") & "');"
        s = s & vbCrLf & "call SysOptions_SAVE(  b2g(aid), '" & pv.ID.ToString() & "', aValue='V_" & pv.bp3qry_def.Item(1).the_Alias & "', 'MAP');"
        MapViews = s
    End Function


    Private Function TypeForStruct(ByVal s As bp3doc.bp3doc.bp3doc_store) As bp3doc.bp3doc.bp3doc_def
        Dim app As bp3doc.bp3doc.Application
        app = s.Application
        TypeForStruct = app.bp3doc_def.Item(1)
    End Function


    Private Sub LoadOptions()

        Dim s As Writer
        s = New Writer
        System.Windows.Forms.Application.DoEvents()
        'Dim os As bp3doc.bp3doc.bp3doc_store
        Dim i As Integer
        Dim j As Integer
        s.putBufLC("drop procedure if exists Init;")
        s.putBufLC("GO")
        s.putBufLC("create procedure Init() begin")
        s.putBufLC("declare aid BINARY(16);")
        s.putBufLC("declare ainstid BINARY(16);")
        s.putBufLC("declare auid BINARY(16);")
        s.putBufLC("declare aSESSION varchar(38);")
        s.putBufLC("declare acid BINARY(16);")
        s.putBufLC("declare asecid BINARY(16);")
        s.putBufLC("declare ahid BINARY(16);")
        s.putBufLC("declare atmpstr varchar(255);")
        s.putBufLC("declare aEC int;")


        s.putBufLC("select count(*) into aEC from instance where objtype='MTZSYSTEM';")
        s.putBufLC("if aEC=0 then")
        s.putBufLC("    set ainstid = G2B('" & GetMap("MTSYSTEMID") & "');")
        s.putBufLC("    insert into instance(InstanceID,OBJTYPE,Name,changestamp) values(ainstid, 'MTZSYSTEM','Системная информация',now());")
        s.putBufLC("else")
        s.putBufLC("    select InstanceID into ainstid from instance where objtype='MTZSYSTEM';")
        s.putBufLC("end if;")
        s.putBufLC("set auid = G2B('" & GetMap("inituser") & "'); ")
        s.putBufLC("set asecid = G2B('" & GetMap("secid") & "'); ")
        s.putBufLC("set ahid = G2B('" & GetMap("helper") & "'); ")
        s.putBufLC("select count(*) into aEC from users where usersid=auid;")
        s.putBufLC("if aEC=0 then")
        s.putBufLC("insert into users(usersid,instanceid,login,password) values(auid,null,'init',md5('init'));")
        s.putBufLC("end if;")



        For i = 1 To m.bp3doc_def.Count
            s.putBufLC("select count(*) into aEC from typelist where name='" & m.bp3doc_def.Item(i).Name & "';")
            s.putBufLC("if aEC=0 then")
            s.putBufLC("insert into typelist(typelistid, name,RegisterProc,DeleteProc, HCLProc, propagateProc) values(G2B(UUID()),'" & m.bp3doc_def.Item(i).Name & "', '" & m.bp3doc_def.Item(i).Name & "_REGISTER', '" & m.bp3doc_def.Item(i).Name & "_DELETE', '" & m.bp3doc_def.Item(i).Name & "_HCL', '" & m.bp3doc_def.Item(i).Name & "_propagate');")
            s.putBufLC("end if;")
        Next


        For i = 0 To dt_ot.Rows.Count - 1
            m = man.GetInstanceObject(New Guid(dt_ot.Rows(i)("instanceid").ToString()))

            'If Not m.bp3doc_def.Item(1). Is Nothing Then
            '    s.putBufLC("set aid = G2B('" & GetMap(m.bp3doc_def.Item(i).Name & "_TDEFVIEW") & "');")

            '    s.putBufLC("call SysOptions_SAVE(b2g(aid), '" & m.bp3doc_def.Item(i).Name & "', '" & CType(m.bp3doc_def.Item(i).ChooseView, PARTVIEW).the_Alias & "', 'TDEFVIEW');")
            'End If

            For j = 1 To m.bp3doc_store.Count
                s.putBufLC(MapAndParent(m.bp3doc_store.Item(j)))

            Next
        Next
        s.putBufLC(MapPartView())

        'For i = 1 To m.SHAREDMETHOD.Count
        '    s.putBufLC("set aid = G2B('" & GetMap(m.SHAREDMETHOD.Item(i).Name & "_METHOD") & "');")
        '    s.putBufLC("call SysOptions_SAVE(b2g(aid),'" & m.SHAREDMETHOD.Item(i).ID.ToString() & "','" & m.SHAREDMETHOD.Item(i).Name & "', 'METHODNAME');")
        'Next


        s.putBufLC("call Login( asession  , 'init', 'init');")


        s.putBufLC("select count(*) into aEC from instance where objtype='MTZUsers';")
        s.putBufLC("if aEC=0 then")
        s.putBufLC("   set asecid=G2B('" & GetMap("MTZUsers") & "');")
        s.putBufLC("   insert into instance(InstanceID,OBJTYPE,Name) values(asecid, 'MTZUsers','Пользователи и группы');")
        s.putBufLC("else")
        s.putBufLC("   select InstanceID into asecid from instance where objtype='MTZUsers';")
        s.putBufLC("end if;")



        ' create new user
        s.putBufLC("delete from users where login = 'supervisor';")
        s.putBufLC("set auid=G2B('" & GetMap("supervisor") & "');")
        s.putBufLC("insert into users(usersid,instanceid,password,login,name,changestamp) values(auid, asecid,  ")
        s.putBufLC(" md5('bami'),  'supervisor', 'Администратор',now());")


        s.putBufLC("call Logout(asession);")

        s.putBufLC("delete from users where login = 'init';")
        s.putBufLC("end")
        s.putBufLC("GO")
        s.putBufLC("call Init();")
        s.putBufLC("GO")
        ' s.putBufLC("drop procedure if exists Init();")
        's.putBufLC("GO")


        o.ModuleName = "--Options"
        o.Block = "--Load"
        o.OutNL(s.getBuf)

        s = Nothing
    End Sub




    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' UTILS
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' return phisical type for .bp3ft_def
    Private Function MapFT(ByVal typeID As String) As String
        Dim s As Object
        Dim ft As bp3ft.bp3ft.bp3ft_def

        On Error Resume Next
        Dim dt_ft As DataTable
        dt_ft = man.Session.GetData("select " + man.Session.GetProvider.InstanceFieldList() + " from instance where objtype='bp3ft'")

        If ftmap Is Nothing Then
            ftmap = New Collection
            Dim r As Integer
            Dim i As Integer
            Dim ftapp As bp3ft.bp3ft.Application
            For r = 0 To dt_ft.Rows.Count - 1
                ftapp = man.GetInstanceObject(New Guid(dt_ft.Rows(r)("instanceid").ToString()))
                For i = 1 To ftapp.bp3ft_map.Count
                    If ftapp.bp3ft_map.Item(i).Target.ID.Equals(New Guid(tid)) Then
                        ftmap.Add(ftapp.bp3ft_map.Item(i), ftapp.bp3ft_def.Item(1).ID.ToString())
                    End If
                Next
            Next
        End If

        If ftmap.Item(typeID) Is Nothing Then
        Else
            s = ftmap.Item(typeID).StoageType
            If ftmap.Item(typeID).FixedSize <> 0 Then
                s = s & vbCrLf & " (" & ftmap.Item(typeID).FixedSize & ")"
            End If
            Return s

        End If



        MapFT = "INTEGER"


        Return "INTEGER"
bye:
        Console.WriteLine("ERROR-" & Err.Description & "<--ERROR")

    End Function


    Private Function MapFTObj(ByVal typeID As String) As bp3ft_map
        On Error Resume Next

        Dim rp As bp3doc.bp3doc.bp3doc_store
        Dim ft As bp3ft.bp3ft.bp3ft_def
        On Error Resume Next
        Dim dt_ft As DataTable
        dt_ft = man.Session.GetData("select " + man.Session.GetProvider.InstanceFieldList() + " from instance where objtype='bp3ft'")

        If ftmap Is Nothing Then
            ftmap = New Collection
            Dim r As Integer
            Dim i As Integer
            Dim ftapp As bp3ft.bp3ft.Application
            For r = 0 To dt_ft.Rows.Count - 1
                ftapp = man.GetInstanceObject(New Guid(dt_ft.Rows(r)("instanceid").ToString()))
                For i = 1 To ftapp.bp3ft_map.Count
                    If ftapp.bp3ft_map.Item(i).Target.ID.Equals(New Guid(tid)) Then
                        ftmap.Add(ftapp.bp3ft_map.Item(i), ftapp.bp3ft_def.Item(1).ID.ToString())
                    End If
                Next
            Next
        End If
        Dim obj As Object
        obj = ftmap.Item(typeID)
        If obj Is Nothing Then
            Return Nothing
        Else
            Return ftmap.Item(typeID)
            Exit Function
        End If


bye:
        Console.WriteLine("ERROR-" & Err.Description & "<--ERROR")

    End Function




    '    Private Function GetScript(ByRef scol As MTZMetaModel.MTZMetaModel.SCRIPT_col) As String
    '        Dim i As Integer
    '        GetScript = ""
    '        On Error GoTo bye
    '        For i = 1 To scol.Count
    '            If scol.Item(i).Target.ID.Equals(New Guid(tid)) Then
    '                GetScript = scol.Item(i).Code
    '                Exit Function
    '            End If
    '        Next
    '        Exit Function
    'bye:
    '        console.writeline( "ERROR-" & Err.Description & "<--ERROR")


    '    End Function


    '    Private Function GetParameters(ByRef scol As MTZMetaModel.MTZMetaModel.SCRIPT_col) As MTZMetaModel.MTZMetaModel.PARAMETERS_col
    '        Dim i As Integer

    '        On Error GoTo bye
    '        For i = 1 To scol.Count
    '            If scol.Item(i).Target.ID.Equals(New Guid(tid)) Then
    '                Return scol.Item(i).PARAMETERS

    '            End If
    '        Next
    '        Return Nothing

    'bye:
    '        console.writeline( "ERROR-" & Err.Description & "<--ERROR")
    '        Return Nothing
    '    End Function



    Private Function MakeName(ByVal s As String) As String
        Dim tt As String
        tt = s
        tt = Replace(tt, "-", "")
        tt = Replace(tt, "{", "")
        tt = Replace(tt, "}", "")
        tt = Replace(tt, " ", "_")
        MakeName = tt
    End Function




    Private Sub CreateBriefProc(ByRef os As bp3doc.bp3doc.bp3doc_store)



        If os.PartType = bp3doc.bp3doc.enumPartType.PartType_Rassirenie Then
            Exit Sub
        End If

        Dim st As bp3doc.bp3doc.bp3doc_store
        st = os
        Dim chos As bp3doc.bp3doc.bp3doc_store
        Dim i, j As Integer
        Dim f As bp3doc.bp3doc.bp3doc_field
        Dim s As Writer
        s = New Writer
        CreateBriefFunc(os)
        CreateMultirefFunc(os)
        Console.WriteLine("-CreateBriefProc " & os.Name)
        On Error GoTo bye

        s.putBufLC(procDropSQL(os.Name & "_BRIEF"))
        s.putBufLC(Delimiter())
        s.putBufLC("create procedure " & os.Name & "_BRIEF  (")
        s.putBufLC(" acursession varchar(38),")
        s.putBufLC(" a" & os.Name & "id varchar(38),")
        s.putBufLC("out aBRIEF varchar(255)")
        s.putBufLC(") body: begin  ")
        s.putBufLC(" declare aaccess int;")
        s.putBufLC(" declare atmpStr varchar(255);")
        s.putBufLC(" declare atmpID BINARY(16);")
        s.putBufLC(" declare aEC int;")
        s.putBufLC(" declare aLang2 varchar(25);")
        s.putBufLC(" select count(*) into aEC  from the_session where the_sessionid=g2b(acursession) and closed=0 ;")
        s.putBufLC(" if aEC=0 then")
        s.putBufLC("     set aBRIEF=''; leave body;")
        s.putBufLC("  end if;")
        s.putBufLC(" select Lang into aLang2 from the_session where the_sessionid=g2b(acursession);")
        s.putBufLC(" if a" & os.Name & "id is null then set aBRIEF=''; leave body; end if;")
        s.putBufLC(" select  count(*) into aEC from " & os.Name & " where " & os.Name & "ID=g2b(a" & os.Name & "ID);")
        s.putBufLC(" if  aEC >0 then")
        s.putBufLC("   set aBRIEF=" & os.Name & "_BRIEF_F(g2b(a" & os.Name & "id), aLang2);")
        s.putBufLC(" else")
        s.putBufLC("   set aBRIEF= 'неверный идентификатор';")
        s.putBufLC(" end if;")
        s.putBufLC(" set aBRIEF=left(aBRIEF,255);")
        s.putBufLC("end ")

        s.putBufLC("GO")

        o.ModuleName = "--Procedures"
        o.Block = "--TableProc"
        o.OutNL(s.getBuf)
        o.OutNL("GO")
        s = Nothing
        Exit Sub
bye:
        Console.WriteLine("ERROR-" & Err.Description & "<--ERROR")
        s = Nothing
    End Sub



    Private Sub CreateOwnershipProc(ByRef ot As bp3doc.bp3doc.bp3doc_def)

        If ot.UseOwnership = bp3doc.bp3doc.enumBoolean.Boolean_Net Then
            Return
        End If


        Dim i, j As Integer

        Dim s As Writer
        s = New Writer

        Console.WriteLine("--CreateOwnershipProc " & ot.Name)
        On Error GoTo bye

        s.putBufLC(procDropSQL(ot.Name & "_INITOWNERSHIP"))
        s.putBufLC(Delimiter())
        s.putBufLC("create procedure " & ot.Name & "_INITOWNERSHIP  (")
        s.putBufLC(" acursession varchar(38),")
        s.putBufLC(" ainstanceid varchar(38)")
        s.putBufLC(") body: begin  ")
        s.putBufLC(" declare aEC int;")
        s.putBufLC(" select count(*) into aEC  from the_session where the_sessionid=g2b(acursession) and closed=0 ;")
        s.putBufLC(" if aEC=0 then")
        s.putBufLC("     leave body;")
        s.putBufLC("  end if;")

        's.putBufLC("  select count(*) into aec from " + ot.Name + "_agents where ")
        's.putBufLC("  instanceid=g2b(ainstanceid);")
        's.putBufLC("  if aEC = 0 then")
        's.putBufLC("      insert into " + ot.Name + "_agents(instanceid," + ot.Name + "_agentsid,TheAgent)")
        's.putBufLC("      select g2b(ainstanceid),g2b(uuid()),busr_defid from the_session join users on the_session.usersid=users.usersid")
        's.putBufLC("      join busr_def on users.login=busr_def.loginname where b2g(the_sessionid)=acursession;")
        's.putBufLC("   end if;")


        s.putBufLC("end ")

        s.putBufLC("GO")

        o.ModuleName = "--Procs"
        o.Block = "--body"
        o.OutNL(s.getBuf)
        o.OutNL("GO")
        s = Nothing
        Exit Sub
bye:
        Console.WriteLine("ERROR-" & Err.Description & "<--ERROR")
        s = Nothing
    End Sub

    Private delimiterChar As String = ""
    Private Sub CreateBriefFunc(ByRef os As bp3doc.bp3doc.bp3doc_store)
        If os.PartType = bp3doc.bp3doc.enumPartType.PartType_Rassirenie Then
            Exit Sub
        End If

        Dim st As bp3doc.bp3doc.bp3doc_store
        st = os
        Dim chos As bp3doc.bp3doc.bp3doc_store
        Dim i, j As Integer
        Dim f As bp3doc.bp3doc.bp3doc_field
        Dim eq As String
        Dim s As Writer
        s = New Writer

        ' делаем отдельно заголовки функций
        CreateBriefFuncHdr(os)




        Console.WriteLine("-CreateBriefFunc " & os.Name)

        On Error GoTo bye

        s.putBufLC("")
        s.putBufLC(funcDropSQL(os.Name & "_BRIEF_F"))
        s.putBufLC(Delimiter())
        s.putBufLC("create function " & os.Name & "_BRIEF_F  (")

        's.putBufLC(" a" & os.Name & "id varchar(38)")
        s.putBufLC(" a" & os.Name & "id binary(16)")
        'eq = "g2b(a" & os.Name & "ID)"
        eq = "a" & os.Name & "ID"


        'MLF
        s.putBufLC(" ,aLang varchar(25)")
        'EMLF
        s.putBufLC(") returns varchar(255) READS SQL DATA " & " begin  ")
        s.putBufLC(" declare aBRIEF varchar(255);")
        s.putBufLC(" declare atmpStr varchar(255);")
        s.putBufLC(" declare atmpBrief varchar(255);")
        s.putBufLC(" declare atmpID BINARY(16);")
        s.putBufLC(" declare atmpMR varchar(255); ")
        'MLF
        s.putBufLC(" declare aMLFTemp varchar(255);")
        s.putBufLC(" declare aMLFBrief varchar(255);")

        'EMLF
        s.putBufLC(" declare aEC int;")

        s.putBufLC("if a" & os.Name & "id is null then  set aBRIEF=''; return aBRIEF; end if;")

        's.putBufLC(" -- Brief body -- ")
        s.putBufLC("select count(*) into aEC from " & os.Name & " where " & os.Name & "ID=" & eq & ";")
        s.putBufLC("if aEC<>0 then")
        s.putBufLC("  set aBRIEF='';")

        st.bp3doc_field.Sort = "sequence"
        Dim arr() As String
        Dim sh As String
        Dim ft As bp3ft_def
        For i = 1 To st.bp3doc_field.Count
            ft = st.bp3doc_field.Item(i).fieldType
            If (ft.TypeStyle <> bp3doc.bp3doc.enumTypeStyle.TypeStyle_Element_oformleniy) Then
                If st.bp3doc_field.Item(i).IsBrief = bp3doc.bp3doc.enumBoolean.Boolean_Da Then
                    f = st.bp3doc_field.Item(i)

                    On Error Resume Next
                    sh = f.shablonBrief
                    If sh = "" Then
                        ReDim arr(1)
                    Else
                        sh = Replace(sh, "'", "")
                        arr = Split(sh, "%%")
                        ReDim Preserve arr(1)
                    End If

                    'enum

                    If ft.TypeStyle = bp3doc.bp3doc.enumTypeStyle.TypeStyle_Perecislenie Then
                        s.putBufLC(" select " & f.Name)
                        s.putBufLC(" into aEC  from " & os.Name & " where " & os.Name & "ID=" & eq & ";")
                        s.putBufLC("  case aEC ")
                        For j = 1 To GetEnums(ft).Count

                            s.putBufLC("when " & GetEnums(ft).Item(j).NameValue & " then ")
                            s.putBufLC("  select concat(aBRIEF ,")
                            s.putBufLC(" '" & arr(0) & GetEnums(ft).Item(j).Name & arr(1) & "; ') into aBRIEF ;")


                        Next

                        s.putBufLC(" else ")
                        s.putBufLC("  select concat(aBRIEF ,")
                        s.putBufLC(" '; ') into aBRIEF ;")
                        s.putBufLC("  end case; ")


                    ElseIf ft.TypeStyle = bp3doc.bp3doc.enumTypeStyle.TypeStyle_Ssilka Then
                        If ft.Name = "MULTIREF" Then

                            If f.ReferenceType = bp3doc.bp3doc.enumReferenceType.ReferenceType_Na_ob_ekt_ Then
                                s.putBufLC("select " & f.Name)
                                s.putBufLC(" into atmpMR  from " & os.Name & "  where  " & os.Name & "ID = " & eq & "; ")
                                s.putBufLC("BEGIN")
                                s.putBufLC("DECLARE fetch_done INT DEFAULT FALSE;")
                                s.putBufLC("declare multiref_cursor cursor for")
                                's.putBufLC("select INSTANCE_BRIEF_F(b2g(InstanceID), aLang) from Instance")
                                s.putBufLC("select INSTANCE_BRIEF_F(InstanceID, aLang) from Instance")
                                s.putBufLC("where atmpMR like concat('%',B2G(InstanceID),'%');")
                                s.putBufLC(" DECLARE CONTINUE HANDLER FOR NOT FOUND SET fetch_done = TRUE;")
                                s.putBufLC("set atmpMR='';")
                                s.putBufLC("open multiref_cursor;")
                                s.putBufLC("  set  fetch_done=false;")
                                s.putBufLC("fetch multiref_cursor into atmpBrief;")
                                s.putBufLC("while not fetch_done DO ")
                                s.putBufLC("    if atmpMR<>'' then ")
                                s.putBufLC("        set atmpMR=concat(atmpMR,'" + delimiterChar + " ');")
                                s.putBufLC("    end if;")
                                s.putBufLC("    set atmpMR=concat(atmpMR,atmpBrief);")
                                s.putBufLC("  set  fetch_done=false;")
                                s.putBufLC("    fetch multiref_cursor into atmpBrief;")
                                s.putBufLC("End while;")
                                s.putBufLC("")
                                s.putBufLC("Close multiref_cursor;")
                                's.putBufLC("deallocate multiref_cursor")

                                s.putBufLC("  set aBRIEF= concat(aBRIEF, '" & arr(0) & "' , IFNULL(atmpbrief,'') , '" & arr(1) + delimiterChar + " ');")
                                s.putBufLC("END;")
                            End If


                            If f.ReferenceType = bp3doc.bp3doc.enumReferenceType.ReferenceType_Na_stroku_razdela Then
                                s.putBufLC("select " & f.Name)
                                s.putBufLC(" into atmpMR from " & os.Name & "  where  " & os.Name & "ID = " & eq & "; ")
                                s.putBufLC("BEGIN")
                                s.putBufLC("DECLARE fetch_done INT DEFAULT FALSE;")
                                s.putBufLC("declare multiref_cursor cursor for")
                                's.putBufLC("select " & CType(f.RefToPart, bp3doc_store).Name & "_BRIEF_F(b2g(" + CType(f.RefToPart, bp3doc_store).Name + "ID), aLang)  from " + CType(f.RefToPart, bp3doc_store).Name)
                                s.putBufLC("select " & CType(f.RefToPart, bp3doc_store).Name & "_BRIEF_F(" + CType(f.RefToPart, bp3doc_store).Name + "ID, aLang)  from " + CType(f.RefToPart, bp3doc_store).Name)
                                s.putBufLC("where atmpMR like concat('%',B2G(" + CType(f.RefToPart, bp3doc_store).Name + "ID),'%');")
                                s.putBufLC(" DECLARE CONTINUE HANDLER FOR NOT FOUND SET fetch_done = TRUE;")
                                s.putBufLC("set atmpMR='';")
                                s.putBufLC("open multiref_cursor;")
                                s.putBufLC("  set  fetch_done=false;")
                                s.putBufLC("fetch multiref_cursor into atmpBrief;")
                                s.putBufLC("while not fetch_done DO ")
                                s.putBufLC("    if atmpMR<>'' then")
                                s.putBufLC("        set atmpMR=concat(atmpMR,'" + delimiterChar + " ');")
                                s.putBufLC("    end if;")
                                s.putBufLC("    set atmpMR=concat(atmpMR,atmpBrief);")
                                s.putBufLC("  set  fetch_done=false;")
                                s.putBufLC("    fetch multiref_cursor into atmpBrief;")
                                s.putBufLC("End while;")
                                s.putBufLC("Close multiref_cursor;")
                                's.putBufLC("deallocate multiref_cursor")
                                s.putBufLC("  set aBRIEF= concat(aBRIEF , '" & arr(0) & "' , IFNULL(atmpbrief,' ') , '" & arr(1) & "" + delimiterChar + " ');")
                                s.putBufLC("END;")
                            End If

                        Else
                            s.putBufLC("select " & f.Name)
                            s.putBufLC(" into atmpID  from " & os.Name & "  where  " & os.Name & "ID = " & eq & "; ")

                            If f.ReferenceType = bp3doc.bp3doc.enumReferenceType.ReferenceType_Na_ob_ekt_ Then
                                'MLF
                                's.putBufLC(" select INSTANCE_BRIEF_F( b2g(atmpID), aLang) into atmpBrief;")
                                s.putBufLC(" select INSTANCE_BRIEF_F( atmpID, aLang) into atmpBrief;")
                            End If


                            If f.ReferenceType = bp3doc.bp3doc.enumReferenceType.ReferenceType_Na_stroku_razdela Then
                                'MLF

                                s.putBufLC(" select " & CType(f.RefToPart, bp3doc_store).Name & "_BRIEF_F(atmpID, aLang) into atmpBrief;")
                                's.putBufLC(" select " & CType(f.RefToPart, bp3doc_store).Name & "_BRIEF_F(b2g(atmpID), aLang) into atmpBrief;")
                            End If

                            If f.ReferenceType = bp3doc.bp3doc.enumReferenceType.ReferenceType_Na_istocnik_dannih Then
                                s.putBufLC("select substring(" & f.Name & ",PATINDEX('%<Brief>%'," & f.Name & ")+7, PATINDEX('%</Brief>%'," & f.Name & ") -PATINDEX('%<Brief>%'," & f.Name & ")-7) into atmpBrief from " & st.Name & " where  " & os.Name & "ID=" & eq & ";")
                            End If

                            s.putBufLC("  set aBRIEF= concat(aBRIEF , '" & arr(0) & "' , IFNULL(atmpbrief,'') , '" & arr(1) & "" + delimiterChar + " ');")
                        End If
                        'MLF
                    ElseIf IsMLFfield(f) Then
                        s.putBufLC("set aMLFBrief='';")
                        's.putBufLC("if not aLang is null then")
                        's.putBufLC("  set aMLFTemp='select " & st..bp3doc_field.Item(i).Name & " into aMLFTemp2 from " & os.Name & "_'+aLang+' where " & os.Name & "ID=a" & os.Name & "ID';")

                        's.putBufLC("  call sp_executesql aMLFTemp,N'a" & os.Name & "ID BINARY(16),aMLFBrief varchar(255) output',a" & os.Name & "ID, aMLFBrief output;")

                        's.putBufLC("End if;")

                        s.putBufLC("  select concat( aBRIEF ")
                        s.putBufLC("  ,  IFNULL(aMLFBrief, concat(IFNULL( " & st.bp3doc_field.Item(i).Name & ",' '),'" + delimiterChar + " ')) )")
                        s.putBufLC(" into aBRIEF  from " & os.Name & "  where  " & os.Name & "ID = " & eq & "; ")
                        'EMLF


                    Else
                        s.putBufLC("  select concat(aBRIEF ")
                        s.putBufLC("  , '" & arr(0) & "' , IFNULL(" & st.bp3doc_field.Item(i).Name & ",'') ,'" & arr(1) & "" + delimiterChar + " '  )")
                        s.putBufLC("  into aBRIEF   from " & os.Name & "  where  " & os.Name & "ID = " & eq & "; ")
                    End If
                End If
            End If
        Next
        s.putBufLC("else")
        s.putBufLC("  set aBRIEF= '';")
        s.putBufLC("end if;")
        s.putBufLC("return aBRIEF;")
        s.putBufLC("end  ")
        Debug.Print(os.Name)

        o.ModuleName = "--Functions"
        o.Block = "--TableProc"
        o.OutNL(s.getBuf)
        o.OutNL("GO")
        s = Nothing
        Exit Sub
bye:
        Console.WriteLine("ERROR-" & Err.Description & "<--ERROR")
        'Resume
        s = Nothing
    End Sub

    Private Sub CreateInstMultirefFuncHdr()

        Dim i, j As Integer
        Dim s As String


        Console.WriteLine("-CreateInstMultirefFuncHdr ")

        On Error GoTo bye

        s = ""
        s = s & vbCrLf & funcDropSQL("INSTANCE_MREF_F")
        s = s & vbCrLf & Delimiter()
        s = s & vbCrLf & "create function INSTANCE_MREF_F  ("
        s = s & vbCrLf & " aINSTANCE_ref varchar(255)"
        'MLF
        s = s & vbCrLf & " ,aLang varchar(25)"
        'EMLF
        s = s & vbCrLf & ") returns varchar(255)  "
        s = s & vbCrLf & "DETERMINISTIC"
        s = s & vbCrLf & " begin  "
        s = s & vbCrLf & " declare aMREF varchar(255);"
        s = s & vbCrLf & "  set aMREF='to do';"

        s = s & vbCrLf & "return aMREF;"
        s = s & vbCrLf & "end "

        o.ModuleName = "--FunctionsHeader"
        o.Block = "--TableProc"
        o.OutNL(s)
        o.OutNL("GO")


        Exit Sub
bye:
        Console.WriteLine("ERROR-" & Err.Description & "<--ERROR")

    End Sub

    Private Sub CreateMultirefFuncHdr(ByRef os As bp3doc.bp3doc.bp3doc_store)

        If os.PartType = bp3doc.bp3doc.enumPartType.PartType_Rassirenie Then
            Exit Sub
        End If

        Dim st As bp3doc.bp3doc.bp3doc_store
        st = os
        Dim chos As bp3doc.bp3doc.bp3doc_store
        Dim i, j As Integer
        Dim f As bp3doc.bp3doc.bp3doc_field
        Dim s As String


        Console.WriteLine("-CreateMultirefFuncHdr " & os.Name)

        On Error GoTo bye

        s = ""
        s = s & vbCrLf & funcDropSQL(os.Name & "_MREF_F")
        s = s & vbCrLf & "create function " & os.Name & "_MREF_F  ("
        s = s & vbCrLf & " a" & os.Name & "_ref varchar(255)"
        'MLF
        s = s & vbCrLf & " ,aLang varchar(25)"
        'EMLF
        s = s & vbCrLf & ") returns varchar(255)  DETERMINISTIC begin  "
        s = s & vbCrLf & " declare aMREF varchar(255);"
        s = s & vbCrLf & "  set aMREF='to do';"

        s = s & vbCrLf & "return aMREF;"
        s = s & vbCrLf & "end "
        Debug.Print(os.Name)

        o.ModuleName = "--FunctionsHeader"
        o.Block = "--TableProc"
        o.OutNL(s)
        o.OutNL("GO")


        Exit Sub
bye:
        Console.WriteLine("ERROR-" & Err.Description & "<--ERROR")

    End Sub
    Private Sub CreateMultirefFunc(ByRef os As bp3doc.bp3doc.bp3doc_store)
        If os.PartType = bp3doc.bp3doc.enumPartType.PartType_Rassirenie Then
            Exit Sub
        End If

        Dim st As bp3doc.bp3doc.bp3doc_store
        st = os
        'Dim chos As bp3doc.bp3doc.bp3doc_store
        'Dim i, j As Integer
        'Dim f As bp3doc.bp3doc.bp3doc_field
        Dim s As Writer
        s = New Writer

        ' делаем отдельно заголовки функций
        CreateMultirefFuncHdr(os)


        Console.WriteLine("-CreateMRefFunc " & os.Name)

        s.putBufLC("")
        s.putBufLC(funcDropSQL(os.Name & "_MREF_F"))
        s.putBufLC(Delimiter())
        s.putBufLC("create function " & os.Name & "_MREF_F  (")
        s.putBufLC(" a" & os.Name & "_ref varchar(255)")
        'MLF
        s.putBufLC(" ,aLang varchar(25)")
        'EMLF
        s.putBufLC(") returns varchar(255) READS SQL DATA " & " begin  ")


        s.putBufLC(" declare aMREF varchar(255);")
        s.putBufLC(" declare atmpBrief varchar(255);")
        s.putBufLC(" DECLARE fetch_done INT DEFAULT FALSE;")


        s.putBufLC("declare multiref_cursor cursor for")
        's.putBufLC("select " & os.Name & "_BRIEF_F(b2g(" + os.Name + "ID), aLang)  from " + os.Name)
        s.putBufLC("select " & os.Name & "_BRIEF_F(" + os.Name + "ID, aLang)  from " + os.Name)
        s.putBufLC("where a" & os.Name & "_ref like concat('%',REPLACE(REPLACE(B2G(" + os.Name + "ID),'{',''),'}',''),'%');")
        s.putBufLC(" DECLARE CONTINUE HANDLER FOR NOT FOUND SET fetch_done = TRUE;")

        s.putBufLC("set aMREF='';")
        s.putBufLC("open multiref_cursor;")
        s.putBufLC("  set  fetch_done=false;")
        s.putBufLC("fetch multiref_cursor into atmpBrief;")
        s.putBufLC("while not fetch_done DO ")
        s.putBufLC("    if aMREF<>'' then")
        s.putBufLC("        set aMREF=concat(aMREF,',');")
        s.putBufLC("    end if;")
        s.putBufLC("    set aMREF=concat(aMREF,atmpBrief);")
        s.putBufLC("  set  fetch_done=false;")
        s.putBufLC("    fetch multiref_cursor into atmpBrief;")
        s.putBufLC("End while;")
        s.putBufLC("Close multiref_cursor;")
        's.putBufLC("deallocate multiref_cursor;")
        s.putBufLC("set aMREF=left(aMREF,255);")
        s.putBufLC("return aMREF;")
        s.putBufLC("end ")

        o.ModuleName = "--Functions"
        o.Block = "--TableProc"
        o.OutNL(s.getBuf)
        o.OutNL("GO")
    End Sub



    Private Sub CreateInstMultirefFunc()
        Dim s As Writer
        s = New Writer

        ' делаем отдельно заголовки функций
        CreateInstMultirefFuncHdr()


        Console.WriteLine("--CreateInstMRefFunc ")

        s.putBufLC(funcDropSQL("INSTANCE_MREF_F"))
        s.putBufLC("create function INSTANCE_MREF_F  (")
        s.putBufLC(" aINSTANCE_ref varchar(255)")
        'MLF
        s.putBufLC(" ,aLang varchar(25)")
        'EMLF
        s.putBufLC(") returns varchar(255) READS SQL DATA " & " begin  ")


        s.putBufLC(" declare aMREF varchar(255);")
        s.putBufLC(" declare atmpBrief varchar(255);")
        s.putBufLC(" DECLARE fetch_done INT DEFAULT FALSE;")
        s.putBufLC("declare multiref_cursor cursor for")
        's.putBufLC("select INSTANCE_BRIEF_F(b2g(INSTANCEID), aLang)  from INSTANCE")
        s.putBufLC("select INSTANCE_BRIEF_F(INSTANCEID, aLang)  from INSTANCE")
        s.putBufLC("where aINSTANCE_ref like concat('%',REPLACE(REPLACE(B2G(INSTANCEID),'{',''),'}',''),'%');")
        s.putBufLC(" DECLARE CONTINUE HANDLER FOR NOT FOUND SET fetch_done = TRUE;")
        s.putBufLC("set aMREF='';")
        s.putBufLC("open multiref_cursor;")
        s.putBufLC("  set  fetch_done=false;")
        s.putBufLC("fetch multiref_cursor into atmpBrief;")
        s.putBufLC("while not fetch_done DO ")
        s.putBufLC("    if aMREF<>'' then")
        s.putBufLC("        set aMREF=concat(aMREF,',');")
        s.putBufLC("    End if;")
        s.putBufLC("    set aMREF=concat(aMREF,atmpBrief);")
        s.putBufLC("  set  fetch_done=false;")
        s.putBufLC("    fetch multiref_cursor into atmpBrief;")
        s.putBufLC("End while;")
        s.putBufLC("Close multiref_cursor;")
        's.putBufLC("deallocate multiref_cursor;")
        s.putBufLC("set aMREF=left(aMREF,255);")
        s.putBufLC("return aMREF;")
        s.putBufLC("end ")

        o.ModuleName = "--Functions"
        o.Block = "--TableProc"
        o.OutNL(s.getBuf)
        o.OutNL("GO")
    End Sub

    Private Sub CreateBriefFuncHdr(ByRef os As bp3doc.bp3doc.bp3doc_store)

        If os.PartType = bp3doc.bp3doc.enumPartType.PartType_Rassirenie Then
            Exit Sub
        End If

        Dim st As bp3doc.bp3doc.bp3doc_store
        st = os
        Dim chos As bp3doc.bp3doc.bp3doc_store
        Dim i, j As Integer
        Dim f As bp3doc.bp3doc.bp3doc_field
        Dim s As String


        Console.WriteLine("-CreateBriefFuncHdr " & os.Name)

        On Error GoTo bye

        s = ""
        s = s & vbCrLf & funcDropSQL(os.Name & "_BRIEF_F")
        s = s & vbCrLf & Delimiter()
        s = s & vbCrLf & "create function " & os.Name & "_BRIEF_F  ("

        s = s & vbCrLf & " a" & os.Name & "id binary(16)"


        'MLF
        s = s & vbCrLf & " ,aLang varchar(25)"
        'EMLF
        s = s & vbCrLf & ") returns varchar(255)  "
        s = s & vbCrLf & " DETERMINISTIC  "
        s = s & vbCrLf & " begin  "
        s = s & vbCrLf & " declare aBRIEF varchar(255);"
        s = s & vbCrLf & "  set aBRIEF='to do';"

        s = s & vbCrLf & "return aBRIEF;"
        s = s & vbCrLf & "end "
        Debug.Print(os.Name)

        o.ModuleName = "--FunctionsHeader"
        o.Block = "--TableProc"
        o.OutNL(s)
        o.OutNL("GO")


        Exit Sub
bye:
        Console.WriteLine("ERROR-" & Err.Description & "<--ERROR")


    End Sub


    Private Sub CreateClientLogic(ByRef os As bp3doc.bp3doc.bp3doc_store)

        If os.PartType = bp3doc.bp3doc.enumPartType.PartType_Rassirenie Then
            Exit Sub
        End If

        Dim st As bp3doc.bp3doc.bp3doc_store
        st = os
        Dim chos As bp3doc.bp3doc.bp3doc_store
        Dim i, j As Integer
        Dim f As bp3doc.bp3doc.bp3doc_field
        Dim s As String


        Console.WriteLine("-CreateClientLogic " & os.Name)

        On Error GoTo bye

        s = ""
        s = s & vbCrLf & procDropSQL(os.Name.ToLower & "_client_trigger")
        s = s & vbCrLf & Delimiter()
        s = s & vbCrLf & "create procedure " & os.Name.ToLower & "_client_trigger  ("
        s = s & vbCrLf & " acursession varchar(38),"

        s = s & vbCrLf & " a" & os.Name & "id varchar(38)"

        s = s & vbCrLf & ") "

        s = s & vbCrLf & " begin  "
        s = s & vbCrLf & "   declare aBRIEF varchar(255);"
        s = s & vbCrLf & "  set aBRIEF='to do';"


        s = s & vbCrLf & "end "
        Debug.Print(os.Name)

        o.ModuleName = "--Procedures"
        o.Block = "--TableProc"
        o.OutNL(s)
        o.OutNL("GO")


        Exit Sub
bye:
        Console.WriteLine("ERROR-" & Err.Description & "<--ERROR")


    End Sub

    Private Function CreateAccessFunc(ByRef ot As bp3doc.bp3doc.bp3doc_def) As String

        Dim s As String

        Console.WriteLine("-CreateAccessFunc " & ot.Name)

        s = ""
        s = s & vbCrLf & funcDropSQL(ot.Name & "_ACCESS_F")
        s = s & vbCrLf & Delimiter()
        s = s & vbCrLf & "create function " & ot.Name & "_ACCESS_F  ("
        s = s & vbCrLf & " aCURSESSION varchar(38)"
        s = s & vbCrLf & ", aINSTANCEid varchar(38)"
        s = s & vbCrLf & ") RETURNS TINYINT(1)  "
        s = s & vbCrLf & "  READS SQL DATA  "
        s = s & vbCrLf & "begin  "
        s = s & vbCrLf & "  declare existsCnt int;  "
        s = s & vbCrLf & "  select count(*) into existsCnt from the_Session "
        s = s & vbCrLf & "  where the_Sessionid =g2b(aCURSESSION)  and closed=0;"
        s = s & vbCrLf & "  If existsCnt > 0 Then"
        s = s & vbCrLf & "     return 1;"
        s = s & vbCrLf & "  else"
        s = s & vbCrLf & "     return 0;"
        s = s & vbCrLf & "  end if;"
        s = s & vbCrLf & " return 1;"
        s = s & vbCrLf & "end"
        s = s & vbCrLf & "GO"
        Debug.Print(ot.Name)

        Return s

    End Function


    Private Function CreateExportFunc(ByRef ot As bp3doc.bp3doc.bp3doc_def) As String

        Dim s As String
        Console.WriteLine("-CreateExportFunc " & ot.Name)
        s = ""
        s = s & vbCrLf & funcDropSQL(ot.Name & "_EXPORT_F")
        s = s & vbCrLf & Delimiter()
        s = s & vbCrLf & "create function " & ot.Name & "_EXPORT_F  ("
        s = s & vbCrLf & " aCURSESSION varchar(38)"
        s = s & vbCrLf & ", aINSTANCEid varchar(38)"
        s = s & vbCrLf & ") RETURNS TINYINT(1)  "
        s = s & vbCrLf & "  READS SQL DATA  "
        s = s & vbCrLf & "begin  "
        s = s & vbCrLf & "  declare existsCnt int;  "
        s = s & vbCrLf & "  select count(*) into existsCnt from the_Session "
        s = s & vbCrLf & "  where the_Sessionid =g2b(aCURSESSION)  and closed=0;"
        s = s & vbCrLf & "  If existsCnt = 0 Then"
        s = s & vbCrLf & "     return 0;"
        s = s & vbCrLf & "  end if;"
        s = s & vbCrLf & " -- export no data by default !!! "
        s = s & vbCrLf & " return 0;"
        s = s & vbCrLf & "end"
        s = s & vbCrLf & "GO"
        Debug.Print(ot.Name)

        Return s

    End Function



    Private Sub CreateTypeProcs(ByRef app As bp3doc.bp3doc.Application)


        Dim obt As bp3doc.bp3doc.bp3doc_def

        obt = app.bp3doc_def.Item(1)

        ' Dim chos As bp3doc.bp3doc.bp3doc_store
        Dim s As Writer
        s = New Writer
        System.Windows.Forms.Application.DoEvents()

        CreateOwnershipProc(obt)

        ' делаем отдельно заголовки функций
        s.putBufLC(CreateAccessFunc(obt))

        s.putBufLC(CreateExportFunc(obt))


        's.putBufLC(procDropSQL(obt.Name & "_DELETE"))
        's.putBufLC("create procedure " & obt.Name & "_DELETE(acursession varchar(38), aInstanceID BINARY(16)) as  ")
        '' delete from root structure of object  - child of instance
        'Dim tos As Short
        's.putBufLC("declare aObjType as varchar(255), achildlistid BINARY(16)")
        's.putBufLC("select  aObjType =objtype from instance where instanceid=ainstanceid")
        's.putBufLC("if  aObjType ='" & obt.Name & "'")
        's.putBufLC("begin")
        'If obt.bp3doc_store.Count > 0 Then
        '    For tos = 1 To obt.bp3doc_store.Count
        '        chos = obt.bp3doc_store.Item(tos)
        '        ''''           If Not chos.PartType = 3 Then
        '        If Not chos.PartType = bp3doc.bp3doc.enumPartType.PartType_Rassirenie Then
        '            s.putBufLC("declare childlist_" & chos.Name & " cursor local for select " & chos.Name & "." & chos.Name & "id from " & chos.Name & " where  " & chos.Name & ".InstanceID = ainstanceid")
        '            s.putBufLC("open childlist_" & chos.Name & "")
        '            s.putBufLC("fetch childlist_" & chos.Name & " into achildlistid")
        '            s.putBufLC("while not fetch_done DO  ")
        '            s.putBufLC("begin")
        '            s.putBufLC(" call " & chos.Name & "_DELETE acursession,achildlistid,aInstanceID")
        '            s.putBufLC(" if aaerror >0 begin")
        '            s.putBufLC("   close childlist_" & chos.Name & "")
        '            s.putBufLC("   goto del_error")
        '            s.putBufLC(" end")
        '            s.putBufLC(" fetch childlist_" & chos.Name & " into achildlistid")
        '            s.putBufLC("end")
        '            s.putBufLC("close childlist_" & chos.Name & "")

        '        End If
        '    Next
        'End If
        's.putBufLC("return")
        's.putBufLC("del_error:")
        's.putBufLC("if aatrancount>0 rollback tran")
        's.putBufLC("end")
        's.putBufLC("GO")





        's.putBufLC(procDropSQL(obt.Name & "_HCL"))
        's.putBufLC("create procedure " & obt.Name & "_HCL(acursession varchar(38), aROWID BINARY(16), aIsLocked integer out) as  ")
        's.putBufLC("declare aObjType as varchar(255)")
        's.putBufLC("declare atmpStr as varchar(255)")

        's.putBufLC(" declare aUserID BINARY(16)")
        's.putBufLC(" declare aLockUserID BINARY(16)")
        's.putBufLC(" declare aLockSessionID BINARY(16)")

        's.putBufLC("select  aObjType =objtype from instance where instanceid=aRowid")
        's.putBufLC("if aobjtype = '" & obt.Name & "'")
        's.putBufLC(" begin")

        'Dim i As Integer
        'If obt.bp3doc_store.Count = 0 Then
        '    s.putBufLC(" set aIsLocked =0")
        'Else
        '    s.putBufLC("if aanestlevel < 25  begin")
        '    '---- проверяем, что нет заблокированных записей в  дочерних разделах
        '    s.putBufLC("declare achildlistid BINARY(16)")
        '    s.putBufLC(" select auserID = usersid from the_session where the_sessionid=acursession")
        '    For i = 1 To obt.bp3doc_store.Count
        '        chos = obt.bp3doc_store.Item(i)
        '        ''''       If Not chos.PartType = 3 Then
        '        If Not chos.PartType = bp3doc.bp3doc.enumPartType.PartType_Rassirenie Then
        '            s.putBufLC("declare lockchild_" & chos.Name & " cursor local for select " & chos.Name & "." & chos.Name & "id from " & chos.Name & " where  " & chos.Name & ".InstanceID = arowid")
        '            s.putBufLC("open lockchild_" & chos.Name & "")
        '            s.putBufLC("fetch lockchild_" & chos.Name & " into achildlistid")
        '            s.putBufLC("while not fetch_done DO  ")
        '            s.putBufLC("begin")

        '            ' если в дочернем разделе есть заблокированная строка
        '            s.putBufLC(" select aLockUserID = LockUserID,aLockSessionID = LockSessionID from " & chos.Name & " where " & chos.Name & "id=achildlistid")
        '            s.putBufLC(" /* verify this row */")
        '            s.putBufLC(" if not aLockUserID is null  ")
        '            s.putBufLC(" begin   ")
        '            s.putBufLC("   if  aLockUserID <> auserID  ")
        '            s.putBufLC("   begin   ")
        '            s.putBufLC("     set aisLocked = 4 /* CheckOut by another user */")
        '            s.putBufLC("     close lockchild_" & chos.Name & "")

        '            s.putBufLC("     return")
        '            s.putBufLC("   end   ")
        '            s.putBufLC(" end   ")
        '            s.putBufLC(" if not aLockSessionID is null  ")
        '            s.putBufLC(" begin   ")
        '            s.putBufLC("   if  aLockSessionID <> aCURSESSION  ")
        '            s.putBufLC("   begin   ")
        '            s.putBufLC("     set aisLocked = 3 /* Lockes by another user */")
        '            s.putBufLC("     close lockchild_" & chos.Name & "")

        '            s.putBufLC("     return")
        '            s.putBufLC("   end  ")
        '            s.putBufLC(" end   ")

        '            ' или еще глубже
        '            s.putBufLC(" call " & chos.Name & "_HCL acursession,achildlistid,aisLocked out")
        '            s.putBufLC(" if aisLocked >2 begin")
        '            s.putBufLC("   close lockchild_" & chos.Name & "")
        '            's.putBufLC("   deallocate lockchild_" & chos.Name & " ")
        '            s.putBufLC("   return")
        '            s.putBufLC(" end")

        '            s.putBufLC(" fetch lockchild_" & chos.Name & " into achildlistid")
        '            s.putBufLC("end")
        '            s.putBufLC("close lockchild_" & chos.Name & "")
        '            's.putBufLC("deallocate lockchild_" & chos.Name & " ")
        '        End If
        '    Next
        '    s.putBufLC(" end ")
        '    s.putBufLC("set aIsLocked =0")
        'End If
        's.putBufLC("end")
        's.putBufLC("GO")





        Console.WriteLine("Create common procs for type " & obt.Name)
        o.ModuleName = "--Procs"
        o.Block = "--body"
        o.OutNL(s.getBuf)
        o.OutNL("GO")
        s = Nothing
    End Sub


    Private Sub MakeAllViews()



        Dim i As Integer
        Dim dt_qry As DataTable
        Dim qry As bp3qry.bp3qry.Application

        dt_qry = man.Session.GetData("select " + man.Session.GetProvider().ID2Base("instanceid", "instanceid") + " from bp3qry_def ")


        For i = 0 To dt_qry.Rows.Count - 1
            qry = man.GetInstanceObject(New Guid(dt_qry.Rows(i)("instanceid").ToString()))

            MakeViews(qry, "")


        Next

    End Sub

    Private Function GetEnums(ByVal ft As bp3ft_def) As bp3ft_enums_col
        Dim fa As bp3ft.bp3ft.Application
        fa = ft.Application
        Return fa.bp3ft_enums
    End Function

    Private Sub MakeViews_PutColumns(ByRef pv As bp3qry.bp3qry.Application, ByRef fcnt As Integer, ByRef s As Writer, ByRef from As String, ByRef log As String, ByRef noagg As Integer, ByRef group As String, ByRef BP As bp3doc.bp3doc.bp3doc_store, ByRef root As bp3doc.bp3doc.bp3doc_store, Optional ByRef NoFirstTable As Boolean = False, Optional ByRef Lang As String = "")
        Dim i As Object
        Dim j As Integer
        Dim vc As bp3qry.bp3qry.bp3qry_column
        Dim refp As bp3doc.bp3doc.bp3doc_store
        Dim p As bp3doc.bp3doc.bp3doc_store
        Dim ft As bp3ft.bp3ft.bp3ft_def
        Dim f As bp3doc.bp3doc.bp3doc_field
        '
        On Error GoTo bye

        Dim isOK As Boolean
        For i = 1 To pv.bp3qry_column.Count
            vc = pv.bp3qry_column.Item(i)
            p = vc.FromPart
            f = vc.Field
            If p Is Nothing Or f Is Nothing Then
                vc.Delete()
            Else
                ft = f.FieldType

                If Not (p Is Nothing) And Not (f Is Nothing) And ft.TypeStyle <> bp3doc.bp3doc.enumTypeStyle.TypeStyle_Element_oformleniy Then
                    '      If fcnt > 1 Then
                    s.putBufLC(", ")
                    '      End If
                    fcnt = fcnt + 1
                    If vc.Aggregation = bp3doc.bp3doc.enumAggregationType.AggregationType_none Then
                        ft = f.FieldType
                        If ft.TypeStyle = bp3doc.bp3doc.enumTypeStyle.TypeStyle_Perecislenie Then
                            ' вписываем значение перечсления
                            s.putBufLC(" " & p.Name & "." & f.Name & "  ")
                            s.putBufLC(vc.the_Alias & "_VAL, ")

                            ' и его расшифровку
                            s.putBufLC(" case " & p.Name & "." & f.Name & " ")
                            For j = 1 To GetEnums(ft).Count
                                s.putBufLC("when " & GetEnums(ft).Item(j).NameValue & " then '" & GetEnums(ft).Item(j).Name & "'")
                            Next
                            s.putBufLC(" else '' ")
                            s.putBufLC(" end  ")

                        ElseIf ft.TypeStyle = bp3doc.bp3doc.enumTypeStyle.TypeStyle_Ssilka Then

                            If (ft.Name = "ReferenceSQL") Then
                                s.putBufLC(" GetBriefFromXML(" & p.Name & "." & f.Name & ") ")
                            Else
                                ' вписываем значение ссылки


                                If ft.Name.ToLower() = "multiref" Then
                                    s.putBufLC(" " & p.Name & "." & f.Name & "  ")
                                    s.putBufLC(vc.the_Alias & "_ID, ")

                                    ' и расшифрованное значение
                                    If f.ReferenceType = bp3doc.bp3doc.enumReferenceType.ReferenceType_Na_ob_ekt_ Then
                                        'MLF
                                        If Lang = "" Then
                                            s.putBufLC(" INSTANCE_MREF_F(" & p.Name & "." & f.Name & " , NULL) ")
                                        Else
                                            s.putBufLC(" INSTANCE_MREF_F(" & p.Name & "." & f.Name & ", '" & Lang & "') ")
                                        End If
                                    ElseIf f.ReferenceType = bp3doc.bp3doc.enumReferenceType.ReferenceType_Na_stroku_razdela Then
                                        refp = f.RefToPart
                                        'MLF
                                        If Lang = "" Then
                                            s.putBufLC(" " & refp.Name & "_MREF_F(" & p.Name & "." & f.Name & ", NULL) ")
                                        Else
                                            s.putBufLC(" " & refp.Name & "_MREF_F(" & p.Name & "." & f.Name & ", '" & Lang & "') ")
                                        End If
                                    End If

                                Else

                                    s.putBufLC(" B2G(" & p.Name & "." & f.Name & ")  ")
                                    s.putBufLC(vc.the_Alias & "_ID, ")

                                    ' и расшифрованное значение
                                    If f.ReferenceType = bp3doc.bp3doc.enumReferenceType.ReferenceType_Na_ob_ekt_ Then
                                        'MLF
                                        If Lang = "" Then
                                            ' s.putBufLC(" INSTANCE_BRIEF_F(b2g(" & p.Name & "." & f.Name & ") , NULL) ")
                                            s.putBufLC(" INSTANCE_BRIEF_F(" & p.Name & "." & f.Name & " , NULL) ")
                                        Else
                                            's.putBufLC(" INSTANCE_BRIEF_F(b2g(" & p.Name & "." & f.Name & "), '" & Lang & "') ")
                                            s.putBufLC(" INSTANCE_BRIEF_F(" & p.Name & "." & f.Name & ", '" & Lang & "') ")
                                        End If
                                    ElseIf f.ReferenceType = bp3doc.bp3doc.enumReferenceType.ReferenceType_Na_stroku_razdela Then
                                        refp = f.RefToPart
                                        'MLF

                                        If Lang = "" Then
                                            s.putBufLC(" " & refp.Name & "_BRIEF_F(" & p.Name & "." & f.Name & ", NULL) ")
                                        Else
                                            s.putBufLC(" " & refp.Name & "_BRIEF_F(" & p.Name & "." & f.Name & ", '" & Lang & "') ")
                                        End If


                                    End If


                                End If



                            End If
                        Else
                            s.putBufLC(GetFullfieldName(f, p, Lang) & " ")
                        End If

                        noagg = noagg + 1
                        group = group & vbCrLf & "," & p.Name & "." & f.Name & " "
                    ElseIf vc.Aggregation = bp3doc.bp3doc.enumAggregationType.AggregationType_MAX Then
                        'MLF s.putBufLC "MAX(" & p.Name & "." & f.Name & ") "

                        s.putBufLC("MAX(" & GetFullfieldName(f, p, Lang) & ") ")

                    ElseIf vc.Aggregation = bp3doc.bp3doc.enumAggregationType.AggregationType_MIN Then
                        'MLF s.putBufLC "MIN(" & p.Name & "." & f.Name & ") "
                        s.putBufLC("MIN(" & GetFullfieldName(f, p, Lang) & ") ")
                    ElseIf vc.Aggregation = bp3doc.bp3doc.enumAggregationType.AggregationType_AVG Then
                        'MLF s.putBufLC "AVG(" & p.Name & "." & f.Name & ") "
                        s.putBufLC("AVG(" & GetFullfieldName(f, p, Lang) & ") ")
                    ElseIf vc.Aggregation = bp3doc.bp3doc.enumAggregationType.AggregationType_SUM Then
                        'MLF s.putBufLC "SUM(" & p.Name & "." & f.Name & ") "
                        s.putBufLC("SUM(" & GetFullfieldName(f, p, Lang) & ") ")
                    ElseIf vc.Aggregation = bp3doc.bp3doc.enumAggregationType.AggregationType_COUNT Then
                        'MLF s.putBufLC "COUNT(" & p.Name & "." & f.Name & ") "
                        s.putBufLC("COUNT(" & GetFullfieldName(f, p, Lang) & ") ")
                    End If
                    s.putBufLC(vc.the_Alias & " ")


                    If BP.ID = p.Parent.Parent.ID Then
                        isOK = False

                        ' проверяем поля, которые входят в раздел

                        For j = 1 To i - 1

                            If pv.bp3qry_column.Item(j).FromPart.ID = p.ID Then
                                isOK = True
                                Exit For
                            End If
                        Next

                        ' если в разделе есть поля, то включаем его в запрос
                        If Not isOK Then
                            from = from & vbCrLf & " left join " & p.Name & " on " & BP.Name & "." & BP.Name & "ID = " & p.Name & ".ParentStructRowID"
                        End If
                    End If


                    ' проверяем верхние разделы, которые не  являются непосредственными родителями нашего базового раздела
                    If TypeName(p.Parent.Parent) = "Application" And (p.ID <> root.ID) Then
                        isOK = False
                        For j = 1 To i - 1
                            If pv.bp3qry_column.Item(j).FromPart.ID = p.ID Then
                                isOK = True
                                Exit For
                            End If
                        Next
                        ' есть поля из верхнего раздела
                        If Not isOK Then
                            from = from & vbCrLf & " left join " & p.Name & " ON " & p.Name & ".InstanceID=" & root.Name & ".InstanceID"
                        End If
                    End If
                Else
                    Console.WriteLine("ERROR-Ошибка определения запроса:" & pv.Name & "(" & pv.bp3qry_def.Item(1).the_Alias & ")" & " колонка: " & vc.the_Alias & " - не задан раздел, или поле.<--ERROR")
                End If
            End If
        Next

        'MLF
        p = pv.bp3qry_def.Item(1).SrcPart
        If Lang <> "" And IsMLFPart(p) Then
            from = from & vbCrLf & " left join " & p.Name & "_" & Lang & " ON " & p.Name & "." & p.Name & "ID=" & p.Name & "_" & Lang & "." & p.Name & "ID"
        End If

        Exit Sub
bye:
        MsgBox(Err.Description)
        'Stop
        'Resume

    End Sub

    'MLF
    Private Function GetFullfieldName(ByRef f As bp3doc.bp3doc.bp3doc_field, ByRef p As bp3doc.bp3doc.bp3doc_store, ByRef Lang As String) As String
        '" & p.Name & "." & f.Name & "
        If IsMLFfield(f) And Lang <> "" Then
            Return p.Name & "_" & Lang & "." & f.Name
        Else
            Return p.Name & "." & f.Name
        End If
    End Function


    Private Sub MakeViews(ByRef pv As bp3qry.bp3qry.Application, ByRef Lang As String)

        Dim s As Writer
        Dim ot As bp3doc.bp3doc.bp3doc_def
        Dim BP As bp3doc.bp3doc.bp3doc_store
        Dim p As bp3doc.bp3doc.bp3doc_store
        Dim refp As bp3doc.bp3doc.bp3doc_store
        Dim f As bp3doc.bp3doc.bp3doc_field
        Dim ft As bp3ft.bp3ft.bp3ft_def
        Dim root As bp3doc.bp3doc.bp3doc_store
        Dim vc As bp3qry_column
        Dim i, j As Integer
        Dim from, group As String
        Dim noagg As Integer
        Dim structfld As String
        On Error GoTo bye


        BP = pv.bp3qry_def.Item(1).SrcPart

        s = New Writer

        ' найти раздел первого уровня и построить цепочку прямых join
        root = BP
        from = " from " & BP.Name
        structfld = "B2G(" & BP.Name & ". " & BP.Name & "ID) " & BP.Name & "ID"
        structfld = structfld & "," & BP.Name & ". changestamp ChangeStamp"
        While TypeName(root.Parent.Parent) <> "Application"
            from = from & vbCrLf & " join " & CType(root.Parent.Parent, bp3doc_store).Name & " on " & CType(root.Parent.Parent, bp3doc_store).Name & "." & CType(root.Parent.Parent, bp3doc_store).Name & "ID=" & root.Name & ".ParentStructRowID "
            structfld = structfld & ", B2G(" & CType(root.Parent.Parent, bp3doc_store).Name & "ID) " & CType(root.Parent.Parent, bp3doc_store).Name & "ID"
            root = root.Parent.Parent
        End While

        from = from & vbCrLf & " join INSTANCE on " & root.Name & ".INSTANCEID=INSTANCE.INSTANCEID"
        from = from & vbCrLf & " left join objstatus XXXMYSTATUSXXX on instance.status=XXXMYSTATUSXXX.objstatusid"

        group = " group by " & root.Name & ".InstanceID, " & BP.Name & "." & BP.Name & "ID "

        ' стандартное начало
        If Lang <> "" Then
            s.putBufLC(viewDropSQL("V_" & pv.bp3qry_def.Item(1).the_Alias & "_" & Lang))
            s.putBufLC("create view V_" & pv.bp3qry_def.Item(1).the_Alias & "_" & Lang & " as ")
        Else
            s.putBufLC(viewDropSQL("V_" & pv.bp3qry_def.Item(1).the_Alias))
            s.putBufLC("create view V_" & pv.bp3qry_def.Item(1).the_Alias & " as ")
        End If
        'MLF
        '   If Lang <> "" Then
        '     s.putBufLC "select   " & BP.Name & "_" & Lang & "." & structfld
        '   Else
        s.putBufLC("select   " & structfld)
        '   End If
        Dim fcnt As Integer
        fcnt = 1

        MakeViews_PutColumns(pv, fcnt, s, from, log, noagg, group, BP, root, , Lang)


        MakeLinkedView(pv, s, from, log, group, Lang)


        s.putBufLC(", B2G(" & root.Name & ".InstanceID) InstanceID ")
        s.putBufLC(", " & root.Name & ".InstanceID InstanceID_VAL ")
        s.putBufLC(",  INSTANCE.archived   instance_archived ")
        s.putBufLC(", B2G(" & BP.Name & "." & BP.Name & "ID) ID ")
        s.putBufLC(", '" & BP.Name & "' VIEWBASE ")
        s.putBufLC(", XXXMYSTATUSXXX.Name StatusName ")
        s.putBufLC(", B2G(XXXMYSTATUSXXX.objstatusid) INTSANCEStatusID")

        ' if no aggregations - no group by
        If noagg = pv.bp3qry_column.Count Then group = ""
        'If isButton Then group = ""

        o.ModuleName = "--Views--"
        o.Block = "--Views--"
        o.OutNL(s.getBuf & vbCrLf & from & vbCrLf & group)
        o.OutNL("GO")


        s = Nothing
        Exit Sub
bye:
        Console.WriteLine("ERROR-" & Err.Description & "<--ERROR")
        s = Nothing
        Exit Sub
    End Sub


    Private Sub MakeLinkedView(ByRef pvml As bp3qry.bp3qry.Application, ByRef s As Writer, ByRef from As String, ByRef log As String, ByRef group As String, Optional ByRef Lang As String = "")
        Dim i As Integer
        On Error GoTo bye
        pvml.bp3qry_link.Sort = "SEQ"

        Dim PVD As bp3qry.bp3qry.bp3qry_def
        Dim fcnt2 As Integer
        Dim noagg2 As Integer
        Dim LinkToPart As bp3doc.bp3doc.bp3doc_store
        Dim root2 As bp3doc.bp3doc.bp3doc_store
        Dim fromAddings As String = ""
        Dim structfld2 As String
        Dim LinkFromPart As bp3doc_store
        LinkFromPart = CType(pvml.Parent.Parent, bp3doc_store)

        For i = 1 To pvml.bp3qry_link.Count
            PVD = pvml.bp3qry_link.Item(i).TheView

            LinkToPart = PVD.Parent.Parent
            ' найти раздел первого уровня и построить цепочку прямых join
            root2 = LinkToPart

            structfld2 = LinkToPart.Name & "ID"
            While TypeName(root2.Parent.Parent) <> "Application"

                fromAddings = fromAddings & vbCrLf & " join " & CType(root2.Parent.Parent, bp3doc_store).Name & " on " & CType(root2.Parent.Parent, bp3doc_store).Name & "." & CType(root2.Parent.Parent, bp3doc_store).Name & "ID=" & root2.Name & ".ParentStructRowID "

                structfld2 = structfld2 & ", " & CType(root2.Parent.Parent, bp3doc_store).Name & "ID"

                root2 = root2.Parent.Parent
            End While

            'MLF

            Dim Tofield As bp3doc_field
            Dim ToPart As bp3doc_store
            Dim FromFiled As bp3doc_field
            Dim FromPart As bp3doc_store

            If pvml.bp3qry_link.Item(i).RefType = bp3doc.bp3doc.enumJournalLinkType.JournalLinkType_Ssilka_na_ob_ekt Then
                FromFiled = CType(CType(pvml.bp3qry_link.Item(i).TheJoinSource, bp3qry_column).Field, bp3doc_field)
                FromPart = CType(FromFiled.Parent.Parent, bp3doc_store)

                from = from & vbCrLf & " left join " + LinkToPart.Name + " on " + root2.Name + ".InstanceID=" + LinkFromPart.Name + "." + _
                FromFiled.Name + " "

            ElseIf pvml.bp3qry_link.Item(i).RefType = bp3doc.bp3doc.enumJournalLinkType.JournalLinkType_Ssilka_na_stroku Then

                FromFiled = CType(CType(pvml.bp3qry_link.Item(i).TheJoinSource, bp3qry_column).Field, bp3doc_field)
                FromPart = CType(FromFiled.Parent.Parent, bp3doc_store)

                from = from & vbCrLf & " left join " + LinkToPart.Name + " on " + LinkToPart.Name + "." + LinkToPart.Name + "ID=" + FromPart.Name + "." + FromFiled.Name + "  "

            ElseIf pvml.bp3qry_link.Item(i).RefType = bp3doc.bp3doc.enumJournalLinkType.JournalLinkType_Svyzka_InstanceID_OPNv_peredlah_ob_ektaCLS Then

                from = ((from & vbCrLf & " left join ") + LinkToPart.Name + (" on ") + LinkFromPart.Name + (".InstanceID=") + LinkToPart.Name + (".") + ("InstanceID "))

            ElseIf pvml.bp3qry_link.Item(i).RefType = bp3doc.bp3doc.enumJournalLinkType.JournalLinkType_Svyzka_ParentStructRowID__OPNv_peredlah_ob_ektaCLS Then

                from = ((from & vbCrLf & " left join ") + LinkToPart.Name + (" on ") + LinkFromPart.Name + (".") + LinkFromPart.Name + ("ID") + ("=") + LinkToPart.Name + (".") + ("ParentStructRowID "))
            Else
                Exit For
            End If

            from = from & vbCrLf & fromAddings

            MakeViews_PutColumns(PVD.Application, fcnt2, s, from, log, noagg2, group, LinkToPart, root2, , Lang)
            MakeLinkedView(PVD.Application, s, from, log, group, Lang)
        Next
        Exit Sub
bye:
        MsgBox(Err.Description)

        'Stop
        'Resume

    End Sub


    Private Function IsParent(ByRef p As bp3doc.bp3doc.bp3doc_store, ByRef Parent As String) As Boolean
        Dim o As Object
        o = p
        While TypeName(o) <> "Application"
            o = o.Parent.Parent
            If o.ID = Parent Then
                IsParent = True
                Exit Function
            End If
        End While
        IsParent = False

    End Function

    ' создаем view для журналов
    Private Sub MakeJournals()
        '  DebugOutput "MYSQLGEN.MakeJournals:start "
        '  Dim jr As Jounal
        '  Dim jc As JournalColumn
        '  Dim js As JournalSrc
        '  Dim jcs As JColumnSource
        '  Dim s As String, out As String
        '
        '  Dim i As Long, j As Long, k As Long, l As Long, NoCol As Boolean
        '  For i = 1 To m.Jounal.Count
        '    Set jr = m.Jounal.Item(i)
        '    s = "create view J_" & jr.Name & " as  " & vbCrLf
        '    For j = 1 To jr.JournalSrc.Count
        '      Set js = jr.JournalSrc.Item(j)
        '      If j > 1 Then s = s & vbCrLf & " union all " & vbCrLf
        '      s = s & vbCrLf & " select InstanceID, ID, VIEWBASE "
        '      For k = 1 To jr.JournalColumn.Count
        '        NoCol = True
        '        Set jc = jr.JournalColumn.Item(k)
        '        For l = 1 To jc.JColumnSource.Count
        '          Set jcs = jc.JColumnSource.Item(l)
        '          If jcs.SrcPartView.ID = js.ID Then
        '            s = s & vbCrLf & ", " & jcs.View.bp3doc_field & " /* " & jc.Name & " */ "
        '            NoCol = False
        '          End If
        '        Next l
        '        If NoCol Then
        '            s = s & vbCrLf & ", null /* " & jc.Name & " */ "
        '        End If
        '      Next k
        '      s = s & vbCrLf & " from V_" & js.PARTVIEW.the_Alias
        '
        '    Next j
        '    o.ModuleName= "--Journals--"
        '    o.Block = "--Journals--"
        '    o.OutNL s
        '    o.OutNL "GO"
        '  Next i
        '  DebugOutput "MYSQLGEN.MakeJournals:done "
    End Sub



    Private Sub CreateV2Proc(ByRef os As bp3doc.bp3doc.bp3doc_store)
        If os.PartType = bp3doc.bp3doc.enumPartType.PartType_Rassirenie Then
            Exit Sub
        End If

        Dim st As bp3doc.bp3doc.bp3doc_store
        st = os
        Dim chos As bp3doc.bp3doc.bp3doc_store
        Dim i As Short
        Dim s As Writer
        s = New Writer

        System.Windows.Forms.Application.DoEvents()
        Console.WriteLine("-CreateV2Proc " & os.Name)

        On Error GoTo bye


        s.putBufLC(procDropSQL(os.Name & "_PARENT"))
        s.putBufLC("create procedure " & os.Name & "_PARENT /* " & os.the_Comment & "*/ (")
        s.putBufLC(" acursession varchar(38),")
        s.putBufLC(" aRowID BINARY(16) ,")
        s.putBufLC("out  aParentID BINARY(16),")
        s.putBufLC("out aParentTable varchar(255)")

        s.putBufLC(") body:begin  ")


        s.putBufLC("declare aEC  int;")
        s.putBufLC("select count(*) into aEC from the_session where the_sessionid=acursession and closed=0 ;")
        s.putBufLC("if aEC=0  then")
        s.putBufLC("    leave body;")
        s.putBufLC("end if;")

        If TypeName(os.Parent.Parent) = "Application" Then
            s.putBufLC("  set aParentTable = 'INSTANCE';")
            s.putBufLC("  select INSTANCEID into aParentID from " & os.Name & " where  " & os.Name & "id=aRowID;")
        Else

            s.putBufLC("  select ParentStructRowID into aParentID from " & os.Name & " where  " & os.Name & "id=aRowID;")
            s.putBufLC("  set aParentTable = '" & CType(os.Parent.Parent, bp3doc_store).Name & "';")
        End If

        s.putBufLC(" end ")
        s.putBufLC("GO")




        o.ModuleName = "--Procedures"
        o.Block = "--TableProc"
        o.OutNL(s.getBuf)
        o.OutNL("GO")



        '------------------------------- IsLockED ----------------------------------------------

        s = Nothing
        s = New Writer
        s.putBufLC(procDropSQL(os.Name & "_ISLOCKED"))
        s.putBufLC("create procedure " & os.Name & "_ISLOCKED /* " & os.the_Comment & " */ (")
        s.putBufLC(" acursession varchar(38),")
        s.putBufLC(" aRowID BINARY(16) ,")
        s.putBufLC(" out aIsLocked integer")
        s.putBufLC(") body:begin  ")



        s.putBufLC(" declare aParentID BINARY(16);")
        s.putBufLC(" declare aUserID BINARY(16);")
        s.putBufLC(" declare aLockUserID BINARY(16);")
        s.putBufLC(" declare aLockSessionID BINARY(16);")
        s.putBufLC(" declare aParentTable varchar(255); ")
        s.putBufLC("declare aEC  int;")
        s.putBufLC(" set aisLocked = 0;")
        s.putBufLC("select count(*) into aEC from the_session where the_sessionid=acursession and closed=0 ;")
        s.putBufLC("if aEC=0  then")
        s.putBufLC("    leave body;")
        s.putBufLC("end if;")


        s.putBufLC(" select  usersid into auserID  from the_session where the_sessionid=acursession;")
        s.putBufLC(" select  LockUserID,LockSessionID into aLockUserID, aLockSessionID from " & os.Name & " where " & os.Name & "id=aRowID;")
        s.putBufLC(" /* verify this row */")
        s.putBufLC(" if not aLockUserID is null  then")
        s.putBufLC("   if  aLockUserID <> auserID  then")
        s.putBufLC("     set aisLocked = 4; /* CheckOut by another user */")
        s.putBufLC("     LEAVE body;")
        s.putBufLC("   else ")
        s.putBufLC("     set aisLocked = 2; /* CheckOut by caller */")
        s.putBufLC("     LEAVE body;")
        s.putBufLC("   end if;  ")
        s.putBufLC(" end if;  ")

        s.putBufLC(" if not aLockSessionID is null then ")
        s.putBufLC("   if  aLockSessionID <> aCURSESSION  then")
        s.putBufLC("     set aisLocked = 3 ;/* Lockes by another user */")
        s.putBufLC("     LEAVE body;")
        s.putBufLC("   else ")
        s.putBufLC("     set aisLocked = 1; /* Locked by caller */")
        s.putBufLC("     LEAVE body;")
        s.putBufLC("   end if;  ")
        s.putBufLC(" end if;  ")

        s.putBufLC(" set aisLocked = 0; ")
        's.putBufLC("  declare as nvarchar(255)")
        's.putBufLC("  call " & os.Name & "_parent aCURSESSION,aROWID,aParentID output ,aParentTable output")
        's.putBufLC("  set as = N' call ' + aPARENTTABLE + N'_islocked acursession,arowid,aislocked OUTPUT'")
        's.putBufLC("  call sp_executesql as,N'acursession varchar(38) ,aRowID BINARY(16),aIsLocked int out',aCURSESSION,aParentID ,aISLocked output")
        s.putBufLC(" end ")
        s.putBufLC(" go")


        o.ModuleName = "--Procedures"
        o.Block = "--TableProc"
        o.OutNL(s.getBuf)
        o.OutNL("GO")

        '--------------------------- Блокируем запись
        s = Nothing
        s = New Writer
        s.putBufLC(procDropSQL(os.Name & "_LOCK"))
        s.putBufLC("create procedure " & os.Name & "_LOCK /* " & os.the_Comment & " */ (")
        s.putBufLC(" acursession varchar(38),")
        s.putBufLC(" aRowID BINARY(16) ,")
        s.putBufLC(" aLockMode integer ")
        s.putBufLC(") body:begin  ")
        s.putBufLC(" declare aParentID BINARY(16);")
        s.putBufLC(" declare aUserID BINARY(16);")
        s.putBufLC(" declare atmpID BINARY(16);")
        s.putBufLC(" declare aaccess integer;")
        s.putBufLC(" declare aIsLocked integer;")
        s.putBufLC(" declare aParentTable varchar(255); ")
        s.putBufLC("declare aEC  int;")
        s.putBufLC("select count(*) into aEC from the_session where the_sessionid=acursession and closed=0 ;")
        s.putBufLC("if aEC=0  then")
        s.putBufLC("    leave body;")
        s.putBufLC("end if;")


        s.putBufLC(" select auserID = usersid  from the_session where the_sessionid=acursession;")
        s.putBufLC(" call " & os.Name & "_ISLOCKED( aCURSESSION,aROWID,aISLocked);")
        s.putBufLC(" if aIsLocked >=3  then")
        's.putBufLC("    raiserror('Строка заблокирована другим пользователем',16,1)")
        s.putBufLC("    LEAVE body;")
        s.putBufLC("  end if;")

        's.putBufLC(" if aIsLocked =0  then")
        's.putBufLC("  call " & os.Name & "_HCL( acursession,aRowID,aisLocked );")
        's.putBufLC("  if aIsLocked >=3  ")
        ''s.putBufLC("     raiserror('У данной строки имеются дочерние строки, которые заблокированы другим пользователем',16,1)")
        's.putBufLC("     leave body;")
        's.putBufLC("   end if;")
        's.putBufLC(" end if;")

        s.putBufLC("   if  aLockMode =2  then")
        s.putBufLC("    update " & os.Name & " set LockUserID =auserID ,LockSessionID=null  where " & os.Name & "id=aRowID;")
        s.putBufLC("     leave body;")
        s.putBufLC("   end if;")

        s.putBufLC("   if  aLockMode =1  then")
        s.putBufLC("    update " & os.Name & " set LockUserID=null ,LockSessionID =aCURSESSION  where " & os.Name & "id=aRowID;")
        s.putBufLC("     leave body;")
        s.putBufLC("   end if;")

        s.putBufLC(" end ")
        s.putBufLC(" go ")

        o.ModuleName = "--Procedures"
        o.Block = "--TableProc"
        o.OutNL(s.getBuf)
        o.OutNL("GO")


        '--------------------------- HCL - Has Children Locked

        's = Nothing
        's = New Writer
        's.putBufLC(procDropSQL(os.Name & "_HCL"))
        's.putBufLC("create procedure " & os.Name & "_HCL /* " & os.the_Comment & "*/ (")
        's.putBufLC(" acursession varchar(38),")
        's.putBufLC(" aRowID BINARY(16) ,")
        's.putBufLC(" out aIsLocked integer")
        's.putBufLC(") body: begin  ")

        ''---- проверяем, что нет заблокированных записей в  дочерних разделах
        's.putBufLC("declare achildlistid BINARY(16)")
        's.putBufLC(" declare aUserID BINARY(16)")
        's.putBufLC(" declare aLockUserID BINARY(16)")
        's.putBufLC(" declare aLockSessionID BINARY(16)")
        's.putBufLC(" select auserID = usersid  from the_session where the_sessionid=acursession")

        'If os.bp3doc_store.Count > 0 Then
        '    s.putBufLC("-- verify child locks")
        'End If

        'For i = 1 To os.bp3doc_store.Count
        '    chos = os.bp3doc_store.Item(i)
        '    ''''     If Not chos.PartType = 3 Then
        '    If Not chos.PartType = bp3doc.bp3doc.enumPartType.PartType_Rassirenie Then
        '        s.putBufLC("declare lockchild_" & chos.Name & " cursor local for select " & chos.Name & "." & chos.Name & "id from " & chos.Name & " where  " & chos.Name & ".ParentStructRowID = aRowid")
        '        s.putBufLC("open lockchild_" & chos.Name & "")
        '        s.putBufLC("fetch lockchild_" & chos.Name & " into achildlistid")
        '        s.putBufLC("while not fetch_done DO  ")
        '        s.putBufLC("begin")

        '        ' если в дочернем разделе есть заблокированная строка
        '        s.putBufLC(" select aLockUserID = LockUserID,aLockSessionID = LockSessionID from " & chos.Name & " where " & chos.Name & "id=achildlistid")
        '        s.putBufLC(" /* verify this row */")
        '        s.putBufLC(" if not aLockUserID is null  ")
        '        s.putBufLC(" begin   ")
        '        s.putBufLC("   if  aLockUserID <> auserID  ")
        '        s.putBufLC("   begin   ")
        '        s.putBufLC("     set aisLocked = 4 /* CheckOut by another user */")
        '        s.putBufLC("     close lockchild_" & chos.Name & "")
        '        's.putBufLC("     deallocate lockchild_" & chos.Name & " ")
        '        s.putBufLC("     return")
        '        s.putBufLC("   end   ")
        '        s.putBufLC(" end   ")
        '        s.putBufLC(" if not aLockSessionID is null  ")
        '        s.putBufLC(" begin   ")
        '        s.putBufLC("   if  aLockSessionID <> aCURSESSION  ")
        '        s.putBufLC("   begin   ")
        '        s.putBufLC("     set aisLocked = 3 /* Lockes by another user */")
        '        s.putBufLC("     close lockchild_" & chos.Name & "")
        '        's.putBufLC("     deallocate lockchild_" & chos.Name & " ")
        '        s.putBufLC("     return")
        '        s.putBufLC("   end  ")
        '        s.putBufLC(" end   ")

        '        ' или еще глубже
        '        s.putBufLC("if aanestlevel <25 begin")
        '        s.putBufLC(" call " & chos.Name & "_HCL acursession,achildlistid,aisLocked out")
        '        s.putBufLC(" if aisLocked >2 begin")
        '        s.putBufLC("   close lockchild_" & chos.Name & "")
        '        's.putBufLC("   deallocate lockchild_" & chos.Name & " ")
        '        s.putBufLC("   return")
        '        s.putBufLC(" end")
        '        s.putBufLC("end")

        '        s.putBufLC(" fetch lockchild_" & chos.Name & " into achildlistid")
        '        s.putBufLC("end")
        '        s.putBufLC("close lockchild_" & chos.Name & "")
        '        's.putBufLC("deallocate lockchild_" & chos.Name & " ")
        '    End If
        'Next
        's.putBufLC("set aIsLocked =0")
        's.putBufLC("end")
        's.putBufLC("GO")
        'If (OptRights) Then
        '    's.putBufLC("revoke all on " & os.Name & "_HCL to public")
        '    's.putBufLC("GO")
        '    's.putBufLC("grant execute on " & os.Name & "_HCL to public")
        '    's.putBufLC("GO")
        'End If


        'o.ModuleName = "--Procedures"
        'o.Block = "--TableProc"
        'o.OutNL(s.getBuf)
        'o.OutNL("GO")


        '--------------------------- Разблокируем запись
        s = Nothing
        s = New Writer
        s.putBufLC(procDropSQL(os.Name & "_UNLOCK"))
        s.putBufLC("create procedure " & os.Name & "_UNLOCK /* " & os.the_Comment & " */ (")
        s.putBufLC(" acursession varchar(38),")
        s.putBufLC(" aRowID BINARY(16) ")
        s.putBufLC(") body: begin  ")

        s.putBufLC(" declare aParentID BINARY(16);")
        s.putBufLC(" declare aUserID BINARY(16);")
        s.putBufLC(" declare aIsLocked integer;")
        s.putBufLC(" declare aParentTable varchar(255); ")

        s.putBufLC("declare aEC  int;")
        s.putBufLC("select count(*) into aEC from the_session where the_sessionid=acursession and closed=0 ;")
        s.putBufLC("if aEC=0  then")
        s.putBufLC("    leave body;")
        s.putBufLC("end if;")


        s.putBufLC(" select usersid into auserID  from the_session where the_sessionid=acursession;")
        s.putBufLC(" call " & os.Name & "_ISLOCKED( aCURSESSION,aROWID,aISLocked );")
        s.putBufLC(" if aIsLocked >=3  then")


        's.putBufLC("    raiserror('Строка заблокирована другим пользователем',16,1)")
        s.putBufLC("    leave body;")
        s.putBufLC("   end if;")

        s.putBufLC("   if  aIsLocked =2  then")

        s.putBufLC("    update " & os.Name & " set LockUserID =null  where " & os.Name & "id=aRowID;")
        s.putBufLC("    leave body;")
        s.putBufLC("   end if;")


        s.putBufLC("   if  aIsLocked =1  then")

        s.putBufLC("    update " & os.Name & " set LockSessionID =null  where " & os.Name & "id=aRowID;")
        s.putBufLC("    leave body;")
        s.putBufLC("   end if;")


        s.putBufLC(" end ")
        s.putBufLC("GO")

        '

        o.ModuleName = "--Procedures"
        o.Block = "--TableProc"
        o.OutNL(s.getBuf)
        o.OutNL("GO")

        CreateClientLogic(os)




        For i = 1 To os.bp3doc_store.Count
            chos = os.bp3doc_store.Item(i)
            CreateV2Proc(chos)
        Next
        s = Nothing

        Exit Sub
bye:
        Console.WriteLine("ERROR-" & Err.Description & "<--ERROR")
        s = Nothing
    End Sub




    Private Sub LoadMap()
        Dim ff As Short
        Dim ID1S As String = "", IDMTZ As String = ""
        Dim idm As IDMAP
        ff = FreeFile()
        Map = New Collection
        On Error GoTo bye
        FileOpen(ff, My.Application.Info.DirectoryPath & "\IDMAP.txt", OpenMode.Input)
        While Not EOF(ff)
            Input(ff, ID1S)
            Input(ff, IDMTZ)
            idm = New IDMAP
            If ID1S <> "" Then
                idm.ID1S = ID1S
                idm.IDMTZ = IDMTZ
                On Error Resume Next
                Map.Add(idm, ID1S)
                On Error GoTo bye
            End If
        End While
        FileClose(ff)
bye:

    End Sub

    Private Sub SaveMap()
        Dim ff As Short
        Dim idm As IDMAP
        ff = FreeFile()

        '  Dim mTempPath As String
        '  mTempPath = GetSetting("MTZ", "CONFIG", "TEMPPATH", "")
        '  If mTempPath = "" Then
        '    ChDir App.Path
        '    On Error Resume Next
        '    MkDir "TMP"
        '    fname = App.Path & "\TMP\" & CreateGUID2 & ".txt"
        '  Else
        '    fname = mTempPath & CreateGUID2 & ".txt"
        '  End If
        '
        Try
            FileOpen(ff, My.Application.Info.DirectoryPath & "\IDMAP.txt", OpenMode.Output)
            For Each idm In Map
                WriteLine(ff, idm.ID1S, idm.IDMTZ)
            Next idm
            FileClose(ff)
        Catch ex As Exception

        End Try

    End Sub

    Private Function GetMap(ByRef ID1S As String) As String
        Dim idm As IDMAP
        On Error Resume Next

        idm = Nothing
        idm = Map.Item(ID1S)
        If idm Is Nothing Then
            idm = New IDMAP
            idm.ID1S = ID1S
            idm.IDMTZ = Guid.NewGuid.ToString()
            Map.Add(idm, ID1S)
        End If
        GetMap = idm.IDMTZ
    End Function


    Private Sub Class_Initialize_Renamed()
        LoadMap()
    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub


    Private Sub Class_Terminate_Renamed()
        SaveMap()
    End Sub
    Protected Overrides Sub Finalize()
        Class_Terminate_Renamed()
        MyBase.Finalize()
    End Sub



    Private Function Delimiter() As String
        Return "" ' "DELIMITER $$" & vbCrLf
    End Function

    Private Function procDropSQL(ByRef p As String) As String
        Dim s As String = ""
        s = s & vbCrLf & "drop procedure IF EXISTS " & p & ""
        s = s & vbCrLf & "GO"
        Return s
    End Function

    Private Function funcDropSQL(ByRef p As String) As String
        Dim s As String = ""
        s = s & vbCrLf & "drop function IF EXISTS " & p & ""
        s = s & vbCrLf & "GO"
        Return s
    End Function



    'Private Function indexDropSQL(ByRef tbl As String, ByRef idx As String) As String
    '    Dim s As String = ""
    '    s = s + vbCrLf + procDropSQL("alter_idx")
    '    s = s + vbCrLf + "create procedure alter_idx() begin"
    '    s = s & vbCrLf & "if exists (SELECT 1 "
    '    s = s & vbCrLf & "  FROM(INFORMATION_SCHEMA.STATISTICS)"
    '    s = s & vbCrLf & "  WHERE(table_schema = DATABASE()) "
    '    s = s & vbCrLf & "  AND   table_name   = '" + tbl + "' "
    '    s = s & vbCrLf & "  AND   index_name   = '" + idx + "' ) THEN"
    '    s = s & vbCrLf & "      drop index " & idx & " on " & tbl & "; "
    '    s = s + vbCrLf + "End If;"
    '    s = s + vbCrLf + "End "
    '    s = s + vbCrLf + "GO"
    '    s = s + vbCrLf + "call alter_idx(); "
    '    s = s + vbCrLf + "GO"
    '    s = s + vbCrLf + procDropSQL("alter_idx")
    '    Return s
    'End Function

    'Private Function keyDropSQL(ByRef tbl As String, ByRef key As String) As String
    '    Dim s As String = ""
    '    s = s + vbCrLf + procDropSQL("alter_fk")
    '    s = s + vbCrLf + "create procedure alter_fk() begin"
    '    s = s + vbCrLf + "IF EXISTS (SELECT 1 FROM information_schema.TABLE_CONSTRAINTS WHERE"
    '    s = s + vbCrLf + "           CONSTRAINT_SCHEMA = DATABASE() AND"
    '    s = s + vbCrLf + "           CONSTRAINT_NAME   = '" + key + "' AND "
    '    s = s + vbCrLf + "           CONSTRAINT_TYPE   = 'FOREIGN KEY') THEN"
    '    s = s + vbCrLf + "         ALTER TABLE " + tbl + " DROP FOREIGN KEY " + key + " ;"
    '    s = s + vbCrLf + "End If;"
    '    s = s + vbCrLf + "End "
    '    s = s + vbCrLf + "GO"
    '    s = s + vbCrLf + "call alter_fk(); "
    '    s = s + vbCrLf + "GO"
    '   s = s + vbCrLf + procDropSQL("alter_fk")


    '    Return s
    'End Function

    'Private Function PkeyDropSQL(ByRef tbl As String, ByRef key As String) As String
    '    Dim s As String = ""
    '    's = "if exists(select * from sysobjects where id=object_id(N'" & key & "') and xtype='PK' and type='K')"
    '    's = s & vbCrLf & "ALTER TABLE " & tbl & " DROP CONSTRAINT " & key
    '    's = s & vbCrLf & "GO"
    '    Return s
    'End Function


    Private Function viewDropSQL(ByRef p As String) As String
        Dim s As String = ""
        s = s & vbCrLf & "drop view IF EXISTS " & p & ""
        s = s & vbCrLf & "GO"
        Return s
    End Function


    Private Function tableDropSQL(ByRef p As String) As String
        Dim s As String = ""
        s = s & vbCrLf & "drop table IF EXISTS " & p & ""
        s = s & vbCrLf & "GO"
        Return s
    End Function
    Private Function ColumnDropSQL(ByRef t As String, ByRef collist As String) As String
        Dim s As String
        s = ""
        's = "GO"
        's = s & vbCrLf & "-- drop extra columns from generated table: " & t
        's = s & vbCrLf & "declare an nvarchar(255)"
        's = s & vbCrLf & "declare ae_str nvarchar(255)"
        's = s & vbCrLf & ""
        's = s & vbCrLf & "declare nnn cursor local for"
        's = s & vbCrLf & "select name from syscolumns where id = object_id('" & t & "')"
        's = s & vbCrLf & "and name not in(" & collist & ")"
        's = s & vbCrLf & "open nnn"
        's = s & vbCrLf & "fetch nnn into an"
        's = s & vbCrLf & "while not fetch_done DO "
        's = s & vbCrLf & "begin"
        's = s & vbCrLf & "  set ae_str=N'alter table " & t & " drop column '+an"
        's = s & vbCrLf & "  call  sp_sqlexec ae_str"
        's = s & vbCrLf & "  fetch nnn into an"
        's = s & vbCrLf & "End"
        's = s & vbCrLf & "Close nnn"
        's = s & vbCrLf & "deallocate nnn"
        's = s & vbCrLf & "GO"
        ColumnDropSQL = s
    End Function










    Private Function AutoCloseJob() As String
        Dim s As Writer
        s = New Writer


        AutoCloseJob = s.getBuf
    End Function



    '    Public Function CheckPartMLF(ByRef os As bp3doc.bp3doc.bp3doc_store, ByRef log As String) As String
    '        On Error GoTo Error_Detected
    '        Dim i As Integer
    '        Dim j As Integer
    '        Dim f As bp3doc.bp3doc.bp3doc_field
    '        Dim ft As bp3ft.bp3ft.bp3ft_def
    '        Dim bDetected As Boolean
    '        Dim s As Writer
    '        s = New Writer
    '        Dim collist As String
    '        'Exit Function
    '        bDetected = False
    '        For i = 1 To os.bp3doc_field.Count
    '            ft = os.bp3doc_field.Item(i).fieldType
    '            If UCase(ft.Name) = UCase("MultiLanguage String") Or UCase(ft.Name) = UCase("MultiLanguage Memo") Then
    '                bDetected = True
    '            End If
    '        Next

    '        Dim sTableName As String
    '        If bDetected Then
    '            'create table IF NOT EXISTS for each cultures

    '            For i = 1 To CType(os.Application, MTZMetaModel.MTZMetaModel.Application).LocalizeInfo.Count

    '                s = Nothing
    '                s = New Writer
    '                sTableName = os.Name & "_" & CType(os.Application, MTZMetaModel.MTZMetaModel.Application).LocalizeInfo.Item(i).LangShort
    '                console.writeline( "-MLE CreateStruct " & os.Name & ":" & CType(os.Application, MTZMetaModel.MTZMetaModel.Application).LocalizeInfo.Item(i).LangFull & " (" & CType(os.Application, MTZMetaModel.MTZMetaModel.Application).LocalizeInfo.Item(i).LangShort & ")"
    '                s.putBufLC("/*" & os.Caption & "_" & CType(os.Application, MTZMetaModel.MTZMetaModel.Application).LocalizeInfo.Item(i).LangShort & "*/")

    '                s.putBufLC("create table IF NOT EXISTS " & sTableName & "/*" & os.the_Comment & "*/ (")
    '                collist = ""
    '                If TypeName(os.Parent.Parent) = "Application" Then
    '                    s.putBufLC("InstanceID BINARY(16) ,")
    '                    collist = collist & "'InstanceID'"
    '                Else
    '                    s.putBufLC("ParentStructRowID BINARY(16) not null,")
    '                    collist = collist & "'ParentStructRowID'"
    '                End If

    '                s.putBufLC(os.Name & "id BINARY(16) not null  ")
    '                collist = collist & ",'" & os.Name & "ID'"

    '                s.putBufLC(",ChangeStamp datetime not null  /* Время последнего изменения */")
    '                collist = collist & ",'ChangeStamp'"

    '                s.putBufLC(",TimeStamp timestamp not null  /* для организации инкрементального индексирования полнотекстовой информации */")
    '                collist = collist & ",'TimeStamp'"


    '                's.putBuf ",LockSessionID BINARY(16) null  /* temporary lock */"
    '                'collist = collist & ",'LockSessionID'"
    '                's.putBuf ",LockUserID BINARY(16) null /* checkout lock */"
    '                'collist = collist & ",'LockUserID'"
    '                's.putBuf ",SecurityStyleID BINARY(16) null /* security formula */"
    '                'collist = collist & ",'SecurityStyleID'"



    '                ' дерево
    '                If os.PartType = 2 Then
    '                    s.putBufLC(",ParentRowid BINARY(16) ")
    '                    collist = collist & ",'ParentRowid'"
    '                End If

    '                s.putBufLC(")")

    '                s.putBufLC("GO")


    '                os.bp3doc_field.Sort = "sequence"
    '                For j = 1 To os.bp3doc_field.Count
    '                    ft = os.bp3doc_field.Item(j).FieldType
    '                    'Только ML поля
    '                    If UCase(ft.Name) = UCase("MultiLanguage String") Or UCase(ft.Name) = UCase("MultiLanguage Memo") Then
    '                        ' Добавляем

    '                        s.putBufLC("alter table " & sTableName & " add ")
    '                        s.putBufLC(fieldForCreate(os.bp3doc_field.Item(j)))
    '                        collist = collist & ",'" & os.bp3doc_field.Item(j).Name & "'"
    '                        s.putBufLC("GO")
    '                    End If
    '                Next

    '                s.putBufLC(ColumnDropSQL(sTableName, collist))
    '                o.ModuleName = "--Tables"
    '                o.Block = "--body"
    '                o.OutNL(s.getBuf)

    '                s = Nothing
    '                s = New Writer

    '                s.putBufLC(procDropSQL("alter_pk"))
    '                s.putBufLC("create procedure alter_pk() begin")
    '                s.putBufLC("IF not EXISTS (SELECT 1 FROM information_schema.TABLE_CONSTRAINTS WHERE")
    '                s.putBufLC("           CONSTRAINT_SCHEMA = DATABASE() AND ")
    '                s.putBufLC("           TABLE_NAME   = '" + sTableName + "' AND ")
    '                s.putBufLC("           CONSTRAINT_TYPE   = 'PRIMARY KEY') THEN")
    '                s.putBufLC("alter table " & sTableName & " add constraint pk_" & sTableName & " primary key (" & os.Name & "ID)")
    '                s.putBufLC("End If;")
    '                s.putBufLC("End ")
    '                s.putBufLC("GO")
    '                s.putBufLC("call alter_pk(); ")
    '                s.putBufLC("GO")
    '                s.putBufLC(procDropSQL("alter_pk"))

    '                o.ModuleName = "--Tables"
    '                o.Block = "--body"
    '                o.OutNL(s.getBuf)
    '                o.OutNL("GO")


    '                s = Nothing
    '                s = New Writer



    '                s.putBufLC(procDropSQL("alter_fk"))
    '                s.putBufLC("create procedure alter_fk() begin")
    '                s.putBufLC("IF not EXISTS (SELECT 1 FROM information_schema.TABLE_CONSTRAINTS WHERE")
    '                s.putBufLC("           CONSTRAINT_SCHEMA = DATABASE() AND")
    '                s.putBufLC("           TABLE_NAME   = '" & sTableName & "' AND ")
    '                s.putBufLC("           CONSTRAINT_TYPE   = 'FOREIGN KEY') THEN")
    '                s.putBufLC("            alter table " & sTableName & " add constraint fk_" & MakeName(os.ID.ToString() & CType(os.Application, MTZMetaModel.MTZMetaModel.Application).LocalizeInfo.Item(i).LangShort) & " foreign key(INSTANCEID) references INSTANCE (INSTANCEID);")

    '                s.putBufLC("End If;")
    '                s.putBufLC("End ")
    '                s.putBufLC("GO")
    '                s.putBufLC("call alter_fk(); ")
    '                s.putBufLC("GO")
    '                s.putBufLC(procDropSQL("alter_fk"))

    '                o.ModuleName = "--Tables"
    '                o.Block = "--ForeignKey"
    '                o.OutNL(s.getBuf)
    '                o.OutNL("GO")


    '                s = Nothing
    '                s = New Writer
    '                s.putBufLC(procDropSQL("alter_idx"))
    '                s.putBufLC("create procedure alter_idx() begin")
    '                s.putBufLC("if not exists (SELECT 1 ")
    '                s.putBufLC("  FROM(INFORMATION_SCHEMA.STATISTICS)")
    '                s.putBufLC("  WHERE(table_schema = DATABASE()) ")
    '                s.putBufLC("  AND   table_name   = '" + sTableName + "' ")
    '                s.putBufLC("  AND   index_name   = 'parent_" & sTableName + "' ) THEN")
    '                s.putBufLC("  create index parent_" & sTableName & " on " & sTableName & "(INSTANCEID);")
    '                s.putBufLC("End If;")
    '                s.putBufLC("End ")
    '                s.putBufLC("GO")
    '                s.putBufLC("call alter_idx(); ")
    '                s.putBufLC("GO")
    '                s.putBufLC(procDropSQL("alter_idx"))


    '                o.ModuleName = "--Tables"
    '                o.Block = "--Index"
    '                o.OutNL(s.getBuf)
    '                o.OutNL("GO")
    '            Next
    '        End If
    '        Exit Function
    'Error_Detected:
    '        MsgBox("CheckPartMLF:" & Err.Description)
    '    End Function

    Private Function IsMLFPart(ByRef os As bp3doc.bp3doc.bp3doc_store) As Boolean
        Dim j As Integer
        IsMLFPart = False
        Dim ft As bp3ft_def
        For j = 1 To os.bp3doc_field.Count
            ft = os.bp3doc_field.Item(j).FieldType
            If UCase(ft.Name) = UCase("MultiLanguage String") Or UCase(ft.Name) = UCase("MultiLanguage Memo") Then
                IsMLFPart = True
                Exit Function
            End If
        Next
    End Function

    Private Function IsMLFfield(ByRef f As bp3doc.bp3doc.bp3doc_field) As Boolean
        Dim ft As bp3ft_def
        ft = f.fieldtype
        If UCase(ft.Name) = UCase("MultiLanguage String") Or UCase(ft.Name) = UCase("MultiLanguage Memo") Then
            Return True
        End If
        Return False
    End Function
End Class