<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmB2SercherMYSQL
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
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmdClearAll = New System.Windows.Forms.Button()
        Me.cmdSelectAll = New System.Windows.Forms.Button()
        Me.pb = New System.Windows.Forms.ProgressBar()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.button3 = New System.Windows.Forms.Button()
        Me.textBoxOutPutFolder = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.chkObjType = New System.Windows.Forms.CheckedListBox()
        Me.cmdGen = New System.Windows.Forms.Button()
        Me.folderBrowserDialogProjectOutput = New System.Windows.Forms.FolderBrowserDialog()
        Me.cmbTheme = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtTemplate = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtTemplateJS = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtTabTemplate = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtCardTemplate = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 207)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(97, 13)
        Me.Label2.TabIndex = 71
        Me.Label2.Text = "Типы документов"
        '
        'cmdClearAll
        '
        Me.cmdClearAll.Location = New System.Drawing.Point(429, 201)
        Me.cmdClearAll.Name = "cmdClearAll"
        Me.cmdClearAll.Size = New System.Drawing.Size(60, 25)
        Me.cmdClearAll.TabIndex = 70
        Me.cmdClearAll.Text = "Clear all"
        Me.cmdClearAll.UseVisualStyleBackColor = True
        '
        'cmdSelectAll
        '
        Me.cmdSelectAll.Location = New System.Drawing.Point(367, 201)
        Me.cmdSelectAll.Name = "cmdSelectAll"
        Me.cmdSelectAll.Size = New System.Drawing.Size(60, 25)
        Me.cmdSelectAll.TabIndex = 69
        Me.cmdSelectAll.Text = "Select all"
        Me.cmdSelectAll.UseVisualStyleBackColor = True
        '
        'pb
        '
        Me.pb.Location = New System.Drawing.Point(12, 365)
        Me.pb.Name = "pb"
        Me.pb.Size = New System.Drawing.Size(483, 20)
        Me.pb.TabIndex = 67
        Me.pb.Visible = False
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(12, 345)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(113, 17)
        Me.Label1.TabIndex = 68
        Me.Label1.Text = "Процесс генерации"
        Me.Label1.Visible = False
        '
        'button3
        '
        Me.button3.Location = New System.Drawing.Point(429, 15)
        Me.button3.Name = "button3"
        Me.button3.Size = New System.Drawing.Size(66, 22)
        Me.button3.TabIndex = 66
        Me.button3.Text = "..."
        '
        'textBoxOutPutFolder
        '
        Me.textBoxOutPutFolder.Location = New System.Drawing.Point(146, 15)
        Me.textBoxOutPutFolder.Name = "textBoxOutPutFolder"
        Me.textBoxOutPutFolder.Size = New System.Drawing.Size(277, 20)
        Me.textBoxOutPutFolder.TabIndex = 65
        Me.textBoxOutPutFolder.Text = "d:\BP3\OUT\Agency\"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(11, 18)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(115, 13)
        Me.Label3.TabIndex = 64
        Me.Label3.Text = "Projects Output Folder:"
        '
        'chkObjType
        '
        Me.chkObjType.FormattingEnabled = True
        Me.chkObjType.Location = New System.Drawing.Point(12, 236)
        Me.chkObjType.Name = "chkObjType"
        Me.chkObjType.Size = New System.Drawing.Size(483, 94)
        Me.chkObjType.Sorted = True
        Me.chkObjType.TabIndex = 63
        '
        'cmdGen
        '
        Me.cmdGen.Location = New System.Drawing.Point(429, 336)
        Me.cmdGen.Name = "cmdGen"
        Me.cmdGen.Size = New System.Drawing.Size(66, 26)
        Me.cmdGen.TabIndex = 62
        Me.cmdGen.Text = "Generate"
        Me.cmdGen.UseVisualStyleBackColor = True
        '
        'folderBrowserDialogProjectOutput
        '
        Me.folderBrowserDialogProjectOutput.SelectedPath = "C:\LATIR2\Generated\"
        '
        'cmbTheme
        '
        Me.cmbTheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTheme.FormattingEnabled = True
        Me.cmbTheme.Items.AddRange(New Object() {"Aqua  ", "BlueNote  ", "Capuccino  ", "Caravan  ", "Clear  ", "Cobalt  ", "Coffee  ", "DeepWater  ", "EasyStreet  ", "Facet  ", "FoggyDay  ", "Folio  ", "GiantSteps  ", "Graphite  ", "Gray  ", "HappyPumpkin ", "InLine  ", "Knockout ", "LightWalk  ", "Mailbox ", "Multipads ", "Nappa ", "Noise  ", "NoMarks  ", "Olive  ", "Purple  ", "Python  ", "RockIt  ", "Salad  ", "Snow  ", "Somatic  ", "Square  ", "StormyWeather  ", "Sulfur"})
        Me.cmbTheme.Location = New System.Drawing.Point(152, 172)
        Me.cmbTheme.Name = "cmbTheme"
        Me.cmbTheme.Size = New System.Drawing.Size(343, 21)
        Me.cmbTheme.TabIndex = 72
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(20, 171)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(34, 13)
        Me.Label4.TabIndex = 73
        Me.Label4.Text = "Тема"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(9, 88)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(130, 13)
        Me.Label5.TabIndex = 74
        Me.Label5.Text = "Шаблон формы запроса"
        '
        'txtTemplate
        '
        Me.txtTemplate.Location = New System.Drawing.Point(152, 85)
        Me.txtTemplate.Name = "txtTemplate"
        Me.txtTemplate.Size = New System.Drawing.Size(343, 20)
        Me.txtTemplate.TabIndex = 75
        Me.txtTemplate.Text = "d:\BP3\blocks\template%TYPE%.htm"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(9, 66)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(61, 13)
        Me.Label6.TabIndex = 76
        Me.Label6.Text = "Шаблон JS"
        '
        'txtTemplateJS
        '
        Me.txtTemplateJS.Location = New System.Drawing.Point(152, 59)
        Me.txtTemplateJS.Name = "txtTemplateJS"
        Me.txtTemplateJS.Size = New System.Drawing.Size(342, 20)
        Me.txtTemplateJS.TabIndex = 77
        Me.txtTemplateJS.Text = "d:\BP3\blocks\template.js"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(9, 114)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(92, 13)
        Me.Label7.TabIndex = 78
        Me.Label7.Text = "Шаблон таблицы"
        '
        'txtTabTemplate
        '
        Me.txtTabTemplate.Location = New System.Drawing.Point(152, 111)
        Me.txtTabTemplate.Name = "txtTabTemplate"
        Me.txtTabTemplate.Size = New System.Drawing.Size(342, 20)
        Me.txtTabTemplate.TabIndex = 79
        Me.txtTabTemplate.Text = "d:\BP3\blocks\tabtemplate%TYPE%.php"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(9, 142)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(95, 13)
        Me.Label8.TabIndex = 80
        Me.Label8.Text = "Шаблон карточки"
        '
        'txtCardTemplate
        '
        Me.txtCardTemplate.Location = New System.Drawing.Point(151, 139)
        Me.txtCardTemplate.Name = "txtCardTemplate"
        Me.txtCardTemplate.Size = New System.Drawing.Size(344, 20)
        Me.txtCardTemplate.TabIndex = 81
        Me.txtCardTemplate.Text = "d:\BP3\blocks\cardtemplate%TYPE%.php"
        '
        'frmB2Sercher
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(512, 397)
        Me.Controls.Add(Me.txtCardTemplate)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtTabTemplate)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtTemplateJS)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtTemplate)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cmbTheme)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmdClearAll)
        Me.Controls.Add(Me.cmdSelectAll)
        Me.Controls.Add(Me.pb)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.button3)
        Me.Controls.Add(Me.textBoxOutPutFolder)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.chkObjType)
        Me.Controls.Add(Me.cmdGen)
        Me.Name = "frmB2Sercher"
        Me.Text = "Генератор поисковых форм"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmdClearAll As System.Windows.Forms.Button
    Friend WithEvents cmdSelectAll As System.Windows.Forms.Button
    Public WithEvents pb As System.Windows.Forms.ProgressBar
    Public WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents button3 As System.Windows.Forms.Button
    Friend WithEvents textBoxOutPutFolder As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents chkObjType As System.Windows.Forms.CheckedListBox
    Friend WithEvents cmdGen As System.Windows.Forms.Button
    Friend WithEvents folderBrowserDialogProjectOutput As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents cmbTheme As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtTemplate As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtTemplateJS As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtTabTemplate As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtCardTemplate As System.Windows.Forms.TextBox
End Class
