' NOTA: puede usar el comando "Cambiar nombre" del menú contextual para cambiar el nombre de clase "Service" en el código, en svc y en el archivo de configuración a la vez.
' NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service.svc o Service.svc.vb en el Explorador de soluciones e inicie la depuración.
Imports BE
Imports DA
Imports System.ServiceModel.Activation

<AspNetCompatibilityRequirements(RequirementsMode:=AspNetCompatibilityRequirementsMode.Allowed)> _
Public Class Service
    Implements IService

    Public Function iniciarSesion(strMail As String, strPassword As String) As BEUser Implements IService.iniciarSesion
        Return (New DAUser().obtenerUser(strMail, strPassword))
    End Function

    Public Function insertarSearch(intUserID As Integer, intCareerID As Integer, intAsignatureID As Integer) As Boolean Implements IService.insertarSearch
        Dim oBESearch As New BESearch With {.UserID = intUserID, .CareerID = intCareerID, .AsignatureID = intAsignatureID, .SearchID = 1}
        Return (New DASearch().insertarSearch(oBESearch))
    End Function

    Public Function listarCarreras() As BECareer() Implements IService.listarCarreras
        Dim loBECareer As List(Of BECareer) = New DACareer().listarCareer()
        If loBECareer IsNot Nothing AndAlso loBECareer.Count > 0 Then
            Return loBECareer.ToArray
        Else
            Return Nothing
        End If
    End Function

    Public Function listarAsignaturePorCareerID(intCareerID As Integer) As BEAsignature() Implements IService.listarAsignaturePorCareerID
        Dim loBEAsignature As List(Of BEAsignature) = New DAAsignature().listarAsignaturePorCareerID(intCareerID)
        If loBEAsignature IsNot Nothing AndAlso loBEAsignature.Count > 0 Then
            Return loBEAsignature.ToArray
        Else
            Return Nothing
        End If
    End Function
End Class