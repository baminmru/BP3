

Option Explicit On

Imports BP3
Imports System
Imports System.xml
Imports System.Data

Namespace bp3qry


''' <summary>
'''Реализация раздела Связанные представленияв виде коллекции
''' </summary>
''' <remarks>
'''
''' </remarks>
    Public Class bp3qry_link_col
        Inherits BP3.Document.DocCollection_Base



''' <summary>
'''Имя раздела в базе данных
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Overrides Function ChildPartName() As String
            ChildPartName = "bp3qry_link"
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
            dt.Columns.Add("TheJoinDestination_ID" , GetType(System.guid))
            dt.Columns.Add("TheJoinDestination", Gettype(System.string))
            dt.Columns.Add("HandJoin", Gettype(System.string))
            dt.Columns.Add("SEQ", Gettype(System.Int32))
            dt.Columns.Add("TheJoinSource_ID" , GetType(System.guid))
            dt.Columns.Add("TheJoinSource", Gettype(System.string))
            dt.Columns.Add("TheView_ID" , GetType(System.guid))
            dt.Columns.Add("TheView", Gettype(System.string))
            dt.Columns.Add("RefType_VAL" , Gettype(System.Int16))
            dt.Columns.Add("RefType", Gettype(System.string))
            return dt
        End Function



''' <summary>
'''Создание нового элемента коллекции
''' </summary>
''' <remarks>
'''
''' </remarks>
        Protected Overrides Function NewItem() As BP3.Document.DocRow_Base
            NewItem = New bp3qry_link
        End Function


''' <summary>
'''Получить элемент коллекции
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Function GetItem( vIndex as object ) As bp3qry.bp3qry_link
            on error resume next
            GetItem = Convert.ChangeType(mybase.Item(vIndex), GetType(bp3qry.bp3qry_link))
        End Function


''' <summary>
'''Получить элемент коллекции, более свежий вариант
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Shadows Function Item( vIndex as object ) As bp3qry.bp3qry_link
            on error resume next
            return GetItem(vIndex)
        End Function
Public Overrides Function FieldList() As String
    If mFieldList = "*" Then
       with application.Session.GetProvider
       mFieldList=.ID2Base("bp3qry_linkID")
           mFieldList =mFieldList+","+.ID2Base("SecurityStyleID") 
           mFieldList =mFieldList+","+.ID2Base("TheJoinDestination") 
           mFieldList =mFieldList+ ", handjoin" 
           mFieldList =mFieldList+ ", seq" 
           mFieldList =mFieldList+","+.ID2Base("TheJoinSource") 
           mFieldList =mFieldList+","+.ID2Base("TheView") 
           mFieldList =mFieldList+ ", reftype" 
       end with
    End If
    Return mFieldList
End Function

    End Class
End Namespace
