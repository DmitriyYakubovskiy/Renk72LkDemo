let initStiky = false;

class Sticky {

    constructor(element) {
        if (!element) {
            return;
        }

        this._fixedClass = 'fixed';
        this._afterClass = 'flex-end';

        this._element = element;
        this._parentElement = this._element.parentElement;

        this.checkScroll = this.checkScroll.bind(this);

        this.init();
    }

    setSizes() {
        this._parentHeight = this._parentElement.offsetHeight;
        this._parentWidth = this._parentElement.offsetWidth;
        this._elHeight = this._element.offsetHeight;
        this._stickyTop = $(this._parentElement).offset().top;
        this._stickyBottom = this._stickyTop + (this._parentHeight - this._elHeight) ;
        this.checkScroll();
    }

    setOffsetTop(offsetTop) {
        this._offsetTop = offsetTop;
        this.setSizes();
    }

    checkScroll() {
        const st = window.pageYOffset;
        let state = null;

        if (st > this._stickyTop - 15  && st < this._stickyBottom - 15 ) {
            this._onFixed();
            state = 'fixed';
        }else {
            if(st < this._stickyTop ) {
                this._onBefore();
                state = 'before';
            }
            if(st >= this._stickyBottom - 15 ) {
                this._onAfter();
                state = 'after';
            }
        }

        return {
            scrollTop: st,
            state
        };
    }

    init() {
        this.setSizes();
        this._setEvents();
        initStiky = true;
    }

    destroy() {
        this._parentElement.classList.remove(this._afterClass);
        this._parentElement.classList.remove(this._fixedClass);
        this._removeEvents();
        initStiky = false;
    }

    _setEvents() {
        window.addEventListener('scroll', this.checkScroll, false);
        // window.addEventListener('resize', this.setSizes, false);
    }

    _removeEvents() {
        window.removeEventListener('scroll', this.checkScroll, false);
        window.removeEventListener('resize', this.setSizes, false);
    }

    _onFixed() {
        this._parentElement.classList.remove(this._afterClass);
        this._parentElement.classList.add(this._fixedClass);
        this._element.style.maxWidth = this._parentWidth + "px";
    }

    _onAfter() {
        this._parentElement.classList.remove(this._fixedClass);
        this._parentElement.classList.add(this._afterClass);
        this._element.style.maxWidth = "100%";
    }

    _onBefore() {
        this._parentElement.classList.remove(this._fixedClass);
        this._element.style.maxWidth = "100%";
    }
}

const SidebarStickyInit = () => {
    const $header = $('.header');
    const Sidebar = document.querySelector('.sidebar__box');

    if (!Sidebar) return;

    const SidebarSticky = new Sticky(Sidebar);
    window.cartSidebarSticky = SidebarSticky;

    const headerHeight = $header.height();
    let topOffset = 0;

    cartSidebarSticky.setOffsetTop(topOffset);
};

function startStiky() {
    if($(window).width() < 768 && initStiky) {
        cartSidebarSticky.destroy();
    }else {
        if (!initStiky){
            SidebarStickyInit();
        }
    }
}

$(function () {
    startStiky();

    $(window).on('resize', function () {

        if (initStiky) {
            cartSidebarSticky.setSizes();
        }

        startStiky();
    });
});


//когда будешь вызывать аякс или еще как то динамически будет подгружаться контент нужно вызвать метод setSizes()
//Пример аякса)0)))0))нулик
//$(document).on('click', '.request_block__more' , function (e) {
//    e.preventDefault();
//    $('.request_block').append('<a href="#" class="request">\n' +
//        '                            <div class="request__left">\n' +
//        '                                <div class="request__title">#23490823 Наименование заявки / обращения</div>\n' +
//        '                                <div class="request__list">\n' +
//        '                                    <div class="request__info">\n' +
//        '                                        <span class="request__name">Отдел:</span>\n' +
//        '                                        <span class="request__text">Обслуживающая компания</span>\n' +
//        '                                    </div>\n' +
//        '                                    <div class="request__info">\n' +
//        '                                        <span class="request__name">Услуга:</span>\n' +
//        '                                        <span class="request__text">Подключение энергопринимающих приборов до 150 кВт</span>\n' +
//        '                                    </div>\n' +
//        '                                </div>\n' +
//        '                            </div>\n' +
//        '                            <div class="request__right">\n' +
//        '                                <div class="request__status"><i class="far fa-window-close" aria-hidden="true"></i>Закрыто</div>\n' +
//        '                                <div class="request__data">от <span>10.05.2020</span></div>\n' +
//        '                            </div>\n' +
//        '                        </a>')

//    cartSidebarSticky.setSizes();
//})
