

Option Explicit On

Imports System
Imports System.IO
Imports BP3
Imports System.xml
Imports System.Data
Imports System.Convert
Imports System.DateTime

Namespace bp3doc


''' <summary>
'''Реализация строки раздела Поле
''' </summary>
''' <remarks>
'''
''' </remarks>
    Public Class bp3doc_field
        Inherits BP3.Document.DocRow_Base



''' <summary>
'''Локальная переменная для поля Имя поля
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_Name  as String


''' <summary>
'''Локальная переменная для поля Имя группы
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_FieldGroupBox  as String


''' <summary>
'''Локальная переменная для поля Размер поля
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_DataSize  as long


''' <summary>
'''Локальная переменная для поля Стиль
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_TheStyle  as String


''' <summary>
'''Локальная переменная для поля Имя вкладки
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_TabName  as String


''' <summary>
'''Локальная переменная для поля Маска
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_TheMask  as String


''' <summary>
'''Локальная переменная для поля Автонумерация
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_IsAutoNumber  as enumBoolean


''' <summary>
'''Локальная переменная для поля Описание
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_TheComment  as STRING


''' <summary>
'''Локальная переменная для поля Надпись
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_Caption  as String


''' <summary>
'''Локальная переменная для поля Для отображения в таблице
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_IsTabBrief  as enumBoolean


''' <summary>
'''Локальная переменная для поля № п/п
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_Sequence  as long


''' <summary>
'''Локальная переменная для поля Краткая информация
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_IsBrief  as enumBoolean


''' <summary>
'''Локальная переменная для поля Тип поля
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_FieldType  as System.Guid


''' <summary>
'''Локальная переменная для поля Тип ссылки
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_ReferenceType  as enumReferenceType


''' <summary>
'''Локальная переменная для поля Ссылка на раздел
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_RefToPart  as System.Guid


''' <summary>
'''Локальная переменная для поля Ссылка в пределах объекта
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_InternalReference  as enumBoolean


''' <summary>
'''Локальная переменная для поля Может быть пустым
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_AllowNull  as enumBoolean


''' <summary>
'''Локальная переменная для поля Шаблон для краткого отображения
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_shablonBrief  as String



''' <summary>
'''Очистить поля раздела
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Overrides Sub CleanFields()
            ' m_Name=   
            ' m_FieldGroupBox=   
            ' m_DataSize=   
            ' m_TheStyle=   
            ' m_TabName=   
            ' m_TheMask=   
            ' m_IsAutoNumber=   
            ' m_TheComment=   
            ' m_Caption=   
            ' m_IsTabBrief=   
            ' m_Sequence=   
            ' m_IsBrief=   
            ' m_FieldType=   
            ' m_ReferenceType=   
            ' m_RefToPart=   
            ' m_InternalReference=   
            ' m_AllowNull=   
            ' m_shablonBrief=   
        End Sub



''' <summary>
'''Количество полей в строке раздела
''' </summary>
''' <remarks>
'''
''' </remarks>
    Public Overrides ReadOnly Property Count() As Long
        Get
           Count = 18
        End Get
    End Property



