Public Class Fire_Auth_Window
    Private Sub Fire_Auth_Window_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If LogoPictureBox.Location.Y < 20 Then
            LogoPictureBox.Location = New Drawing.Point(LogoPictureBox.Location.X, LogoPictureBox.Location.Y + 4)
        Else
            If AuthorizationLbl.Location.X < -2 Then
                AuthorizationLbl.Location = New Drawing.Point(AuthorizationLbl.Location.X + 11, AuthorizationLbl.Location.Y)
            Else
                If UsernameTxtBox.Location.X > 16 Then
                    UsernameTxtBox.Location = New Drawing.Point(UsernameTxtBox.Location.X - 2, UsernameTxtBox.Location.Y)
                    If UsernameTxtBox.Location.X > 16 Then
                        UsernameTxtBox.Location = New Drawing.Point(UsernameTxtBox.Location.X - 2, UsernameTxtBox.Location.Y)
                        If UsernameTxtBox.Location.X > 16 Then
                            UsernameTxtBox.Location = New Drawing.Point(UsernameTxtBox.Location.X - 2, UsernameTxtBox.Location.Y)
                        End If
                    End If
                Else
                    If PassTxtBox.Location.X < 16 Then
                        PassTxtBox.Location = New Drawing.Point(PassTxtBox.Location.X + 2, PassTxtBox.Location.Y)
                        If PassTxtBox.Location.X < 16 Then
                            PassTxtBox.Location = New Drawing.Point(PassTxtBox.Location.X + 2, PassTxtBox.Location.Y)
                            If PassTxtBox.Location.X < 16 Then
                                PassTxtBox.Location = New Drawing.Point(PassTxtBox.Location.X + 2, PassTxtBox.Location.Y)
                            End If
                        End If

                    Else
                        If LoginButton.Location.Y > 350 Then
                            LoginButton.Location = New Drawing.Point(LoginButton.Location.X, LoginButton.Location.Y - 2)
                        End If
                    End If
                End If
            End If
        End If
    End Sub
End Class
