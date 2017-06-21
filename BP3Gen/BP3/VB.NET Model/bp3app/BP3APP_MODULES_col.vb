

Option Explicit On

Imports LATIR2
Imports System
Imports System.xml
Imports System.Data

Namespace bp3app


''' <summary>
'''Реализация раздела Модулив виде коллекции
''' </summary>
''' <remarks>
'''
''' </remarks>
    Public Class BP3APP_MODULES_col
        Inherits LATIR2.Document.DocCollection_Base



''' <summary>
'''Имя раздела в базе данных
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Overrides Function ChildPartName() As String
            ChildPartName = "BP3APP_MODULES"
        End Function



''' <summary>
'''Вернуть даные текущей коллекции в виде DataTable
''' </summary>
''' <remarks>
'''
''' </remarks>
        Protected Overrides Function CreateDataTable() As System.Data.DataTable
            Dim dt As DataTable
            dt = New DataTable
            dt.Columns.Add("ID", GetType(System.guid))
            dt.Columns.Add("Brief", Gettype(System.string))
            dt.Columns.Add("TopMenu_ID" , GetType(System.guid))
            dt.Columns.Add("TopMenu", Gettype(System.string))
            dt.Columns.Add("GroupName", Gettype(System.string))
            dt.Columns.Add("Caption", Gettype(System.string))
            dt.Columns.Add("Name", Gettype(System.string))
            dt.Columns.Add("Sequence", Gettype(System.Int32))
            dt.Columns.Add("TheComment", Gettype(System.string))
            dt.Columns.Add("TheIcon", Gettype(System.string))
            dt.Columns.Add("CustomizeVisibility_VAL" , Gettype(System.Int16))
            dt.Columns.Add("CustomizeVisibility", Gettype(System.string))
            dt.Columns.Add("Journal_ID" , GetType(System.guid))
            dt.Columns.Add("Journal", Gettype(System.string))
            dt.Columns.Add("Document", Gettype(System.string))
            dt.Columns.Add("ActionType_VAL" , Gettype(System.Int16))
            dt.Columns.Add("ActionType", Gettype(System.string))
            dt.Columns.Add("ObjectType_ID" , GetType(System.guid))
            dt.Columns.Add("ObjectType", Gettype(System.string))
            dt.Columns.Add("Report_ID" , GetType(System.guid))
            dt.Columns.Add("Report", Gettype(System.string))
            return dt
        End Function



''' <summary>
'''Создание нового элемента коллекции
''' </summary>
''' <remarks>
'''
''' </remarks>
        Protected Overrides Function NewItem() As LATIR2.Document.DocRow_Base
            NewItem = New BP3APP_MODULES
        End Function


''' <summary>
'''Получить элемент коллекции
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Function GetItem( vIndex as object ) As bp3app.BP3APP_MODULES
            on error resume next
            GetItem = Convert.ChangeType(mybase.Item(vIndex), GetType(bp3app.BP3APP_MODULES))
        End Function


''' <summary>
'''Получить элемент коллекции, более свежий вариант
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Shadows Function Item( vIndex as object ) As bp3app.BP3APP_MODULES
            on error resume next
            return GetItem(vIndex)
        End Function
Public Overrides Function FieldList() As String
    If mFieldList = "*" Then
       with application.Session.GetProvider
       mFieldList=.ID2Base("BP3APP_MODULESID")
           mFieldList =mFieldList+","+.ID2Base("SecurityStyleID") 
           mFieldList =mFieldList+","+.ID2Base("TopMenu") 
           mFieldList =mFieldList+ ", groupname" 
           mFieldList =mFieldList+ ", caption" 
           mFieldList =mFieldList+ ", name" 
           mFieldList =mFieldList+ ", sequence" 
           mFieldList =mFieldList+ ", thecomment" 
           mFieldList =mFieldList+ ", theicon" 
           mFieldList =mFieldList+ ", customizevisibility" 
           mFieldList =mFieldList+","+.ID2Base("Journal") 
           mFieldList =mFieldList+ ", document" 
           mFieldList =mFieldList+ ", actiontype" 
           mFieldList =mFieldList+","+.ID2Base("ObjectType") 
           mFieldList =mFieldList+","+.ID2Base("Report") 
       end with
    End If
    Return mFieldList
End Function

    End Class
End Namespace
