Imports System.Data.SqlClient
Imports BE
Public Class DACareer
    Public Function listarCareer() As List(Of BECareer)
        Dim strNameSp As String = "uspListarCareer"
        Dim oBE As BECareer
        Dim loBE As New List(Of BECareer)
        Try
            Using ocnx As New SqlConnection(DAGeneral.p_strCnx)
                ocnx.Open()
                Dim cmd As New SqlCommand(strNameSp, ocnx)
                cmd.CommandType = CommandType.StoredProcedure
                Dim drd As SqlDataReader = cmd.ExecuteReader
                If drd IsNot Nothing AndAlso drd.HasRows Then
                    While drd.Read
                        oBE = New BECareer
                        With oBE
                            .CareerID = drd.GetInt32(0)
                            .Name = drd.GetString(1)
                        End With
                        loBE.Add(oBE)
                    End While
                End If
            End Using
            Return loBE
        Catch ex As Exception
            EnviarError(ex)
            Return Nothing
        End Try
    End Function
End Class
