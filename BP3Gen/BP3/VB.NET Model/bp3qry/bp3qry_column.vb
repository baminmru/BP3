

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
'''Реализация строки раздела Колонка
''' </summary>
''' <remarks>
'''
''' </remarks>
    Public Class bp3qry_column
        Inherits BP3.Document.DocRow_Base



''' <summary>
'''Локальная переменная для поля Название
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_Name  as String


''' <summary>
'''Локальная переменная для поля Раздел
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_FromPart  as System.Guid


''' <summary>
'''Локальная переменная для поля Агрегация
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_Aggregation  as enumAggregationType


''' <summary>
'''Локальная переменная для поля №
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_sequence  as long


''' <summary>
'''Локальная переменная для поля Для комбо
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_ForCombo  as enumBoolean


''' <summary>
'''Локальная переменная для поля Формула
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_Expression  as STRING


''' <summary>
'''Локальная переменная для поля Псвдоним
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_the_Alias  as String


''' <summary>
'''Локальная переменная для поля Поле
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_Field  as System.Guid



''' <summary>
'''Очистить поля раздела
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Overrides Sub CleanFields()
            ' m_Name=   
            ' m_FromPart=   
            ' m_Aggregation=   
            ' m_sequence=   
            ' m_ForCombo=   
            ' m_Expression=   
            ' m_the_Alias=   
            ' m_Field=   
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
                    Value = ForCombo
                Case 2
                    Value = FromPart
                Case 3
                    Value = Aggregation
                Case 4
                    Value = sequence
                Case 5
                    Value = the_Alias
                Case 6
                    Value = Name
                Case 7
                    Value = Field
                Case 8
                    Value = Expression
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
                    ForCombo = value
                Case 2
                    FromPart = value
                Case 3
                    Aggregation = value
                Case 4
                    sequence = value
                Case 5
                    the_Alias = value
                Case 6
                    Name = value
                Case 7
                    Field = value
                Case 8
                    Expression = value
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
                    Return "ForCombo"
                Case 2
                    Return "FromPart"
                Case 3
                    Return "Aggregation"
                Case 4
                    Return "sequence"
                Case 5
                    Return "the_Alias"
                Case 6
                    Return "Name"
                Case 7
                    Return "Field"
                Case 8
                    Return "Expression"
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
             select case ForCombo
            case enumBoolean.Boolean_Da
              dr ("ForCombo")  = "Да"
              dr ("ForCombo_VAL")  = -1
            case enumBoolean.Boolean_Net
              dr ("ForCombo")  = "Нет"
              dr ("ForCombo_VAL")  = 0
              end select 'ForCombo
             if FromPart is nothing then
               dr("FromPart") =system.dbnull.value
               dr("FromPart_ID") =System.Guid.Empty
             else
               dr("FromPart") =FromPart.BRIEF
               dr("FromPart_ID") =FromPart.ID
             end if 
             select case Aggregation
            case enumAggregationType.AggregationType_SUM
              dr ("Aggregation")  = "SUM"
              dr ("Aggregation_VAL")  = 3
            case enumAggregationType.AggregationType_AVG
              dr ("Aggregation")  = "AVG"
              dr ("Aggregation_VAL")  = 1
            case enumAggregationType.AggregationType_CUSTOM
              dr ("Aggregation")  = "CUSTOM"
              dr ("Aggregation_VAL")  = 6
            case enumAggregationType.AggregationType_none
              dr ("Aggregation")  = "none"
              dr ("Aggregation_VAL")  = 0
            case enumAggregationType.AggregationType_COUNT
              dr ("Aggregation")  = "COUNT"
              dr ("Aggregation_VAL")  = 2
            case enumAggregationType.AggregationType_MAX
              dr ("Aggregation")  = "MAX"
              dr ("Aggregation_VAL")  = 5
            case enumAggregationType.AggregationType_MIN
              dr ("Aggregation")  = "MIN"
              dr ("Aggregation_VAL")  = 4
              end select 'Aggregation
             dr("sequence") =sequence
             dr("the_Alias") =the_Alias
             dr("Name") =Name
             if Field is nothing then
               dr("Field") =system.dbnull.value
               dr("Field_ID") =System.Guid.Empty
             else
               dr("Field") =Field.BRIEF
               dr("Field_ID") =Field.ID
             end if 
             dr("Expression") =Expression
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
          nv.Add("ForCombo", ForCombo, dbtype.int16)
          if m_FromPart.Equals(System.Guid.Empty) then
            nv.Add("FromPart", system.dbnull.value, Application.Session.GetProvider.ID2DbType, Application.Session.GetProvider.ID2Size)
          else
            nv.Add("FromPart", Application.Session.GetProvider.ID2Param(m_FromPart), Application.Session.GetProvider.ID2DbType, Application.Session.GetProvider.ID2Size)
          end if 
          nv.Add("Aggregation", Aggregation, dbtype.int16)
          nv.Add("sequence", sequence, dbtype.Int32)
          nv.Add("the_Alias", the_Alias, dbtype.string)
          nv.Add("Name", Name, dbtype.string)
          if m_Field.Equals(System.Guid.Empty) then
            nv.Add("Field", system.dbnull.value, Application.Session.GetProvider.ID2DbType, Application.Session.GetProvider.ID2Size)
          else
            nv.Add("Field", Application.Session.GetProvider.ID2Param(m_Field), Application.Session.GetProvider.ID2DbType, Application.Session.GetProvider.ID2Size)
          end if 
          nv.Add("Expression", Expression, dbtype.string)
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
          If reader.Table.Columns.Contains("ForCombo") Then m_ForCombo=reader.item("ForCombo")
      If reader.Table.Columns.Contains("FromPart") Then
          if isdbnull(reader.item("FromPart")) then
            If reader.Table.Columns.Contains("FromPart") Then m_FromPart = System.GUID.Empty
          else
            If reader.Table.Columns.Contains("FromPart") Then m_FromPart= New System.Guid(reader.item("FromPart").ToString())
          end if 
      end if 
          If reader.Table.Columns.Contains("Aggregation") Then m_Aggregation=reader.item("Aggregation")
          If reader.Table.Columns.Contains("sequence") Then m_sequence=reader.item("sequence")
          If reader.Table.Columns.Contains("the_Alias") Then m_the_Alias=reader.item("the_Alias").ToString()
          If reader.Table.Columns.Contains("Name") Then m_Name=reader.item("Name").ToString()
      If reader.Table.Columns.Contains("Field") Then
          if isdbnull(reader.item("Field")) then
            If reader.Table.Columns.Contains("Field") Then m_Field = System.GUID.Empty
          else
            If reader.Table.Columns.Contains("Field") Then m_Field= New System.Guid(reader.item("Field").ToString())
          end if 
      end if 
          If reader.Table.Columns.Contains("Expression") Then m_Expression=reader.item("Expression").ToString()
        End Sub


''' <summary>
'''Доступ к полю Для комбо
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property ForCombo() As enumBoolean
            Get
                LoadFromDatabase()
                ForCombo = m_ForCombo
                AccessTime = Now
            End Get
            Set(ByVal Value As enumBoolean )
                LoadFromDatabase()
                m_ForCombo = Value
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю Раздел
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property FromPart() As BP3.Document.docrow_base
            Get
                LoadFromDatabase()
                FromPart = me.application.Findrowobject("bp3doc_store",m_FromPart)
                AccessTime = Now
            End Get
            Set(ByVal Value As BP3.Document.docrow_base )
                LoadFromDatabase()
                if not Value is nothing then
                    m_FromPart = Value.id
                else
                   m_FromPart=System.Guid.Empty
                end if
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю Агрегация
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property Aggregation() As enumAggregationType
            Get
                LoadFromDatabase()
                Aggregation = m_Aggregation
                AccessTime = Now
            End Get
            Set(ByVal Value As enumAggregationType )
                LoadFromDatabase()
                m_Aggregation = Value
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю №
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
'''Доступ к полю Псвдоним
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
'''Доступ к полю Поле
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property Field() As BP3.Document.docrow_base
            Get
                LoadFromDatabase()
                Field = me.application.Findrowobject("bp3doc_field",m_Field)
                AccessTime = Now
            End Get
            Set(ByVal Value As BP3.Document.docrow_base )
                LoadFromDatabase()
                if not Value is nothing then
                    m_Field = Value.id
                else
                   m_Field=System.Guid.Empty
                end if
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю Формула
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property Expression() As STRING
            Get
                LoadFromDatabase()
                Expression = m_Expression
                AccessTime = Now
            End Get
            Set(ByVal Value As STRING )
                LoadFromDatabase()
                m_Expression = Value
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
            ForCombo = node.Attributes.GetNamedItem("ForCombo").Value
            m_FromPart = new system.guid(node.Attributes.GetNamedItem("FromPart").Value)
            Aggregation = node.Attributes.GetNamedItem("Aggregation").Value
            sequence = node.Attributes.GetNamedItem("sequence").Value
            the_Alias = node.Attributes.GetNamedItem("the_Alias").Value
            Name = node.Attributes.GetNamedItem("Name").Value
            m_Field = new system.guid(node.Attributes.GetNamedItem("Field").Value)
            Expression = node.Attributes.GetNamedItem("Expression").Value
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
          node.SetAttribute("ForCombo", ForCombo)  
          node.SetAttribute("FromPart", m_FromPart.tostring)  
          node.SetAttribute("Aggregation", Aggregation)  
          node.SetAttribute("sequence", sequence)  
          node.SetAttribute("the_Alias", the_Alias)  
          node.SetAttribute("Name", Name)  
          node.SetAttribute("Field", m_Field.tostring)  
          node.SetAttribute("Expression", Expression)  
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