''' <summary>
'''Получить \Задать поле по номеру 
''' </summary>
''' <remarks>
'''
''' </remarks>
Public Overrides Property Value(ByVal Index As Object) As Object
    Get
        If Microsoft.VisualBasic.IsNumeric(Index) Then
            Dim l As Long
            l = CLng(Index)
            Select Case l
                Case 0
                    Value = ID
                Case 1
                    Value = Sequence
                Case 2
                    Value = Name
                Case 3
                    Value = Caption
                Case 4
                    Value = TabName
                Case 5
                    Value = FieldGroupBox
                Case 6
                    Value = AllowNull
                Case 7
                    Value = FieldType
                Case 8
                    Value = ReferenceType
                Case 9
                    Value = DataSize
                Case 10
                    Value = RefToPart
                Case 11
                    Value = InternalReference
                Case 12
                    Value = TheComment
                Case 13
                    Value = IsAutoNumber
                Case 14
                    Value = IsBrief
                Case 15
                    Value = IsTabBrief
                Case 16
                    Value = TheStyle
                Case 17
                    Value = TheMask
                Case 18
                    Value = shablonBrief
            End Select
        else
        On Error Resume Next
        Value = Microsoft.VisualBasic.CallByName(Me, Index.ToString(), Microsoft.VisualBasic.CallType.Get, Nothing)
        End If
    End Get
    Set(ByVal value As Object)
    If Microsoft.VisualBasic.IsNumeric(Index) Then
        Dim l As Long
        l = CLng(Index)
        Select Case l
            Case 0
                 ID=value
                Case 1
                    Sequence = value
                Case 2
                    Name = value
                Case 3
                    Caption = value
                Case 4
                    TabName = value
                Case 5
                    FieldGroupBox = value
                Case 6
                    AllowNull = value
                Case 7
                    FieldType = value
                Case 8
                    ReferenceType = value
                Case 9
                    DataSize = value
                Case 10
                    RefToPart = value
                Case 11
                    InternalReference = value
                Case 12
                    TheComment = value
                Case 13
                    IsAutoNumber = value
                Case 14
                    IsBrief = value
                Case 15
                    IsTabBrief = value
                Case 16
                    TheStyle = value
                Case 17
                    TheMask = value
                Case 18
                    shablonBrief = value
        End Select
     Else
        Try
            Try
                Microsoft.VisualBasic.CallByName(Me, Index.ToString(), Microsoft.VisualBasic.CallType.Set, value)
            Catch ex As Exception
                Microsoft.VisualBasic.CallByName(Me, Index.ToString(), Microsoft.VisualBasic.CallType.Let, value)
            End Try
        Catch ex As Exception
        End Try
     End If
  End Set

End Property



''' <summary>
'''Название поля по номеру
''' </summary>
''' <remarks>
'''
''' </remarks>
Public Overrides Function FieldNameByID(ByVal Index As long) As String
        If Microsoft.VisualBasic.IsNumeric(Index) Then
            Dim l As Long
            l = CLng(Index)
            Select Case l
                Case 0
                   Return "ID"
                Case 1
                    Return "Sequence"
                Case 2
                    Return "Name"
                Case 3
                    Return "Caption"
                Case 4
                    Return "TabName"
                Case 5
                    Return "FieldGroupBox"
                Case 6
                    Return "AllowNull"
                Case 7
                    Return "FieldType"
                Case 8
                    Return "ReferenceType"
                Case 9
                    Return "DataSize"
                Case 10
                    Return "RefToPart"
                Case 11
                    Return "InternalReference"
                Case 12
                    Return "TheComment"
                Case 13
                    Return "IsAutoNumber"
                Case 14
                    Return "IsBrief"
                Case 15
                    Return "IsTabBrief"
                Case 16
                    Return "TheStyle"
                Case 17
                    Return "TheMask"
                Case 18
                    Return "shablonBrief"
                Case else
                return "" 
            End Select
        End If
        return "" 
End Function



