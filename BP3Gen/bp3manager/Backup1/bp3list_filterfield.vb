

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
'''Реализация строки раздела Поле фильтра
''' </summary>
''' <remarks>
'''
''' </remarks>
    Public Class bp3list_filterfield
        Inherits LATIR2.Document.DocRow_Base



''' <summary>
'''Локальная переменная для поля Раздел, куда ссылаемся
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_RefToPart  as System.Guid


''' <summary>
'''Локальная переменная для поля Тип ссылки
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_RefType  as enumReferenceType


''' <summary>
'''Локальная переменная для поля Массив значений
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_ValueArray  as enumBoolean


''' <summary>
'''Локальная переменная для поля Последовательность
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_sequence  as long


''' <summary>
'''Локальная переменная для поля Заголовок
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_Caption  as String


''' <summary>
'''Локальная переменная для поля Название
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_Name  as String


''' <summary>
'''Локальная переменная для поля Тип поля
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_FieldType  as System.Guid


''' <summary>
'''Локальная переменная для поля Размер
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_FieldSize  as long



''' <summary>
'''Очистить поля раздела
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Overrides Sub CleanFields()
            ' m_RefToPart=   
            ' m_RefType=   
            ' m_ValueArray=   
            ' m_sequence=   
            ' m_Caption=   
            ' m_Name=   
            ' m_FieldType=   
            ' m_FieldSize=   
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
                    Value = FieldSize
                Case 2
                    Value = RefType
                Case 3
                    Value = Caption
                Case 4
                    Value = Name
                Case 5
                    Value = FieldType
                Case 6
                    Value = ValueArray
                Case 7
                    Value = sequence
                Case 8
                    Value = RefToPart
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
                    FieldSize = value
                Case 2
                    RefType = value
                Case 3
                    Caption = value
                Case 4
                    Name = value
                Case 5
                    FieldType = value
                Case 6
                    ValueArray = value
                Case 7
                    sequence = value
                Case 8
                    RefToPart = value
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
                    Return "FieldSize"
                Case 2
                    Return "RefType"
                Case 3
                    Return "Caption"
                Case 4
                    Return "Name"
                Case 5
                    Return "FieldType"
                Case 6
                    Return "ValueArray"
                Case 7
                    Return "sequence"
                Case 8
                    Return "RefToPart"
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
             dr("FieldSize") =FieldSize
             select case RefType
            case enumReferenceType.ReferenceType_Na_istocnik_dannih
              dr ("RefType")  = "На источник данных"
              dr ("RefType_VAL")  = 3
            case enumReferenceType.ReferenceType_Skalyrnoe_pole_OPN_ne_ssilkaCLS
              dr ("RefType")  = "Скалярное поле ( не ссылка)"
              dr ("RefType_VAL")  = 0
            case enumReferenceType.ReferenceType_Na_stroku_razdela
              dr ("RefType")  = "На строку раздела"
              dr ("RefType_VAL")  = 2
            case enumReferenceType.ReferenceType_Na_ob_ekt_
              dr ("RefType")  = "На объект "
              dr ("RefType_VAL")  = 1
              end select 'RefType
             dr("Caption") =Caption
             dr("Name") =Name
             if FieldType is nothing then
               dr("FieldType") =system.dbnull.value
               dr("FieldType_ID") =System.Guid.Empty
             else
               dr("FieldType") =FieldType.BRIEF
               dr("FieldType_ID") =FieldType.ID
             end if 
             select case ValueArray
            case enumBoolean.Boolean_Da
              dr ("ValueArray")  = "Да"
              dr ("ValueArray_VAL")  = -1
            case enumBoolean.Boolean_Net
              dr ("ValueArray")  = "Нет"
              dr ("ValueArray_VAL")  = 0
              end select 'ValueArray
             dr("sequence") =sequence
             if RefToPart is nothing then
               dr("RefToPart") =system.dbnull.value
               dr("RefToPart_ID") =System.Guid.Empty
             else
               dr("RefToPart") =RefToPart.BRIEF
               dr("RefToPart_ID") =RefToPart.ID
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
          nv.Add("FieldSize", FieldSize, dbtype.Int32)
          nv.Add("RefType", RefType, dbtype.int16)
          nv.Add("Caption", Caption, dbtype.string)
          nv.Add("Name", Name, dbtype.string)
          if m_FieldType.Equals(System.Guid.Empty) then
            nv.Add("FieldType", system.dbnull.value, Application.Session.GetProvider.ID2DbType, Application.Session.GetProvider.ID2Size)
          else
            nv.Add("FieldType", Application.Session.GetProvider.ID2Param(m_FieldType), Application.Session.GetProvider.ID2DbType, Application.Session.GetProvider.ID2Size)
          end if 
          nv.Add("ValueArray", ValueArray, dbtype.int16)
          nv.Add("sequence", sequence, dbtype.Int32)
          if m_RefToPart.Equals(System.Guid.Empty) then
            nv.Add("RefToPart", system.dbnull.value, Application.Session.GetProvider.ID2DbType, Application.Session.GetProvider.ID2Size)
          else
            nv.Add("RefToPart", Application.Session.GetProvider.ID2Param(m_RefToPart), Application.Session.GetProvider.ID2DbType, Application.Session.GetProvider.ID2Size)
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
          If reader.Table.Columns.Contains("FieldSize") Then m_FieldSize=reader.item("FieldSize")
          If reader.Table.Columns.Contains("RefType") Then m_RefType=reader.item("RefType")
          If reader.Table.Columns.Contains("Caption") Then m_Caption=reader.item("Caption").ToString()
          If reader.Table.Columns.Contains("Name") Then m_Name=reader.item("Name").ToString()
      If reader.Table.Columns.Contains("FieldType") Then
          if isdbnull(reader.item("FieldType")) then
            If reader.Table.Columns.Contains("FieldType") Then m_FieldType = System.GUID.Empty
          else
            If reader.Table.Columns.Contains("FieldType") Then m_FieldType= New System.Guid(reader.item("FieldType").ToString())
          end if 
      end if 
          If reader.Table.Columns.Contains("ValueArray") Then m_ValueArray=reader.item("ValueArray")
          If reader.Table.Columns.Contains("sequence") Then m_sequence=reader.item("sequence")
      If reader.Table.Columns.Contains("RefToPart") Then
          if isdbnull(reader.item("RefToPart")) then
            If reader.Table.Columns.Contains("RefToPart") Then m_RefToPart = System.GUID.Empty
          else
            If reader.Table.Columns.Contains("RefToPart") Then m_RefToPart= New System.Guid(reader.item("RefToPart").ToString())
          end if 
      end if 
        End Sub


''' <summary>
'''Доступ к полю Размер
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property FieldSize() As long
            Get
                LoadFromDatabase()
                FieldSize = m_FieldSize
                AccessTime = Now
            End Get
            Set(ByVal Value As long )
                LoadFromDatabase()
                m_FieldSize = Value
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю Тип ссылки
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property RefType() As enumReferenceType
            Get
                LoadFromDatabase()
                RefType = m_RefType
                AccessTime = Now
            End Get
            Set(ByVal Value As enumReferenceType )
                LoadFromDatabase()
                m_RefType = Value
                ChangeTime = Now
            End Set
        End Property


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
'''Доступ к полю Тип поля
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property FieldType() As LATIR2.Document.docrow_base
            Get
                LoadFromDatabase()
                FieldType = me.application.Findrowobject("bp3ft_def",m_FieldType)
                AccessTime = Now
            End Get
            Set(ByVal Value As LATIR2.Document.docrow_base )
                LoadFromDatabase()
                if not Value is nothing then
                    m_FieldType = Value.id
                else
                   m_FieldType=System.Guid.Empty
                end if
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю Массив значений
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property ValueArray() As enumBoolean
            Get
                LoadFromDatabase()
                ValueArray = m_ValueArray
                AccessTime = Now
            End Get
            Set(ByVal Value As enumBoolean )
                LoadFromDatabase()
                m_ValueArray = Value
                ChangeTime = Now
            End Set
        End Property


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
'''Доступ к полю Раздел, куда ссылаемся
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property RefToPart() As LATIR2.Document.docrow_base
            Get
                LoadFromDatabase()
                RefToPart = me.application.Findrowobject("bp3doc_store",m_RefToPart)
                AccessTime = Now
            End Get
            Set(ByVal Value As LATIR2.Document.docrow_base )
                LoadFromDatabase()
                if not Value is nothing then
                    m_RefToPart = Value.id
                else
                   m_RefToPart=System.Guid.Empty
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
            FieldSize = node.Attributes.GetNamedItem("FieldSize").Value
            RefType = node.Attributes.GetNamedItem("RefType").Value
            Caption = node.Attributes.GetNamedItem("Caption").Value
            Name = node.Attributes.GetNamedItem("Name").Value
            m_FieldType = new system.guid(node.Attributes.GetNamedItem("FieldType").Value)
            ValueArray = node.Attributes.GetNamedItem("ValueArray").Value
            sequence = node.Attributes.GetNamedItem("sequence").Value
            m_RefToPart = new system.guid(node.Attributes.GetNamedItem("RefToPart").Value)
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
          node.SetAttribute("FieldSize", FieldSize)  
          node.SetAttribute("RefType", RefType)  
          node.SetAttribute("Caption", Caption)  
          node.SetAttribute("Name", Name)  
          node.SetAttribute("FieldType", m_FieldType.tostring)  
          node.SetAttribute("ValueArray", ValueArray)  
          node.SetAttribute("sequence", sequence)  
          node.SetAttribute("RefToPart", m_RefToPart.tostring)  
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
