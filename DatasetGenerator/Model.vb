Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary

<Serializable>
Public Class TaskModel
	Implements IReadOnlyCollection(Of Task)

	Private Shared _Serializer As New BinaryFormatter

	Private _List As New List(Of Task)
	Private _Folder As String

	Public ReadOnly Property DataFolder As String
		Get
			Return _Folder
		End Get
	End Property
	Public Property TaskID As Integer
	Public ReadOnly Property SelectedTask As Task
		Get
			Return Me(TaskID)
		End Get
	End Property
	Public ReadOnly Property Count As Integer Implements IReadOnlyCollection(Of Task).Count
		Get
			Return _List.Count
		End Get
	End Property

	Private Sub AddTask(t As Task)
		_List.Add(t)
	End Sub
	Public Sub Save(filePath As String)
		Using fx = File.OpenWrite(filePath)
			_Serializer.Serialize(fx, Me)
		End Using
	End Sub

	Public Sub ExportCSV(fileName As String)
		Using fx = New StreamWriter(fileName)

			For Each task In Me
				fx.Write(task.FileName)
				fx.Write(","c)

				fx.Write(task.Type)
				fx.Write(","c)

				Select Case task.Type
					Case TaskTypes.CarRect
						'Write Polygon task
						WritePoly(fx, task)
					Case TaskTypes.Company
						'Write Company
						fx.Write(task.As(Of CompanyTask)().Value)
					Case TaskTypes.PlateRect
						'Write Poly
						WritePoly(fx, task)
					Case TaskTypes.SelectColor
						Dim tx = task.As(Of ColorTask)().Color
						'Write RGB
						fx.Write(tx.R)
						fx.Write(","c)
						fx.Write(tx.G)
						fx.Write(","c)
						fx.Write(tx.B)
					Case TaskTypes.VType
						'Write Vehicle Type
						fx.Write(task.As(Of VehicleTypeTask)().Value)
				End Select

				fx.WriteLine()
			Next

		End Using

	End Sub

	Private Shared Sub WritePoly(fx As StreamWriter, task As Task)
		Dim tax = task.As(Of PolygonTask)()
		fx.Write(tax.Points.Count)
		For Each point In tax.Points
			fx.Write(","c)
			fx.Write(point.X)

			fx.Write(","c)
			fx.Write(point.Y)
		Next
	End Sub

	Public Sub Export(fileName As String)
		Using fx = New BinaryWriter(File.OpenWrite(fileName))
			'Write Signature
			fx.Write(CUShort(&HDAFA))
			'Write No of Elements
			fx.Write(CShort(Count))
			'Write All Tasks
			For Each task In Me
				'Write Filename
				WriteString(fx, task.FileName)
				'Write Task type
				fx.Write(task.Type)
				'Write Extra Data
				Select Case task.Type
					Case TaskTypes.CarRect
						'Write Polygon task
						WritePoly(fx, task)
					Case TaskTypes.Company
						'Write Company
						fx.Write(task.As(Of CompanyTask)().Value)
					Case TaskTypes.PlateRect
						'Write Poly
						WritePoly(fx, task)
					Case TaskTypes.SelectColor
						Dim tx = task.As(Of ColorTask)().Color
						'Write RGB
						fx.Write(tx.R)
						fx.Write(tx.G)
						fx.Write(tx.B)
					Case TaskTypes.VType
						'Write Vehicle Type
						fx.Write(task.As(Of VehicleTypeTask)().Value)
				End Select
			Next
		End Using
	End Sub
	Private Shared Sub WriteString(fx As BinaryWriter, text As String)
		'Write Filename Length
		fx.Write(CByte(text.Length))
		'Write Filename
		For Each ch In text
			fx.Write(CByte(AscW(ch)))
		Next
	End Sub
	Private Shared Sub WritePoly(fx As BinaryWriter, task As Task)
		Dim tx = task.As(Of PolygonTask)()
		'Write Point Count
		fx.Write(CByte(tx.Points.Count))
		'Write all points
		For Each point In tx.Points
			'Write X
			fx.Write(point.X)
			'Write Y
			fx.Write(point.Y)
		Next
	End Sub

	Public Shared Function Load(filePath As String) As TaskModel
		Using fx = File.OpenRead(filePath)
			Load = _Serializer.Deserialize(fx)
			Load._Folder = Path.GetDirectoryName(filePath)
		End Using
	End Function
	Public Shared Function Create(inputFolder As String, outputFolder As String) As TaskModel
		Dim id As Integer = 0
		Dim outFile As String
		Dim model As New TaskModel With {._Folder = outputFolder}

		For Each fx In Directory.GetFiles(inputFolder, "*.*").Where(
			Function(s) (s.ToLower().EndsWith(".png") Or s.ToLower.EndsWith(".jpg")))

			outFile = id.ToString("D3") + ".jpg"
			File.Copy(fx, Path.Combine(outputFolder, outFile), True)
			model.AddTask(New ColorTask With {.FileName = outFile})
			model.AddTask(New CarRectTask With {.FileName = outFile})
			model.AddTask(New PlateRectTask With {.FileName = outFile})
			model.AddTask(New CompanyTask With {.FileName = outFile})
			model.AddTask(New VehicleTypeTask With {.FileName = outFile})
			id += 1
		Next

		Return model
	End Function

	Public Function GetEnumerator() As IEnumerator(Of Task) Implements IEnumerable(Of Task).GetEnumerator
		Return _List.GetEnumerator
	End Function
	Private Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
		Return _List.GetEnumerator
	End Function

End Class
<Serializable>
Public MustInherit Class Task

	Public ReadOnly Property Type As TaskTypes
	Public Property FileName As String

	Public Sub New(type As TaskTypes)
		Me.Type = type
	End Sub
	Public Function [As](Of T As Task)() As T
		Return Me
	End Function

	Public ReadOnly Property Tag As String
		Get
			Return $"{Path.GetFileName(FileName)} - {Type.ToString}"
		End Get
	End Property
End Class
<Serializable>
Public Class ColorTask
	Inherits Task
	Public Property Color As Color
	Public Sub New()
		MyBase.New(TaskTypes.SelectColor)
	End Sub
End Class
<Serializable>
Public Class PolygonTask
	Inherits Task
	Public ReadOnly Property Points As New List(Of Point)
	Public Sub New(type As TaskTypes)
		MyBase.New(type)
	End Sub
End Class
<Serializable>
Public Class CarRectTask
	Inherits PolygonTask

	Public Sub New()
		MyBase.New(TaskTypes.CarRect)
	End Sub
End Class
<Serializable>
Public Class PlateRectTask
	Inherits PolygonTask

	Public Sub New()
		MyBase.New(TaskTypes.PlateRect)
	End Sub
End Class
<Serializable>
Public Class CompanyTask
	Inherits Task

	Public Property Value As CompanyTypes

	Public Sub New()
		MyBase.New(TaskTypes.Company)
	End Sub
End Class
<Serializable>
Public Class VehicleTypeTask
	Inherits Task

	Public Property Value As VehicleTypes

	Public Sub New()
		MyBase.New(TaskTypes.VType)
	End Sub
End Class
<Serializable>
Public Enum TaskTypes As Byte
	SelectColor = 0
	PlateRect = 1
	CarRect = 2
	Company = 3
	VType = 4
End Enum
<Serializable>
Public Enum CompanyTypes As Byte
	Honda = 0
End Enum
<Serializable>
Public Enum VehicleTypes As Byte
	Car = 0
End Enum
