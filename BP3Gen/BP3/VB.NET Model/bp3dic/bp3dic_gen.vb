

Option Explicit On

Imports System
Imports System.IO
Imports BP3
Imports System.xml
Imports System.Data
Imports System.Convert
Imports System.DateTime

Namespace bp3dic


    ''' <summary>
    '''Реализация строки раздела Генераторы
    ''' </summary>
    ''' <remarks>
    '''
    ''' </remarks>
    Public Class bp3dic_gen
        Inherits BP3.Document.DocRow_Base



        ''' <summary>
        '''Локальная переменная для поля Название
        ''' </summary>
        ''' <remarks>
        '''
        ''' </remarks>
        Private m_Name As String


        ''' <summary>
        '''Локальная переменная для поля Вариант
        ''' </summary>
        ''' <remarks>
        '''
        ''' </remarks>
        Private m_GeneratorStyle As enumGeneratorStyle


        ''' <summary>
        '''Локальная переменная для поля Очередь
        ''' </summary>
        ''' <remarks>
        '''
        ''' </remarks>
        Private m_QueueName As String


        ''' <summary>
        '''Локальная переменная для поля Среда разработки
        ''' </summary>
        ''' <remarks>
        '''
        ''' </remarks>
        Private m_TheDevelopmentEnv As enumDevelopmentBase


        ''' <summary>
        '''Локальная переменная для поля COM класс
        ''' </summary>
        ''' <remarks>
        '''
        ''' </remarks>
        Private m_GeneratorProgID As String


        ''' <summary>
        '''Локальная переменная для поля Тип платформы
        ''' </summary>
        ''' <remarks>
        '''
        ''' </remarks>
        Private m_TargetType As enumTargetType



        ''' <summary>
        '''Очистить поля раздела
        ''' </summary>
        ''' <remarks>
        '''
        ''' </remarks>
        Public Overrides Sub CleanFields()
            ' m_Name=   
            ' m_GeneratorStyle=   
            ' m_QueueName=   
            ' m_TheDevelopmentEnv=   
            ' m_GeneratorProgID=   
            ' m_TargetType=   
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
                            Value = Name
                        Case 2
                            Value = GeneratorStyle
                        Case 3
                            Value = QueueName
                        Case 4
                            Value = TheDevelopmentEnv
                        Case 5
                            Value = GeneratorProgID
                        Case 6
                            Value = TargetType
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
                            Name = value
                        Case 2
                            GeneratorStyle = value
                        Case 3
                            QueueName = value
                        Case 4
                            TheDevelopmentEnv = value
                        Case 5
                            GeneratorProgID = value
                        Case 6
                            TargetType = value
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
                        Return "Name"
                    Case 2
                        Return "GeneratorStyle"
                    Case 3
                        Return "QueueName"
                    Case 4
                        Return "TheDevelopmentEnv"
                    Case 5
                        Return "GeneratorProgID"
                    Case 6
                        Return "TargetType"
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
            dr("Name") = Name
            Select Case GeneratorStyle
                Case enumGeneratorStyle.GeneratorStyle_Odin_tip
                    dr("GeneratorStyle") = "Один тип"
                    dr("GeneratorStyle_VAL") = 0
                Case enumGeneratorStyle.GeneratorStyle_Vse_tipi_srazu
                    dr("GeneratorStyle") = "Все типы сразу"
                    dr("GeneratorStyle_VAL") = 1
            End Select 'GeneratorStyle
            dr("QueueName") = QueueName
            Select Case TheDevelopmentEnv
                Case enumDevelopmentBase.DevelopmentBase_OTHER
                    dr("TheDevelopmentEnv") = "OTHER"
                    dr("TheDevelopmentEnv_VAL") = 3
                Case enumDevelopmentBase.DevelopmentBase_DOTNET
                    dr("TheDevelopmentEnv") = "DOTNET"
                    dr("TheDevelopmentEnv_VAL") = 1
                Case enumDevelopmentBase.DevelopmentBase_JAVA
                    dr("TheDevelopmentEnv") = "JAVA"
                    dr("TheDevelopmentEnv_VAL") = 2
                Case enumDevelopmentBase.DevelopmentBase_VB6
                    dr("TheDevelopmentEnv") = "VB6"
                    dr("TheDevelopmentEnv_VAL") = 0
            End Select 'TheDevelopmentEnv
            dr("GeneratorProgID") = GeneratorProgID
            Select Case TargetType
                Case enumTargetType.TargetType_SUBD
                    dr("TargetType") = "СУБД"
                    dr("TargetType_VAL") = 0
                Case enumTargetType.TargetType_Dokumentaciy
                    dr("TargetType") = "Документация"
                    dr("TargetType_VAL") = 3
                Case enumTargetType.TargetType_MODEL_
                    dr("TargetType") = "МОДЕЛЬ"
                    dr("TargetType_VAL") = 1
                Case enumTargetType.TargetType_Prilogenie
                    dr("TargetType") = "Приложение"
                    dr("TargetType_VAL") = 2
                Case enumTargetType.TargetType_ARM
                    dr("TargetType") = "АРМ"
                    dr("TargetType_VAL") = 4
            End Select 'TargetType
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
            Return Nothing
        End Function



        ''' <summary>
        '''Заполнить коллекцю именованных полей
        ''' </summary>
        ''' <remarks>
        '''
        ''' </remarks>
        Public Overrides Sub Pack(ByVal nv As BP3.NamedValues)
            nv.Add("Name", Name, dbtype.string)
            nv.Add("GeneratorStyle", GeneratorStyle, dbtype.int16)
            nv.Add("QueueName", QueueName, dbtype.string)
            nv.Add("TheDevelopmentEnv", TheDevelopmentEnv, dbtype.int16)
            nv.Add("GeneratorProgID", GeneratorProgID, dbtype.string)
            nv.Add("TargetType", TargetType, dbtype.int16)
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
            If reader.Table.Columns.Contains("Name") Then m_Name = reader.item("Name").ToString()
            If reader.Table.Columns.Contains("GeneratorStyle") Then m_GeneratorStyle = reader.item("GeneratorStyle")
            If reader.Table.Columns.Contains("QueueName") Then m_QueueName = reader.item("QueueName").ToString()
            If reader.Table.Columns.Contains("TheDevelopmentEnv") Then m_TheDevelopmentEnv = reader.item("TheDevelopmentEnv")
            If reader.Table.Columns.Contains("GeneratorProgID") Then m_GeneratorProgID = reader.item("GeneratorProgID").ToString()
            If reader.Table.Columns.Contains("TargetType") Then m_TargetType = reader.item("TargetType")
        End Sub


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
        '''Доступ к полю Вариант
        ''' </summary>
        ''' <remarks>
        '''
        ''' </remarks>
        Public Property GeneratorStyle() As enumGeneratorStyle
            Get
                LoadFromDatabase()
                GeneratorStyle = m_GeneratorStyle
                AccessTime = Now
            End Get
            Set(ByVal Value As enumGeneratorStyle)
                LoadFromDatabase()
                m_GeneratorStyle = Value
                ChangeTime = Now
            End Set
        End Property


        ''' <summary>
        '''Доступ к полю Очередь
        ''' </summary>
        ''' <remarks>
        '''
        ''' </remarks>
        Public Property QueueName() As String
            Get
                LoadFromDatabase()
                QueueName = m_QueueName
                AccessTime = Now
            End Get
            Set(ByVal Value As String)
                LoadFromDatabase()
                m_QueueName = Value
                ChangeTime = Now
            End Set
        End Property


        ''' <summary>
        '''Доступ к полю Среда разработки
        ''' </summary>
        ''' <remarks>
        '''
        ''' </remarks>
        Public Property TheDevelopmentEnv() As enumDevelopmentBase
            Get
                LoadFromDatabase()
                TheDevelopmentEnv = m_TheDevelopmentEnv
                AccessTime = Now
            End Get
            Set(ByVal Value As enumDevelopmentBase)
                LoadFromDatabase()
                m_TheDevelopmentEnv = Value
                ChangeTime = Now
            End Set
        End Property


        ''' <summary>
        '''Доступ к полю COM класс
        ''' </summary>
        ''' <remarks>
        '''
        ''' </remarks>
        Public Property GeneratorProgID() As String
            Get
                LoadFromDatabase()
                GeneratorProgID = m_GeneratorProgID
                AccessTime = Now
            End Get
            Set(ByVal Value As String)
                LoadFromDatabase()
                m_GeneratorProgID = Value
                ChangeTime = Now
            End Set
        End Property


        ''' <summary>
        '''Доступ к полю Тип платформы
        ''' </summary>
        ''' <remarks>
        '''
        ''' </remarks>
        Public Property TargetType() As enumTargetType
            Get
                LoadFromDatabase()
                TargetType = m_TargetType
                AccessTime = Now
            End Get
            Set(ByVal Value As enumTargetType)
                LoadFromDatabase()
                m_TargetType = Value
                ChangeTime = Now
            End Set
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
            Name = node.Attributes.GetNamedItem("Name").Value
            GeneratorStyle = node.Attributes.GetNamedItem("GeneratorStyle").Value
            QueueName = node.Attributes.GetNamedItem("QueueName").Value
            TheDevelopmentEnv = node.Attributes.GetNamedItem("TheDevelopmentEnv").Value
            GeneratorProgID = node.Attributes.GetNamedItem("GeneratorProgID").Value
            TargetType = node.Attributes.GetNamedItem("TargetType").Value
            Changed = True
        End Sub
        Public Overrides Sub Dispose()
        End Sub


        ''' <summary>
        '''Записать данные раздела в XML файл
        ''' </summary>
        ''' <remarks>
        '''
        ''' </remarks>
        Protected Overrides Sub XLMPack(ByVal node As System.Xml.XmlElement, ByVal Xdom As System.Xml.XmlDocument)
            On Error Resume Next
            node.SetAttribute("Name", Name)
            node.SetAttribute("GeneratorStyle", GeneratorStyle)
            node.SetAttribute("QueueName", QueueName)
            node.SetAttribute("TheDevelopmentEnv", TheDevelopmentEnv)
            node.SetAttribute("GeneratorProgID", GeneratorProgID)
            node.SetAttribute("TargetType", TargetType)
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
            Return Nothing
        End Function
    End Class
End Namespace
