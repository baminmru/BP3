

Option Explicit On

Imports System
Imports System.IO
Imports BP3
Imports System.Xml
Imports System.Data
Imports System.Convert
Imports System.DateTime

Namespace bp3doc


    ''' <summary>
    '''Реализация строки раздела Раздел
    ''' </summary>
    ''' <remarks>
    '''
    ''' </remarks>
    Public Class bp3doc_store
        Inherits BP3.Document.DocRow_Base



        ''' <summary>
        '''Локальная переменная для поля Заголовок
        ''' </summary>
        ''' <remarks>
        '''
        ''' </remarks>
        Private m_Caption As String


        ''' <summary>
        '''Локальная переменная для поля № п/п
        ''' </summary>
        ''' <remarks>
        '''
        ''' </remarks>
        Private m_Sequence As Long


        ''' <summary>
        '''Локальная переменная для поля Шаблон Бриеф
        ''' </summary>
        ''' <remarks>
        '''
        ''' </remarks>
        Private m_shablonBrief As String


        ''' <summary>
        '''Локальная переменная для поля Архивировать
        ''' </summary>
        ''' <remarks>
        '''
        ''' </remarks>
        Private m_UseArchiving As enumBoolean


        ''' <summary>
        '''Локальная переменная для поля Не  журналировать
        ''' </summary>
        ''' <remarks>
        '''
        ''' </remarks>
        Private m_NoLog As enumBoolean


        ''' <summary>
        '''Локальная переменная для поля Главный раздел
        ''' </summary>
        ''' <remarks>
        '''
        ''' </remarks>
        Private m_IsDocInstance As enumBoolean


        ''' <summary>
        '''Локальная переменная для поля Описание
        ''' </summary>
        ''' <remarks>
        '''
        ''' </remarks>
        Private m_the_Comment As String


        ''' <summary>
        '''Локальная переменная для поля Правило  BRIEF
        ''' </summary>
        ''' <remarks>
        '''
        ''' </remarks>
        Private m_ruleBrief As String


        ''' <summary>
        '''Локальная переменная для поля Тип структры
        ''' </summary>
        ''' <remarks>
        '''
        ''' </remarks>
        Private m_PartType As enumPartType


        ''' <summary>
        '''Локальная переменная для поля Название
        ''' </summary>
        ''' <remarks>
        '''
        ''' </remarks>
        Private m_Name As String


        ''' <summary>
        '''Локальная переменная для поля Вести журнал
        ''' </summary>
        ''' <remarks>
        '''
        ''' </remarks>
        Private m_UseChangeLog As enumBoolean


        ''' <summary>
        '''Локальная переменная для дочернего раздела Поле
        ''' </summary>
        ''' <remarks>
        '''
        ''' </remarks>
        Private m_bp3doc_field As bp3doc_field_col


        ''' <summary>
        '''Локальная переменная для дочерних записей раздела Раздел
        ''' </summary>
        ''' <remarks>
        '''
        ''' </remarks>
        Private m_bp3doc_store As bp3doc_store_col



        ''' <summary>
        '''Очистить поля раздела
        ''' </summary>
        ''' <remarks>
        '''
        ''' </remarks>
        Public Overrides Sub CleanFields()
            ' m_Caption=   
            ' m_Sequence=   
            ' m_shablonBrief=   
            ' m_UseArchiving=   
            ' m_NoLog=   
            ' m_IsDocInstance=   
            ' m_the_Comment=   
            ' m_ruleBrief=   
            ' m_PartType=   
            ' m_Name=   
            ' m_UseChangeLog=   
        End Sub



        ''' <summary>
        '''Количество полей в строке раздела
        ''' </summary>
        ''' <remarks>
        '''
        ''' </remarks>
        Public Overrides ReadOnly Property Count() As Long
            Get
                Count = 11
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
                            Value = Caption
                        Case 3
                            Value = PartType
                        Case 4
                            Value = Name
                        Case 5
                            Value = the_Comment
                        Case 6
                            Value = IsDocInstance
                        Case 7
                            Value = UseArchiving
                        Case 8
                            Value = NoLog
                        Case 9
                            Value = shablonBrief
                        Case 10
                            Value = UseChangeLog
                        Case 11
                            Value = ruleBrief
                    End Select
                Else
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
                            ID = value
                        Case 1
                            Sequence = value
                        Case 2
                            Caption = value
                        Case 3
                            PartType = value
                        Case 4
                            Name = value
                        Case 5
                            the_Comment = value
                        Case 6
                            IsDocInstance = value
                        Case 7
                            UseArchiving = value
                        Case 8
                            NoLog = value
                        Case 9
                            shablonBrief = value
                        Case 10
                            UseChangeLog = value
                        Case 11
                            ruleBrief = value
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
        Public Overrides Function FieldNameByID(ByVal Index As Long) As String
            If Microsoft.VisualBasic.IsNumeric(Index) Then
                Dim l As Long
                l = CLng(Index)
                Select Case l
                    Case 0
                        Return "ID"
                    Case 1
                        Return "Sequence"
                    Case 2
                        Return "Caption"
                    Case 3
                        Return "PartType"
                    Case 4
                        Return "Name"
                    Case 5
                        Return "the_Comment"
                    Case 6
                        Return "IsDocInstance"
                    Case 7
                        Return "UseArchiving"
                    Case 8
                        Return "NoLog"
                    Case 9
                        Return "shablonBrief"
                    Case 10
                        Return "UseChangeLog"
                    Case 11
                        Return "ruleBrief"
                    Case Else
                        Return ""
                End Select
            End If
            Return ""
        End Function



        ''' <summary>
        '''Заполнить строку таблицы данными из полей
        ''' </summary>
        ''' <remarks>
        '''
        ''' </remarks>
        Public Overrides Sub FillDataTable(ByRef DestDataTable As System.Data.DataTable)
            Dim dr As DataRow
            dr = destdatatable.NewRow
            On Error Resume Next
            dr("ID") = ID
            dr("Brief") = Brief
            dr("Sequence") = Sequence
            dr("Caption") = Caption
            Select Case PartType
                Case enumPartType.PartType_Kollekciy
                    dr("PartType") = "Коллекция"
                    dr("PartType_VAL") = 1
                Case enumPartType.PartType_Derevo
                    dr("PartType") = "Дерево"
                    dr("PartType_VAL") = 2
                Case enumPartType.PartType_Stroka
                    dr("PartType") = "Строка"
                    dr("PartType_VAL") = 0
                Case enumPartType.PartType_Rassirenie_s_dannimi
                    dr("PartType") = "Расширение с данными"
                    dr("PartType_VAL") = 4
                Case enumPartType.PartType_Rassirenie
                    dr("PartType") = "Расширение"
                    dr("PartType_VAL") = 3
            End Select 'PartType
            dr("Name") = Name
            dr("the_Comment") = the_Comment
            Select Case IsDocInstance
                Case enumBoolean.Boolean_Da
                    dr("IsDocInstance") = "Да"
                    dr("IsDocInstance_VAL") = -1
                Case enumBoolean.Boolean_Net
                    dr("IsDocInstance") = "Нет"
                    dr("IsDocInstance_VAL") = 0
            End Select 'IsDocInstance
            Select Case UseArchiving
                Case enumBoolean.Boolean_Da
                    dr("UseArchiving") = "Да"
                    dr("UseArchiving_VAL") = -1
                Case enumBoolean.Boolean_Net
                    dr("UseArchiving") = "Нет"
                    dr("UseArchiving_VAL") = 0
            End Select 'UseArchiving
            Select Case NoLog
                Case enumBoolean.Boolean_Da
                    dr("NoLog") = "Да"
                    dr("NoLog_VAL") = -1
                Case enumBoolean.Boolean_Net
                    dr("NoLog") = "Нет"
                    dr("NoLog_VAL") = 0
            End Select 'NoLog
            dr("shablonBrief") = shablonBrief
            Select Case UseChangeLog
                Case enumBoolean.Boolean_Da
                    dr("UseChangeLog") = "Да"
                    dr("UseChangeLog_VAL") = -1
                Case enumBoolean.Boolean_Net
                    dr("UseChangeLog") = "Нет"
                    dr("UseChangeLog_VAL") = 0
            End Select 'UseChangeLog
            dr("ruleBrief") = ruleBrief
            DestDataTable.Rows.Add(dr)
        End Sub



        ''' <summary>
        '''Найти строку в коллекции по идентификатору
        ''' </summary>
        ''' <remarks>
        '''
        ''' </remarks>
        Public Overrides Function FindInside(ByVal Table As String, ByVal RowID As String) As BP3.Document.DocRow_Base
            Dim mFindInside As BP3.Document.DocRow_Base = Nothing
            mFindInside = bp3doc_store.FindObject(table, RowID)
            If Not mFindInside Is Nothing Then Return mFindInside
            mFindInside = bp3doc_field.FindObject(table, RowID)
            If Not mFindInside Is Nothing Then Return mFindInside
            Return Nothing
        End Function



        ''' <summary>
        '''Заполнить коллекцю именованных полей
        ''' </summary>
        ''' <remarks>
        '''
        ''' </remarks>
        Public Overrides Sub Pack(ByVal nv As BP3.NamedValues)
            If Me.Parent.Parent.GetType.name = Me.GetType.name Then
                nv.Add("ParentRowID", Application.Session.GetProvider.ID2Param(Me.Parent.Parent.ID), Application.Session.GetProvider.ID2DbType, Application.Session.GetProvider.ID2Size)
            Else
                nv.Add("ParentRowID", system.dbnull.value, Application.Session.GetProvider.ID2DbType, Application.Session.GetProvider.ID2Size)
            End If
            nv.Add("Sequence", Sequence, dbtype.Int32)
            nv.Add("Caption", Caption, dbtype.string)
            nv.Add("PartType", PartType, dbtype.int16)
            nv.Add("Name", Name, dbtype.string)
            nv.Add("the_Comment", the_Comment, dbtype.string)
            nv.Add("IsDocInstance", IsDocInstance, dbtype.int16)
            nv.Add("UseArchiving", UseArchiving, dbtype.int16)
            nv.Add("NoLog", NoLog, dbtype.int16)
            nv.Add("shablonBrief", shablonBrief, dbtype.string)
            nv.Add("UseChangeLog", UseChangeLog, dbtype.int16)
            nv.Add("ruleBrief", ruleBrief, dbtype.string)
            nv.Add(PartName() & "id", Application.Session.GetProvider.ID2Param(ID), Application.Session.GetProvider.ID2DbType, Application.Session.GetProvider.ID2Size)
        End Sub




        ''' <summary>
        '''Заполнить поля из именованной коллекции
        ''' </summary>
        ''' <remarks>
        '''
        ''' </remarks>
        Public Overrides Sub Unpack(ByVal reader As System.Data.DataRow)
            On Error Resume Next
            If IsDBNull(reader.item("SecurityStyleID")) Then
                SecureStyleID = System.guid.Empty
            Else
                SecureStyleID = New Guid(reader.item("SecurityStyleID").ToString())
            End If

            RowRetrived = True
            RetriveTime = Now
            If reader.Table.Columns.Contains("Sequence") Then m_Sequence = reader.item("Sequence")
            If reader.Table.Columns.Contains("Caption") Then m_Caption = reader.item("Caption").ToString()
            If reader.Table.Columns.Contains("PartType") Then m_PartType = reader.item("PartType")
            If reader.Table.Columns.Contains("Name") Then m_Name = reader.item("Name").ToString()
            If reader.Table.Columns.Contains("the_Comment") Then m_the_Comment = reader.item("the_Comment").ToString()
            If reader.Table.Columns.Contains("IsDocInstance") Then m_IsDocInstance = reader.item("IsDocInstance")
            If reader.Table.Columns.Contains("UseArchiving") Then m_UseArchiving = reader.item("UseArchiving")
            If reader.Table.Columns.Contains("NoLog") Then m_NoLog = reader.item("NoLog")
            If reader.Table.Columns.Contains("shablonBrief") Then m_shablonBrief = reader.item("shablonBrief").ToString()
            If reader.Table.Columns.Contains("UseChangeLog") Then m_UseChangeLog = reader.item("UseChangeLog")
            If reader.Table.Columns.Contains("ruleBrief") Then m_ruleBrief = reader.item("ruleBrief").ToString()
        End Sub


        ''' <summary>
        '''Доступ к полю № п/п
        ''' </summary>
        ''' <remarks>
        '''
        ''' </remarks>
        Public Property Sequence() As Long
            Get
                LoadFromDatabase()
                Sequence = m_Sequence
                AccessTime = Now
            End Get
            Set(ByVal Value As Long)
                LoadFromDatabase()
                m_Sequence = Value
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
            Set(ByVal Value As String)
                LoadFromDatabase()
                m_Caption = Value
                ChangeTime = Now
            End Set
        End Property


        ''' <summary>
        '''Доступ к полю Тип структры
        ''' </summary>
        ''' <remarks>
        '''
        ''' </remarks>
        Public Property PartType() As enumPartType
            Get
                LoadFromDatabase()
                PartType = m_PartType
                AccessTime = Now
            End Get
            Set(ByVal Value As enumPartType)
                LoadFromDatabase()
                m_PartType = Value
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
            Set(ByVal Value As String)
                LoadFromDatabase()
                m_Name = Value
                ChangeTime = Now
            End Set
        End Property


        ''' <summary>
        '''Доступ к полю Описание
        ''' </summary>
        ''' <remarks>
        '''
        ''' </remarks>
        Public Property the_Comment() As String
            Get
                LoadFromDatabase()
                the_Comment = m_the_Comment
                AccessTime = Now
            End Get
            Set(ByVal Value As String)
                LoadFromDatabase()
                m_the_Comment = Value
                ChangeTime = Now
            End Set
        End Property


        ''' <summary>
        '''Доступ к полю Главный раздел
        ''' </summary>
        ''' <remarks>
        '''
        ''' </remarks>
        Public Property IsDocInstance() As enumBoolean
            Get
                LoadFromDatabase()
                IsDocInstance = m_IsDocInstance
                AccessTime = Now
            End Get
            Set(ByVal Value As enumBoolean)
                LoadFromDatabase()
                m_IsDocInstance = Value
                ChangeTime = Now
            End Set
        End Property


        ''' <summary>
        '''Доступ к полю Архивировать
        ''' </summary>
        ''' <remarks>
        '''
        ''' </remarks>
        Public Property UseArchiving() As enumBoolean
            Get
                LoadFromDatabase()
                UseArchiving = m_UseArchiving
                AccessTime = Now
            End Get
            Set(ByVal Value As enumBoolean)
                LoadFromDatabase()
                m_UseArchiving = Value
                ChangeTime = Now
            End Set
        End Property


        ''' <summary>
        '''Доступ к полю Не  журналировать
        ''' </summary>
        ''' <remarks>
        '''
        ''' </remarks>
        Public Property NoLog() As enumBoolean
            Get
                LoadFromDatabase()
                NoLog = m_NoLog
                AccessTime = Now
            End Get
            Set(ByVal Value As enumBoolean)
                LoadFromDatabase()
                m_NoLog = Value
                ChangeTime = Now
            End Set
        End Property


        ''' <summary>
        '''Доступ к полю Шаблон Бриеф
        ''' </summary>
        ''' <remarks>
        '''
        ''' </remarks>
        Public Property shablonBrief() As String
            Get
                LoadFromDatabase()
                shablonBrief = m_shablonBrief
                AccessTime = Now
            End Get
            Set(ByVal Value As String)
                LoadFromDatabase()
                m_shablonBrief = Value
                ChangeTime = Now
            End Set
        End Property


        ''' <summary>
        '''Доступ к полю Вести журнал
        ''' </summary>
        ''' <remarks>
        '''
        ''' </remarks>
        Public Property UseChangeLog() As enumBoolean
            Get
                LoadFromDatabase()
                UseChangeLog = m_UseChangeLog
                AccessTime = Now
            End Get
            Set(ByVal Value As enumBoolean)
                LoadFromDatabase()
                m_UseChangeLog = Value
                ChangeTime = Now
            End Set
        End Property


        ''' <summary>
        '''Доступ к полю Правило  BRIEF
        ''' </summary>
        ''' <remarks>
        '''
        ''' </remarks>
        Public Property ruleBrief() As String
            Get
                LoadFromDatabase()
                ruleBrief = m_ruleBrief
                AccessTime = Now
            End Get
            Set(ByVal Value As String)
                LoadFromDatabase()
                m_ruleBrief = Value
                ChangeTime = Now
            End Set
        End Property


        ''' <summary>
        '''Доступ к дочернему разделу Поле
        ''' </summary>
        ''' <remarks>
        '''
        ''' </remarks>
        Public ReadOnly Property bp3doc_field() As bp3doc_field_col
            Get
                If m_bp3doc_field Is Nothing Then
                    m_bp3doc_field = New bp3doc_field_col
                    m_bp3doc_field.Parent = Me
                    m_bp3doc_field.Application = Me.Application
                    m_bp3doc_field.Refresh()
                End If
                bp3doc_field = m_bp3doc_field
                AccessTime = Now
            End Get
        End Property


        ''' <summary>
        '''Доступ к дочернему разделу Раздел
        ''' </summary>
        ''' <remarks>
        '''
        ''' </remarks>
        Public ReadOnly Property bp3doc_store() As bp3doc_store_col
            Get
                If m_bp3doc_store Is Nothing Then
                    m_bp3doc_store = New bp3doc_store_col
                    m_bp3doc_store.Parent = Me
                    m_bp3doc_store.Application = Me.Application
                    m_bp3doc_store.Refresh()
                End If
                bp3doc_store = m_bp3doc_store
                AccessTime = Now
            End Get
        End Property


        ''' <summary>
        '''Заполнить поля данными из XML
        ''' </summary>
        ''' <remarks>
        '''
        ''' </remarks>
        Protected Overrides Sub XMLUnpack(ByVal node As System.Xml.XmlNode, Optional ByVal LoadMode As Integer = 0)
            Dim e_list As XmlNodeList
            On Error Resume Next
            Sequence = node.Attributes.GetNamedItem("Sequence").Value
            Caption = node.Attributes.GetNamedItem("Caption").Value
            PartType = node.Attributes.GetNamedItem("PartType").Value
            Name = node.Attributes.GetNamedItem("Name").Value
            the_Comment = node.Attributes.GetNamedItem("the_Comment").Value
            IsDocInstance = node.Attributes.GetNamedItem("IsDocInstance").Value
            UseArchiving = node.Attributes.GetNamedItem("UseArchiving").Value
            NoLog = node.Attributes.GetNamedItem("NoLog").Value
            shablonBrief = node.Attributes.GetNamedItem("shablonBrief").Value
            UseChangeLog = node.Attributes.GetNamedItem("UseChangeLog").Value
            ruleBrief = node.Attributes.GetNamedItem("ruleBrief").Value
            e_list = node.SelectNodes("bp3doc_store_COL")
            bp3doc_store.XMLLoad(e_list, LoadMode)
            e_list = node.SelectNodes("bp3doc_field_COL")
            bp3doc_field.XMLLoad(e_list, LoadMode)
            Changed = True
        End Sub
        Public Overrides Sub Dispose()
            bp3doc_field.Dispose()
        End Sub


        ''' <summary>
        '''Записать данные раздела в XML файл
        ''' </summary>
        ''' <remarks>
        '''
        ''' </remarks>
        Protected Overrides Sub XLMPack(ByVal node As System.Xml.XmlElement, ByVal Xdom As System.Xml.XmlDocument)
            On Error Resume Next
            node.SetAttribute("Sequence", Sequence)
            node.SetAttribute("Caption", Caption)
            node.SetAttribute("PartType", PartType)
            node.SetAttribute("Name", Name)
            node.SetAttribute("the_Comment", the_Comment)
            node.SetAttribute("IsDocInstance", IsDocInstance)
            node.SetAttribute("UseArchiving", UseArchiving)
            node.SetAttribute("NoLog", NoLog)
            node.SetAttribute("shablonBrief", shablonBrief)
            node.SetAttribute("UseChangeLog", UseChangeLog)
            node.SetAttribute("ruleBrief", ruleBrief)
            bp3doc_store.XMLSave(node, xdom)
            bp3doc_field.XMLSave(node, xdom)
        End Sub


        ''' <summary>
        '''Записать изменения в базу данных
        ''' </summary>
        ''' <remarks>
        '''
        ''' </remarks>
        Public Overrides Sub BatchUpdate()
            If Deleted Then
                Delete()
                Exit Sub
            End If
            If Changed Then Save()
            bp3doc_store.BatchUpdate()
            bp3doc_field.BatchUpdate()
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
                    Return bp3doc_field
            End Select
            Return Nothing
        End Function
    End Class
End Namespace
