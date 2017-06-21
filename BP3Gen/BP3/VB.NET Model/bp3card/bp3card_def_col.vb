

Option Explicit On

Imports BP3
Imports System
Imports System.xml
Imports System.Data

Namespace bp3card


''' <summary>
'''Реализация раздела Карточка документав виде коллекции
''' </summary>
''' <remarks>
'''
''' </remarks>
    Public Class bp3card_def_col
        Inherits BP3.Document.DocCollection_Base



''' <summary>
'''Имя раздела в базе данных
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Overrides Function ChildPartName() As String
            ChildPartName = "bp3card_def"
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
            dt.Columns.Add("CardFor_ID" , GetType(System.guid))
            dt.Columns.Add("CardFor", Gettype(System.string))
            dt.Columns.Add("TheComment", Gettype(System.string))
            dt.Columns.Add("Name", Gettype(System.string))
            dt.Columns.Add("DefaultMode_VAL" , Gettype(System.Int16))
            dt.Columns.Add("DefaultMode", Gettype(System.string))
            dt.Columns.Add("cardiconcls", Gettype(System.string))
            return dt
        End Function



''' <summary>
'''Создание нового элемента коллекции
''' </summary>
''' <remarks>
'''
''' </remarks>
        Protected Overrides Function NewItem() As BP3.Document.DocRow_Base
            NewItem = New bp3card_def
        End Function


''' <summary>
'''Получить элемент коллекции
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Function GetItem( vIndex as object ) As bp3card.bp3card_def
            on error resume next
            GetItem = Convert.ChangeType(mybase.Item(vIndex), GetType(bp3card.bp3card_def))
        End Function


''' <summary>
'''Получить элемент коллекции, более свежий вариант
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Shadows Function Item( vIndex as object ) As bp3card.bp3card_def
            on error resume next
            return GetItem(vIndex)
        End Function
Public Overrides Function FieldList() As String
    If mFieldList = "*" Then
       with application.Session.GetProvider
       mFieldList=.ID2Base("bp3card_defID")
           mFieldList =mFieldList+","+.ID2Base("SecurityStyleID") 
           mFieldList =mFieldList+","+.ID2Base("CardFor") 
           mFieldList =mFieldList+ ", thecomment" 
           mFieldList =mFieldList+ ", name" 
           mFieldList =mFieldList+ ", defaultmode" 
           mFieldList =mFieldList+ ", cardiconcls" 
       end with
    End If
    Return mFieldList
End Function

    End Class
End Namespace
