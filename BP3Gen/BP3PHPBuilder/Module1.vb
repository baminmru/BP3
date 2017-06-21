Module Module1
    Public Manager As BP3.Manager
   
    Public viCol As Collection
    Public NewViewName As String
    Public NewViewAlias As String
    Public NewForChoose As Boolean
    Public NewForChooseObject As Boolean
    Public DelOtherView As Boolean
    Public CreatedView As bp3qry.bp3qry.Application
    Public BasePartID As Guid
    Public BasePart As bp3doc.bp3doc.bp3doc_store
    Public ViewForChange As bp3qry.bp3qry.bp3qry_def
    Public BaseType As bp3doc.bp3doc.bp3doc_def

    Public Enum InterfaceType

        InterfaceTypeCommon = 0
        InterfaceTypeRightTree = 1
        InterfaceTypeRightGrid = 2
        InterfaceTypeRightPanel = 3
        InterfaceTypeBottomTree = 4
        InterfaceTypeBottomGrid = 5
        InterfaceTypeBottomPanel = 6
        InterfaceTypeLeftTree = 7
        InterfaceTypeTopGrid = 8
        InterfaceTypeLeftPanel = 9
        InterfaceTypeTree = 10
        InterfaceTypeGrid = 11
        InterfaceTypePanel = 12
        InterfaceTypeGridGrid = 13
        InterfaceTypeTreeGrid = 14
        InterfaceTypeExtention = 100

    End Enum


    

    Public Function CountStructs(s As bp3doc.bp3doc.bp3doc_store_col, mode As bp3card.bp3card.Application) As Integer

        Dim part As bp3doc.bp3doc.bp3doc_store = s.Parent
        Dim i As Integer, j As Integer

        If (mode Is Nothing) Then
            Return s.Count
        End If

        Dim cnt As Integer
        Dim OK As Boolean = False
        CNT = 0
        For j = 1 To s.Count
            OK = True
            For i = 1 To mode.bp3card_part.Count
                Dim Struct As bp3doc.bp3doc.bp3doc_store = mode.bp3card_part.Item(i).Struct
                If (Struct.ID.Equals(s.Item(j).ID)) Then

                    If (mode.bp3card_part.Item(i).AllowRead = bp3card.bp3card.enumBoolean.Boolean_Da) Then
                        OK = True
                    Else
                        OK = False
                    End If
                    Exit For
                End If
            Next
        Next
        If (OK) Then

            cnt += 1
        End If
        Return cnt

    End Function

    Public Function AnalyzeInterfaceForGUI(s As bp3doc.bp3doc.bp3doc_store, mode As bp3card.bp3card.Application) As InterfaceType

        Dim ot As bp3doc.bp3doc.bp3doc_def = Nothing
        Dim prev As bp3doc.bp3doc.bp3doc_store = Nothing

        Dim level As Integer = 1

        ot = TypeForStruct(s)

        If TypeName(s.Parent.Parent) = "Application" Then
            level = 1
        ElseIf TypeName(s.Parent.Parent.Parent.Parent) = "Application" Then
            level = 2
        Else
            level = 3
        End If


        If (level > 2) Then
            Return InterfaceType.InterfaceTypeCommon
        End If


        If (level = 2) Then

            If (s.PartType = bp3doc.bp3doc.enumPartType.PartType_Stroka) Then
                Return InterfaceType.InterfaceTypeCommon
            End If

            '//'        ' сколько детей у родительской структуры
            prev = s.Parent.Parent
            If (CountStructs(prev.bp3doc_store, mode) > 1) Then

                Return InterfaceType.InterfaceTypeCommon
            End If
            '// '        ' сколько детей у текущей структуры
            If (CountStructs(s.bp3doc_store, mode) > 0) Then

                Return InterfaceType.InterfaceTypeCommon
            End If

            If (prev.PartType = bp3doc.bp3doc.enumPartType.PartType_Derevo) Then


                If (s.PartType = bp3doc.bp3doc.enumPartType.PartType_Derevo) Then

                    Return InterfaceType.InterfaceTypeCommon
                End If
                If (s.PartType = bp3doc.bp3doc.enumPartType.PartType_Kollekciy) Then

                    Return InterfaceType.InterfaceTypeTreeGrid
                End If

                If (s.PartType = bp3doc.bp3doc.enumPartType.PartType_Stroka) Then

                    Return InterfaceType.InterfaceTypeCommon
                End If

            Else

                If (s.PartType = bp3doc.bp3doc.enumPartType.PartType_Derevo) Then

                    Return InterfaceType.InterfaceTypeCommon
                End If
                If (s.PartType = bp3doc.bp3doc.enumPartType.PartType_Kollekciy) Then

                    Return InterfaceType.InterfaceTypeGridGrid
                End If

                If (s.PartType = bp3doc.bp3doc.enumPartType.PartType_Stroka) Then

                    Return InterfaceType.InterfaceTypeCommon
                End If

            End If


        End If

        If (level = 1) Then

            If (s.PartType = bp3doc.bp3doc.enumPartType.PartType_Rassirenie) Then

                Return InterfaceType.InterfaceTypeExtention
            End If

            '// много детей
            If (CountStructs(s.bp3doc_store, mode) > 1) Then

                Return InterfaceType.InterfaceTypeCommon
            End If

            For i = 1 To s.bp3doc_store.Count

                If (PartIsPresent(s.bp3doc_store.Item(i), mode)) Then

                    '// ' у зависимой коллекции есть много детей
                    If (CountStructs(s.bp3doc_store.Item(1).bp3doc_store, mode) > 0) Then

                        Return InterfaceType.InterfaceTypeCommon
                    End If
                End If
            Next




            If (CountStructs(s.bp3doc_store, mode) = 1) Then
                If (s.PartType = bp3doc.bp3doc.enumPartType.PartType_Stroka) Then

                    Return InterfaceType.InterfaceTypeCommon

                End If

                ' если дочерняя строка - без вариантов
                For i = 1 To s.bp3doc_store.Count

                    If (PartIsPresent(s.bp3doc_store.Item(i), mode)) Then

                        ' у зависимой коллекции есть много детей
                        If (s.bp3doc_store.Item(i).PartType = bp3doc.bp3doc.enumPartType.PartType_Stroka) Then

                            Return InterfaceType.InterfaceTypeCommon

                        End If
                    End If
                Next



                If (s.PartType = bp3doc.bp3doc.enumPartType.PartType_Derevo) Then

                    Return InterfaceType.InterfaceTypeTreeGrid
                End If

                If (s.PartType = bp3doc.bp3doc.enumPartType.PartType_Kollekciy) Then
                    ' исключаем вариант таблица - дерево
                    For i = 1 To s.bp3doc_store.Count

                        If (PartIsPresent(s.bp3doc_store.Item(i), mode)) Then
                            ' у зависимой коллекции есть много детей
                            If (s.bp3doc_store.Item(i).PartType = bp3doc.bp3doc.enumPartType.PartType_Derevo) Then

                                Return InterfaceType.InterfaceTypeCommon
                            End If
                        End If
                    Next
                    Return InterfaceType.InterfaceTypeGridGrid
                End If


            End If
            If CountStructs(s.bp3doc_store, mode) = 0 Then

                If (s.PartType = bp3doc.bp3doc.enumPartType.PartType_Derevo) Then

                    Return InterfaceType.InterfaceTypeTree
                End If
                If (s.PartType = bp3doc.bp3doc.enumPartType.PartType_Kollekciy) Then

                    Return InterfaceType.InterfaceTypeGrid
                End If

                If (s.PartType = bp3doc.bp3doc.enumPartType.PartType_Stroka) Then

                    Return InterfaceType.InterfaceTypePanel
                End If
            End If

        End If
        Return InterfaceType.InterfaceTypeCommon
    End Function


    Public Function IsFieldMandatory(P As bp3doc.bp3doc.bp3doc_store, ByVal FieldID As Guid, ByVal mode As bp3card.bp3card.Application) As Boolean
        Dim i As Integer
        Dim rfld As bp3card.bp3card.bp3card_fld
        If mode Is Nothing Then Return False
        For i = 1 To mode.bp3card_fld.Count
            rfld = mode.bp3card_fld.Item(i)
            If rfld.TheField.ID.Equals(FieldID) Then
                If rfld.MandatoryField = bp3card.bp3card.enumTriState.TriState_Da Then
                    Return True
                Else
                    Return False
                End If
            End If
        Next

        Return False
    End Function


    Public Function FindJournal(ByRef ot As bp3doc.bp3doc.Application) As bp3list.bp3list.Application
        Dim rs As DataTable
        Dim jr As bp3list.bp3list.Application = Nothing
        rs = Manager.Session.GetRowsEx("INSTANCE", Manager.Session.GetProvider.InstanceFieldList, , , "OBJTYPE='bp3list' and Name='" & ot.bp3doc_def.Item(1).Name & "'")
        If Not rs Is Nothing Then
            If rs.Rows.Count > 0 Then
                jr = Manager.GetInstanceObject(New Guid(rs.Rows(0)("InstanceID").ToString))
            End If
        End If
        FindJournal = jr
    End Function

    Public Function JournalPart(ByRef ot As bp3doc.bp3doc.Application) As bp3doc.bp3doc.bp3doc_store
        Dim p As bp3doc.bp3doc.bp3doc_store
        Dim i As Integer
        If ot.bp3doc_store.Count = 0 Then Return Nothing
        ot.bp3doc_store.Sort = "sequence"
        For i = 1 To ot.bp3doc_store.Count
            p = ot.bp3doc_store.Item(i)
            If p.PartType = bp3doc.bp3doc.enumPartType.PartType_Stroka Then
                Return p
            End If
        Next
        Return ot.bp3doc_store.Item(1)
    End Function


    Public Function FindQueryForPart(p As bp3doc.bp3doc.bp3doc_store, ByVal Auto As Boolean) As bp3qry.bp3qry.Application
        Dim dt_ot As DataTable
        Dim doc_app As bp3qry.bp3qry.Application
        Dim q As String
        q = "select " + Manager.Session.GetProvider().ID2Base("instanceid", "instanceid") + " from bp3qry_def where SrcPart=" & Manager.Session.GetProvider.ID2Const(p.ID)
        If Auto Then
            q = q & " and the_alias like 'AUTO%' "
        End If
        dt_ot = Manager.Session.GetData(q)


        If dt_ot.Rows.Count > 0 Then
            doc_app = Manager.GetInstanceObject(New Guid(dt_ot.Rows(0)("instanceid").ToString()))
            Return doc_app
        End If
        Return Nothing

    End Function

    Public Function PartIsPresent(P As bp3doc.bp3doc.bp3doc_store, mode As bp3card.bp3card.Application) As Boolean
        Dim i As Integer
        Dim rPrt As bp3card.bp3card.bp3card_part
        If mode Is Nothing Then Return True
        For i = 1 To mode.bp3card_part.Count
            rPrt = mode.bp3card_part.Item(i)
            If rPrt.Struct.ID.Equals(P.ID) Then
                If rPrt.AllowRead = bp3card.bp3card.enumBoolean.Boolean_Da Then
                    Return True
                Else
                    Return False
                End If
            End If
        Next
        Return True
    End Function

    Public Function IsFieldPresent(P As bp3doc.bp3doc.bp3doc_store, fieldid As Guid, mode As bp3card.bp3card.Application) As Boolean
        Dim i As Integer
        Dim rfld As bp3card.bp3card.bp3card_fld
        If mode Is Nothing Then Return True
        For i = 1 To mode.bp3card_fld.Count
            rfld = mode.bp3card_fld.Item(i)
            If rfld.TheField.ID.Equals(fieldid) Then

                If rfld.AllowRead = bp3card.bp3card.enumBoolean.Boolean_Da Then
                    Return True
                Else
                    Return False
                End If
            End If
        Next
        Return True
    End Function

    Public Function IsFieldReadonly(P As bp3doc.bp3doc.bp3doc_store, FieldId As Guid, mode As bp3card.bp3card.Application) As Boolean
        Dim i As Integer
        Dim rfld As bp3card.bp3card.bp3card_fld
        If mode Is Nothing Then Return False
        For i = 1 To mode.bp3card_fld.Count
            rfld = mode.bp3card_fld.Item(i)
            If rfld.TheField.ID.Equals(FieldId) Then

                If rfld.AllowModify = bp3card.bp3card.enumBoolean.Boolean_Da Then
                    Return False
                Else
                    Return True
                End If
            End If
        Next

        Return False

    End Function


    Public Function GetPartRestriction(P As bp3doc.bp3doc.bp3doc_store, mode As bp3card.bp3card.Application) As bp3card.bp3card.bp3card_part
        Dim i As Integer
        Dim rPrt As bp3card.bp3card.bp3card_part
        If mode Is Nothing Then Return Nothing
        For i = 1 To mode.bp3card_part.Count
            rPrt = mode.bp3card_part.Item(i)
            If rPrt.Struct.ID.Equals(P.ID) Then
                Return rPrt
            End If
        Next

        Return Nothing
    End Function


    Public Function TypeForStruct(ByVal s As bp3doc.bp3doc.bp3doc_store) As bp3doc.bp3doc.bp3doc_def
        Dim app As bp3doc.bp3doc.Application
        app = s.Application
        TypeForStruct = app.bp3doc_def.Item(1)
    End Function



    Public Function AppForDoc(ByVal ot As bp3doc.bp3doc.bp3doc_def) As bp3doc.bp3doc.Application
        Dim app As bp3doc.bp3doc.Application
        app = ot.Application
        Return app
    End Function
    Public Function AppForFT(ByVal ot As bp3ft.bp3ft.bp3ft_def) As bp3ft.bp3ft.Application
        Dim app As bp3ft.bp3ft.Application
        app = ot.Application
        Return app
    End Function


    Public Function CountOfID(ByVal ID As String, ByVal n As System.Windows.Forms.TreeNode) As Integer
        Dim nn As System.Windows.Forms.TreeNode
        Dim cnt As Integer
        cnt = 0
        nn = n
        While Not n Is Nothing
            If Left(n.Name, 38) = ID Then
                cnt = cnt + 1
            End If
            n = n.Parent
        End While
        CountOfID = cnt
    End Function


    Public Sub ExractLevel(ByVal Key As String, ByRef ID As String, ByRef Level As String)
        ID = Left(Key, 38)
        Level = Right(Key, 38)
    End Sub


End Module
