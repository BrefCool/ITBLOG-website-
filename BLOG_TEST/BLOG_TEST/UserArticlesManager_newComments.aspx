<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserArticlesManager_newComments.aspx.cs" Inherits="BLOG_TEST.UserArticlesManager_newComments" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel" style="margin-top:5px;width:100%;height:100px;background-color:#ffffff;border:1px solid #0094ff;">
        <table style="width:98%;margin-left:auto;margin-right:auto;">
            <tr>
                <td style="width:70px;text-align:left;"><asp:Image ID="userHeadImg" runat="server" Width="64px" Height="64px" ImageUrl="images/default_user.png" style="margin-top:15px;"/></td>
                <td style="text-align:left">
                    <asp:Label ID="userNameLabel" runat="server" Text="XXXXXXX"></asp:Label>
                    <ul class="list-unstyled list-inline">
                        <li><a href="UserInfo.aspx" runat="server" id="toMyInfo">个人中心</a></li>
                        <li><p> | </p></li>
                        <li><a href="#" runat="server" id="toMyBlog">我的博客</a></li>
                    </ul>
                </td>
            </tr>
        </table>
    </div>
    <div class="panel" style="margin-top:5px;width:100%;background-color:#ffffff;border:1px solid #0094ff;">
        <ul class="nav nav-tabs">
            <li role="presentation"><a href="UserArticlesManager_addArticles.aspx">发表文章</a></li>
            <li role="presentation"><a href="UserArticlesManager_myArticles.aspx">我的文章</a></li>
            <li role="presentation"   class="active"><a href="#">新增评论</a></li>
        </ul>
        <asp:PlaceHolder ID="viewMyComments" runat="server">
            <%--<div style="margin-left:10px;margin-top:20px;margin-right:10px;border-bottom:1px solid #CCCCCC ;">
                <ul class="list-unstyled list-inline" style="text-align:left;">
                    <li><p style="font-size:12px;">用户</p></li>
                    <li><asp:HyperLink ID="commenterName" runat="server" Text="XXXXX" NavigateUrl="#"></asp:HyperLink></li>
                    <li><p style="font-size:12px;"> 在文章</p></li>
                    <li><asp:HyperLink ID="articleTitle" runat="server" Text="XXXXXXXXXXXXXX" NavigateUrl="#"></asp:HyperLink></li>
                    <li><p style="font-size:12px;">中评论道:</p></li>   
                </ul>
                <div style="margin-left:10px;margin-right:10px;margin-bottom:3px;background-color:#eeeeee">
                    <asp:Label ID="commentContent" runat="server" Text="XXXXXXXXXXXXXXXXXXXXXX"></asp:Label>
                </div>
            </div>--%>
        </asp:PlaceHolder>
    </div>
</asp:Content>
