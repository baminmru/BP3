

Option Explicit On

Imports LATIR2
Imports System
Imports System.xml
Imports System.Data

Namespace bp3app


''' <summary>
'''Реализация раздела Тип правав виде коллекции
''' </summary>
''' <remarks>
'''
''' </remarks>
    Public Class BP3APP_RIGTHTYPE_col
        Inherits LATIR2.Document.DocCollection_Base



''' <summary>
'''Имя раздела в базе данных
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Overrides Function ChildPartName() As String
            ChildPartName = "BP3APP_RIGTHTYPE"
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
            dt.Columns.Add("Name", Gettype(System.string))
            dt.Columns.Add("ListSortOrder", Gettype(System.Int32))
            dt.Columns.Add("ShortName", Gettype(System.string))
            dt.Columns.Add("selectType", Gettype(System.Int32))
            return dt
        End Function



''' <summary>
'''Создание нового элемента коллекции
''' </summary>
''' <remarks>
'''
''' </remarks>
        Protected Overrides Function NewItem() As LATIR2.Document.DocRow_Base
            NewItem = New BP3APP_RIGTHTYPE
        End Function


''' <summary>
'''Получить элемент коллекции
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Function GetItem( vIndex as object ) As bp3app.BP3APP_RIGTHTYPE
            on error resume next
            GetItem = Convert.ChangeType(mybase.Item(vIndex), GetType(bp3app.BP3APP_RIGTHTYPE))
        End Function


''' <summary>
'''Получить элемент коллекции, более свежий вариант
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Shadows Function Item( vIndex as object ) As bp3app.BP3APP_RIGTHTYPE
            on error resume next
            return GetItem(vIndex)
        End Function
Public Overrides Function FieldList() As String
    If mFieldList = "*" Then
       with application.Session.GetProvider
       mFieldList=.ID2Base("BP3APP_RIGTHTYPEID")
           mFieldList =mFieldList+","+.ID2Base("SecurityStyleID") 
           mFieldList =mFieldList+ ", name" 
           mFieldList =mFieldList+ ", listsortorder" 
           mFieldList =mFieldList+ ", shortname" 
           mFieldList =mFieldList+ ", selecttype" 
       end with
    End If
    Return mFieldList
End Function

    End Class
End Namespace
