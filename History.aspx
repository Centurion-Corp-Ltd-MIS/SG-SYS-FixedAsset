﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="History.aspx.cs" Inherits="FixedAssetSystem.History" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Fix Asset</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <link href="vendor/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css" />
    <link
        href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i"
        rel="stylesheet" />
    <!-- Custom styles for this template-->
    <link href="css/sb-admin-2.min.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

         <%----------------------Details Modal Controls -----------%>
         <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
        <div class="modal fade" runat="server" id="controls" tabindex="-1" role="dialog" >
            <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                <div class="modal-content">
                    
                    <div class="modal-body">

                       
                                      

                                         <asp:GridView ID="logTable" runat="server" class="table table-bordered" Width="80%" CellSpacing="0" AutoGenerateColumns="false" HeaderStyle-HorizontalAlign="right" style="margin-left:10%; margin-right:10%">
                                            <Columns>
                                                <asp:BoundField DataField="DateAction" HeaderText="Date" ItemStyle-HorizontalAlign="right" />
                                                <asp:BoundField DataField="Event" HeaderText="Event" ItemStyle-HorizontalAlign="right" />
                                                <asp:BoundField DataField="FieldChanged" HeaderText="Field" ItemStyle-HorizontalAlign="right" />
                                                <asp:BoundField DataField="ChangedFrom" HeaderText="Change From" ItemStyle-HorizontalAlign="right" />
                                                <asp:BoundField DataField="ChangedTo" HeaderText="Changed To" ItemStyle-HorizontalAlign="right" />
                                                <asp:BoundField DataField="ActionBy" HeaderText="Action By" ItemStyle-HorizontalAlign="right" />
                                                
                                            </Columns>
                                        </asp:GridView>

                       
                    </div>
                               
                </div>
            </div>
        </div>
        </ContentTemplate>
                        </asp:UpdatePanel>

        <div id="wrapper">
            <!-- Sidebar -->
            <ul class="navbar-nav bg-gradient-info text-white sidebar sidebar-dark accordion" id="accordionSidebar">

                <!-- Sidebar - Brand -->
                 <a class="sidebar-brand d-flex align-items-center justify-content-center" href="MainPage.aspx">
                    <div class="sidebar-brand-icon">
                        <%--<i class="fas fa-laugh-wink"></i>--%>
                        <img src="img/westlite-white-logo.png" style="max-height:40px; max-width:80px" class="img-fluid">
                    </div>
                    <div class="sidebar-brand-text mx-3">Westlite</div>
                </a>


                <!-- Heading -->
                <div class="sidebar-heading">
                </div>
                <!-- Nav Item - Dorms -->
                <li class="nav-item">
                    <asp:HyperLink CssClass="nav-link" ID="HyperOverallList" href="Dashboard.aspx" runat="server"><i class="fas fa-fw fa-building"></i>Dashboard</asp:HyperLink>
                </li>

                <li class="nav-item">
                    <asp:HyperLink CssClass="nav-link" ID="HyperLink1" href="Assets.aspx" runat="server"><i class="fas fa-fw fa-building"></i>Assets</asp:HyperLink>
                </li>

                 <li class="nav-item" id="accountDiv" runat="server">
                    <asp:HyperLink CssClass="nav-link" ID="HyperLink2" href="History.aspx" runat="server"><i class="fas fa-fw fa-building"></i>History</asp:HyperLink>
                </li>

                  <li class="nav-item" >
                <ajaxToolkit:Accordion ID="Accordion1" runat="server" RequireOpenedPane="false" SelectedIndex="-1" CssClass="nav-link" >  
                <Panes >  
                    <ajaxToolkit:AccordionPane ID="AccordionPane1" runat="server">
                        <Header><i class="fas fa-fw fa-building"></i>Master Setup</Header>  
                        <Content>
                            <asp:HyperLink CssClass="nav-link" ID="HyperLink3" href="SetupBrand.aspx" runat="server"><i class="fas fa-fw fa-building"></i>Brand</asp:HyperLink>
                            <asp:HyperLink CssClass="nav-link" ID="HyperLink5" href="SetupSite.aspx" runat="server"><i class="fas fa-fw fa-building"></i>Site</asp:HyperLink>
                            <asp:HyperLink CssClass="nav-link" ID="HyperLink6" href="SetupCategory.aspx" runat="server"><i class="fas fa-fw fa-building"></i>Category</asp:HyperLink>
                            <asp:HyperLink CssClass="nav-link" ID="HyperLink7" href="SetupDepartment.aspx" runat="server"><i class="fas fa-fw fa-building"></i>Department</asp:HyperLink>
                        </Content>  
                    </ajaxToolkit:AccordionPane>  
                </Panes> 
                </ajaxToolkit:Accordion>  
                </li>

                <!-- Divider -->
                <hr class="sidebar-divider my-0">
                <li class="nav-item">
                    <asp:HyperLink CssClass="nav-link" ID="HyperLogout" href="Login.aspx" runat="server"><i class="fas fa-fw fa-building"></i>Logout</asp:HyperLink>
                </li>

            </ul>
            <!-- End of Sidebar -->
            <!-- Content Wrapper -->
            <div id="content-wrapper" class="d-flex flex-column">

                <!-- Main Content -->
                <div id="content">
                    <nav class="navbar navbar-expand navbar-light bg-white topbar mb-4 static-top shadow">

                        <!-- Sidebar Toggle (Topbar) -->
                        <button id="sidebarToggleTop" class="btn btn-link d-md-none rounded-circle mr-3">
                            <i class="fa fa-bars"></i>
                        </button>

                        <asp:Label ID="lblDorm1" runat="server" class="h3 m-0">History Tab</asp:Label>
                        <asp:Label ID="lblDorm" runat="server" class="h3 m-0"></asp:Label>
                        <!-- Topbar Navbar -->
                        <ul class="navbar-nav ml-auto">

                            <div class="topbar-divider d-none d-sm-block"></div>

                            <!-- Nav Item - User Information -->
                            <li class="nav-item dropdown no-arrow">
                                <a class="nav-link dropdown-toggle" href="#" id="userDropdown" role="button"
                                    data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                    <asp:Label ID="lblUserName" runat="server" class="mr-2 d-none d-lg-inline text-gray-600 small"> </asp:Label>
                                    <%--<span class="mr-2 d-none d-lg-inline text-gray-600 small">Douglas McGee</span>--%>
                                    <img class="img-profile rounded-circle"
                                        src="img/undraw_profile.svg">
                                </a>
                                <!-- Dropdown - User Information -->

                            </li>

                        </ul>

                    </nav>
                    <!-- End of Topbar -->

                    <div class="container-fluid">
                      
                       
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                
                        <div class="row">
                            <div class="col-xl-12 col-lg-12">
                                <div class="card shadow mb-4 ">
                                    <!-- Card Header - Dropdown -->
                                    <div class="card-header py-3 d-flex flex-row align-items-center justify-content-between">
                                        <asp:Label ID="Label24" runat="server" class="m-0 font-weight-bold text-primary">History Field</asp:Label>
                                       <%--<asp:Label ID="timeLabel"  runat="server" class="h5 m-0 font-weight-bold text-primary"></asp:Label>--%>
                                         <asp:DropDownList ID="ddlCategoryMain" runat="server" OnSelectedIndexChanged="ddlCategoryMain_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" Width="15%"></asp:DropDownList>
                                    </div>
                                    <!-- Card Body -->
                                    <div class="card-body" style="overflow:auto; height:800px;">
                                        
                                        <asp:GridView ID="gvTable" runat="server" class="table table-bordered" Width="80%" CellSpacing="0" AutoGenerateColumns="false" HeaderStyle-HorizontalAlign="right" style="margin-left:10%; margin-right:10%">
                                            <Columns>
                                                <asp:BoundField DataField="AutoID" HeaderText="ID" ItemStyle-HorizontalAlign="right" />
                                                <asp:BoundField DataField="AssetTag" HeaderText="Asset Tag" ItemStyle-HorizontalAlign="right" />
                                                <asp:BoundField DataField="Site" HeaderText="Site" ItemStyle-HorizontalAlign="right" />
                                                <asp:BoundField DataField="Category" HeaderText="Category" ItemStyle-HorizontalAlign="right" />
                                                <asp:BoundField DataField="Personnel" HeaderText="Personnel" ItemStyle-HorizontalAlign="right" />
                                                <asp:BoundField DataField="Description" HeaderText="Description" ItemStyle-HorizontalAlign="right" />
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">  
                                                    <ItemTemplate>  
                                                        <asp:LinkButton ID="lnkView" runat="server" Text="View" OnClick="lnkView_Click"></asp:LinkButton> 
                                                    </ItemTemplate>  
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>

                       

                                </ContentTemplate>
                        </asp:UpdatePanel>
                     

















                    </div>
                </div>

            </div>
            <!-- End of Main Content -->
        </div>

         <!-- Bootstrap core JavaScript-->
        <script src="vendor/jquery/jquery.min.js"></script>
        <script src="vendor/bootstrap/js/bootstrap.bundle.min.js"></script>

        <!-- Core plugin JavaScript-->
        <script src="vendor/jquery-easing/jquery.easing.min.js"></script>

        <!-- Custom scripts for all pages-->
        <script src="js/sb-admin-2.min.js"></script>

        <!-- Page level plugins -->
        <script src="vendor/chart.js/Chart.min.js"></script>

        <!-- Page level custom scripts -->
        <script src="js/demo/chart-area-demo.js"></script>
        <script src="js/demo/chart-pie-demo.js"></script>
        
        <!-- Error Modal-->
    <div class="modal fade" id="errorModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <asp:Label id="lblPromptTitle" class="modal-title" runat="server" ></asp:Label>
                    <button class="close" type="button" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:Label ID="lblDisplayError" runat="server"></asp:Label>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnModalCancel" runat="server" class="btn btn-secondary" data-dismiss="modal" Text="Cancel"></asp:Button>
                </div>
            </div>
        </div>
    </div>


    <script>
        function openErrorModal() {
            $('#errorModal').modal('show');
        }
    </script>
    <!-- Bootstrap core JavaScript-->
    <script src="vendor/jquery/jquery.min.js"></script>
    <script src="vendor/bootstrap/js/bootstrap.bundle.min.js"></script>

    <!-- Core plugin JavaScript-->
    <script src="vendor/jquery-easing/jquery.easing.min.js"></script>

    <!-- Custom scripts for all pages-->
    <script src="js/sb-admin-2.min.js"></script>

    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/js/bootstrap-datepicker.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/css/bootstrap-datepicker3.css"/> 

    </form>
</body>
</html>

