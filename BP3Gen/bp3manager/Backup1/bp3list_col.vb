

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
'''Реализация строки раздела Колонки журнала
''' </summary>
''' <remarks>
'''
''' </remarks>
    Public Class bp3list_col
        Inherits LATIR2.Document.DocRow_Base



''' <summary>
'''Локальная переменная для поля Сортировка колонки
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_ColSort  as enumColumnSortType


''' <summary>
'''Локальная переменная для поля Последовательность
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_sequence  as long


''' <summary>
'''Локальная переменная для поля Выравнивание
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_ColumnAlignment  as enumVHAlignment


''' <summary>
'''Локальная переменная для поля Ширина колонки
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_colwidth  as long


''' <summary>
'''Локальная переменная для поля Стиль
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_TheStyle  as String


''' <summary>
'''Локальная переменная для поля Аггрегация при группировке
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_GroupAggregation  as enumAggregationType


''' <summary>
'''Локальная переменная для поля Поле представления
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_ViewField  as String


''' <summary>
'''Локальная переменная для поля Название
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_name  as String



''' <summary>
'''Очистить поля раздела
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Overrides Sub CleanFields()
            ' m_ColSort=   
            ' m_sequence=   
            ' m_ColumnAlignment=   
            ' m_colwidth=   
            ' m_TheStyle=   
            ' m_GroupAggregation=   
            ' m_ViewField=   
            ' m_name=   
        End Sub



''' <summary>
'''Количество полей в строке раздела
''' </summary>
''' <remarks>
'''
''' </remarks>
    Public Overrides ReadOnly Property Count() As Long
        Get
           Count = 8
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
                    Value = sequence
                Case 2
                    Value = name
                Case 3
                    Value = ViewField
                Case 4
                    Value = colwidth
                Case 5
                    Value = ColumnAlignment
                Case 6
                    Value = ColSort
                Case 7
                    Value = TheStyle
                Case 8
                    Value = GroupAggregation
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
                    sequence = value
                Case 2
                    name = value
                Case 3
                    ViewField = value
                Case 4
                    colwidth = value
                Case 5
                    ColumnAlignment = value
                Case 6
                    ColSort = value
                Case 7
                    TheStyle = value
                Case 8
                    GroupAggregation = value
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
                    Return "sequence"
                Case 2
                    Return "name"
                Case 3
                    Return "ViewField"
                Case 4
                    Return "colwidth"
                Case 5
                    Return "ColumnAlignment"
                Case 6
                    Return "ColSort"
                Case 7
                    Return "TheStyle"
                Case 8
                    Return "GroupAggregation"
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
             dr("sequence") =sequence
             dr("name") =name
             dr("ViewField") =ViewField
             dr("colwidth") =colwidth
             select case ColumnAlignment
            case enumVHAlignment.VHAlignment_Right_Top
              dr ("ColumnAlignment")  = "Right Top"
              dr ("ColumnAlignment_VAL")  = 6
            case enumVHAlignment.VHAlignment_Right_Center
              dr ("ColumnAlignment")  = "Right Center"
              dr ("ColumnAlignment_VAL")  = 7
            case enumVHAlignment.VHAlignment_Right_Bottom
              dr ("ColumnAlignment")  = "Right Bottom"
              dr ("ColumnAlignment_VAL")  = 8
            case enumVHAlignment.VHAlignment_Center_Top
              dr ("ColumnAlignment")  = "Center Top"
              dr ("ColumnAlignment_VAL")  = 3
            case enumVHAlignment.VHAlignment_Left_Top
              dr ("ColumnAlignment")  = "Left Top"
              dr ("ColumnAlignment_VAL")  = 0
            case enumVHAlignment.VHAlignment_Center_Center
              dr ("ColumnAlignment")  = "Center Center"
              dr ("ColumnAlignment_VAL")  = 4
            case enumVHAlignment.VHAlignment_Left_Center
              dr ("ColumnAlignment")  = "Left Center"
              dr ("ColumnAlignment_VAL")  = 1
            case enumVHAlignment.VHAlignment_Center_Bottom
              dr ("ColumnAlignment")  = "Center Bottom"
              dr ("ColumnAlignment_VAL")  = 5
            case enumVHAlignment.VHAlignment_Left_Bottom
              dr ("ColumnAlignment")  = "Left Bottom"
              dr ("ColumnAlignment_VAL")  = 2
              end select 'ColumnAlignment
             select case ColSort
            case enumColumnSortType.ColumnSortType_As_String
              dr ("ColSort")  = "As String"
              dr ("ColSort_VAL")  = 0
            case enumColumnSortType.ColumnSortType_As_Numeric
              dr ("ColSort")  = "As Numeric"
              dr ("ColSort_VAL")  = 1
            case enumColumnSortType.ColumnSortType_As_Date
              dr ("ColSort")  = "As Date"
              dr ("ColSort_VAL")  = 2
              end select 'ColSort
             dr("TheStyle") =TheStyle
             select case GroupAggregation
            case enumAggregationType.AggregationType_SUM
              dr ("GroupAggregation")  = "SUM"
              dr ("GroupAggregation_VAL")  = 3
            case enumAggregationType.AggregationType_AVG
              dr ("GroupAggregation")  = "AVG"
              dr ("GroupAggregation_VAL")  = 1
            case enumAggregationType.AggregationType_CUSTOM
              dr ("GroupAggregation")  = "CUSTOM"
              dr ("GroupAggregation_VAL")  = 6
            case enumAggregationType.AggregationType_none
              dr ("GroupAggregation")  = "none"
              dr ("GroupAggregation_VAL")  = 0
            case enumAggregationType.AggregationType_COUNT
              dr ("GroupAggregation")  = "COUNT"
              dr ("GroupAggregation_VAL")  = 2
            case enumAggregationType.AggregationType_MAX
              dr ("GroupAggregation")  = "MAX"
              dr ("GroupAggregation_VAL")  = 5
            case enumAggregationType.AggregationType_MIN
              dr ("GroupAggregation")  = "MIN"
              dr ("GroupAggregation_VAL")  = 4
              end select 'GroupAggregation
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
          nv.Add("sequence", sequence, dbtype.Int32)
          nv.Add("name", name, dbtype.string)
          nv.Add("ViewField", ViewField, dbtype.string)
          nv.Add("colwidth", colwidth, dbtype.Int32)
          nv.Add("ColumnAlignment", ColumnAlignment, dbtype.int16)
          nv.Add("ColSort", ColSort, dbtype.int16)
          nv.Add("TheStyle", TheStyle, dbtype.string)
          nv.Add("GroupAggregation", GroupAggregation, dbtype.int16)
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
          If reader.Table.Columns.Contains("sequence") Then m_sequence=reader.item("sequence")
          If reader.Table.Columns.Contains("name") Then m_name=reader.item("name").ToString()
          If reader.Table.Columns.Contains("ViewField") Then m_ViewField=reader.item("ViewField").ToString()
          If reader.Table.Columns.Contains("colwidth") Then m_colwidth=reader.item("colwidth")
          If reader.Table.Columns.Contains("ColumnAlignment") Then m_ColumnAlignment=reader.item("ColumnAlignment")
          If reader.Table.Columns.Contains("ColSort") Then m_ColSort=reader.item("ColSort")
          If reader.Table.Columns.Contains("TheStyle") Then m_TheStyle=reader.item("TheStyle").ToString()
          If reader.Table.Columns.Contains("GroupAggregation") Then m_GroupAggregation=reader.item("GroupAggregation")
        End Sub


''' <summary>
'''Доступ к полю Последовательность
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property sequence() As long
            Get
                LoadFromDatabase()
                sequence = m_sequence
                AccessTime = Now
            End Get
            Set(ByVal Value As long )
                LoadFromDatabase()
                m_sequence = Value
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю Название
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property name() As String
            Get
                LoadFromDatabase()
                name = m_name
                AccessTime = Now
            End Get
            Set(ByVal Value As String )
                LoadFromDatabase()
                m_name = Value
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю Поле представления
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property ViewField() As String
            Get
                LoadFromDatabase()
                ViewField = m_ViewField
                AccessTime = Now
            End Get
            Set(ByVal Value As String )
                LoadFromDatabase()
                m_ViewField = Value
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю Ширина колонки
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property colwidth() As long
            Get
                LoadFromDatabase()
                colwidth = m_colwidth
                AccessTime = Now
            End Get
            Set(ByVal Value As long )
                LoadFromDatabase()
                m_colwidth = Value
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю Выравнивание
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property ColumnAlignment() As enumVHAlignment
            Get
                LoadFromDatabase()
                ColumnAlignment = m_ColumnAlignment
                AccessTime = Now
            End Get
            Set(ByVal Value As enumVHAlignment )
                LoadFromDatabase()
                m_ColumnAlignment = Value
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю Сортировка колонки
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property ColSort() As enumColumnSortType
            Get
                LoadFromDatabase()
                ColSort = m_ColSort
                AccessTime = Now
            End Get
            Set(ByVal Value As enumColumnSortType )
                LoadFromDatabase()
                m_ColSort = Value
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
'''Доступ к полю Аггрегация при группировке
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property GroupAggregation() As enumAggregationType
            Get
                LoadFromDatabase()
                GroupAggregation = m_GroupAggregation
                AccessTime = Now
            End Get
            Set(ByVal Value As enumAggregationType )
                LoadFromDatabase()
                m_GroupAggregation = Value
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
            sequence = node.Attributes.GetNamedItem("sequence").Value
            name = node.Attributes.GetNamedItem("name").Value
            ViewField = node.Attributes.GetNamedItem("ViewField").Value
            colwidth = node.Attributes.GetNamedItem("colwidth").Value
            ColumnAlignment = node.Attributes.GetNamedItem("ColumnAlignment").Value
            ColSort = node.Attributes.GetNamedItem("ColSort").Value
            TheStyle = node.Attributes.GetNamedItem("TheStyle").Value
            GroupAggregation = node.Attributes.GetNamedItem("GroupAggregation").Value
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
          node.SetAttribute("sequence", sequence)  
          node.SetAttribute("name", name)  
          node.SetAttribute("ViewField", ViewField)  
          node.SetAttribute("colwidth", colwidth)  
          node.SetAttribute("ColumnAlignment", ColumnAlignment)  
          node.SetAttribute("ColSort", ColSort)  
          node.SetAttribute("TheStyle", TheStyle)  
          node.SetAttribute("GroupAggregation", GroupAggregation)  
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
