
Public Class JaneScript

  Sub New()

  End Sub

  Sub New(ByVal Script As String)

  End Sub

  Public Function CheckSyntax() As Boolean

  End Function

  Public Function GeneratepmScript() As String
    Dim sOutput As String

    sOutput = "ScriptBoot:" & vbCrLf
    sOutput &= " hostr ActiveCharacter, 0" & vbCrLf
    sOutput &= " hostr ActiveObject, 1" & vbCrLf
    sOutput &= " hostr ActiveRoom, 2" & vbCrLf
    sOutput &= " hostr ActiveGame, 3" & vbCrLf

    sOutput &= " jmp CustomScript" & vbCrLf

    sOutput &= "ActiveCharacter var 0" & vbCrLf
    sOutput &= "ActiveObject var 0" & vbCrLf
    sOutput &= "ActiveRoom var 0" & vbCrLf
    sOutput &= "ActiveGame var 0" & vbCrLf

    ' TODO: Add all script variables here

    sOutput &= "CustomScript:" & vbCrLf

    ' TODO: Generate pmScript code here

    Return sOutput
  End Function

  Public Function CompileToBinary(ByVal outputFilename As String) As Boolean
    Dim bSuccess As Boolean = False

    Return bSuccess
  End Function

End Class
