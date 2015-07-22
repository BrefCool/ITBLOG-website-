<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserInfo.aspx.cs" Inherits="BLOG_TEST.UserInfo" validateRequest="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!--个人信息-->
    <div class="panel UserInfo" style="background-color:#ffffff;height:250px;border:solid 1px #0094ff" >
        <table style="text-align:center;width:18%;height:250px;float:left;">
                <tr>
                <td><br /><asp:Image runat="server" ID="HeadImg" Height="120px" Width="120px"/></td>
                </tr>
                <tr>
                    <td><p style="font-size:15px;"><strong>加入博客时间</strong></p>
                        <asp:Label ID="signTimeLabel" runat="server" Text="" Font-Size="Medium"></asp:Label>
                    </td>
                </tr>
        </table>
        <div style="background-color:transparent;height:250px;margin-left:10px;float:left;width:80%;">
            <img class="editImg" src="images/edit_icon.png" style="position:relative;left:880px;top:10px;"/>
            <br />
            <ul class="list-unstyled list-inline" >
            <li><asp:Label ID="Info_userNameLabel" runat="server" Text="XXXXXXXXXX" Font-Bold="true" Font-Size="28px"></asp:Label></li>
            <li><button type="button" class="btn btn-default"  data-toggle="modal" data-target="#headImgUploadModal" >上传头像</button></li>
            </ul>
            <ul class="list-unstyled list-inline" >
                <li><asp:Label ID="Info_userSexLabel" runat="server"  Font-Size="Small" ></asp:Label>
                    <asp:TextBox ID="Info_userSexTextBox" runat="server" Font-Size="Small" ></asp:TextBox>
                </li><li><p>   |   </p></li>
                <li><asp:Label ID="Info_userBirthLabel" runat="server" Font-Size="Small" ></asp:Label>
                    <asp:TextBox ID="Info_userBirthTextBox" runat="server" Font-Size="Small" ></asp:TextBox>
                </li><li><p>   |   </p></li>
                <li><asp:Label ID="Info_userEmailLabel" runat="server" Font-Size="Small" ></asp:Label>
                    <asp:TextBox ID="Info_userEmailTextBox" runat="server" Font-Size="Small" ></asp:TextBox>
                </li><li><p>   |   </p></li>
                <li><asp:Label ID="Info_userQQLabel" runat="server" Font-Size="Small" ></asp:Label>
                    <asp:TextBox ID="Info_userQQTextBox" runat="server" Font-Size="Small" ></asp:TextBox>
                </li>
            </ul>
            <h5><strong>个人简介</strong></h5>
            <asp:Label ID="Info_userIntroduceLabel" runat="server" Cssclass="panel" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" Height="80px" Width="100%" style="word-break:break-all;" ></asp:Label>
            <asp:TextBox ID="Info_userIntroduceTextBox" runat="server" Font-Size="Small" TextMode="MultiLine" Rows="3" Width="100%"></asp:TextBox>
            <asp:Button ID="editButton" runat="server" CssClass="btn btn-primary" Text="保存" OnClick="editButton_Click" OnClientClick="return edit()"/>
        </div>
    </div>
    <!--按钮组-->
    <div class="btn-group btn-group-justified" role="group" style="margin-top:10px;">
        <div class="btn-group" role="group">
            <asp:Button ID="myMessageBtn" runat="server" Cssclass="btn btn-default"  Text="我的消息" BorderColor="#0094ff" BorderStyle="Solid" BorderWidth="1px" Font-Bold="true" ForeColor="#0094ff" Font-Size="15px" OnClick="myMessageBtn_Click" OnClientClick="return shutdownall()"></asp:Button>
        </div>
        <div class="btn-group" role="group">
            <asp:Button ID="myBlogBtn" runat="server" Cssclass="btn btn-default" BorderColor="#0094ff" BorderStyle="Solid" BorderWidth="1px" Text="我的博客" Font-Bold="true" ForeColor="#0094ff" Font-Size="15px" OnClick="myBlogBtn_Click" OnClientClick="return shutdownall()"></asp:Button>
        </div>
        <div class="btn-group" role="group">
            <asp:Button ID="myCollectBtn" runat="server" Cssclass="btn btn-default" BorderColor="#0094ff" BorderStyle="Solid" BorderWidth="1px" Text="我的收藏" Font-Bold="true" ForeColor="#0094ff" Font-Size="15px" OnClick="myCollectBtn_Click" OnClientClick="return shutdownall()"></asp:Button>
        </div>
    </div>
    <!--详细显示-->
    <div class="panel" style="background-color:#ffffff;border:solid 1px #0094ff;margin-top:10px;">
    <!--我的消息-->
    <asp:PlaceHolder ID="myMessageHolder" runat="server" Visible="false">
        <h4><strong>我的消息</strong></h4>
    </asp:PlaceHolder>
    <!--我的博客-->
    <asp:PlaceHolder ID="myBlogHolder" runat="server" Visible="true">
        <div style="width: 80%; margin-left: auto; margin-right: auto; text-align: center; background: url(images/tomyBlog_background.PNG) no-repeat; height:140px; " >
            <a href="UserArticlesManager_addArticles.aspx" class="btn btn-primary btn-lg" role="button" style="margin-top:50px;margin-right:10px;">文章管理</a>
            <a href="#" id="toMyBlog" class="btn btn-primary btn-lg" role="button" style="margin-top:50px;" runat="server">前往我的博客</a>
        </div>
    </asp:PlaceHolder>
    <!--我的收藏-->
    <asp:PlaceHolder ID="myCollectHolder" runat="server" Visible="false">
        <h4><strong>我的收藏</strong></h4>
    </asp:PlaceHolder>
    </div>
            
    <!--头像上传-->
    <div class="modal fade bs-example-modal-sm" id="headImgUploadModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
           <div class="modal-dialog modal-sm">
               <div class="modal-content">
                   <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title" id="myModalLabel">头像上传</h4>
                   </div>
                   <div class="modal-body">
                       <asp:FileUpload runat="server" ID="HeadImgUpload" />
                   </div>
                   <div class="modal-footer">
                       <asp:Button ID="HeadImgUploadBtn" runat="server" Text="上传" CssClass="btn btn-primary" OnClick="HeadImgUploadBtn_Click" OnClientClick="return upload()"/>
                   </div>
               </div>
           </div>
    </div>
</asp:Content>
