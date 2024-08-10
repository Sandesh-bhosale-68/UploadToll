<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title> </title>
    <!-- meta tags -->
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
   
    <!-- /meta tags -->
    <!-- custom style sheet -->
    <link href="css/style (2).css" rel="stylesheet" type="text/css" />
    <!-- /custom style sheet -->
    <!-- fontawesome css -->
    <link href="css/fontawesome-all.css" rel="stylesheet" />
    <!-- /fontawesome css -->
    <!-- google fonts-->
    <link href="//fonts.googleapis.com/css?family=Raleway:100,100i,200,200i,300,300i,400,400i,500,500i,600,600i,700,700i,800,800i,900,900i"
        rel="stylesheet">
    <!-- /google fonts-->

</head>


<body>
    <div class=" w3l-login-form">
        <h2>Login Here</h2>
        <form id="form1" runat="server">

            <div class=" w3l-form-group">
                <label>Username:</label>
                <div class="group">
                    <i class="fas fa-user"></i>
                    <input type="text" class="form-control" placeholder="Username" id="txtUser" required="required" runat="server"  />
                </div>
            </div>
            <div class=" w3l-form-group">
                <label>Password:</label>
                <div class="group">
                    <i class="fas fa-unlock"></i>
                    <input type="password" class="form-control" placeholder="Password" id="txtPassword" required="required" runat="server" />
                </div>
            </div>
           <div>
            <button type="submit" id="btnSubmit" runat="server" onserverclick="btnSubmit_Click">Login</button>
           <%-- <input type="submit" name="btnSubmit" id="btnSubmit" value="Login" runat="server" onserverclick="Submit_Click"/>--%>
         
         
            </div>
        </form>
       

</body>

</html>
