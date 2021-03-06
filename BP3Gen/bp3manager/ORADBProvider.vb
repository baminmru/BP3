Imports System
Imports System.Data
Imports System.Data.Common
Imports System.Data.OracleClient
Imports LATIR2.Utils


Public Class ORADBProvider
    Inherits LATIR2.DBProvider

    Public Sub New()
        ProviderType = DBProviderType.ORACLE
    End Sub

    Public Overrides Function BuildCN( _
           ByVal Server As String, _
           ByVal DataBaseName As String, _
           ByVal UserName As String, _
           ByVal Password As String, _
           ByVal LoginTimeOut As String, _
           ByVal Provider As String, _
           ByVal Integrated As Boolean) As String


        If Integrated Then
            Return "Integrated Security=true;Server=" & Server & ";"
        Else
            Return "Integrated Security=false;Server=" & Server & ";User ID=" & UserName & ";Password=" & Password & ";"
        End If
    End Function

    Public Overrides Function CreateConnection(ByVal ConnectionString As String) As DbConnection
        Dim cn As DbConnection
        cn = New OracleClient.OracleConnection
        cn.ConnectionString = ConnectionString
        Return cn
    End Function

    Public Overrides Function CreateCommand() As DbCommand
        Return New OracleClient.OracleCommand
    End Function

    Public Overrides Function CreateDataAdapter() As DbDataAdapter
        Return New OracleClient.OracleDataAdapter
    End Function

    Public Overrides Function GetTestQuery() As String
        Return "SELECT 'OK' SRV_TEST from dummy"
    End Function
    Public Overrides Function GetServerDate() As String
        Return "sysdate"
    End Function

    Public Overrides Function Date2Const(ByVal d As Date) As String
        Return MakeORACLEDate(d)
    End Function


    Public Overrides Function ID2Base(ByVal fieldName As String) As String
        Return fieldName
    End Function


    Public Overrides Function ID2Const(ByVal ID As Guid) As String
        Return "'" + ID2String(ID.ToString()) + "'"
    End Function

    Public Overrides Function ID2DbType() As DbType
        Return DbType.String
    End Function

    Public Overrides Function ID2Size() As Integer
        Return 38
    End Function



    Public Overrides Function ID2Param(ByVal ID As Guid) As Object
        Return ID2Const(ID)
    End Function

    Public Overrides Function Date2Base(ByVal fieldName As String) As String
        Return fieldName
    End Function


   
End Class
