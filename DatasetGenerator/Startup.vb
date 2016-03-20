Imports System.IO
Imports MetroFramework.Forms

Public Class Startup
	Inherits MetroForm

	Friend WithEvents tilNew As MetroFramework.Controls.MetroTile
	Friend WithEvents btnSourceSelect As MetroFramework.Controls.MetroButton
	Friend WithEvents txtSourcePath As MetroFramework.Controls.MetroTextBox
	Friend WithEvents tilLoad As MetroFramework.Controls.MetroTile

	Public Sub New()
		InitializeComponent()
	End Sub

	Private Sub InitializeComponent()
		Me.tilNew = New MetroFramework.Controls.MetroTile()
		Me.tilLoad = New MetroFramework.Controls.MetroTile()
		Me.btnSourceSelect = New MetroFramework.Controls.MetroButton()
		Me.txtSourcePath = New MetroFramework.Controls.MetroTextBox()
		Me.SuspendLayout()
		'
		'tilNew
		'
		Me.tilNew.ActiveControl = Nothing
		Me.tilNew.Location = New System.Drawing.Point(23, 98)
		Me.tilNew.Name = "tilNew"
		Me.tilNew.Size = New System.Drawing.Size(348, 88)
		Me.tilNew.TabIndex = 1
		Me.tilNew.Text = "New Project"
		Me.tilNew.UseSelectable = True
		'
		'tilLoad
		'
		Me.tilLoad.ActiveControl = Nothing
		Me.tilLoad.Location = New System.Drawing.Point(377, 63)
		Me.tilLoad.Name = "tilLoad"
		Me.tilLoad.Size = New System.Drawing.Size(154, 123)
		Me.tilLoad.TabIndex = 2
		Me.tilLoad.Text = "Load Project"
		Me.tilLoad.UseSelectable = True
		'
		'btnSourceSelect
		'
		Me.btnSourceSelect.Location = New System.Drawing.Point(233, 63)
		Me.btnSourceSelect.Name = "btnSourceSelect"
		Me.btnSourceSelect.Size = New System.Drawing.Size(138, 29)
		Me.btnSourceSelect.TabIndex = 3
		Me.btnSourceSelect.Text = "New Project Source"
		Me.btnSourceSelect.UseSelectable = True
		'
		'txtSourcePath
		'
		'
		'
		'
		Me.txtSourcePath.CustomButton.Image = Nothing
		Me.txtSourcePath.CustomButton.Location = New System.Drawing.Point(184, 2)
		Me.txtSourcePath.CustomButton.Name = ""
		Me.txtSourcePath.CustomButton.Size = New System.Drawing.Size(17, 17)
		Me.txtSourcePath.CustomButton.Style = MetroFramework.MetroColorStyle.Blue
		Me.txtSourcePath.CustomButton.TabIndex = 1
		Me.txtSourcePath.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light
		Me.txtSourcePath.CustomButton.UseSelectable = True
		Me.txtSourcePath.CustomButton.Visible = False
		Me.txtSourcePath.Lines = New String(-1) {}
		Me.txtSourcePath.Location = New System.Drawing.Point(23, 70)
		Me.txtSourcePath.MaxLength = 32767
		Me.txtSourcePath.Name = "txtSourcePath"
		Me.txtSourcePath.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
		Me.txtSourcePath.ReadOnly = True
		Me.txtSourcePath.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtSourcePath.SelectedText = ""
		Me.txtSourcePath.SelectionLength = 0
		Me.txtSourcePath.SelectionStart = 0
		Me.txtSourcePath.Size = New System.Drawing.Size(204, 22)
		Me.txtSourcePath.TabIndex = 4
		Me.txtSourcePath.UseSelectable = True
		Me.txtSourcePath.WaterMarkColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
		Me.txtSourcePath.WaterMarkFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel)
		'
		'Startup
		'
		Me.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle
		Me.ClientSize = New System.Drawing.Size(554, 209)
		Me.Controls.Add(Me.txtSourcePath)
		Me.Controls.Add(Me.btnSourceSelect)
		Me.Controls.Add(Me.tilLoad)
		Me.Controls.Add(Me.tilNew)
		Me.MaximizeBox = False
		Me.Name = "Startup"
		Me.ShowIcon = False
		Me.Text = "Dataset Generator"
		Me.ResumeLayout(False)

	End Sub

	Private Sub btnSourceSelect_Click(sender As Object, e As EventArgs) Handles btnSourceSelect.Click
		Using ofd As New FolderBrowserDialog
			If ofd.ShowDialog() = DialogResult.OK Then
				txtSourcePath.Text = ofd.SelectedPath
			End If
		End Using
	End Sub

	Private Sub tilNew_Click(sender As Object, e As EventArgs) Handles tilNew.Click
		If Not Directory.Exists(txtSourcePath.Text) Then
			MsgBox("Selected Source File is invalid.", MsgBoxStyle.Critical)
			Return
		End If
		Using sfd As New FolderBrowserDialog
			If sfd.ShowDialog() = DialogResult.OK AndAlso
				Directory.Exists(sfd.SelectedPath) Then
				Dim model = TaskModel.Create(txtSourcePath.Text, sfd.SelectedPath)
				RunMain(model)
			End If
		End Using
	End Sub

	Private Sub RunMain(model As TaskModel)
		Using mf As New MainForm(model)
			Me.Hide()
			mf.ShowDialog()
			Me.Close()
		End Using
	End Sub

	Private Sub tilLoad_Click(sender As Object, e As EventArgs) Handles tilLoad.Click
		Using ofd As New OpenFileDialog
			If ofd.ShowDialog() = DialogResult.OK AndAlso
				File.Exists(ofd.FileName) Then
				Dim model = TaskModel.Load(ofd.FileName)
				RunMain(model)
			End If
		End Using
	End Sub

End Class
