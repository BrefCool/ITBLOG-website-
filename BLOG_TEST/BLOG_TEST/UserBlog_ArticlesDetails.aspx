<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserBlog_ArticlesDetails.aspx.cs" Inherits="BLOG_TEST.UserBlog_ArticlesDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="background:url(images/bg_title_blue.png);height:300px;">
        <a class=" page-header" style="position:relative;left:150px;top:80px;font-size:30px;" id="blogNameLink" runat="server" >XXXXXXXXx</a>
    </div>
    <div id="mainContainer" style="margin-top:10px;overflow:hidden;">
        <div id="userInfoContainer" style="width:180px;float:left;width:15%;">
            <div class="panel" style="background-color:#ffffff;border:solid 1px #0094ff;height:500px;">
                <div class="flip" style="background-color:#0099FF;color:#ffffff;height:20px;">个人资料</div>
                <table class="userInfo" style="text-align:center;height:450px;width:100%;">
                    <tr>
                        <td><asp:Image ID="userHeadImg" runat="server" ImageUrl="images/default_user.png" Height="120px" Width="120px"/></td>
                    </tr>
                    <tr>
                        <td><asp:Label ID="userNameLabel" runat="server" Text="XXXXX" Font-Bold="true" Font-Size="20px"></asp:Label>
                            <br />
                            <a role="button" class="btn btn-success" id="sendMessages" href="UserSendMessages.aspx" runat="server">发私信</a>
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
        <div id="articleContainer" style="float:left;width:84%" >
            <div class="panel" style="background-color: #ffffff;border:solid 1px #0094ff;margin-left:10px;width:100%;">
            <div style="margin-left:auto;margin-right:auto;margin-top:10px;width:96%;text-align:left;">
                <asp:Label runat="server" ID="ArticleTitleLabel" Text="XXXXXXXXXXXX" Font-Size="28px" Font-Bold="true"></asp:Label>
            </div>
            <div style="margin-left:auto;margin-right:auto;width:96%;text-align:right;">
                <ul class="list-unstyled list-inline">
                    <li><asp:Label ID="publishTimeLabel" runat="server" Text="XXXX-XX-XX" Font-Size="12px"></asp:Label></li>
                    <li><p style="font-size:12px;">阅读人数:</p></li>
                    <li><asp:Label ID="readTimesLabel" runat="server"></asp:Label></li>
                    <li><a style="font-size:12px;" href="#anchor">评论</a></li>
                </ul>
            </div>
            <div style="margin-left:auto;margin-right:auto;margin-top:10px;width:96%;text-align:left;">
                <asp:Label ID="ArticleContentLabel" runat="server" Text="XXXXXXXXXXXXXXXX" Font-Size="18px"></asp:Label>
            </div>
        </div>
            <div class="panel" style="background-color: #ffffff;border:solid 1px #0094ff;float:left;margin-left:10px;width:100%;margin-top:10px;">
            <div style="background-color:#0099FF;color:#ffffff;height:20px;">查看评论</div>
            <div>
                <asp:PlaceHolder ID="commentsHolder" runat="server">
                    <%--<div style="width:100%;">
                        <div class="panel" style="width:100%;background-color:#CCCCCC;height:90px;">
                            <asp:Label ID="commentInfoLabel" runat="server" Text="XX楼XXXXX于XXXX-XX-XX XX:XX:XX 发表" Font-Size="10px" Height="20px"></asp:Label>
                            <a style="font-size:10px;">回复</a>
                            <div style="height:70px;">
                            <table style="text-align:left;width:100%;background-color:#ffffff">
                                <tr>
                                    <td style="width:70px;"><asp:Image ID="commenterHeadImg" runat="server" ImageUrl="images/default_user.png" Width="64px" Height="64px" /></td>
                                    <td><asp:Label ID="commentContent" runat="server" style="word-break:break-all;" Text="XXXXXXXXXXXXXXXXXXXXXXXX"></asp:Label></td>
                                </tr>
                            </table>
                        </div>
                        </div>
                    </div>--%>
                </asp:PlaceHolder>
                <div style="width:100%;background-color:#ffffff;height:50px;text-align:center;border-top:#CCCCCC 1px solid;">
                    <h3>没有评论了...</h3>
                </div>
            </div>
        </div>
            <div class="panel" style="background-color: #ffffff;border:solid 1px #0094ff;float:left;margin-left:10px;width:100%;margin-top:10px;">
            <div style="background-color:#0099FF;color:#ffffff;height:20px;">添加评论</div> 
            <div style="height:200px;">
                <table style="width:90%;text-align:center;margin-left:auto;margin-right:auto;">
                    <tr>
                        <td style="width:10%;"><p>用户名</p></td>
                        <td style="text-align:left;"><a name="anchor"><asp:Label ID="commentsProviderLabel" runat="server" Text="XXXXXX"></asp:Label></a></td>
                    </tr>
                    <tr>
                        <td style="width:10%;">评论内容</td>
                        <td style="text-align:left;">
                            <asp:TextBox ID="commentDetailBox" runat="server" CssClass="panel" BorderColor="#0094ff" BorderStyle="Solid" TextMode="MultiLine" BorderWidth="1px" Rows="5" Width="100%">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:10%;">&nbsp;</td>
                        <td style="text-align:right;"><asp:Button ID="PostBtn" runat="server" CssClass="btn btn-primary" Text="提交" OnClick="PostBtn_Click" OnClientClick="return shutdownall();"/></td>
                    </tr>
                </table>
            </div>   
        </div>
        </div>  
    </div>

</asp:Content>
