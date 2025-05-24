// select
let selectTimeout;
$(document).on('click', '.select-block__title', function() {
    var $this = $(this),
        p = $this.closest('.select-block'),
        list = $this.siblings('.select-block__list'),
        notThis = $('.select-block__title').not($this);
    notThis.removeClass('active').closest('.select-block').removeClass('active').find('.select-block__list').slideUp(200);

    clearTimeout(selectTimeout);

    if ($this.hasClass('active')) {
        list.slideUp();
        selectTimeout = setTimeout(function() {
            p.removeClass('active');
            $this.removeClass('active');
        }, 200);
    } else {
        $this.addClass('active');
        p.addClass('active');
        list.slideDown(200);
    }
});

$(document).on('click', '.select-block__list a', function(e) {
    e.preventDefault();
    var $this = $(this),
        text = $this.text(),
        p = $this.closest('.select-block');
    p.find('input[type=hidden]').val(text);
    p.find('.select-block__title').removeClass('active').find('span').text(text);
    p.find('.select-block__list').slideUp(200);
});

$(document).click(function(e) {
    if ($(e.target).closest('.select-block').length) return;
    $('.select-block__title').removeClass('active');
    $('.select-block__list').slideUp(200);
    e.stopPropagation();
});