''' <summary>
'''Заполнить строку таблицы данными из полей
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Overrides Sub FillDataTable(ByRef DestDataTable As System.Data.DataTable)
            Dim dr As  DataRow
            dr = destdatatable.NewRow
            on error resume next
            dr("ID") =ID
            dr("Brief") =Brief
             dr("Sequence") =Sequence
             dr("Name") =Name
             dr("Caption") =Caption
             dr("TabName") =TabName
             dr("FieldGroupBox") =FieldGroupBox
             select case AllowNull
            case enumBoolean.Boolean_Da
              dr ("AllowNull")  = "Да"
              dr ("AllowNull_VAL")  = -1
            case enumBoolean.Boolean_Net
              dr ("AllowNull")  = "Нет"
              dr ("AllowNull_VAL")  = 0
              end select 'AllowNull
             if FieldType is nothing then
               dr("FieldType") =system.dbnull.value
               dr("FieldType_ID") =System.Guid.Empty
             else
               dr("FieldType") =FieldType.BRIEF
               dr("FieldType_ID") =FieldType.ID
             end if 
             select case ReferenceType
            case enumReferenceType.ReferenceType_Na_istocnik_dannih
              dr ("ReferenceType")  = "На источник данных"
              dr ("ReferenceType_VAL")  = 3
            case enumReferenceType.ReferenceType_Skalyrnoe_pole_OPN_ne_ssilkaCLS
              dr ("ReferenceType")  = "Скалярное поле ( не ссылка)"
              dr ("ReferenceType_VAL")  = 0
            case enumReferenceType.ReferenceType_Na_stroku_razdela
              dr ("ReferenceType")  = "На строку раздела"
              dr ("ReferenceType_VAL")  = 2
            case enumReferenceType.ReferenceType_Na_ob_ekt_
              dr ("ReferenceType")  = "На объект "
              dr ("ReferenceType_VAL")  = 1
              end select 'ReferenceType
             dr("DataSize") =DataSize
             if RefToPart is nothing then
               dr("RefToPart") =system.dbnull.value
               dr("RefToPart_ID") =System.Guid.Empty
             else
               dr("RefToPart") =RefToPart.BRIEF
               dr("RefToPart_ID") =RefToPart.ID
             end if 
             select case InternalReference
            case enumBoolean.Boolean_Da
              dr ("InternalReference")  = "Да"
              dr ("InternalReference_VAL")  = -1
            case enumBoolean.Boolean_Net
              dr ("InternalReference")  = "Нет"
              dr ("InternalReference_VAL")  = 0
              end select 'InternalReference
             dr("TheComment") =TheComment
             select case IsAutoNumber
            case enumBoolean.Boolean_Da
              dr ("IsAutoNumber")  = "Да"
              dr ("IsAutoNumber_VAL")  = -1
            case enumBoolean.Boolean_Net
              dr ("IsAutoNumber")  = "Нет"
              dr ("IsAutoNumber_VAL")  = 0
              end select 'IsAutoNumber
             select case IsBrief
            case enumBoolean.Boolean_Da
              dr ("IsBrief")  = "Да"
              dr ("IsBrief_VAL")  = -1
            case enumBoolean.Boolean_Net
              dr ("IsBrief")  = "Нет"
              dr ("IsBrief_VAL")  = 0
              end select 'IsBrief
             select case IsTabBrief
            case enumBoolean.Boolean_Da
              dr ("IsTabBrief")  = "Да"
              dr ("IsTabBrief_VAL")  = -1
            case enumBoolean.Boolean_Net
              dr ("IsTabBrief")  = "Нет"
              dr ("IsTabBrief_VAL")  = 0
              end select 'IsTabBrief
             dr("TheStyle") =TheStyle
             dr("TheMask") =TheMask
             dr("shablonBrief") =shablonBrief
            DestDataTable.Rows.Add (dr)
        End Sub



''' <summary>
'''Найти строку в коллекции по идентификатору
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Overrides Function FindInside(ByVal Table As String, ByVal RowID As String) As BP3.Document.DocRow_Base
            dim mFindInside As BP3.Document.DocRow_Base = Nothing
            Return Nothing
        End Function



''' <summary>
'''Заполнить коллекцю именованных полей
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Overrides Sub Pack(ByVal nv As BP3.NamedValues)
          nv.Add("Sequence", Sequence, dbtype.Int32)
          nv.Add("Name", Name, dbtype.string)
          nv.Add("Caption", Caption, dbtype.string)
          nv.Add("TabName", TabName, dbtype.string)
          nv.Add("FieldGroupBox", FieldGroupBox, dbtype.string)
          nv.Add("AllowNull", AllowNull, dbtype.int16)
          if m_FieldType.Equals(System.Guid.Empty) then
            nv.Add("FieldType", system.dbnull.value, Application.Session.GetProvider.ID2DbType, Application.Session.GetProvider.ID2Size)
          else
            nv.Add("FieldType", Application.Session.GetProvider.ID2Param(m_FieldType), Application.Session.GetProvider.ID2DbType, Application.Session.GetProvider.ID2Size)
          end if 
          nv.Add("ReferenceType", ReferenceType, dbtype.int16)
          nv.Add("DataSize", DataSize, dbtype.Int32)
          if m_RefToPart.Equals(System.Guid.Empty) then
            nv.Add("RefToPart", system.dbnull.value, Application.Session.GetProvider.ID2DbType, Application.Session.GetProvider.ID2Size)
          else
            nv.Add("RefToPart", Application.Session.GetProvider.ID2Param(m_RefToPart), Application.Session.GetProvider.ID2DbType, Application.Session.GetProvider.ID2Size)
          end if 
          nv.Add("InternalReference", InternalReference, dbtype.int16)
          nv.Add("TheComment", TheComment, dbtype.string)
          nv.Add("IsAutoNumber", IsAutoNumber, dbtype.int16)
          nv.Add("IsBrief", IsBrief, dbtype.int16)
          nv.Add("IsTabBrief", IsTabBrief, dbtype.int16)
          nv.Add("TheStyle", TheStyle, dbtype.string)
          nv.Add("TheMask", TheMask, dbtype.string)
          nv.Add("shablonBrief", shablonBrief, dbtype.string)
            nv.Add(PartName() & "id", Application.Session.GetProvider.ID2Param(ID),  Application.Session.GetProvider.ID2DbType, Application.Session.GetProvider.ID2Size)
        End Sub




