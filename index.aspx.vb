Imports Microsoft.AspNet.Identity
Imports System.Globalization
Imports System.Threading
Imports System.Configuration
Imports System.Web.Configuration
Imports System.Collections.Specialized
Imports System.Reflection
Imports System
Imports System.Data.SqlClient

Public Class index
    Inherits System.Web.UI.Page
    Dim Sql As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Session("TukarPTj") = 0
        Dim trackno As Integer = 0

        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")

        ' Stop Caching in IE
        Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache)
        ' Stop Caching in Firefox
        Response.Cache.SetNoStore()

        Try


            'Dim config As Configuration = WebConfigurationManager.OpenWebConfiguration("~/Web.Config")
            'Dim section As SessionStateSection = DirectCast(config.GetSection("system.web/sessionState"), SessionStateSection)
            'Dim intTimeout As Integer = CInt(section.Timeout.TotalMinutes) * 60 * 1000

            'hidTimeOut.Value = intTimeout
            'hidWarn.Value = intTimeout + 10000

            trackno = 6
            If Not Page.IsPostBack Then


                Dim usession As String = Request.QueryString("usession")

                Dim iLogin As String = "01662"


                Session("ssusrID") = iLogin
                'Session("sessionID") = "03080066420221611"
                Session("sessionID") = "0705025542019"

                UserInfo.strSesStaffID = Session("ssusrID")
                UserInfo.strSessId = Session("sessionID")
                ' dbconn = New SqlConnection(strCon)

                Using dtUserInfo = fGetUserInfo(Session("ssusrID"))
                    If dtUserInfo.Rows.Count > 0 Then
                        strSesUserName = dtUserInfo.Rows.Item(0).Item("MS01_Nama")
                        strSesUserPost = dtUserInfo.Rows.Item(0).Item("JawGiliran")
                        struserPtj = dtUserInfo.Rows.Item(0).Item("Pejabat")
                        strSesUserKodPtj = dtUserInfo.Rows.Item(0).Item("KodPejabat") & "0000"
                        struserKodBahagian = dtUserInfo.Rows.Item(0).Item("MS08_Bahagian")
                        struserKodUnit = dtUserInfo.Rows.Item(0).Item("MS08_Unit")

                        Session("ssusrName") = strSesUserName
                        Session("ssusrPost") = strSesUserPost
                        Session("ssusrKodBahagian") = struserKodBahagian
                        Session("ssusrKodUnit") = struserKodUnit
                        Session("ssusrKodPTj") = strSesUserKodPtj
                        Session("ssusrPTj") = struserPtj

                        Using dtUserInfo1 = fGetUserCLM(Session("ssusrID"))
                            If dtUserInfo1.Rows.Count > 0 Then
                                Dim strDate As DateTime = dtUserInfo1.Rows.Item(0).Item("CLM_LastLogin")
                                strDateLastLogin = strDate.ToString("dd/MM/yyyy hh:mm:ss tt")
                                Session("ssLastLogin") = strDateLastLogin
                            End If
                        End Using

                    End If
                End Using

                'Dim iLogin As String = "00760"

                dbconnNew = New SqlConnection(strCon)
                dbconnNew.Open()


                Sql = "SELECT no_staf, kod_tahap from SMKB_UTahapDT where No_Staf  = '" & iLogin & "' "
                dbcomm = New SqlCommand(Sql, dbconnNew)
                dbread = dbcomm.ExecuteReader()

                If dbread.HasRows Then
                    Do While dbread.Read()
                        Session("RefTahap") = dbread("kod_tahap")
                    Loop
                End If

                dbread.Close()
                dbcomm = Nothing

                Session("LoggedIn") = True

                '-------- check tahap---
                dbconnNew = New SqlConnection(strCon)
                dbconnNew.Open()

                Sql = "SELECT Kod_Modul, Nama_Modul, icon_location FROM SMKB_Modul WHERE Kod_Modul IN (SELECT Kod_Modul FROM SMKB_Sub_Modul
	                    WHERE Kod_Sub 
		                    IN (SELECT Kod_Sub FROM SMKB_Sub_Menu
		                    WHERE KOD_SUB_MENU 
			                    IN (SELECT KOD_SUB_MENU FROM SMKB_UProses_Kump
			                    WHERE Kod_Tahap = '" & Session("RefTahap") & "' AND Status = 1) 
		                    ))"

                dbcomm = New SqlCommand(Sql, dbconnNew)

                Dim paramSql() As SqlParameter = {
                            }
                Dim ds As DataSet = NewGetDataPB(Sql, paramSql, dbconnNew)

                If (ds.Tables.Count > 0) Then
                    rptMenu.DataSource = ds
                    rptMenu.DataBind()
                End If

                dbconnNew.Close()
                '---------------
            End If

            UserLogin.status = True

        Catch ex As Exception
            fErrorLog("Page_Load(), Err - " & ex.ToString)
        End Try
    End Sub



    'End Sub
    Protected Sub rptMenu_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
        'Dim Modul As String = e.CommandArgument.ToString()
        ''Session("AspPage") = ASPPage
        ''Session("AspPage") = "FORMS/Main.aspx"
        'Session("KodModul") = Modul
        ''If Session("LOGINID") = "" Then
        ''    Session("LOGINID") = Session("ssusrID")
        ''End If


        ''Server.Transfer(Session("AspPage"))
        ''Response.Redirect("Site.Master.aspx")
        'Response.Redirect("~/" & Session("AspPage"))


        'Session("AspPage") = ASPPage
        Dim argument As String = e.CommandArgument.ToString()

        Dim values As String() = argument.Split("|")

        ' Use the individual values as needed
        Session("KodModul") = values(0)
        Session("ssusrID") = values(1)
        Session("Page") = "FORMS/Main.aspx"

        Response.Redirect("~/" & Session("Page"))





    End Sub

End Class