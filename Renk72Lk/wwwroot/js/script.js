$(function () {

    $.fn.setCursorPosition = function(pos) {
        if ($(this).get(0).setSelectionRange) {
            $(this).get(0).setSelectionRange(pos, pos);
        } else if ($(this).get(0).createTextRange) {
            var range = $(this).get(0).createTextRange();
            range.collapse(true);
            range.moveEnd('character', pos);
            range.moveStart('character', pos);
            range.select();
        }
    };


    $('[data-masked-phone]').click(function(){
        if($(this).val() === '+7 (___) ___ __ __'){
            $(this).setCursorPosition(4);  // set position number
        }
    });
    $('[data-masked-phone]').mask('+7 (999) 999 99 99');


    $('[data-masked-snils]').click(function(){
        if($(this).val() === '___-___-___ __'){
            $(this).setCursorPosition(0);  // set position number
        }
    });
    $('[data-masked-snils]').mask('999-999-999 99');

    $('[data-masked-date_of_birth]').click(function(){
      if($(this).val() === '___-___-___ __'){
          $(this).setCursorPosition(0);  // set position number
      }
  });
  $('[data-masked-date_of_birth').mask('99.99.9999');


    $('[data-masked-kadr]').click(function(){
        // alert($(this).val());
        if($(this).val() === '__:__:_______:______'){
            $(this).setCursorPosition(0);  // set position number
        }
    });
    $('[data-masked-kadr]').mask('99:99:9999999:9?99999');


    $('[data-masked-series]').click(function(){
        if($(this).val() === '___ ___'){
            $(this).setCursorPosition(0);  // set position number
        }
    });
    $('[data-masked-pseries]').mask('99 99');

    $('[data-masked-pnumber]').click(function(){
        if($(this).val() === '____ ____'){
            $(this).setCursorPosition(0);  // set position number
        }
    });
    $('[data-masked-pnumber]').mask('999 999');

    
    // $.mask.definitions['.']='[.,/]';

    $(".datepicker-here").mask("99.99.9999",{"placeholder": "_"});

    let data_input = 'туть храним инпут с датой на который нажали';

    $(document).on('click', '.datepicker-here', function () {
        data_input = $(this);
    });
    // let u = {f:'xzVb}55MN#wm03p$58vP6O03',u:'xvIIfF|aT}ALF@VYfBCV31{Q',p:'mkbKDE9R2d$%j4$TX7Li{uwE'};

    $(document).on('click', '.datepicker--cell',  function (e) {
        e.preventDefault();
        let data = data_input.val();

        setTimeout(function () {
            data_input.val(data);
        },10)
    });


    $(document).on('click', '.file_btn' , function () {
        $(this).find('input').click();
    });

    $(document).on('change', '.form-group__input', function() {
        let attrVal = $(this).attr('name'),
            val = $(this).val();
        $('input[name="'+attrVal+'"]').val(val);
    });

    // $(document).on('click', '.btn-form', function () {
    //     let val = $('.form-group__hidden').val();
    //     $('.form-group__role').val(val);
    // });

    $('input').on('change', function () {
        $(this).removeClass('is-active');
        $(this).siblings('.form-group__error').remove();
    });

    $(document).on('click', '.select-block__title' , function () {
        $(this).removeClass('is-active');
        $(this).siblings('.form-group__error').remove();
    });

    setTimeout(() => {
        $('.alert').slideUp('400', function(){
            $(this).remove();
        });
    }, 3000);
});

//
// Dadate
//

function join(arr /*, separator */) {
  var separator = arguments.length > 1 ? arguments[1] : ", ";
  return arr.filter(function(n){return n}).join(separator);
}

function geoQuality(qc_geo) {
  var localization = {
    "0": "точные",
    "1": "ближайший дом",
    "2": "улица",
    "3": "населенный пункт",
    "4": "город"
  };
  return localization[qc_geo] || qc_geo;
}

function geoLink(address) {
  return join(["<a target=\"_blank\" href=\"", 
               "https://maps.yandex.ru/?text=", 
               address.geo_lat, ",", address.geo_lon, "\">", 
               address.geo_lat, ", ", address.geo_lon, "</a>"], "");
}

function showPostalCode(address,id) {
  $(id+"_index").val(address.postal_code);
}

function showRegion(address,id) {
  $(id+"_obl").val(join([
    join([address.region_type, address.region], " "),
    join([address.area_type, address.area], " ")
  ]));
}

function showCity(address,id) {
  $(id+"_city").val(join([
    join([address.city_type, address.city], " "),
    join([address.settlement_type, address.settlement], " ")
  ]));
}

function showStreet(address,id) {
  $(id+"_str").val(
    join([address.street_type, address.street], " ")
  );
}

function showHouse(address,id) {
  $(id+"_house").val(join([
    join([address.house_type, address.house], " "),
    join([address.block_type, address.block], " ")
  ]));
}

function showFlat(address,id) {
  $(id+"_office").val(
    join([address.flat_type, address.flat], " ")
  );
}

function showGeo(address,id) {
  if (address.qc_geo != "5") {
    var geo = geoLink(address) + " (" + geoQuality(address.qc_geo) + ")";
    $("#geo").html(geo);
  }
}

function showFull(elname) {
  document.getElementById(elname).style.display = "block";
 
}

function dadate(id) {
  $("#"+id).suggestions({
  token: tokendadata,
  type: "ADDRESS",
  onSelect: function showSelected(suggestion) {
  elname = id +'_full';
  id = '#adr'+ id.split('_')[1].substr(0, 3);
  address = suggestion.data;
  showPostalCode(address,id);
  showRegion(address,id);
  showCity(address,id);
  showStreet(address,id);
  showHouse(address,id);
  showFlat(address,id); 
  showGeo(address,id);
  showFull(elname);
}
});
}
// 
// Ручной ввод адреса 
// 
function dadateManual(address,id) {
  address = address.split('_')[0];
  $('#'+id).val(join([
    $('#'+address+'_obl').val(),
    $('#'+address+'_city').val(),
    $('#'+address+'_str').val(),
    $('#'+address+'_house').val(),
    $('#'+address+'_office').val(),
    $('#'+address+'_index').val()
  ]));
  

}