''' <summary>
'''Заполнить поля из именованной коллекции
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Overrides Sub Unpack(ByVal reader As System.Data.DataRow)
            on error resume next  
            If IsDBNull(reader.item("SecurityStyleID")) Then
                SecureStyleID = System.guid.Empty
            Else
                SecureStyleID = new Guid(reader.item("SecurityStyleID").ToString())
            End If

            RowRetrived = True
            RetriveTime = Now
          If reader.Table.Columns.Contains("Sequence") Then m_Sequence=reader.item("Sequence")
          If reader.Table.Columns.Contains("Name") Then m_Name=reader.item("Name").ToString()
          If reader.Table.Columns.Contains("Caption") Then m_Caption=reader.item("Caption").ToString()
          If reader.Table.Columns.Contains("TabName") Then m_TabName=reader.item("TabName").ToString()
          If reader.Table.Columns.Contains("FieldGroupBox") Then m_FieldGroupBox=reader.item("FieldGroupBox").ToString()
          If reader.Table.Columns.Contains("AllowNull") Then m_AllowNull=reader.item("AllowNull")
      If reader.Table.Columns.Contains("FieldType") Then
          if isdbnull(reader.item("FieldType")) then
            If reader.Table.Columns.Contains("FieldType") Then m_FieldType = System.GUID.Empty
          else
            If reader.Table.Columns.Contains("FieldType") Then m_FieldType= New System.Guid(reader.item("FieldType").ToString())
          end if 
      end if 
          If reader.Table.Columns.Contains("ReferenceType") Then m_ReferenceType=reader.item("ReferenceType")
          If reader.Table.Columns.Contains("DataSize") Then m_DataSize=reader.item("DataSize")
      If reader.Table.Columns.Contains("RefToPart") Then
          if isdbnull(reader.item("RefToPart")) then
            If reader.Table.Columns.Contains("RefToPart") Then m_RefToPart = System.GUID.Empty
          else
            If reader.Table.Columns.Contains("RefToPart") Then m_RefToPart= New System.Guid(reader.item("RefToPart").ToString())
          end if 
      end if 
          If reader.Table.Columns.Contains("InternalReference") Then m_InternalReference=reader.item("InternalReference")
          If reader.Table.Columns.Contains("TheComment") Then m_TheComment=reader.item("TheComment").ToString()
          If reader.Table.Columns.Contains("IsAutoNumber") Then m_IsAutoNumber=reader.item("IsAutoNumber")
          If reader.Table.Columns.Contains("IsBrief") Then m_IsBrief=reader.item("IsBrief")
          If reader.Table.Columns.Contains("IsTabBrief") Then m_IsTabBrief=reader.item("IsTabBrief")
          If reader.Table.Columns.Contains("TheStyle") Then m_TheStyle=reader.item("TheStyle").ToString()
          If reader.Table.Columns.Contains("TheMask") Then m_TheMask=reader.item("TheMask").ToString()
          If reader.Table.Columns.Contains("shablonBrief") Then m_shablonBrief=reader.item("shablonBrief").ToString()
        End Sub


