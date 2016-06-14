Imports System.Data.SqlClient
Imports System.Reflection
Imports System.Net.Mail
Imports System.ComponentModel
Imports System.Net

Module DAModule1
    Public Sub fillDataToObject(ByVal oReader As SqlDataReader, ByVal oBE As Object, Optional blnStringPropio As Boolean = False)
        Dim oProperties() As PropertyInfo = oBE.GetType.GetProperties
        Dim intOrdinal As Integer = -1
        Dim strPropertyTypeName As String = ""

        For Each oProper In oProperties
            If Not oProper.Name.StartsWith("lo") AndAlso Not oProper.Name.StartsWith("p_") AndAlso Not oReader.IsDBNull(oReader.GetOrdinal(oProper.Name)) Then
                intOrdinal = oReader.GetOrdinal(oProper.Name)
                strPropertyTypeName = oProper.PropertyType.FullName

                If Not oReader.IsDBNull(intOrdinal) Then
                    Select Case strPropertyTypeName
                        Case "System.Int32"
                            oProper.SetValue(oBE, oReader.GetInt32(intOrdinal), Nothing)
                        Case "System.Boolean"
                            oProper.SetValue(oBE, oReader.GetBoolean(intOrdinal), Nothing)
                        Case Else
                            If blnStringPropio Then
                                oProper.SetValue(oBE, StrConv(oReader.GetString(intOrdinal), VbStrConv.ProperCase), Nothing)
                            Else
                                oProper.SetValue(oBE, oReader.GetString(intOrdinal), Nothing)
                            End If
                    End Select
                End If
            End If
        Next
    End Sub

    Public Sub EnviarError(ByVal ex As Exception, Optional ByVal strMensaje As String = "")
        Dim correo As New MailMessage
        With correo
            .From = New MailAddress("raul@intisolutionsdev.com")
            .To.Add("raul@intisolutionsdev.com")
            .Subject = "error aplicativo " & strMensaje
            .Body = ex.Message & " - - - " & ex.StackTrace & " - - - " & ex.Source & " - - - " & strMensaje
            .IsBodyHtml = False
            .Priority = MailPriority.Normal
        End With

        Dim smtp As New SmtpClient("smtp.gmail.com", 587)
        With smtp
            .EnableSsl = True
            .UseDefaultCredentials = False
            .Credentials = New NetworkCredential("raul@intisolutionsdev.com", "s0UlPOw3r1")
            .DeliveryMethod = SmtpDeliveryMethod.Network
            .Send(correo)
        End With

        correo.Dispose()
        correo = Nothing
        smtp = Nothing
    End Sub
    Public Sub crearParametros(ByVal cmd As SqlCommand, ByVal oBE As Object, Optional ByVal blnDelete As Boolean = False)
        Dim strTodo As String = ""
        Try
            SqlCommandBuilder.DeriveParameters(cmd)
            Dim Propiedad() As PropertyInfo = oBE.GetType.GetProperties()
            Dim intIndexParam As Integer = 1
            Dim paramNAme As String = ""
            For i = 0 To Propiedad.Count - 1
                Dim Valor As Object = Propiedad(i).GetValue(oBE, Nothing)
                Dim nombre As String = Propiedad(i).Name
                If Not nombre.StartsWith("p_") Then
                    If Valor IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(Valor.ToString) AndAlso Valor.ToString <> "-2" Then
                        paramNAme = cmd.Parameters(intIndexParam).ParameterName
                        If paramNAme = "@" & nombre Then
                            cmd.Parameters(intIndexParam).Value = Valor
                            intIndexParam += 1
                            If blnDelete Then Exit For
                        End If
                    End If
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Module
