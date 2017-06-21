Imports System.IO
Imports System.Text
Module dbCreate

    

    Private Sub WriteToLog(ByVal s As String)
        Console.WriteLine(s)
    End Sub

    Private Sub prepareFolder(ByVal path As String, Optional ByVal Delete As Boolean = True)
        If (Delete) Then
            If (System.IO.Directory.Exists(path)) Then

                Dim dinf As DirectoryInfo
                dinf = New System.IO.DirectoryInfo(path)
                For Each fi As FileInfo In dinf.GetFiles()
                    Try
                        fi.Delete()
                    Catch
                    End Try
                Next

            End If
        End If
        Dim di As DirectoryInfo
        'If (Not System.IO.Directory.Exists(path)) Then
        '    Stop
        'End If
        Dim again As Boolean
        again = True
        While (Not System.IO.Directory.Exists(path) And again)
            Try
                di = System.IO.Directory.CreateDirectory(path)

            Catch

            End Try
            If Not System.IO.Directory.Exists(path) Then
                again = (MsgBox("Ошибка создания директории :" & path & vbCrLf & "Повторить попытку создания?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes)
            End If
        End While


        'End If
    End Sub
    Public Sub generateMYSQL()
        Dim xmlBasePath As String = GetSetting("BP3BUILDER", "EXTJS2MYSQL_" & Manager.Site, "PATH", "c:\")
        Dim projBasePath As String

        If Not xmlBasePath.EndsWith("\") Then
            xmlBasePath += "\"
        End If

        projBasePath = xmlBasePath + "\" + "MySQL" + "\"
        If Not projBasePath.EndsWith("\") Then
            projBasePath += "\"
        End If
        Dim targetID As System.Guid
        targetID = New System.Guid("{B29CCFC4-7344-446A-8F14-824C92DC2D30}")
        Dim generator As BP3MYSQLGen.Generator = New BP3MYSQLGen.Generator()

        WriteToLog(Environment.NewLine + "MySQL generator started")
        Dim response As BP3Generator.Response
        response = New BP3Generator.Response()
        Dim fname As String
        generator.Run(Manager, CType(response, Object), targetID.ToString)
        prepareFolder(projBasePath + Manager.Site, False)
        fname = projBasePath + Manager.Site + "\" + Now.ToString("yyyyMMddHHmmss") + ".xml"
        response.Save(fname)
        ReplaceInFile(fname, "&#xD;&#xA;", vbCrLf)
        MsgBox(Environment.NewLine + "MySQL generator finished")

    End Sub

    Private Sub ReplaceInFile(ByVal FileName As String, ByVal Mask As String, ByVal Repl As String)
        Dim fs As FileStream = File.Open(FileName, FileMode.Open, FileAccess.Read)
        Dim sr As StreamReader = New StreamReader(fs)
        Dim filecontent As String = sr.ReadToEnd()
        filecontent = filecontent.Replace(Mask, Repl)
        sr.Close()
        fs.Close()
        File.Delete(FileName)
        fs = File.Open(FileName, FileMode.Create, FileAccess.Write)
        Dim sw As StreamWriter = New StreamWriter(fs)
        sw.Write(filecontent)
        sw.Close()
        fs.Close()
    End Sub

End Module
