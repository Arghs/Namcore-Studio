<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Quests_interface
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.qst_lst = New System.Windows.Forms.ListView()
        Me.qstid = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.qstname = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.finished = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.rewarded = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.SuspendLayout()
        '
        'qst_lst
        '
        Me.qst_lst.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.qstid, Me.qstname, Me.finished, Me.rewarded})
        Me.qst_lst.Location = New System.Drawing.Point(12, 12)
        Me.qst_lst.Name = "qst_lst"
        Me.qst_lst.Size = New System.Drawing.Size(467, 422)
        Me.qst_lst.TabIndex = 0
        Me.qst_lst.UseCompatibleStateImageBehavior = False
        Me.qst_lst.View = System.Windows.Forms.View.Details
        '
        'qstid
        '
        Me.qstid.Text = "Quest ID"
        Me.qstid.Width = 67
        '
        'qstname
        '
        Me.qstname.Text = "Name"
        Me.qstname.Width = 200
        '
        'finished
        '
        Me.finished.Text = "Finished"
        '
        'rewarded
        '
        Me.rewarded.Text = "Rewarded"
        Me.rewarded.Width = 70
        '
        'Quests_interface
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(494, 446)
        Me.Controls.Add(Me.qst_lst)
        Me.Name = "Quests_interface"
        Me.Text = "Quests_interface"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents qst_lst As System.Windows.Forms.ListView
    Friend WithEvents qstid As System.Windows.Forms.ColumnHeader
    Friend WithEvents qstname As System.Windows.Forms.ColumnHeader
    Friend WithEvents finished As System.Windows.Forms.ColumnHeader
    Friend WithEvents rewarded As System.Windows.Forms.ColumnHeader
End Class
