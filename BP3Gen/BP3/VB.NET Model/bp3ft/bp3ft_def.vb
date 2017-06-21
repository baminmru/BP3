

Option Explicit On

Imports System
Imports System.IO
Imports BP3
Imports System.xml
Imports System.Data
Imports System.Convert
Imports System.DateTime

Namespace bp3ft


''' <summary>
'''Реализация строки раздела Тип поля
''' </summary>
''' <remarks>
'''
''' </remarks>
    Public Class bp3ft_def
        Inherits BP3.Document.DocRow_Base



''' <summary>
'''Локальная переменная для поля Трактовка
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_TypeStyle  as enumTypeStyle


''' <summary>
'''Локальная переменная для поля Название
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_Name  as String


''' <summary>
'''Локальная переменная для поля Вариант сортировки в табличном представлении
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_GridSortType  as enumColumnSortType


''' <summary>
'''Локальная переменная для поля Описание
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_the_Comment  as STRING


''' <summary>
'''Локальная переменная для поля Нужен размер
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_AllowSize  as enumBoolean


''' <summary>
'''Локальная переменная для поля Поиск текста
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_AllowLikeSearch  as enumBoolean


''' <summary>
'''Локальная переменная для поля Максимум
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_Maximum  as String


''' <summary>
'''Локальная переменная для поля Минимум
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_Minimum  as String


''' <summary>
'''Локальная переменная для поля Отложенное сохранение
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_DelayedSave  as enumBoolean



''' <summary>
'''Очистить поля раздела
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Overrides Sub CleanFields()
            ' m_TypeStyle=   
            ' m_Name=   
            ' m_GridSortType=   
            ' m_the_Comment=   
            ' m_AllowSize=   
            ' m_AllowLikeSearch=   
            ' m_Maximum=   
            ' m_Minimum=   
            ' m_DelayedSave=   
        End Sub



''' <summary>
'''Количество полей в строке раздела
''' </summary>
''' <remarks>
'''
''' </remarks>
    Public Overrides ReadOnly Property Count() As Long
        Get
           Count = 9
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
                    Value = TypeStyle
                Case 2
                    Value = Name
                Case 3
                    Value = GridSortType
                Case 4
                    Value = the_Comment
                Case 5
                    Value = AllowSize
                Case 6
                    Value = AllowLikeSearch
                Case 7
                    Value = Maximum
                Case 8
                    Value = Minimum
                Case 9
                    Value = DelayedSave
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
                    TypeStyle = value
                Case 2
                    Name = value
                Case 3
                    GridSortType = value
                Case 4
                    the_Comment = value
                Case 5
                    AllowSize = value
                Case 6
                    AllowLikeSearch = value
                Case 7
                    Maximum = value
                Case 8
                    Minimum = value
                Case 9
                    DelayedSave = value
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
                    Return "TypeStyle"
                Case 2
                    Return "Name"
                Case 3
                    Return "GridSortType"
                Case 4
                    Return "the_Comment"
                Case 5
                    Return "AllowSize"
                Case 6
                    Return "AllowLikeSearch"
                Case 7
                    Return "Maximum"
                Case 8
                    Return "Minimum"
                Case 9
                    Return "DelayedSave"
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
             select case TypeStyle
            case enumTypeStyle.TypeStyle_Ssilka
              dr ("TypeStyle")  = "Ссылка"
              dr ("TypeStyle_VAL")  = 4
            case enumTypeStyle.TypeStyle_Viragenie
              dr ("TypeStyle")  = "Выражение"
              dr ("TypeStyle_VAL")  = 1
            case enumTypeStyle.TypeStyle_Element_oformleniy
              dr ("TypeStyle")  = "Элемент оформления"
              dr ("TypeStyle_VAL")  = 5
            case enumTypeStyle.TypeStyle_Interval
              dr ("TypeStyle")  = "Интервал"
              dr ("TypeStyle_VAL")  = 3
            case enumTypeStyle.TypeStyle_Perecislenie
              dr ("TypeStyle")  = "Перечисление"
              dr ("TypeStyle_VAL")  = 2
            case enumTypeStyle.TypeStyle_Skalyrniy_tip
              dr ("TypeStyle")  = "Скалярный тип"
              dr ("TypeStyle_VAL")  = 0
              end select 'TypeStyle
             dr("Name") =Name
             select case GridSortType
            case enumColumnSortType.ColumnSortType_As_String
              dr ("GridSortType")  = "As String"
              dr ("GridSortType_VAL")  = 0
            case enumColumnSortType.ColumnSortType_As_Numeric
              dr ("GridSortType")  = "As Numeric"
              dr ("GridSortType_VAL")  = 1
            case enumColumnSortType.ColumnSortType_As_Date
              dr ("GridSortType")  = "As Date"
              dr ("GridSortType_VAL")  = 2
              end select 'GridSortType
             dr("the_Comment") =the_Comment
             select case AllowSize
            case enumBoolean.Boolean_Da
              dr ("AllowSize")  = "Да"
              dr ("AllowSize_VAL")  = -1
            case enumBoolean.Boolean_Net
              dr ("AllowSize")  = "Нет"
              dr ("AllowSize_VAL")  = 0
              end select 'AllowSize
             select case AllowLikeSearch
            case enumBoolean.Boolean_Da
              dr ("AllowLikeSearch")  = "Да"
              dr ("AllowLikeSearch_VAL")  = -1
            case enumBoolean.Boolean_Net
              dr ("AllowLikeSearch")  = "Нет"
              dr ("AllowLikeSearch_VAL")  = 0
              end select 'AllowLikeSearch
             dr("Maximum") =Maximum
             dr("Minimum") =Minimum
             select case DelayedSave
            case enumBoolean.Boolean_Da
              dr ("DelayedSave")  = "Да"
              dr ("DelayedSave_VAL")  = -1
            case enumBoolean.Boolean_Net
              dr ("DelayedSave")  = "Нет"
              dr ("DelayedSave_VAL")  = 0
              end select 'DelayedSave
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
          nv.Add("TypeStyle", TypeStyle, dbtype.int16)
          nv.Add("Name", Name, dbtype.string)
          nv.Add("GridSortType", GridSortType, dbtype.int16)
          nv.Add("the_Comment", the_Comment, dbtype.string)
          nv.Add("AllowSize", AllowSize, dbtype.int16)
          nv.Add("AllowLikeSearch", AllowLikeSearch, dbtype.int16)
          nv.Add("Maximum", Maximum, dbtype.string)
          nv.Add("Minimum", Minimum, dbtype.string)
          nv.Add("DelayedSave", DelayedSave, dbtype.int16)
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
          If reader.Table.Columns.Contains("TypeStyle") Then m_TypeStyle=reader.item("TypeStyle")
          If reader.Table.Columns.Contains("Name") Then m_Name=reader.item("Name").ToString()
          If reader.Table.Columns.Contains("GridSortType") Then m_GridSortType=reader.item("GridSortType")
          If reader.Table.Columns.Contains("the_Comment") Then m_the_Comment=reader.item("the_Comment").ToString()
          If reader.Table.Columns.Contains("AllowSize") Then m_AllowSize=reader.item("AllowSize")
          If reader.Table.Columns.Contains("AllowLikeSearch") Then m_AllowLikeSearch=reader.item("AllowLikeSearch")
          If reader.Table.Columns.Contains("Maximum") Then m_Maximum=reader.item("Maximum").ToString()
          If reader.Table.Columns.Contains("Minimum") Then m_Minimum=reader.item("Minimum").ToString()
          If reader.Table.Columns.Contains("DelayedSave") Then m_DelayedSave=reader.item("DelayedSave")
        End Sub


