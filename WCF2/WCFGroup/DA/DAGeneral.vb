Imports System.Reflection
Imports System.Data.SqlClient
Imports BE
Public Class DAGeneral
    Private Shared strCnx As String = "" 
    Public Shared ReadOnly Property p_strCnx() As String
        Get
            If String.IsNullOrEmpty(strCnx) Then strCnx = System.Configuration.ConfigurationManager.ConnectionStrings("cnx").ConnectionString
            Return strCnx
        End Get
    End Property
End Class
