<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserBlog_ArticlesList.aspx.cs" Inherits="BLOG_TEST.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title runat="server" id="webTitle"></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="background:url(images/bg_title_blue.png);height:300px;">
        <a class=" page-header" style="position:relative;left:150px;top:80px;font-size:30px;" id="blogNameLink" runat="server" >XXXXXXXXx</a>
    </div>
    <div style="margin-top:10px;overflow:hidden;">
        <div style="width:180px;float:left;width:15%;">
            <div class="panel" style="background-color:#ffffff;border:solid 1px #0094ff;height:500px;">
                <div class="flip" style="background-color:#0099FF;color:#ffffff;height:20px;">个人资料</div>
                <table class="userInfo" style="text-align:center;height:450px;width:100%;">
                    <tr>
                        <td><asp:Image ID="userHeadImg" runat="server" ImageUrl="images/default_user.png" Height="120px" Width="120px"/></td>
                    </tr>
                    <tr>
                        <td><asp:Label ID="userNameLabel" runat="server" Text="XXXXX" Font-Bold="true" Font-Size="20px"></asp:Label>
                            <br />
                            <a role="button" class="btn btn-success" runat="server" id="sendMessages" href="UserSendMessages.aspx">发私信</a>
                            <p><strong>个人简介</strong></p>
                            <asp:Label ID="userIntroduceLabel" runat="server" Text="XXXXXXXXXXXXXXXXXXXXXXX" Cssclass="panel" BorderColor="#0094ff" BorderStyle="Solid" BorderWidth="1px" Height="80px" Width="90%" style="word-break:break-all;" ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <p>E-mail:</p><asp:Label ID="emailLabel" runat="server" Text="XXXXXXXX@xxx.com" Font-Size="15px"></asp:Label>
                            <p>QQ:</p><asp:Label ID="qqLabel" runat="server" Text="XXXXXXXXXXXXXXX" Font-Size="15px"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="panel" style="margin-top:10px;background-color: #ffffff;border:solid 1px #0094ff;">
                <div class="hotBlogTitle" style="background-color:#0099FF;color:#ffffff;height:20px;">热门文章</div>
                <div class="hotBlog" >
                     <asp:PlaceHolder ID="hotBlogHolder" runat="server"></asp:PlaceHolder>
                </div>
            </div>
            <div class="panel" style="margin-top:10px;background-color: #ffffff;border:solid 1px #0094ff;">
                <div class="latestBlogTitle" style="background-color:#0099FF;color:#ffffff;height:20px;">最近文章</div>
                    <div class="latestBlog" >
                        <asp:PlaceHolder ID="latestBlogHolder" runat="server"></asp:PlaceHolder>
                    </div>
            </div>
        </div>    
        <div class="panel" style="background-color: #ffffff;border:solid 1px #0094ff;float:left;margin-left:10px;width:84%">
            <asp:PlaceHolder ID="mainContentHolder" runat="server">
            </asp:PlaceHolder>
        </div>
    </div>

</asp:Content>
