

Option Explicit On

Imports System
Imports System.IO
Imports LATIR2
Imports System.xml
Imports System.Data
Imports System.Convert
Imports System.DateTime

Namespace bp3app


''' <summary>
'''Реализация строки раздела Модули
''' </summary>
''' <remarks>
'''
''' </remarks>
    Public Class BP3APP_MODULES
        Inherits LATIR2.Document.DocRow_Base



''' <summary>
'''Локальная переменная для поля Отчет
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_Report  as System.Guid


''' <summary>
'''Локальная переменная для поля Описание
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_TheComment  as STRING


''' <summary>
'''Локальная переменная для поля Меню
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_TopMenu  as System.Guid


''' <summary>
'''Локальная переменная для поля Код модуля
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_Name  as String


''' <summary>
'''Локальная переменная для поля Настраивать видимость
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_CustomizeVisibility  as enumBoolean


''' <summary>
'''Локальная переменная для поля Вариант действия
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_ActionType  as enumMenuActionType


''' <summary>
'''Локальная переменная для поля Надпись
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_Caption  as String


''' <summary>
'''Локальная переменная для поля Иконка
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_TheIcon  as String


''' <summary>
'''Локальная переменная для поля Тип документа
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_ObjectType  as System.Guid


''' <summary>
'''Локальная переменная для поля Журнал
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_Journal  as System.Guid


''' <summary>
'''Локальная переменная для поля № п/п
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_Sequence  as long


''' <summary>
'''Локальная переменная для поля ID Документа
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_Document  as String


''' <summary>
'''Локальная переменная для поля Имя группы
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_GroupName  as String


''' <summary>
'''Локальная переменная для дочернего раздела Действия и отчеты
''' </summary>
''' <remarks>
'''
''' </remarks>
        private m_BP3APP_OPER As BP3APP_OPER_col



''' <summary>
'''Очистить поля раздела
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Overrides Sub CleanFields()
            ' m_Report=   
            ' m_TheComment=   
            ' m_TopMenu=   
            ' m_Name=   
            ' m_CustomizeVisibility=   
            ' m_ActionType=   
            ' m_Caption=   
            ' m_TheIcon=   
            ' m_ObjectType=   
            ' m_Journal=   
            ' m_Sequence=   
            ' m_Document=   
            ' m_GroupName=   
        End Sub



