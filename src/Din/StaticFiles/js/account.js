﻿/* Menu Highlighting */

$(document).delegate('.nav a',
    'click',
    function() {
        $('.nav').find('.active').removeClass('active');
        $(this).parent().addClass('active');
    });

/* Menu Navigation render partial views */

$(document).delegate('#account-menu-btn-information',
    'click',
    function(e) {
        hideAllViews();
        $('#account-view-info').css({ display: '', opacity: 0 }).animate({ opacity: 1 }, 500).val('visible');
    });

$(document).delegate('#account-menu-btn-addedcontent',
    'click',
    function(e) {
        hideAllViews();
        $('#account-view-addedcontent').css({ display: '', opacity: 0 }).animate({ opacity: 1 }, 500).val('visible');
    });

$(document).delegate('#account-menu-btn-calendar',
    'click',
    function(e) {
        hideAllViews();
        $('#account-view-calendar').css({ display: '', opacity: 0 }).animate({ opacity: 1 }, 500).val('visible');
    });

function hideAllViews() {
    $.each($('.data-pane').find('.account-partial'),
        function(i, e) {
            if ($(e).val() === 'visible') {
                $(e).css({ display: 'none' }).val('');
                return false;
            }
        });
}

/* Account validate email */

$(document).delegate('#invite-friend-input',
    'keyup',
    function() {
        var value = $(this).val();
        if (validateEmail(value)) {
            $('#invite-friend-btn').attr('disabled', false);
        } else {
            $('#invite-friend-btn').attr('disabled', true);
        }
    });

function validateEmail(email) {
    var re =
        /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
}

/* account validate account deletion */

$(document).delegate('#delete-account-input',
    'keyup',
    function() {
        var value = $(this).val();
        if (value === 'delete') {
            $('#delete-account-btn').attr('disabled', false);
        } else {
            $('#delete-account-btn').attr('disabled', true);
        }
    });