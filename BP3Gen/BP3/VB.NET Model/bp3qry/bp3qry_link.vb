

Option Explicit On

Imports System
Imports System.IO
Imports BP3
Imports System.xml
Imports System.Data
Imports System.Convert
Imports System.DateTime

Namespace bp3qry


''' <summary>
'''Реализация строки раздела Связанные представления
''' </summary>
''' <remarks>
'''
''' </remarks>
    Public Class bp3qry_link
        Inherits BP3.Document.DocRow_Base



''' <summary>
'''Локальная переменная для поля Ручной join
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_HandJoin  as String


''' <summary>
'''Локальная переменная для поля Связь: Поле для join источник
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_TheJoinSource  as System.Guid


''' <summary>
'''Локальная переменная для поля Свзяь: Поле для join приемник
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_TheJoinDestination  as System.Guid


''' <summary>
'''Локальная переменная для поля Порядок
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_SEQ  as long


''' <summary>
'''Локальная переменная для поля Связывать как
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_RefType  as enumJournalLinkType


''' <summary>
'''Локальная переменная для поля Представление
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_TheView  as System.Guid



''' <summary>
'''Очистить поля раздела
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Overrides Sub CleanFields()
            ' m_HandJoin=   
            ' m_TheJoinSource=   
            ' m_TheJoinDestination=   
            ' m_SEQ=   
            ' m_RefType=   
            ' m_TheView=   
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
                    Value = TheJoinDestination
                Case 2
                    Value = HandJoin
                Case 3
                    Value = SEQ
                Case 4
                    Value = TheJoinSource
                Case 5
                    Value = TheView
                Case 6
                    Value = RefType
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
                    TheJoinDestination = value
                Case 2
                    HandJoin = value
                Case 3
                    SEQ = value
                Case 4
                    TheJoinSource = value
                Case 5
                    TheView = value
                Case 6
                    RefType = value
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
                    Return "TheJoinDestination"
                Case 2
                    Return "HandJoin"
                Case 3
                    Return "SEQ"
                Case 4
                    Return "TheJoinSource"
                Case 5
                    Return "TheView"
                Case 6
                    Return "RefType"
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
             if TheJoinDestination is nothing then
               dr("TheJoinDestination") =system.dbnull.value
               dr("TheJoinDestination_ID") =System.Guid.Empty
             else
               dr("TheJoinDestination") =TheJoinDestination.BRIEF
               dr("TheJoinDestination_ID") =TheJoinDestination.ID
             end if 
             dr("HandJoin") =HandJoin
             dr("SEQ") =SEQ
             if TheJoinSource is nothing then
               dr("TheJoinSource") =system.dbnull.value
               dr("TheJoinSource_ID") =System.Guid.Empty
             else
               dr("TheJoinSource") =TheJoinSource.BRIEF
               dr("TheJoinSource_ID") =TheJoinSource.ID
             end if 
             if TheView is nothing then
               dr("TheView") =system.dbnull.value
               dr("TheView_ID") =System.Guid.Empty
             else
               dr("TheView") =TheView.BRIEF
               dr("TheView_ID") =TheView.ID
             end if 
             select case RefType
            case enumJournalLinkType.JournalLinkType_Net
              dr ("RefType")  = "Нет"
              dr ("RefType_VAL")  = 0
            case enumJournalLinkType.JournalLinkType_Svyzka_ParentStructRowID__OPNv_peredlah_ob_ektaCLS
              dr ("RefType")  = "Связка ParentStructRowID  (в передлах объекта)"
              dr ("RefType_VAL")  = 4
            case enumJournalLinkType.JournalLinkType_Svyzka_InstanceID_OPNv_peredlah_ob_ektaCLS
              dr ("RefType")  = "Связка InstanceID (в передлах объекта)"
              dr ("RefType_VAL")  = 3
            case enumJournalLinkType.JournalLinkType_Ssilka_na_ob_ekt
              dr ("RefType")  = "Ссылка на объект"
              dr ("RefType_VAL")  = 1
            case enumJournalLinkType.JournalLinkType_Ssilka_na_stroku
              dr ("RefType")  = "Ссылка на строку"
              dr ("RefType_VAL")  = 2
              end select 'RefType
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
          if m_TheJoinDestination.Equals(System.Guid.Empty) then
            nv.Add("TheJoinDestination", system.dbnull.value, Application.Session.GetProvider.ID2DbType, Application.Session.GetProvider.ID2Size)
          else
            nv.Add("TheJoinDestination", Application.Session.GetProvider.ID2Param(m_TheJoinDestination), Application.Session.GetProvider.ID2DbType, Application.Session.GetProvider.ID2Size)
          end if 
          nv.Add("HandJoin", HandJoin, dbtype.string)
          nv.Add("SEQ", SEQ, dbtype.Int32)
          if m_TheJoinSource.Equals(System.Guid.Empty) then
            nv.Add("TheJoinSource", system.dbnull.value, Application.Session.GetProvider.ID2DbType, Application.Session.GetProvider.ID2Size)
          else
            nv.Add("TheJoinSource", Application.Session.GetProvider.ID2Param(m_TheJoinSource), Application.Session.GetProvider.ID2DbType, Application.Session.GetProvider.ID2Size)
          end if 
          if m_TheView.Equals(System.Guid.Empty) then
            nv.Add("TheView", system.dbnull.value, Application.Session.GetProvider.ID2DbType, Application.Session.GetProvider.ID2Size)
          else
            nv.Add("TheView", Application.Session.GetProvider.ID2Param(m_TheView), Application.Session.GetProvider.ID2DbType, Application.Session.GetProvider.ID2Size)
          end if 
          nv.Add("RefType", RefType, dbtype.int16)
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
      If reader.Table.Columns.Contains("TheJoinDestination") Then
          if isdbnull(reader.item("TheJoinDestination")) then
            If reader.Table.Columns.Contains("TheJoinDestination") Then m_TheJoinDestination = System.GUID.Empty
          else
            If reader.Table.Columns.Contains("TheJoinDestination") Then m_TheJoinDestination= New System.Guid(reader.item("TheJoinDestination").ToString())
          end if 
      end if 
          If reader.Table.Columns.Contains("HandJoin") Then m_HandJoin=reader.item("HandJoin").ToString()
          If reader.Table.Columns.Contains("SEQ") Then m_SEQ=reader.item("SEQ")
      If reader.Table.Columns.Contains("TheJoinSource") Then
          if isdbnull(reader.item("TheJoinSource")) then
            If reader.Table.Columns.Contains("TheJoinSource") Then m_TheJoinSource = System.GUID.Empty
          else
            If reader.Table.Columns.Contains("TheJoinSource") Then m_TheJoinSource= New System.Guid(reader.item("TheJoinSource").ToString())
          end if 
      end if 
      If reader.Table.Columns.Contains("TheView") Then
          if isdbnull(reader.item("TheView")) then
            If reader.Table.Columns.Contains("TheView") Then m_TheView = System.GUID.Empty
          else
            If reader.Table.Columns.Contains("TheView") Then m_TheView= New System.Guid(reader.item("TheView").ToString())
          end if 
      end if 
          If reader.Table.Columns.Contains("RefType") Then m_RefType=reader.item("RefType")
        End Sub


