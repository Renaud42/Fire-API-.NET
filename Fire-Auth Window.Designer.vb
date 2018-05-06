<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Fire_Auth_Window
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
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

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.UsernameTxtBox = New System.Windows.Forms.TextBox()
        Me.PassTxtBox = New System.Windows.Forms.TextBox()
        Me.LoginButton = New System.Windows.Forms.Button()
        Me.AuthorizationLbl = New System.Windows.Forms.Label()
        Me.LogoPictureBox = New System.Windows.Forms.PictureBox()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        CType(Me.LogoPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'UsernameTxtBox
        '
        Me.UsernameTxtBox.BackColor = System.Drawing.SystemColors.Control
        Me.UsernameTxtBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.UsernameTxtBox.Font = New System.Drawing.Font("Arial Narrow", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsernameTxtBox.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.UsernameTxtBox.Location = New System.Drawing.Point(340, 250)
        Me.UsernameTxtBox.Name = "UsernameTxtBox"
        Me.UsernameTxtBox.Size = New System.Drawing.Size(300, 38)
        Me.UsernameTxtBox.TabIndex = 0
        Me.UsernameTxtBox.Text = "Username"
        Me.UsernameTxtBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'PassTxtBox
        '
        Me.PassTxtBox.BackColor = System.Drawing.SystemColors.Control
        Me.PassTxtBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PassTxtBox.Font = New System.Drawing.Font("Arial Narrow", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PassTxtBox.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.PassTxtBox.Location = New System.Drawing.Point(-306, 300)
        Me.PassTxtBox.Name = "PassTxtBox"
        Me.PassTxtBox.Size = New System.Drawing.Size(300, 38)
        Me.PassTxtBox.TabIndex = 1
        Me.PassTxtBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.PassTxtBox.UseSystemPasswordChar = True
        '
        'LoginButton
        '
        Me.LoginButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LoginButton.FlatAppearance.BorderSize = 0
        Me.LoginButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LoginButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LoginButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.LoginButton.Font = New System.Drawing.Font("Calibri", 26.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LoginButton.ForeColor = System.Drawing.Color.White
        Me.LoginButton.Location = New System.Drawing.Point(12, 414)
        Me.LoginButton.Name = "LoginButton"
        Me.LoginButton.Size = New System.Drawing.Size(310, 50)
        Me.LoginButton.TabIndex = 2
        Me.LoginButton.Text = "Login"
        Me.LoginButton.UseMnemonic = False
        Me.LoginButton.UseVisualStyleBackColor = False
        '
        'AuthorizationLbl
        '
        Me.AuthorizationLbl.Font = New System.Drawing.Font("Calibri Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AuthorizationLbl.Location = New System.Drawing.Point(-352, 152)
        Me.AuthorizationLbl.Name = "AuthorizationLbl"
        Me.AuthorizationLbl.Size = New System.Drawing.Size(339, 88)
        Me.AuthorizationLbl.TabIndex = 3
        Me.AuthorizationLbl.Text = "Application will have access following informations on your account :" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Username, " &
    "Mail, e.g"
        Me.AuthorizationLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LogoPictureBox
        '
        Me.LogoPictureBox.Location = New System.Drawing.Point(104, -130)
        Me.LogoPictureBox.Name = "LogoPictureBox"
        Me.LogoPictureBox.Size = New System.Drawing.Size(128, 128)
        Me.LogoPictureBox.TabIndex = 4
        Me.LogoPictureBox.TabStop = False
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 1
        '
        'Fire_Auth_Window
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(334, 411)
        Me.Controls.Add(Me.LogoPictureBox)
        Me.Controls.Add(Me.AuthorizationLbl)
        Me.Controls.Add(Me.LoginButton)
        Me.Controls.Add(Me.PassTxtBox)
        Me.Controls.Add(Me.UsernameTxtBox)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Fire_Auth_Window"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Fire-Auth"
        Me.TopMost = True
        CType(Me.LogoPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents UsernameTxtBox As System.Windows.Forms.TextBox
    Friend WithEvents PassTxtBox As System.Windows.Forms.TextBox
    Friend WithEvents LoginButton As System.Windows.Forms.Button
    Friend WithEvents AuthorizationLbl As System.Windows.Forms.Label
    Friend WithEvents LogoPictureBox As System.Windows.Forms.PictureBox
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
End Class