''' <summary>
'''Доступ к полю Трактовка
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property TypeStyle() As enumTypeStyle
            Get
                LoadFromDatabase()
                TypeStyle = m_TypeStyle
                AccessTime = Now
            End Get
            Set(ByVal Value As enumTypeStyle )
                LoadFromDatabase()
                m_TypeStyle = Value
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
'''Доступ к полю Вариант сортировки в табличном представлении
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property GridSortType() As enumColumnSortType
            Get
                LoadFromDatabase()
                GridSortType = m_GridSortType
                AccessTime = Now
            End Get
            Set(ByVal Value As enumColumnSortType )
                LoadFromDatabase()
                m_GridSortType = Value
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю Описание
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property the_Comment() As STRING
            Get
                LoadFromDatabase()
                the_Comment = m_the_Comment
                AccessTime = Now
            End Get
            Set(ByVal Value As STRING )
                LoadFromDatabase()
                m_the_Comment = Value
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю Нужен размер
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property AllowSize() As enumBoolean
            Get
                LoadFromDatabase()
                AllowSize = m_AllowSize
                AccessTime = Now
            End Get
            Set(ByVal Value As enumBoolean )
                LoadFromDatabase()
                m_AllowSize = Value
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю Поиск текста
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property AllowLikeSearch() As enumBoolean
            Get
                LoadFromDatabase()
                AllowLikeSearch = m_AllowLikeSearch
                AccessTime = Now
            End Get
            Set(ByVal Value As enumBoolean )
                LoadFromDatabase()
                m_AllowLikeSearch = Value
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю Максимум
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property Maximum() As String
            Get
                LoadFromDatabase()
                Maximum = m_Maximum
                AccessTime = Now
            End Get
            Set(ByVal Value As String )
                LoadFromDatabase()
                m_Maximum = Value
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю Минимум
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property Minimum() As String
            Get
                LoadFromDatabase()
                Minimum = m_Minimum
                AccessTime = Now
            End Get
            Set(ByVal Value As String )
                LoadFromDatabase()
                m_Minimum = Value
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю Отложенное сохранение
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property DelayedSave() As enumBoolean
            Get
                LoadFromDatabase()
                DelayedSave = m_DelayedSave
                AccessTime = Now
            End Get
            Set(ByVal Value As enumBoolean )
                LoadFromDatabase()
                m_DelayedSave = Value
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
            TypeStyle = node.Attributes.GetNamedItem("TypeStyle").Value
            Name = node.Attributes.GetNamedItem("Name").Value
            GridSortType = node.Attributes.GetNamedItem("GridSortType").Value
            the_Comment = node.Attributes.GetNamedItem("the_Comment").Value
            AllowSize = node.Attributes.GetNamedItem("AllowSize").Value
            AllowLikeSearch = node.Attributes.GetNamedItem("AllowLikeSearch").Value
            Maximum = node.Attributes.GetNamedItem("Maximum").Value
            Minimum = node.Attributes.GetNamedItem("Minimum").Value
            DelayedSave = node.Attributes.GetNamedItem("DelayedSave").Value
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
          node.SetAttribute("TypeStyle", TypeStyle)  
          node.SetAttribute("Name", Name)  
          node.SetAttribute("GridSortType", GridSortType)  
          node.SetAttribute("the_Comment", the_Comment)  
          node.SetAttribute("AllowSize", AllowSize)  
          node.SetAttribute("AllowLikeSearch", AllowLikeSearch)  
          node.SetAttribute("Maximum", Maximum)  
          node.SetAttribute("Minimum", Minimum)  
          node.SetAttribute("DelayedSave", DelayedSave)  
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
