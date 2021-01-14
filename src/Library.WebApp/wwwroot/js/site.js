const englishNumbersToPersian = s => String(s).replace(/\d/g, d => '۰۱۲۳۴۵۶۷۸۹'[d]);

$(document).ready(function () {
    $('<div class="mt-5"></div>').css("height", $('#footer').outerHeight()).appendTo('body>.wrapper');
    $('.js-tooltip').tooltip();
    $('.js-gotop').click(function () {
        $('html, body').animate({ scrollTop: 0 });
    });
    $('#js-nav-shortcuts-trigger').click(function () {
        $(this).siblings('#js-nav-shortcuts').toggleClass('open').hide().fadeIn();
    });
});

document.addEventListener('DOMContentLoaded', function () {
    let historyBackUrl = document.referrer;
    document.querySelectorAll('.js-history-back').forEach(function (El) {
        if (historyBackUrl) {
            El.setAttribute('href', historyBackUrl);
            El.addEventListener('click', (e) => { window.history.go(-1); e.preventDefault(); return false; });
            El.innerHTML = '<i class="bi bi-arrow-right"></i> بازگشت';
        }
        else {
            El.setAttribute('href', '/');
            El.innerHTML = '<i class="bi bi-house"></i> بازگشت به خانه';
        }
    });
});