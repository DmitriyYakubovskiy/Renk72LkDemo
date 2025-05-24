
const div = document.createElement('div');
div.style.overflowY = 'scroll';
div.style.width = '50px';
div.style.height = '50px';
div.style.visibility = 'hidden';
document.body.appendChild(div);
const scrollWidth = div.offsetWidth - div.clientWidth;
document.body.removeChild(div);

const body = document.getElementsByTagName("body")[0];
let bodyScrollTop = null;
let locked = false;

lockScroll = () => {
    if (!locked) {
        if (window.document.documentElement.clientHeight < body.scrollHeight) {
            body.style.paddingRight = scrollWidth + 'px';
        }
        bodyScrollTop =
            typeof window.pageYOffset !== "undefined"
                ? window.pageYOffset
                : (document.documentElement ||
                document.body.parentNode ||
                document.body).scrollTop;

        body.classList.add("scroll-locked");

        body.style.top = `-${bodyScrollTop}px`;
        locked = true;
    }
};

unlockScroll = () => {
    if (locked) {
        body.classList.remove("scroll-locked");

        body.style.top = null;
        window.scrollTo(0, bodyScrollTop);
        body.style.paddingRight = '';
        locked = false;
    }
};

class Modal {
    constructor() {
        this.modalSelector = '[data-modal]';
        this.body = $('body');
        this.doc = $(document);
        this.closeTimeout = 300;
        this.activeClass = 'active';
        this.loadingClass = 'modal-loading';
    }

    isOtherOpen() {
        return $(this.modalSelector).filter(`.${this.activeClass}`).length;
    }

    open (id) {
        const $modal = $(`[data-modal=${id}]`);

        lockScroll();

        $modal.addClass(this.activeClass).focus();
        this.body.addClass('modal-open');
        this.doc.trigger('modal-open', [id]);
    }

    openWithAjax (id, params) {
        const isModalExist = $(`[data-modal=${id}]`).length;

        if (isModalExist) {
            this.open(id);
            return;
        }
        //console.log(id);
        let ajaxUrl = location.hostname === 'localhost' ? `/ajax/${id}.html` : `/local/ajax/`;
        let ajaxDataType = location.hostname === 'localhost' ? `html` : `json`;
        let ajaxType = location.hostname === 'localhost' ? `GET` : `POST`;
        $.ajax({
            type: ajaxType,
            url: ajaxUrl,
            data: {
                action: 'LoadBlocks/getModal',
                modalID: id,
                params
            },
            dataType: ajaxDataType,
            beforeSend: () => {
                this.showLoader();
            },
            success: (data) => {
                const dataHtml = data.MODAL_HTML ? data.MODAL_HTML : data;
                document.body.insertAdjacentHTML('beforeend', dataHtml);
                modal.open(id);
            },
            error: () => {
                this.showError();
            },
            complete: () => {
                this.hideLoader();
            }
        });
    }

    close(id) {
        const $modal = $(`[data-modal=${id}]`),
            dataOnClose = $modal.data('modal-onclose');

        if (id) {
            $modal.removeClass(this.activeClass);
        } else {
            $('[data-modal]').removeClass(this.activeClass);
        }

        if (!this.isOtherOpen()) {
            setTimeout(() => {
                unlockScroll();
                this.body.removeClass('modal-open');

                if (dataOnClose === 'remove') {
                    $modal.remove();
                }
            }, this.closeTimeout);
        }

        this.doc.trigger('modal-close', [id]);
    }

    toggle(id) {
        const $modal = $(`[data-modal=${id}]`);
        const isActive = $modal.hasClass(this.activeClass);
        if (isActive) {
            this.close(id);
        } else {
            this.open(id);
        }
    }

    showLoader() {
        let $loader = $(`.modal-loader`);

        if (!$loader.length) {
            $loader = $(`<div class="modal-loader" />`);
            this.body.append($loader);
        }

        this.body.addClass(this.loadingClass);
    }

    hideLoader() {
        this.body.removeClass(this.loadingClass);
    }

    showError() {
        let $errorModal = $(`[data-modal="modal-error"]`);

        if (!$errorModal.length) {
            $errorModal = $(`<div class="modal" data-modal="modal-error"><div class="modal__error">Произошла ошибка</div></div>`);
            this.body.append($errorModal);
        }

        this.open('modal-error');
    }
}

const modal = new Modal();

$(function () {
    const $document = $(document);

    $document.on('click', '[data-modal-open]', function (e) {
        e.preventDefault();
        const $this = $(this),
            modalId = $this.data('modal-open');
        modal.open(modalId);
        // $('.form-profile .lk-block-info__row').addClass('no-position');
        // $('.form-profile .form-group').addClass('no-flex');
        // $('.form-profile .select-block').addClass('no-flex');
        // $('.form-profile .select-block__title').addClass('no-position');
        // $('.form-profile .form-group__box').addClass('no-position');
    });

    // $(document).on('click', '.modal__inner, .modal__close', function (e) {
    //     $('.form-profile .lk-block-info__row').removeClass('no-position');
    //     $('.form-profile .form-group').removeClass('no-flex');
    //     $('.form-profile .select-block').removeClass('no-flex');
    //     $('.form-profile .select-block__title').removeClass('no-position');
    //     $('.form-profile .form-group__box').removeClass('no-position');
        
    // });



    $document.on('click', '[data-modal-ajax-open]', function (e) {
        e.preventDefault();

        const $this = $(this),
            modalId = $this.data('modal-ajax-open'),
            params = $this.data('modal-params');

        modal.openWithAjax(modalId, params);

    });

    $document.on('click', '[data-modal-close]', function (e) {
        e.preventDefault();
        const $this = $(this),
            modalId = $this.data('modal-close');
        modal.close(modalId);
    });

    $document.on('click', '[data-modal]', function (e) {
        if ($(e.target).closest('[data-modal-inner]').length) return;
        e.preventDefault();
        const modalId = $(e.currentTarget).data('modal');
        modal.close(modalId);
    });


    
});

