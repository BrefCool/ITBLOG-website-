function showTextarea(K) {
    var editor1 = K.create('#MainContent_blogTextarea', {
        cssPath: 'kindeditor-4.1.10/plugins/code/prettify.css',
        uploadJson: 'kindeditor-4.1.10/asp.net/upload_json.ashx',
        fileManagerJson: 'kindeditor-4.1.10/asp.net/file_manager_json.ashx',
        allowFileManager: false,
        allowPreviewEmoticons: true,
        allowImageUpload: false,
        items: [
                'fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold', 'italic', 'underline',
                'removeformat', 'code', '|', 'justifyleft', 'justifycenter', 'justifyright', 'insertorderedlist',
                'insertunorderedlist', '|', 'emoticons', 'image', 'link']
    });
    prettyPrint();
}
KindEditor.ready(function (K) {
    showTextarea(K);
});
function showmodal() {
    $('#myModal').modal('show');
}
function shutdown1() {
    $("#SignButton").attr("Disabled", true);
}
function shutdown2() {
    $("#LoginButton").attr("Disabled", true);
}
function shutdownall() {
    $("#SignButton").attr("Disabled", true);
    $("#LoginButton").attr("Disabled", true);
    $("#MainContent_editButton").attr("Disabled", true);
}
function shutdownCollection() {
    $("#collectButton").attr("Disabled", true);
}
function blogPublish() {
    shutdownall();
    edit();
}
function edit() {
    $("#MainContent_myCollectBtn").attr("Disabled", true);
    $("#MainContent_myBlogBtn").attr("Disabled", true);
    $("#MainContent_myMessageBtn").attr("Disabled", true);
}
function reDirect() {
    self.location = 'UserInfo.aspx';
}
function toSendMessages() {
    self.location = 'UserSendMessages.aspx';
}
function toWriteBlog() {
    self.location = 'UserArticlesManager_addArticles.aspx';
}
function upload() {
    edit();
    shutdownall();
}
function userInfoPrepare() {
    $("#SignButton").attr("Disabled", true);
    $("#LoginButton").attr("Disabled", true);
    $(".editImg").toggle();
    $("#MainContent_editButton").hide();
    $("#MainContent_editButton").attr("Disabled", true);
    $("#MainContent_Info_userSexTextBox").hide();
    $("#MainContent_Info_userBirthTextBox").hide();
    $("#MainContent_Info_userEmailTextBox").hide();
    $("#MainContent_Info_userQQTextBox").hide();
    $("#MainContent_Info_userIntroduceTextBox").hide();
}
$(document).ready(function () {
    $(".latestBlogTitle").click(function () {
        $(".latestBlog").slideToggle("slow");
    });
    $(".editImg").click(function () {
        $("#MainContent_Info_userSexLabel").hide();
        $("#MainContent_Info_userSexTextBox").show();
        $("#MainContent_Info_userBirthLabel").hide();
        $("#MainContent_Info_userBirthTextBox").show();
        $("#MainContent_Info_userEmailLabel").hide();
        $("#MainContent_Info_userEmailTextBox").show();
        $("#MainContent_Info_userQQLabel").hide();
        $("#MainContent_Info_userQQTextBox").show();
        $("#MainContent_Info_userIntroduceLabel").hide();
        $("#MainContent_Info_userIntroduceTextBox").show();
        $("#MainContent_editButton").show();
        $("#MainContent_editButton").attr("Disabled", false);
    });
    $(".hotBlogTitle").click(function () {
        $(".hotBlog").slideToggle("slow");
    });
    $(".UserInfo").mouseenter(function () {
        $(".editImg").toggle();
    });
    $(".UserInfo").mouseleave(function () {
        $(".editImg").toggle();
    });
});