''' <summary>
'''Доступ к полю № п/п
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property Sequence() As long
            Get
                LoadFromDatabase()
                Sequence = m_Sequence
                AccessTime = Now
            End Get
            Set(ByVal Value As long )
                LoadFromDatabase()
                m_Sequence = Value
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю Имя поля
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property Name() As String
            Get
                LoadFromDatabase()
                Name = m_Name
                AccessTime = Now
            End Get
            Set(ByVal Value As String )
                LoadFromDatabase()
                m_Name = Value
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю Надпись
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property Caption() As String
            Get
                LoadFromDatabase()
                Caption = m_Caption
                AccessTime = Now
            End Get
            Set(ByVal Value As String )
                LoadFromDatabase()
                m_Caption = Value
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю Имя вкладки
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property TabName() As String
            Get
                LoadFromDatabase()
                TabName = m_TabName
                AccessTime = Now
            End Get
            Set(ByVal Value As String )
                LoadFromDatabase()
                m_TabName = Value
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю Имя группы
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property FieldGroupBox() As String
            Get
                LoadFromDatabase()
                FieldGroupBox = m_FieldGroupBox
                AccessTime = Now
            End Get
            Set(ByVal Value As String )
                LoadFromDatabase()
                m_FieldGroupBox = Value
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю Может быть пустым
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property AllowNull() As enumBoolean
            Get
                LoadFromDatabase()
                AllowNull = m_AllowNull
                AccessTime = Now
            End Get
            Set(ByVal Value As enumBoolean )
                LoadFromDatabase()
                m_AllowNull = Value
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю Тип поля
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property FieldType() As BP3.Document.docrow_base
            Get
                LoadFromDatabase()
                FieldType = me.application.Findrowobject("bp3ft_def",m_FieldType)
                AccessTime = Now
            End Get
            Set(ByVal Value As BP3.Document.docrow_base )
                LoadFromDatabase()
                if not Value is nothing then
                    m_FieldType = Value.id
                else
                   m_FieldType=System.Guid.Empty
                end if
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю Тип ссылки
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property ReferenceType() As enumReferenceType
            Get
                LoadFromDatabase()
                ReferenceType = m_ReferenceType
                AccessTime = Now
            End Get
            Set(ByVal Value As enumReferenceType )
                LoadFromDatabase()
                m_ReferenceType = Value
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю Размер поля
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property DataSize() As long
            Get
                LoadFromDatabase()
                DataSize = m_DataSize
                AccessTime = Now
            End Get
            Set(ByVal Value As long )
                LoadFromDatabase()
                m_DataSize = Value
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю Ссылка на раздел
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property RefToPart() As BP3.Document.docrow_base
            Get
                LoadFromDatabase()
                RefToPart = me.application.Findrowobject("bp3doc_store",m_RefToPart)
                AccessTime = Now
            End Get
            Set(ByVal Value As BP3.Document.docrow_base )
                LoadFromDatabase()
                if not Value is nothing then
                    m_RefToPart = Value.id
                else
                   m_RefToPart=System.Guid.Empty
                end if
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю Ссылка в пределах объекта
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property InternalReference() As enumBoolean
            Get
                LoadFromDatabase()
                InternalReference = m_InternalReference
                AccessTime = Now
            End Get
            Set(ByVal Value As enumBoolean )
                LoadFromDatabase()
                m_InternalReference = Value
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю Описание
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property TheComment() As STRING
            Get
                LoadFromDatabase()
                TheComment = m_TheComment
                AccessTime = Now
            End Get
            Set(ByVal Value As STRING )
                LoadFromDatabase()
                m_TheComment = Value
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю Автонумерация
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property IsAutoNumber() As enumBoolean
            Get
                LoadFromDatabase()
                IsAutoNumber = m_IsAutoNumber
                AccessTime = Now
            End Get
            Set(ByVal Value As enumBoolean )
                LoadFromDatabase()
                m_IsAutoNumber = Value
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю Краткая информация
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property IsBrief() As enumBoolean
            Get
                LoadFromDatabase()
                IsBrief = m_IsBrief
                AccessTime = Now
            End Get
            Set(ByVal Value As enumBoolean )
                LoadFromDatabase()
                m_IsBrief = Value
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю Для отображения в таблице
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property IsTabBrief() As enumBoolean
            Get
                LoadFromDatabase()
                IsTabBrief = m_IsTabBrief
                AccessTime = Now
            End Get
            Set(ByVal Value As enumBoolean )
                LoadFromDatabase()
                m_IsTabBrief = Value
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю Стиль
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property TheStyle() As String
            Get
                LoadFromDatabase()
                TheStyle = m_TheStyle
                AccessTime = Now
            End Get
            Set(ByVal Value As String )
                LoadFromDatabase()
                m_TheStyle = Value
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю Маска
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property TheMask() As String
            Get
                LoadFromDatabase()
                TheMask = m_TheMask
                AccessTime = Now
            End Get
            Set(ByVal Value As String )
                LoadFromDatabase()
                m_TheMask = Value
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю Шаблон для краткого отображения
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property shablonBrief() As String
            Get
                LoadFromDatabase()
                shablonBrief = m_shablonBrief
                AccessTime = Now
            End Get
            Set(ByVal Value As String )
                LoadFromDatabase()
                m_shablonBrief = Value
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Заполнить поля данными из XML
''' </summary>
''' <remarks>
'''
''' </remarks>
        Protected Overrides sub XMLUnpack(ByVal node As System.Xml.XmlNode, Optional ByVal LoadMode As Integer = 0)
          Dim e_list As XmlNodeList
          on error resume next  
            Sequence = node.Attributes.GetNamedItem("Sequence").Value
            Name = node.Attributes.GetNamedItem("Name").Value
            Caption = node.Attributes.GetNamedItem("Caption").Value
            TabName = node.Attributes.GetNamedItem("TabName").Value
            FieldGroupBox = node.Attributes.GetNamedItem("FieldGroupBox").Value
            AllowNull = node.Attributes.GetNamedItem("AllowNull").Value
            m_FieldType = new system.guid(node.Attributes.GetNamedItem("FieldType").Value)
            ReferenceType = node.Attributes.GetNamedItem("ReferenceType").Value
            DataSize = node.Attributes.GetNamedItem("DataSize").Value
            m_RefToPart = new system.guid(node.Attributes.GetNamedItem("RefToPart").Value)
            InternalReference = node.Attributes.GetNamedItem("InternalReference").Value
            TheComment = node.Attributes.GetNamedItem("TheComment").Value
            IsAutoNumber = node.Attributes.GetNamedItem("IsAutoNumber").Value
            IsBrief = node.Attributes.GetNamedItem("IsBrief").Value
            IsTabBrief = node.Attributes.GetNamedItem("IsTabBrief").Value
            TheStyle = node.Attributes.GetNamedItem("TheStyle").Value
            TheMask = node.Attributes.GetNamedItem("TheMask").Value
            shablonBrief = node.Attributes.GetNamedItem("shablonBrief").Value
             Changed = true
        End sub
        Public Overrides Sub Dispose()
        End Sub


