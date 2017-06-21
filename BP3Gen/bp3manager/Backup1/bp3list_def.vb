

Option Explicit On

Imports System
Imports System.IO
Imports LATIR2
Imports System.xml
Imports System.Data
Imports System.Convert
Imports System.DateTime

Namespace bp3list


''' <summary>
'''Реализация строки раздела Журнал
''' </summary>
''' <remarks>
'''
''' </remarks>
    Public Class bp3list_def
        Inherits LATIR2.Document.DocRow_Base



''' <summary>
'''Локальная переменная для поля Карточка для создания
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_NewCard  as System.Guid


''' <summary>
'''Локальная переменная для поля При открытии
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_OnRun  as enumOnJournalRowClick


''' <summary>
'''Локальная переменная для поля Название
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_Name  as String


''' <summary>
'''Локальная переменная для поля Описание
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_TheComment  as STRING


''' <summary>
'''Локальная переменная для поля Запрос
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_SourceView  as System.Guid


''' <summary>
'''Локальная переменная для поля Карточка для редактирования
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_EditCard  as System.Guid



''' <summary>
'''Очистить поля раздела
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Overrides Sub CleanFields()
            ' m_NewCard=   
            ' m_OnRun=   
            ' m_Name=   
            ' m_TheComment=   
            ' m_SourceView=   
            ' m_EditCard=   
        End Sub



''' <summary>
'''Количество полей в строке раздела
''' </summary>
''' <remarks>
'''
''' </remarks>
    Public Overrides ReadOnly Property Count() As Long
        Get
           Count = 6
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
                    Value = Name
                Case 2
                    Value = TheComment
                Case 3
                    Value = SourceView
                Case 4
                    Value = OnRun
                Case 5
                    Value = EditCard
                Case 6
                    Value = NewCard
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
                    Name = value
                Case 2
                    TheComment = value
                Case 3
                    SourceView = value
                Case 4
                    OnRun = value
                Case 5
                    EditCard = value
                Case 6
                    NewCard = value
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
                    Return "Name"
                Case 2
                    Return "TheComment"
                Case 3
                    Return "SourceView"
                Case 4
                    Return "OnRun"
                Case 5
                    Return "EditCard"
                Case 6
                    Return "NewCard"
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
             dr("Name") =Name
             dr("TheComment") =TheComment
             if SourceView is nothing then
               dr("SourceView") =system.dbnull.value
               dr("SourceView_ID") =System.Guid.Empty
             else
               dr("SourceView") =SourceView.BRIEF
               dr("SourceView_ID") =SourceView.ID
             end if 
             select case OnRun
            case enumOnJournalRowClick.OnJournalRowClick_Otkrit__dokument
              dr ("OnRun")  = "Открыть документ"
              dr ("OnRun_VAL")  = 2
            case enumOnJournalRowClick.OnJournalRowClick_Nicego_ne_delat_
              dr ("OnRun")  = "Ничего не делать"
              dr ("OnRun_VAL")  = 0
            case enumOnJournalRowClick.OnJournalRowClick_Otkrit__stroku
              dr ("OnRun")  = "Открыть строку"
              dr ("OnRun_VAL")  = 1
              end select 'OnRun
             if EditCard is nothing then
               dr("EditCard") =system.dbnull.value
               dr("EditCard_ID") =System.Guid.Empty
             else
               dr("EditCard") =EditCard.BRIEF
               dr("EditCard_ID") =EditCard.ID
             end if 
             if NewCard is nothing then
               dr("NewCard") =system.dbnull.value
               dr("NewCard_ID") =System.Guid.Empty
             else
               dr("NewCard") =NewCard.BRIEF
               dr("NewCard_ID") =NewCard.ID
             end if 
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
          nv.Add("Name", Name, dbtype.string)
          nv.Add("TheComment", TheComment, dbtype.string)
          if m_SourceView.Equals(System.Guid.Empty) then
            nv.Add("SourceView", system.dbnull.value, Application.Session.GetProvider.ID2DbType, Application.Session.GetProvider.ID2Size)
          else
            nv.Add("SourceView", Application.Session.GetProvider.ID2Param(m_SourceView), Application.Session.GetProvider.ID2DbType, Application.Session.GetProvider.ID2Size)
          end if 
          nv.Add("OnRun", OnRun, dbtype.int16)
          if m_EditCard.Equals(System.Guid.Empty) then
            nv.Add("EditCard", system.dbnull.value, Application.Session.GetProvider.ID2DbType, Application.Session.GetProvider.ID2Size)
          else
            nv.Add("EditCard", Application.Session.GetProvider.ID2Param(m_EditCard), Application.Session.GetProvider.ID2DbType, Application.Session.GetProvider.ID2Size)
          end if 
          if m_NewCard.Equals(System.Guid.Empty) then
            nv.Add("NewCard", system.dbnull.value, Application.Session.GetProvider.ID2DbType, Application.Session.GetProvider.ID2Size)
          else
            nv.Add("NewCard", Application.Session.GetProvider.ID2Param(m_NewCard), Application.Session.GetProvider.ID2DbType, Application.Session.GetProvider.ID2Size)
          end if 
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
          If reader.Table.Columns.Contains("Name") Then m_Name=reader.item("Name").ToString()
          If reader.Table.Columns.Contains("TheComment") Then m_TheComment=reader.item("TheComment").ToString()
      If reader.Table.Columns.Contains("SourceView") Then
          if isdbnull(reader.item("SourceView")) then
            If reader.Table.Columns.Contains("SourceView") Then m_SourceView = System.GUID.Empty
          else
            If reader.Table.Columns.Contains("SourceView") Then m_SourceView= New System.Guid(reader.item("SourceView").ToString())
          end if 
      end if 
          If reader.Table.Columns.Contains("OnRun") Then m_OnRun=reader.item("OnRun")
      If reader.Table.Columns.Contains("EditCard") Then
          if isdbnull(reader.item("EditCard")) then
            If reader.Table.Columns.Contains("EditCard") Then m_EditCard = System.GUID.Empty
          else
            If reader.Table.Columns.Contains("EditCard") Then m_EditCard= New System.Guid(reader.item("EditCard").ToString())
          end if 
      end if 
      If reader.Table.Columns.Contains("NewCard") Then
          if isdbnull(reader.item("NewCard")) then
            If reader.Table.Columns.Contains("NewCard") Then m_NewCard = System.GUID.Empty
          else
            If reader.Table.Columns.Contains("NewCard") Then m_NewCard= New System.Guid(reader.item("NewCard").ToString())
          end if 
      end if 
        End Sub


