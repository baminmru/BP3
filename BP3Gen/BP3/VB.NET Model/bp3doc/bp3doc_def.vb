

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
'''Реализация строки раздела Описание
''' </summary>
''' <remarks>
'''
''' </remarks>
    Public Class bp3doc_def
        Inherits BP3.Document.DocRow_Base



''' <summary>
'''Локальная переменная для поля Архивировать вместо удаления
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_UseArchiving  as enumBoolean


''' <summary>
'''Локальная переменная для поля Название
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_TheCaption  as String


''' <summary>
'''Локальная переменная для поля Код
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_Name  as String


''' <summary>
'''Локальная переменная для поля Видмость зависит от пользователя
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_UseOwnership  as enumBoolean


''' <summary>
'''Локальная переменная для поля Допускается только один объект
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_IsSingleInstance  as enumBoolean


''' <summary>
'''Локальная переменная для поля Сохранять объект целиком
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_CommitFullObject  as enumBoolean


''' <summary>
'''Локальная переменная для поля Описание
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_TheComment  as STRING



''' <summary>
'''Очистить поля раздела
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Overrides Sub CleanFields()
            ' m_UseArchiving=   
            ' m_TheCaption=   
            ' m_Name=   
            ' m_UseOwnership=   
            ' m_IsSingleInstance=   
            ' m_CommitFullObject=   
            ' m_TheComment=   
        End Sub



''' <summary>
'''Количество полей в строке раздела
''' </summary>
''' <remarks>
'''
''' </remarks>
    Public Overrides ReadOnly Property Count() As Long
        Get
           Count = 7
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
                    Value = TheCaption
                Case 2
                    Value = Name
                Case 3
                    Value = IsSingleInstance
                Case 4
                    Value = TheComment
                Case 5
                    Value = UseOwnership
                Case 6
                    Value = UseArchiving
                Case 7
                    Value = CommitFullObject
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
                    TheCaption = value
                Case 2
                    Name = value
                Case 3
                    IsSingleInstance = value
                Case 4
                    TheComment = value
                Case 5
                    UseOwnership = value
                Case 6
                    UseArchiving = value
                Case 7
                    CommitFullObject = value
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
                    Return "TheCaption"
                Case 2
                    Return "Name"
                Case 3
                    Return "IsSingleInstance"
                Case 4
                    Return "TheComment"
                Case 5
                    Return "UseOwnership"
                Case 6
                    Return "UseArchiving"
                Case 7
                    Return "CommitFullObject"
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
             dr("TheCaption") =TheCaption
             dr("Name") =Name
             select case IsSingleInstance
            case enumBoolean.Boolean_Da
              dr ("IsSingleInstance")  = "Да"
              dr ("IsSingleInstance_VAL")  = -1
            case enumBoolean.Boolean_Net
              dr ("IsSingleInstance")  = "Нет"
              dr ("IsSingleInstance_VAL")  = 0
              end select 'IsSingleInstance
             dr("TheComment") =TheComment
             select case UseOwnership
            case enumBoolean.Boolean_Da
              dr ("UseOwnership")  = "Да"
              dr ("UseOwnership_VAL")  = -1
            case enumBoolean.Boolean_Net
              dr ("UseOwnership")  = "Нет"
              dr ("UseOwnership_VAL")  = 0
              end select 'UseOwnership
             select case UseArchiving
            case enumBoolean.Boolean_Da
              dr ("UseArchiving")  = "Да"
              dr ("UseArchiving_VAL")  = -1
            case enumBoolean.Boolean_Net
              dr ("UseArchiving")  = "Нет"
              dr ("UseArchiving_VAL")  = 0
              end select 'UseArchiving
             select case CommitFullObject
            case enumBoolean.Boolean_Da
              dr ("CommitFullObject")  = "Да"
              dr ("CommitFullObject_VAL")  = -1
            case enumBoolean.Boolean_Net
              dr ("CommitFullObject")  = "Нет"
              dr ("CommitFullObject_VAL")  = 0
              end select 'CommitFullObject
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
          nv.Add("TheCaption", TheCaption, dbtype.string)
          nv.Add("Name", Name, dbtype.string)
          nv.Add("IsSingleInstance", IsSingleInstance, dbtype.int16)
          nv.Add("TheComment", TheComment, dbtype.string)
          nv.Add("UseOwnership", UseOwnership, dbtype.int16)
          nv.Add("UseArchiving", UseArchiving, dbtype.int16)
          nv.Add("CommitFullObject", CommitFullObject, dbtype.int16)
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
          If reader.Table.Columns.Contains("TheCaption") Then m_TheCaption=reader.item("TheCaption").ToString()
          If reader.Table.Columns.Contains("Name") Then m_Name=reader.item("Name").ToString()
          If reader.Table.Columns.Contains("IsSingleInstance") Then m_IsSingleInstance=reader.item("IsSingleInstance")
          If reader.Table.Columns.Contains("TheComment") Then m_TheComment=reader.item("TheComment").ToString()
          If reader.Table.Columns.Contains("UseOwnership") Then m_UseOwnership=reader.item("UseOwnership")
          If reader.Table.Columns.Contains("UseArchiving") Then m_UseArchiving=reader.item("UseArchiving")
          If reader.Table.Columns.Contains("CommitFullObject") Then m_CommitFullObject=reader.item("CommitFullObject")
        End Sub


''' <summary>
'''Доступ к полю Название
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property TheCaption() As String
            Get
                LoadFromDatabase()
                TheCaption = m_TheCaption
                AccessTime = Now
            End Get
            Set(ByVal Value As String )
                LoadFromDatabase()
                m_TheCaption = Value
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю Код
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
'''Доступ к полю Допускается только один объект
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property IsSingleInstance() As enumBoolean
            Get
                LoadFromDatabase()
                IsSingleInstance = m_IsSingleInstance
                AccessTime = Now
            End Get
            Set(ByVal Value As enumBoolean )
                LoadFromDatabase()
                m_IsSingleInstance = Value
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
'''Доступ к полю Видмость зависит от пользователя
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property UseOwnership() As enumBoolean
            Get
                LoadFromDatabase()
                UseOwnership = m_UseOwnership
                AccessTime = Now
            End Get
            Set(ByVal Value As enumBoolean )
                LoadFromDatabase()
                m_UseOwnership = Value
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю Архивировать вместо удаления
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property UseArchiving() As enumBoolean
            Get
                LoadFromDatabase()
                UseArchiving = m_UseArchiving
                AccessTime = Now
            End Get
            Set(ByVal Value As enumBoolean )
                LoadFromDatabase()
                m_UseArchiving = Value
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю Сохранять объект целиком
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property CommitFullObject() As enumBoolean
            Get
                LoadFromDatabase()
                CommitFullObject = m_CommitFullObject
                AccessTime = Now
            End Get
            Set(ByVal Value As enumBoolean )
                LoadFromDatabase()
                m_CommitFullObject = Value
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
            TheCaption = node.Attributes.GetNamedItem("TheCaption").Value
            Name = node.Attributes.GetNamedItem("Name").Value
            IsSingleInstance = node.Attributes.GetNamedItem("IsSingleInstance").Value
            TheComment = node.Attributes.GetNamedItem("TheComment").Value
            UseOwnership = node.Attributes.GetNamedItem("UseOwnership").Value
            UseArchiving = node.Attributes.GetNamedItem("UseArchiving").Value
            CommitFullObject = node.Attributes.GetNamedItem("CommitFullObject").Value
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
          node.SetAttribute("TheCaption", TheCaption)  
          node.SetAttribute("Name", Name)  
          node.SetAttribute("IsSingleInstance", IsSingleInstance)  
          node.SetAttribute("TheComment", TheComment)  
          node.SetAttribute("UseOwnership", UseOwnership)  
          node.SetAttribute("UseArchiving", UseArchiving)  
          node.SetAttribute("CommitFullObject", CommitFullObject)  
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
