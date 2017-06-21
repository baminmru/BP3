Option Strict Off
Option Explicit On
Imports BP3
Friend Class frmSmartArm
	Inherits System.Windows.Forms.Form

	Dim ARMName As String
	
	Private Sub cmdClearAll_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClearAll.Click
		Dim i As Integer
		For i = 0 To lstTypes.Items.Count - 1
			lstTypes.SetItemChecked(i, False)
		Next 
	End Sub
	
	Private Sub cmdSelAll_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSelAll.Click
		Dim i As Integer
		For i = 0 To lstTypes.Items.Count - 1
			lstTypes.SetItemChecked(i, True)
		Next 
	End Sub
	
	Private Sub cmdStart_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdStart.Click
        Dim i As Integer
        Dim cnt As Integer
        Dim doc_app As bp3doc.bp3doc.Application
        Dim ot As bp3doc.bp3doc.bp3doc_def
		cnt = 0
		If chkCreateARM.CheckState = System.Windows.Forms.CheckState.Checked Then
			ARMName = InputBox("Название АРМ", "Подготовка АРМ", "АРМ " & Now)
		End If
		cnt = lstTypes.CheckedIndices.Count * 3 + 1
		
		pb.Minimum = 0
		pb.Maximum = cnt
		pb.Value = 0
		pb.Visible = True
		Label1.Visible = True
		For i = 0 To lstTypes.Items.Count - 1
			If lstTypes.GetItemChecked(i) Then
                Dim ti As tmpInst
                ti = lstTypes.Items(i)
                doc_app = Manager.GetInstanceObject(ti.ID)
                ot = doc_app.bp3doc_def.Item(1)
                MakeType(doc_app)
				System.Windows.Forms.Application.DoEvents()
			End If
		Next 
	
		pb.Visible = False
		Label1.Visible = False
		MsgBox("Подготовка АРМа завершена")
	End Sub
	
	Private Sub frmSmartArm_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
		lstTypes.Items.Clear()
        Dim i As Integer
        Dim ti As tmpInst


        Dim dt_ot As DataTable
        Dim doc_app As bp3doc.bp3doc.Application
        dt_ot = Manager.Session.GetData("select " + Manager.Session.GetProvider().ID2Base("instanceid", "instanceid") + " from bp3doc_def order by name ")


        For i = 0 To dt_ot.Rows.Count - 1
            doc_app = Manager.GetInstanceObject(New Guid(dt_ot.Rows(i)("instanceid").ToString()))

            ti = New tmpInst()
            ti.ID = doc_app.ID
            ti.Name = doc_app.bp3doc_def.Item(1).TheCaption
            ti.ObjType = doc_app.bp3doc_def.Item(1).Name
            lstTypes.Items.Add(ti)
        Next

	End Sub
	
    Private Sub MakeType(ByRef ot As bp3doc.bp3doc.Application)

        '
        pb.Value = pb.Value + 1
        Label1.Text = "Views for type " & ot.Name

        ProcessView(ot.bp3doc_store)

        pb.Value = pb.Value + 1
        Label1.Text = "Journal for type " & ot.Name


        Application.DoEvents()
        ProcessJournal(ot)



        pb.Value = pb.Value + 1
        Label1.Text = "Filter for type " & ot.Name
        Application.DoEvents()
        ProcessFilter(ot)


    End Sub



    Private Sub ProcessFilter(ByRef ot As bp3doc.bp3doc.Application)
        Dim p As bp3doc.bp3doc.bp3doc_store = Nothing
        p = JournalPart(ot)
        If p Is Nothing Then Exit Sub

        Dim pv As bp3qry.bp3qry.Application
        Dim i As Integer
        Dim f As bp3doc.bp3doc.bp3doc_field
        Dim ft As bp3ft.bp3ft.bp3ft_def
        Dim fltr As bp3list.bp3list.Application
        Dim fg As bp3list.bp3list.bp3list_filter

        pv = FindQueryForPart(p, True)

        If pv Is Nothing Then
            MsgBox("Ошибка получения запроса для объекта " & ot.Name)
            Exit Sub
        End If

        fltr = FindJournal(ot)
       
        While fltr.bp3list_filter.Count > 0
            fltr.bp3list_filter.Delete(1)
            fltr.bp3list_filter.Refresh()
        End While
        fg = fltr.bp3list_filter.Add
        With fg
            .Name = ot.Name
            .sequence = 1
            .Caption = "Фильтр для " & p.Caption
            .Save()
        End With

     

        Dim seq As Integer
        seq = 0

        pv.bp3qry_column.Sort = "sequence"

        Dim fname As String
        For i = 1 To pv.bp3qry_column.Count

            f = pv.bp3qry_column.Item(i).Field
            fname = pv.bp3qry_column.Item(i).the_Alias
            If IsFieldPresent(f.Parent.Parent, f.ID, Nothing) Then
                ft = f.FieldType
                If ft.TypeStyle <> bp3doc.bp3doc.enumTypeStyle.TypeStyle_Element_oformleniy Then
                    If ft.GridSortType = bp3doc.bp3doc.enumColumnSortType.ColumnSortType_As_Date Then
                        If ft.Name.ToLower = "birthday" Then

                            With CType(fg.bp3list_filterfield.Add, bp3list.bp3list.bp3list_filterfield)
                                .sequence = seq
                                .Name = fname
                                .Caption = f.Caption
                                .FieldType = ft
                                .RefType = f.ReferenceType
                                .RefToPart = f.RefToPart
                                .ValueArray = bp3doc.bp3doc.enumBoolean.Boolean_Net
                                .FieldSize = f.DataSize
                                seq = seq + 1
                                .Save()
                            End With
                        Else
                            With CType(fg.bp3list_filterfield.Add, bp3list.bp3list.bp3list_filterfield)
                                .sequence = seq
                                .Name = fname & "_GE"
                                .Caption = f.Caption & " C"
                                .FieldType = ft
                                .RefType = f.ReferenceType
                                .RefToPart = f.RefToPart

                                .ValueArray = bp3doc.bp3doc.enumBoolean.Boolean_Net
                                .FieldSize = f.DataSize
                                seq = seq + 1
                                .Save()
                            End With

                            With CType(fg.bp3list_filterfield.Add, bp3list.bp3list.bp3list_filterfield)
                                .sequence = seq
                                .Name = fname & "_LE"
                                .Caption = f.Caption & " по"
                                .FieldType = ft
                                .RefType = f.ReferenceType
                                .RefToPart = f.RefToPart

                                .ValueArray = bp3doc.bp3doc.enumBoolean.Boolean_Net
                                .FieldSize = f.DataSize
                                seq = seq + 1
                                .Save()
                            End With
                        End If
                    End If

                    If ft.GridSortType = bp3doc.bp3doc.enumColumnSortType.ColumnSortType_As_Numeric Then
                        With CType(fg.bp3list_filterfield.Add, bp3list.bp3list.bp3list_filterfield)
                            .sequence = seq
                            .Name = fname & "_GE"
                            .Caption = f.Caption & " >="
                            .FieldType = ft
                            .RefType = f.ReferenceType
                            .RefToPart = f.RefToPart

                            .ValueArray = bp3doc.bp3doc.enumBoolean.Boolean_Net
                            .FieldSize = f.DataSize
                            seq = seq + 1
                            .Save()
                        End With
                        With CType(fg.bp3list_filterfield.Add, bp3list.bp3list.bp3list_filterfield)
                            .sequence = seq
                            .Name = fname & "_LE"
                            .Caption = f.Caption & " <="
                            .FieldType = ft
                            .RefType = f.ReferenceType
                            .RefToPart = f.RefToPart

                            .ValueArray = bp3doc.bp3doc.enumBoolean.Boolean_Net
                            .FieldSize = f.DataSize
                            seq = seq + 1
                            .Save()
                        End With

                    End If

                    If ft.GridSortType = bp3doc.bp3doc.enumColumnSortType.ColumnSortType_As_String Then
                        '      If ft.TypeStyle = TypeStyle_Ssilka Then
                        '
                        '      Else
                        '
                        '      End If

                        With CType(fg.bp3list_filterfield.Add, bp3list.bp3list.bp3list_filterfield)
                            .sequence = seq
                            .Name = fname
                            .Caption = f.Caption
                            .FieldType = ft
                            .RefType = f.ReferenceType
                            .RefToPart = f.RefToPart
                            .ValueArray = bp3doc.bp3doc.enumBoolean.Boolean_Net
                            .FieldSize = f.DataSize
                            seq = seq + 1
                            .Save()
                        End With
                    End If



                    '    With fg.FileterField.Add
                    '      .sequence = seq
                    '      .Name = f.Name
                    '      .Caption = f.Caption
                    '      Set .FIELDTYPE = ft
                    '      Set .RefType = f.ReferenceType
                    '      Set .RefToPart = f.RefToPart
                    '      Set .RefToType = f.RefToType
                    '      .ValueArray = Boolean_Net
                    '      .Rows(i)ize = f.DataSize
                    '      seq = seq + 1
                    '      .Save
                    '    End With
                End If
            End If
        Next




    End Sub


    Private Sub ProcessJournal(ByRef ot As bp3doc.bp3doc.Application)
        Dim p As bp3doc.bp3doc.bp3doc_store

        Dim pv As bp3qry.bp3qry.Application
        p = JournalPart(ot)
        If p Is Nothing Then Exit Sub
        Dim i As Integer
        Dim f As bp3doc.bp3doc.bp3doc_field
        Dim jr As bp3list.bp3list.Application
        Dim ID As Guid

        Dim jc As bp3list.bp3list.bp3list_col

        Dim ft As bp3ft.bp3ft.bp3ft_def

        pv = FindQueryForPart(p, True)

        If Not pv Is Nothing Then
            jr = FindJournal(ot)
            If Not jr Is Nothing Then
                ID = jr.ID
                Manager.DeleteInstance(jr)
            Else
                ID = Guid.NewGuid
            End If


            Manager.NewInstance(ID, "bp3list", ot.bp3doc_def.Item(1).Name)
            jr = Manager.GetInstanceObject(ID)
            On Error Resume Next
            With CType(jr.bp3list_def.Add, bp3list.bp3list.bp3list_def)

                .Name = ot.bp3doc_def.Item(1).Name
                .TheComment = "Журнал для документов типа: " & ot.bp3doc_def.Item(1).TheCaption
                '.jrnlIconCls = "" & ot.objIconCls
                .SourceView = pv.bp3qry_def.Item(1)
                .Save()

            End With

           
            pv.bp3qry_column.Sort = "sequence"

            For i = 1 To pv.bp3qry_column.Count
                f = pv.bp3qry_column.Item(i).Field

                If IsFieldPresent(f.Parent.Parent, f.ID, Nothing) Then
                    jc = jr.bp3list_col.Add
                    With jc
                        .sequence = i
                        f = pv.bp3qry_column.Item(i).Field
                        .name = f.Caption
                        .ViewField = pv.bp3qry_column.Item(i).the_Alias
                        ft = f.FieldType
                        .ColSort = ft.GridSortType
                        .ColumnAlignment = bp3doc.bp3doc.enumVHAlignment.VHAlignment_Left_Top
                        .GroupAggregation = bp3doc.bp3doc.enumAggregationType.AggregationType_none

                        .Save()
                    End With
                  
                End If

            Next

            Dim j As Integer
            Dim LastI As Integer
            LastI = i



            'For j = 1 To pv.PARTVIEW_LNK.Count
            '    'UPGRADE_WARNING: Couldn't resolve default property of object pv.PARTVIEW_LNK.item().TheView.ViewColumn. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            '    CType(pv.PARTVIEW_LNK.Item(j).TheView, bp3doc.bp3doc.PARTVIEW).ViewColumn.Sort = "sequence"
            '    'UPGRADE_WARNING: Couldn't resolve default property of object pv.PARTVIEW_LNK.item(j).TheView.ViewColumn. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            '    For i = 1 To CType(pv.PARTVIEW_LNK.Item(j).TheView, bp3doc.bp3doc.PARTVIEW).ViewColumn.Count
            '        LastI = LastI + 1
            '        jc = jr.JournalColumn.Add
            '        With jc
            '            .sequence = LastI
            '            'UPGRADE_WARNING: Couldn't resolve default property of object pv.PARTVIEW_LNK.item().TheView.ViewColumn. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            '            f = CType(pv.PARTVIEW_LNK.Item(j).TheView, bp3doc.bp3doc.PARTVIEW).ViewColumn.Item(i).Field
            '            .name = f.Caption
            '            ft = f.FieldType
            '            .ColSort = ft.GridSortType
            '            .ColumnAlignment = bp3doc.bp3doc.enumVHAlignment.VHAlignment_Left_Top
            '            .GroupAggregation = bp3doc.bp3doc.enumAggregationType.AggregationType_none
            '            .Save()
            '        End With
            '        jcs = jc.JColumnSource.Add
            '        With jcs
            '            'UPGRADE_WARNING: Couldn't resolve default property of object pv.PARTVIEW_LNK.item().TheView.ViewColumn. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            '            .ViewField = CType(pv.PARTVIEW_LNK.Item(j).TheView, bp3doc.bp3doc.PARTVIEW).ViewColumn.Item(i).the_Alias
            '            .SrcPartView = jsrc
            '            .Save()
            '        End With
            '    Next
            'Next
        End If


    End Sub


   

    Private Sub ProcessView(ByRef parts As bp3doc.bp3doc.bp3doc_store_col)
        Dim p As bp3doc.bp3doc.bp3doc_store
        Dim i, k As Integer
        Dim vi As ViewItems


        Dim pv As bp3qry.bp3qry.Application
        Dim HasDefault As Boolean
        Dim AutoID As Guid
        For i = 1 To parts.Count

            p = parts.Item(i)
            AutoID = Guid.Empty
            Label1.Text = "View for " & p.Name
            Application.DoEvents()


            pv = FindQueryForPart(p, True)
            While Not pv Is Nothing

                Manager.DeleteInstance(pv)
                pv = FindQueryForPart(p, True)
            End While

            viCol = New Collection

            p.bp3doc_field.Sort = "sequence"

            Dim ft As bp3ft.bp3ft.bp3ft_def
            Dim f As bp3doc.bp3doc.bp3doc_field
            For k = 1 To p.bp3doc_field.Count
                f = p.bp3doc_field.Item(k)
                If True Then 'LATIR2Framework.ObjectHelper.IsFieldPresent(p, f.ID.ToString, "") Then
                    ft = f.FieldType
                    If ft.Name.ToLower <> "password" Then
                        If ft.TypeStyle <> bp3doc.bp3doc.enumTypeStyle.TypeStyle_Element_oformleniy Then
                            If chkNoMultiref.Checked Then
                                If ft.Name.ToLower <> "multiref" Then
                                    vi = New ViewItems
                                    vi.FieldID = p.bp3doc_field.Item(k).ID.ToString
                                    vi.Aggregation = ""
                                    viCol.Add(vi, Guid.NewGuid.ToString())
                                End If
                            Else
                                vi = New ViewItems
                                vi.FieldID = p.bp3doc_field.Item(k).ID.ToString
                                vi.Aggregation = ""
                                viCol.Add(vi, Guid.NewGuid.ToString())
                            End If
                        End If
                    End If
                End If

            Next

            NewViewName = p.Caption & " авто"
            NewViewAlias = "AUTO" & p.Name

            If p.PartType <> bp3doc.bp3doc.enumPartType.PartType_Derevo Then
                NewForChoose = Not HasDefault
            Else
                NewForChoose = False
            End If

            BasePart = p
            SaveView(AutoID)
           

            ProcessView(p.bp3doc_store)
        Next

    End Sub


    Public Sub SaveView(ByVal PVid As Guid)

        Dim i As Integer
        Dim vc As bp3qry.bp3qry.bp3qry_column
        Dim vi As ViewItems
        Dim fld As bp3doc.bp3doc.bp3doc_field
        Dim bReplacedView As Boolean
        Dim iid As Guid
        Dim sNM As String
        Dim sAliace As String
        Dim ft As bp3ft.bp3ft.bp3ft_def

        Dim pva As bp3qry.bp3qry.Application


        bReplacedView = False

           

        If PVid.Equals(Guid.Empty) Then
            PVid = Guid.NewGuid
        End If
            
        pva = Manager.NewInstance(PVid, "bp3qry", BasePart.Name)
        With CType(pva.bp3qry_def.Add, bp3qry.bp3qry.bp3qry_def)
            .SrcPart = BasePart
            .Name = BasePart.Name
            .the_Alias = "AUTO" + BasePart.Name
            .Save()

        End With
            
     



        'If NewForChoose Then
        '    pv.ForChoose = bp3doc.bp3doc.enumBoolean.Boolean_Da
        'Else
        '    pv.ForChoose = bp3doc.bp3doc.enumBoolean.Boolean_Net
        'End If

        pva.Save()

          
        CreatedView = pva

            For i = 1 To viCol.Count()

                On Error Resume Next
                vi = viCol.Item(i)

            fld = BasePart.Application.FindObject("bp3doc_field", vi.FieldID)
                If Not fld Is Nothing Then
                    ft = fld.FieldType
                    If ft.TypeStyle <> bp3doc.bp3doc.enumTypeStyle.TypeStyle_Element_oformleniy Then

                    vc = pva.bp3qry_column.Add()

                        If vi.Aggregation = "" Then
                            vc.Aggregation = bp3doc.bp3doc.enumAggregationType.AggregationType_none
                        ElseIf vi.Aggregation = "COUNT" Then
                            vc.Aggregation = bp3doc.bp3doc.enumAggregationType.AggregationType_COUNT
                        ElseIf vi.Aggregation = "SUM" Then
                            vc.Aggregation = bp3doc.bp3doc.enumAggregationType.AggregationType_SUM
                        ElseIf vi.Aggregation = "AVG" Then
                            vc.Aggregation = bp3doc.bp3doc.enumAggregationType.AggregationType_AVG
                        ElseIf vi.Aggregation = "MIN" Then
                            vc.Aggregation = bp3doc.bp3doc.enumAggregationType.AggregationType_MIN
                        ElseIf vi.Aggregation = "MAX" Then
                            vc.Aggregation = bp3doc.bp3doc.enumAggregationType.AggregationType_MAX
                        End If

                    vc.Name = fld.Caption & " (" & CType(fld.Parent.Parent, bp3doc.bp3doc.bp3doc_store).Caption & ")"

                    vc.the_Alias = CType(fld.Parent.Parent, bp3doc.bp3doc.bp3doc_store).Name & "_" & fld.Name

                    vc.FromPart = CType(fld.Parent.Parent, bp3doc.bp3doc.bp3doc_store)
                        vc.Field = fld
                        vc.sequence = i
                        vc.Save()
                    End If

                End If
            Next




    End Sub



    'Private Sub MakeArm()
    '    Dim arm As MTZwp.MTZwp.Application
    '    Dim ID As Guid
    '    Dim ot As bp3doc.bp3doc.OBJECTTYPE
    '    ID = Guid.NewGuid
    '    Call Manager.NewInstance(ID, "MTZwp", ARMName)
    '    arm = Manager.GetInstanceObject(ID)

    '    pb.Value = pb.Value + 1
    '    Label1.Text = "Combine WorkPlace "
    '    arm.Name = ARMName
    '    arm.Save()

    '    With CType(arm.WorkPlace.Add, MTZwp.MTZwp.WorkPlace)

    '        .Name = ARMName
    '        .Caption = ARMName
    '        .Save()
    '    End With

    '    Dim i As Integer
    '    ' добавили типы
    '    For i = 0 To lstTypes.Items.Count - 1
    '        If lstTypes.GetItemChecked(i) Then
    '            With CType(arm.ARMTypes.Add, MTZwp.MTZwp.ARMTypes)
    '                Dim ti As tmpInst
    '                ti = lstTypes.Items(i)
    '                .TheDocumentType = model.OBJECTTYPE.Item(ti.ID.ToString())
    '                .Save()
    '            End With
    '        End If
    '    Next

    '    ' Формируем Меню

    '    Dim dicMenu As MTZwp.MTZwp.EntryPoints
    '    Dim jrnlMenu As MTZwp.MTZwp.EntryPoints

    '    dicMenu = arm.EntryPoints.Add
    '    dicMenu.Name = "mnuDictionary"
    '    dicMenu.TheComment = "Меню для справочников"
    '    dicMenu.Caption = "Справочники"
    '    dicMenu.ActionType = bp3doc.bp3doc.enumMenuActionType.MenuActionType_Nicego_ne_delat_
    '    dicMenu.Save()

    '    jrnlMenu = arm.EntryPoints.Add
    '    jrnlMenu.Name = "mnuJRNL"
    '    jrnlMenu.Caption = "Журналы"
    '    jrnlMenu.TheComment = " "
    '    jrnlMenu.ActionType = bp3doc.bp3doc.enumMenuActionType.MenuActionType_Nicego_ne_delat_
    '    jrnlMenu.Save()
    '    'dicmenu.

    '    Dim mseq As Integer
    '    mseq = 1
    '    Dim TypeMenu As MTZwp.MTZwp.EntryPoints
    '    Dim ii As Integer
    '    For i = 0 To lstTypes.Items.Count - 1
    '        If lstTypes.GetItemChecked(i) Then
    '            Dim ti As tmpInst
    '            ti = lstTypes.Items(i)
    '            ot = model.OBJECTTYPE.Item(ti.ID.ToString())

    '            If ot.IsSingleInstance = bp3doc.bp3doc.enumBoolean.Boolean_Da Then
    '                With CType(dicMenu.EntryPoints.Add, MTZwp.MTZwp.EntryPoints)
    '                    .sequence = mseq
    '                    mseq = mseq + 1
    '                    .Name = "mnu" & ot.Name
    '                    .Caption = ot.the_Comment
    '                    .ActionType = bp3doc.bp3doc.enumMenuActionType.MenuActionType_Otkrit__dokument
    '                    .ObjectType = ot
    '                    .Save()
    '                End With

    '            Else

    '                ' сразу можно сделать журналы по состояниям  ???

    '                If ot.OBJSTATUS.Count = 0 Then


    '                    With CType(jrnlMenu.EntryPoints.Add(), MTZwp.MTZwp.EntryPoints)
    '                        ID = .ID
    '                        .sequence = mseq
    '                        mseq = mseq + 1
    '                        .Name = "mnu" & ot.Name
    '                        .Caption = ot.the_Comment
    '                        .ActionType = bp3doc.bp3doc.enumMenuActionType.MenuActionType_Otkrit__gurnal
    '                        .Journal = FindJournal(ot)
    '                        .TheFilter = FindFilter(ot)
    '                        .Save()
    '                    End With

    '                    MakeFilterMapping(jrnlMenu.EntryPoints.Item(ID.ToString()), ot)

    '                Else

    '                    TypeMenu = jrnlMenu.EntryPoints.Add()

    '                    With TypeMenu
    '                        .sequence = mseq
    '                        mseq = mseq + 1
    '                        .Name = "mnu" & ot.Name
    '                        .Caption = ot.the_Comment
    '                        .ActionType = bp3doc.bp3doc.enumMenuActionType.MenuActionType_Nicego_ne_delat_
    '                        .Save()
    '                    End With


    '                    With CType(TypeMenu.EntryPoints.Add(), MTZwp.MTZwp.EntryPoints)
    '                        ID = .ID
    '                        .sequence = mseq
    '                        mseq = mseq + 1
    '                        .Name = "mnuAll" & ot.Name
    '                        .Caption = ot.the_Comment & " - все состояния"
    '                        .ActionType = bp3doc.bp3doc.enumMenuActionType.MenuActionType_Otkrit__gurnal
    '                        .Journal = FindJournal(ot)
    '                        .TheFilter = FindFilter(ot)
    '                        .Save()
    '                    End With
    '                    MakeFilterMapping(TypeMenu.EntryPoints.Item(ID.ToString()), ot)

    '                    For ii = 1 To ot.OBJSTATUS.Count

    '                        With CType(TypeMenu.EntryPoints.Add, MTZwp.MTZwp.EntryPoints)
    '                            ID = .ID
    '                            .sequence = mseq
    '                            mseq = mseq + 1
    '                            .Name = "mnu" & ot.Name & "_" & ii
    '                            .Caption = ot.the_Comment & " :" & ot.OBJSTATUS.Item(ii).name
    '                            .ActionType = bp3doc.bp3doc.enumMenuActionType.MenuActionType_Otkrit__gurnal
    '                            .Journal = FindJournal(ot)
    '                            .TheFilter = FindFilter(ot)
    '                            .JournalFixedQuery = " INTSANCEStatusID='" & ot.OBJSTATUS.Item(ii).ID.ToString() & "'"
    '                            .Save()
    '                        End With
    '                        MakeFilterMapping(TypeMenu.EntryPoints.Item(ID.ToString()), ot)
    '                    Next
    '                End If

    '            End If

    '        End If
    '    Next
    'End Sub


    'Private Sub MakeFilterMapping(ByRef ep As MTZwp.MTZwp.EntryPoints, ByRef ot As bp3doc.bp3doc.OBJECTTYPE)
    '    Dim p As bp3doc.bp3doc.PART
    '    p = JournalPart(ot)
    '    'UPGRADE_NOTE: pv was upgraded to pv. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    '    Dim pv As bp3doc.bp3doc.PARTVIEW
    '    Dim i As Integer
    '    Dim f As bp3doc.bp3doc.FIELD
    '    Dim ft As bp3doc.bp3doc.FIELDTYPE

    '    For i = 1 To p.PARTVIEW.Count
    '        pv = p.PARTVIEW.Item(i)
    '        If UCase(pv.the_Alias) = "AUTO" & UCase(p.Name) Then
    '            Exit For
    '        End If
    '    Next



    '    pv.ViewColumn.Sort = "sequence"

    '    For i = 1 To pv.ViewColumn.Count

    '        f = pv.ViewColumn.Item(i).Field
    '        ft = f.FieldType

    '        If ft.GridSortType = bp3doc.bp3doc.enumColumnSortType.ColumnSortType_As_Date Then
    '            With CType(ep.EPFilterLink.Add, MTZwp.MTZwp.EPFilterLink)
    '                .RowSource = pv.the_Alias
    '                .FilterField = f.Name & "_GE"
    '                .TheExpression = pv.ViewColumn.Item(i).the_Alias & ">="" & GetFormattedDate(fltr.dtp" & f.Name & "_GE.value)"
    '                .Save()
    '            End With
    '            With CType(ep.EPFilterLink.Add, MTZwp.MTZwp.EPFilterLink)
    '                .RowSource = pv.the_Alias
    '                .FilterField = f.Name & "_LE"
    '                .TheExpression = pv.ViewColumn.Item(i).the_Alias & "<="" & GetFormattedDate(fltr.dtp" & f.Name & "_LE.value)"
    '                .Save()
    '            End With
    '        End If

    '        If ft.GridSortType = bp3doc.bp3doc.enumColumnSortType.ColumnSortType_As_Numeric Then
    '            With CType(ep.EPFilterLink.Add, MTZwp.MTZwp.EPFilterLink)
    '                .RowSource = pv.the_Alias
    '                .FilterField = f.Name & "_GE"
    '                .TheExpression = pv.ViewColumn.Item(i).the_Alias & ">="" & val(fltr.txt" & f.Name & "_GE.Text)"
    '                .Save()
    '            End With
    '            With CType(ep.EPFilterLink.Add, MTZwp.MTZwp.EPFilterLink)
    '                .RowSource = pv.the_Alias
    '                .FilterField = f.Name & "_LE"
    '                .TheExpression = pv.ViewColumn.Item(i).the_Alias & "<="" & val(fltr.txt" & f.Name & "_LE.Text)"
    '                .Save()
    '            End With
    '        End If

    '        If ft.GridSortType = bp3doc.bp3doc.enumColumnSortType.ColumnSortType_As_String Then
    '            If ft.TypeStyle = bp3doc.bp3doc.enumTypeStyle.TypeStyle_Ssilka Then
    '                With CType(ep.EPFilterLink.Add, MTZwp.MTZwp.EPFilterLink)
    '                    .RowSource = pv.the_Alias
    '                    .FilterField = f.Name
    '                    .TheExpression = pv.ViewColumn.Item(i).the_Alias & "_ID='"" & fltr.txt" & f.Name & ".Tag & ""'"" "
    '                    .Save()
    '                End With

    '            ElseIf ft.TypeStyle = bp3doc.bp3doc.enumTypeStyle.TypeStyle_Perecislenie Then
    '                With CType(ep.EPFilterLink.Add, MTZwp.MTZwp.EPFilterLink)
    '                    .RowSource = pv.the_Alias
    '                    .FilterField = f.Name
    '                    .TheExpression = pv.ViewColumn.Item(i).the_Alias & "='"" & fltr.cmb" & f.Name & ".Text & ""'"" "
    '                    .Save()
    '                End With
    '            ElseIf UCase(ft.Name) <> "FILE" And UCase(ft.Name) <> "ID" And UCase(ft.Name) <> "IMAGE" Then
    '                With CType(ep.EPFilterLink.Add, MTZwp.MTZwp.EPFilterLink)
    '                    .RowSource = pv.the_Alias
    '                    .FilterField = f.Name
    '                    .TheExpression = pv.ViewColumn.Item(i).the_Alias & " like '%"" & fltr.txt" & f.Name & ".Text & ""%'"" "
    '                    .Save()
    '                End With
    '            End If
    '        End If
    '    Next
    '    'доп вьюхи
    '    Dim j As Integer
    '    Dim LastI As Integer
    '    LastI = i

    '    Dim pvd As bp3doc.bp3doc.PARTVIEW
    '    For j = 1 To pv.PARTVIEW_LNK.Count

    '        pvd = pv.PARTVIEW_LNK.Item(j).TheView
    '        pvd.ViewColumn.Sort = "sequence"
    '        'UPGRADE_WARNING: Couldn't resolve default property of object pv.PARTVIEW_LNK.item(j).TheView.ViewColumn. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
    '        For i = 1 To pvd.ViewColumn.Count

    '            LastI = LastI + 1
    '            f = pvd.ViewColumn.Item(i).Field
    '            ft = f.FieldType

    '            If ft.GridSortType = bp3doc.bp3doc.enumColumnSortType.ColumnSortType_As_Date Then
    '                With CType(ep.EPFilterLink.Add, MTZwp.MTZwp.EPFilterLink)
    '                    .RowSource = pvd.the_Alias
    '                    .FilterField = f.Name & "_GE"
    '                    .TheExpression = pvd.ViewColumn.Item(i).the_Alias & ">="" & GetFormattedDate(fltr.dtp" & f.Name & "_GE.value)"
    '                    .Save()
    '                End With
    '                With CType(ep.EPFilterLink.Add, MTZwp.MTZwp.EPFilterLink)
    '                    .RowSource = pvd.the_Alias
    '                    .FilterField = f.Name & "_LE"
    '                    .TheExpression = pvd.ViewColumn.Item(i).the_Alias & "<="" & GetFormattedDate(fltr.dtp" & f.Name & "_LE.value)"
    '                    .Save()
    '                End With
    '            End If

    '            If ft.GridSortType = bp3doc.bp3doc.enumColumnSortType.ColumnSortType_As_Numeric Then
    '                With CType(ep.EPFilterLink.Add, MTZwp.MTZwp.EPFilterLink)
    '                    .RowSource = pvd.the_Alias
    '                    .FilterField = f.Name & "_GE"
    '                    .TheExpression = pvd.ViewColumn.Item(i).the_Alias & ">="" & val(fltr.txt" & f.Name & "_GE.Text)"
    '                    .Save()
    '                End With
    '                With CType(ep.EPFilterLink.Add, MTZwp.MTZwp.EPFilterLink)
    '                    .RowSource = pvd.the_Alias
    '                    .FilterField = f.Name & "_LE"
    '                    .TheExpression = pvd.ViewColumn.Item(i).the_Alias & "<="" & val(fltr.txt" & f.Name & "_LE.Text)"
    '                    .Save()
    '                End With
    '            End If

    '            If ft.GridSortType = bp3doc.bp3doc.enumColumnSortType.ColumnSortType_As_String Then
    '                If ft.TypeStyle = bp3doc.bp3doc.enumTypeStyle.TypeStyle_Ssilka Then
    '                    With CType(ep.EPFilterLink.Add, MTZwp.MTZwp.EPFilterLink)
    '                        .RowSource = pvd.the_Alias
    '                        .FilterField = f.Name
    '                        .TheExpression = pvd.ViewColumn.Item(i).the_Alias & "_ID='"" & fltr.txt" & f.Name & ".Tag & ""'"" "
    '                        .Save()
    '                    End With

    '                ElseIf ft.TypeStyle = bp3doc.bp3doc.enumTypeStyle.TypeStyle_Perecislenie Then
    '                    With CType(ep.EPFilterLink.Add, MTZwp.MTZwp.EPFilterLink)
    '                        .RowSource = pvd.the_Alias
    '                        .FilterField = f.Name
    '                        .TheExpression = pvd.ViewColumn.Item(i).the_Alias & "='"" & fltr.cmb" & f.Name & ".Text & ""'"" "
    '                        .Save()
    '                    End With
    '                ElseIf UCase(ft.Name) <> "FILE" And UCase(ft.Name) <> "ID" And UCase(ft.Name) <> "IMAGE" Then
    '                    With CType(ep.EPFilterLink.Add, MTZwp.MTZwp.EPFilterLink)
    '                        .RowSource = pvd.the_Alias
    '                        .FilterField = f.Name
    '                        .TheExpression = pvd.ViewColumn.Item(i).the_Alias & " like '%"" & fltr.txt" & f.Name & ".Text & ""%'"" "
    '                        .Save()
    '                    End With
    '                End If
    '            End If
    '        Next
    '    Next
    'End Sub


   


    Private Sub chkDelOtherView_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkDelOtherView.CheckedChanged
        If chkDelOtherView.Checked Then
            DelOtherView = True
        Else
            DelOtherView = False
        End If
    End Sub
End Class