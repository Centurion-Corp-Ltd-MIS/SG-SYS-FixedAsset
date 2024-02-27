<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="FixedAssetSystem.Login" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>Fixed Asset System</title>

    <!-- Custom fonts for this template-->
    <link href="vendor/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css">
    <link
        href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i"
        rel="stylesheet">
    <%--  --%>
    <!-- Custom styles for this template-->
    <link href="css/sb-admin-2.min.css" rel="stylesheet">
    <link href="css/ccldashboard.css" rel="stylesheet">
</head>
<body style="background-image:url(https://images.pexels.com/photos/733857/pexels-photo-733857.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1); background-size:cover" > 
    
    <form id="form1" runat="server">
          <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container">
        <!-- Outer Row -->
        <div class="row justify-content-center">

            <div class="col-xl-10 col-lg-12 col-md-9">

                <div class="card o-hidden border-0 shadow-lg my-5 ">
                    <div class="card-body p-0">
                        <!-- Nested Row within Card Body -->
                          
                        <div class="row">
                            <%--<div class="col-lg-6 d-none d-lg-block bg-login-img"></div>--%>
                            <div class="col-lg-12">
                                <div class="p-5">
                                    <div class="text-center">
                                        <img src="img/ccllogo.png" />
                                        <br />
                                        <br />
                                        <h1 class="h4 text-gray-900 mb-4">Fixed Asset System</h1>
                                    </div>
                                    <form class="user">
                                       
                                        <div class="form-group">
                                            <%--<input type="email" class="form-control form-control-user"
                                                id="exampleInputEmail" aria-describedby="emailHelp"
                                                placeholder="Enter Email Address...">--%>
                                            <asp:TextBox ID="tbxUsername" runat="server" class="form-control form-control-user"
                                                aria-describedby="emailHelp" placeholder="Enter Email Address...">
                                                 </asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <%--<input type="password" class="form-control form-control-user"
                                                id="exampleInputPassword" placeholder="Password">--%>
                                            <asp:TextBox ID="tbxPassword" runat="server" class="form-control form-control-user"
                                                 placeholder="Password" TextMode="Password">
                                                 </asp:TextBox>
                                        </div>

                                       
                                        
                                       <%-- <a href="index.html" class="btn btn-primary btn-user btn-block">
                                            Login
                                        </a>--%>

                          
                                        <asp:Button ID="btnLogin" runat="server" class="btn btn-primary btn-user btn-block" Text="Login" OnClick="btnLogin_Click"/>
                                
                                    </form>
                                </div>
                            </div>
                        </div>
                                  

                    </div>
                </div>
            </div>
        </div>
    </div>

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
    </form>
</body>
</html>

