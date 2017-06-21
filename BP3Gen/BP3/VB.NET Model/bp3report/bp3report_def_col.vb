

Option Explicit On

Imports LATIR2
Imports System
Imports System.xml
Imports System.Data

Namespace bp3report


''' <summary>
'''Реализация раздела Описаниев виде коллекции
''' </summary>
''' <remarks>
'''
''' </remarks>
    Public Class bp3report_def_col
        Inherits LATIR2.Document.DocCollection_Base



''' <summary>
'''Имя раздела в базе данных
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Overrides Function ChildPartName() As String
            ChildPartName = "bp3report_def"
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
            dt.Columns.Add("Caption", Gettype(System.string))
            dt.Columns.Add("ReportType_VAL" , Gettype(System.Int16))
            dt.Columns.Add("ReportType", Gettype(System.string))
            dt.Columns.Add("TheComment", Gettype(System.string))
            dt.Columns.Add("ReportFile", GetType(System.object))
            dt.Columns.Add("Name", Gettype(System.string))
            dt.Columns.Add("ReportView_ID" , GetType(System.guid))
            dt.Columns.Add("ReportView", Gettype(System.string))
            return dt
        End Function



''' <summary>
'''Создание нового элемента коллекции
''' </summary>
''' <remarks>
'''
''' </remarks>
        Protected Overrides Function NewItem() As LATIR2.Document.DocRow_Base
            NewItem = New bp3report_def
        End Function


''' <summary>
'''Получить элемент коллекции
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Function GetItem( vIndex as object ) As bp3report.bp3report_def
            on error resume next
            GetItem = Convert.ChangeType(mybase.Item(vIndex), GetType(bp3report.bp3report_def))
        End Function


''' <summary>
'''Получить элемент коллекции, более свежий вариант
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Shadows Function Item( vIndex as object ) As bp3report.bp3report_def
            on error resume next
            return GetItem(vIndex)
        End Function
Public Overrides Function FieldList() As String
    If mFieldList = "*" Then
       with application.Session.GetProvider
       mFieldList=.ID2Base("bp3report_defID")
           mFieldList =mFieldList+","+.ID2Base("SecurityStyleID") 
           mFieldList =mFieldList+ ", caption" 
           mFieldList =mFieldList+ ", reporttype" 
           mFieldList =mFieldList+ ", thecomment" 
           mFieldList =mFieldList+ ", reportfile" 
           mFieldList =mFieldList+ ", name" 
           mFieldList =mFieldList+","+.ID2Base("ReportView") 
       end with
    End If
    Return mFieldList
End Function

    End Class
End Namespace
