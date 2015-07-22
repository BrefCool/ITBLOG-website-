<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserSendMessages.aspx.cs" Inherits="BLOG_TEST.UserSendMessages" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel" style="margin-top:5px;width:100%;background-color:#ffffff;border:1px solid #0094ff;">
        <div style="border-bottom:1px solid #0094ff;font-size:12px;color:#0094ff;">发件人</div>
        <div><asp:TextBox ID="senderTextBox" runat="server" Width="100%" ></asp:TextBox></div>
        <div style="border-bottom:1px solid #0094ff;font-size:12px;color:#0094ff;">收件人</div>
        <div><asp:TextBox ID="receiverTextBox" runat="server" Width="100%" ></asp:TextBox></div>
        <div style="border-bottom:1px solid #0094ff;font-size:12px;color:#0094ff;">内容</div>
        <div><asp:TextBox ID="messageTextBox" runat="server" Width="100%"  TextMode="MultiLine" Rows="3"></asp:TextBox></div>
        <div style="text-align:right;margin-right:20px;"><asp:Button ID="sendBtn" runat="server" CssClass="btn btn-primary" Text="发送" OnClick="sendBtn_Click" OnClientClick="return shutdownCollection()" /></div>
    </div>
</asp:Content>
