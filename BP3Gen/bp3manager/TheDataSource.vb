

Option Explicit On

Imports System
Imports System.Data
Imports System.Data.Common
'Imports System.Data.OracleClient
Imports System.IO
Imports BP3.Utils


Public Class TheDataSource
    Implements IDisposable
    Private _cn As String
    Private _connection As DbConnection
    Private _provider As BP3.DBProvider
    Private _transaction As System.Data.Common.DbTransaction
    Private Application As Session
    Private m_reader As DataTable

    'Public Function IsOracle() As BP3.DBProvider
    '    _provider = _provider
    'End Function

    Public Sub New(ByVal connectionString As String, ByVal Provider As BP3.DBProvider, ByVal parent As Session)
        Application = parent
        _provider = Provider
        _cn = connectionString
        InitConnection()
    End Sub

    Protected Sub InitConnection()
        _connection = _provider.CreateConnection(_cn)
        Try
            _connection.Open()
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try

    End Sub

    Public Overridable Function ExecuteReader(ByVal command As System.Data.Common.DbCommand) As DataTable
        Dim da As DbDataAdapter
        Dim dt As DataTable
        da = _provider.CreateDataAdapter()
        da.SelectCommand = command
        dt = New DataTable
        da.Fill(dt)
        Return dt
    End Function

    Public Overridable Function ExecuteReader(ByVal sqltext As String) As DataTable
        Dim command As System.Data.Common.DbCommand
        command = CreateCommand(sqltext)
        Dim da As DbDataAdapter
        Dim dt As DataTable
        SyncLock _connection
            da = _provider.CreateDataAdapter()
            da.SelectCommand = command
            dt = New DataTable
            da.Fill(dt)
        End SyncLock
        Return dt
    End Function

    Public Function AddParameter(ByVal cmd As System.Data.Common.DbCommand, ByVal paramName As String, _
    ByVal dbType As DbType, ByVal value As Object, Optional ByVal Direction As ParameterDirection = ParameterDirection.Input, Optional ByVal ParSize As Integer = 0) As IDbDataParameter
        Dim parameter As IDbDataParameter = cmd.CreateParameter()
        parameter.ParameterName = CreateCollectionParameterName(paramName)
        parameter.DbType = dbType
        parameter.SourceColumn = parameter.ParameterName
        parameter.Direction = Direction
        If ParSize > 0 Then
            parameter.Size = CInt(ParSize)
        End If
        If value Is Nothing Then
            parameter.Value = DBNull.Value
        Else
            If dbType = Data.DbType.Guid Then
                If value.ToString() = "" Then
                    parameter.Value = value
                Else
                    parameter.Value = New System.Guid(value.ToString())
                End If

            Else
                parameter.Value = value
            End If
        End If
        cmd.Parameters.Add(parameter)
        Return parameter
    End Function




    Public Function GetParameter(ByVal cmd As System.Data.Common.DbCommand, ByVal paramName As String) As IDbDataParameter
        Dim parameter As IDbDataParameter
        Dim s As String
        s = CreateCollectionParameterName(paramName)
        parameter = cmd.Parameters.Item(s)
        Return parameter
    End Function

    Public ReadOnly Property Connection() As DbConnection
        Get
            If _connection.State = ConnectionState.Open Then
                Return _connection
            End If
            InitConnection()
            Return _connection
        End Get
    End Property

    Public Function BeginTransaction() As DbTransaction
        CheckTransactionState(False)
        _transaction = _connection.BeginTransaction()
        Return _transaction
    End Function

    Public Function BeginTransaction(ByVal isolationLevel As IsolationLevel) As DbTransaction
        CheckTransactionState(False)
        _transaction = _connection.BeginTransaction(isolationLevel)
        Return _transaction
    End Function

    Public Overridable Function Execute(ByVal sqltext As String) As Long
        Dim cmd As System.Data.Common.DbCommand

        Debug.Print(sqltext)
        cmd = CreateCommand(sqltext)
        Dim l As Long
        SyncLock _connection
            l = cmd.ExecuteNonQuery
            cmd.Dispose()
        End SyncLock
        Return l
    End Function


    Public Overridable Function ExecuteProc(ByVal sqltext As String, ByVal Params As BP3.NamedValues, Optional ByVal AddCurSession As Boolean = True) As Boolean
        Dim cmd As System.Data.Common.DbCommand, i As Long
        Dim IsSessionWas As Boolean = False
        Dim bNeedAppendChunk As Boolean = False
        cmd = CreateCommand(sqltext, True)
        Debug.Print(sqltext)
        If Not Params Is Nothing Then
            For i = 1 To Params.Count

                If UCase(Params.Item(i).TheName) = UCase("CURSESSION") Then
                    Params.Item(i).Value = Application.GetProvider.ID2Param(Application.SessionID())
                    IsSessionWas = True

                End If

                If IsDBNull(Params.Item(i).Value) Then
                    AddParameter(cmd, CreateSqlParameterName(Params.Item(i).TheName), Params.Item(i).DataType, DBNull.Value, Params.Item(i).Direction, Params.Item(i).Size)
                    Debug.Print(Params.Item(i).TheName + "->" + "NULL")
                Else
                    If Params.Item(i).DataType = DbType.String Then
                        If Params.Item(i).Value Is Nothing Then
                            AddParameter(cmd, CreateSqlParameterName(Params.Item(i).TheName), Params.Item(i).DataType, DBNull.Value, Params.Item(i).Direction, Params.Item(i).Size)
                            Debug.Print(Params.Item(i).TheName + "->" + "NULL")
                        Else


                            If Params.Item(i).Value.ToString = "" And Not Provider.ProviderType = DBProvider.DBProviderType.ORACLE Then
                                AddParameter(cmd, CreateSqlParameterName(Params.Item(i).TheName), Params.Item(i).DataType, DBNull.Value, Params.Item(i).Direction, Params.Item(i).Size)
                                Debug.Print(Params.Item(i).TheName + "->" + "NULL")
                            Else
                                'Params.Item(i).DataType = DbType.StringFixedLength
                                Debug.Print(Params.Item(i).TheName + "->" + Params.Item(i).Value.ToString)
                                AddParameter(cmd, CreateSqlParameterName(Params.Item(i).TheName), Params.Item(i).DataType, Params.Item(i).Value, Params.Item(i).Direction, Params.Item(i).Size)
                            End If
                        End If
                    ElseIf Params.Item(i).DataType = DbType.Binary And _provider.ProviderType = DBProvider.DBProviderType.ORACLE Then
                        AddParameter(cmd, CreateSqlParameterName(Params.Item(i).TheName), Params.Item(i).DataType, Nothing, Params.Item(i).Direction, 0)
                        Debug.Print(Params.Item(i).TheName + "->" + Params.Item(i).Value.ToString)
                        bNeedAppendChunk = True
                    Else
                        AddParameter(cmd, CreateSqlParameterName(Params.Item(i).TheName), Params.Item(i).DataType, Params.Item(i).Value, Params.Item(i).Direction, Params.Item(i).Size)
                        Debug.Print(Params.Item(i).TheName + "->" + Params.Item(i).Value.ToString)
                    End If
                End If
            Next
        End If


        'Try

        'Catch
        'End Try



        If Not IsSessionWas And AddCurSession Then
            AddParameter(cmd, "CURSESSION", _provider.ID2DbType(), _provider.ID2Param(Application.SessionID()), ParameterDirection.Input, _provider.ID2Size())
        End If

        Try

            SyncLock _connection
                cmd.ExecuteNonQuery()
            End SyncLock




            'Check for binary data
            If bNeedAppendChunk And UCase(Right(sqltext, 5)) = "_SAVE" Then
                '���� �� Save, �� ����� �����...
                Dim sTableName As String
                'Dim sFiledName As String
                '���������� ���������, ���� ��, ��� ����� ��������...
                ' ��� �������: =left(SqlString, instr(1,SqlString,"_save",vbTextCompare)-1)
                ' ��� ����: =Right(Parameter.Item(I).TheName, len(Parameter.Item(I).TheName)-len(Application.SymbolAt))
                If InStrRev(sqltext, ".", -1, vbTextCompare) > 0 Then
                    sTableName = Mid(sqltext, InStrRev(sqltext, ".", -1, vbTextCompare) + 1, Len(sqltext) - InStrRev(sqltext, ".", -1, vbTextCompare) - Len("_save"))
                Else
                    sTableName = Left(sqltext, InStr(1, sqltext, "_save", vbTextCompare) - 1)
                End If
                i = 0
                If Not Params Is Nothing Then
                    For i = 1 To Params.Count
                        If i = 0 Then Exit For
                        If Params.Item(i).DataType = DbType.Binary Then

                            '���� ��� ����� ��������, ������ ���� ������ � �������...
                            '������������� ������: Application.SymbolAt + ��� ������� + "id"
                            Dim j As Long
                            Dim RowID As String
                            RowID = ""
                            For j = 1 To Params.Count
                                If UCase(Params.Item(j).TheName) = UCase(sTableName + "id") Then
                                    RowID = CStr(Params.Item(j).Value)
                                    Exit For
                                End If
                            Next
                            If RowID <> "" Then
                                'If Provider.ProviderType = DBProvider.DBProviderType.ORACLE Then
                                '    SyncLock _connection
                                '        Dim da As OracleDataAdapter = New OracleDataAdapter("select * from " + sTableName + " where " + sTableName + "id='" + RowID + "'", CType(_connection, System.Data.OracleClient.OracleConnection))
                                '        Dim dt As DataTable = New DataTable()
                                '        da.FillSchema(dt, SchemaType.Source)
                                '        da.Fill(dt)
                                '        Dim cb As OracleCommandBuilder = New OracleCommandBuilder(da)
                                '        Dim row As DataRow
                                '        row = dt.Rows(0)
                                '        row.Item(Params.Item(i).TheName) = Params.Item(i).Value
                                '        da.Update(dt)
                                '    End SyncLock
                                'End If


                            End If
                        End If
                    Next i
                End If

            End If
        Catch ex As Exception
            ' ��� ����� ������
            Throw ex
        End Try


        Try
            If Not Params Is Nothing Then
                For i = 1 To Params.Count
                    Params.Item(i).Value = cmd.Parameters.Item(CreateCollectionParameterName(Params.Item(i).TheName)).Value
                Next
            End If
            cmd.Dispose()
            Return True
        Catch ex As System.Exception
            cmd.Dispose()
            Return False
        End Try
    End Function

    Public Overridable Function ExecuteProcNoSession(ByVal sqltext As String, ByVal Params As BP3.NamedValues) As Boolean
        Dim cmd As System.Data.Common.DbCommand, i As Long
        cmd = CreateCommand(sqltext, True)
        For i = 1 To Params.Count

            If UCase(Params.Item(i).TheName) = UCase("CURSESSION") Then
                Params.Item(i).Value = Application.SessionID()
            End If

            If IsDBNull(Params.Item(i).Value) Then
                AddParameter(cmd, Params.Item(i).TheName, Params.Item(i).DataType, DBNull.Value, Params.Item(i).Direction, Params.Item(i).Size)
            Else
                If Params.Item(i).DataType = DbType.String Then
                    If Params.Item(i).Value.ToString = "" Then
                        AddParameter(cmd, Params.Item(i).TheName, Params.Item(i).DataType, DBNull.Value, Params.Item(i).Direction, Params.Item(i).Size)
                    Else
                        AddParameter(cmd, Params.Item(i).TheName, Params.Item(i).DataType, Params.Item(i).Value, Params.Item(i).Direction, Params.Item(i).Size)
                    End If
                Else
                    AddParameter(cmd, Params.Item(i).TheName, Params.Item(i).DataType, Params.Item(i).Value, Params.Item(i).Direction, Params.Item(i).Size)
                End If
            End If
        Next

        Try

        Catch
        End Try

        ' ������� ��������� ��� ��������� CURSESSION
        Try
            SyncLock _connection
                cmd.ExecuteNonQuery()
            End SyncLock

        Catch ex As Exception
            Throw ex
        End Try


        Try
            For i = 1 To Params.Count
                Params.Item(i).Value = cmd.Parameters(CreateCollectionParameterName(Params.Item(i).TheName)).Value
            Next
            cmd.Dispose()
            Return True
        Catch ex As System.Exception
            cmd.Dispose()
            Return False
        End Try
    End Function


    Public Sub CommitTransaction()
        CheckTransactionState(True)
        _transaction.Commit()
        _transaction = Nothing
    End Sub

    Public Sub RollbackTransaction()
        CheckTransactionState(True)
        _transaction.Rollback()
        _transaction = Nothing
    End Sub

    Private Sub CheckTransactionState(ByVal mustBeOpen As Boolean)
        If mustBeOpen Then
            If _transaction Is Nothing Then
                Throw New InvalidOperationException("Transaction is not open.")
            End If
        Else
            If Not (_transaction Is Nothing) Then
                Throw New InvalidOperationException("Transaction is already open.")
            End If
        End If
    End Sub

    Friend Function CreateCommand(ByVal sqlText As String) As System.Data.Common.DbCommand
        Return CreateCommand(sqlText, False)
    End Function

    Friend Function CreateCommand(ByVal sqlText As String, ByVal procedure As Boolean) As System.Data.Common.DbCommand
        Dim cmd As System.Data.Common.DbCommand = _connection.CreateCommand()
        cmd.CommandText = sqlText

        Try
            cmd.Transaction = _transaction
        Catch
        End Try
        If procedure Then
            cmd.CommandType = CommandType.StoredProcedure
        End If

        Return cmd
    End Function

    Public Overridable Sub Close()
        If Not _connection Is Nothing Then
            _connection.Close()
        End If
    End Sub

    Public Overridable Sub Dispose() Implements IDisposable.Dispose
        Close()
        If Not _connection Is Nothing Then
            _connection.Dispose()
        End If
    End Sub

    Public Function CreateDataTable(ByVal command As System.Data.Common.DbCommand) As DataTable
        Dim DataTable As DataTable = New DataTable
        Dim dataAdapter As DbDataAdapter
        dataAdapter = _provider.CreateDataAdapter()
        dataAdapter.SelectCommand = command
        Try
            dataAdapter.Fill(DataTable)

        Catch ex As Exception
            Debug.Print(ex.Message)
        End Try

        dataAdapter.Dispose()
        Return DataTable
    End Function
    Public Function ExecuteSQL(ByVal sqltext As String) As DataTable
        Dim cmd As System.Data.Common.DbCommand
        cmd = CreateCommand(sqltext)
        Dim DataTable As DataTable
        'SyncLock _connection
        DataTable = CreateDataTable(cmd)
        cmd.Dispose()
        'End SyncLock
        Return DataTable
    End Function


    Protected Friend Function CreateSqlParameterName(ByVal paramName As String) As String
        Return paramName
        'Return Application.SymbolAt + paramName
        'Return "@" + paramName
    End Function

    Protected Function CreateCollectionParameterName(ByVal baseParamName As String) As String
        Return Application.SymbolAt + baseParamName
        'Return "@" + baseParamName
    End Function

    Public ReadOnly Property Provider() As BP3.DBProvider
        Get
            Return _provider
        End Get
    End Property


    Public Sub LoadFileToField(ByVal filepath As String, ByVal table As String, ByVal field As String, ByVal idField As String, ByVal RowID As Guid)


        If filepath <> "" Then
            Dim file As IO.FileStream

            'If Provider.ProviderType = DBProvider.DBProviderType.ORACLE Then
            '    Try
            '        Dim strSQL As String = _
            '                "UPDATE " + table + " SET " + field + " = :Data WHERE " + idField + "=:ID"
            '        Dim cmd As System.Data.Common.DbCommand

            '        cmd = CreateCommand(strSQL)
            '        Dim aBytes() As Byte
            '        file = New IO.FileStream(filepath, IO.FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
            '        ReDim aBytes(CInt(file.Length))
            '        file.Read(aBytes, 0, CInt(file.Length))
            '        cmd.Parameters.Add(New OracleClient.OracleParameter("Data", OracleClient.OracleType.Blob))
            '        cmd.Parameters.Add(New OracleClient.OracleParameter("ID", RowID.ToString))
            '        cmd.Parameters("Data").Value = aBytes
            '        cmd.ExecuteNonQuery()
            '        file.Close()
            '    Catch ex As Exception

            '    Finally
            '    End Try


            'End If


            If Provider.ProviderType = DBProvider.DBProviderType.MSSQL Then
                Try
                    Dim strSQL As String = _
                            "UPDATE " + table + " SET " + field + " = @Data WHERE " + idField + "=@ID"
                    Dim cmd As System.Data.Common.DbCommand

                    cmd = CreateCommand(strSQL)
                    Dim aBytes() As Byte
                    file = New IO.FileStream(filepath, IO.FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
                    ReDim aBytes(CInt(file.Length))
                    file.Read(aBytes, 0, CInt(file.Length))
                    cmd.Parameters.Add(New SqlClient.SqlParameter("Data", SqlDbType.Image))
                    cmd.Parameters.Add(New SqlClient.SqlParameter("ID", RowID))
                    cmd.Parameters("Data").Value = aBytes
                    cmd.ExecuteNonQuery()
                    file.Close()
                Catch ex As Exception

                Finally
                End Try
            End If
        Else
            Dim strSQL As String = _
                        "UPDATE " + table + " SET " + field + " = null WHERE " + idField + " ='" & RowID.ToString() & "'"
            Dim cmd As System.Data.Common.DbCommand
            cmd = CreateCommand(strSQL)
            cmd.ExecuteNonQuery()
        End If

    End Sub

    'Public Function SaveFileFromField(ByVal filepath As String, ByVal table As String, ByVal field As String, ByVal idField As String, ByVal RowID As Guid) As Long
    '    Dim fs As FileStream                 ' Writes the BLOB to a file (*.bmp).
    '    Dim bw As BinaryWriter               ' Streams the binary data to the FileStream object.
    '    Dim bufferSize As Integer = 32000      ' The size of the BLOB buffer.
    '    Dim outbyte(bufferSize - 1) As Byte  ' The BLOB byte() buffer to be filled by GetBytes.
    '    Dim retval As Long                   ' The bytes returned from GetBytes.
    '    Dim startIndex As Long = 0           ' The starting position in the BLOB output.
    '    Dim cmd As System.Data.Common.DbCommand
    '    cmd = CreateCommand("select " + field + " from " + table + " where " + idField + "='" + RowID.ToString + "'")

    '    Dim myReader As System.Data.OracleClient.OracleDataReader = Nothing
    '    Try
    '        fs = New FileStream(filepath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite)
    '    Catch
    '        Return 0
    '    End Try
    '    Try
    '        myReader = CType(cmd.ExecuteReader(CommandBehavior.SequentialAccess), OracleDataReader)
    '        Do While myReader.Read()

    '            bw = New BinaryWriter(fs)
    '            startIndex = 0
    '            retval = myReader.GetBytes(0, startIndex, outbyte, 0, bufferSize)
    '            Do While retval = bufferSize
    '                bw.Write(outbyte)
    '                bw.Flush()
    '                startIndex += bufferSize
    '                Try
    '                    retval = myReader.GetBytes(0, startIndex, outbyte, 0, bufferSize)
    '                Catch ex As Exception
    '                    retval = 0
    '                End Try
    '            Loop
    '            'bw.Write(outbyte, 0, retval - 1)
    '            bw.Write(outbyte, 0, CInt(retval))
    '            bw.Flush()
    '            bw.Close()
    '        Loop
    '    Catch

    '    Finally
    '        If (Not myReader Is Nothing) Then
    '            myReader.Dispose()
    '        End If
    '        If (Not cmd Is Nothing) Then
    '            cmd.Dispose()
    '        End If
    '    End Try

    '    Try
    '        fs.Close()
    '    Catch
    '    End Try
    '    Return retval
    'End Function

End Class

