Imports Qoollo.Net.Http.HttpServer
Imports System.IO
Imports System.Net
Imports Newtonsoft.Json
Module Module1
    Private Function GetIPv4Address() As String
        GetIPv4Address = String.Empty
        Dim strHostName As String = System.Net.Dns.GetHostName()
        Dim iphe As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(strHostName)

        For Each ipheal As System.Net.IPAddress In iphe.AddressList
            If ipheal.AddressFamily = System.Net.Sockets.AddressFamily.InterNetwork Then
                GetIPv4Address = ipheal.ToString()
            End If
        Next

    End Function
    Sub Main()



        Dim server As New Server(8070)

        server.Scheme = "http"

        server.Hostname = GetIPv4Address()
        server.Run()
        St("HttpServer started @ " & GetIPv4Address())
        St("Listening at " & server.BaseUrl)
        St()
        St("Press <Enter> to exit")
        Console.ReadLine()
        server.Stop()
    End Sub

    Public Sub St(Optional t As String = "")
        Console.WriteLine("[{0} UTC] {1}", DateTime.UtcNow, t)
    End Sub

End Module
Public Class Server
    Inherits Qoollo.Net.Http.HttpServer

    Public Sub New(ByVal port As Integer)
        MyBase.New(port)

        [Get]("/") = Function(__) _root()
        [Get]("/start/") = Function(__) _start()
        Post("/start/") = Function(x) _post(x)

    End Sub
   

    Private Function _post(t As Object) As String
        St("[POST] /")
        Return "Hello From Post"
    End Function


    Private Function _root() As String
        St("[GET] /")
        Return "Hello world!"
    End Function
    Private Function _start() As String
        St("[GET] /start/")
        Return "Hello START!"
    End Function
End Class