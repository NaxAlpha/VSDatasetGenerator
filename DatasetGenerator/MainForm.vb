Imports System.IO

Public Class MainForm
	Inherits MetroFramework.Forms.MetroForm
	Friend WithEvents imgView As PictureBox
	Friend WithEvents lstTasks As ListBox
	Friend WithEvents lblTasks As MetroFramework.Controls.MetroLabel
	Friend WithEvents cmbVehType As MetroFramework.Controls.MetroComboBox
	Friend WithEvents cmbCompany As MetroFramework.Controls.MetroComboBox
	Friend WithEvents lblVehType As MetroFramework.Controls.MetroLabel
	Friend WithEvents lblCompany As MetroFramework.Controls.MetroLabel
	Private components As System.ComponentModel.IContainer
	Friend WithEvents lblInfo As MetroFramework.Drawing.Html.HtmlLabel
	Friend WithEvents btnExport As MetroFramework.Controls.MetroButton
	Friend WithEvents btnSave As MetroFramework.Controls.MetroButton
	Friend WithEvents prgTotalProgress As ProgressBar

	Public Sub New(model As TaskModel)
		InitializeComponent()
		Me.Model = model
		InitializeSystem()
	End Sub

	Public Sub New()
		InitializeComponent()
		Me.Model = TaskModel.Create("C:\Users\Nauman.DESKTOP-15N19V9\Desktop\Gaber",
									 "C:\Users\Nauman.DESKTOP-15N19V9\Desktop\Tst_Out")
		InitializeSystem()
	End Sub

	Private Sub InitializeSystem()
		Directory.SetCurrentDirectory(Model.DataFolder)

		lstTasks.Items.AddRange(Model.ToArray())
		If lstTasks.Items.Count > 0 Then
			lstTasks.SelectedIndex = Model.TaskID
		End If

		cmbVehType.Items.AddRange([Enum].GetNames(GetType(VehicleTypes)))
		cmbVehType.SelectedIndex = 0
		cmbCompany.Items.AddRange([Enum].GetNames(GetType(CompanyTypes)))
		cmbCompany.SelectedIndex = 0
	End Sub

	Private Sub InitializeComponent()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
		Me.prgTotalProgress = New System.Windows.Forms.ProgressBar()
		Me.imgView = New System.Windows.Forms.PictureBox()
		Me.lstTasks = New System.Windows.Forms.ListBox()
		Me.lblTasks = New MetroFramework.Controls.MetroLabel()
		Me.cmbVehType = New MetroFramework.Controls.MetroComboBox()
		Me.cmbCompany = New MetroFramework.Controls.MetroComboBox()
		Me.lblVehType = New MetroFramework.Controls.MetroLabel()
		Me.lblCompany = New MetroFramework.Controls.MetroLabel()
		Me.lblInfo = New MetroFramework.Drawing.Html.HtmlLabel()
		Me.btnExport = New MetroFramework.Controls.MetroButton()
		Me.btnSave = New MetroFramework.Controls.MetroButton()
		CType(Me.imgView, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'prgTotalProgress
		'
		Me.prgTotalProgress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.prgTotalProgress.Location = New System.Drawing.Point(21, 425)
		Me.prgTotalProgress.Name = "prgTotalProgress"
		Me.prgTotalProgress.Size = New System.Drawing.Size(708, 20)
		Me.prgTotalProgress.TabIndex = 1
		Me.prgTotalProgress.Value = 50
		'
		'imgView
		'
		Me.imgView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.imgView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.imgView.Location = New System.Drawing.Point(286, 117)
		Me.imgView.Name = "imgView"
		Me.imgView.Size = New System.Drawing.Size(443, 267)
		Me.imgView.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
		Me.imgView.TabIndex = 5
		Me.imgView.TabStop = False
		'
		'lstTasks
		'
		Me.lstTasks.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.lstTasks.DisplayMember = "Tag"
		Me.lstTasks.Font = New System.Drawing.Font("Open Sans", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lstTasks.FormattingEnabled = True
		Me.lstTasks.ItemHeight = 22
		Me.lstTasks.Location = New System.Drawing.Point(21, 85)
		Me.lstTasks.Name = "lstTasks"
		Me.lstTasks.Size = New System.Drawing.Size(259, 334)
		Me.lstTasks.TabIndex = 6
		'
		'lblTasks
		'
		Me.lblTasks.AutoSize = True
		Me.lblTasks.Location = New System.Drawing.Point(23, 63)
		Me.lblTasks.Name = "lblTasks"
		Me.lblTasks.Size = New System.Drawing.Size(37, 19)
		Me.lblTasks.TabIndex = 7
		Me.lblTasks.Text = "Tasks"
		'
		'cmbVehType
		'
		Me.cmbVehType.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmbVehType.FormattingEnabled = True
		Me.cmbVehType.ItemHeight = 23
		Me.cmbVehType.Location = New System.Drawing.Point(375, 390)
		Me.cmbVehType.Name = "cmbVehType"
		Me.cmbVehType.Size = New System.Drawing.Size(141, 29)
		Me.cmbVehType.TabIndex = 8
		Me.cmbVehType.UseSelectable = True
		'
		'cmbCompany
		'
		Me.cmbCompany.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmbCompany.FormattingEnabled = True
		Me.cmbCompany.ItemHeight = 23
		Me.cmbCompany.Location = New System.Drawing.Point(597, 390)
		Me.cmbCompany.Name = "cmbCompany"
		Me.cmbCompany.Size = New System.Drawing.Size(132, 29)
		Me.cmbCompany.TabIndex = 9
		Me.cmbCompany.UseSelectable = True
		'
		'lblVehType
		'
		Me.lblVehType.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lblVehType.AutoSize = True
		Me.lblVehType.Location = New System.Drawing.Point(286, 390)
		Me.lblVehType.Name = "lblVehType"
		Me.lblVehType.Size = New System.Drawing.Size(83, 19)
		Me.lblVehType.TabIndex = 10
		Me.lblVehType.Text = "Vehicle Type:"
		'
		'lblCompany
		'
		Me.lblCompany.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lblCompany.AutoSize = True
		Me.lblCompany.Location = New System.Drawing.Point(522, 390)
		Me.lblCompany.Name = "lblCompany"
		Me.lblCompany.Size = New System.Drawing.Size(69, 19)
		Me.lblCompany.TabIndex = 11
		Me.lblCompany.Text = "Company:"
		'
		'lblInfo
		'
		Me.lblInfo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lblInfo.AutoScroll = True
		Me.lblInfo.AutoScrollMinSize = New System.Drawing.Size(28, 23)
		Me.lblInfo.AutoSize = False
		Me.lblInfo.BackColor = System.Drawing.SystemColors.Window
		Me.lblInfo.Location = New System.Drawing.Point(286, 85)
		Me.lblInfo.Name = "lblInfo"
		Me.lblInfo.Size = New System.Drawing.Size(277, 26)
		Me.lblInfo.TabIndex = 12
		Me.lblInfo.Text = "Info"
		'
		'btnExport
		'
		Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btnExport.Location = New System.Drawing.Point(569, 85)
		Me.btnExport.Name = "btnExport"
		Me.btnExport.Size = New System.Drawing.Size(77, 26)
		Me.btnExport.TabIndex = 13
		Me.btnExport.Text = "Export"
		Me.btnExport.UseSelectable = True
		'
		'btnSave
		'
		Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btnSave.Location = New System.Drawing.Point(652, 85)
		Me.btnSave.Name = "btnSave"
		Me.btnSave.Size = New System.Drawing.Size(77, 26)
		Me.btnSave.TabIndex = 14
		Me.btnSave.Text = "Save"
		Me.btnSave.UseSelectable = True
		'
		'MainForm
		'
		Me.ClientSize = New System.Drawing.Size(752, 468)
		Me.Controls.Add(Me.btnSave)
		Me.Controls.Add(Me.btnExport)
		Me.Controls.Add(Me.lblInfo)
		Me.Controls.Add(Me.lblCompany)
		Me.Controls.Add(Me.lblVehType)
		Me.Controls.Add(Me.cmbCompany)
		Me.Controls.Add(Me.cmbVehType)
		Me.Controls.Add(Me.lblTasks)
		Me.Controls.Add(Me.lstTasks)
		Me.Controls.Add(Me.imgView)
		Me.Controls.Add(Me.prgTotalProgress)
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.Name = "MainForm"
		Me.Text = "Vehicle Dataset Generator"
		CType(Me.imgView, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Private Sub lstTasks_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstTasks.SelectedIndexChanged
		Model.TaskID = lstTasks.SelectedIndex
		Dim task = Model.SelectedTask

		imgView.Image?.Dispose()
		imgView.Image = Image.FromFile(task.FileName)

		Select Case task.Type
			Case TaskTypes.CarRect
				lblInfo.Text = "Draw Rectangle on Vehicle"
				cmbCompany.Enabled = False
				cmbVehType.Enabled = False

			Case TaskTypes.Company
				lblInfo.Text = "Choose Vehicle Company"
				cmbCompany.Enabled = True
				cmbVehType.Enabled = False
				cmbVehType.Text = task.As(Of CompanyTask)().Value.ToString()

			Case TaskTypes.PlateRect
				lblInfo.Text = "Draw Plate Rectangle"
				cmbCompany.Enabled = False
				cmbVehType.Enabled = False

			Case TaskTypes.SelectColor
				lblInfo.Text = "Select Color on Car"
				cmbCompany.Enabled = False
				cmbVehType.Enabled = False
				lblInfo.BackColor = task.As(Of ColorTask)().Color

			Case TaskTypes.VType
				lblInfo.Text = "Choose Vehicle Type"
				cmbCompany.Enabled = False
				cmbVehType.Enabled = True
				cmbVehType.Text = task.As(Of VehicleTypeTask)().Value.ToString()

		End Select

		prgTotalProgress.Value = 100 * (Model.TaskID + 1) / Model.Count
	End Sub

	Private Sub cmbCompany_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCompany.SelectedIndexChanged
		If Model.SelectedTask.Type = TaskTypes.Company Then
			Dim text = cmbCompany.SelectedItem
			Dim value = [Enum].Parse(GetType(CompanyTypes), text)
			Model.SelectedTask.As(Of CompanyTask).Value = value
			MoveForward()
		End If
	End Sub

	Private Sub cmbVehType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbVehType.SelectedIndexChanged
		If Model.SelectedTask.Type = TaskTypes.VType Then
			Dim text = cmbVehType.SelectedItem
			Dim value = [Enum].Parse(GetType(VehicleTypes), text)
			Model.SelectedTask.As(Of VehicleTypeTask).Value = value
			MoveForward()
		End If
	End Sub

	Private Sub MoveForward()
		If lstTasks.SelectedIndex = Model.Count - 1 Then
			lblInfo.Text = "Completed!"
		Else
			lstTasks.SelectedIndex += 1
		End If
	End Sub

	Private Sub imgView_Paint(sender As Object, e As PaintEventArgs) Handles imgView.Paint
		Dim pen = New Pen(Color.Red, 2)
		Dim res = New Size(10, 10)
		Dim rs2 = New Size(res.Width / 2, res.Height / 2)

		Select Case Model.SelectedTask.Type
			Case TaskTypes.SelectColor

				Dim point = imgView.PointToClient(MousePosition) - rs2
				Dim rect = New Rectangle(point, res)
				e.Graphics.DrawRectangle(pen, rect)

				Dim iRect = ViewRect2ImageRect(rect)
				Dim avgColor = ImageAverageColorAt(iRect)

				lblInfo.BackColor = avgColor

			Case TaskTypes.CarRect
				Dim task = Model.SelectedTask.As(Of PolygonTask)
				If task.Points.Count = 0 Then
					Exit Select
				End If

				For Each p As Point In task.Points
					e.Graphics.DrawEllipse(pen, New Rectangle(ImagePoint2ViewPoint(p) - rs2, res))
				Next

				If task.Points.Count > 1 Then
					e.Graphics.DrawPolygon(pen, task.Points.Select(Function(p) ImagePoint2ViewPoint(p)).ToArray)
				End If

			Case TaskTypes.PlateRect
				Dim task = Model.SelectedTask.As(Of PolygonTask)
				If task.Points.Count = 0 Then
					Exit Select
				End If

				For Each p As Point In task.Points
					e.Graphics.DrawEllipse(pen, New Rectangle(ImagePoint2ViewPoint(p) - rs2, res))
				Next

				If task.Points.Count > 1 Then
					e.Graphics.DrawPolygon(pen, task.Points.Select(Function(p) ImagePoint2ViewPoint(p)).ToArray)
				End If
		End Select
	End Sub

	Private Function ViewRect2ImageRect(rect As Rectangle) As Rectangle
		Dim r As New Rectangle
		r.X = imgView.Image.Width * rect.X / imgView.Width
		r.Y = imgView.Image.Height * rect.Y / imgView.Height
		r.Width = imgView.Image.Width * rect.Width / imgView.Width
		r.Height = imgView.Image.Height * rect.Height / imgView.Height
		Return r
	End Function

	Private Function ImageAverageColorAt(rect As Rectangle) As Color
		Dim bmp As Bitmap = imgView.Image
		Dim R As Integer = 0,
			G As Integer = 0,
			B As Integer = 0
		Dim pxl As Color

		Dim xMin = Math.Max(rect.X, 0)
		Dim yMin = Math.Max(rect.Y, 0)

		Dim xMax = Math.Min(rect.Right, bmp.Width - 1)
		Dim yMax = Math.Min(rect.Bottom, bmp.Height - 1)

		For j = yMin To yMax
			For i = xMin To xMax
				pxl = bmp.GetPixel(i, j)
				R += pxl.R
				G += pxl.G
				B += pxl.B
			Next
		Next

		Dim area = (yMax - yMin + 1) * (xMax - xMin + 1)

		If area = 0 Then
			Return Color.Black
		End If

		Return Color.FromArgb(R / area, G / area, B / area)
	End Function

	Private Sub imgView_MouseClick(sender As Object, e As MouseEventArgs) Handles imgView.MouseClick
		If e.Button = MouseButtons.Left Then
			Select Case Model.SelectedTask.Type
				Case TaskTypes.SelectColor
					Model.SelectedTask.As(Of ColorTask).Color = lblInfo.BackColor
					lblInfo.BackColor = Color.White
					MoveForward()
				Case TaskTypes.CarRect
					Model.SelectedTask.As(Of PolygonTask).Points.Add(ViewPoint2ImagePoint(e.Location))
					imgView.Invalidate()
				Case TaskTypes.PlateRect
					Model.SelectedTask.As(Of PolygonTask).Points.Add(ViewPoint2ImagePoint(e.Location))
					imgView.Invalidate()
			End Select
		ElseIf e.Button = MouseButtons.Right Then
			If Model.SelectedTask.Type = TaskTypes.CarRect OrElse
				Model.SelectedTask.Type = TaskTypes.PlateRect Then
				MoveForward()
			End If
		End If
	End Sub

	Public Function ViewPoint2ImagePoint(point As Point) As Point
		Dim p As New Point
		p.X = imgView.Image.Width * point.X / imgView.Width
		p.Y = imgView.Image.Height * point.Y / imgView.Height
		Return p
	End Function

	Public Function ImagePoint2ViewPoint(point As Point) As Point
		Dim p As New Point
		p.X = imgView.Width * point.X / imgView.Image.Width
		p.Y = imgView.Height * point.Y / imgView.Image.Height
		Return p
	End Function

	Private Sub imgView_MouseMove(sender As Object, e As MouseEventArgs) Handles imgView.MouseMove
		If Model.SelectedTask.Type = TaskTypes.SelectColor Then
			imgView.Invalidate()
		End If
	End Sub

	Private Sub MainForm_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown, imgView.KeyDown, lstTasks.KeyDown, cmbCompany.KeyDown, cmbVehType.KeyDown
		If e.KeyCode = Keys.Escape AndAlso
			(Model.SelectedTask.Type = TaskTypes.PlateRect OrElse
			Model.SelectedTask.Type = TaskTypes.CarRect) Then
			Model.SelectedTask.As(Of PolygonTask).Points.Clear()
			imgView.Invalidate()
		End If
	End Sub

	Public ReadOnly Property Model As TaskModel

	Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
		Using sfd As New SaveFileDialog
			If sfd.ShowDialog() = DialogResult.OK Then
				Model.Save(sfd.FileName)
			End If
		End Using
	End Sub

	Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
		Using ofd As New SaveFileDialog
			If ofd.ShowDialog() = DialogResult.OK Then
				Model.ExportCSV(ofd.FileName)
			End If
		End Using
	End Sub

End Class
