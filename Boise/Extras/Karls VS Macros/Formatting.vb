
' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'  Visual Studio format all files macro.
'  Copyright (C) 2009 Tim Abell <tim@timwise.co.uk>
'
'  This program is free software: you can redistribute it and/or modify
'  it under the terms of the GNU General Public License as published by
'  the Free Software Foundation, either version 3 of the License, or
'  (at your option) any later version.
'
'  This program is distributed in the hope that it will be useful,
'  but WITHOUT ANY WARRANTY; without even the implied warranty of
'  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
'  GNU General Public License for more details.
'
'  You should have received a copy of the GNU General Public License
'  along with this program. If not, see <http://www.gnu.org/licenses/>.
'
' See the file COPYING for the full license.
'
' http://timwise.blogspot.com/2009/01/format-all-document-in-visual-studio.html
' http://github.com/timabell/vi-formatter-macro
' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports System
Imports EnvDTE
Imports EnvDTE80
Imports EnvDTE90
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Text


Public Module Formatting

    Dim allowed As List(Of String) = New List(Of String)
    Dim processed As Integer = 0
    Dim ignored As Integer = 0
    Dim errors As StringBuilder = New StringBuilder()
    Dim skippedExtensions As List(Of String) = New List(Of String)

    Public Sub FormatProject()
        'TODO - add/remove extensions here
        'Karl changed these
        allowed.Add(".cs")
        allowed.Add(".vb")
        Try
            recurseSolution(AddressOf processItem)
        Catch ex As Exception
            Debug.Print("error in main loop: " + ex.ToString())
        End Try
        Debug.Print("processed items: " + processed.ToString())
        Debug.Print("ignored items: " + ignored.ToString())
        Debug.Print("ignored extensions: " + String.Join(" ", skippedExtensions.ToArray()))
        Debug.Print(errors.ToString())
    End Sub

    Private Sub processItem(ByVal Item As ProjectItem)
        If Not Item.Name.Contains(".") Then
            ignored += 1
            Return
        End If
        Dim ext As String
        ext = Item.Name.Substring(Item.Name.LastIndexOf("."))   'get file extension
        If allowed.Contains(ext) Then
            formatItem(Item)
            processed += 1
        Else
            If Not skippedExtensions.Contains(ext) Then
                skippedExtensions.Add(ext)
            End If
            ignored += 1
        End If
    End Sub

    Private Sub formatItem(ByVal Item As ProjectItem)

        'Karl added this 
        If Item.Name.Contains(".Designer.") Then
            Exit Sub
        End If

        Debug.Print("processing file " + Item.Name)
        Try
            Dim window As EnvDTE.Window
            window = Item.Open()
            window.Activate()

            'Karl added this 
            If Item.Name.EndsWith(".cs") Then
                DTE.ExecuteCommand("Edit.RemoveAndSort")
            End If
            DTE.ExecuteCommand("Edit.FormatDocument", "")
            window.Document.Save()
            window.Close()
        Catch ex As Exception
            Debug.Print("error processing file." + ex.ToString())
            errors.Append("error processing file " + Item.Name + "  " + ex.ToString())
        End Try
    End Sub

    Private Delegate Sub task(ByVal Item As ProjectItem)

    Private Sub recurseSolution(ByVal taskRoutine As task)
        For Each Proj As Project In DTE.Solution.Projects
            Debug.Print("project " + Proj.Name)
            For Each Item As ProjectItem In Proj.ProjectItems
                recurseItems(Item, 0, taskRoutine)
            Next
        Next
    End Sub

    Private Sub recurseItems(ByVal Item As ProjectItem, ByVal depth As Integer, ByVal taskRoutine As task)
        Dim indent As String = New String("-", depth)
        Debug.Print(indent + " " + Item.Name)
        taskRoutine(Item)
        If Not Item.ProjectItems Is Nothing Then
            For Each Child As ProjectItem In Item.ProjectItems
                taskRoutine(Child)
                recurseItems(Child, depth + 1, taskRoutine)
            Next
        End If
    End Sub

End Module
