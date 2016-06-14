Imports System.Data.SqlClient
Imports BE
Public Class DAAsignature
    Public Function listarAsignaturePorCareerID(ByVal intCareerID As Integer) As List(Of BEAsignature)
        Dim strNameSp As String = "uspListarAsignaturePorCareerID"
        Dim oBE As BEAsignature
        Dim loBE As New List(Of BEAsignature)
        Try
            Using ocnx As New SqlConnection(DAGeneral.p_strCnx)
                ocnx.Open()
                Dim cmd As New SqlCommand(strNameSp, ocnx)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("CareerID", intCareerID)
                Dim drd As SqlDataReader = cmd.ExecuteReader
                If drd IsNot Nothing AndAlso drd.HasRows Then
                    While drd.Read
                        oBE = New BEAsignature
                        With oBE
                            .AsignatureID = drd.GetInt32(0)
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
