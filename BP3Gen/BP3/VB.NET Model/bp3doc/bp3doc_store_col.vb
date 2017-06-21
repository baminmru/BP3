

Option Explicit On

Imports BP3
Imports System
Imports System.xml
Imports System.Data

Namespace bp3doc


''' <summary>
'''Реализация раздела Разделв виде коллекции
''' </summary>
''' <remarks>
'''
''' </remarks>
    Public Class bp3doc_store_col
        Inherits BP3.Document.DocCollection_Base



''' <summary>
'''Имя раздела в базе данных
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Overrides Function ChildPartName() As String
            ChildPartName = "bp3doc_store"
        End Function



''' <summary>
'''Признак древовидного раздела
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Overrides Function IsTree() As Boolean
            IsTree=true
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
            dt.Columns.Add("Caption", Gettype(System.string))
            dt.Columns.Add("PartType_VAL" , Gettype(System.Int16))
            dt.Columns.Add("PartType", Gettype(System.string))
            dt.Columns.Add("Name", Gettype(System.string))
            dt.Columns.Add("the_Comment", Gettype(System.string))
            dt.Columns.Add("IsDocInstance_VAL" , Gettype(System.Int16))
            dt.Columns.Add("IsDocInstance", Gettype(System.string))
            dt.Columns.Add("UseArchiving_VAL" , Gettype(System.Int16))
            dt.Columns.Add("UseArchiving", Gettype(System.string))
            dt.Columns.Add("NoLog_VAL" , Gettype(System.Int16))
            dt.Columns.Add("NoLog", Gettype(System.string))
            dt.Columns.Add("shablonBrief", Gettype(System.string))
            dt.Columns.Add("UseChangeLog_VAL" , Gettype(System.Int16))
            dt.Columns.Add("UseChangeLog", Gettype(System.string))
            dt.Columns.Add("ruleBrief", Gettype(System.string))
            return dt
        End Function



''' <summary>
'''Создание нового элемента коллекции
''' </summary>
''' <remarks>
'''
''' </remarks>
        Protected Overrides Function NewItem() As BP3.Document.DocRow_Base
            NewItem = New bp3doc_store
        End Function


''' <summary>
'''Получить элемент коллекции
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Function GetItem( vIndex as object ) As bp3doc.bp3doc_store
            on error resume next
            GetItem = Convert.ChangeType(mybase.Item(vIndex), GetType(bp3doc.bp3doc_store))
        End Function


''' <summary>
'''Получить элемент коллекции, более свежий вариант
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Shadows Function Item( vIndex as object ) As bp3doc.bp3doc_store
            on error resume next
            return GetItem(vIndex)
        End Function
Public Overrides Function FieldList() As String
    If mFieldList = "*" Then
       with application.Session.GetProvider
       mFieldList=.ID2Base("bp3doc_storeID")
           mFieldList =mFieldList+","+.ID2Base("SecurityStyleID") 
           mFieldList =mFieldList+","+.ID2Base("PARENTROWID") 
           mFieldList =mFieldList+ ", sequence" 
           mFieldList =mFieldList+ ", caption" 
           mFieldList =mFieldList+ ", parttype" 
           mFieldList =mFieldList+ ", name" 
           mFieldList =mFieldList+ ", the_comment" 
           mFieldList =mFieldList+ ", isdocinstance" 
           mFieldList =mFieldList+ ", usearchiving" 
           mFieldList =mFieldList+ ", nolog" 
           mFieldList =mFieldList+ ", shablonbrief" 
           mFieldList =mFieldList+ ", usechangelog" 
           mFieldList =mFieldList+ ", rulebrief" 
       end with
    End If
    Return mFieldList
End Function

    End Class
End Namespace
