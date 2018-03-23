$(function () {
    $('.navbar-toggle').click(function () {
        $('.navbar-nav').toggleClass('slide-in');
        $('.side-body').toggleClass('body-slide-in');
        $('#search').remaoveClass('in').addClass('collapse').slideUp(200);

        /// uncomment code for absolute positioning tweek see top comment in css
        //$('.absolute-wrapper').toggleClass('slide-in');

    });

    // Remove menu for searching
    $('#search-trigger').click(function () {
        $('.navbar-nav').removeClass('slide-in');
        $('.side-body').removeClass('body-slide-in');

       
        /// uncomment code for absolute positioning tweek see top comment in css
        //$('.absolute-wrapper').removeClass('slide-in');
    });

    function QuickSearch(IDs) {
        $.ajax({
            type: 'post',
            url: '/Product/QuickSearch',
            data: { strids: IDs },
            success: function (strhtmlresponse) {
                $('#quicksearchproduct').remove();
                $('#QuickSearchDialog').append(strhtmlresponse);

                $('#QuickSearchDialog').show();
                $('#QuickSearchDialog').dialog({
                    closeText: '',
                    title: 'Tìm kiếm thông tin sản phẩm nhanh',
                    autoOpen: false,
                    width: 1100,
                    height: 500,
                    resizable: true,
                    modal: true
                });

                $('#QuickSearchDialog').dialog('open');

            },
            error: function () {
                alert('error');
            }
        });
    }

    $('#btnquicklysearch').bind('click', function () {

        var IDsData = $('#quicklysearch').val();
        QuickSearch(IDsData);

    });
  
    $('.quicksearchindialog').bind('click', function () {
        var IDsData = $('#QuickSearchDialog #quicklysearch').val();
        QuickSearch(IDsData);

    });

    $('#quicklysearch').keypress(function (e) {
        var key = e.which;
        if (key == 13)  // the enter key code
        {
            var IDsData = $(this).val();
            QuickSearch(IDsData);
            return false;
        }
    });

    $('#QuickSearchDialog #quicklysearch').keypress(function (e) {
        var key = e.which;
        if (key == 13)  // the enter key code
        {
            var IDsData = $(this).val();
            QuickSearch(IDsData);
            return false;
        }
    });
});
