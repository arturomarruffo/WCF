Imports System.Data.SqlClient
Imports BE
Public Class DAUser
    Public Function obtenerUser(strMail As String, strPassword As String) As BEUser
        Dim strNameSp As String = "uspObtenerUserSesion"
        Dim oBE As BEUser = Nothing
        Try
            Using ocnx As New SqlConnection(DAGeneral.p_strCnx)
                ocnx.Open()
                Dim cmd As New SqlCommand(strNameSp, ocnx)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@Mail", strMail)
                cmd.Parameters.AddWithValue("@Password", strPassword)
                Dim drd As SqlDataReader = cmd.ExecuteReader(CommandBehavior.SingleRow)
                If drd IsNot Nothing AndAlso drd.HasRows Then
                    While drd.Read
                        oBE = New BEUser
                        With oBE
                            .UserID = drd.GetInt32(0)
                            .Mail = drd.GetString(1)
                            '.Password = drd.GetString(2)
                            .FirstName = drd.GetString(2)
                            .LastName = drd.GetString(3)
                            .Birthday = drd.GetString(4)
                            .Cellphone = drd.GetString(5)
                            .Code = drd.GetString(6)
                        End With
                    End While
                End If
            End Using
            Return oBE
        Catch ex As Exception
            EnviarError(ex)
            Return Nothing
        End Try
    End Function
End Class
