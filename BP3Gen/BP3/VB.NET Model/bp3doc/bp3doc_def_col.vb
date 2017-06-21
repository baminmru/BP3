

Option Explicit On

Imports BP3
Imports System
Imports System.xml
Imports System.Data

Namespace bp3doc


''' <summary>
'''Реализация раздела Описаниев виде коллекции
''' </summary>
''' <remarks>
'''
''' </remarks>
    Public Class bp3doc_def_col
        Inherits BP3.Document.DocCollection_Base



''' <summary>
'''Имя раздела в базе данных
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Overrides Function ChildPartName() As String
            ChildPartName = "bp3doc_def"
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
            dt.Columns.Add("TheCaption", Gettype(System.string))
            dt.Columns.Add("Name", Gettype(System.string))
            dt.Columns.Add("IsSingleInstance_VAL" , Gettype(System.Int16))
            dt.Columns.Add("IsSingleInstance", Gettype(System.string))
            dt.Columns.Add("TheComment", Gettype(System.string))
            dt.Columns.Add("UseOwnership_VAL" , Gettype(System.Int16))
            dt.Columns.Add("UseOwnership", Gettype(System.string))
            dt.Columns.Add("UseArchiving_VAL" , Gettype(System.Int16))
            dt.Columns.Add("UseArchiving", Gettype(System.string))
            dt.Columns.Add("CommitFullObject_VAL" , Gettype(System.Int16))
            dt.Columns.Add("CommitFullObject", Gettype(System.string))
            return dt
        End Function



''' <summary>
'''Создание нового элемента коллекции
''' </summary>
''' <remarks>
'''
''' </remarks>
        Protected Overrides Function NewItem() As BP3.Document.DocRow_Base
            NewItem = New bp3doc_def
        End Function


''' <summary>
'''Получить элемент коллекции
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Function GetItem( vIndex as object ) As bp3doc.bp3doc_def
            on error resume next
            GetItem = Convert.ChangeType(mybase.Item(vIndex), GetType(bp3doc.bp3doc_def))
        End Function


''' <summary>
'''Получить элемент коллекции, более свежий вариант
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Shadows Function Item( vIndex as object ) As bp3doc.bp3doc_def
            on error resume next
            return GetItem(vIndex)
        End Function
Public Overrides Function FieldList() As String
    If mFieldList = "*" Then
       with application.Session.GetProvider
       mFieldList=.ID2Base("bp3doc_defID")
           mFieldList =mFieldList+","+.ID2Base("SecurityStyleID") 
           mFieldList =mFieldList+ ", thecaption" 
           mFieldList =mFieldList+ ", name" 
           mFieldList =mFieldList+ ", issingleinstance" 
           mFieldList =mFieldList+ ", thecomment" 
           mFieldList =mFieldList+ ", useownership" 
           mFieldList =mFieldList+ ", usearchiving" 
           mFieldList =mFieldList+ ", commitfullobject" 
       end with
    End If
    Return mFieldList
End Function

    End Class
End Namespace
