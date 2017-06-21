

Option Explicit On

Imports System
Imports System.IO
Imports BP3
Imports System.xml
Imports System.Data
Imports System.Convert
Imports System.DateTime

Namespace bp3card


''' <summary>
'''Реализация строки раздела Поля карточки
''' </summary>
''' <remarks>
'''
''' </remarks>
    Public Class bp3card_fld
        Inherits BP3.Document.DocRow_Base



''' <summary>
'''Локальная переменная для поля Имя группы
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_FieldGroupBox  as String


''' <summary>
'''Локальная переменная для поля № п/п
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_Sequence  as long


''' <summary>
'''Локальная переменная для поля Структура, которой принадлежит поле
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_ThePart  as System.Guid


''' <summary>
'''Локальная переменная для поля Разрешена модификация
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_AllowModify  as enumBoolean


''' <summary>
'''Локальная переменная для поля Поле, на которое накладывается ограничение
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_TheField  as System.Guid


''' <summary>
'''Локальная переменная для поля Имя вкладки
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_TabName  as String


''' <summary>
'''Локальная переменная для поля Стиль
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_TheStyle  as String


''' <summary>
'''Локальная переменная для поля Надпись
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_Caption  as String


''' <summary>
'''Локальная переменная для поля Разрешен просмотр
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_AllowRead  as enumBoolean


''' <summary>
'''Локальная переменная для поля Обязательное поле
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_MandatoryField  as enumTriState



''' <summary>
'''Очистить поля раздела
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Overrides Sub CleanFields()
            ' m_FieldGroupBox=   
            ' m_Sequence=   
            ' m_ThePart=   
            ' m_AllowModify=   
            ' m_TheField=   
            ' m_TabName=   
            ' m_TheStyle=   
            ' m_Caption=   
            ' m_AllowRead=   
            ' m_MandatoryField=   
        End Sub



