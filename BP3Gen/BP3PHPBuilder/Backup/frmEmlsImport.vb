Imports System.IO
Imports System.Collections
Imports System.Text

Public Class frmEmlsImport

    Private Sub cmdFile_Click(sender As System.Object, e As System.EventArgs) Handles cmdFile.Click
        If cdlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtFile.Text = cdlg.FileName
        End If

    End Sub

    Private Sub cmdImport_Click(sender As System.Object, e As System.EventArgs) Handles cmdImport.Click
        Dim s As String
        If txtFile.Text = "" Then Exit Sub

        s = File.ReadAllText(txtFile.Text, System.Text.Encoding.Default)
        Dim lines() As String
        Dim header() As String
        Dim blocks() As String
        s = s.Replace(vbCrLf, vbCr)
        lines = s.Split(vbCr)
        Dim i As Integer
        header = lines(lines.GetLowerBound(0)).Split(Chr(&H9A))
        For i = lines.GetLowerBound(0) + 1 To lines.GetUpperBound(0)
            blocks = lines(i).Split(Chr(&H9A))
            ProcessBlock(header, blocks)
        Next

    End Sub

    Private Sub ProcessBlock(hdr() As String, info() As String)
        Dim row As Dictionary(Of String, String)
        row = New Dictionary(Of String, String)
        Dim i As Integer
        For i = hdr.GetLowerBound(0) To hdr.GetUpperBound(0)
            If info.GetUpperBound(0) >= i Then
                row.Add(hdr(i), info(i))
            End If
        Next

        Debug.Print("----------------------------------")
        For Each kvp As KeyValuePair(Of String, String) In row
            Debug.Print(kvp.Key + ":" + kvp.Value)
        Next
    End Sub

    Private Sub frmEmlsImport_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim dt As DataTable
        dt = Manager.Session.GetData("select name," + Manager.Session.GetProvider.ID2Base("INSTANCEID") + " from UIS_SRC order by name")
        cmbSrc.DisplayMember = "name"
        cmbSrc.ValueMember = "INSTANCEID"
        cmbSrc.DataSource = dt
    End Sub

    Private src_ID As Guid
    Private Sub cmbSrc_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbSrc.SelectedIndexChanged

        src_ID = New Guid(cmbSrc.SelectedValue.ToString)
        Dim dt As DataTable
        dt = Manager.Session.GetData("select name," + Manager.Session.GetProvider.ID2Base("UIS_FILEID", "ID") + " from UIS_FILE where instanceid=" + Manager.Session.GetProvider.ID2Const(src_ID) + " order by name")
        cmbFMT.DataSource = Nothing
        cmbFMT.DisplayMember = "name"
        cmbFMT.ValueMember = "ID"
        cmbFMT.DataSource = dt
    End Sub

    Private Sub cmdLoadFields_Click(sender As System.Object, e As System.EventArgs) Handles cmdLoadFields.Click
        'Dim s As String
        'If txtFile.Text = "" Then Exit Sub

        's = File.ReadAllText(txtFile.Text, System.Text.Encoding.Default)
        'Dim lines() As String
        'Dim hdr() As String
        'Dim uis As UIS.UIS.Application
        'Dim uif As UIS.UIS.UIS_FILE
        'uis = Manager.GetInstanceObject(src_ID)

        'Dim fid As Guid
        'fid = New Guid(cmbFMT.SelectedValue.ToString)

        'uif = uis.UIS_FILE.Item(fid.ToString)

        'Dim dt As DataTable
        'dt = Manager.Session.GetData("select name," + Manager.Session.GetProvider.ID2Base("UIS_FILEID", "ID") + " from UIS_FILE where instanceid=" + Manager.Session.GetProvider.ID2Const(src_ID) + " order by name")


        's = s.Replace(vbCrLf, vbCr)
        'lines = s.Split(vbCr)
        'Dim i As Integer
        'hdr = lines(lines.GetLowerBound(0)).Split(Chr(&H9A))

        'For i = hdr.GetLowerBound(0) To hdr.GetUpperBound(0)

        'Next
    End Sub
End Class