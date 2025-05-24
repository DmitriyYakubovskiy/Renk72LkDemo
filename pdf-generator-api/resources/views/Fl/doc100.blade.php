<!DOCTYPE html>
<html lang="ru">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <style>
        header, .form-bid-modal, .footer, .modal {
            display: none;
        }

      * {
        font-family: "DejaVu Sans";
        font-size: 11.5px;
        line-height: 1;
      }

      p.text1 {
        margin-top:8px;
        margin-bottom:0pt;
        text-align: justify;
        border-bottom:0.55pt solid #000000;

      }

      p.text1::before, .abzats::before {
        content: " ";
        padding-right: 30px;
        line-height:1.1;
        border-bottom: 2pt solid white;
      }

      .text1 span, .text span  {
        line-height:1.1;
        border-bottom: 2pt solid white;
        font-size: 11.5px;
      }

      p.text {
        margin: 2pt 0pt 0pt;
        border-bottom:0.55pt solid #000000;
        text-align: justify;
      }

      span.textinput {
    /* font-weight: bold;*/
        border-bottom:0.75pt solidrgba(255, 255, 255, 0);
    }

    span.textinputalone {
    /* font-weight: bold;*/
        border-bottom:0.75pt solid black;
    }

      .raz {
        margin: 2pt 0pt 0pt;
        text-align:center;
        font-size:9.5px;
      }

      .snoska {
         font-size:8px !important;
         vertical-align:super;
         color:#000000;
         border:none;
      }

      p.pole{
        margin-top:13px;
        margin-bottom:0pt;
        font-size:10px;
        border-bottom:0.55pt solid #000000;
      }

      .noborder {
          border: none !important;;
      }
      </style>
