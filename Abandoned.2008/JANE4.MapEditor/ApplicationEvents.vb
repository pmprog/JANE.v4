Namespace My

  ' The following events are availble for MyApplication:
  ' 
  ' Startup: Raised when the application starts, before the startup form is created.
  ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
  ' UnhandledException: Raised if the application encounters an unhandled exception.
  ' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
  ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.
  Partial Friend Class MyApplication

    Private Sub MyApplication_UnhandledException(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs) Handles Me.UnhandledException
      Dim sErrPath As String

      Try
        sErrPath = Environ("TEMP")
        If Not sErrPath.EndsWith("\") Then sErrPath &= "\"
        sErrPath &= "JANE_Editor_" & Format(Now, "yyyyMMddhhmmss") & ".txt"

        FileOpen(8767, sErrPath, OpenMode.Append, OpenAccess.Write)
        PrintLine(1, "------------------------------------------------------[ Unhandled Exception")
        PrintLine(1, "Message: " & e.Exception.Message)
        PrintLine(1, "Stack: " & vbCrLf & e.Exception.StackTrace.ToString)

        FileClose(8767)

        MsgBox("In order for me to improve this application, please forward the debug file to bugs@pmprog.co.uk" & vbCrLf & "Filename: " & sErrPath & vbCrLf & vbCrLf & "Note: No personal or identifiable data will be sent")
      Catch ex As Exception

      End Try
    End Sub

  End Class

End Namespace

