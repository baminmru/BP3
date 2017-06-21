

Option Explicit On

Imports LATIR2
Imports System
Imports System.xml
Imports System.Data

Namespace bp3card


''' <summary>
'''Реализация раздела Разделв виде коллекции
''' </summary>
''' <remarks>
'''
''' </remarks>
    Public Class bp3card_part_col
        Inherits LATIR2.Document.DocCollection_Base



''' <summary>
'''Имя раздела в базе данных
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Overrides Function ChildPartName() As String
            ChildPartName = "bp3card_part"
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
            dt.Columns.Add("Struct_ID" , GetType(System.guid))
            dt.Columns.Add("Struct", Gettype(System.string))
            dt.Columns.Add("AllowAdd_VAL" , Gettype(System.Int16))
            dt.Columns.Add("AllowAdd", Gettype(System.string))
            dt.Columns.Add("AllowDelete_VAL" , Gettype(System.Int16))
            dt.Columns.Add("AllowDelete", Gettype(System.string))
            dt.Columns.Add("AllowRead_VAL" , Gettype(System.Int16))
            dt.Columns.Add("AllowRead", Gettype(System.string))
            dt.Columns.Add("AllowEdit_VAL" , Gettype(System.Int16))
            dt.Columns.Add("AllowEdit", Gettype(System.string))
            dt.Columns.Add("Sequence", Gettype(System.Int32))
            dt.Columns.Add("Caption", Gettype(System.string))
            return dt
        End Function



''' <summary>
'''Создание нового элемента коллекции
''' </summary>
''' <remarks>
'''
''' </remarks>
        Protected Overrides Function NewItem() As LATIR2.Document.DocRow_Base
            NewItem = New bp3card_part
        End Function


''' <summary>
'''Получить элемент коллекции
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Function GetItem( vIndex as object ) As bp3card.bp3card_part
            on error resume next
            GetItem = Convert.ChangeType(mybase.Item(vIndex), GetType(bp3card.bp3card_part))
        End Function


''' <summary>
'''Получить элемент коллекции, более свежий вариант
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Shadows Function Item( vIndex as object ) As bp3card.bp3card_part
            on error resume next
            return GetItem(vIndex)
        End Function
Public Overrides Function FieldList() As String
    If mFieldList = "*" Then
       with application.Session.GetProvider
       mFieldList=.ID2Base("bp3card_partID")
           mFieldList =mFieldList+","+.ID2Base("SecurityStyleID") 
           mFieldList =mFieldList+","+.ID2Base("Struct") 
           mFieldList =mFieldList+ ", allowadd" 
           mFieldList =mFieldList+ ", allowdelete" 
           mFieldList =mFieldList+ ", allowread" 
           mFieldList =mFieldList+ ", allowedit" 
           mFieldList =mFieldList+ ", sequence" 
           mFieldList =mFieldList+ ", caption" 
       end with
    End If
    Return mFieldList
End Function

    End Class
End Namespace