''' <summary>
'''Количество полей в строке раздела
''' </summary>
''' <remarks>
'''
''' </remarks>
    Public Overrides ReadOnly Property Count() As Long
        Get
           Count = 13
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
                    Value = TopMenu
                Case 2
                    Value = GroupName
                Case 3
                    Value = Caption
                Case 4
                    Value = Name
                Case 5
                    Value = Sequence
                Case 6
                    Value = TheComment
                Case 7
                    Value = TheIcon
                Case 8
                    Value = CustomizeVisibility
                Case 9
                    Value = Journal
                Case 10
                    Value = Document
                Case 11
                    Value = ActionType
                Case 12
                    Value = ObjectType
                Case 13
                    Value = Report
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
                    TopMenu = value
                Case 2
                    GroupName = value
                Case 3
                    Caption = value
                Case 4
                    Name = value
                Case 5
                    Sequence = value
                Case 6
                    TheComment = value
                Case 7
                    TheIcon = value
                Case 8
                    CustomizeVisibility = value
                Case 9
                    Journal = value
                Case 10
                    Document = value
                Case 11
                    ActionType = value
                Case 12
                    ObjectType = value
                Case 13
                    Report = value
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
                    Return "TopMenu"
                Case 2
                    Return "GroupName"
                Case 3
                    Return "Caption"
                Case 4
                    Return "Name"
                Case 5
                    Return "Sequence"
                Case 6
                    Return "TheComment"
                Case 7
                    Return "TheIcon"
                Case 8
                    Return "CustomizeVisibility"
                Case 9
                    Return "Journal"
                Case 10
                    Return "Document"
                Case 11
                    Return "ActionType"
                Case 12
                    Return "ObjectType"
                Case 13
                    Return "Report"
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
             if TopMenu is nothing then
               dr("TopMenu") =system.dbnull.value
               dr("TopMenu_ID") =System.Guid.Empty
             else
               dr("TopMenu") =TopMenu.BRIEF
               dr("TopMenu_ID") =TopMenu.ID
             end if 
             dr("GroupName") =GroupName
             dr("Caption") =Caption
             dr("Name") =Name
             dr("Sequence") =Sequence
             dr("TheComment") =TheComment
             dr("TheIcon") =TheIcon
             select case CustomizeVisibility
            case enumBoolean.Boolean_Da
              dr ("CustomizeVisibility")  = "Да"
              dr ("CustomizeVisibility_VAL")  = -1
            case enumBoolean.Boolean_Net
              dr ("CustomizeVisibility")  = "Нет"
              dr ("CustomizeVisibility_VAL")  = 0
              end select 'CustomizeVisibility
             if Journal is nothing then
               dr("Journal") =system.dbnull.value
               dr("Journal_ID") =System.Guid.Empty
             else
               dr("Journal") =Journal.BRIEF
               dr("Journal_ID") =Journal.ID
             end if 
             dr("Document") =Document
             select case ActionType
            case enumMenuActionType.MenuActionType_Zapustit__ARM
              dr ("ActionType")  = "Запустить АРМ"
              dr ("ActionType_VAL")  = 4
            case enumMenuActionType.MenuActionType_Vipolnit__metod
              dr ("ActionType")  = "Выполнить метод"
              dr ("ActionType_VAL")  = 2
            case enumMenuActionType.MenuActionType_Otkrit__otcet
              dr ("ActionType")  = "Открыть отчет"
              dr ("ActionType_VAL")  = 5
            case enumMenuActionType.MenuActionType_Nicego_ne_delat_
              dr ("ActionType")  = "Ничего не делать"
              dr ("ActionType_VAL")  = 0
            case enumMenuActionType.MenuActionType_Otkrit__dokument
              dr ("ActionType")  = "Открыть документ"
              dr ("ActionType_VAL")  = 1
            case enumMenuActionType.MenuActionType_Otkrit__gurnal
              dr ("ActionType")  = "Открыть журнал"
              dr ("ActionType_VAL")  = 3
              end select 'ActionType
             if ObjectType is nothing then
               dr("ObjectType") =system.dbnull.value
               dr("ObjectType_ID") =System.Guid.Empty
             else
               dr("ObjectType") =ObjectType.BRIEF
               dr("ObjectType_ID") =ObjectType.ID
             end if 
             if Report is nothing then
               dr("Report") =system.dbnull.value
               dr("Report_ID") =System.Guid.Empty
             else
               dr("Report") =Report.BRIEF
               dr("Report_ID") =Report.ID
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
            mFindInside = BP3APP_OPER.FindObject(table,RowID)
            if not mFindInside is nothing then return mFindInside
            Return Nothing
        End Function



