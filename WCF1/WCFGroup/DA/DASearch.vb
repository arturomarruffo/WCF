Imports System.Data.SqlClient
Imports BE
Public Class DASearch
    Public Function insertarSearch(oBESearch As BESearch) As Boolean
        Dim strNameSp As String = "uspInsertarSearch"
        Dim blnExito As Boolean = False
        Try
            Using ocnx As New SqlConnection(DAGeneral.p_strCnx)
                ocnx.Open()
                Dim cmd As New SqlCommand(strNameSp, ocnx)
                cmd.CommandType = CommandType.StoredProcedure
                With cmd.Parameters
                    .Add("@UserID", SqlDbType.Int).Value = oBESearch.UserID
                    .Add("@CareerID", SqlDbType.Int).Value = oBESearch.CareerID
                    .Add("@AsignatureID", SqlDbType.Int).Value = oBESearch.AsignatureID
                    .Add("@SearchStatusID", SqlDbType.Int).Value = oBESearch.SearchStatusID
                End With

                blnExito = (cmd.ExecuteNonQuery > 0)
            End Using
        Catch ex As Exception
            EnviarError(ex)
            blnExito = False
        End Try

        Return blnExito
    End Function
End Class
