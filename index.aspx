<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="index.aspx.vb" Inherits="SMKB_Web_Portal.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="utf-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1" />
  <link rel="stylesheet" href="./assets/bootstrap/css/bootstrap.min.css" />
  <script src='./assets/bootstrap/js/bootstrap.min.js'></script>
  <link rel="stylesheet" href="./styles.css" />
  <script src="./assets/script/script.js"></script>

  <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.0.7/css/all.css">
  <link rel="stylesheet"
    href="https://maxst.icons8.com/vue-static/landings/line-awesome/line-awesome/1.3.0/css/line-awesome.min.css">

  <link rel="stylesheet"
    href="https://maxst.icons8.com/vue-static/landings/line-awesome/font-awesome-line-awesome/css/all.min.css">



</head>
<body>
    <form id="form1" runat="server">
  <div class="header">
    <div class="header-logo">
      <img src="<%=ResolveClientUrl("~/assets/images/smkb-logo.svg")%>" />
    </div>
    <div class="account-info dark">
        <a><%=Session("ssusrName")%></a>
      <div class="action">
        <div class="profile" onclick="menuToggle();">
          
            <img src="https://portal.utem.edu.my/oas/directory/bak/image.asp?myStaff=<%=Session("ssusrID")%>" />
            
        </div>
        
        <div class="menu">
          <h3><%=Session("ssusrName")%><br /><span><%=Session("ssusrPost")%></span></h3>
          <ul>
            <li>
              <i class="las la-sign-out-alt"></i>
              <a href="#">Logout</a>
            </li>
          </ul>
        </div>


      </div>
    </div>
  </div>

  <div class="dashboard-content">
    <div id="nav-placeholder">

    </div>
    <div class="row">

    <asp:Repeater ID="rptMenu" runat="server" OnItemCommand="rptMenu_ItemCommand"   >
        <ItemTemplate>
          <div class="col-md-3">
               <asp:LinkButton ID="ASPPage" runat="server" CommandArgument ='<%#Trim(Eval("Kod_Modul")) + "|" + Session("ssusrID") %>'   >
                    <div id="menumain" runat="server" class="menu-card">
                      <img src="<%#Trim(Eval("icon_location")) %>"/>
                      <p><%#Trim(Eval("Nama_Modul")) %></p>
                    </div>

               </asp:LinkButton>
          </div>
          

        </ItemTemplate>
    </asp:Repeater>


<%--      <div class="col-md-3">
        <div class="menu-card" onclick="location.href='./pages/System Admin/pendaftaran-menu.html';">
          <img src="./assets/icon/user-admin.svg"/>
          <p>Pentadbir Sistem</p>
        </div>
      </div>

      <div class="col-md-3">
        <div class="menu-card" onclick="location.href='./pages/Akaun Belum Terima/akaun-penghutang.html';">
          <img src="./assets/icon/receive-cash.svg"/>
          <p>Akaun Belum Terima</p>
        </div>
      </div>

      <div class="col-md-3">
        <div class="menu-card">
          <img src="./assets/icon/journal.svg"/>
          <p>Jurnal</p>
        </div>
      </div>

      <div class="col-md-3">
        <div class="menu-card">
          <img src="./assets/icon/pay-cash.svg"/>
          <p>Akaun Belum Bayar</p>
        </div>
      </div>--%>


    </div>
  </div>
  <script>
      $(function () {
          $("#nav-placeholder").load("/nav.html");
      });


      function divClicked() {
          __doPostBack('menumain', '');
      }
    </script>
    </form>
</body>
</html>