''' <summary>
'''Количество полей в строке раздела
''' </summary>
''' <remarks>
'''
''' </remarks>
    Public Overrides ReadOnly Property Count() As Long
        Get
           Count = 10
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
                    Value = Sequence
                Case 2
                    Value = MandatoryField
                Case 3
                    Value = TheField
                Case 4
                    Value = ThePart
                Case 5
                    Value = AllowModify
                Case 6
                    Value = AllowRead
                Case 7
                    Value = TabName
                Case 8
                    Value = FieldGroupBox
                Case 9
                    Value = TheStyle
                Case 10
                    Value = Caption
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
                    Sequence = value
                Case 2
                    MandatoryField = value
                Case 3
                    TheField = value
                Case 4
                    ThePart = value
                Case 5
                    AllowModify = value
                Case 6
                    AllowRead = value
                Case 7
                    TabName = value
                Case 8
                    FieldGroupBox = value
                Case 9
                    TheStyle = value
                Case 10
                    Caption = value
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
                    Return "Sequence"
                Case 2
                    Return "MandatoryField"
                Case 3
                    Return "TheField"
                Case 4
                    Return "ThePart"
                Case 5
                    Return "AllowModify"
                Case 6
                    Return "AllowRead"
                Case 7
                    Return "TabName"
                Case 8
                    Return "FieldGroupBox"
                Case 9
                    Return "TheStyle"
                Case 10
                    Return "Caption"
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
             dr("Sequence") =Sequence
             select case MandatoryField
            case enumTriState.TriState_Ne_susestvenno
              dr ("MandatoryField")  = "Не существенно"
              dr ("MandatoryField_VAL")  = -1
            case enumTriState.TriState_Da
              dr ("MandatoryField")  = "Да"
              dr ("MandatoryField_VAL")  = 1
            case enumTriState.TriState_Net
              dr ("MandatoryField")  = "Нет"
              dr ("MandatoryField_VAL")  = 0
              end select 'MandatoryField
             if TheField is nothing then
               dr("TheField") =system.dbnull.value
               dr("TheField_ID") =System.Guid.Empty
             else
               dr("TheField") =TheField.BRIEF
               dr("TheField_ID") =TheField.ID
             end if 
             if ThePart is nothing then
               dr("ThePart") =system.dbnull.value
               dr("ThePart_ID") =System.Guid.Empty
             else
               dr("ThePart") =ThePart.BRIEF
               dr("ThePart_ID") =ThePart.ID
             end if 
             select case AllowModify
            case enumBoolean.Boolean_Da
              dr ("AllowModify")  = "Да"
              dr ("AllowModify_VAL")  = -1
            case enumBoolean.Boolean_Net
              dr ("AllowModify")  = "Нет"
              dr ("AllowModify_VAL")  = 0
              end select 'AllowModify
             select case AllowRead
            case enumBoolean.Boolean_Da
              dr ("AllowRead")  = "Да"
              dr ("AllowRead_VAL")  = -1
            case enumBoolean.Boolean_Net
              dr ("AllowRead")  = "Нет"
              dr ("AllowRead_VAL")  = 0
              end select 'AllowRead
             dr("TabName") =TabName
             dr("FieldGroupBox") =FieldGroupBox
             dr("TheStyle") =TheStyle
             dr("Caption") =Caption
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
          nv.Add("Sequence", Sequence, dbtype.Int32)
          nv.Add("MandatoryField", MandatoryField, dbtype.int16)
          if m_TheField.Equals(System.Guid.Empty) then
            nv.Add("TheField", system.dbnull.value, Application.Session.GetProvider.ID2DbType, Application.Session.GetProvider.ID2Size)
          else
            nv.Add("TheField", Application.Session.GetProvider.ID2Param(m_TheField), Application.Session.GetProvider.ID2DbType, Application.Session.GetProvider.ID2Size)
          end if 
          if m_ThePart.Equals(System.Guid.Empty) then
            nv.Add("ThePart", system.dbnull.value, Application.Session.GetProvider.ID2DbType, Application.Session.GetProvider.ID2Size)
          else
            nv.Add("ThePart", Application.Session.GetProvider.ID2Param(m_ThePart), Application.Session.GetProvider.ID2DbType, Application.Session.GetProvider.ID2Size)
          end if 
          nv.Add("AllowModify", AllowModify, dbtype.int16)
          nv.Add("AllowRead", AllowRead, dbtype.int16)
          nv.Add("TabName", TabName, dbtype.string)
          nv.Add("FieldGroupBox", FieldGroupBox, dbtype.string)
          nv.Add("TheStyle", TheStyle, dbtype.string)
          nv.Add("Caption", Caption, dbtype.string)
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
          If reader.Table.Columns.Contains("Sequence") Then m_Sequence=reader.item("Sequence")
          If reader.Table.Columns.Contains("MandatoryField") Then m_MandatoryField=reader.item("MandatoryField")
      If reader.Table.Columns.Contains("TheField") Then
          if isdbnull(reader.item("TheField")) then
            If reader.Table.Columns.Contains("TheField") Then m_TheField = System.GUID.Empty
          else
            If reader.Table.Columns.Contains("TheField") Then m_TheField= New System.Guid(reader.item("TheField").ToString())
          end if 
      end if 
      If reader.Table.Columns.Contains("ThePart") Then
          if isdbnull(reader.item("ThePart")) then
            If reader.Table.Columns.Contains("ThePart") Then m_ThePart = System.GUID.Empty
          else
            If reader.Table.Columns.Contains("ThePart") Then m_ThePart= New System.Guid(reader.item("ThePart").ToString())
          end if 
      end if 
          If reader.Table.Columns.Contains("AllowModify") Then m_AllowModify=reader.item("AllowModify")
          If reader.Table.Columns.Contains("AllowRead") Then m_AllowRead=reader.item("AllowRead")
          If reader.Table.Columns.Contains("TabName") Then m_TabName=reader.item("TabName").ToString()
          If reader.Table.Columns.Contains("FieldGroupBox") Then m_FieldGroupBox=reader.item("FieldGroupBox").ToString()
          If reader.Table.Columns.Contains("TheStyle") Then m_TheStyle=reader.item("TheStyle").ToString()
          If reader.Table.Columns.Contains("Caption") Then m_Caption=reader.item("Caption").ToString()
        End Sub


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
'''Доступ к полю Обязательное поле
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property MandatoryField() As enumTriState
            Get
                LoadFromDatabase()
                MandatoryField = m_MandatoryField
                AccessTime = Now
            End Get
            Set(ByVal Value As enumTriState )
                LoadFromDatabase()
                m_MandatoryField = Value
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю Поле, на которое накладывается ограничение
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property TheField() As BP3.Document.docrow_base
            Get
                LoadFromDatabase()
                TheField = me.application.Findrowobject("bp3card_fld",m_TheField)
                AccessTime = Now
            End Get
            Set(ByVal Value As BP3.Document.docrow_base )
                LoadFromDatabase()
                if not Value is nothing then
                    m_TheField = Value.id
                else
                   m_TheField=System.Guid.Empty
                end if
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю Структура, которой принадлежит поле
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property ThePart() As BP3.Document.docrow_base
            Get
                LoadFromDatabase()
                ThePart = me.application.Findrowobject("bp3card_part",m_ThePart)
                AccessTime = Now
            End Get
            Set(ByVal Value As BP3.Document.docrow_base )
                LoadFromDatabase()
                if not Value is nothing then
                    m_ThePart = Value.id
                else
                   m_ThePart=System.Guid.Empty
                end if
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю Разрешена модификация
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property AllowModify() As enumBoolean
            Get
                LoadFromDatabase()
                AllowModify = m_AllowModify
                AccessTime = Now
            End Get
            Set(ByVal Value As enumBoolean )
                LoadFromDatabase()
                m_AllowModify = Value
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю Разрешен просмотр
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property AllowRead() As enumBoolean
            Get
                LoadFromDatabase()
                AllowRead = m_AllowRead
                AccessTime = Now
            End Get
            Set(ByVal Value As enumBoolean )
                LoadFromDatabase()
                m_AllowRead = Value
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю Имя вкладки
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property TabName() As String
            Get
                LoadFromDatabase()
                TabName = m_TabName
                AccessTime = Now
            End Get
            Set(ByVal Value As String )
                LoadFromDatabase()
                m_TabName = Value
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю Имя группы
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property FieldGroupBox() As String
            Get
                LoadFromDatabase()
                FieldGroupBox = m_FieldGroupBox
                AccessTime = Now
            End Get
            Set(ByVal Value As String )
                LoadFromDatabase()
                m_FieldGroupBox = Value
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
'''Заполнить поля данными из XML
''' </summary>
''' <remarks>
'''
''' </remarks>
        Protected Overrides sub XMLUnpack(ByVal node As System.Xml.XmlNode, Optional ByVal LoadMode As Integer = 0)
          Dim e_list As XmlNodeList
          on error resume next  
            Sequence = node.Attributes.GetNamedItem("Sequence").Value
            MandatoryField = node.Attributes.GetNamedItem("MandatoryField").Value
            m_TheField = new system.guid(node.Attributes.GetNamedItem("TheField").Value)
            m_ThePart = new system.guid(node.Attributes.GetNamedItem("ThePart").Value)
            AllowModify = node.Attributes.GetNamedItem("AllowModify").Value
            AllowRead = node.Attributes.GetNamedItem("AllowRead").Value
            TabName = node.Attributes.GetNamedItem("TabName").Value
            FieldGroupBox = node.Attributes.GetNamedItem("FieldGroupBox").Value
            TheStyle = node.Attributes.GetNamedItem("TheStyle").Value
            Caption = node.Attributes.GetNamedItem("Caption").Value
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
          node.SetAttribute("Sequence", Sequence)  
          node.SetAttribute("MandatoryField", MandatoryField)  
          node.SetAttribute("TheField", m_TheField.tostring)  
          node.SetAttribute("ThePart", m_ThePart.tostring)  
          node.SetAttribute("AllowModify", AllowModify)  
          node.SetAttribute("AllowRead", AllowRead)  
          node.SetAttribute("TabName", TabName)  
          node.SetAttribute("FieldGroupBox", FieldGroupBox)  
          node.SetAttribute("TheStyle", TheStyle)  
          node.SetAttribute("Caption", Caption)  
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
