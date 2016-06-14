Imports System.ServiceModel
Imports BE
' NOTA: puede usar el comando "Cambiar nombre" del menú contextual para cambiar el nombre de interfaz "IService" en el código y en el archivo de configuración a la vez.
<ServiceContract()>
Public Interface IService

    <OperationContract()>
    <WebInvoke(Method:="GET", ResponseFormat:=WebMessageFormat.Json)> _
    Function iniciarSesion(strMail As String, strPassword As String) As BEUser

    <OperationContract()>
    <WebInvoke(Method:="GET", ResponseFormat:=WebMessageFormat.Json)> _
    Function insertarSearch(intUserID As Integer, intCareerID As Integer, intAsignatureID As Integer) As Boolean

End Interface