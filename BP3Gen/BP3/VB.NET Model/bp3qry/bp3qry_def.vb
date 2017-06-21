

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
'''Реализация строки раздела Представление
''' </summary>
''' <remarks>
'''
''' </remarks>
    Public Class bp3qry_def
        Inherits BP3.Document.DocRow_Base



''' <summary>
'''Локальная переменная для поля Название
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_Name  as String


''' <summary>
'''Локальная переменная для поля Псевдоним
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_the_Alias  as String


''' <summary>
'''Локальная переменная для поля Представление дя раздела
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_SrcPart  as System.Guid


''' <summary>
'''Локальная переменная для поля Для поиска
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_ForChoose  as enumBoolean



''' <summary>
'''Очистить поля раздела
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Overrides Sub CleanFields()
            ' m_Name=   
            ' m_the_Alias=   
            ' m_SrcPart=   
            ' m_ForChoose=   
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
                    Value = SrcPart
                Case 2
                    Value = the_Alias
                Case 3
                    Value = ForChoose
                Case 4
                    Value = Name
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
                    SrcPart = value
                Case 2
                    the_Alias = value
                Case 3
                    ForChoose = value
                Case 4
                    Name = value
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
                    Return "SrcPart"
                Case 2
                    Return "the_Alias"
                Case 3
                    Return "ForChoose"
                Case 4
                    Return "Name"
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
             if SrcPart is nothing then
               dr("SrcPart") =system.dbnull.value
               dr("SrcPart_ID") =System.Guid.Empty
             else
               dr("SrcPart") =SrcPart.BRIEF
               dr("SrcPart_ID") =SrcPart.ID
             end if 
             dr("the_Alias") =the_Alias
             select case ForChoose
            case enumBoolean.Boolean_Da
              dr ("ForChoose")  = "Да"
              dr ("ForChoose_VAL")  = -1
            case enumBoolean.Boolean_Net
              dr ("ForChoose")  = "Нет"
              dr ("ForChoose_VAL")  = 0
              end select 'ForChoose
             dr("Name") =Name
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
          if m_SrcPart.Equals(System.Guid.Empty) then
            nv.Add("SrcPart", system.dbnull.value, Application.Session.GetProvider.ID2DbType, Application.Session.GetProvider.ID2Size)
          else
            nv.Add("SrcPart", Application.Session.GetProvider.ID2Param(m_SrcPart), Application.Session.GetProvider.ID2DbType, Application.Session.GetProvider.ID2Size)
          end if 
          nv.Add("the_Alias", the_Alias, dbtype.string)
          nv.Add("ForChoose", ForChoose, dbtype.int16)
          nv.Add("Name", Name, dbtype.string)
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
      If reader.Table.Columns.Contains("SrcPart") Then
          if isdbnull(reader.item("SrcPart")) then
            If reader.Table.Columns.Contains("SrcPart") Then m_SrcPart = System.GUID.Empty
          else
            If reader.Table.Columns.Contains("SrcPart") Then m_SrcPart= New System.Guid(reader.item("SrcPart").ToString())
          end if 
      end if 
          If reader.Table.Columns.Contains("the_Alias") Then m_the_Alias=reader.item("the_Alias").ToString()
          If reader.Table.Columns.Contains("ForChoose") Then m_ForChoose=reader.item("ForChoose")
          If reader.Table.Columns.Contains("Name") Then m_Name=reader.item("Name").ToString()
        End Sub


''' <summary>
'''Доступ к полю Представление дя раздела
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property SrcPart() As BP3.Document.docrow_base
            Get
                LoadFromDatabase()
                SrcPart = me.application.Findrowobject("bp3doc_store",m_SrcPart)
                AccessTime = Now
            End Get
            Set(ByVal Value As BP3.Document.docrow_base )
                LoadFromDatabase()
                if not Value is nothing then
                    m_SrcPart = Value.id
                else
                   m_SrcPart=System.Guid.Empty
                end if
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю Псевдоним
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property the_Alias() As String
            Get
                LoadFromDatabase()
                the_Alias = m_the_Alias
                AccessTime = Now
            End Get
            Set(ByVal Value As String )
                LoadFromDatabase()
                m_the_Alias = Value
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю Для поиска
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property ForChoose() As enumBoolean
            Get
                LoadFromDatabase()
                ForChoose = m_ForChoose
                AccessTime = Now
            End Get
            Set(ByVal Value As enumBoolean )
                LoadFromDatabase()
                m_ForChoose = Value
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
'''Заполнить поля данными из XML
''' </summary>
''' <remarks>
'''
''' </remarks>
        Protected Overrides sub XMLUnpack(ByVal node As System.Xml.XmlNode, Optional ByVal LoadMode As Integer = 0)
          Dim e_list As XmlNodeList
          on error resume next  
            m_SrcPart = new system.guid(node.Attributes.GetNamedItem("SrcPart").Value)
            the_Alias = node.Attributes.GetNamedItem("the_Alias").Value
            ForChoose = node.Attributes.GetNamedItem("ForChoose").Value
            Name = node.Attributes.GetNamedItem("Name").Value
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
          node.SetAttribute("SrcPart", m_SrcPart.tostring)  
          node.SetAttribute("the_Alias", the_Alias)  
          node.SetAttribute("ForChoose", ForChoose)  
          node.SetAttribute("Name", Name)  
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