''' <summary>
'''Записать данные раздела в XML файл
''' </summary>
''' <remarks>
'''
''' </remarks>
        Protected Overrides sub XLMPack(ByVal node As System.Xml.XmlElement, ByVal Xdom As System.Xml.XmlDocument)
           on error resume next  
          node.SetAttribute("Sequence", Sequence)  
          node.SetAttribute("Name", Name)  
          node.SetAttribute("Caption", Caption)  
          node.SetAttribute("TabName", TabName)  
          node.SetAttribute("FieldGroupBox", FieldGroupBox)  
          node.SetAttribute("AllowNull", AllowNull)  
          node.SetAttribute("FieldType", m_FieldType.tostring)  
          node.SetAttribute("ReferenceType", ReferenceType)  
          node.SetAttribute("DataSize", DataSize)  
          node.SetAttribute("RefToPart", m_RefToPart.tostring)  
          node.SetAttribute("InternalReference", InternalReference)  
          node.SetAttribute("TheComment", TheComment)  
          node.SetAttribute("IsAutoNumber", IsAutoNumber)  
          node.SetAttribute("IsBrief", IsBrief)  
          node.SetAttribute("IsTabBrief", IsTabBrief)  
          node.SetAttribute("TheStyle", TheStyle)  
          node.SetAttribute("TheMask", TheMask)  
          node.SetAttribute("shablonBrief", shablonBrief)  
        End sub


''' <summary>
'''Записать изменения в базу данных
''' </summary>
''' <remarks>
'''
''' </remarks>
Public Overrides Sub BatchUpdate()
  If Deleted Then
    Delete
    Exit Sub
  End If
  If Changed Then Save
End Sub


''' <summary>
'''Количество дочерних разделов
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Overrides ReadOnly Property CountOfParts() As Long
            Get
                CountOfParts = 0
            End Get
        End Property



''' <summary>
'''Доступ к дочернему разделу по номеру
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Overrides Function GetDocCollection_Base(ByVal Index As Long) As BP3.Document.DocCollection_Base
            Select Case Index
            End Select
            return nothing
        End Function
    End Class
End Namespace
