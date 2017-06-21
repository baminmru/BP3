

Option Explicit On

Imports BP3
Imports System
Imports System.xml
Imports System.Data

Namespace bp3dic


''' <summary>
'''Реализация раздела Генераторыв виде коллекции
''' </summary>
''' <remarks>
'''
''' </remarks>
    Public Class bp3dic_gen_col
        Inherits BP3.Document.DocCollection_Base



''' <summary>
'''Имя раздела в базе данных
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Overrides Function ChildPartName() As String
            ChildPartName = "bp3dic_gen"
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
            dt.Columns.Add("Name", Gettype(System.string))
            dt.Columns.Add("GeneratorStyle_VAL" , Gettype(System.Int16))
            dt.Columns.Add("GeneratorStyle", Gettype(System.string))
            dt.Columns.Add("QueueName", Gettype(System.string))
            dt.Columns.Add("TheDevelopmentEnv_VAL" , Gettype(System.Int16))
            dt.Columns.Add("TheDevelopmentEnv", Gettype(System.string))
            dt.Columns.Add("GeneratorProgID", Gettype(System.string))
            dt.Columns.Add("TargetType_VAL" , Gettype(System.Int16))
            dt.Columns.Add("TargetType", Gettype(System.string))
            return dt
        End Function



''' <summary>
'''Создание нового элемента коллекции
''' </summary>
''' <remarks>
'''
''' </remarks>
        Protected Overrides Function NewItem() As BP3.Document.DocRow_Base
            NewItem = New bp3dic_gen
        End Function


''' <summary>
'''Получить элемент коллекции
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Function GetItem( vIndex as object ) As bp3dic.bp3dic_gen
            on error resume next
            GetItem = Convert.ChangeType(mybase.Item(vIndex), GetType(bp3dic.bp3dic_gen))
        End Function


''' <summary>
'''Получить элемент коллекции, более свежий вариант
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Shadows Function Item( vIndex as object ) As bp3dic.bp3dic_gen
            on error resume next
            return GetItem(vIndex)
        End Function
Public Overrides Function FieldList() As String
    If mFieldList = "*" Then
       with application.Session.GetProvider
       mFieldList=.ID2Base("bp3dic_genID")
           mFieldList =mFieldList+","+.ID2Base("SecurityStyleID") 
           mFieldList =mFieldList+ ", name" 
           mFieldList =mFieldList+ ", generatorstyle" 
           mFieldList =mFieldList+ ", queuename" 
           mFieldList =mFieldList+ ", thedevelopmentenv" 
           mFieldList =mFieldList+ ", generatorprogid" 
           mFieldList =mFieldList+ ", targettype" 
       end with
    End If
    Return mFieldList
End Function

    End Class
End Namespace
