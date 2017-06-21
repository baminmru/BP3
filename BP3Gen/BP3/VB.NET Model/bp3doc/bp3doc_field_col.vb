

Option Explicit On

Imports BP3
Imports System
Imports System.xml
Imports System.Data

Namespace bp3doc


''' <summary>
'''Реализация раздела Полев виде коллекции
''' </summary>
''' <remarks>
'''
''' </remarks>
    Public Class bp3doc_field_col
        Inherits BP3.Document.DocCollection_Base



''' <summary>
'''Имя раздела в базе данных
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Overrides Function ChildPartName() As String
            ChildPartName = "bp3doc_field"
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
            dt.Columns.Add("Name", Gettype(System.string))
            dt.Columns.Add("Caption", Gettype(System.string))
            dt.Columns.Add("TabName", Gettype(System.string))
            dt.Columns.Add("FieldGroupBox", Gettype(System.string))
            dt.Columns.Add("AllowNull_VAL" , Gettype(System.Int16))
            dt.Columns.Add("AllowNull", Gettype(System.string))
            dt.Columns.Add("FieldType_ID" , GetType(System.guid))
            dt.Columns.Add("FieldType", Gettype(System.string))
            dt.Columns.Add("ReferenceType_VAL" , Gettype(System.Int16))
            dt.Columns.Add("ReferenceType", Gettype(System.string))
            dt.Columns.Add("DataSize", Gettype(System.Int32))
            dt.Columns.Add("RefToPart_ID" , GetType(System.guid))
            dt.Columns.Add("RefToPart", Gettype(System.string))
            dt.Columns.Add("InternalReference_VAL" , Gettype(System.Int16))
            dt.Columns.Add("InternalReference", Gettype(System.string))
            dt.Columns.Add("TheComment", Gettype(System.string))
            dt.Columns.Add("IsAutoNumber_VAL" , Gettype(System.Int16))
            dt.Columns.Add("IsAutoNumber", Gettype(System.string))
            dt.Columns.Add("IsBrief_VAL" , Gettype(System.Int16))
            dt.Columns.Add("IsBrief", Gettype(System.string))
            dt.Columns.Add("IsTabBrief_VAL" , Gettype(System.Int16))
            dt.Columns.Add("IsTabBrief", Gettype(System.string))
            dt.Columns.Add("TheStyle", Gettype(System.string))
            dt.Columns.Add("TheMask", Gettype(System.string))
            dt.Columns.Add("shablonBrief", Gettype(System.string))
            return dt
        End Function



''' <summary>
'''Создание нового элемента коллекции
''' </summary>
''' <remarks>
'''
''' </remarks>
        Protected Overrides Function NewItem() As BP3.Document.DocRow_Base
            NewItem = New bp3doc_field
        End Function


''' <summary>
'''Получить элемент коллекции
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Function GetItem( vIndex as object ) As bp3doc.bp3doc_field
            on error resume next
            GetItem = Convert.ChangeType(mybase.Item(vIndex), GetType(bp3doc.bp3doc_field))
        End Function


''' <summary>
'''Получить элемент коллекции, более свежий вариант
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Shadows Function Item( vIndex as object ) As bp3doc.bp3doc_field
            on error resume next
            return GetItem(vIndex)
        End Function
Public Overrides Function FieldList() As String
    If mFieldList = "*" Then
       with application.Session.GetProvider
       mFieldList=.ID2Base("bp3doc_fieldID")
           mFieldList =mFieldList+","+.ID2Base("SecurityStyleID") 
           mFieldList =mFieldList+ ", sequence" 
           mFieldList =mFieldList+ ", name" 
           mFieldList =mFieldList+ ", caption" 
           mFieldList =mFieldList+ ", tabname" 
           mFieldList =mFieldList+ ", fieldgroupbox" 
           mFieldList =mFieldList+ ", allownull" 
           mFieldList =mFieldList+","+.ID2Base("FieldType") 
           mFieldList =mFieldList+ ", referencetype" 
           mFieldList =mFieldList+ ", datasize" 
           mFieldList =mFieldList+","+.ID2Base("RefToPart") 
           mFieldList =mFieldList+ ", internalreference" 
           mFieldList =mFieldList+ ", thecomment" 
           mFieldList =mFieldList+ ", isautonumber" 
           mFieldList =mFieldList+ ", isbrief" 
           mFieldList =mFieldList+ ", istabbrief" 
           mFieldList =mFieldList+ ", thestyle" 
           mFieldList =mFieldList+ ", themask" 
           mFieldList =mFieldList+ ", shablonbrief" 
       end with
    End If
    Return mFieldList
End Function

    End Class
End Namespace