</head>
<body>
    <div class="" style="max-width: 670px;">
        <p style="margin-top:1pt; margin-bottom:3pt; text-align:center; font-size:12pt;"><span style="font-weight:bold; letter-spacing:3pt;">ЗАЯВКA</span></p>
          <p style="margin-top:3pt; margin-bottom:4pt; text-align:center; font-size:12pt;"><span style="font-weight:bold;">юридического лица (индивидуального предпринимателя),</span><br><span style="font-weight:bold;">физического лица на присоединение по одному источнику</span><br><span style="font-weight:bold;">электроснабжения энергопринимающих устройств с максимальной</span><br><span style="font-weight:bold;">мощностью до 150 кВт включительно</span></p>
          <p class="text1"><span>1. </span><span class="textinput">{{$bid1->surname ?? ''}} {{$bid1->name ?? ''}} {{ $bid1->patronymic?? '' }}</span></p>
          <p class="raz">(полное наименование заявителя – юридического лица;</p>
          <p class="pole"></p>
          <p class="raz">фамилия, имя, отчество заявителя – индивидуального предпринимателя)</p>
          <p class="text1"><span>2. Номер записи в Едином государственном реестре юридических лиц (номер записи в Едином государственном реестре индивидуальных
              предпринимателей) и дата ее внесения в реестр<span class="snoska">[1]</span></span><span class="textinputalone"> {{html_entity_decode($bid1->number_ergyl ?? '&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;') }}, {{html_entity_decode($bid1->ergyl_date ?? '&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;')}}</span></p>
          <p class="raz">&#xa0;</p>
          <p class="pole"></p>
          <p class="text1"><span>3. Место нахождения заявителя, в том числе фактический адрес </span></p>

          <p class="text"><span class="textinput">{{html_entity_decode($bid3->region ?? '&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;') }}, {{html_entity_decode($bid3->district ?? '&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;') }}, {{html_entity_decode($bid3->address_of_object  ?? '&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;') }}</p>
          <p class="raz">(индекс, адрес)</p>
          <p class="text"><span>Паспортные данные<span class="snoska">[2]</span>: серия </span><span class="textinput">{{html_entity_decode($bid1->passport_series ?? '&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;') }}</span><span> номер </span><span class="textinput">{{html_entity_decode($bid1->passport_number ?? '&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;') }}</span></p>
          <p class="text"><span>выдан (кем, когда) </span><span class="textinput">{{html_entity_decode($bid1->passport_issued_by ?? '&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;') }} {{html_entity_decode($bid1->passport_date ?? '&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;') }}</span></p>
          <p class="text1"><span>3(1). Страховой  номер  индивидуального лицевого счета заявителя  (для физических лиц) </span><span class="textinputalone">{{html_entity_decode($bid1->snils ?? '&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;') }}</span></p>
          <p class="text1"><span>4. В связи с </span><span class="textinput">{{html_entity_decode($bid3->reason_for_bid ?? '&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;') }}</span></p>
          <p class="raz">&#xa0;</p>
          <p class="pole"></p>
          <p class="raz">(увеличение объема максимальной мощности, новое строительство, изменение категории надежности электроснабжения и др. – указать нужное)</p>
          <p class="text"><span>просит осуществить технологическое присоединение  </span><span class="textinput">{{html_entity_decode($bid3->object_name ?? '&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;') }},</span></p>

          <p class="text"><span class="textinput">{{html_entity_decode($bid3->power_device ?? '&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;') }}, {{html_entity_decode($bid3->connection_type ?? '&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;') }}, {{html_entity_decode($bid3->voltage_class ?? '&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;') }}, </span></p>
          <p class="raz">(наименование энергопринимающих устройств для присоединения)</p>
          <p class="text"><span>расположенных </span><span class="textinput">{{html_entity_decode($bid3->region ?? '&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;') }}, {{html_entity_decode($bid3->district ?? '&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;') }}, {{html_entity_decode($bid3->address_of_object  ?? '&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;') }},{{$bid3->cadastral_number  ?? '' }}.</span></p>
          <p class="raz">(место нахождения энергопринимающих устройств)</p>
          <p class="text1 noborder"><span>5. Максимальная мощность </span><span class="snoska">[3]</span><span> энергопринимающих устройств (присоединяемых и ранее присоединенных) составляет </span>
            <span class="textinputalone"> {{html_entity_decode($points[0]['power'] ?? '&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;')}} </span><span> кВт, при напряжении</span><span class="snoska">[4]</span>
            <span class="textinputalone"> {{html_entity_decode($points[0]['voltage'] ?? '&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;  ') }} </span><span> кВ, в том числе:</span></p>
        <p class="text abzats noborder"><span>а) максимальная мощность присоединяемых энергопринимающих устройств составляет</span>
            <span class="textinputalone"> {{html_entity_decode($points[0]['power'] ?? '&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;')}}</span><span> кВт при напряжении </span><span class="snoska">[4]</span>
            <span class="textinputalone"> {{html_entity_decode($points[0]['voltage'] ?? '&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;')}}</span><span> кВ;</span></p>
        <p class="text abzats noborder"><span>б) максимальная мощность ранее присоединенных в данной точке присоединения энергопринимающих устройств составляет</span>
        @if ($bid3->reason_for_bid != 'Новое технологичекое присоединение впервые вводимое в эксплуатацию энергопринимающего устройства')    
            <span class="textinputalone"> {{html_entity_decode($bid4->old_point_power ?? '&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;')}}</span><span> кВт при напряжении </span><span class="snoska">[4]</span>
            <span class="textinputalone"> {{html_entity_decode($bid4->old_point_volt ?? '&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;')}}</span><span> кВ.</span></p>
        @else
            <span class="textinput"> {{html_entity_decode('&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;')}}</span><span> кВт при напряжении </span><span class="snoska">[4]</span>
            <span class="textinput"> {{html_entity_decode('&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;')}}</span><span> кВ.</span></p>
        @endif
        <p class="text1 noborder"><span>6. Заявляемая категория надежности энергопринимающих устройств: </span><span class="textinputalone">{{html_entity_decode($bid4->reliability_category  ?? '&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;')}}</span></p>


        <p class="text1"><span>7. Характер нагрузки (вид экономической деятельности заявителя) </span><span class="textinput">{{html_entity_decode($bid4->nature_load ?? '&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;')}}</span></p>
        <p class="text1 noborder">8. Сроки проектирования и поэтапного введения в эксплуатацию объекта (в том числе по этапам и очередям), планируемое поэтапное распределение максимальной мощности:</p>
                <table cellspacing="0" cellpadding="0" style="border-collapse:collapse">
            <tr>
                <td style="width:90.75pt; border-style:solid; border-width:0.75pt; padding-right:1.02pt; padding-left:1.02pt; vertical-align:middle">
                    <p style="margin-top:0pt; margin-bottom:0pt; text-align:center; font-size:9.5pt">
                        <span>Этап</span><br /><span>(очередь) строительства</span>
                    </p>
                </td>
                <td style="width:106.35pt; border-style:solid; border-width:0.75pt; padding-right:1.02pt; padding-left:1.02pt; vertical-align:middle">
                    <p style="margin-top:0pt; margin-bottom:0pt; text-align:center; font-size:9.5pt">
                        <span>Планируемый срок проектирования</span><br /><span>(месяц, год)</span>
                    </p>
                </td>
                <td style="width:106.35pt; border-style:solid; border-width:0.75pt; padding-right:1.02pt; padding-left:1.02pt; vertical-align:middle">
                    <p style="margin-top:0pt; margin-bottom:0pt; text-align:center; font-size:9.5pt">
                        <span>Планируемый срок введения в эксплуатацию</span><br /><span>(месяц, год)</span>
                    </p>
                </td>
                <td style="width:90.75pt; border-style:solid; border-width:0.75pt; padding-right:1.02pt; padding-left:1.02pt; vertical-align:middle">
                    <p style="margin-top:0pt; margin-bottom:0pt; text-align:center; font-size:9.5pt">
                        <span>Максимальная мощность</span><br /><span>(кВт)</span>
                    </p>
                </td>
                <td style="width:90.75pt; border-style:solid; border-width:0.75pt; padding-right:1.02pt; padding-left:1.02pt; vertical-align:middle">
                    <p style="margin-top:0pt; margin-bottom:0pt; text-align:center; font-size:9.5pt">
                        <span>Категория надежности</span>
                    </p>
                </td>
            </tr>

            @foreach($stages as $index => $stage)
            <tr>
                <td style="width:90.75pt; border-style:solid; border-width:0.75pt; padding-right:1.02pt; padding-left:1.02pt; vertical-align:top">
                    <p style="margin-top:0pt; margin-bottom:0pt; text-align:center; font-size:9.5pt">
                        <span>{{ $index + 1 }}</span>
                    </p>
                </td>
                <td style="width:106.35pt; border-style:solid; border-width:0.75pt; padding-right:1.02pt; padding-left:1.02pt; vertical-align:top">
                    <p style="margin-top:0pt; margin-bottom:0pt; text-align:center; font-size:9.5pt">
                        <span>
                            @if(!empty($stage['designPeriod']))
                                {{ html_entity_decode(\Carbon\Carbon::parse($stage['designPeriod'])->format('d.m.Y')) }}
                            @else
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            @endif
                        </span>
                    </p>
                </td>
                <td style="width:106.35pt; border-style:solid; border-width:0.75pt; padding-right:1.02pt; padding-left:1.02pt; vertical-align:top">
                    <p style="margin-top:0pt; margin-bottom:0pt; text-align:center; font-size:9.5pt">
                        <span>
                            @if(!empty($stage['commissioningPeriod']))
                                {{ html_entity_decode(\Carbon\Carbon::parse($stage['commissioningPeriod'])->format('d.m.Y')) }}
                            @else
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            @endif
                        </span>
                    </p>
                </td>
                <td style="width:90.75pt; border-style:solid; border-width:0.75pt; padding-right:1.02pt; padding-left:1.02pt; vertical-align:top">
                    <p style="margin-top:0pt; margin-bottom:0pt; text-align:center; font-size:9.5pt">
                        <span>{{ html_entity_decode($stage['power'] ?? '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;') }}</span>
                    </p>
                </td>
                <td style="width:90.75pt; border-style:solid; border-width:0.75pt; padding-right:1.02pt; padding-left:1.02pt; vertical-align:top">
                    <p style="margin-top:0pt; margin-bottom:0pt; text-align:center; font-size:9.5pt">
                        @if($index === 0)
                            {{ html_entity_decode($bid4->reliability_category ?? '&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;') }}
                        @else
                            &#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;
                        @endif
                    </p>
                </td>
            </tr>
            @endforeach
        </table>
          <!-- 9. Порядок расчета и условия рассрочки внесения платы за технологическое присоединение по договору осуществляются по -->
          <p class="text1 "><span>9. Намерение воспользоваться рассрочкой платежа за технологическое присоединение<span class="snoska">[5]</span>:</span>
          <span class="textinput">
            @if($bid4->payment_order === 'Вариант 1')
            <!-- {{$bid4->payment_order ?? '&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;' }} -->
                Не требуется
            @elseif($bid4->payment_order === 'Вариант 2')
                Прошу предоставить рассрочку платежа за технологическое присоединение
            @endif
            </span></p>


            @if($bid4->payment_order === 'Вариант 1')
                <p class="text noborder">«при котором:
                    <br>-15 процентов платы за технологическое присоединение вносятся в течение 5 рабочих дней со дня размещения в личном кабинете заявителя счета;
                    <br>-30 процентов платы за технологическое присоединение вносятся в течение 20 дней со дня размещения в личном кабинете заявителя счета;
                    <br>-35 процентов платы за технологическое присоединение вносятся в течение 40 дней со дня размещения в личном кабинете заявителя счета;
                    <br>-20 процентов платы за технологическое присоединение вносятся в течение 10 дней со дня размещения в личном кабинете заявителя акта об осуществлении технологического присоединения или уведомления об обеспечении сетевой организацией возможности присоединения к электрическим сетям.»</p>
            @elseif($bid4->payment_order === 'Вариант 2')
                <p class="text noborder">«при котором:
                    <br>-авансовый платеж вносится в размере 5 процентов размера платы за технологическое присоединение;
                    <br>-осуществляется рассрочка платежа в размере 95 процентов платы за технологическое присоединение с условием ежеквартального внесения платы равными долями от общей суммы рассрочки на период до 3 лет со дня подписания сторонами акта об осуществлении технологического присоединения;
                    <br>-за предоставление рассрочки платежа за технологическое присоединение сетевой организации заявителем выплачиваются проценты. Проценты начисляются на остаток задолженности заявителя и подлежат оплате одновременно с очередным платежом, которым погашается частично или полностью такая задолженность. Размер процентов (в процентах годовых) за каждый день рассрочки определяется в размере действовавшей на указанный день ключевой ставки Центрального банка Российской Федерации, увеличенной на 4 процентных пункта»
                </p>
            @endif
          <p class="text1"><span>10. Гарантирующий поставщик (энергосбытовая организация), с которым планируется заключение договора энергоснабжения (купли-продажи электрической энергии (мощности) </span><span class="textinput">{{html_entity_decode($bid3->guarant_supplier ?? '&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;')}}, {{html_entity_decode($bid3->type_of_contract ?? '&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;')}}.</span></p>
          <p class="pole noborder"></p>
          <p style="margin-top:0pt; margin-bottom:0pt; text-indent:28.35pt; font-size:11pt"><span style="">&#xa0;</span></p>
          <p style="margin-top:0pt; margin-bottom:0pt; text-indent:28.35pt; font-size:11pt"><span style="">Приложения:</span></p>
          <p style="margin-top:0pt; margin-bottom:0pt; text-indent:28.35pt; font-size:9pt"><span style="">(указать перечень прилагаемых документов)</span></p>
          <ol type="1">
          {!! $bid5->filename_1 != NULL ? '<li><p class="text" ><span></span><span class="textinput"> Документ удостоверяющий личность</span></p></li>' : '' !!}
          {!! $bid5->filename_6 != NULL ? '<li><p class="text" ><span></span><span class="textinput"> СНИЛС</span></p></li>' : '' !!}
          {!! $bid5->filename_2 != NULL ? '<li><p class="text" ><span></span><span class="textinput"> План расположения энергопринимающих устройств</span></p></li>' : NULL !!}
          {!! $bid5->filename_3 !=NULL ? '<li><p class="text" ><span></span><span class="textinput"> Копия документа, подтверждающего право собственности</span></p></li>' : NULL !!}
          {!! $bid5->filename_7 !=NULL ? '<li><p class="text" ><span></span><span class="textinput"> Документ, подтверждающий право на льготу</span></p></li>' : NULL !!}
          <li><p class="text" ><span></span><span class="textinput">&#xa0;</span></p></li>
          </ol>

          <p style="margin-top:8pt; margin-right:120pt; margin-bottom:0pt; page-break-after:avoid; font-size:11pt"><span style="">Заявитель</span></p>
          <table cellspacing="0" cellpadding="0" style="border-collapse:collapse">
              <tr>
                  <td colspan="3" style="width:275.75pt; border-bottom-style:solid; border-bottom-width:0.55pt; padding-right:1.4pt; padding-left:1.4pt; vertical-align:bottom">
                      <p style="margin-top:0pt; margin-bottom:0pt; text-align:center; page-break-after:avoid; font-size:12pt"><span style="">{{html_entity_decode($bid1->surname ?? '&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;')}} {{html_entity_decode($bid1->name ?? '&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;')}} {{html_entity_decode($bid1->patronymic ?? '&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;')}}</span></p>
                  </td>
              </tr>
              <tr>
                  <td colspan="3" style="width:275.75pt; border-top-style:solid; border-top-width:0.55pt; padding-right:1.4pt; padding-left:1.4pt; vertical-align:top">
                      <p style="margin-top:0pt; margin-bottom:0pt; text-align:center; page-break-after:avoid; font-size:9pt"><span style="">(фамилия, имя, отчество)</span></p>
                  </td>
              </tr>
              <tr>
                  <td colspan="3" style="width:275.75pt; border-bottom-style:solid; border-bottom-width:0.55pt; padding-right:1.4pt; padding-left:1.4pt; vertical-align:bottom">
                      <p style="margin-top:0pt; margin-bottom:0pt; text-align:center; page-break-after:avoid; font-size:11pt"><span style="">{{html_entity_decode($bid1->phone ?? '&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;')}}</span></p>
                  </td>
              </tr>
              <tr>
                  <td colspan="3" style="width:275.75pt; border-top-style:solid; border-top-width:0.55pt; padding-right:1.4pt; padding-left:1.4pt; vertical-align:top">
                      <p style="margin-top:0pt; margin-bottom:0pt; text-align:center; page-break-after:avoid; font-size:9pt"><span style="">(выделенный оператором подвижной радиотелефонной связи абонентский номер и адрес электронной почты заявителя)</span></p>
                  </td>
              </tr>
              <tr>
                  <td style="width:161.65pt; border-bottom-style:solid; border-bottom-width:0.55pt; padding-right:1.4pt; padding-left:1.4pt; vertical-align:bottom">
                      <p style="margin-top:0pt; margin-bottom:0pt; text-align:center; page-break-after:avoid; font-size:11pt"><span style="">{{html_entity_decode($bid1->position ?? '&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;')}}, {{html_entity_decode($bid1->act ?? '&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;')}}</span></p>
                  </td>
                  <td style="width:4.25pt; padding-right:1.4pt; padding-left:1.4pt; vertical-align:bottom">
                      <p style="margin-top:0pt; margin-bottom:0pt; page-break-after:avoid; font-size:12pt"><span style="">&#xa0;</span></p>
                  </td>
                  <td style="width:82.25pt; border-bottom-style:solid; border-bottom-width:0.55pt; padding-right:1.4pt; padding-left:1.4pt; vertical-align:bottom">
                      <p style="margin-top:0pt; margin-bottom:0pt; text-align:center; page-break-after:avoid; font-size:11pt"><span style="">&#xa0;</span></p>
                  </td>
              </tr>
              <tr>
                  <td style="width:161.65pt; border-top-style:solid; border-top-width:0.55pt; padding-right:1.4pt; padding-left:1.4pt; vertical-align:top">
                      <p style="margin-top:0pt; margin-bottom:0pt; text-align:center; page-break-after:avoid; font-size:9pt"><span style="">(должность)</span></p>
                  </td>
                  <td style="width:4.25pt; padding-right:1.4pt; padding-left:1.4pt; vertical-align:top">
                      <p style="margin-top:0pt; margin-bottom:0pt; page-break-after:avoid; font-size:9pt"><span style="">&#xa0;</span></p>
                  </td>
                  <td style="width:82.25pt; border-top-style:solid; border-top-width:0.55pt; padding-right:1.4pt; padding-left:1.4pt; vertical-align:top">
                      <p style="margin-top:0pt; margin-bottom:0pt; text-align:center; page-break-after:avoid; font-size:9pt"><span style="">(подпись)</span></p>
                  </td>
              </tr>
          </table>
          <p style="margin-top:0pt; margin-bottom:0pt; font-size:1pt"><span style="">&#xa0;</span></p>
          <table cellspacing="0" cellpadding="0" style="border-collapse:collapse">
              <tr>
                  <td style="width:7.1pt; padding-right:1.4pt; padding-left:1.4pt; vertical-align:bottom">
                      <p style="margin-top:0pt; margin-bottom:0pt; text-align:right; font-size:11pt"><span style="">“</span></p>
                  </td>
                  <td style="width:19.9pt; border-bottom-style:solid; border-bottom-width:0.55pt; padding-right:1.4pt; padding-left:1.4pt; vertical-align:bottom">
                      <p style="margin-top:0pt; margin-bottom:0pt; text-align:center; font-size:11pt"><span style="">{{html_entity_decode(date('d', strtotime($user_bid->created_at)) ?? '&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;')}}</span></p>
                  </td>
                  <td style="width:9.95pt; padding-right:1.4pt; padding-left:1.4pt; vertical-align:bottom">
                      <p style="margin-top:0pt; margin-bottom:0pt; font-size:11pt"><span style="">”</span></p>
                  </td>
                  <td style="width:70.9pt; border-bottom-style:solid; border-bottom-width:0.55pt; padding-right:1.4pt; padding-left:1.4pt; vertical-align:bottom">
                      <?php
                          $trans = array("January" => "Января",
                                          "February" => "Февраля",
                                          "March" => "Марта",
                                          "April" => "Апреля",
                                          "May" => "Мая",
                                          "June" => "Июня",
                                          "July" => "Июля",
                                          "August" => "Августа",
                                          "September" => "Сентября",
                                          "October" => "Октября",
                                          "November" => "Ноября",
                                          "December" => "Декабря");

                          $result = strtr(date('F', strtotime($user_bid->created_at)), $trans);
                      ?>

                      <p style="margin-top:0pt; margin-bottom:0pt; text-align:center; font-size:11pt"><span style="">{{html_entity_decode($result ?? '&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;')}}</span></p>
                  </td>
                  <td style="width:15.65pt; padding-right:1.4pt; padding-left:1.4pt; vertical-align:bottom">
                      <p style="margin-top:0pt; margin-bottom:0pt; text-align:right; font-size:11pt"><span style=""></span></p>
                  </td>
                  <td style="width:15.65pt; border-bottom-style:solid; border-bottom-width:0.55pt; padding-right:1.4pt; padding-left:1.4pt; vertical-align:bottom">
                      <p style="margin-top:0pt; margin-bottom:0pt; font-size:11pt"><span style="">{{html_entity_decode(date('Y', strtotime($user_bid->created_at)) ?? '&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;')}}</span></p>
                  </td>
                  <td style="width:17.05pt; padding-right:1.4pt; padding-left:1.4pt; vertical-align:bottom">
                      <p style="margin-top:0pt; margin-left:2.85pt; margin-bottom:0pt; font-size:11pt"><span style="">г.</span></p>
                  </td>
              </tr>
          </table>
          <p style="margin-top:8px; font-size:11pt"><span style="">М.П.</span></p>
      </div>
      <div style="margin-top:8px; display:block"></div>
      <hr style="width:33%; height:1px; text-align:left; -aw-footnote-numberstyle:2; -aw-footnote-startnumber:1; -aw-footnote-type:1" />
      <div id="_edn1" style="-aw-footnote-isauto:0">
          <p style="margin-top:0pt; margin-bottom:0pt; text-indent:28.35pt; text-align:justify; font-size:9pt"><a href="#_ednref1" style="text-decoration:none"><span style="font-size:8pt; vertical-align:super; color:#000000;">[1]</span></a><span style="">&#xa0;</span><span style=""> Для юридических лиц и индивидуальных предпринимателей.</span></p>
      </div>
      <div id="_edn2" style="-aw-footnote-isauto:0">
          <p style="margin-top:0pt; margin-bottom:0pt; text-indent:28.35pt; text-align:justify; font-size:9pt"><a href="#_ednref2" style="text-decoration:none"><span style="font-size:8pt; vertical-align:super; color:#000000;">[2]</span></a><span style="">&#xa0;</span><span style="">Для физических лиц.</span></p>
      </div>
      <div id="_edn3" style="-aw-footnote-isauto:0">
          <p style="margin-top:0pt; margin-bottom:0pt; text-indent:28.35pt; text-align:justify; font-size:9pt"><a href="#_ednref4" style="text-decoration:none"><span style="font-size:8pt; vertical-align:super; color:#000000;">[3]</span></a><span style="">&#xa0;</span><span style="">Максимальная мощность указывается равной максимальной мощности присоединяемых энергопринимающих устройств в случае отсутствия максимальной мощности ранее присоединенных энергопринимающих устройств (то есть в пункте 5 и подпункте “а” пункта 5 настоящего приложения величина мощности указывается одинаковая).</span></p>
      </div>
      <div id="_edn5" style="-aw-footnote-isauto:0">
          <p style="margin-top:0pt; margin-bottom:0pt; text-indent:28.35pt; text-align:justify; font-size:9pt"><a href="#_ednref5" style="text-decoration:none"><span style="font-size:8pt; vertical-align:super; color:#000000;">[4]</span></a><span style="">&#xa0;</span><span style="">Классы напряжения (0,4; 6; 10) кВ.</span></p>
      </div>
      <div id="_edn6" style="-aw-footnote-isauto:0">
          <p style="margin-top:0pt; margin-bottom:0pt; text-indent:28.35pt; text-align:justify; font-size:9pt"><a href="#_ednref6" style="text-decoration:none"><span style="font-size:8pt; vertical-align:super; color:#000000;">[5]</span></a><span style="">&#xa0;</span><span style="">Заполняется заявителем, максимальная мощность энергопринимающих устройств которого составляет до 150 кВт включительно (с учетом ранее присоединенной в данной точке присоединения мощности).</span></p>
      </div>
</body>
</html>


