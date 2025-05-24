$(function () {
    let counterError = 0,
        actualStep = 1,
        maxActualStep = 2,
        errorArray = {
            errorStep: 'Вы не можете перейти на этот шаг.',
            errorStepVal: 'Заполните поля для перехода на следующий шаг.',
            errorFormVal: 'Заполните выделенные поля для отправки формы.',
            errorInput: 'Заполните поле.',
            errorInputFile: 'Добавьте файл.',
            errorInputEmail: 'Email введен некорректно.'
        };

    $(document).on('click', '[data-next-step]', function () {
        let next = parseInt($(this).data('next-step')),
            thisStep = $('[data-step].active');


        if( next > maxActualStep ){
            validation(thisStep);
            errorStep(errorArray.errorStep);
            cartSidebarSticky.setSizes();
            return;
        }

        if( next <= maxActualStep ) {
            validation(thisStep);
            if(counterError < 1 ) {
                delHelip();
                toggleStep(next);
            }
        }

    });

    function toggleStep(next) {
        actualStep = next;

        if(actualStep === maxActualStep) {
            maxActualStep += 1;
        }

        setTimeout(() => {
            if (!$('.form-group__error').length) {
                $('.steps__item').removeClass('active');
                $('.steps__item[data-next-step='+next+']').addClass('active');
                $('[data-step]').removeClass('active');
                $('[data-step='+next+']').addClass('active');
            }
        }, 200); 

        if($('.sidebar').length) {
            cartSidebarSticky.setSizes();
        }
    }

    function delHelip() {
        $('.form-step-error').remove();
    }

    function errorStep(error) {
        delHelip();
        $('.steps_wrapper').prepend('<div class="form-step-error">'+error+'</div>');
    }

    function errorInput(t, error) {
        $(t).prepend('<div class="form-group__error">'+error+'</div>');
    }

    function clearErrors() {
        $('.form-group__error').remove();
        $('.form-group').removeClass('error');
    }

    function validation(t) {
        let input = t.find('select, input');

        clearErrors();

        counterError = 0;
        input.each(function () {
            let $t = $(this),
                formGroup = $t.closest('.form-group:not(.form-bid)'),
                type = $t.attr('type'),
                valInput = $t.val();

            if($t.attr('data-validation') === "N" && type == 'radio') {
                return;
            }

            if(type === "file") {
                if(!valInput) {
                    errorInput(formGroup, errorArray.errorInputFile);
                    return;
                }
            }

            if(type ==='email'){
                let text = $t.val();
                if(text.indexOf('@') <= 1 || text.length < 4 ){
                    counterError += 1;
                    $t.closest('.form-group').addClass('error');
                    if(!valInput) errorInput(formGroup, errorArray.errorInput);
                    if(valInput) errorInput(formGroup, errorArray.errorInputEmail);
                }
            }else {
                if(!valInput){
                    counterError += 1;
                    console.log(valInput);
                    formGroup.addClass('error');
                    errorInput(formGroup, errorArray.errorInput);
                    if(!$t.closest('.attachment-points').length ) {
                        errorStep(errorArray.errorStepVal);
                    }
                }
            }
        });
    }

    $(document).on('change', 'input', function () {
        $(this).closest('.form-group').find('.form-group__error').remove();
        $(this).closest('.form-group').removeClass('error');
    });//удаление ошибки при изменении инпута

    $(document).on('change', '.form-group.error .form-group__input', function () {
        $(this).closest('.form-group').removeClass('error').removeClass('active').find('.form-group__error').remove();
    });

    $(document).on('click', '[data-toggle-step]', function () {
        let t = $(this),
            getNextStep = t.attr('data-toggle-step');

        maxActualStep = getNextStep;
        t.closest('.steps_content ').find('[data-next-step]').attr('data-next-step', getNextStep);
    });

    //// Точки присоединения

    //$('#points').submit(function (e) {
    //    e.preventDefault();

    //    validation($('.attachment-points'));
    //    if (counterError > 0) {
    //        console.log('ошибка'); return;
    //    }

    //    let currentPoints = parseInt($('#currentPoints').val());

    //    currentPoints += 1;

    //    $('#currentPoints').val(currentPoints);
            

    //    let t = $(this),
    //        pointsInfo = {
    //            description: '',
    //            power: '',
    //        },
    //        content = t.closest('.modal__content');

    //        content.find('select, input').each(function () {
    //        let t = $(this),
    //            value = t.val();

    //        if(t.hasClass('description')){
    //            pointsInfo.description = value;

    //        }
    //        if(t.hasClass('power')){
    //            pointsInfo.power = value;
    //        }

    //    });

    //    $('.connection_points__col').removeClass('is-active');
    //    $('.connection_points').find('.form-group__error').remove();

    //    $('.connection_points__tabl ').append('<div id="points_row" class="connection_points__row" data-points="">\n' +
    //       '                                                        <div class="connection_points__col"><span class="xs-show-inlane">Номер точки: </span> '+currentPoints+'</div>\n' +
    //       '                                                        <div class="connection_points__col"><span class="xs-show-inlane">Описание точки: </span> '+pointsInfo.description+'</div>\n' +
    //       '                                                        <div class="connection_points__col"><span class="xs-show-inlane">Мощность: </span> '+pointsInfo.power+'</div>\n' +
    //       '                                                        <input hidden="" type="number" name="attachpoints'+currentPoints+'" value="'+pointsInfo.description+'">\n' +
    //       '                                                        <input hidden="" type="number" name="attachpoints'+currentPoints+'p" value="'+pointsInfo.power+'">\n' +
   
    //       '                                                    </div>');

            
    //    modal.close('attachment_points');

    //    if (PointObj.volt != null) {
    //        $('.attachment-points .power ').val('');
    //    } else if (PointObj.power != null) {
    //        $('.attachment-points .description ').val('');
    //    } else {
    //        $('.attachment-points .power,.description ').val('');
    //    }
        
    //    cartSidebarSticky.setSizes();

    //    if (currentPoints === 1) {
    //        var power_modal = document.getElementById("modal_etap_power");
    //        power_modal.value = pointsInfo.power;            
    //    }
    //});

    //$(document).on('click', '.connection_points__del', function (e) {
    //    e.preventDefault();

    //    $('#points_row:last-child').remove();
    //    cartSidebarSticky.setSizes();

    //    let currentPoints = parseInt($('#currentPoints').val());
    //    if (currentPoints > 0) {
    //        currentPoints -= 1;

    //        $('#currentPoints').val(currentPoints);
    //    }
    //});

    //let lnk = $('.steps__item');


    //$(document).on('click', '.steps__item', ()=> {
    //    $('.steps__item').removeClass('active');
    //    $(".steps__item[href*='" + location.pathname + "']").addClass('active');
    //});


    //$(".steps__item").each(function() {
    //    if (this.href == window.location.href) {
    //        $(this).addClass("active");
    //    }
    //});

    // Этапы строительства 

    //$('#etaps').on('click', function (e) {
    //    e.preventDefault();

    //    validation($('.attachment-etap'));
    //    if (counterError > 0) {            
    //        console.log('ошибка'); return;
    //    }

    //    let currentEtap = parseInt($('#currentEtap').val()) || 0;

    //    currentEtap += 1;

    //    $('#currentEtap').val(currentEtap);

    //    let t = $(this),
    //        etapsInfo = {
    //            date_proect: '',
    //            date_vvod: '',
    //            power: '',
    //        },
    //        content = t.closest('.modal__content');

    //        content.find('input').each(function () {
    //        let t = $(this),
    //            value = t.val();

    //        if(t.hasClass('date_proect')){
    //            etapsInfo.date_proect = value;
    //        }
    //        if(t.hasClass('date_vvod')){

    //            etapsInfo.date_vvod = value;
    //        }
    //        if(t.hasClass('power')){
                        
    //            etapsInfo.power = value;
    //        }
    //    });

    //    $('.connection_points').find('.form-group__error').remove();

    //    $('#etap_tab').append(
    //    '                                                        <div id="etap_row" class="connection_points__row" data-points="">\n' +
    //    '                                                        <div class="connection_points__col"><span class="  ">Номер точки: </span> '+currentEtap+'</div>\n' +
    //    '                                                        <div class="connection_points__col"><span class="xs-show-inlane">Описание точки: </span> '+etapsInfo.date_proect+'</div>\n' +
    //    '                                                        <div class="connection_points__col"><span class="xs-show-inlane">Мощность: </span> '+etapsInfo.date_vvod+'</div>\n' +
    //    '                                                        <div class="connection_points__col"><span class="xs-show-inlane">Мощность: </span> '+etapsInfo.power+'</div>\n' +
    //    '                                                        <input hidden="" type="date" name="attachetap'+currentEtap+'" value="'+etapsInfo.date_proect+'">\n' +
    //    '                                                        <input hidden="" type="date" name="attachetap'+currentEtap+'v" value="'+etapsInfo.date_vvod+'">\n' +
    //    '                                                        <input hidden="" type="number" name="attachetap'+currentEtap+'p" value="'+etapsInfo.power+'">\n' +
    //    '                                                        </div>'
    //    );

            
    //    modal.close('attachment_etap');

    //    $('.attachment-etap input').val('');
    //    cartSidebarSticky.setSizes();
    //});

    //$(document).on('click', '.connection_etaps__del', function (e) {
    //    e.preventDefault();

    //    $('#etap_row:last-child').remove();
    //    cartSidebarSticky.setSizes();
    //    let currentEtap = parseInt($('#currentEtap ').val());

    //    if (currentEtap > 0) {
    //        currentEtap -= 1;
    //        $('#currentEtap').val(currentEtap);
    //    }
    //});

});