''' <summary>
'''Заполнить коллекцю именованных полей
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Overrides Sub Pack(ByVal nv As LATIR2.NamedValues)
          if m_TopMenu.Equals(System.Guid.Empty) then
            nv.Add("TopMenu", system.dbnull.value, Application.Session.GetProvider.ID2DbType, Application.Session.GetProvider.ID2Size)
          else
            nv.Add("TopMenu", Application.Session.GetProvider.ID2Param(m_TopMenu), Application.Session.GetProvider.ID2DbType, Application.Session.GetProvider.ID2Size)
          end if 
          nv.Add("GroupName", GroupName, dbtype.string)
          nv.Add("Caption", Caption, dbtype.string)
          nv.Add("Name", Name, dbtype.string)
          nv.Add("Sequence", Sequence, dbtype.Int32)
          nv.Add("TheComment", TheComment, dbtype.string)
          nv.Add("TheIcon", TheIcon, dbtype.string)
          nv.Add("CustomizeVisibility", CustomizeVisibility, dbtype.int16)
          if m_Journal.Equals(System.Guid.Empty) then
            nv.Add("Journal", system.dbnull.value, Application.Session.GetProvider.ID2DbType, Application.Session.GetProvider.ID2Size)
          else
            nv.Add("Journal", Application.Session.GetProvider.ID2Param(m_Journal), Application.Session.GetProvider.ID2DbType, Application.Session.GetProvider.ID2Size)
          end if 
          nv.Add("Document", Document, dbtype.string)
          nv.Add("ActionType", ActionType, dbtype.int16)
          if m_ObjectType.Equals(System.Guid.Empty) then
            nv.Add("ObjectType", system.dbnull.value, Application.Session.GetProvider.ID2DbType, Application.Session.GetProvider.ID2Size)
          else
            nv.Add("ObjectType", Application.Session.GetProvider.ID2Param(m_ObjectType), Application.Session.GetProvider.ID2DbType, Application.Session.GetProvider.ID2Size)
          end if 
          if m_Report.Equals(System.Guid.Empty) then
            nv.Add("Report", system.dbnull.value, Application.Session.GetProvider.ID2DbType, Application.Session.GetProvider.ID2Size)
          else
            nv.Add("Report", Application.Session.GetProvider.ID2Param(m_Report), Application.Session.GetProvider.ID2DbType, Application.Session.GetProvider.ID2Size)
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
      If reader.Table.Columns.Contains("TopMenu") Then
          if isdbnull(reader.item("TopMenu")) then
            If reader.Table.Columns.Contains("TopMenu") Then m_TopMenu = System.GUID.Empty
          else
            If reader.Table.Columns.Contains("TopMenu") Then m_TopMenu= New System.Guid(reader.item("TopMenu").ToString())
          end if 
      end if 
          If reader.Table.Columns.Contains("GroupName") Then m_GroupName=reader.item("GroupName").ToString()
          If reader.Table.Columns.Contains("Caption") Then m_Caption=reader.item("Caption").ToString()
          If reader.Table.Columns.Contains("Name") Then m_Name=reader.item("Name").ToString()
          If reader.Table.Columns.Contains("Sequence") Then m_Sequence=reader.item("Sequence")
          If reader.Table.Columns.Contains("TheComment") Then m_TheComment=reader.item("TheComment").ToString()
          If reader.Table.Columns.Contains("TheIcon") Then m_TheIcon=reader.item("TheIcon").ToString()
          If reader.Table.Columns.Contains("CustomizeVisibility") Then m_CustomizeVisibility=reader.item("CustomizeVisibility")
      If reader.Table.Columns.Contains("Journal") Then
          if isdbnull(reader.item("Journal")) then
            If reader.Table.Columns.Contains("Journal") Then m_Journal = System.GUID.Empty
          else
            If reader.Table.Columns.Contains("Journal") Then m_Journal= New System.Guid(reader.item("Journal").ToString())
          end if 
      end if 
          If reader.Table.Columns.Contains("Document") Then m_Document=reader.item("Document").ToString()
          If reader.Table.Columns.Contains("ActionType") Then m_ActionType=reader.item("ActionType")
      If reader.Table.Columns.Contains("ObjectType") Then
          if isdbnull(reader.item("ObjectType")) then
            If reader.Table.Columns.Contains("ObjectType") Then m_ObjectType = System.GUID.Empty
          else
            If reader.Table.Columns.Contains("ObjectType") Then m_ObjectType= New System.Guid(reader.item("ObjectType").ToString())
          end if 
      end if 
      If reader.Table.Columns.Contains("Report") Then
          if isdbnull(reader.item("Report")) then
            If reader.Table.Columns.Contains("Report") Then m_Report = System.GUID.Empty
          else
            If reader.Table.Columns.Contains("Report") Then m_Report= New System.Guid(reader.item("Report").ToString())
          end if 
      end if 
        End Sub


