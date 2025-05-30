// policy
$(document).on('change', '[data-input-policy]', function() {
    let $this = $(this),
        p = $this.closest('form'),
        b = p.find('button[type=submit]');

    if($this.prop('checked')) {
        b.prop('disabled', false);
    }
    else {
        b.prop('disabled', true);
    }
});

//Совпадение адресов

$(document).on('keyup ', '[data-input-copy]', function () {
    let $t = $(this),
        parentContainer = $t.closest('[data-copy-container]'),
        checkbox = parentContainer.find('.checkbox__input');

    if(checkbox.prop('checked')){
        parentContainer.find('[data-input-copy]').val($t.val());
    }
});

$(document).on('change', '[data-copy-container] .checkbox__input', function () {
    let $t = $(this),
        parentContainer = $t.closest('[data-copy-container]'),
        valueInput = '',
        input = parentContainer.find('[data-input-copy]');

    input.each(function () {
        let text = $(this).val();

        if(text.length) {
            if(text.length > valueInput.length){
                valueInput = text;
            }
        }
    });

    input.val(valueInput);
});

// изменение поля данных пользователя
$(function () {
    let refactorInput = '';//храним инпут который рефакторим

    $(document).on('click', '.lk-block-user__refactor', function () {
        $('body').addClass('user-refactor');

        let $t = $(this),
            input = $t.closest('.lk-block-user__row').find('input');
        refactorInput = input;

        input.removeClass('is-disabled').click();
    });

    $(document).on('click', function (e) {

        let parentBox = $(e.target).closest('.lk-block-user');
        if(!parentBox.length ){
            $('.lk-block-user__lock input').addClass('is-disabled');
            $('.password input').attr('type', 'password');
        }
    });


    //показать пароль
    $(document).on('click', '.password__eye', function () {
        let input = $(this).closest('.password').find('input');

        if(input.attr('type') == 'text'){
            input.attr('type', 'password');
        }else{
            input.attr('type', 'text');
        }
    });

    $(document).on('click', '.password-control', function () {
        let input = $(this).closest('.password-group').find('.form-group__input');

        if (input.attr('type') == 'text') {
            $(this).removeClass('view');
            input.attr('type', 'password');
        } else {
            $(this).addClass('view');
            input.attr('type', 'text');
        }
    });

    //прикрепить файл

    $(document).ready(function () {
        $('input[type="file"]').on('change', function () {
            var file = this.files[0];
            if (file) {
                var fileSizeMB = file.size / (1024 * 1024);
                if (fileSizeMB > 10) {
                    alert('Размер файла "' + file.name + '" превышает 10 МБ. Пожалуйста, выберите другой файл.');
                    $(this).val('');
                }
            }
        });
    });
    $(document).on('change','[type="file"]', function () {
        console.log($(this).select())
    })
});
