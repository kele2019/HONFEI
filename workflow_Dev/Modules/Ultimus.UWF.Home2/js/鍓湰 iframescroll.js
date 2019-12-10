$(window).resize(function () {
    onWindowResize();
});
$(document).ready(function () {
    var parentFrm = $(window.parent.document).find("#fContent");
    parentFrm.height(500);
    onWindowResize();
});
function onWindowResize() {
    $(window.parent.document).find("#bigFrame").height(500);
    var height = $(document).height();
    var parentFrm = $(window.parent.document).find("#fContent");
    parentFrm.height(height);
    window.parent.onInitBigFrameBackground();
}