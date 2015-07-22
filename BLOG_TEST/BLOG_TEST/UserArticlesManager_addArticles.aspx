<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserArticlesManager_addArticles.aspx.cs" Inherits="BLOG_TEST.UserArticlesManager" validateRequest="false"%>
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
            <li role="presentation" class="active"><a href="#">发表文章</a></li>
            <li role="presentation"><a href="UserArticlesManager_myArticles.aspx">我的文章</a></li>
            <li role="presentation"><a href="UserArticlesManager_newComments.aspx">新增评论</a></li>
        </ul>
        <div style="border-bottom:1px solid #0094ff;font-size:12px;color:#0094ff;">文章标题</div>
        <div><asp:TextBox ID="articleTitleTextBox" runat="server" Width="100%" style="margin-top:4px;"></asp:TextBox></div>
        <div style="border-bottom:1px solid #0094ff;font-size:12px;color:#0094ff;">文章分类</div>
        <div><asp:RadioButtonList ID="CategoryRadioButtonList" runat="server" RepeatColumns="4" RepeatDirection="Horizontal" CellSpacing="80" Width="350px" >
                <asp:ListItem Text="移动开发" Value="1" ></asp:ListItem><asp:ListItem Text="编程语言" Value="2" ></asp:ListItem>
                <asp:ListItem Text="web开发" Value="3" ></asp:ListItem><asp:ListItem Text="系统运维" Value="4" ></asp:ListItem>
             </asp:RadioButtonList>
        </div>
        <div style="border-bottom:1px solid #0094ff;font-size:12px;color:#0094ff;">文章描述</div>
        <div><asp:TextBox ID="articleDescriptionTextBox" runat="server" Width="100%" style="margin-top:4px;" TextMode="MultiLine" Rows="3"></asp:TextBox></div>
        <div style="border-bottom:1px solid #0094ff;font-size:12px;color:#0094ff;">文章内容</div>
        <textarea id="blogTextarea" name="blogTextarea" cols="100" rows="35" style="width:100%;" runat="server"></textarea>
        <div style="text-align:right;margin-right:20px;"><asp:Button ID="publishBtn" runat="server" CssClass="btn btn-primary btn-lg" Text="发表" OnClientClick="return blogPublish()" OnClick="publishBtn_Click"/></div>
    </div>
</asp:Content>
