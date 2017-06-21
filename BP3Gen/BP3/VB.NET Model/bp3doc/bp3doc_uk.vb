

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
'''Реализация строки раздела Ограничение уникальности
''' </summary>
''' <remarks>
'''
''' </remarks>
    Public Class bp3doc_uk
        Inherits BP3.Document.DocRow_Base



''' <summary>
'''Локальная переменная для поля По родителю
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_PerParent  as enumBoolean


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
'''Локальная переменная для поля Раздел
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_UKStore  as System.Guid


''' <summary>
'''Локальная переменная для дочернего раздела Поля ограничения
''' </summary>
''' <remarks>
'''
''' </remarks>
        private m_bp3doc_ukfld As bp3doc_ukfld_col



''' <summary>
'''Очистить поля раздела
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Overrides Sub CleanFields()
            ' m_PerParent=   
            ' m_Name=   
            ' m_TheComment=   
            ' m_UKStore=   
        End Sub



''' <summary>
'''Количество полей в строке раздела
''' </summary>
''' <remarks>
'''
''' </remarks>
    Public Overrides ReadOnly Property Count() As Long
        Get
           Count = 4
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
                    Value = UKStore
                Case 2
                    Value = TheComment
                Case 3
                    Value = Name
                Case 4
                    Value = PerParent
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
                    UKStore = value
                Case 2
                    TheComment = value
                Case 3
                    Name = value
                Case 4
                    PerParent = value
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
                    Return "UKStore"
                Case 2
                    Return "TheComment"
                Case 3
                    Return "Name"
                Case 4
                    Return "PerParent"
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
             if UKStore is nothing then
               dr("UKStore") =system.dbnull.value
               dr("UKStore_ID") =System.Guid.Empty
             else
               dr("UKStore") =UKStore.BRIEF
               dr("UKStore_ID") =UKStore.ID
             end if 
             dr("TheComment") =TheComment
             dr("Name") =Name
             select case PerParent
            case enumBoolean.Boolean_Da
              dr ("PerParent")  = "Да"
              dr ("PerParent_VAL")  = -1
            case enumBoolean.Boolean_Net
              dr ("PerParent")  = "Нет"
              dr ("PerParent_VAL")  = 0
              end select 'PerParent
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
            mFindInside = bp3doc_ukfld.FindObject(table,RowID)
            if not mFindInside is nothing then return mFindInside
            Return Nothing
        End Function



''' <summary>
'''Заполнить коллекцю именованных полей
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Overrides Sub Pack(ByVal nv As BP3.NamedValues)
          if m_UKStore.Equals(System.Guid.Empty) then
            nv.Add("UKStore", system.dbnull.value, Application.Session.GetProvider.ID2DbType, Application.Session.GetProvider.ID2Size)
          else
            nv.Add("UKStore", Application.Session.GetProvider.ID2Param(m_UKStore), Application.Session.GetProvider.ID2DbType, Application.Session.GetProvider.ID2Size)
          end if 
          nv.Add("TheComment", TheComment, dbtype.string)
          nv.Add("Name", Name, dbtype.string)
          nv.Add("PerParent", PerParent, dbtype.int16)
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
      If reader.Table.Columns.Contains("UKStore") Then
          if isdbnull(reader.item("UKStore")) then
            If reader.Table.Columns.Contains("UKStore") Then m_UKStore = System.GUID.Empty
          else
            If reader.Table.Columns.Contains("UKStore") Then m_UKStore= New System.Guid(reader.item("UKStore").ToString())
          end if 
      end if 
          If reader.Table.Columns.Contains("TheComment") Then m_TheComment=reader.item("TheComment").ToString()
          If reader.Table.Columns.Contains("Name") Then m_Name=reader.item("Name").ToString()
          If reader.Table.Columns.Contains("PerParent") Then m_PerParent=reader.item("PerParent")
        End Sub


''' <summary>
'''Доступ к полю Раздел
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property UKStore() As BP3.Document.docrow_base
            Get
                LoadFromDatabase()
                UKStore = me.application.Findrowobject("bp3doc_store",m_UKStore)
                AccessTime = Now
            End Get
            Set(ByVal Value As BP3.Document.docrow_base )
                LoadFromDatabase()
                if not Value is nothing then
                    m_UKStore = Value.id
                else
                   m_UKStore=System.Guid.Empty
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
'''Доступ к полю По родителю
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property PerParent() As enumBoolean
            Get
                LoadFromDatabase()
                PerParent = m_PerParent
                AccessTime = Now
            End Get
            Set(ByVal Value As enumBoolean )
                LoadFromDatabase()
                m_PerParent = Value
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к дочернему разделу Поля ограничения
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public readonly Property bp3doc_ukfld() As bp3doc_ukfld_col
            Get
                if  m_bp3doc_ukfld is nothing then
                  m_bp3doc_ukfld = new bp3doc_ukfld_col
                  m_bp3doc_ukfld.Parent = me
                  m_bp3doc_ukfld.Application = me.Application
                  m_bp3doc_ukfld.Refresh
                end if
                bp3doc_ukfld = m_bp3doc_ukfld
                AccessTime = Now
            End Get
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
            m_UKStore = new system.guid(node.Attributes.GetNamedItem("UKStore").Value)
            TheComment = node.Attributes.GetNamedItem("TheComment").Value
            Name = node.Attributes.GetNamedItem("Name").Value
            PerParent = node.Attributes.GetNamedItem("PerParent").Value
            e_list = node.SelectNodes("bp3doc_ukfld_COL")
            bp3doc_ukfld.XMLLoad(e_list,LoadMode)
             Changed = true
        End sub
        Public Overrides Sub Dispose()
            bp3doc_ukfld.Dispose
        End Sub


''' <summary>
'''Записать данные раздела в XML файл
''' </summary>
''' <remarks>
'''
''' </remarks>
        Protected Overrides sub XLMPack(ByVal node As System.Xml.XmlElement, ByVal Xdom As System.Xml.XmlDocument)
           on error resume next  
          node.SetAttribute("UKStore", m_UKStore.tostring)  
          node.SetAttribute("TheComment", TheComment)  
          node.SetAttribute("Name", Name)  
          node.SetAttribute("PerParent", PerParent)  
            bp3doc_ukfld.XMLSave(node,xdom)
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
            bp3doc_ukfld.BatchUpdate
End Sub


''' <summary>
'''Количество дочерних разделов
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Overrides ReadOnly Property CountOfParts() As Long
            Get
                CountOfParts = 1
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
         Case 1
            return bp3doc_ukfld
            End Select
            return nothing
        End Function
    End Class
End Namespace
