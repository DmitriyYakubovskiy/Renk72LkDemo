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
});


