

Option Explicit On

Imports BP3
Imports System
Imports System.xml
Imports System.Data

Namespace bp3list


''' <summary>
'''Реализация раздела Колонки журналав виде коллекции
''' </summary>
''' <remarks>
'''
''' </remarks>
    Public Class bp3list_col_col
        Inherits BP3.Document.DocCollection_Base



''' <summary>
'''Имя раздела в базе данных
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Overrides Function ChildPartName() As String
            ChildPartName = "bp3list_col"
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
            dt.Columns.Add("sequence", Gettype(System.Int32))
            dt.Columns.Add("name", Gettype(System.string))
            dt.Columns.Add("ViewField", Gettype(System.string))
            dt.Columns.Add("colwidth", Gettype(System.Int32))
            dt.Columns.Add("ColumnAlignment_VAL" , Gettype(System.Int16))
            dt.Columns.Add("ColumnAlignment", Gettype(System.string))
            dt.Columns.Add("ColSort_VAL" , Gettype(System.Int16))
            dt.Columns.Add("ColSort", Gettype(System.string))
            dt.Columns.Add("TheStyle", Gettype(System.string))
            dt.Columns.Add("GroupAggregation_VAL" , Gettype(System.Int16))
            dt.Columns.Add("GroupAggregation", Gettype(System.string))
            return dt
        End Function



''' <summary>
'''Создание нового элемента коллекции
''' </summary>
''' <remarks>
'''
''' </remarks>
        Protected Overrides Function NewItem() As BP3.Document.DocRow_Base
            NewItem = New bp3list_col
        End Function


''' <summary>
'''Получить элемент коллекции
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Function GetItem( vIndex as object ) As bp3list.bp3list_col
            on error resume next
            GetItem = Convert.ChangeType(mybase.Item(vIndex), GetType(bp3list.bp3list_col))
        End Function


''' <summary>
'''Получить элемент коллекции, более свежий вариант
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Shadows Function Item( vIndex as object ) As bp3list.bp3list_col
            on error resume next
            return GetItem(vIndex)
        End Function
Public Overrides Function FieldList() As String
    If mFieldList = "*" Then
       with application.Session.GetProvider
       mFieldList=.ID2Base("bp3list_colID")
           mFieldList =mFieldList+","+.ID2Base("SecurityStyleID") 
           mFieldList =mFieldList+ ", sequence" 
           mFieldList =mFieldList+ ", name" 
           mFieldList =mFieldList+ ", viewfield" 
           mFieldList =mFieldList+ ", colwidth" 
           mFieldList =mFieldList+ ", columnalignment" 
           mFieldList =mFieldList+ ", colsort" 
           mFieldList =mFieldList+ ", thestyle" 
           mFieldList =mFieldList+ ", groupaggregation" 
       end with
    End If
    Return mFieldList
End Function

    End Class
End Namespace
