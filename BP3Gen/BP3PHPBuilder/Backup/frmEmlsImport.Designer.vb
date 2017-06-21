<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEmlsImport
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.cmdFile = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtFile = New System.Windows.Forms.TextBox()
        Me.pb = New System.Windows.Forms.ProgressBar()
        Me.lblInfo = New System.Windows.Forms.Label()
        Me.cmdImport = New System.Windows.Forms.Button()
        Me.cdlg = New System.Windows.Forms.OpenFileDialog()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbSrc = New System.Windows.Forms.ComboBox()
        Me.cmbFMT = New System.Windows.Forms.ComboBox()
        Me.cmdLoadFields = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'cmdFile
        '
        Me.cmdFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdFile.Location = New System.Drawing.Point(335, 34)
        Me.cmdFile.Name = "cmdFile"
        Me.cmdFile.Size = New System.Drawing.Size(109, 19)
        Me.cmdFile.TabIndex = 0
        Me.cmdFile.Text = "..."
        Me.cmdFile.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 33)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(36, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Файл"
        '
        'txtFile
        '
        Me.txtFile.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtFile.Location = New System.Drawing.Point(54, 33)
        Me.txtFile.Name = "txtFile"
        Me.txtFile.ReadOnly = True
        Me.txtFile.Size = New System.Drawing.Size(275, 20)
        Me.txtFile.TabIndex = 2
        '
        'pb
        '
        Me.pb.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pb.Location = New System.Drawing.Point(15, 217)
        Me.pb.Name = "pb"
        Me.pb.Size = New System.Drawing.Size(314, 30)
        Me.pb.TabIndex = 3
        '
        'lblInfo
        '
        Me.lblInfo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblInfo.Location = New System.Drawing.Point(15, 284)
        Me.lblInfo.Name = "lblInfo"
        Me.lblInfo.Size = New System.Drawing.Size(429, 51)
        Me.lblInfo.TabIndex = 4
        '
        'cmdImport
        '
        Me.cmdImport.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdImport.Location = New System.Drawing.Point(335, 217)
        Me.cmdImport.Name = "cmdImport"
        Me.cmdImport.Size = New System.Drawing.Size(109, 30)
        Me.cmdImport.TabIndex = 5
        Me.cmdImport.Text = "Импорт"
        Me.cmdImport.UseVisualStyleBackColor = True
        '
        'cdlg
        '
        Me.cdlg.AddExtension = False
        Me.cdlg.Filter = "All files|*.*"
        Me.cdlg.Title = "Экспортный файл EMLS"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 70)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(95, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Источник данных"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 106)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(84, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Формат файла"
        '
        'cmbSrc
        '
        Me.cmbSrc.FormattingEnabled = True
        Me.cmbSrc.Location = New System.Drawing.Point(112, 67)
        Me.cmbSrc.Name = "cmbSrc"
        Me.cmbSrc.Size = New System.Drawing.Size(331, 21)
        Me.cmbSrc.TabIndex = 8
        '
        'cmbFMT
        '
        Me.cmbFMT.FormattingEnabled = True
        Me.cmbFMT.Location = New System.Drawing.Point(112, 103)
        Me.cmbFMT.Name = "cmbFMT"
        Me.cmbFMT.Size = New System.Drawing.Size(330, 21)
        Me.cmbFMT.TabIndex = 9
        '
        'cmdLoadFields
        '
        Me.cmdLoadFields.Location = New System.Drawing.Point(12, 148)
        Me.cmdLoadFields.Name = "cmdLoadFields"
        Me.cmdLoadFields.Size = New System.Drawing.Size(137, 30)
        Me.cmdLoadFields.TabIndex = 10
        Me.cmdLoadFields.Text = "Подготовка формата"
        Me.cmdLoadFields.UseVisualStyleBackColor = True
        '
        'frmEmlsImport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(456, 361)
        Me.Controls.Add(Me.cmdLoadFields)
        Me.Controls.Add(Me.cmbFMT)
        Me.Controls.Add(Me.cmbSrc)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmdImport)
        Me.Controls.Add(Me.lblInfo)
        Me.Controls.Add(Me.pb)
        Me.Controls.Add(Me.txtFile)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmdFile)
        Me.Name = "frmEmlsImport"
        Me.Text = "Импорт из EMLS"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmdFile As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtFile As System.Windows.Forms.TextBox
    Friend WithEvents pb As System.Windows.Forms.ProgressBar
    Friend WithEvents lblInfo As System.Windows.Forms.Label
    Friend WithEvents cmdImport As System.Windows.Forms.Button
    Friend WithEvents cdlg As System.Windows.Forms.OpenFileDialog
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbSrc As System.Windows.Forms.ComboBox
    Friend WithEvents cmbFMT As System.Windows.Forms.ComboBox
    Friend WithEvents cmdLoadFields As System.Windows.Forms.Button
End Class
