<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="BLOG_TEST.Index1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:PlaceHolder ID="UserBlogHolder" runat="server">
        <div style="background:url(images/index_title.png) right no-repeat; height:250px;">
            <span class=" page-header" style="position:relative;left:120px;top:80px;font-size:40px;">ITBLOG</span>
        </div>
        <div style="margin-top:10px;overflow:hidden;">
            <div class="list-group" style="height:350px;width:200px;float:left;position:fixed;top:330px;">
                <a href="Index.aspx" class="list-group-item active">全部分类</a>
                <a href="Index.aspx?par_cate=1" class="list-group-item list-group-item-success">移动开发</a>
                <a href="Index.aspx?par_cate=2" class="list-group-item list-group-item-info">编程语言</a>
                <a href="Index.aspx?par_cate=3" class="list-group-item list-group-item-warning">web开发</a>
                <a href="Index.aspx?par_cate=4" class="list-group-item list-group-item-danger">系统运维</a>
            </div>
            <div style="margin-left:215px;width:81%;float:left;" class="panel panel-primary" id="articlesContainer" runat="server">
                <div class="panel-heading" id="panelTitle" runat="server">XXXXXX</div>
                <div class="panel-body">
                    <asp:PlaceHolder ID="articleCategoryHolder" runat="server"></asp:PlaceHolder>
                </div>
            </div>
        </div>
    </asp:PlaceHolder>
</asp:Content>