''' <summary>
'''Доступ к полю Меню
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property TopMenu() As LATIR2.Document.docrow_base
            Get
                LoadFromDatabase()
                TopMenu = me.application.Findrowobject("BP3APP_MENU",m_TopMenu)
                AccessTime = Now
            End Get
            Set(ByVal Value As LATIR2.Document.docrow_base )
                LoadFromDatabase()
                if not Value is nothing then
                    m_TopMenu = Value.id
                else
                   m_TopMenu=System.Guid.Empty
                end if
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю Имя группы
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property GroupName() As String
            Get
                LoadFromDatabase()
                GroupName = m_GroupName
                AccessTime = Now
            End Get
            Set(ByVal Value As String )
                LoadFromDatabase()
                m_GroupName = Value
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
'''Доступ к полю Код модуля
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
'''Доступ к полю Иконка
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property TheIcon() As String
            Get
                LoadFromDatabase()
                TheIcon = m_TheIcon
                AccessTime = Now
            End Get
            Set(ByVal Value As String )
                LoadFromDatabase()
                m_TheIcon = Value
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю Настраивать видимость
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property CustomizeVisibility() As enumBoolean
            Get
                LoadFromDatabase()
                CustomizeVisibility = m_CustomizeVisibility
                AccessTime = Now
            End Get
            Set(ByVal Value As enumBoolean )
                LoadFromDatabase()
                m_CustomizeVisibility = Value
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю Журнал
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property Journal() As LATIR2.Document.docrow_base
            Get
                LoadFromDatabase()
                Journal = me.application.Findrowobject("bp3list_def",m_Journal)
                AccessTime = Now
            End Get
            Set(ByVal Value As LATIR2.Document.docrow_base )
                LoadFromDatabase()
                if not Value is nothing then
                    m_Journal = Value.id
                else
                   m_Journal=System.Guid.Empty
                end if
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю ID Документа
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property Document() As String
            Get
                LoadFromDatabase()
                Document = m_Document
                AccessTime = Now
            End Get
            Set(ByVal Value As String )
                LoadFromDatabase()
                m_Document = Value
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю Вариант действия
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property ActionType() As enumMenuActionType
            Get
                LoadFromDatabase()
                ActionType = m_ActionType
                AccessTime = Now
            End Get
            Set(ByVal Value As enumMenuActionType )
                LoadFromDatabase()
                m_ActionType = Value
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю Тип документа
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property ObjectType() As LATIR2.Document.docrow_base
            Get
                LoadFromDatabase()
                ObjectType = me.application.Findrowobject("bp3doc_def",m_ObjectType)
                AccessTime = Now
            End Get
            Set(ByVal Value As LATIR2.Document.docrow_base )
                LoadFromDatabase()
                if not Value is nothing then
                    m_ObjectType = Value.id
                else
                   m_ObjectType=System.Guid.Empty
                end if
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю Отчет
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property Report() As LATIR2.Document.docrow_base
            Get
                LoadFromDatabase()
                Report = me.application.Findrowobject("bp3report_def",m_Report)
                AccessTime = Now
            End Get
            Set(ByVal Value As LATIR2.Document.docrow_base )
                LoadFromDatabase()
                if not Value is nothing then
                    m_Report = Value.id
                else
                   m_Report=System.Guid.Empty
                end if
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к дочернему разделу Действия и отчеты
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public readonly Property BP3APP_OPER() As BP3APP_OPER_col
            Get
                if  m_BP3APP_OPER is nothing then
                  m_BP3APP_OPER = new BP3APP_OPER_col
                  m_BP3APP_OPER.Parent = me
                  m_BP3APP_OPER.Application = me.Application
                  m_BP3APP_OPER.Refresh
                end if
                BP3APP_OPER = m_BP3APP_OPER
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
            m_TopMenu = new system.guid(node.Attributes.GetNamedItem("TopMenu").Value)
            GroupName = node.Attributes.GetNamedItem("GroupName").Value
            Caption = node.Attributes.GetNamedItem("Caption").Value
            Name = node.Attributes.GetNamedItem("Name").Value
            Sequence = node.Attributes.GetNamedItem("Sequence").Value
            TheComment = node.Attributes.GetNamedItem("TheComment").Value
            TheIcon = node.Attributes.GetNamedItem("TheIcon").Value
            CustomizeVisibility = node.Attributes.GetNamedItem("CustomizeVisibility").Value
            m_Journal = new system.guid(node.Attributes.GetNamedItem("Journal").Value)
            Document = node.Attributes.GetNamedItem("Document").Value
            ActionType = node.Attributes.GetNamedItem("ActionType").Value
            m_ObjectType = new system.guid(node.Attributes.GetNamedItem("ObjectType").Value)
            m_Report = new system.guid(node.Attributes.GetNamedItem("Report").Value)
            e_list = node.SelectNodes("BP3APP_OPER_COL")
            BP3APP_OPER.XMLLoad(e_list,LoadMode)
             Changed = true
        End sub
        Public Overrides Sub Dispose()
            BP3APP_OPER.Dispose
        End Sub


''' <summary>
'''Записать данные раздела в XML файл
''' </summary>
''' <remarks>
'''
''' </remarks>
        Protected Overrides sub XLMPack(ByVal node As System.Xml.XmlElement, ByVal Xdom As System.Xml.XmlDocument)
           on error resume next  
          node.SetAttribute("TopMenu", m_TopMenu.tostring)  
          node.SetAttribute("GroupName", GroupName)  
          node.SetAttribute("Caption", Caption)  
          node.SetAttribute("Name", Name)  
          node.SetAttribute("Sequence", Sequence)  
          node.SetAttribute("TheComment", TheComment)  
          node.SetAttribute("TheIcon", TheIcon)  
          node.SetAttribute("CustomizeVisibility", CustomizeVisibility)  
          node.SetAttribute("Journal", m_Journal.tostring)  
          node.SetAttribute("Document", Document)  
          node.SetAttribute("ActionType", ActionType)  
          node.SetAttribute("ObjectType", m_ObjectType.tostring)  
          node.SetAttribute("Report", m_Report.tostring)  
            BP3APP_OPER.XMLSave(node,xdom)
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
            BP3APP_OPER.BatchUpdate
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
        Public Overrides Function GetDocCollection_Base(ByVal Index As Long) As LATIR2.Document.DocCollection_Base
            Select Case Index
         Case 1
            return BP3APP_OPER
            End Select
            return nothing
        End Function
    End Class
End Namespace
