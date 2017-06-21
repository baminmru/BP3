

Option Explicit On

Imports BP3
Imports System
Imports System.xml
Imports System.Data

Namespace bp3ft


''' <summary>
'''Реализация раздела Тип поляв виде коллекции
''' </summary>
''' <remarks>
'''
''' </remarks>
    Public Class bp3ft_def_col
        Inherits BP3.Document.DocCollection_Base



''' <summary>
'''Имя раздела в базе данных
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Overrides Function ChildPartName() As String
            ChildPartName = "bp3ft_def"
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
            dt.Columns.Add("TypeStyle_VAL" , Gettype(System.Int16))
            dt.Columns.Add("TypeStyle", Gettype(System.string))
            dt.Columns.Add("Name", Gettype(System.string))
            dt.Columns.Add("GridSortType_VAL" , Gettype(System.Int16))
            dt.Columns.Add("GridSortType", Gettype(System.string))
            dt.Columns.Add("the_Comment", Gettype(System.string))
            dt.Columns.Add("AllowSize_VAL" , Gettype(System.Int16))
            dt.Columns.Add("AllowSize", Gettype(System.string))
            dt.Columns.Add("AllowLikeSearch_VAL" , Gettype(System.Int16))
            dt.Columns.Add("AllowLikeSearch", Gettype(System.string))
            dt.Columns.Add("Maximum", Gettype(System.string))
            dt.Columns.Add("Minimum", Gettype(System.string))
            dt.Columns.Add("DelayedSave_VAL" , Gettype(System.Int16))
            dt.Columns.Add("DelayedSave", Gettype(System.string))
            return dt
        End Function



''' <summary>
'''Создание нового элемента коллекции
''' </summary>
''' <remarks>
'''
''' </remarks>
        Protected Overrides Function NewItem() As BP3.Document.DocRow_Base
            NewItem = New bp3ft_def
        End Function


''' <summary>
'''Получить элемент коллекции
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Function GetItem( vIndex as object ) As bp3ft.bp3ft_def
            on error resume next
            GetItem = Convert.ChangeType(mybase.Item(vIndex), GetType(bp3ft.bp3ft_def))
        End Function


''' <summary>
'''Получить элемент коллекции, более свежий вариант
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Shadows Function Item( vIndex as object ) As bp3ft.bp3ft_def
            on error resume next
            return GetItem(vIndex)
        End Function
Public Overrides Function FieldList() As String
    If mFieldList = "*" Then
       with application.Session.GetProvider
       mFieldList=.ID2Base("bp3ft_defID")
           mFieldList =mFieldList+","+.ID2Base("SecurityStyleID") 
           mFieldList =mFieldList+ ", typestyle" 
           mFieldList =mFieldList+ ", name" 
           mFieldList =mFieldList+ ", gridsorttype" 
           mFieldList =mFieldList+ ", the_comment" 
           mFieldList =mFieldList+ ", allowsize" 
           mFieldList =mFieldList+ ", allowlikesearch" 
           mFieldList =mFieldList+ ", maximum" 
           mFieldList =mFieldList+ ", minimum" 
           mFieldList =mFieldList+ ", delayedsave" 
       end with
    End If
    Return mFieldList
End Function

    End Class
End Namespace
