Public Class frmSetUnique

    Private objectTypes As Hashtable
    Private Sub frmSetUnique_Load(sender As Object, e As System.EventArgs) Handles Me.Load
      
        Dim i As Long
      
        objectTypes = New Hashtable
        Dim dt_ot As DataTable
        Dim doc_app As bp3doc.bp3doc.Application
        dt_ot = Manager.Session.GetData("select " + Manager.Session.GetProvider().ID2Base("instanceid", "instanceid") + " from bp3doc_def order by name ")


        For i = 0 To dt_ot.Rows.Count - 1
            doc_app = Manager.GetInstanceObject(New Guid(dt_ot.Rows(i)("instanceid").ToString()))
            objectTypes.Add(doc_app.bp3doc_def.Item(1).Name, doc_app)
            chkTypes.Items.Add(doc_app.bp3doc_def.Item(1).Name, False)
        Next
     
    End Sub

    Private Sub cmdMakeUniq_Click(sender As System.Object, e As System.EventArgs) Handles cmdMakeUniq.Click
      
        If (chkTypes.CheckedItems.Count > 0) Then
            Dim i As Integer
            Dim j As Integer
            Dim k As Integer
            Dim TypeID As String
            Dim ObjType As bp3doc.bp3doc.Application

            For j = 0 To chkTypes.CheckedItems.Count - 1
                ObjType = objectTypes.Item(chkTypes.CheckedItems().Item(j))
                TypeID = ObjType.ID.ToString()

                Dim dt As DataTable
                dt = Manager.Session.GetData("select name from part where name like'" + txtPart.Text + "'")
                For i = 0 To dt.Rows.Count - 1
                    For k = 1 To ObjType.bp3doc_store.Count
                        If ObjType.bp3doc_store.Item(k).Name = dt.Rows(i)("Name") Then
                            MakeUniqueFor(ObjType.bp3doc_store.Item(k))
                            Exit For
                        End If
                    Next
                Next

            Next

        Else
            MsgBox("Укажите тип объекта")
        End If
    
    End Sub

    Private Sub MakeUniqueFor(ByRef p As bp3doc.bp3doc.bp3doc_store)
        Dim i As Integer
        Dim fld As bp3doc.bp3doc.bp3doc_field
        Dim uc As bp3doc.bp3doc.bp3doc_uk
        Dim cf As bp3doc.bp3doc.bp3doc_ukfld
        Dim ot As bp3doc.bp3doc.Application
        ot = p.Application
        For i = 1 To p.bp3doc_field.Count
            fld = p.bp3doc_field.Item(i)
            If fld.Name.ToLower = txtFld.Text.ToLower Then
                uc = ot.bp3doc_uk.Add()
                uc.UKStore = p
                uc.Name = "UC_ " + p.Name + "_" + fld.Name
                uc.TheComment = "Уникальность для " + p.Caption + "." + fld.Caption
                txtOut.Text = uc.Name & vbCrLf & txtOut.Text
                uc.PerParent = bp3doc.bp3doc.enumBoolean.Boolean_Net
                uc.Save()
                cf = uc.bp3doc_ukfld.Add()
                cf.TheField = fld
                cf.Save()
                Exit For
            End If
        Next


    End Sub

End Class