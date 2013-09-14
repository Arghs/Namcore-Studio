Imports NCFramework
Imports NCFramework.GlobalVariables
Public Class SpellSkill_interface
    Private spellItemList As New List(Of ListViewItem)
    Private skillItemList As New List(Of ListViewItem)
    Private ptMouseDownLocation As Point
    Private Sub me_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            ptMouseDownLocation = e.Location
        End If
    End Sub

    Private Sub me_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Me.Location = e.Location - ptMouseDownLocation + Location
        End If
    End Sub
    Private Sub highlighter_MouseEnter(sender As Object, e As EventArgs) Handles highlighter1.MouseEnter, highlighter2.MouseEnter
        sender.backgroundimage = My.Resources.highlight
    End Sub

    Private Sub highlighter_MouseLeave(sender As Object, e As EventArgs) Handles highlighter1.MouseLeave, highlighter2.MouseLeave
        sender.backgroundimage = Nothing
    End Sub

    Private Sub highlighter2_Click(sender As Object, e As EventArgs) Handles highlighter2.Click
        Me.Close()
    End Sub

    Private Sub highlighter1_Click(sender As Object, e As EventArgs) Handles highlighter1.Click
        WindowState = FormWindowState.Minimized
    End Sub

    Private Sub header_MouseDown(sender As Object, e As MouseEventArgs) Handles header.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            ptMouseDownLocation = e.Location
        End If
    End Sub
    Private Sub header_MouseMove(sender As Object, e As MouseEventArgs) Handles header.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Me.Location = e.Location - ptMouseDownLocation + Location
        End If
    End Sub


    Private Sub SpellSkill_interface_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim controlLST As List(Of Control)
        controlLST = FindAllChildren()
        For Each item_control As Control In controlLST
            item_control.SetDoubleBuffered()
        Next
    End Sub
    Public Event ThisCompleted As EventHandler(Of CompletedEventArgs)
    Shared lstitems As List(Of ListViewItem)

    Protected Overridable Sub OnCompleted(ByVal e As CompletedEventArgs)
        RaiseEvent ThisCompleted(Me, e)
    End Sub
    Private context As Threading.SynchronizationContext = Threading.SynchronizationContext.Current
    Private WithEvents m_handler As New LayoutHandler
    Public Sub prepareInterface(ByVal setId As Integer)
        Hide()
        m_handler.doOperate_spellSkill(setId)
    End Sub
    Private Sub MeCompleted() Handles Me.ThisCompleted
        resultstatusSpell_lbl.Text = spellList.Items.Count.ToString & " results"
        resultstatusSkill_lbl.Text = skillList.Items.Count.ToString & " results!"
        If spellItemList Is Nothing Then spellItemList = New List(Of ListViewItem)()
        If skillItemList Is Nothing Then skillItemList = New List(Of ListViewItem)()
        For Each spellItm As ListViewItem In spellList.Items
            spellItemList.Add(spellItm)
        Next
        For Each skillItm As ListViewItem In skillList.Items
            skillItemList.Add(skillItm)
        Next
        Userwait.Close()
        Application.DoEvents()
        Show()
    End Sub
    Public Function continueOperation(ByVal setId As Integer)
        LogAppend("Loading Spells/Skills", "SpellSkill_interface_continueOperation", True)
        If Not currentViewedCharSet.Spells Is Nothing Then
            For Each PSpell As Spell In currentViewedCharSet.Spells
                If Not PSpell.id = 0 Then
                    If PSpell.name Is Nothing Then
                        PSpell.name = GetSpellNameById(PSpell.id)
                    End If
                    Dim itm As New ListViewItem({PSpell.id.ToString, PSpell.name})
                    itm.Tag = PSpell
                    spellList.BeginInvoke(New AddItemDelegate(AddressOf DelegateControlAdding), itm, spellList)
                End If
            Next
        End If
        If Not currentViewedCharSet.Skills Is Nothing Then
            For Each PSkill As Skill In currentViewedCharSet.Skills
                If Not PSkill.id = 0 Then
                    If PSkill.name Is Nothing Then
                        PSkill.name = GetSkillNameById(PSkill.id)
                    End If
                    Dim itm As New ListViewItem({PSkill.id.ToString, PSkill.name, PSkill.value.ToString, PSkill.max.ToString})
                    itm.Tag = PSkill
                    skillList.BeginInvoke(New AddItemDelegate(AddressOf DelegateControlAdding), itm, skillList)
                End If
            Next
        End If
        ThreadExtensions.ScSend(context, New Action(Of CompletedEventArgs)(AddressOf OnCompleted), New CompletedEventArgs())
    End Function
    Delegate Sub AddItemDelegate(itm As ListViewItem, control As ListView)
    Private Sub DelegateControlAdding(additm As ListViewItem, control As ListView)
        control.Items.Add(additm)
    End Sub

End Class