''' <summary>
'''Доступ к полю Название
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
'''Доступ к полю Запрос
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property SourceView() As LATIR2.Document.docrow_base
            Get
                LoadFromDatabase()
                SourceView = me.application.Findrowobject("bp3qry_def",m_SourceView)
                AccessTime = Now
            End Get
            Set(ByVal Value As LATIR2.Document.docrow_base )
                LoadFromDatabase()
                if not Value is nothing then
                    m_SourceView = Value.id
                else
                   m_SourceView=System.Guid.Empty
                end if
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю При открытии
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property OnRun() As enumOnJournalRowClick
            Get
                LoadFromDatabase()
                OnRun = m_OnRun
                AccessTime = Now
            End Get
            Set(ByVal Value As enumOnJournalRowClick )
                LoadFromDatabase()
                m_OnRun = Value
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю Карточка для редактирования
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property EditCard() As LATIR2.Document.docrow_base
            Get
                LoadFromDatabase()
                EditCard = me.application.Findrowobject("bp3card_def",m_EditCard)
                AccessTime = Now
            End Get
            Set(ByVal Value As LATIR2.Document.docrow_base )
                LoadFromDatabase()
                if not Value is nothing then
                    m_EditCard = Value.id
                else
                   m_EditCard=System.Guid.Empty
                end if
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю Карточка для создания
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property NewCard() As LATIR2.Document.docrow_base
            Get
                LoadFromDatabase()
                NewCard = me.application.Findrowobject("bp3card_def",m_NewCard)
                AccessTime = Now
            End Get
            Set(ByVal Value As LATIR2.Document.docrow_base )
                LoadFromDatabase()
                if not Value is nothing then
                    m_NewCard = Value.id
                else
                   m_NewCard=System.Guid.Empty
                end if
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
            Name = node.Attributes.GetNamedItem("Name").Value
            TheComment = node.Attributes.GetNamedItem("TheComment").Value
            m_SourceView = new system.guid(node.Attributes.GetNamedItem("SourceView").Value)
            OnRun = node.Attributes.GetNamedItem("OnRun").Value
            m_EditCard = new system.guid(node.Attributes.GetNamedItem("EditCard").Value)
            m_NewCard = new system.guid(node.Attributes.GetNamedItem("NewCard").Value)
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
          node.SetAttribute("Name", Name)  
          node.SetAttribute("TheComment", TheComment)  
          node.SetAttribute("SourceView", m_SourceView.tostring)  
          node.SetAttribute("OnRun", OnRun)  
          node.SetAttribute("EditCard", m_EditCard.tostring)  
          node.SetAttribute("NewCard", m_NewCard.tostring)  
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
