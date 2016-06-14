Imports System.Data.SqlClient
Imports System.Reflection
Imports System.Net.Mail
Imports System.ComponentModel
Imports System.Net

Module DAModule1
    Public Sub EnviarError(ByVal ex As Exception, Optional ByVal strMensaje As String = "")
        Dim correo As New MailMessage
        With correo
            .From = New MailAddress("raul@intisolutionsdev.com")
            .To.Add("raul@intisolutionsdev.com")
            .Subject = "error aplicativo R2ro" & strMensaje
            .Body = ex.Message & " - - - " & ex.StackTrace & " - - - " & ex.Source & " - - - " & strMensaje
            .IsBodyHtml = False
            .Priority = MailPriority.Normal
        End With

        Dim smtp As New SmtpClient("smtp.gmail.com", 587)
        With smtp
            .EnableSsl = True
            .UseDefaultCredentials = False
            .Credentials = New NetworkCredential("raul@intisolutionsdev.com", "xxx")
            .DeliveryMethod = SmtpDeliveryMethod.Network
            .Send(correo)
        End With

        correo.Dispose()
        correo = Nothing
        smtp = Nothing
    End Sub
End Module
