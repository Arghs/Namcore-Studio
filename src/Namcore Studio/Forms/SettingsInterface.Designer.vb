Imports NamCore_Studio.Forms.Extension

Namespace Forms
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class SettingsInterface
        Inherits EventTrigger

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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SettingsInterface))
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.password_tb = New System.Windows.Forms.TextBox()
            Me.Label4 = New System.Windows.Forms.Label()
            Me.username_tb = New System.Windows.Forms.TextBox()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.defcred_cb = New System.Windows.Forms.CheckBox()
            Me.port_ud = New System.Windows.Forms.NumericUpDown()
            Me.manualproxy_radio = New System.Windows.Forms.RadioButton()
            Me.detectproxy_radio = New System.Windows.Forms.RadioButton()
            Me.noproxy_radio = New System.Windows.Forms.RadioButton()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.url_tb = New System.Windows.Forms.TextBox()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.apply_bt = New System.Windows.Forms.Button()
            Me.close_bt = New System.Windows.Forms.Button()
            Me.GroupBox1.SuspendLayout()
            CType(Me.port_ud, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'GroupBox1
            '
            Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
            Me.GroupBox1.Controls.Add(Me.password_tb)
            Me.GroupBox1.Controls.Add(Me.Label4)
            Me.GroupBox1.Controls.Add(Me.username_tb)
            Me.GroupBox1.Controls.Add(Me.Label3)
            Me.GroupBox1.Controls.Add(Me.defcred_cb)
            Me.GroupBox1.Controls.Add(Me.port_ud)
            Me.GroupBox1.Controls.Add(Me.manualproxy_radio)
            Me.GroupBox1.Controls.Add(Me.detectproxy_radio)
            Me.GroupBox1.Controls.Add(Me.noproxy_radio)
            Me.GroupBox1.Controls.Add(Me.Label2)
            Me.GroupBox1.Controls.Add(Me.url_tb)
            Me.GroupBox1.Controls.Add(Me.Label1)
            Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.GroupBox1.Location = New System.Drawing.Point(12, 85)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(426, 220)
            Me.GroupBox1.TabIndex = 0
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Proxy settings"
            '
            'password_tb
            '
            Me.password_tb.Enabled = False
            Me.password_tb.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.password_tb.Location = New System.Drawing.Point(111, 183)
            Me.password_tb.Name = "password_tb"
            Me.password_tb.Size = New System.Drawing.Size(185, 21)
            Me.password_tb.TabIndex = 15
            '
            'Label4
            '
            Me.Label4.AutoSize = True
            Me.Label4.Enabled = False
            Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label4.Location = New System.Drawing.Point(41, 186)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(64, 15)
            Me.Label4.TabIndex = 14
            Me.Label4.Text = "Password:"
            '
            'username_tb
            '
            Me.username_tb.Enabled = False
            Me.username_tb.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.username_tb.Location = New System.Drawing.Point(111, 158)
            Me.username_tb.Name = "username_tb"
            Me.username_tb.Size = New System.Drawing.Size(185, 21)
            Me.username_tb.TabIndex = 13
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Enabled = False
            Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label3.Location = New System.Drawing.Point(41, 161)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(68, 15)
            Me.Label3.TabIndex = 12
            Me.Label3.Text = "Username:"
            '
            'defcred_cb
            '
            Me.defcred_cb.AutoSize = True
            Me.defcred_cb.Checked = True
            Me.defcred_cb.CheckState = System.Windows.Forms.CheckState.Checked
            Me.defcred_cb.Enabled = False
            Me.defcred_cb.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.defcred_cb.Location = New System.Drawing.Point(33, 137)
            Me.defcred_cb.Name = "defcred_cb"
            Me.defcred_cb.Size = New System.Drawing.Size(151, 19)
            Me.defcred_cb.TabIndex = 11
            Me.defcred_cb.Text = "Use default credentials"
            Me.defcred_cb.UseVisualStyleBackColor = True
            '
            'port_ud
            '
            Me.port_ud.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.port_ud.Location = New System.Drawing.Point(340, 111)
            Me.port_ud.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
            Me.port_ud.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
            Me.port_ud.Name = "port_ud"
            Me.port_ud.Size = New System.Drawing.Size(74, 21)
            Me.port_ud.TabIndex = 10
            Me.port_ud.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
            Me.port_ud.Value = New Decimal(New Integer() {80, 0, 0, 0})
            '
            'manualproxy_radio
            '
            Me.manualproxy_radio.AutoSize = True
            Me.manualproxy_radio.Location = New System.Drawing.Point(15, 80)
            Me.manualproxy_radio.Name = "manualproxy_radio"
            Me.manualproxy_radio.Size = New System.Drawing.Size(211, 20)
            Me.manualproxy_radio.TabIndex = 9
            Me.manualproxy_radio.Text = "Manual proxy configuration"
            Me.manualproxy_radio.UseVisualStyleBackColor = True
            '
            'detectproxy_radio
            '
            Me.detectproxy_radio.AutoSize = True
            Me.detectproxy_radio.Location = New System.Drawing.Point(15, 54)
            Me.detectproxy_radio.Name = "detectproxy_radio"
            Me.detectproxy_radio.Size = New System.Drawing.Size(374, 20)
            Me.detectproxy_radio.TabIndex = 8
            Me.detectproxy_radio.Text = "Detect proxy settings automatically for this network"
            Me.detectproxy_radio.UseVisualStyleBackColor = True
            '
            'noproxy_radio
            '
            Me.noproxy_radio.AutoSize = True
            Me.noproxy_radio.Checked = True
            Me.noproxy_radio.Location = New System.Drawing.Point(15, 28)
            Me.noproxy_radio.Name = "noproxy_radio"
            Me.noproxy_radio.Size = New System.Drawing.Size(88, 20)
            Me.noproxy_radio.TabIndex = 7
            Me.noproxy_radio.TabStop = True
            Me.noproxy_radio.Text = "No proxy"
            Me.noproxy_radio.UseVisualStyleBackColor = True
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Enabled = False
            Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label2.Location = New System.Drawing.Point(302, 113)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(32, 15)
            Me.Label2.TabIndex = 5
            Me.Label2.Text = "Port:"
            '
            'url_tb
            '
            Me.url_tb.Enabled = False
            Me.url_tb.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.url_tb.Location = New System.Drawing.Point(111, 110)
            Me.url_tb.Name = "url_tb"
            Me.url_tb.Size = New System.Drawing.Size(185, 21)
            Me.url_tb.TabIndex = 4
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Enabled = False
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(30, 113)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(75, 15)
            Me.Label1.TabIndex = 3
            Me.Label1.Text = "HTTP-Proxy:"
            '
            'apply_bt
            '
            Me.apply_bt.BackColor = System.Drawing.Color.DimGray
            Me.apply_bt.Cursor = System.Windows.Forms.Cursors.Hand
            Me.apply_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.apply_bt.ForeColor = System.Drawing.Color.Black
            Me.apply_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.apply_bt.Location = New System.Drawing.Point(12, 311)
            Me.apply_bt.Name = "apply_bt"
            Me.apply_bt.Size = New System.Drawing.Size(143, 34)
            Me.apply_bt.TabIndex = 166
            Me.apply_bt.Text = "Apply"
            Me.apply_bt.UseVisualStyleBackColor = False
            '
            'close_bt
            '
            Me.close_bt.BackColor = System.Drawing.Color.DimGray
            Me.close_bt.Cursor = System.Windows.Forms.Cursors.Hand
            Me.close_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.close_bt.ForeColor = System.Drawing.Color.Black
            Me.close_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.close_bt.Location = New System.Drawing.Point(294, 311)
            Me.close_bt.Name = "close_bt"
            Me.close_bt.Size = New System.Drawing.Size(143, 34)
            Me.close_bt.TabIndex = 167
            Me.close_bt.Text = "Exit"
            Me.close_bt.UseVisualStyleBackColor = False
            '
            'SettingsInterface
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(449, 354)
            Me.Controls.Add(Me.GroupBox1)
            Me.Controls.Add(Me.close_bt)
            Me.Controls.Add(Me.apply_bt)
            Me.DoubleBuffered = True
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "SettingsInterface"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Settings"
            Me.Controls.SetChildIndex(Me.apply_bt, 0)
            Me.Controls.SetChildIndex(Me.close_bt, 0)
            Me.Controls.SetChildIndex(Me.GroupBox1, 0)
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            CType(Me.port_ud, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents url_tb As System.Windows.Forms.TextBox
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents manualproxy_radio As System.Windows.Forms.RadioButton
        Friend WithEvents detectproxy_radio As System.Windows.Forms.RadioButton
        Friend WithEvents noproxy_radio As System.Windows.Forms.RadioButton
        Friend WithEvents apply_bt As System.Windows.Forms.Button
        Friend WithEvents close_bt As System.Windows.Forms.Button
        Friend WithEvents port_ud As System.Windows.Forms.NumericUpDown
        Friend WithEvents password_tb As System.Windows.Forms.TextBox
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents username_tb As System.Windows.Forms.TextBox
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents defcred_cb As System.Windows.Forms.CheckBox
    End Class
End Namespace