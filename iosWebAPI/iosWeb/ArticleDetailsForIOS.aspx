<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArticleDetailsForIOS.aspx.cs" Inherits="iosWeb.ArticleDetailsForIOS" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<%--<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no"/>--%>
    <title></title>
    <style type="text/css">
        body
        {
            position:relative;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width:50%;border:solid 1px #000000" class="container body-content">
            <asp:Label runat="server" Text="XXXXXXXXXXXXXXXX" ID="ArticleTitleLabel" Font-Bold="true" Font-Size="2.0em"></asp:Label>
        <div style="margin-left:auto;margin-right:auto;margin-top:10px;width:96%;text-align:left;">
            <asp:Label Width="100%" runat="server" ID="ArticleContentLabel" Font-Size="1.125em" style="word-break:break-all" Text="
                XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXx
                XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXx
                xXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
                xXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
                xXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXx
                "></asp:Label>
        </div>
    </div>
    </form>
</body>
</html>
