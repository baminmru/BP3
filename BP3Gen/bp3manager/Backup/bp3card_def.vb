

Option Explicit On

Imports System
Imports System.IO
Imports LATIR2
Imports System.xml
Imports System.Data
Imports System.Convert
Imports System.DateTime

Namespace bp3card


''' <summary>
'''Реализация строки раздела Карточка документа
''' </summary>
''' <remarks>
'''
''' </remarks>
    Public Class bp3card_def
        Inherits LATIR2.Document.DocRow_Base



''' <summary>
'''Локальная переменная для поля Главное представление
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_DefaultMode  as enumBoolean


''' <summary>
'''Локальная переменная для поля Описание
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_TheComment  as STRING


''' <summary>
'''Локальная переменная для поля Документ
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_CardFor  as System.Guid


''' <summary>
'''Локальная переменная для поля Иконка карточки
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_cardiconcls  as String


''' <summary>
'''Локальная переменная для поля Название карточки
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_Name  as String



''' <summary>
'''Очистить поля раздела
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Overrides Sub CleanFields()
            ' m_DefaultMode=   
            ' m_TheComment=   
            ' m_CardFor=   
            ' m_cardiconcls=   
            ' m_Name=   
        End Sub



''' <summary>
'''Количество полей в строке раздела
''' </summary>
''' <remarks>
'''
''' </remarks>
    Public Overrides ReadOnly Property Count() As Long
        Get
           Count = 5
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
                    Value = CardFor
                Case 2
                    Value = TheComment
                Case 3
                    Value = Name
                Case 4
                    Value = DefaultMode
                Case 5
                    Value = cardiconcls
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
                    CardFor = value
                Case 2
                    TheComment = value
                Case 3
                    Name = value
                Case 4
                    DefaultMode = value
                Case 5
                    cardiconcls = value
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
                    Return "CardFor"
                Case 2
                    Return "TheComment"
                Case 3
                    Return "Name"
                Case 4
                    Return "DefaultMode"
                Case 5
                    Return "cardiconcls"
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
             if CardFor is nothing then
               dr("CardFor") =system.dbnull.value
               dr("CardFor_ID") =System.Guid.Empty
             else
               dr("CardFor") =CardFor.BRIEF
               dr("CardFor_ID") =CardFor.ID
             end if 
             dr("TheComment") =TheComment
             dr("Name") =Name
             select case DefaultMode
            case enumBoolean.Boolean_Da
              dr ("DefaultMode")  = "Да"
              dr ("DefaultMode_VAL")  = -1
            case enumBoolean.Boolean_Net
              dr ("DefaultMode")  = "Нет"
              dr ("DefaultMode_VAL")  = 0
              end select 'DefaultMode
             dr("cardiconcls") =cardiconcls
            DestDataTable.Rows.Add (dr)
        End Sub



''' <summary>
'''Найти строку в коллекции по идентификатору
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Overrides Function FindInside(ByVal Table As String, ByVal RowID As String) As LATIR2.Document.DocRow_Base
            dim mFindInside As LATIR2.Document.DocRow_Base = Nothing
            Return Nothing
        End Function



''' <summary>
'''Заполнить коллекцю именованных полей
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Overrides Sub Pack(ByVal nv As LATIR2.NamedValues)
          if m_CardFor.Equals(System.Guid.Empty) then
            nv.Add("CardFor", system.dbnull.value, Application.Session.GetProvider.ID2DbType, Application.Session.GetProvider.ID2Size)
          else
            nv.Add("CardFor", Application.Session.GetProvider.ID2Param(m_CardFor), Application.Session.GetProvider.ID2DbType, Application.Session.GetProvider.ID2Size)
          end if 
          nv.Add("TheComment", TheComment, dbtype.string)
          nv.Add("Name", Name, dbtype.string)
          nv.Add("DefaultMode", DefaultMode, dbtype.int16)
          nv.Add("cardiconcls", cardiconcls, dbtype.string)
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
      If reader.Table.Columns.Contains("CardFor") Then
          if isdbnull(reader.item("CardFor")) then
            If reader.Table.Columns.Contains("CardFor") Then m_CardFor = System.GUID.Empty
          else
            If reader.Table.Columns.Contains("CardFor") Then m_CardFor= New System.Guid(reader.item("CardFor").ToString())
          end if 
      end if 
          If reader.Table.Columns.Contains("TheComment") Then m_TheComment=reader.item("TheComment").ToString()
          If reader.Table.Columns.Contains("Name") Then m_Name=reader.item("Name").ToString()
          If reader.Table.Columns.Contains("DefaultMode") Then m_DefaultMode=reader.item("DefaultMode")
          If reader.Table.Columns.Contains("cardiconcls") Then m_cardiconcls=reader.item("cardiconcls").ToString()
        End Sub


''' <summary>
'''Доступ к полю Документ
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property CardFor() As LATIR2.Document.docrow_base
            Get
                LoadFromDatabase()
                CardFor = me.application.Findrowobject("bp3doc_def",m_CardFor)
                AccessTime = Now
            End Get
            Set(ByVal Value As LATIR2.Document.docrow_base )
                LoadFromDatabase()
                if not Value is nothing then
                    m_CardFor = Value.id
                else
                   m_CardFor=System.Guid.Empty
                end if
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
'''Доступ к полю Название карточки
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
'''Доступ к полю Главное представление
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property DefaultMode() As enumBoolean
            Get
                LoadFromDatabase()
                DefaultMode = m_DefaultMode
                AccessTime = Now
            End Get
            Set(ByVal Value As enumBoolean )
                LoadFromDatabase()
                m_DefaultMode = Value
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю Иконка карточки
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property cardiconcls() As String
            Get
                LoadFromDatabase()
                cardiconcls = m_cardiconcls
                AccessTime = Now
            End Get
            Set(ByVal Value As String )
                LoadFromDatabase()
                m_cardiconcls = Value
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
            m_CardFor = new system.guid(node.Attributes.GetNamedItem("CardFor").Value)
            TheComment = node.Attributes.GetNamedItem("TheComment").Value
            Name = node.Attributes.GetNamedItem("Name").Value
            DefaultMode = node.Attributes.GetNamedItem("DefaultMode").Value
            cardiconcls = node.Attributes.GetNamedItem("cardiconcls").Value
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
          node.SetAttribute("CardFor", m_CardFor.tostring)  
          node.SetAttribute("TheComment", TheComment)  
          node.SetAttribute("Name", Name)  
          node.SetAttribute("DefaultMode", DefaultMode)  
          node.SetAttribute("cardiconcls", cardiconcls)  
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
        Public Overrides Function GetDocCollection_Base(ByVal Index As Long) As LATIR2.Document.DocCollection_Base
            Select Case Index
            End Select
            return nothing
        End Function
    End Class
End Namespace
