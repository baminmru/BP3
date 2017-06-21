

Option Explicit On

Imports BP3
Imports System
Imports System.xml
Imports System.Data

Namespace bp3qry


''' <summary>
'''Реализация раздела Представлениев виде коллекции
''' </summary>
''' <remarks>
'''
''' </remarks>
    Public Class bp3qry_def_col
        Inherits BP3.Document.DocCollection_Base



''' <summary>
'''Имя раздела в базе данных
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Overrides Function ChildPartName() As String
            ChildPartName = "bp3qry_def"
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
            dt.Columns.Add("SrcPart_ID" , GetType(System.guid))
            dt.Columns.Add("SrcPart", Gettype(System.string))
            dt.Columns.Add("the_Alias", Gettype(System.string))
            dt.Columns.Add("ForChoose_VAL" , Gettype(System.Int16))
            dt.Columns.Add("ForChoose", Gettype(System.string))
            dt.Columns.Add("Name", Gettype(System.string))
            return dt
        End Function



''' <summary>
'''Создание нового элемента коллекции
''' </summary>
''' <remarks>
'''
''' </remarks>
        Protected Overrides Function NewItem() As BP3.Document.DocRow_Base
            NewItem = New bp3qry_def
        End Function


''' <summary>
'''Получить элемент коллекции
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Function GetItem( vIndex as object ) As bp3qry.bp3qry_def
            on error resume next
            GetItem = Convert.ChangeType(mybase.Item(vIndex), GetType(bp3qry.bp3qry_def))
        End Function


''' <summary>
'''Получить элемент коллекции, более свежий вариант
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Shadows Function Item( vIndex as object ) As bp3qry.bp3qry_def
            on error resume next
            return GetItem(vIndex)
        End Function
Public Overrides Function FieldList() As String
    If mFieldList = "*" Then
       with application.Session.GetProvider
       mFieldList=.ID2Base("bp3qry_defID")
           mFieldList =mFieldList+","+.ID2Base("SecurityStyleID") 
           mFieldList =mFieldList+","+.ID2Base("SrcPart") 
           mFieldList =mFieldList+ ", the_alias" 
           mFieldList =mFieldList+ ", forchoose" 
           mFieldList =mFieldList+ ", name" 
       end with
    End If
    Return mFieldList
End Function

    End Class
End Namespace
