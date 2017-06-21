

Option Explicit On

Imports LATIR2
Imports System
Imports System.xml
Imports System.Data

Namespace bp3card


''' <summary>
'''Реализация раздела Поля карточкив виде коллекции
''' </summary>
''' <remarks>
'''
''' </remarks>
    Public Class bp3card_fld_col
        Inherits LATIR2.Document.DocCollection_Base



''' <summary>
'''Имя раздела в базе данных
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Overrides Function ChildPartName() As String
            ChildPartName = "bp3card_fld"
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
            dt.Columns.Add("Sequence", Gettype(System.Int32))
            dt.Columns.Add("MandatoryField_VAL" , Gettype(System.Int16))
            dt.Columns.Add("MandatoryField", Gettype(System.string))
            dt.Columns.Add("TheField_ID" , GetType(System.guid))
            dt.Columns.Add("TheField", Gettype(System.string))
            dt.Columns.Add("ThePart_ID" , GetType(System.guid))
            dt.Columns.Add("ThePart", Gettype(System.string))
            dt.Columns.Add("AllowModify_VAL" , Gettype(System.Int16))
            dt.Columns.Add("AllowModify", Gettype(System.string))
            dt.Columns.Add("AllowRead_VAL" , Gettype(System.Int16))
            dt.Columns.Add("AllowRead", Gettype(System.string))
            dt.Columns.Add("TabName", Gettype(System.string))
            dt.Columns.Add("FieldGroupBox", Gettype(System.string))
            dt.Columns.Add("TheStyle", Gettype(System.string))
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
            NewItem = New bp3card_fld
        End Function


''' <summary>
'''Получить элемент коллекции
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Function GetItem( vIndex as object ) As bp3card.bp3card_fld
            on error resume next
            GetItem = Convert.ChangeType(mybase.Item(vIndex), GetType(bp3card.bp3card_fld))
        End Function


''' <summary>
'''Получить элемент коллекции, более свежий вариант
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Shadows Function Item( vIndex as object ) As bp3card.bp3card_fld
            on error resume next
            return GetItem(vIndex)
        End Function
Public Overrides Function FieldList() As String
    If mFieldList = "*" Then
       with application.Session.GetProvider
       mFieldList=.ID2Base("bp3card_fldID")
           mFieldList =mFieldList+","+.ID2Base("SecurityStyleID") 
           mFieldList =mFieldList+ ", sequence" 
           mFieldList =mFieldList+ ", mandatoryfield" 
           mFieldList =mFieldList+","+.ID2Base("TheField") 
           mFieldList =mFieldList+","+.ID2Base("ThePart") 
           mFieldList =mFieldList+ ", allowmodify" 
           mFieldList =mFieldList+ ", allowread" 
           mFieldList =mFieldList+ ", tabname" 
           mFieldList =mFieldList+ ", fieldgroupbox" 
           mFieldList =mFieldList+ ", thestyle" 
           mFieldList =mFieldList+ ", caption" 
       end with
    End If
    Return mFieldList
End Function

    End Class
End Namespace
