

Option Explicit On

Imports BP3
Imports System
Imports System.xml
Imports System.Data

Namespace bp3list


''' <summary>
'''Реализация раздела Журналв виде коллекции
''' </summary>
''' <remarks>
'''
''' </remarks>
    Public Class bp3list_def_col
        Inherits BP3.Document.DocCollection_Base



''' <summary>
'''Имя раздела в базе данных
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Overrides Function ChildPartName() As String
            ChildPartName = "bp3list_def"
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
            dt.Columns.Add("TheComment", Gettype(System.string))
            dt.Columns.Add("SourceView_ID" , GetType(System.guid))
            dt.Columns.Add("SourceView", Gettype(System.string))
            dt.Columns.Add("OnRun_VAL" , Gettype(System.Int16))
            dt.Columns.Add("OnRun", Gettype(System.string))
            dt.Columns.Add("EditCard_ID" , GetType(System.guid))
            dt.Columns.Add("EditCard", Gettype(System.string))
            dt.Columns.Add("NewCard_ID" , GetType(System.guid))
            dt.Columns.Add("NewCard", Gettype(System.string))
            return dt
        End Function



''' <summary>
'''Создание нового элемента коллекции
''' </summary>
''' <remarks>
'''
''' </remarks>
        Protected Overrides Function NewItem() As BP3.Document.DocRow_Base
            NewItem = New bp3list_def
        End Function


''' <summary>
'''Получить элемент коллекции
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Function GetItem( vIndex as object ) As bp3list.bp3list_def
            on error resume next
            GetItem = Convert.ChangeType(mybase.Item(vIndex), GetType(bp3list.bp3list_def))
        End Function


''' <summary>
'''Получить элемент коллекции, более свежий вариант
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Shadows Function Item( vIndex as object ) As bp3list.bp3list_def
            on error resume next
            return GetItem(vIndex)
        End Function
Public Overrides Function FieldList() As String
    If mFieldList = "*" Then
       with application.Session.GetProvider
       mFieldList=.ID2Base("bp3list_defID")
           mFieldList =mFieldList+","+.ID2Base("SecurityStyleID") 
           mFieldList =mFieldList+ ", name" 
           mFieldList =mFieldList+ ", thecomment" 
           mFieldList =mFieldList+","+.ID2Base("SourceView") 
           mFieldList =mFieldList+ ", onrun" 
           mFieldList =mFieldList+","+.ID2Base("EditCard") 
           mFieldList =mFieldList+","+.ID2Base("NewCard") 
       end with
    End If
    Return mFieldList
End Function

    End Class
End Namespace
