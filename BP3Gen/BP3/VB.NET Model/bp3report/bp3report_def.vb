

Option Explicit On

Imports System
Imports System.IO
Imports LATIR2
Imports System.xml
Imports System.Data
Imports System.Convert
Imports System.DateTime

Namespace bp3report


''' <summary>
'''Реализация строки раздела Описание
''' </summary>
''' <remarks>
'''
''' </remarks>
    Public Class bp3report_def
        Inherits LATIR2.Document.DocRow_Base



''' <summary>
'''Локальная переменная для поля Заголовок
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_Caption  as String


''' <summary>
'''Локальная переменная для поля Тип отчета
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_ReportType  as enumReportType


''' <summary>
'''Локальная переменная для поля Описание
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_TheComment  as STRING


''' <summary>
'''Локальная переменная для поля Файл отчета
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_ReportFile  as Object


''' <summary>
'''Локальная переменная для поля Название
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_Name  as String


''' <summary>
'''Локальная переменная для поля Базовый запрос
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_ReportView  as System.Guid



''' <summary>
'''Очистить поля раздела
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Overrides Sub CleanFields()
            ' m_Caption=   
            ' m_ReportType=   
            ' m_TheComment=   
            ' m_ReportFile=   
            ' m_Name=   
            ' m_ReportView=   
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
                    Value = Caption
                Case 2
                    Value = ReportType
                Case 3
                    Value = TheComment
                Case 4
                    Value = ReportFile
                Case 5
                    Value = Name
                Case 6
                    Value = ReportView
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
                    Caption = value
                Case 2
                    ReportType = value
                Case 3
                    TheComment = value
                Case 4
                    ReportFile = value
                Case 5
                    Name = value
                Case 6
                    ReportView = value
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
                    Return "Caption"
                Case 2
                    Return "ReportType"
                Case 3
                    Return "TheComment"
                Case 4
                    Return "ReportFile"
                Case 5
                    Return "Name"
                Case 6
                    Return "ReportView"
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
             dr("Caption") =Caption
             select case ReportType
            case enumReportType.ReportType_Eksport_po_Excel_sablonu
              dr ("ReportType")  = "Экспорт по Excel шаблону"
              dr ("ReportType_VAL")  = 4
            case enumReportType.ReportType_Tablica
              dr ("ReportType")  = "Таблица"
              dr ("ReportType_VAL")  = 0
            case enumReportType.ReportType_Eksport_po_WORD_sablonu
              dr ("ReportType")  = "Экспорт по WORD шаблону"
              dr ("ReportType_VAL")  = 3
            case enumReportType.ReportType_Dvumernay_matrica
              dr ("ReportType")  = "Двумерная матрица"
              dr ("ReportType_VAL")  = 1
            case enumReportType.ReportType_Tol_ko_rascet
              dr ("ReportType")  = "Только расчет"
              dr ("ReportType_VAL")  = 2
              end select 'ReportType
             dr("TheComment") =TheComment
             dr("ReportFile") =ReportFile
             dr("Name") =Name
             if ReportView is nothing then
               dr("ReportView") =system.dbnull.value
               dr("ReportView_ID") =System.Guid.Empty
             else
               dr("ReportView") =ReportView.BRIEF
               dr("ReportView_ID") =ReportView.ID
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
          nv.Add("Caption", Caption, dbtype.string)
          nv.Add("ReportType", ReportType, dbtype.int16)
          nv.Add("TheComment", TheComment, dbtype.string)
          nv.Add("ReportFile", ReportFile, dbtype.Binary)
          nv.Add("Name", Name, dbtype.string)
          if m_ReportView.Equals(System.Guid.Empty) then
            nv.Add("ReportView", system.dbnull.value, Application.Session.GetProvider.ID2DbType, Application.Session.GetProvider.ID2Size)
          else
            nv.Add("ReportView", Application.Session.GetProvider.ID2Param(m_ReportView), Application.Session.GetProvider.ID2DbType, Application.Session.GetProvider.ID2Size)
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
          If reader.Table.Columns.Contains("Caption") Then m_Caption=reader.item("Caption").ToString()
          If reader.Table.Columns.Contains("ReportType") Then m_ReportType=reader.item("ReportType")
          If reader.Table.Columns.Contains("TheComment") Then m_TheComment=reader.item("TheComment").ToString()
          If reader.Table.Columns.Contains("ReportFile") Then m_ReportFile=reader.item("ReportFile")
          If reader.Table.Columns.Contains("Name") Then m_Name=reader.item("Name").ToString()
      If reader.Table.Columns.Contains("ReportView") Then
          if isdbnull(reader.item("ReportView")) then
            If reader.Table.Columns.Contains("ReportView") Then m_ReportView = System.GUID.Empty
          else
            If reader.Table.Columns.Contains("ReportView") Then m_ReportView= New System.Guid(reader.item("ReportView").ToString())
          end if 
      end if 
        End Sub


''' <summary>
'''Доступ к полю Заголовок
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
'''Доступ к полю Тип отчета
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property ReportType() As enumReportType
            Get
                LoadFromDatabase()
                ReportType = m_ReportType
                AccessTime = Now
            End Get
            Set(ByVal Value As enumReportType )
                LoadFromDatabase()
                m_ReportType = Value
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
'''Доступ к полю Файл отчета
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property ReportFile() As Object
            Get
                LoadFromDatabase()
                ReportFile = m_ReportFile
                AccessTime = Now
            End Get
            Set(ByVal Value As Object )
                LoadFromDatabase()
                m_ReportFile = Value
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
'''Доступ к полю Базовый запрос
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property ReportView() As LATIR2.Document.docrow_base
            Get
                LoadFromDatabase()
                ReportView = me.application.Findrowobject("bp3qry_def",m_ReportView)
                AccessTime = Now
            End Get
            Set(ByVal Value As LATIR2.Document.docrow_base )
                LoadFromDatabase()
                if not Value is nothing then
                    m_ReportView = Value.id
                else
                   m_ReportView=System.Guid.Empty
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
            Caption = node.Attributes.GetNamedItem("Caption").Value
            ReportType = node.Attributes.GetNamedItem("ReportType").Value
            TheComment = node.Attributes.GetNamedItem("TheComment").Value
            ReportFile = System.Convert.FromBase64String(node.Attributes.GetNamedItem("ReportFile").Value.ToString())
            Name = node.Attributes.GetNamedItem("Name").Value
            m_ReportView = new system.guid(node.Attributes.GetNamedItem("ReportView").Value)
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
          node.SetAttribute("Caption", Caption)  
          node.SetAttribute("ReportType", ReportType)  
          node.SetAttribute("TheComment", TheComment)  
          node.SetAttribute("ReportFile", System.Convert.ToBase64String(ReportFile))  
          node.SetAttribute("Name", Name)  
          node.SetAttribute("ReportView", m_ReportView.tostring)  
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
