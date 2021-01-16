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

function ImagePreview(fileEl, previewEl) {
    if (fileEl.files != null && fileEl.files.length > 0) {
        let preview = URL.createObjectURL(fileEl.files[0]);
        previewEl.style.backgroundImage = `url('${preview}')`;
        preview.addEventListener('load', function () {
            URL.revokeObjectURL(preview);
        });
    }
}

let AppHelpers = {
    authorsUi: {
        writeListEl: null,
        writeItemClassSelector: 'list-item',
        inputItemName: null,
        readAll: function () {
            let items = [];
            AppHelpers.authorsUi.writeListEl.querySelectorAll(`input.${AppHelpers.authorsUi.writeItemClassSelector}`).forEach(item => items.push(item.value.trim()));
            return items;
        },
        writeOne: function (itemName) {
            itemName = itemName.trim();
            if (itemName == "" || AppHelpers.authorsUi.hasWriteBefore(itemName)) return;

            let itemEl = document.createElement('div');
            itemEl.classList.add('alert', 'alert-light', 'border', 'alert-dismissible', 'fade', 'show', 'd-inline-block', 'mx-2');

            let itemInputEl = document.createElement('input');
            itemInputEl.classList.add(AppHelpers.authorsUi.writeItemClassSelector, 'd-none');
            itemInputEl.name = AppHelpers.authorsUi.inputItemName;
            itemInputEl.value = itemName;
            itemEl.appendChild(itemInputEl);

            let itemNameEl = document.createElement('span');
            itemNameEl.textContent = itemName;
            itemEl.appendChild(itemNameEl);

            let deleteEl = document.createElement('button');
            deleteEl.setAttribute('type', 'button');
            deleteEl.classList.add('btn-close');
            deleteEl.dataset.bsDismiss = "alert";

            itemEl.appendChild(deleteEl);
            AppHelpers.authorsUi.writeListEl.appendChild(itemEl);
        },
        writeRange: function (itemNames) {
            itemNames.forEach(item => AppHelpers.authorsUi.writeOne(item));
        },
        hasWriteBefore: function (itemName) {
            return AppHelpers.authorsUi.readAll().some(value => value == itemName);
        },
        ajaxSuggestion: function (name) {

        }
    }
};