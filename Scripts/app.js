$(document).ready(function () {
    $('#selectInvestors').popover({
        html: true,
        content: function () {
            return $('#selectInvestorsPopover').html();
        }
    });
});