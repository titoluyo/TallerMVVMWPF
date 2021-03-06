Imports System
Imports EnvDTE
Imports EnvDTE80
Imports EnvDTE90
Imports EnvDTE90a
Imports EnvDTE100
Imports System.Diagnostics

Public Module Utilities

    Public Sub CleanAll()

        DTE.ExecuteCommand("Edit.RemoveAndSort")
        DTE.ExecuteCommand("Edit.FormatDocument")

    End Sub

    Public Sub CreateDependency()

        If DTE.SelectedItems.Count = 2 Then

            Dim parentProjectItem As ProjectItem
            Dim childProjectItem As ProjectItem
            Dim message As String
            Dim msgBoxResult As MsgBoxResult

            parentProjectItem = DTE.SelectedItems.Item(1).ProjectItem
            childProjectItem = DTE.SelectedItems.Item(2).ProjectItem

            message = String.Format("Is '{0}' dependent from '{1}' (Yes) or the other way around (No)?", childProjectItem.Name, parentProjectItem.Name)
            msgBoxResult = MsgBox(message, MsgBoxStyle.YesNoCancel, "Create Dependency")

            If Not msgBoxResult = Microsoft.VisualBasic.MsgBoxResult.Cancel Then

                If msgBoxResult = Microsoft.VisualBasic.MsgBoxResult.No Then
                    parentProjectItem = DTE.SelectedItems.Item(2).ProjectItem
                    childProjectItem = DTE.SelectedItems.Item(1).ProjectItem
                End If

                parentProjectItem.ProjectItems.AddFromFile(childProjectItem.FileNames(1))
                parentProjectItem.ExpandView()


            End If

        End If

    End Sub

    Sub MoveOutOfXAMLQuotes()
        Dim objSel As TextSelection = DTE.ActiveDocument.Selection

        While True
            If objSel.ActivePoint.AtEndOfLine Then
                Exit While
            ElseIf objSel.ActivePoint.CreateEditPoint.GetText(1) = "}" Then
                While True
                    objSel.WordRight()
                    If objSel.ActivePoint.CreateEditPoint.GetText(1) <> "}" Then
                        Exit While
                    End If
                End While
                Exit While
            ElseIf objSel.ActivePoint.CreateEditPoint.GetText(1) = Chr(34) Then
                DTE.ActiveDocument.Selection.CharRight()
                Exit While
            Else
                objSel.WordRight()
            End If

        End While
        DTE.ActiveDocument.Selection.Text = " "

    End Sub

End Module