''' <summary>
'''Доступ к полю Свзяь: Поле для join приемник
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property TheJoinDestination() As BP3.Document.docrow_base
            Get
                LoadFromDatabase()
                TheJoinDestination = me.application.Findrowobject("bp3qry_column",m_TheJoinDestination)
                AccessTime = Now
            End Get
            Set(ByVal Value As BP3.Document.docrow_base )
                LoadFromDatabase()
                if not Value is nothing then
                    m_TheJoinDestination = Value.id
                else
                   m_TheJoinDestination=System.Guid.Empty
                end if
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю Ручной join
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property HandJoin() As String
            Get
                LoadFromDatabase()
                HandJoin = m_HandJoin
                AccessTime = Now
            End Get
            Set(ByVal Value As String )
                LoadFromDatabase()
                m_HandJoin = Value
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю Порядок
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property SEQ() As long
            Get
                LoadFromDatabase()
                SEQ = m_SEQ
                AccessTime = Now
            End Get
            Set(ByVal Value As long )
                LoadFromDatabase()
                m_SEQ = Value
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю Связь: Поле для join источник
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property TheJoinSource() As BP3.Document.docrow_base
            Get
                LoadFromDatabase()
                TheJoinSource = me.application.Findrowobject("bp3qry_column",m_TheJoinSource)
                AccessTime = Now
            End Get
            Set(ByVal Value As BP3.Document.docrow_base )
                LoadFromDatabase()
                if not Value is nothing then
                    m_TheJoinSource = Value.id
                else
                   m_TheJoinSource=System.Guid.Empty
                end if
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю Представление
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property TheView() As BP3.Document.docrow_base
            Get
                LoadFromDatabase()
                TheView = me.application.Findrowobject("bp3qry_def",m_TheView)
                AccessTime = Now
            End Get
            Set(ByVal Value As BP3.Document.docrow_base )
                LoadFromDatabase()
                if not Value is nothing then
                    m_TheView = Value.id
                else
                   m_TheView=System.Guid.Empty
                end if
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю Связывать как
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property RefType() As enumJournalLinkType
            Get
                LoadFromDatabase()
                RefType = m_RefType
                AccessTime = Now
            End Get
            Set(ByVal Value As enumJournalLinkType )
                LoadFromDatabase()
                m_RefType = Value
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
            m_TheJoinDestination = new system.guid(node.Attributes.GetNamedItem("TheJoinDestination").Value)
            HandJoin = node.Attributes.GetNamedItem("HandJoin").Value
            SEQ = node.Attributes.GetNamedItem("SEQ").Value
            m_TheJoinSource = new system.guid(node.Attributes.GetNamedItem("TheJoinSource").Value)
            m_TheView = new system.guid(node.Attributes.GetNamedItem("TheView").Value)
            RefType = node.Attributes.GetNamedItem("RefType").Value
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
          node.SetAttribute("TheJoinDestination", m_TheJoinDestination.tostring)  
          node.SetAttribute("HandJoin", HandJoin)  
          node.SetAttribute("SEQ", SEQ)  
          node.SetAttribute("TheJoinSource", m_TheJoinSource.tostring)  
          node.SetAttribute("TheView", m_TheView.tostring)  
          node.SetAttribute("RefType", RefType)  
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
