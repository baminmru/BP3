

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
'''Реализация строки раздела Отображение
''' </summary>
''' <remarks>
'''
''' </remarks>
    Public Class bp3ft_map
        Inherits BP3.Document.DocRow_Base



''' <summary>
'''Локальная переменная для поля Размер
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_FixedSize  as long


''' <summary>
'''Локальная переменная для поля Тип хранения
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_StoageType  as String


''' <summary>
'''Локальная переменная для поля Платформа
''' </summary>
''' <remarks>
'''
''' </remarks>
            private m_Target  as System.Guid



''' <summary>
'''Очистить поля раздела
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Overrides Sub CleanFields()
            ' m_FixedSize=   
            ' m_StoageType=   
            ' m_Target=   
        End Sub



''' <summary>
'''Количество полей в строке раздела
''' </summary>
''' <remarks>
'''
''' </remarks>
    Public Overrides ReadOnly Property Count() As Long
        Get
           Count = 3
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
                    Value = FixedSize
                Case 2
                    Value = StoageType
                Case 3
                    Value = Target
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
                    FixedSize = value
                Case 2
                    StoageType = value
                Case 3
                    Target = value
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
                    Return "FixedSize"
                Case 2
                    Return "StoageType"
                Case 3
                    Return "Target"
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
             dr("FixedSize") =FixedSize
             dr("StoageType") =StoageType
             if Target is nothing then
               dr("Target") =system.dbnull.value
               dr("Target_ID") =System.Guid.Empty
             else
               dr("Target") =Target.BRIEF
               dr("Target_ID") =Target.ID
             end if 
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
          nv.Add("FixedSize", FixedSize, dbtype.Int32)
          nv.Add("StoageType", StoageType, dbtype.string)
          if m_Target.Equals(System.Guid.Empty) then
            nv.Add("Target", system.dbnull.value, Application.Session.GetProvider.ID2DbType, Application.Session.GetProvider.ID2Size)
          else
            nv.Add("Target", Application.Session.GetProvider.ID2Param(m_Target), Application.Session.GetProvider.ID2DbType, Application.Session.GetProvider.ID2Size)
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
          If reader.Table.Columns.Contains("FixedSize") Then m_FixedSize=reader.item("FixedSize")
          If reader.Table.Columns.Contains("StoageType") Then m_StoageType=reader.item("StoageType").ToString()
      If reader.Table.Columns.Contains("Target") Then
          if isdbnull(reader.item("Target")) then
            If reader.Table.Columns.Contains("Target") Then m_Target = System.GUID.Empty
          else
            If reader.Table.Columns.Contains("Target") Then m_Target= New System.Guid(reader.item("Target").ToString())
          end if 
      end if 
        End Sub


''' <summary>
'''Доступ к полю Размер
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property FixedSize() As long
            Get
                LoadFromDatabase()
                FixedSize = m_FixedSize
                AccessTime = Now
            End Get
            Set(ByVal Value As long )
                LoadFromDatabase()
                m_FixedSize = Value
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю Тип хранения
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property StoageType() As String
            Get
                LoadFromDatabase()
                StoageType = m_StoageType
                AccessTime = Now
            End Get
            Set(ByVal Value As String )
                LoadFromDatabase()
                m_StoageType = Value
                ChangeTime = Now
            End Set
        End Property


''' <summary>
'''Доступ к полю Платформа
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Property Target() As BP3.Document.docrow_base
            Get
                LoadFromDatabase()
                Target = me.application.Findrowobject("bp3dic_gen",m_Target)
                AccessTime = Now
            End Get
            Set(ByVal Value As BP3.Document.docrow_base )
                LoadFromDatabase()
                if not Value is nothing then
                    m_Target = Value.id
                else
                   m_Target=System.Guid.Empty
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
            FixedSize = node.Attributes.GetNamedItem("FixedSize").Value
            StoageType = node.Attributes.GetNamedItem("StoageType").Value
            m_Target = new system.guid(node.Attributes.GetNamedItem("Target").Value)
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
          node.SetAttribute("FixedSize", FixedSize)  
          node.SetAttribute("StoageType", StoageType)  
          node.SetAttribute("Target", m_Target.tostring)  
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
