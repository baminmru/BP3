Imports System.IO
Imports System.Text


Public Class frmB2Sercher
    Private basepath As String = "index.php/"
    Private Sub ExtJSMaker_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim i As Integer
        Dim ti As tmpInst
        For i = 1 To model.OBJECTTYPE.Count
            ti = New tmpInst()
            ti.ID = model.OBJECTTYPE.Item(i).ID
            ti.Name = model.OBJECTTYPE.Item(i).the_Comment
            ti.ObjType = model.OBJECTTYPE.Item(i).Name
            chkObjType.Items.Add(ti, False)
        Next



    End Sub
    Private Sub button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button3.Click
        folderBrowserDialogProjectOutput.SelectedPath = textBoxOutPutFolder.Text
        folderBrowserDialogProjectOutput.ShowDialog()
        textBoxOutPutFolder.Text = folderBrowserDialogProjectOutput.SelectedPath
        If Not textBoxOutPutFolder.Text.EndsWith("\") Then
            textBoxOutPutFolder.Text += "\"
        End If
    End Sub

    Private Sub cmdSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelectAll.Click
        Dim i As Integer
        For i = 0 To chkObjType.Items.Count - 1
            chkObjType.SetItemChecked(i, True)
        Next
    End Sub

    Private Sub cmdClearAll_Click(sender As System.Object, e As System.EventArgs) Handles cmdClearAll.Click
        Dim i As Integer
        For i = 0 To chkObjType.Items.Count - 1
            chkObjType.SetItemChecked(i, False)
        Next
    End Sub

    Private template As String

    Private Sub cmdGen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGen.Click
        Dim i As Integer
        Dim ti As tmpInst
       

        ' %PARAMS% - сгенерированные параметры поиска
        ' %THEME% - название темы
        ' %TYPE% - тип рынка ( objecttype)
        ' %MARKET - название типа

        pb.Minimum = 0
        pb.Maximum = model.OBJECTTYPE.Count
        pb.Value = 0
        pb.Visible = True


        Me.Text = "GetTemplate"


        Application.DoEvents()
       

        pb.Minimum = 0
        pb.Value = 0
        pb.Maximum = chkObjType.CheckedItems.Count
        Me.Text = "Generating types"
        Application.DoEvents()

        pb.Visible = True
        For i = 0 To chkObjType.CheckedItems.Count - 1
            pb.Value = i + 1
            ti = chkObjType.CheckedItems(i)
            GenType(ti.ID)
        Next
        ti = Nothing


        pb.Minimum = 0
        pb.Value = 0
        pb.Maximum = chkObjType.CheckedItems.Count
        Me.Text = "Generating types JS"
        Application.DoEvents()

        pb.Visible = True
        For i = 0 To chkObjType.CheckedItems.Count - 1
            pb.Value = i + 1
            ti = chkObjType.CheckedItems(i)
            GenTypeJS(ti.ID)
        Next
        ti = Nothing


        pb.Minimum = 0
        pb.Value = 0
        pb.Maximum = chkObjType.CheckedItems.Count
        Me.Text = "Generating types model"
        Application.DoEvents()

        pb.Visible = True
        For i = 0 To chkObjType.CheckedItems.Count - 1
            pb.Value = i + 1
            ti = chkObjType.CheckedItems(i)
            MakeJPHPModel(ti.ID)
        Next
        ti = Nothing


        pb.Minimum = 0
        pb.Value = 0
        pb.Maximum = chkObjType.CheckedItems.Count
        Me.Text = "Generating types controllers"
        Application.DoEvents()

        pb.Visible = True
        For i = 0 To chkObjType.CheckedItems.Count - 1
            pb.Value = i + 1
            ti = chkObjType.CheckedItems(i)
            MakeJPHPController(ti.ID)
            MakeFPHPController(ti.ID)
        Next
        ti = Nothing


        pb.Minimum = 0
        pb.Value = 0
        pb.Maximum = chkObjType.CheckedItems.Count
        Me.Text = "Generating types views"
        Application.DoEvents()

        pb.Visible = True
        For i = 0 To chkObjType.CheckedItems.Count - 1
            pb.Value = i + 1
            ti = chkObjType.CheckedItems(i)
            MakeJPHPViews(ti.ID)
        Next
        ti = Nothing



        pb.Minimum = 0
        pb.Value = 0
        pb.Maximum = chkObjType.CheckedItems.Count
        Me.Text = "Generating types forms"
        Application.DoEvents()

        pb.Visible = True
        For i = 0 To chkObjType.CheckedItems.Count - 1
            pb.Value = i + 1
            ti = chkObjType.CheckedItems(i)
            GenForm(ti.ID)
        Next
        ti = Nothing

        MsgBox("Генерация кода завершена")
    End Sub


    Private Sub MakeProjectFile(ByVal s As String, ByVal path As String, ByVal fname As String)
        Dim p As String
        p = path
        If Not p.EndsWith("\") Then
            p += "\"
        End If
        File.WriteAllText(p & fname, s, System.Text.Encoding.UTF8)
    End Sub


    Private Sub GenType(ByVal ID As Guid)

        Dim ot As MTZMetaModel.MTZMetaModel.OBJECTTYPE
        Dim P As MTZMetaModel.MTZMetaModel.PART
        ot = model.OBJECTTYPE.Item(ID.ToString())

        Try
            template = File.ReadAllText(txtTemplate.Text.Replace("%TYPE%", ot.Name), System.Text.Encoding.UTF8)
        Catch ex As Exception
            template = File.ReadAllText(txtTemplate.Text.Replace("%TYPE%", ""), System.Text.Encoding.UTF8)
        End Try


        template = template.Replace("%THEME%", cmbTheme.Text.Trim)

        Dim mytemplate As String
        Dim i As Integer
        Dim j As Integer
        Dim fld As MTZMetaModel.MTZMetaModel.FIELD
        Dim sw As StringBuilder
        sw = New StringBuilder

        
        sw.Append("<table class=""FormTABLE"" >")
        sw.Append("<tbody>")
   

        ot.PART.Sort = "sequence"
        For i = 1 To ot.PART.Count
            P = ot.PART.Item(i)
            If P.PartType = MTZMetaModel.MTZMetaModel.enumPartType.PartType_Stroka Then
                For j = 1 To P.FIELD.Count
                    fld = P.FIELD.Item(j)
                    sw.Append(MakeSearchFld(fld))

                Next

                Exit For

            End If
            

        Next
        sw.Append("</tbody>")
        sw.Append("</table>")



        mytemplate = template.Replace("%MARKET%", ot.the_Comment)
        mytemplate = mytemplate.Replace("%TYPE%", ot.Name)
        mytemplate = mytemplate.Replace("%PARAMS%", sw.ToString())
        mytemplate = mytemplate.Replace("%REGION%", GetRegion())
        mytemplate = mytemplate.Replace("%COMPANY%", GetCompany())

       
        mytemplate = mytemplate.Replace("%TYPEOBJ%", GetTypeList(ot.Name.ToUpper))
       
        MakeProjectFile(mytemplate, textBoxOutPutFolder.Text & "FORMS\", ot.Name.ToLower() & ".htm")


    End Sub


    Private Function GetTypeList(ByVal tname As String)
        Dim sw As StringBuilder
        sw = New StringBuilder

        Dim reg As DataTable

        Dim i As Integer


        Select Case tname
            Case "B2P"
                reg = Manager.Session.GetData("select TTypeObjSecondid id,name from TTypeObjSecond  order by name")
                For i = 0 To reg.Rows.Count - 1
                    sw.Append(vbCrLf & "   <option value=""" & reg.Rows(i)("id").ToString & """>" & reg.Rows(i)("name") & "</option>")
                Next
            Case "B2S"
                reg = Manager.Session.GetData("select TTypeObjSecondid id,name from TTypeObjSecond  order by name")
                For i = 0 To reg.Rows.Count - 1
                    sw.Append(vbCrLf & "   <option value=""" & reg.Rows(i)("id").ToString & """>" & reg.Rows(i)("name") & "</option>")
                Next
            Case "B2Z"
                reg = Manager.Session.GetData("select TTypeObjZagorodid id,name from TTypeObjZagorod  order by name")
                For i = 0 To reg.Rows.Count - 1
                    sw.Append(vbCrLf & "   <option value=""" & reg.Rows(i)("id").ToString & """>" & reg.Rows(i)("name") & "</option>")
                Next

            Case "B2C"
                reg = Manager.Session.GetData("select TTypeObjCommercid id,name from TTypeObjCommerc  order by name")
                For i = 0 To reg.Rows.Count - 1
                    sw.Append(vbCrLf & "   <option value=""" & reg.Rows(i)("id").ToString & """>" & reg.Rows(i)("name") & "</option>")
                Next
            Case "B2LR"
                reg = Manager.Session.GetData("select TTypeObjSecondid id,name from TTypeObjSecond  order by name")
                For i = 0 To reg.Rows.Count - 1
                    sw.Append(vbCrLf & "   <option value=""" & reg.Rows(i)("id").ToString & """>" & reg.Rows(i)("name") & "</option>")
                Next
            Case "B2FR"
                reg = Manager.Session.GetData("select TTypeObjCommercid id,name from TTypeObjCommerc  order by name")
                For i = 0 To reg.Rows.Count - 1
                    sw.Append(vbCrLf & "   <option value=""" & reg.Rows(i)("id").ToString & """>" & reg.Rows(i)("name") & "</option>")
                Next
            Case Else

        End Select

        Return sw.ToString

    End Function

    Private Sub GenForm(ByVal ID As Guid)

        Dim ot As MTZMetaModel.MTZMetaModel.OBJECTTYPE
        Dim P As MTZMetaModel.MTZMetaModel.PART
        ot = model.OBJECTTYPE.Item(ID.ToString())

        Try
            template = File.ReadAllText(txtCardTemplate.Text.Replace("%TYPE%", ot.Name), System.Text.Encoding.UTF8)
        Catch ex As Exception
            template = File.ReadAllText(txtCardTemplate.Text.Replace("%TYPE%", ""), System.Text.Encoding.UTF8)
        End Try

        template = template.Replace("%THEME%", cmbTheme.Text.Trim)


        Dim Jrnl As MTZJrnl.MTZJrnl.Application
        Dim rs As DataTable
        rs = Manager.Session.GetData("select instanceid from instance where objtype='mtzjrnl' and name='" + ot.Name + "'")

        If rs.Rows.Count = 0 Then Return
        Jrnl = Manager.GetInstanceObject(rs.Rows(0)("instanceid"))
        If Jrnl Is Nothing Then
            Return
        End If

        Dim mytemplate As String
        Dim i As Integer
        Dim j As Integer
        Dim fld As MTZMetaModel.MTZMetaModel.FIELD
        Dim sw As StringBuilder
        sw = New StringBuilder



        Dim pv As MTZMetaModel.MTZMetaModel.PARTVIEW

        Dim jr As MTZJrnl.MTZJrnl.Journal
        jr = Jrnl.Journal.Item(1)
        pv = model.FindRowObject("PartView", Jrnl.JournalSrc.Item(1).PartView)
        P = pv.Parent.Parent



        ot.PART.Sort = "sequence"

        For j = 1 To P.FIELD.Count
            fld = P.FIELD.Item(j)
            If fld.FieldGroupBox.ToLower() = "транспортная доступность" Then

                sw.Append(MakeInfoFld(fld, P.Name.ToLower() + "_" + fld.Name.ToLower))

            End If
        Next

        For j = 1 To P.FIELD.Count
            fld = P.FIELD.Item(j)
            If fld.FieldGroupBox.ToLower() = "характеристики" Then
                sw.Append(MakeInfoFld(fld, P.Name.ToLower() + "_" + fld.Name.ToLower))
            End If
        Next

        For j = 1 To P.FIELD.Count
            fld = P.FIELD.Item(j)
            If fld.FieldGroupBox.ToLower() = "дополнительно" Then

                sw.Append(MakeInfoFld(fld, P.Name.ToLower() + "_" + fld.Name.ToLower))

            End If
        Next

        For j = 1 To P.FIELD.Count
            fld = P.FIELD.Item(j)
            If fld.FieldGroupBox.ToLower() = "участок" Then

                sw.Append(MakeInfoFld(fld, P.Name.ToLower() + "_" + fld.Name.ToLower))

            End If
        Next


        For j = 1 To P.FIELD.Count
            fld = P.FIELD.Item(j)
            If fld.FieldGroupBox.ToLower() = "состояние" Then

                sw.Append(MakeInfoFld(fld, P.Name.ToLower() + "_" + fld.Name.ToLower))

            End If
        Next

        For j = 1 To P.FIELD.Count
            fld = P.FIELD.Item(j)
            If fld.FieldGroupBox.ToLower() = "даты" Then

                sw.Append(MakeInfoFld(fld, P.Name.ToLower() + "_" + fld.Name.ToLower))

            End If
        Next



        mytemplate = template.Replace("%MARKET%", ot.the_Comment)


        mytemplate = mytemplate.Replace("%PERSON%", "<?php   echo '&nbsp;'.$entry['" + P.Name.ToLower() + "_" + "firmid" + "']; ?>")
        mytemplate = mytemplate.Replace("%PHONE%", "<телефон>")

        mytemplate = mytemplate.Replace("%COUNTRY%", "<?php   echo '&nbsp;'.$entry['" + P.Name.ToLower() + "_" + "countryid" + "']; ?>")

        mytemplate = mytemplate.Replace("%REGION%", "<?php   echo '&nbsp;'.$entry['" + P.Name.ToLower() + "_" + "regionid" + "']; ?>")
        mytemplate = mytemplate.Replace("%DISTRICT%", "<?php   echo '&nbsp;'.$entry['" + P.Name.ToLower() + "_" + "districtid" + "']; ?>")

        mytemplate = mytemplate.Replace("%TITLE%", "<?php   echo '&nbsp;' ?>")

        mytemplate = mytemplate.Replace("%ADDR%", "<?php   echo '&nbsp;'.$entry['" + P.Name.ToLower() + "_" + "addressstring" + "']; ?>")
        If ot.Name.ToUpper = "B2FR" Then
            mytemplate = mytemplate.Replace("%FULLADDRESS%", _
                            "<?php   $f= $entry['" + P.Name.ToLower() + "_" + "regionid" + "'].','." & _
                            "$entry['" + P.Name.ToLower() + "_" + "districtid" + "'].' район,';" & _
                             " if($entry['" + P.Name.ToLower() + "_" + "cityid" + "']!="""" && $entry['" + P.Name.ToLower() + "_" + "cityid" + "']!=$entry['" + P.Name.ToLower() + "_" + "regionid" + "'] ) $f=$f.'  '.$entry['" + P.Name.ToLower() + "_" + "cityid" + "'].','; " & _
                            "  if($entry['" + P.Name.ToLower() + "_" + "addressstring" + "']!="""") $f=$f.' '.$entry['" + P.Name.ToLower() + "_" + "addressstring" + "']; else $f=$f.' '.$entry['" + P.Name.ToLower() + "_" + "streetid1" + "'];" & _
                            "  echo '&nbsp;'.str_replace(';',' ',$f); ?>")
        Else
            mytemplate = mytemplate.Replace("%FULLADDRESS%", _
                        "<?php   $f= $entry['" + P.Name.ToLower() + "_" + "regionid" + "'].','." & _
                        "$entry['" + P.Name.ToLower() + "_" + "districtid" + "'].' район,';" & _
                         " if($entry['" + P.Name.ToLower() + "_" + "cityid" + "']!="""" && $entry['" + P.Name.ToLower() + "_" + "cityid" + "']!=$entry['" + P.Name.ToLower() + "_" + "regionid" + "'] ) $f=$f.'  '.$entry['" + P.Name.ToLower() + "_" + "cityid" + "'].','; " & _
                        "  if($entry['" + P.Name.ToLower() + "_" + "addressstring" + "']!="""") $f=$f.' '.$entry['" + P.Name.ToLower() + "_" + "addressstring" + "']; else $f=$f.' '.$entry['" + P.Name.ToLower() + "_" + "streetid1" + "'];" & _
                        "  if($entry['" + P.Name.ToLower() + "_" + "house" + "']!="""") $f=$f.', дом '.$entry['" + P.Name.ToLower() + "_" + "house" + "']; " & _
                        "  if($entry['" + P.Name.ToLower() + "_" + "block" + "']!="""") $f=$f.', к. '.$entry['" + P.Name.ToLower() + "_" + "block" + "']; " & _
                        "  echo '&nbsp;'.str_replace(';',' ',$f); ?>")
        End If


        mytemplate = mytemplate.Replace("%LAT%", "<?php   echo '&nbsp;'.$entry['" + P.Name.ToLower() + "_" + "latitude" + "']; ?>")
        mytemplate = mytemplate.Replace("%LONG%", "<?php   echo '&nbsp;'.$entry['" + P.Name.ToLower() + "_" + "longitude" + "']; ?>")
        mytemplate = mytemplate.Replace("%SUBWAY%", "<?php   echo '&nbsp;'.$entry['" + P.Name.ToLower() + "_" + "subway" + "']; ?>")
        ' mytemplate = mytemplate.Replace("%STATION%", "<?php   echo '&nbsp;'.$entry['" + P.Name.ToLower() + "_" + "subway" + "']; ?>")

        If ot.Name.ToUpper = "B2FR" Then
            mytemplate = mytemplate.Replace("%SALL%", "<?php   echo '&nbsp;'.$entry['" + P.Name.ToLower() + "_" + "sappartment" + "']; ?>")
        Else
            mytemplate = mytemplate.Replace("%SALL%", "<?php   echo '&nbsp;'.$entry['" + P.Name.ToLower() + "_" + "sall" + "']; ?>")
        End If



        mytemplate = mytemplate.Replace("%SKITCHEN%", "<?php   echo '&nbsp;'.$entry['" + P.Name.ToLower() + "_" + "skitchen" + "']; ?>")
        mytemplate = mytemplate.Replace("%SLIVING%", "<?php   echo '&nbsp;'.$entry['" + P.Name.ToLower() + "_" + "sliving" + "']; ?>")
        mytemplate = mytemplate.Replace("%SROOMS%", "<?php   echo '&nbsp;'.$entry['" + P.Name.ToLower() + "_" + "srooms" + "']; ?>")
        mytemplate = mytemplate.Replace("%SLAND%", "<?php   echo '&nbsp;'.$entry['" + P.Name.ToLower() + "_" + "sland" + "']; ?>")
        mytemplate = mytemplate.Replace("%ROOMS%", "<?php   echo '&nbsp;'.$entry['" + P.Name.ToLower() + "_" + "rooms" + "']; ?>")
        mytemplate = mytemplate.Replace("%SROOMS%", "<?php   echo '&nbsp;'.$entry['" + P.Name.ToLower() + "_" + "salerooms" + "']; ?>")
        mytemplate = mytemplate.Replace("%AMOUNT%", "<?php   echo '&nbsp;'.$entry['" + P.Name.ToLower() + "_" + "amount" + "']; ?>")
        mytemplate = mytemplate.Replace("%CURRENCY%", "<?php   echo '&nbsp;'.$entry['" + P.Name.ToLower() + "_" + "tcurrencyid" + "']; ?>")
        mytemplate = mytemplate.Replace("%COMMENTS%", "<?php   echo '&nbsp;'.$entry['" + P.Name.ToLower() + "_" + "note1" + "']; ?>")
        mytemplate = mytemplate.Replace("%OPERATION%", "<?php   echo '&nbsp;'.$entry['" + P.Name.ToLower() + "_" + "taction" + "']; ?>")
        mytemplate = mytemplate.Replace("%FLOOR%", "<?php   echo '&nbsp;'.$entry['" + P.Name.ToLower() + "_" + "floor" + "']; ?>")
        mytemplate = mytemplate.Replace("%FLOORS%", "<?php   echo '&nbsp;'.$entry['" + P.Name.ToLower() + "_" + "floors" + "']; ?>")

        mytemplate = mytemplate.Replace("%TYPE%", ot.Name)

        Select Case ot.Name.ToUpper
            Case "B2P"
                mytemplate = mytemplate.Replace("%TYPEOBJ%", "<?php   echo '&nbsp;'.$entry['" + P.Name.ToLower() + "_" + "TTypeObjSecondId".ToLower() + "']; ?>")
            Case "B2S"
                mytemplate = mytemplate.Replace("%TYPEOBJ%", "<?php   echo '&nbsp;'.$entry['" + P.Name.ToLower() + "_" + "TTypeObjSecondId".ToLower() + "']; ?>")
            Case "B2Z"
                mytemplate = mytemplate.Replace("%TYPEOBJ%", "<?php   echo '&nbsp;'.$entry['" + P.Name.ToLower() + "_" + "TTypeObjZagorodId".ToLower() + "']; ?>")
                mytemplate = mytemplate.Replace("%OBJSIZE%", "<?php   echo '&nbsp;'.$entry['" + P.Name.ToLower() + "_" + "ObjSize".ToLower() + "']; ?>")

            Case "B2LR"
                mytemplate = mytemplate.Replace("%TYPEOBJ%", "<?php   echo '&nbsp;'.$entry['" + P.Name.ToLower() + "_" + "TTypeObjSecondId".ToLower() + "']; ?>")
            Case "B2FR"
                mytemplate = mytemplate.Replace("%TYPEOBJ%", "<?php   echo '&nbsp;'.$entry['" + P.Name.ToLower() + "_" + "TypeId".ToLower() + "']; ?>")
            Case "B2C"
                mytemplate = mytemplate.Replace("%TYPEOBJ%", "<?php   echo '&nbsp;'.$entry['" + P.Name.ToLower() + "_" + "TTypeObj".ToLower() + "']; ?>")


            Case Else
                mytemplate = mytemplate.Replace("%TYPEOBJ%", "")

        End Select



        mytemplate = mytemplate.Replace("%INFO%", sw.ToString())





        MakeProjectFile(mytemplate, textBoxOutPutFolder.Text & "views\", "form_" + ot.Name.ToLower() & ".php")


    End Sub


    Private Sub GenTypeJS(ByVal ID As Guid)
        Dim jstemplate As String

        jstemplate = File.ReadAllText(txtTemplateJS.Text, System.Text.Encoding.UTF8)

        Dim ot As MTZMetaModel.MTZMetaModel.OBJECTTYPE
        Dim P As MTZMetaModel.MTZMetaModel.PART
        ot = model.OBJECTTYPE.Item(ID.ToString())
        Dim mytemplate As String
        Dim i As Integer
        Dim j As Integer
        Dim fld As MTZMetaModel.MTZMetaModel.FIELD

        Dim check As StringBuilder
        check = New StringBuilder

        Dim sw As StringBuilder
        sw = New StringBuilder



        ot.PART.Sort = "sequence"
        For i = 1 To ot.PART.Count
            P = ot.PART.Item(i)
            If P.PartType = MTZMetaModel.MTZMetaModel.enumPartType.PartType_Stroka Then
                For j = 1 To P.FIELD.Count
                    fld = P.FIELD.Item(j)
                    sw.Append(MakeJSFld(fld))
                    check.Append(MakeJS_checkFld(fld))

                Next

                Exit For

            End If


        Next




        mytemplate = jstemplate.Replace("%PARAMCHECK%", check.ToString)

        mytemplate = mytemplate.Replace("%PARAMPROCS%", sw.ToString())


        ' %SW_DISTRICT%
        mytemplate = mytemplate.Replace("%SW_DISTRICT%", GetDistrictJS())
        ' %SW_SUBWAY%
        mytemplate = mytemplate.Replace("%SW_SUBWAY%", GetSubwayJS())

        MakeProjectFile(mytemplate, textBoxOutPutFolder.Text & "FORMS\", ot.Name.ToLower() & ".js")


    End Sub

    Private Function GetRegion() As String
        Dim sw As StringBuilder
        sw = New StringBuilder

        Dim reg As DataTable

        Dim i As Integer
        Dim j As Integer

        reg = Manager.Session.GetData("select treecode,name from b2g_addr where  objecttype in (1,7) and parentobj is null order by name")
        For i = 0 To reg.Rows.Count - 1
            sw.Append(vbCrLf & "   <option value=""" & reg.Rows(i)("treecode") & """>" & reg.Rows(i)("name") & "</option>")

        Next

        Return sw.ToString
    End Function


    Private Function GetCompany() As String
        Dim sw As StringBuilder
        sw = New StringBuilder

        Dim reg As DataTable

        Dim i As Integer
        Dim j As Integer

        reg = Manager.Session.GetData("select BFRM_Defid,name from BFRM_Def order by name")
        For i = 0 To reg.Rows.Count - 1
            sw.Append(vbCrLf & "   <option value=""" & reg.Rows(i)("BFRM_Defid").ToString() & """>" & reg.Rows(i)("name") & "</option>")

        Next

        Return sw.ToString
    End Function
    Private Function GetDistrictJS() As String
        Dim sw As StringBuilder
        sw = New StringBuilder

        Dim reg As DataTable
        Dim dist As DataTable
        Dim i As Integer
        Dim j As Integer

        reg = Manager.Session.GetData("select treecode,name from b2g_addr where  objecttype in (1,7) and parentobj is null order by name")
        For i = 0 To reg.Rows.Count - 1
            sw.Append(vbCrLf & "if (id.value == '" + reg.Rows(i)("treecode") + "') {")
            sw.Append(vbCrLf & "       addOpt(cbox, '(Выбрать район)', '-1');")
            dist = Manager.Session.GetData("select treecode,name from b2g_addr where objecttype in (2,8) and  treecode like '" + reg.Rows(i)("treecode") + "%' order by name")
            For j = 0 To dist.Rows.Count - 1
                sw.Append(vbCrLf & "      addOpt(cbox, '" + dist.Rows(j)("name") + "', '" + dist.Rows(j)("treecode") + "');")
            Next
            sw.Append(vbCrLf & "}")
        Next

        Return sw.ToString
    End Function

    Private Function GetSubwayJS() As String
        Dim sw As StringBuilder
        sw = New StringBuilder
        Dim reg As DataTable
        Dim dist As DataTable
        Dim i As Integer
        Dim j As Integer
        reg = Manager.Session.GetData("select treecode,name from b2g_addr where  objecttype in (1,7) and parentobj is null order by name")
        For i = 0 To reg.Rows.Count - 1
            sw.Append(vbCrLf & "if (id.value == '" + reg.Rows(i)("treecode") + "') {")
            sw.Append(vbCrLf & "       addOpt(cbox, '(Выбрать станцию)', '-1');")
            dist = Manager.Session.GetData("select TSubwayid,TSubway.name from TSubway join b2g_addr on tsubway.name=b2g_addr.name  where objecttype in (5,6) and  treecode like '" + reg.Rows(i)("treecode") + "%' order by name")
            For j = 0 To dist.Rows.Count - 1
                sw.Append(vbCrLf & "      addOpt(cbox, '" + dist.Rows(j)("name") + "', '" + dist.Rows(j)("TSubwayid").ToString + "');")
            Next
            sw.Append(vbCrLf & "}")
        Next

        Return sw.ToString
    End Function

    Private Function MakeSearchFldHeader(ByVal fld As MTZMetaModel.MTZMetaModel.FIELD) As String
        Dim sw As StringBuilder
        sw = New StringBuilder

        sw.Append(vbCrLf & " <tr>")
        sw.Append(vbCrLf & "<td  width=""550px"" align=""left"">")
        sw.Append(vbCrLf & "  <table width=""550px"" border=""0"" cellpadding=""0"" cellspacing=""0"">")
        sw.Append(vbCrLf & "        <tr>")
        sw.Append(vbCrLf & "           <td  width=""146px"" align=""left""><label >" + fld.Caption + ":&nbsp;</label>")
        sw.Append(vbCrLf & "           </td>")
        sw.Append(vbCrLf & "           <td  width=""400px"" align=""left"">  ")
        Return sw.ToString
    End Function

    Private Function MakeSearchFldFooter(ByVal fld As MTZMetaModel.MTZMetaModel.FIELD) As String
        Dim sw As StringBuilder
        sw = New StringBuilder
        sw.Append(vbCrLf & "           </td>")
        sw.Append(vbCrLf & "        </tr>")
        sw.Append(vbCrLf & "       </table>")
        sw.Append(vbCrLf & "    </td>")
        sw.Append(vbCrLf & "  </tr>")
        Return sw.ToString
    End Function
    Private Function MakeSearchFld(ByVal fld As MTZMetaModel.MTZMetaModel.FIELD) As String

        Dim sw As StringBuilder
        sw = New StringBuilder
        Dim k As Integer
        Dim ft As MTZMetaModel.MTZMetaModel.FIELDTYPE


        If fld.FieldGroupBox.ToLower() = "дополнительно" Or fld.FieldGroupBox.ToLower() = "характеристики" Then
            ft = fld.FieldType

            If fld.ReferenceType = MTZMetaModel.MTZMetaModel.enumReferenceType.ReferenceType_Skalyrnoe_pole_OPN_ne_ssilkaCLS Then

                If ft.Name.ToLower() = "masterstring" Then


                ElseIf ft.Name.ToLower() = "file" Then

                ElseIf ft.TypeStyle = MTZMetaModel.MTZMetaModel.enumTypeStyle.TypeStyle_Perecislenie Then

                    sw.Append(MakeSearchFldHeader(fld))
                    sw.Append(vbCrLf & "<select class=""InputSelect"" name=""" + fld.Name + """  id=""" + fld.Name + """ title=""" + fld.Caption + """ ")
                    sw.Append(vbCrLf & "         >")
                    sw.Append(vbCrLf & "   <option selected=""selected"" value=""-1"">(несущественно)</option>")


                    For k = 1 To ft.ENUMITEM.Count
                        If k > 1 Then
                            sw.Append(vbCrLf & "   <option value=""" & ft.ENUMITEM.Item(k).NameValue & """>" & ft.ENUMITEM.Item(k).Name & "</option>")
                        End If
                    Next

                    sw.Append(vbCrLf & " </select>")
                    sw.Append(MakeSearchFldFooter(fld))


                Else


                    If ft.GridSortType = MTZMetaModel.MTZMetaModel.enumColumnSortType.ColumnSortType_As_String Then

                        'If ft.Name.ToLower() = "password" Then
                        '    sw.Append(vbCrLf & "xtype:  'textfield',")
                        '    sw.Append(vbCrLf & "inputType:  'password',")
                        '    sw.Append(vbCrLf & "value:  '',")
                        'ElseIf ft.Name.ToLower() = "memo" Then
                        '    sw.Append(vbCrLf & "xtype:  'textareafield',")
                        '    sw.Append(vbCrLf & "value:  '',")
                        'Else
                        '    sw.Append(vbCrLf & "xtype:  'textfield',")
                        '    sw.Append(vbCrLf & "value:  '',")
                        'End If


                    End If

                    If ft.GridSortType = MTZMetaModel.MTZMetaModel.enumColumnSortType.ColumnSortType_As_Date Then
                        'sw.Append(vbCrLf & "xtype:  'datefield',")
                        'If FldReadOnly Then
                        '    sw.Append(vbCrLf & "hideTrigger: true,")
                        'End If
                        'sw.Append(vbCrLf & "value:  '',")
                    End If

                    If ft.GridSortType = MTZMetaModel.MTZMetaModel.enumColumnSortType.ColumnSortType_As_Numeric Then
                        'sw.Append(vbCrLf & "xtype:  'numberfield',")
                        'If FldReadOnly Then
                        '    sw.Append(vbCrLf & "hideTrigger: true,")
                        'End If
                        'sw.Append(vbCrLf & "value:  '0',")
                    End If
                End If

            ElseIf fld.ReferenceType = MTZMetaModel.MTZMetaModel.enumReferenceType.ReferenceType_Na_stroku_razdela Then
                Dim refP As MTZMetaModel.MTZMetaModel.PART
                refP = fld.RefToPart

                If Not refP Is Nothing Then


                    If refP.Name.ToLower <> "b2g_addr" Then
                        sw.Append(MakeSearchFldHeader(fld))
                        sw.Append(vbCrLf & "<select class=""InputSelect"" name=""" + fld.Name + """  id=""" + fld.Name + """ title=""" + fld.Caption + """ ")
                        sw.Append(vbCrLf & "         >")
                        sw.Append(vbCrLf & "   <option selected=""selected"" value=""-1"">(несущественно)</option>")

                        Dim rs As DataTable

                        rs = Manager.Session.GetData(" select " + refP.Name + "id id, dbo." + refP.Name + "_BRIEF_F(B2G(" + refP.Name + "." + refP.Name + "Id), NULL) Brief from " + refP.Name)



                        For k = 0 To rs.Rows.Count - 1
                            If k > 1 Then
                                sw.Append(vbCrLf & "   <option value=""" & rs.Rows(k)("id").ToString & """>" & rs.Rows(k)("Brief").ToString & "</option>")
                            End If
                        Next

                        sw.Append(vbCrLf & " </select>")
                        sw.Append(MakeSearchFldFooter(fld))
                    End If

                End If

            Else


                'sw.Append(vbCrLf & "xtype:  'textfield',")
                'sw.Append(vbCrLf & "value:  'todo - refernce to object',")
            End If


            'If fld.AllowNull = MTZMetaModel.MTZMetaModel.enumBoolean.Boolean_Net Then
            '    sw.Append(vbCrLf & "allowBlank:false,")
            'Else
            '    sw.Append(vbCrLf & "allowBlank:true,")
            'End If

            'sw.Append(vbCrLf & "name:   '" & fld.Caption.ToLower() & "',")
            'sw.Append(vbCrLf & "fieldLabel:  '" & fld.Caption & "'")


        End If

        Return sw.ToString()
    End Function



    Private Function MakeInfoFld(ByVal fld As MTZMetaModel.MTZMetaModel.FIELD, ByVal FieldAlias As String) As String

        Dim sw As StringBuilder
        sw = New StringBuilder
        Dim k As Integer
        Dim ft As MTZMetaModel.MTZMetaModel.FIELDTYPE

        'fld.FieldGroupBox.ToLower() = "информация" Or
        'If fld.FieldGroupBox.ToLower() = "дополнительно" Or fld.FieldGroupBox.ToLower() = "состояние" Then
        ft = fld.FieldType

        If fld.ReferenceType = MTZMetaModel.MTZMetaModel.enumReferenceType.ReferenceType_Skalyrnoe_pole_OPN_ne_ssilkaCLS Then

            If ft.Name.ToLower() = "masterstring" Then

                sw.Append(vbCrLf & "<tr>")
                sw.Append(vbCrLf & "<th  ><label>" + fld.Caption + ":</label></th>")
                sw.Append(vbCrLf & "<td> <?php   echo '&nbsp;'.$entry['" + FieldAlias.ToLower() + "']; ?></td></tr>")
            ElseIf ft.Name.ToLower() = "file" Then

            ElseIf ft.TypeStyle = MTZMetaModel.MTZMetaModel.enumTypeStyle.TypeStyle_Perecislenie Then

                sw.Append(vbCrLf & "<tr>")
                sw.Append(vbCrLf & "<th ><label>" + fld.Caption + ":</label></th>")
                sw.Append(vbCrLf & "<td> <?php   echo '&nbsp;'.$entry['" + FieldAlias.ToLower() + "']; ?></td></tr>")


            Else


                If ft.GridSortType = MTZMetaModel.MTZMetaModel.enumColumnSortType.ColumnSortType_As_String Then

                    If ft.Name.ToLower() = "password" Then

                    ElseIf ft.Name.ToLower() = "memo" Then
                        sw.Append(vbCrLf & "<tr>")
                        sw.Append(vbCrLf & "<th ><label>" + fld.Caption + ":</label></th>")
                        sw.Append(vbCrLf & "<td> <?php   echo '&nbsp;'.$entry['" + FieldAlias.ToLower() + "']; ?></td></tr>")
                    Else


                        sw.Append(vbCrLf & "<tr>")
                        sw.Append(vbCrLf & "<th ><label>" + fld.Caption + ":</label></th>")
                        sw.Append(vbCrLf & "<td> <?php   echo '&nbsp;'.$entry['" + FieldAlias.ToLower() + "']; ?></td></tr>")


                    End If


                End If

                If ft.GridSortType = MTZMetaModel.MTZMetaModel.enumColumnSortType.ColumnSortType_As_Date Then
                    sw.Append(vbCrLf & "<tr>")
                    sw.Append(vbCrLf & "<th ><label>" + fld.Caption + ":</label></th>")
                    sw.Append(vbCrLf & "<td> <?php   echo '&nbsp;'.$entry['" + FieldAlias.ToLower() + "']; ?></td></tr>")
                End If

                If ft.GridSortType = MTZMetaModel.MTZMetaModel.enumColumnSortType.ColumnSortType_As_Numeric Then
                    sw.Append(vbCrLf & "<tr>")
                    sw.Append(vbCrLf & "<th ><label>" + fld.Caption + ":</label></th>")
                    sw.Append(vbCrLf & "<td> <?php   echo '&nbsp;'.$entry['" + FieldAlias.ToLower() + "']; ?></td></tr>")
                End If
            End If

        ElseIf fld.ReferenceType = MTZMetaModel.MTZMetaModel.enumReferenceType.ReferenceType_Na_stroku_razdela Then
            sw.Append(vbCrLf & "<tr>")
            sw.Append(vbCrLf & "<th ><label>" + fld.Caption + ":</label></th>")
            sw.Append(vbCrLf & "<td> <?php   echo '&nbsp;'.$entry['" + FieldAlias.ToLower() + "']; ?></td></tr>")

        Else


            sw.Append(vbCrLf & "<tr>")
            sw.Append(vbCrLf & "<th ><label>" + fld.Caption + ":</label></th>")
            sw.Append(vbCrLf & "<td> <?php   echo '&nbsp;'.$entry['" + FieldAlias.ToLower() + "']; ?></td></tr>")
        End If




        'End If

        Return sw.ToString()
    End Function


    Private Function MakeJS_checkFld(ByVal fld As MTZMetaModel.MTZMetaModel.FIELD) As String

        Dim sw As StringBuilder
        sw = New StringBuilder
        Dim k As Integer
        Dim ft As MTZMetaModel.MTZMetaModel.FIELDTYPE


        If fld.FieldGroupBox.ToLower() = "дополнительно" Or fld.FieldGroupBox.ToLower() = "характеристики" Then
            ft = fld.FieldType

            If fld.ReferenceType = MTZMetaModel.MTZMetaModel.enumReferenceType.ReferenceType_Skalyrnoe_pole_OPN_ne_ssilkaCLS Then

                If ft.Name.ToLower() = "masterstring" Then


                ElseIf ft.Name.ToLower() = "file" Then

                ElseIf ft.TypeStyle = MTZMetaModel.MTZMetaModel.enumTypeStyle.TypeStyle_Perecislenie Then

                    sw.Append(vbCrLf & "// проверяем поле " + fld.Caption + " ...")
                    sw.Append(vbCrLf & "if (typeof Check_" + fld.Name + " == 'function') {")
                    sw.Append(vbCrLf & "    var Pre_" + fld.Name + " = Check_" + fld.Name + "(TypeId);")
                    sw.Append(vbCrLf & "    if (Pre_" + fld.Name + " != """") PreQuery +=(""" + fld.Name + ":""+ Pre_" + fld.Name + "+"";"");")
                    sw.Append(vbCrLf & "}")


                Else


                    If ft.GridSortType = MTZMetaModel.MTZMetaModel.enumColumnSortType.ColumnSortType_As_String Then

                        'If ft.Name.ToLower() = "password" Then
                        '    sw.Append(vbCrLf & "xtype:  'textfield',")
                        '    sw.Append(vbCrLf & "inputType:  'password',")
                        '    sw.Append(vbCrLf & "value:  '',")
                        'ElseIf ft.Name.ToLower() = "memo" Then
                        '    sw.Append(vbCrLf & "xtype:  'textareafield',")
                        '    sw.Append(vbCrLf & "value:  '',")
                        'Else
                        '    sw.Append(vbCrLf & "xtype:  'textfield',")
                        '    sw.Append(vbCrLf & "value:  '',")
                        'End If


                    End If

                    If ft.GridSortType = MTZMetaModel.MTZMetaModel.enumColumnSortType.ColumnSortType_As_Date Then
                        'sw.Append(vbCrLf & "xtype:  'datefield',")
                        'If FldReadOnly Then
                        '    sw.Append(vbCrLf & "hideTrigger: true,")
                        'End If
                        'sw.Append(vbCrLf & "value:  '',")
                    End If

                    If ft.GridSortType = MTZMetaModel.MTZMetaModel.enumColumnSortType.ColumnSortType_As_Numeric Then
                        'sw.Append(vbCrLf & "xtype:  'numberfield',")
                        'If FldReadOnly Then
                        '    sw.Append(vbCrLf & "hideTrigger: true,")
                        'End If
                        'sw.Append(vbCrLf & "value:  '0',")
                    End If
                End If

            ElseIf fld.ReferenceType = MTZMetaModel.MTZMetaModel.enumReferenceType.ReferenceType_Na_stroku_razdela Then
                Dim refP As MTZMetaModel.MTZMetaModel.PART
                refP = fld.RefToPart

                If Not refP Is Nothing Then


                    'If refP.Name.ToLower <> "b2g_addr" Then
                    sw.Append(vbCrLf & "// проверяем поле " + fld.Name + " ...")
                    sw.Append(vbCrLf & "if (typeof Check_" + fld.Name + " == 'function') {")
                    sw.Append(vbCrLf & "    var Pre_" + fld.Name + " = Check_" + fld.Name + "(TypeId);")
                    sw.Append(vbCrLf & "    if (Pre_" + fld.Name + " != """") PreQuery +=(""" + fld.Name + ":""+ Pre_" + fld.Name + "+"";"");")
                    sw.Append(vbCrLf & "}")

                    'End If

                End If

            Else


                'sw.Append(vbCrLf & "xtype:  'textfield',")
                'sw.Append(vbCrLf & "value:  'todo - refernce to object',")
            End If


            'If fld.AllowNull = MTZMetaModel.MTZMetaModel.enumBoolean.Boolean_Net Then
            '    sw.Append(vbCrLf & "allowBlank:false,")
            'Else
            '    sw.Append(vbCrLf & "allowBlank:true,")
            'End If

            'sw.Append(vbCrLf & "name:   '" & fld.Caption.ToLower() & "',")
            'sw.Append(vbCrLf & "fieldLabel:  '" & fld.Caption & "'")


        End If

        Return sw.ToString()
    End Function




    Private Function MakeJSFld(ByVal fld As MTZMetaModel.MTZMetaModel.FIELD) As String

        Dim sw As StringBuilder
        sw = New StringBuilder
        Dim k As Integer
        Dim ft As MTZMetaModel.MTZMetaModel.FIELDTYPE


        If fld.FieldGroupBox.ToLower() = "дополнительно" Or fld.FieldGroupBox.ToLower() = "характеристики" Then
            ft = fld.FieldType

            If fld.ReferenceType = MTZMetaModel.MTZMetaModel.enumReferenceType.ReferenceType_Skalyrnoe_pole_OPN_ne_ssilkaCLS Then

                If ft.Name.ToLower() = "masterstring" Then


                ElseIf ft.Name.ToLower() = "file" Then

                ElseIf ft.TypeStyle = MTZMetaModel.MTZMetaModel.enumTypeStyle.TypeStyle_Perecislenie Then

                    sw.Append(vbCrLf & "function Check_" + fld.Name + "(TypeId) {")
                    sw.Append(vbCrLf & "    if (document.getElementById(""" + fld.Name + """)) {")
                    sw.Append(vbCrLf & "        var sval = document.getElementById(""" + fld.Name + """);")
                    sw.Append(vbCrLf & "        var Pre" + fld.Name + " = sval.options[sval.selectedIndex].value;")
                    sw.Append(vbCrLf & "        if (Pre" + fld.Name + " == ""-1"") Pre" + fld.Name + " = """";")

                    sw.Append(vbCrLf & "        return Pre" + fld.Name + ";")
                    sw.Append(vbCrLf & "    } else {")
                    sw.Append(vbCrLf & "        return """";")
                    sw.Append(vbCrLf & "    }")
                    sw.Append(vbCrLf & "}")




                Else


                    If ft.GridSortType = MTZMetaModel.MTZMetaModel.enumColumnSortType.ColumnSortType_As_String Then

                        'If ft.Name.ToLower() = "password" Then
                        '    sw.Append(vbCrLf & "xtype:  'textfield',")
                        '    sw.Append(vbCrLf & "inputType:  'password',")
                        '    sw.Append(vbCrLf & "value:  '',")
                        'ElseIf ft.Name.ToLower() = "memo" Then
                        '    sw.Append(vbCrLf & "xtype:  'textareafield',")
                        '    sw.Append(vbCrLf & "value:  '',")
                        'Else
                        '    sw.Append(vbCrLf & "xtype:  'textfield',")
                        '    sw.Append(vbCrLf & "value:  '',")
                        'End If


                    End If

                    If ft.GridSortType = MTZMetaModel.MTZMetaModel.enumColumnSortType.ColumnSortType_As_Date Then
                        'sw.Append(vbCrLf & "xtype:  'datefield',")
                        'If FldReadOnly Then
                        '    sw.Append(vbCrLf & "hideTrigger: true,")
                        'End If
                        'sw.Append(vbCrLf & "value:  '',")
                    End If

                    If ft.GridSortType = MTZMetaModel.MTZMetaModel.enumColumnSortType.ColumnSortType_As_Numeric Then
                        'sw.Append(vbCrLf & "xtype:  'numberfield',")
                        'If FldReadOnly Then
                        '    sw.Append(vbCrLf & "hideTrigger: true,")
                        'End If
                        'sw.Append(vbCrLf & "value:  '0',")
                    End If
                End If

            ElseIf fld.ReferenceType = MTZMetaModel.MTZMetaModel.enumReferenceType.ReferenceType_Na_stroku_razdela Then
                Dim refP As MTZMetaModel.MTZMetaModel.PART
                refP = fld.RefToPart

                If Not refP Is Nothing Then


                    If refP.Name.ToLower <> "b2g_addr" Then
                        sw.Append(vbCrLf & "function Check_" + fld.Name + "(TypeId) {")
                        sw.Append(vbCrLf & "    if (document.getElementById(""" + fld.Name + """)) {")
                        sw.Append(vbCrLf & "        var sval = document.getElementById(""" + fld.Name + """);")
                        sw.Append(vbCrLf & "        var Pre" + fld.Name + " = sval.options[sval.selectedIndex].value;")
                        sw.Append(vbCrLf & "        if (Pre" + fld.Name + " == ""-1"") Pre" + fld.Name + " = """";")

                        sw.Append(vbCrLf & "        return Pre" + fld.Name + ";")
                        sw.Append(vbCrLf & "    } else {")
                        sw.Append(vbCrLf & "        return """";")
                        sw.Append(vbCrLf & "    }")
                        sw.Append(vbCrLf & "}")

                    End If

                End If

            Else


                'sw.Append(vbCrLf & "xtype:  'textfield',")
                'sw.Append(vbCrLf & "value:  'todo - refernce to object',")
            End If


            'If fld.AllowNull = MTZMetaModel.MTZMetaModel.enumBoolean.Boolean_Net Then
            '    sw.Append(vbCrLf & "allowBlank:false,")
            'Else
            '    sw.Append(vbCrLf & "allowBlank:true,")
            'End If

            'sw.Append(vbCrLf & "name:   '" & fld.Caption.ToLower() & "',")
            'sw.Append(vbCrLf & "fieldLabel:  '" & fld.Caption & "'")


        End If

        Return sw.ToString()
    End Function


    Private Function MakeJPHPModel(ByVal ID As Guid) As String


        Dim ot As MTZMetaModel.MTZMetaModel.OBJECTTYPE
        ot = model.OBJECTTYPE.Item(ID.ToString())

        Dim Jrnl As MTZJrnl.MTZJrnl.Application
        Dim rs As DataTable
        rs = Manager.Session.GetData("select instanceid from instance where objtype='mtzjrnl' and name='" + ot.Name + "'")

        If rs.Rows.Count = 0 Then Return ""
        Jrnl = Manager.GetInstanceObject(rs.Rows(0)("instanceid"))

        If Jrnl Is Nothing Then
            Return ""
        End If
        Dim sw As StringBuilder
        sw = New StringBuilder

        Dim i As Integer
        Dim j As Integer


        Dim flist As String


        Dim part As MTZMetaModel.MTZMetaModel.PART

        Dim fld As MTZMetaModel.MTZMetaModel.FIELD
        Dim ft As MTZMetaModel.MTZMetaModel.FIELDTYPE

        Dim pv As MTZMetaModel.MTZMetaModel.PARTVIEW

        Dim jr As MTZJrnl.MTZJrnl.Journal
        jr = Jrnl.Journal.Item(1)
        pv = model.FindRowObject("PartView", Jrnl.JournalSrc.Item(1).PartView)

        If jr Is Nothing Or pv Is Nothing Then
            Return ""
        End If

        part = pv.Parent.Parent
        flist = "x_" + pv.the_Alias + ".instanceid,id"

        For i = 1 To Jrnl.JournalColumn.Count
            ' formatting date column for journal
            If Jrnl.JournalColumn.Item(i).ColSort = MTZJrnl.MTZJrnl.enumColumnSortType.ColumnSortType_As_Date Then
                flist = flist & ",convert(varchar(100)," & Jrnl.JournalColumn.Item(i).JColumnSource.Item(1).ViewField.ToLower & ",111) " & Jrnl.JournalColumn.Item(i).JColumnSource.Item(1).ViewField.ToLower
            Else
                flist = flist & "," & Jrnl.JournalColumn.Item(i).JColumnSource.Item(1).ViewField.ToLower
                For j = 1 To pv.ViewColumn.Count
                    If pv.ViewColumn.Item(j).the_Alias.ToLower = Jrnl.JournalColumn.Item(i).JColumnSource.Item(1).ViewField.ToLower Then
                        fld = pv.ViewColumn.Item(j).Field
                        ft = fld.FieldType
                        If ft.TypeStyle = MTZMetaModel.MTZMetaModel.enumTypeStyle.TypeStyle_Ssilka Then
                            flist = flist & "," & Jrnl.JournalColumn.Item(i).JColumnSource.Item(1).ViewField.ToLower + "_id"
                        End If
                        If ft.TypeStyle = MTZMetaModel.MTZMetaModel.enumTypeStyle.TypeStyle_Perecislenie Then
                            flist = flist & "," & Jrnl.JournalColumn.Item(i).JColumnSource.Item(1).ViewField.ToLower + "_val"
                        End If
                        Exit For
                    End If
                Next

            End If

        Next

        sw.Append(vbCrLf & "<?php")

        sw.Append(vbCrLf & "class  M_search_" & ot.Name & " extends CI_Model {")
        sw.Append(vbCrLf & "    public function  __construct()")
        sw.Append(vbCrLf & "    {")
        sw.Append(vbCrLf & "         parent::__construct();")
        sw.Append(vbCrLf & "    }")


        sw.Append(vbCrLf & "    function getRows()")
        sw.Append(vbCrLf & "		{")
        sw.Append(vbCrLf & "            $query = $this->db2->query(' select " & flist & " from x_" & pv.the_Alias.ToLower() & "');")
        sw.Append(vbCrLf & "			if ($query->num_rows() == 0)")
        sw.Append(vbCrLf & "			{")
        sw.Append(vbCrLf & "                 $return= array('success'=>FALSE, 'msg' => 'No data found');")
        sw.Append(vbCrLf & "				return $return;")
        sw.Append(vbCrLf & "			}else{")
        sw.Append(vbCrLf & "				return $query->result();")
        sw.Append(vbCrLf & "			}")
        sw.Append(vbCrLf & "		}")

        sw.Append(vbCrLf & "  function getRow($ID)")
        sw.Append(vbCrLf & "  {")
        sw.Append(vbCrLf & "        $w ="" where id='"".$ID.""'"";")
        sw.Append(vbCrLf & "	    $sql='select  " & flist & " from x_" & pv.the_Alias.ToLower() & "'.$w;")
        sw.Append(vbCrLf & "	    $cn=$this->db2->db_pconnect(true);")
        sw.Append(vbCrLf & "	    $stmt= sqlsrv_query( $cn, $sql, array(), array( 'Scrollable' => 'static' ));")
        sw.Append(vbCrLf & "	    $result=array();")
        sw.Append(vbCrLf & "	    if($row=sqlsrv_fetch_array( $stmt, SQLSRV_FETCH_BOTH,SQLSRV_SCROLL_ABSOLUTE,0)){")
        sw.Append(vbCrLf & "		    $result[]=$row;")
        sw.Append(vbCrLf & "	    }else{")
        sw.Append(vbCrLf & "	    }")
        sw.Append(vbCrLf & "	    sqlsrv_free_stmt( $stmt);")
        sw.Append(vbCrLf & "	    sqlsrv_close( $cn);")
        sw.Append(vbCrLf & "	    $root->rows=$result;")
        sw.Append(vbCrLf & "	    return $root;")
        sw.Append(vbCrLf & "  }")

        sw.Append(vbCrLf & "  function getRowsSL($offset,$limit,$Q)")
        sw.Append(vbCrLf & "  {")

        sw.Append(vbCrLf & "            $params =explode("";"",$Q);")

        sw.Append(vbCrLf & "               $w ="" where 1=1 "";")
        sw.Append(vbCrLf & "        $cnt=count($params);")
        sw.Append(vbCrLf & "        for($i=0;$i<$cnt;$i++){")
        sw.Append(vbCrLf & "        	if($params[$i]!=''){")
        sw.Append(vbCrLf & "        		$p = explode(':',$params[$i]);")

        sw.Append(vbCrLf & "        		switch($p[0]){")

        sw.Append(vbCrLf & "        		 case 'TypeObj':")


        Select Case ot.Name.ToUpper

            Case "B2P"
                sw.Append(vbCrLf & "        		$w=$w."" and " + part.Name.ToLower() + "_" + "TTypeObjSecondId_id".ToLower() + " like '%"".$p[1].""%' "";")
                sw.Append(vbCrLf & "        		 break;")
                sw.Append(vbCrLf & "        		case 'MapL':")
                sw.Append(vbCrLf & "        		$w=$w."" and b2p_info_latitude >="".$p[1]."" and b2p_info_mapok_val=-1 "";")
                sw.Append(vbCrLf & "        		 break;")
                sw.Append(vbCrLf & "        		case 'MapR':")
                sw.Append(vbCrLf & "        		$w=$w."" and b2p_info_latitude <="".$p[1]."" and b2P_info_mapok_val=-1 "";")
                sw.Append(vbCrLf & "        		break;")
                sw.Append(vbCrLf & "        		case 'MapT':")
                sw.Append(vbCrLf & "        		$w=$w."" and b2p_info_longitude <="".$p[1]."" and b2P_info_mapok_val=-1 "";")
                sw.Append(vbCrLf & "        		 break;")
                sw.Append(vbCrLf & "        		case 'MapB':")
                sw.Append(vbCrLf & "        		$w=$w."" and b2p_info_longitude >="".$p[1]."" and b2p_info_mapok_val=-1 "";")
                sw.Append(vbCrLf & "        		break;")
            Case "B2S"
                sw.Append(vbCrLf & "        			$w=$w."" and " + part.Name.ToLower() + "_" + "TTypeObjSecondId_id".ToLower() + " like '%"".$p[1].""%' "";")
                sw.Append(vbCrLf & "        		 break;")
                sw.Append(vbCrLf & "        		case 'MapL':")
                sw.Append(vbCrLf & "        		$w=$w."" and b2s_info_latitude >="".$p[1]."" and b2s_info_mapok_val=-1 "";")
                sw.Append(vbCrLf & "        		 break;")
                sw.Append(vbCrLf & "        		case 'MapR':")
                sw.Append(vbCrLf & "        		$w=$w."" and b2s_info_latitude <="".$p[1]."" and b2s_info_mapok_val=-1 "";")
                sw.Append(vbCrLf & "        		break;")
                sw.Append(vbCrLf & "        		case 'MapT':")
                sw.Append(vbCrLf & "        		$w=$w."" and b2s_info_longitude <="".$p[1]."" and b2s_info_mapok_val=-1 "";")
                sw.Append(vbCrLf & "        		 break;")
                sw.Append(vbCrLf & "        		case 'MapB':")
                sw.Append(vbCrLf & "        		$w=$w."" and b2s_info_longitude >="".$p[1]."" and b2s_info_mapok_val=-1 "";")
                sw.Append(vbCrLf & "        		break;")



            Case "B2Z"
                sw.Append(vbCrLf & "        	    $w=$w."" and " + part.Name.ToLower() + "_" + "TTypeObjZagorodId_id".ToLower() + " like '%"".$p[1].""%' "";")
                sw.Append(vbCrLf & "        		 break;")
                sw.Append(vbCrLf & "        		case 'MapL':")
                sw.Append(vbCrLf & "        		$w=$w."" and b2z_info_latitude >="".$p[1]."" and b2z_info_mapok_val=-1 "";")
                sw.Append(vbCrLf & "        		 break;")
                sw.Append(vbCrLf & "        		case 'MapR':")
                sw.Append(vbCrLf & "        		$w=$w."" and b2z_info_latitude <="".$p[1]."" and b2z_info_mapok_val=-1 "";")
                sw.Append(vbCrLf & "        		break;")
                sw.Append(vbCrLf & "        		case 'MapT':")
                sw.Append(vbCrLf & "        		$w=$w."" and b2z_info_longitude <="".$p[1]."" and b2z_info_mapok_val=-1 "";")
                sw.Append(vbCrLf & "        		 break;")
                sw.Append(vbCrLf & "        		case 'MapB':")
                sw.Append(vbCrLf & "        		$w=$w."" and b2z_info_longitude >="".$p[1]."" and b2z_info_mapok_val=-1 "";")
                sw.Append(vbCrLf & "        		break;")


            Case "B2LR"
                sw.Append(vbCrLf & "        	    $w=$w."" and " + part.Name.ToLower() + "_" + "TTypeObjSecondId_id".ToLower() + " like '%"".$p[1].""%' "";")
                sw.Append(vbCrLf & "        		 break;")
                sw.Append(vbCrLf & "        		case 'MapL':")
                sw.Append(vbCrLf & "        		$w=$w."" and b2lr_info_latitude >="".$p[1]."" and b2lr_info_mapok_val=-1 "";")
                sw.Append(vbCrLf & "        		 break;")
                sw.Append(vbCrLf & "        		case 'MapR':")
                sw.Append(vbCrLf & "        		$w=$w."" and b2lr_info_latitude <="".$p[1]."" and b2lr_info_mapok_val=-1 "";")
                sw.Append(vbCrLf & "        		break;")
                sw.Append(vbCrLf & "        		case 'MapT':")
                sw.Append(vbCrLf & "        		$w=$w."" and b2lr_info_longitude <="".$p[1]."" and b2lr_info_mapok_val=-1 "";")
                sw.Append(vbCrLf & "        		 break;")
                sw.Append(vbCrLf & "        		case 'MapB':")
                sw.Append(vbCrLf & "        		$w=$w."" and b2lr_info_longitude >="".$p[1]."" and b2lr_info_mapok_val=-1 "";")
                sw.Append(vbCrLf & "        		break;")
            Case "B2FR"
                sw.Append(vbCrLf & "        		$w=$w."" and " + part.Name.ToLower() + "_" + "TypeId_id".ToLower() + " like '%"".$p[1].""%' "";")
                sw.Append(vbCrLf & "        		 break;")
                sw.Append(vbCrLf & "        		case 'MapL':")
                sw.Append(vbCrLf & "        		$w=$w."" and b2fr_info_latitude >="".$p[1]."" and b2fr_info_mapok_val=-1 "";")
                sw.Append(vbCrLf & "        		 break;")
                sw.Append(vbCrLf & "        		case 'MapR':")
                sw.Append(vbCrLf & "        		$w=$w."" and b2fr_info_latitude <="".$p[1]."" and b2fr_info_mapok_val=-1 "";")
                sw.Append(vbCrLf & "        		break;")
                sw.Append(vbCrLf & "        		case 'MapT':")
                sw.Append(vbCrLf & "        		$w=$w."" and b2fr_info_longitude <="".$p[1]."" and b2fr_info_mapok_val=-1 "";")
                sw.Append(vbCrLf & "        		 break;")
                sw.Append(vbCrLf & "        		case 'MapB':")
                sw.Append(vbCrLf & "        		$w=$w."" and b2fr_info_longitude >="".$p[1]."" and b2fr_info_mapok_val=-1 "";")
                sw.Append(vbCrLf & "        		break;")
            Case "B2C"
                sw.Append(vbCrLf & "        			$w=$w."" and " + part.Name.ToLower() + "_" + "TTypeObj_id".ToLower() + " like '%"".$p[1].""%' "";")
                sw.Append(vbCrLf & "        		 break;")
                sw.Append(vbCrLf & "        		case 'MapL':")
                sw.Append(vbCrLf & "        		$w=$w."" and b2C_info_latitude >="".$p[1]."" and b2C_info_mapok_val=-1 "";")
                sw.Append(vbCrLf & "        		 break;")
                sw.Append(vbCrLf & "        		case 'MapR':")
                sw.Append(vbCrLf & "        		$w=$w."" and b2C_info_latitude <="".$p[1]."" and b2C_info_mapok_val=-1 "";")
                sw.Append(vbCrLf & "        		break;")
                sw.Append(vbCrLf & "        		case 'MapT':")
                sw.Append(vbCrLf & "        		$w=$w."" and b2Cr_info_longitude <="".$p[1]."" and b2C_info_mapok_val=-1 "";")
                sw.Append(vbCrLf & "        		 break;")
                sw.Append(vbCrLf & "        		case 'MapB':")
                sw.Append(vbCrLf & "        		$w=$w."" and b2C_info_longitude >="".$p[1]."" and b2C_info_mapok_val=-1 "";")
                sw.Append(vbCrLf & "        		break;")

            Case Else


        End Select






        sw.Append(vbCrLf & "        		case 'PriceMin':")
        sw.Append(vbCrLf & "        			$w=$w."" and " + part.Name.ToLower() + "_amount>="".$p[1]."" "";")
        sw.Append(vbCrLf & "        			break;")
        sw.Append(vbCrLf & "        		case 'PriceMax':")
        sw.Append(vbCrLf & "        			$w=$w."" and " + part.Name.ToLower() + "_amount<="".$p[1]."" "";")
        sw.Append(vbCrLf & "        			break;")
        sw.Append(vbCrLf & "        		case 'SAllMin':")
        sw.Append(vbCrLf & "        			$w=$w."" and " + part.Name.ToLower() + "_sall>="".$p[1]."" "";")
        sw.Append(vbCrLf & "        			break;")
        sw.Append(vbCrLf & "        		case 'SAllMax':")
        sw.Append(vbCrLf & "        			$w=$w."" and " + part.Name.ToLower() + "_sall>="".$p[1]."" "";")
        sw.Append(vbCrLf & "        			break;")
        sw.Append(vbCrLf & "        		case 'Rooms':")
        sw.Append(vbCrLf & "        			$w=$w."" and " + part.Name.ToLower() + "_rooms in ("".$p[1]."") "";")
        sw.Append(vbCrLf & "        			break;")
        sw.Append(vbCrLf & "        		case 'RentRooms':")
        sw.Append(vbCrLf & "        			$w=$w."" and " + part.Name.ToLower() + "_rentrooms in ("".$p[1]."") "";")
        sw.Append(vbCrLf & "        			break;")
        sw.Append(vbCrLf & "        		case 'FloorsMin':")
        sw.Append(vbCrLf & "        		$w=$w."" and " + part.Name.ToLower() + "_floors>="".$p[1]."" "";")
        sw.Append(vbCrLf & "        			break;")
        sw.Append(vbCrLf & "        		case 'FloorsMax':")
        sw.Append(vbCrLf & "        		$w=$w."" and " + part.Name.ToLower() + "_floors<="".$p[1]."" "";")
        sw.Append(vbCrLf & "        			break;")
        sw.Append(vbCrLf & "        		case 'FloorMin':")
        sw.Append(vbCrLf & "        		$w=$w."" and " + part.Name.ToLower() + "_floor>="".$p[1]."" "";")
        sw.Append(vbCrLf & "        			break;")
        sw.Append(vbCrLf & "        		case 'FloorMax':")
        sw.Append(vbCrLf & "        		$w=$w."" and " + part.Name.ToLower() + "_floor<="".$p[1]."" "";")
        sw.Append(vbCrLf & "        			break;")

        
        sw.Append(vbCrLf & "        			case 'FloorNoFirst':")
        sw.Append(vbCrLf & "        			$w=$w."" and " + part.Name.ToLower() + "_floor<>1 "";")
        sw.Append(vbCrLf & "        			break;")
        sw.Append(vbCrLf & "        			case 'FloorNoLast':")
        sw.Append(vbCrLf & "        			$w=$w."" and ((" + part.Name.ToLower() + "_floors>0 and " + part.Name.ToLower() + "o_floor<>" + part.Name.ToLower() + "_floors) or " + part.Name.ToLower() + "_floors=0)  "";")
        sw.Append(vbCrLf & "        			break;")
       

        Select Case ot.Name.ToUpper

            Case "B2P"
                sw.Append(vbCrLf & "        			case 'OnlyPhoto':")
                sw.Append(vbCrLf & "        			$w=$w."" and ( x_auto" + part.Name.ToLower() + ".instanceid in (select distinct instanceid from b2p_photo))  "";")
                sw.Append(vbCrLf & "        			break;")

            Case "B2S"
                sw.Append(vbCrLf & "        			case 'OnlyPhoto':")
                sw.Append(vbCrLf & "        			$w=$w."" and ( x_auto" + part.Name.ToLower() + ".instanceid in (select distinct instanceid from b2s_photo))  "";")
                sw.Append(vbCrLf & "        			break;")

            Case "B2Z"
                sw.Append(vbCrLf & "        			case 'OnlyPhoto':")
                sw.Append(vbCrLf & "        			$w=$w."" and ( x_auto" + part.Name.ToLower() + ".instanceid in (select distinct instanceid from bz_photo))  "";")
                sw.Append(vbCrLf & "        			break;")

            Case "B2LR"
                sw.Append(vbCrLf & "        			case 'OnlyPhoto':")
                sw.Append(vbCrLf & "        			$w=$w."" and ( x_auto" + part.Name.ToLower() + ".instanceid in (select distinct instanceid from b2lr_photo))  "";")
                sw.Append(vbCrLf & "        			break;")

            Case "B2FR"
                sw.Append(vbCrLf & "        			case 'OnlyPhoto':")
                sw.Append(vbCrLf & "        			$w=$w."" and ( x_auto" + part.Name.ToLower() + ".instanceid in (select distinct instanceid from b2fr_photo))  "";")
                sw.Append(vbCrLf & "        			break;")
            Case "B2C"
                sw.Append(vbCrLf & "        			case 'OnlyPhoto':")
                sw.Append(vbCrLf & "        			$w=$w."" and ( x_auto" + part.Name.ToLower() + ".instanceid in (select distinct instanceid from b2c_photo))  "";")
                sw.Append(vbCrLf & "        			break;")


            Case Else


        End Select



        sw.Append(vbCrLf & "        		case 'Company':")
        sw.Append(vbCrLf & "        			$w=$w."" and " + part.Name.ToLower() + "_firmid_id='"".$p[1].""' "";")
        sw.Append(vbCrLf & "        			break;")
        sw.Append(vbCrLf & "        		case 'Districts':")
        sw.Append(vbCrLf & "        			$w=$w."" and " + part.Name.ToLower() + "_districtid_id in (select b2g_addrid from b2g_addr where treecode='"".$p[1].""') "";")
        sw.Append(vbCrLf & "        			break;")
        sw.Append(vbCrLf & "        		case 'District':")
        sw.Append(vbCrLf & "        			$w=$w."" and " + part.Name.ToLower() + "_districtid_id in (select b2g_addrid from b2g_addr where treecode='"".$p[1].""') "";")
        sw.Append(vbCrLf & "        			break;")

        sw.Append(vbCrLf & "        		case 'Subway':")


        Select Case ot.Name.ToUpper
            Case "B2P"
                sw.Append(vbCrLf & "        			$w=$w."" and " + part.Name.ToLower() + "_" + "Subway_id".ToLower() + " like '%"".$p[1].""%' "";")
            Case "B2S"
                sw.Append(vbCrLf & "        			$w=$w."" and " + part.Name.ToLower() + "_" + "Subway_id".ToLower() + " like '%"".$p[1].""%' "";")
            Case "B2Z"
                sw.Append(vbCrLf & "        			$w=$w."" and " + part.Name.ToLower() + "_" + "TTypeObjZagorodId_id".ToLower() + " like '%"".$p[1].""%' "";")

            Case "B2LR"
                sw.Append(vbCrLf & "        			$w=$w."" and " + part.Name.ToLower() + "_" + "Subway_id".ToLower() + " like '%"".$p[1].""%' "";")
            Case "B2FR"
                sw.Append(vbCrLf & "        			$w=$w."" and " + part.Name.ToLower() + "_" + "TypeId_id".ToLower() + " like '%"".$p[1].""%' "";")
            Case "B2C"
                sw.Append(vbCrLf & "        			$w=$w."" and " + part.Name.ToLower() + "_" + "Subway_id".ToLower() + " like '%"".$p[1].""%' "";")


            Case Else


        End Select

        'sw.Append(vbCrLf & "        		    $w=$w."" and " + part.Name.ToLower() + "_subway_id like '%"".$p[1].""%'"";")
        sw.Append(vbCrLf & "        			break;")


        For i = 1 To Jrnl.JournalColumn.Count
            ' formatting date column for journal

            For j = 1 To pv.ViewColumn.Count
                If pv.ViewColumn.Item(j).the_Alias = Jrnl.JournalColumn.Item(i).JColumnSource.Item(1).ViewField Then
                    fld = pv.ViewColumn.Item(j).Field
                    ft = fld.FieldType

                    If ft.TypeStyle = MTZMetaModel.MTZMetaModel.enumTypeStyle.TypeStyle_Ssilka Then
                        sw.Append(vbCrLf & "        		case '" + fld.Name + "':")
                        If ft.Name.ToLower <> "multiref" Then
                            sw.Append(vbCrLf & "        		    $w=$w."" and " + pv.ViewColumn.Item(j).the_Alias + "_id = '"".$p[1].""'"";")
                        Else
                            sw.Append(vbCrLf & "        		    $w=$w."" and " + pv.ViewColumn.Item(j).the_Alias + "_id like '%"".$p[1].""%'"";")
                        End If

                        sw.Append(vbCrLf & "        			break;")
                    End If


                    If ft.TypeStyle = MTZMetaModel.MTZMetaModel.enumTypeStyle.TypeStyle_Perecislenie Then
                        sw.Append(vbCrLf & "        		case '" + fld.Name + "':")

                        sw.Append(vbCrLf & "        		    $w=$w."" and " + pv.ViewColumn.Item(j).the_Alias + "_val = '"".$p[1].""'"";")

                        sw.Append(vbCrLf & "        			break;")
                    End If

                    If ft.TypeStyle = MTZMetaModel.MTZMetaModel.enumTypeStyle.TypeStyle_Skalyrniy_tip Then

                        If Jrnl.JournalColumn.Item(i).ColSort = MTZJrnl.MTZJrnl.enumColumnSortType.ColumnSortType_As_Date Then
                            sw.Append(vbCrLf & "        		case '" + fld.Name + "':")

                            sw.Append(vbCrLf & "        		    $w=$w."" and convert(varchar(100)," + pv.ViewColumn.Item(j).the_Alias + ",111) = convert(varchar(100),'"".$p[1].""',111)"";")

                            sw.Append(vbCrLf & "        			break;")
                        ElseIf Jrnl.JournalColumn.Item(i).ColSort = MTZJrnl.MTZJrnl.enumColumnSortType.ColumnSortType_As_Numeric Then
                            sw.Append(vbCrLf & "        		case '" + fld.Name + "':")

                            sw.Append(vbCrLf & "        		    $w=$w."" and " + pv.ViewColumn.Item(j).the_Alias + " = "".$p[1]."" "";")

                            sw.Append(vbCrLf & "        			break;")
                        Else
                            sw.Append(vbCrLf & "        		case '" + fld.Name + "':")

                            sw.Append(vbCrLf & "        		    $w=$w."" and " + pv.ViewColumn.Item(j).the_Alias + " like '%"".$p[1].""%'"";")

                            sw.Append(vbCrLf & "        			break;")
                        End If



                    End If


                    Exit For
                End If
            Next




        Next

        'sw.Append(vbCrLf & "        		default:")
        'sw.Append(vbCrLf & "        			$w=$w."" and " + part.name.tolower() +"_"".$p[0].""='"".$p[1].""' "";")
        sw.Append(vbCrLf & "        		}")
        sw.Append(vbCrLf & "        	}")
        sw.Append(vbCrLf & "        }")




        sw.Append(vbCrLf & "	$sql1='select count(*) as total  from x_" & pv.the_Alias.ToLower() & "'.$w;")
        'sw.Append(vbCrLf & "	$sql='select  " & flist & " from x_" & pv.the_Alias.ToLower() & "'.$w;")

        Select Case ot.Name.ToUpper
            Case "B2P"
                sw.Append(vbCrLf & "	$sql='select " & flist & ",isnull(b2p_photo.filename,\'\') filename from x_" & pv.the_Alias.ToLower() & " left join b2p_photo on x_" & pv.the_Alias.ToLower() & ".instanceid=b2p_photo.instanceid and b2p_photo.ismain=-1 '.$w;")
            Case "B2S"
                sw.Append(vbCrLf & "	$sql='select " & flist & ",isnull(b2s_photo.filename,\'\') filename from x_" & pv.the_Alias.ToLower() & " left join b2s_photo on x_" & pv.the_Alias.ToLower() & ".instanceid=b2s_photo.instanceid and b2s_photo.ismain=-1 '.$w;")
            Case "B2Z"
                sw.Append(vbCrLf & "	$sql='select " & flist & ",isnull(b2z_photo.filename,\'\') filename from x_" & pv.the_Alias.ToLower() & " left join b2z_photo on x_" & pv.the_Alias.ToLower() & ".instanceid=b2z_photo.instanceid and b2z_photo.ismain=-1 '.$w;")
            Case "B2LR"
                sw.Append(vbCrLf & "	$sql='select " & flist & ",isnull(b2lr_photo.filename,\'\') filename from x_" & pv.the_Alias.ToLower() & " left join b2lr_photo on x_" & pv.the_Alias.ToLower() & ".instanceid=b2lr_photo.instanceid and b2lr_photo.ismain=-1 '.$w;")
            Case "B2FR"
                sw.Append(vbCrLf & "	$sql='select " & flist & ",isnull(b2fr_photo.filename,\'\') filename from x_" & pv.the_Alias.ToLower() & " left join b2fr_photo on x_" & pv.the_Alias.ToLower() & ".instanceid=b2fr_photo.instanceid and b2fr_photo.ismain=-1 '.$w;")
            Case "B2C"
                sw.Append(vbCrLf & "	$sql='select " & flist & ",isnull(b2c_photo.filename,\'\') filename from x_" & pv.the_Alias.ToLower() & " left join b2c_photo on x_" & pv.the_Alias.ToLower() & ".instanceid=b2c_photo.instanceid and b2c_photo.ismain=-1 '.$w;")


            Case Else
                sw.Append(vbCrLf & "	$sql='select  " & flist & " from x_" & pv.the_Alias.ToLower() & "'.$w;")

        End Select


        sw.Append(vbCrLf & "	$cn=$this->db2->db_pconnect(true);")
        sw.Append(vbCrLf & "	$stmt= sqlsrv_query( $cn, $sql1, array(), array( 'Scrollable' => 'static' ));")

        sw.Append(vbCrLf & "	if($row=sqlsrv_fetch_array( $stmt, SQLSRV_FETCH_ASSOC)){")
        sw.Append(vbCrLf & "		$root->total=$row['total'];")
        sw.Append(vbCrLf & "		$root->sucess=true;")
        sw.Append(vbCrLf & "	}else{")
        sw.Append(vbCrLf & "		$root->total=0;")
        sw.Append(vbCrLf & "		$root->sucess=false;")
        sw.Append(vbCrLf & "	}")

        sw.Append(vbCrLf & "	sqlsrv_free_stmt( $stmt);")

        sw.Append(vbCrLf & "	$stmt= sqlsrv_query( $cn, $sql, array(), array( 'Scrollable' => 'static' ));")
        sw.Append(vbCrLf & "	$result=array();")
        sw.Append(vbCrLf & "	if($row=sqlsrv_fetch_array( $stmt, SQLSRV_FETCH_BOTH,SQLSRV_SCROLL_ABSOLUTE,$offset)){")
        sw.Append(vbCrLf & "		$cnt=1;")
        sw.Append(vbCrLf & "		$result[]=$row;")
        sw.Append(vbCrLf & "		while(  $cnt<$limit)")
        sw.Append(vbCrLf & "		{")
        sw.Append(vbCrLf & "		  $cnt=$cnt+1;")
        sw.Append(vbCrLf & "		  if ($row = sqlsrv_fetch_array( $stmt, SQLSRV_FETCH_BOTH,SQLSRV_SCROLL_NEXT)){")
        sw.Append(vbCrLf & "			$result[]=$row;")
        'sw.Append(vbCrLf & "			log_message('debug',$row['id']);")
        sw.Append(vbCrLf & "		  }else{")
        sw.Append(vbCrLf & "		    $root->sucess=false;")
        sw.Append(vbCrLf & "			break;")
        sw.Append(vbCrLf & "		  }")
        sw.Append(vbCrLf & "		}")
        sw.Append(vbCrLf & "	}else{")
        sw.Append(vbCrLf & "	}")
        sw.Append(vbCrLf & "	sqlsrv_free_stmt( $stmt);")
        sw.Append(vbCrLf & "	sqlsrv_close( $cn);")
        sw.Append(vbCrLf & "	$root->rows=$result;")
        sw.Append(vbCrLf & "	return $root;")

        sw.Append(vbCrLf & "}")

        sw.Append(vbCrLf & "function getRowsMAP($Q){")
        sw.Append(vbCrLf & "    return $this->getRowsSL(0,500,$Q);")
        sw.Append(vbCrLf & "}")

        Dim tt As String


        If ot.Name.ToUpper = "B2S" Or ot.Name.ToUpper = "B2Z" Or ot.Name.ToUpper = "B2C" Or ot.Name.ToUpper = "B2FR" Or ot.Name.ToUpper = "B2P" Or ot.Name.ToUpper = "B2LR" Then
            tt = ot.Name.ToUpper


            sw.Append(vbCrLf & "function getPhotos($ID)")
            sw.Append(vbCrLf & "{")
            sw.Append(vbCrLf & "   log_message('debug','getPhotos');")
            sw.Append(vbCrLf & "   $w ="" where " + tt + "_infoid='"".$ID.""'"";")
            sw.Append(vbCrLf & "   $sql='select   " + tt + "_photo.instanceid, " + tt + "_photo." + tt + "_photoid, " + tt + "_photo.filename, " + tt + "_photo.ismain from " + tt + "_photo join " + tt + "_info on " + tt + "_info.instanceid=" + tt + "_photo.instanceid '.$w;")
            sw.Append(vbCrLf & "   $cn=$this->db2->db_pconnect(true);")
            sw.Append(vbCrLf & "   $stmt= sqlsrv_query( $cn, $sql, array(), array( 'Scrollable' => 'static' ));")
            sw.Append(vbCrLf & "   $result=array();")
            sw.Append(vbCrLf & "   $cnt=0;")
            sw.Append(vbCrLf & "   if($row=sqlsrv_fetch_array( $stmt, SQLSRV_FETCH_BOTH,SQLSRV_SCROLL_ABSOLUTE,0)){")
            sw.Append(vbCrLf & "	 $cnt=1;")
            sw.Append(vbCrLf & "	 $result[]=$row;")
            sw.Append(vbCrLf & "     While (True)")
            sw.Append(vbCrLf & "	 {")
            sw.Append(vbCrLf & "		$cnt=$cnt+1;")
            sw.Append(vbCrLf & "	    if ($row = sqlsrv_fetch_array( $stmt, SQLSRV_FETCH_BOTH,SQLSRV_SCROLL_NEXT)){")
            sw.Append(vbCrLf & "		  $result[]=$row;")
            sw.Append(vbCrLf & "	    }else{")
            sw.Append(vbCrLf & "		  break;")
            sw.Append(vbCrLf & "	    }")
            sw.Append(vbCrLf & "	 }")
            sw.Append(vbCrLf & "   }")
            sw.Append(vbCrLf & "   sqlsrv_free_stmt( $stmt);")
            sw.Append(vbCrLf & "   sqlsrv_close( $cn);")
            sw.Append(vbCrLf & "   $root->rows=$result;")
            sw.Append(vbCrLf & "   log_message('debug','getPhotos '. $cnt. ' items loaded');")
            sw.Append(vbCrLf & "   return $root;")
            sw.Append(vbCrLf & "}")

            sw.Append(vbCrLf & "function getMainPhoto($ID)")
            sw.Append(vbCrLf & "{")
            sw.Append(vbCrLf & "   log_message('debug','getPhotos');")
            sw.Append(vbCrLf & "   $w ="" where " + tt + "_photo.ismain=-1 and " + tt + "_infoid='"".$ID.""'"";")
            sw.Append(vbCrLf & "   $sql='select   " + tt + "_photo.instanceid, " + tt + "_photo." + tt + "_photoid, " + tt + "_photo.filename, " + tt + "_photo.ismain from " + tt + "_photo join " + tt + "_info on " + tt + "_info.instanceid=" + tt + "_photo.instanceid '.$w;")
            sw.Append(vbCrLf & "   $cn=$this->db2->db_pconnect(true);")
            sw.Append(vbCrLf & "   $stmt= sqlsrv_query( $cn, $sql, array(), array( 'Scrollable' => 'static' ));")
            sw.Append(vbCrLf & "   $result=array();")
            sw.Append(vbCrLf & "   $cnt=0;")
            sw.Append(vbCrLf & "   if($row=sqlsrv_fetch_array( $stmt, SQLSRV_FETCH_BOTH,SQLSRV_SCROLL_ABSOLUTE,0)){")
            sw.Append(vbCrLf & "	  $cnt=1;")
            sw.Append(vbCrLf & "	  $result[]=$row;")
            sw.Append(vbCrLf & "   }")
            sw.Append(vbCrLf & "   sqlsrv_free_stmt( $stmt);")
            sw.Append(vbCrLf & "   sqlsrv_close( $cn);")
            sw.Append(vbCrLf & "   $root->rows=$result;")
            sw.Append(vbCrLf & "   log_message('debug','getPhotos '. $cnt. ' items loaded');")
            sw.Append(vbCrLf & "   return $root;")
            sw.Append(vbCrLf & "}")

            sw.Append(vbCrLf & "function getContacts($ID)")
            sw.Append(vbCrLf & "{")
            sw.Append(vbCrLf & "   log_message('debug','getcontacts');")
            sw.Append(vbCrLf & "   $w ="" where " + tt + "_infoid='"".$ID.""'"";")
            sw.Append(vbCrLf & "   $sql='select   " + tt + "_contacts.instanceid, " + tt + "_contacts." + tt + "_contactsid, " + tt + "_contacts.owner, " + tt + "_contacts.ownercontact from " + tt + "_contacts join " + tt + "_info on " + tt + "_info.instanceid=" + tt + "_contacts.instanceid '.$w;")
            sw.Append(vbCrLf & "   $cn=$this->db2->db_pconnect(true);")
            sw.Append(vbCrLf & "   $stmt= sqlsrv_query( $cn, $sql, array(), array( 'Scrollable' => 'static' ));")
            sw.Append(vbCrLf & "   $result=array();")
            sw.Append(vbCrLf & "   $cnt=0;")
            sw.Append(vbCrLf & "   if($row=sqlsrv_fetch_array( $stmt, SQLSRV_FETCH_BOTH,SQLSRV_SCROLL_ABSOLUTE,0)){")
            sw.Append(vbCrLf & "	 $cnt=1;")
            sw.Append(vbCrLf & "	 $result[]=$row;")
            sw.Append(vbCrLf & "     while (True)")
            sw.Append(vbCrLf & "	 {")
            sw.Append(vbCrLf & "		$cnt=$cnt+1;")
            sw.Append(vbCrLf & "	    if ($row = sqlsrv_fetch_array( $stmt, SQLSRV_FETCH_BOTH,SQLSRV_SCROLL_NEXT)){")
            sw.Append(vbCrLf & "		  $result[]=$row;")
            sw.Append(vbCrLf & "	    }else{")
            sw.Append(vbCrLf & "		  break;")
            sw.Append(vbCrLf & "	    }")
            sw.Append(vbCrLf & "	  }")
            sw.Append(vbCrLf & "     }")
            sw.Append(vbCrLf & "    sqlsrv_free_stmt( $stmt);")
            sw.Append(vbCrLf & "    sqlsrv_close( $cn);")
            sw.Append(vbCrLf & "    $root->rows=$result;")
            sw.Append(vbCrLf & "    log_message('debug','getcontacts '. $cnt. ' items loaded');")
            sw.Append(vbCrLf & "    return $root;")
            sw.Append(vbCrLf & "}")
        End If


        sw.Append(vbCrLf & "}")
        sw.Append(vbCrLf & "?>")

        MakeProjectFile(sw.ToString(), textBoxOutPutFolder.Text & "models\", "m_search_" + ot.Name.ToLower() & ".php")

        Return sw.ToString()
    End Function

    Private Function MakeJPHPController(ByVal ID As Guid) As String


        Dim ot As MTZMetaModel.MTZMetaModel.OBJECTTYPE
        ot = model.OBJECTTYPE.Item(ID.ToString())

        Dim Jrnl As MTZJrnl.MTZJrnl.Application
        Dim rs As DataTable
        rs = Manager.Session.GetData("select instanceid from instance where objtype='mtzjrnl' and name='" + ot.Name + "'")

        If rs.Rows.Count = 0 Then Return ""
        Jrnl = Manager.GetInstanceObject(rs.Rows(0)("instanceid"))
        If Jrnl Is Nothing Then
            Return ""
        End If
        Dim sw As StringBuilder
        sw = New StringBuilder

        Dim i As Integer


        Dim flist As String
        flist = "instanceid,id"


        Dim pv As MTZMetaModel.MTZMetaModel.PARTVIEW

        Dim p As MTZJrnl.MTZJrnl.Journal
        p = Jrnl.Journal.Item(1)
        pv = model.FindRowObject("PartView", Jrnl.JournalSrc.Item(1).PartView)

        If p Is Nothing Or pv Is Nothing Then
            Return ""
        End If


        sw.Append(vbCrLf & "    <?php")
        sw.Append(vbCrLf & "	 class C_search_" & ot.Name.ToLower() & " extends CI_Controller {")

        sw.Append(vbCrLf & "    var $CI;")

        sw.Append(vbCrLf & "    function __construct() {")
        sw.Append(vbCrLf & "         parent::__construct();")
        sw.Append(vbCrLf & "         $this->CI = & get_instance();")
        sw.Append(vbCrLf & "         $this->CI->db2 = $this->CI->load->database('b2', TRUE);  ")

        sw.Append(vbCrLf & "        $this->CI->load->model('M_search_" & ot.Name.ToLower() & "', 'm_search_" & ot.Name.ToLower() & "');")
        sw.Append(vbCrLf & "    }")

        sw.Append(vbCrLf & "    function getRows() {")
        sw.Append(vbCrLf & "            log_message('debug', '" & ot.Name.ToLower() & ".getRows post : '.json_encode($this->input->post(NULL, FALSE)));")
        sw.Append(vbCrLf & "           $start=$this->input->get('start', FALSE);")
        sw.Append(vbCrLf & "           if( $start==FALSE)  $start=0;")
        sw.Append(vbCrLf & "           $limit=$this->input->get('limit', FALSE);")
        sw.Append(vbCrLf & "           if ($limit==FALSE) $limit=100;")
        sw.Append(vbCrLf & "           $params=$this->input->get('Q', FALSE);")
        sw.Append(vbCrLf & "           $" & ot.Name.ToLower() & "= $this->m_search_" & ot.Name.ToLower() & "->getRowsSL($start,$limit,$params);")
        sw.Append(vbCrLf & "           print json_encode($" & ot.Name.ToLower() & ");")
        sw.Append(vbCrLf & "    }")


        sw.Append(vbCrLf & "function index(){")
        sw.Append(vbCrLf & "           $pageID=$this->input->get('pageID', FALSE);")
        sw.Append(vbCrLf & "            if($pageID==FALSE){")
        sw.Append(vbCrLf & "             $start=$this->input->get('start', FALSE);")
        sw.Append(vbCrLf & "             if( $start==FALSE)  $start=0;")
        sw.Append(vbCrLf & "             $limit=$this->input->get('limit', FALSE);")
        sw.Append(vbCrLf & "             if ($limit==FALSE) $limit=100;")
        sw.Append(vbCrLf & "            }else{")
        sw.Append(vbCrLf & "           $start=25 *($pageID-1);")
        sw.Append(vbCrLf & "           $limit=25;")
        sw.Append(vbCrLf & "            }")
        sw.Append(vbCrLf & "           $params=$this->input->get('Q', FALSE);")
        sw.Append(vbCrLf & "           $data['posts']= $this->m_search_" & ot.Name.ToLower() & "->getRowsSL($start,$limit,$params);")
        sw.Append(vbCrLf & "           $this->load->view('result_" & ot.Name.ToLower() & "',$data);")
        sw.Append(vbCrLf & "}")
        sw.Append(vbCrLf & "}")

        MakeProjectFile(sw.ToString(), textBoxOutPutFolder.Text & "controllers\", "c_search_" + ot.Name.ToLower() + ".php")

        Return sw.ToString()
    End Function


    Private Function MakeFPHPController(ByVal ID As Guid) As String


        Dim ot As MTZMetaModel.MTZMetaModel.OBJECTTYPE
        ot = model.OBJECTTYPE.Item(ID.ToString())

        Dim Jrnl As MTZJrnl.MTZJrnl.Application
        Dim rs As DataTable
        rs = Manager.Session.GetData("select instanceid from instance where objtype='mtzjrnl' and name='" + ot.Name + "'")

        If rs.Rows.Count = 0 Then Return ""
        Jrnl = Manager.GetInstanceObject(rs.Rows(0)("instanceid"))
        If Jrnl Is Nothing Then
            Return ""
        End If
        Dim sw As StringBuilder
        sw = New StringBuilder

        Dim i As Integer


        Dim flist As String
        flist = "instanceid,id"


        Dim pv As MTZMetaModel.MTZMetaModel.PARTVIEW

        Dim p As MTZJrnl.MTZJrnl.Journal
        p = Jrnl.Journal.Item(1)
        pv = model.FindRowObject("PartView", Jrnl.JournalSrc.Item(1).PartView)

        If p Is Nothing Or pv Is Nothing Then
            Return ""
        End If


        sw.Append(vbCrLf & "    <?php")
        sw.Append(vbCrLf & "	 class C_form_" & ot.Name.ToLower() & " extends CI_Controller {")

        sw.Append(vbCrLf & "    var $CI;")

        sw.Append(vbCrLf & "    function __construct() {")
        sw.Append(vbCrLf & "         parent::__construct();")
        sw.Append(vbCrLf & "         $this->CI = & get_instance();")
        sw.Append(vbCrLf & "         $this->CI->db2 = $this->CI->load->database('b2', TRUE);  ")
        sw.Append(vbCrLf & "         $this->CI->load->model('M_search_" & ot.Name.ToLower() & "', 'm_search_" & ot.Name.ToLower() & "');")
        sw.Append(vbCrLf & "    }")


        sw.Append(vbCrLf & "function index(){")
        sw.Append(vbCrLf & "    $ID=$this->input->get('ID', FALSE);")

        sw.Append(vbCrLf & "    $data['posts']= $this->m_search_" & ot.Name.ToLower() & "->getRow($ID);")

        If ot.Name.ToUpper = "B2S" Or ot.Name.ToUpper = "B2Z" Or ot.Name.ToUpper = "B2C" Or ot.Name.ToUpper = "B2FR" Or ot.Name.ToUpper = "B2P" Or ot.Name.ToUpper = "B2LR" Then
            sw.Append(vbCrLf & "    $data['photos']= $this->m_search_" & ot.Name.ToLower() & "->getPhotos($ID);")
            sw.Append(vbCrLf & "    $data['contacts']= $this->m_search_" & ot.Name.ToLower() & "->getContacts($ID);")
        End If

        sw.Append(vbCrLf & "    $this->load->view('form_" & ot.Name.ToLower() & "',$data);")

        'sw.Append(vbCrLf & "           $ID=$this->input->get('ID', FALSE);")

        'sw.Append(vbCrLf & "           $data['posts']= $this->m_search_" & ot.Name.ToLower() & "->getRow($ID);")
        'sw.Append(vbCrLf & "           $this->load->view('form_" & ot.Name.ToLower() & "',$data);")
        sw.Append(vbCrLf & "}")

        sw.Append(vbCrLf & "    private function _loadModels () {")
        sw.Append(vbCrLf & "        $this->load->model('M_search_" & ot.Name.ToLower() & "', 'm_search_" & ot.Name.ToLower() & "');")
        sw.Append(vbCrLf & "    }")
        sw.Append(vbCrLf & "}")

        MakeProjectFile(sw.ToString(), textBoxOutPutFolder.Text & "controllers\", "c_form_" + ot.Name.ToLower() + ".php")

        Return sw.ToString()
    End Function


    Private Function MakeJPHPViews(ByVal ID As Guid) As String


        Dim ot As MTZMetaModel.MTZMetaModel.OBJECTTYPE
        ot = model.OBJECTTYPE.Item(ID.ToString())
        Dim tabtemplate As String
        Dim mytemplate As String

        Try
            tabtemplate = File.ReadAllText(txtTabTemplate.Text.Replace("%TYPE%", ot.Name), System.Text.Encoding.UTF8)
        Catch ex As Exception
            tabtemplate = File.ReadAllText(txtTabTemplate.Text.Replace("%TYPE%", ""), System.Text.Encoding.UTF8)
        End Try

        tabtemplate = tabtemplate.Replace("%THEME%", cmbTheme.Text.Trim)
        mytemplate = tabtemplate.Replace("%MARKET%", ot.the_Comment)
        mytemplate = mytemplate.Replace("%TYPE%", ot.Name)


       

       
        Dim Jrnl As MTZJrnl.MTZJrnl.Application
        Dim rs As DataTable
        rs = Manager.Session.GetData("select instanceid from instance where objtype='mtzjrnl' and name='" + ot.Name + "'")

        If rs.Rows.Count = 0 Then Return ""
        Jrnl = Manager.GetInstanceObject(rs.Rows(0)("instanceid"))
        If Jrnl Is Nothing Then
            Return ""
        End If

    

        Dim sw As StringBuilder


        Dim i As Integer


        Dim flist As String
        flist = "instanceid,id"


        Dim pv As MTZMetaModel.MTZMetaModel.PARTVIEW

        Dim p As MTZJrnl.MTZJrnl.Journal
        p = Jrnl.Journal.Item(1)
        pv = model.FindRowObject("PartView", Jrnl.JournalSrc.Item(1).PartView)

        If p Is Nothing Or pv Is Nothing Then
            Return ""
        End If

        Jrnl.JournalColumn.Sort = "sequence"

        ' HEADER
        sw = New StringBuilder
        For i = 1 To Jrnl.JournalColumn.Count
            sw.Append(vbCrLf & "<TH>" + Jrnl.JournalColumn.Item(i).name)
            sw.Append(vbCrLf & "</TH>")
        Next


       
        mytemplate = mytemplate.Replace("%HEADER%", sw.ToString())


        'ROWS
        sw = New StringBuilder

        sw.Append(vbCrLf & "<?php foreach($posts->rows as $entry): ?>")

        sw.Append(vbCrLf & "<tr >")
        sw.Append(vbCrLf & " <?php ")

        For i = 1 To Jrnl.JournalColumn.Count
            ' formatting date column for journal

            sw.Append(vbCrLf & "	echo '<td>';")
            If i = 1 Then
                sw.Append(vbCrLf & "    echo '<A href=""../card_b2/" + ot.Name + "?ID='.$entry['id'].'"">&nbsp;'.$entry['" + Jrnl.JournalColumn.Item(i).JColumnSource.Item(1).ViewField.ToLower + "'].'</A>';")
            Else
                sw.Append(vbCrLf & "    echo '&nbsp;'.$entry['" + Jrnl.JournalColumn.Item(i).JColumnSource.Item(1).ViewField.ToLower + "'];")
            End If


            sw.Append(vbCrLf & "	echo '</td>';")

        Next


        sw.Append(vbCrLf & "   ?>")

        sw.Append(vbCrLf & "</tr>")

        sw.Append(vbCrLf & "<?php endforeach; ?>")

        mytemplate = mytemplate.Replace("%ROWS%", sw.ToString())





        MakeProjectFile(mytemplate, textBoxOutPutFolder.Text & "views\", "result_" + ot.Name.ToLower() + ".php")

        Return sw.ToString()
    End Function

End Class