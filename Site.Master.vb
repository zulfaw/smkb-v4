Imports System.CodeDom
Imports System.Data.SqlClient

Public Class SiteMaster
    Inherits MasterPage
    Dim Sql As String
    Dim uKodModul As String
    Dim nostaf As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        uKodModul = Session("KodModul")
        nostaf = Session("LOGINID")

        If Page.IsPostBack = False Then

            '    Dim nostaf As String = Request.QueryString("UsrLogin")
            Dim uSession As String = Request.QueryString("usession")


            '---SEMAK---
            dbconnNew = New SqlConnection(strCon)
            dbconnNew.Open()


            If uKodModul <> "" Then


                Sql = "select Kod_Sub, Nama_Sub, isnull(icon_location,'-') icon_location, Nama_Icon from SMKB_Sub_Modul where status = 1 and Kod_Modul = '" & uKodModul & "'
                order by Urutan"
                dbcomm = New SqlCommand(Sql, dbconnNew)

                Dim paramSql() As SqlParameter = {
                            }
                Dim ds As DataSet = NewGetDataPB(Sql, paramSql, dbconnNew)

                If (ds.Tables.Count > 0) Then
                    rptMenu.DataSource = ds
                    rptMenu.DataBind()
                End If

                dbconnNew.Close()
                dbconnNew.Dispose()

                'Response.Redirect("~/Header.aspx")
            End If

            'Response.Redirect("~/Header.aspx")
        End If
    End Sub



End Class