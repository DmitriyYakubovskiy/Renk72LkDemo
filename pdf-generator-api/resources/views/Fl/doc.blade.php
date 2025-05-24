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
        font-size: 12.5px;
        line-height: 1;
    }

    p.text1 {
        line-height: 1;
        margin-top:12.8px;
        margin-bottom:0pt;
        text-align: justify;
        border-bottom:0.75pt solid #000000;
    }

    p.text1::before, .abzats::before {
        line-height:1.1;
        content: " ";
        padding-right: 30px;
        border-bottom: 2pt solid white;
    }
    .text1 span, .text span  {
        line-height:1.1;
        border-bottom: 2pt solid white;
    /* font-size: 13px; */
    }

    p.text {
    margin: 2pt 0pt 0pt;
    border-bottom:0.75pt solid black;
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
        font-size:10.5px;
    }

    .snoska {
        font-size:8.5px !important;
        vertical-align:super;
        color:#000000;
        border:none;
    }

    p.pole{
        margin-top:20px;
        margin-bottom:0pt;
        font-size:12.5px;
        border-bottom:0.75pt solid black;
    }

    .noborder {
        border: none !important;;
    }
    </style>
</head>
<body>
    <div class="" style="max-width: 670px; margin: auto">
        <p style="margin-top:0pt; margin-bottom:24pt; font-size:9pt"><span style="">&#xa0;</span></p>
        <p style="margin-top:24pt; margin-bottom:6pt; text-align:center; font-size:13pt"><span style=" font-weight:bold; letter-spacing:3pt">ЗАЯВКА</span><span style=" font-weight:bold"></span><span style=" font-weight:bold">&#xa0;</span><a name="_ednref1"></a><a href="#_edn1" style="text-decoration:none"><span style=" font-size:8.67pt; font-weight:bold; vertical-align:super; color:#000000">[1]</span></a></p>
        <p style="margin-top:6pt; margin-bottom:24pt; text-align:center; font-size:13pt"><span style=" font-weight:bold">физического лица на присоединение по одному источнику</span><br /><span style=" font-weight:bold">электроснабжения энергопринимающих устройств с максимальной</span><br /><span style=" font-weight:bold">мощностью до 15 кВт включительно (используемых для бытовых</span><br /><span style=" font-weight:bold">и иных нужд, не связанных с осуществлением</span><br /><span style=" font-weight:bold">предпринимательской деятельности)</span></p>
        <p class="text1"><span>1. </span><span class="textinput"> {{$bid1->surname ?? '' }} {{$bid1->name ?? '' }} {{$bid1->patronymic ?? '' }}</span></p>
        <p class='raz'>(фамилия, имя, отчество)</p>
        <p class="text1"><span>2. Паспортные данные: серия</span><span class="textinput"> {{html_entity_decode($bid1->passport_series ?? '&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;') }} </span>
            <span>номер</span><span class="textinput"> {{html_entity_decode($bid1->passport_number ?? '&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;') }}</span></p>
        <p class="text"><span>выдан (кем, когда) </span><span class="textinput"> {{$bid1->passport_issued_by ?? '' }} {{$bid1->passport_date ?? '' }}</span><span> СНИЛС: </span><span class="textinput"> {{$bid1->snils ?? '' }}</span></p>
        <p class="text"><span>Дата рождения </span><span class="textinput"> {{$bid1->date_of_birth ?? '' }}</p>
        <p class="text"><span>Место рождения </span><span class="textinput"> {{$bid1->place_of_birth ?? '' }}</p>
        <p class="text noborder"><span>Даю согласие на обработку персональных данных в соответствии с требованиями Федерального закона «О персональных данных» </span></p>
        <p class="text1"><span>3. Зарегистрирован(а) </span><span class="textinput"> {{$bid1->address_reg ?? '' }}</span></p>
        <p class='raz'>(индекс, адрес)</p>
        <p class="text1"><span>4. Фактический адрес проживания </span><span class="textinput"> {{$bid1->address_actual ?? '' }}</span></p>
        <p class="pole" ></p>
        <p class='raz'>(индекс, адрес)</p>
        <p class="text1"><span>5. В связи с </span><span class="textinput"> {{$bid3->reason_for_bid ?? '' }}</span></p>
        <p class='raz'>(увеличение объема максимальной мощности, новое строительство, изменения уровня напряжения)</p>
        <p class="text"><span>просит осуществить технологическое присоединение </span>
            <span class="textinput"> {{$bid3->object_name ?? '' }}, {{$bid3->power_device ?? '' }}, {{$bid3->connection_type ?? '' }}, {{$bid3->voltage_class ?? '' }},</span></p>
        <p class='raz'>(наименование энергопринимающих устройств для присоединения)</p>
        <p class="text"><span>расположенных </span><span class="textinput"> {{$bid3->region ?? '' }}, {{$bid3->district ?? '' }}, {{$bid3->address_of_object  ?? '' }}, {{$bid3->cadastral_number  ?? '' }}.</span></p>
        <p class='raz'>(место нахождения энергопринимающих устройств)</p>
        <p class="text1 noborder"><span>6. Максимальная мощность </span><span class="snoska">[2]</span><span> энергопринимающих устройств (присоединяемых и ранее присоединенных) составляет </span>
            <span class="textinputalone"> {{ html_entity_decode($points[0]['power'] ?? '&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;') }} </span><span> кВт, при напряжении</span><span class="snoska">[3]</span>
            <span class="textinputalone"> {{ html_entity_decode($points[0]['voltage'] ?? '&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;') }} </span><span> кВ, в том числе:</span></p>
        <p class="text abzats noborder"><span>а) максимальная мощность присоединяемых энергопринимающих устройств составляет</span>
            <span class="textinputalone"> {{ html_entity_decode($points[0]['power'] ?? '&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;') }}</span><span> кВт при напряжении </span><span class="snoska">[3]</span>
            <span class="textinputalone"> {{ html_entity_decode($points[0]['voltage'] ?? '&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;') }}</span><span> кВ;</span></p>
        <p class="text abzats noborder"><span>б) максимальная мощность ранее присоединенных в данной точке присоединения энергопринимающих устройств составляет</span>
            @if ($bid3->reason_for_bid != 'Новое технологичекое присоединение впервые вводимое в эксплуатацию энергопринимающего устройства')
                <span class="textinputalone"> {{ html_entity_decode($bid4->old_point_power ?? '&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;') }}  </span><span> кВт при напряжении </span><span class="snoska">[3]</span>
                <span class="textinputalone"> {{ html_entity_decode($bid4->old_point_volt ?? '&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;') }}</span><span> кВ.</span></p>
            @else
                <span class="textinputalone"> {{'&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;'}}</span><span> кВт при напряжении </span><span class="snoska">[3]</span>
                <span class="textinputalone"> {{'&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;'}}</span><span> кВ.</span></p>
            @endif

        <p class="text1 noborder">7. Заявляемая категория энергопринимающего устройства по надежности электроснабжения: {{$bid4->reliability_category ?? '' }} (по одному источнику электроснабжения).</span></p>
        <p class="text1 noborder">8. Сроки проектирования и поэтапного введения в эксплуатацию объекта (в том числе по этапам и очередям):</span></p>
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
        <p class="text1 ">9. Гарантирующий поставщик (энергосбытовая организация), с которым планируется заключение договора электроснабжения (купли-продажи электрической энергии (мощности)</span>
            <span class="textinput"> {{$bid3->guarant_supplier ?? '' }}.</span></p>
        <p style="margin-top:0pt; margin-bottom:0pt; text-indent:28.35pt; font-size:11.5pt"><span style="">&#xa0;</span></p>
        {!! $bid5->filename_7 != NULL ? '': '<p class="text"><span class="textinput">Я не отношусь к льготной категории граждан</span></p>' !!}

        <p style="margin-top:0pt; margin-bottom:0pt; text-indent:28.35pt; font-size:11.5pt"><span style="">&#xa0;</span></p>
        <p style="margin-top:0pt; margin-bottom:0pt; text-indent:28.35pt; font-size:11.5pt"><span style="">Приложения:</span></p>
        <p style="margin-top:0pt; margin-bottom:0pt; text-indent:28.35pt; font-size:9.5pt"><span style="">(указать перечень прилагаемых документов)</span></p>
        <ol type="1">
        {!! $bid5->filename_1 != NULL ? '<li><p class="text" ><span></span><span class="textinput"> Документ удостоверяющий личность</span></p></li>' : '' !!}
        {!! $bid5->filename_6 != NULL ? '<li><p class="text" ><span></span><span class="textinput"> СНИЛС</span></p></li>' : '' !!}
        {!! $bid5->filename_2 != NULL ? '<li><p class="text" ><span></span><span class="textinput"> План расположения энергопринимающих устройств</span></p></li>' : NULL !!}
        {!! $bid5->filename_3 !=NULL ? '<li><p class="text" ><span></span><span class="textinput"> Копия документа, подтверждающего право собственности</span></p></li>' : NULL !!}
        {!! $bid5->filename_7 !=NULL ? '<li><p class="text" ><span></span><span class="textinput"> Документ, подтверждающий право на льготу</span></p></li>' : NULL !!}
        <li><p class="text" ><span></span><span class="textinput">&#xa0;</span></p></li>
        </ol>
        <p style="margin-top:12pt; margin-right:240.9pt; margin-bottom:0pt; font-size:11.5pt"><span style="">Заявитель</span></p>
        <table cellspacing="0" cellpadding="0" style="border-collapse:collapse">
            <tr>
                <td style="width:197.05pt; border-bottom-style:solid; border-bottom-width:0.75pt; padding-right:1.4pt; padding-left:1.4pt; vertical-align:bottom">
                    <p style="margin-top:0pt; margin-bottom:0pt; text-align:center; font-size:11.5pt"><span style="">{{$bid1->surname ?? '' }} {{$bid1->name ?? '' }} {{$bid1->patronymic ?? '' }}</span></p>
                </td>
            </tr>
            <tr>
                <td style="width:197.05pt; border-top-style:solid; border-top-width:0.75pt; padding-right:1.4pt; padding-left:1.4pt; vertical-align:top">
                    <p style="margin-top:0pt; margin-bottom:0pt; text-align:center; font-size:9.5pt"><span style="">(фамилия, имя, отчество)</span></p>
                </td>
            </tr>
            <tr>
                <td style="width:197.05pt; border-bottom-style:solid; border-bottom-width:0.75pt; padding-right:1.4pt; padding-left:1.4pt; vertical-align:bottom">
                    <p style="margin-top:0pt; margin-bottom:0pt; text-align:center; font-size:11.5pt"><a name="_Hlk51678595"><span style="">{{$bid1->phone ?? '' }}</span></a></p>
                </td>
            </tr>
            <tr>
                <td style="width:197.05pt; border-top-style:solid; border-top-width:0.75pt; padding-right:1.4pt; padding-left:1.4pt; vertical-align:top">
                    <p style="margin-top:0pt; margin-bottom:0pt; text-align:center; font-size:9.5pt"><span style="">(</span><span style="">контактный телефон)</span></p>
                    <table cellspacing="0" cellpadding="0" style="border-collapse:collapse">
                        <tr>
                            <td style="width:197.05pt; border-bottom-style:solid; border-bottom-width:0.75pt; padding-right:1.4pt; padding-left:1.4pt; vertical-align:bottom">
                                <p style="margin-top:0pt; margin-bottom:0pt; text-align:center; font-size:11.5pt"><a name="_Hlk51678703"><span style="">{{$bid1->email ?? '' }}</span></a></p>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:197.05pt; border-top-style:solid; border-top-width:0.75pt; padding-right:1.4pt; padding-left:1.4pt; vertical-align:top">
                                <p style="margin-top:0pt; margin-bottom:0pt; text-align:center; font-size:9.5pt"><span style="">(</span><span style="">e-mail</span><span style="">)</span></p>
                            </td>
                        </tr>
                    </table>
                    <p style="margin-top:0pt; margin-bottom:0pt; text-align:center; font-size:9.5pt"><span style="-aw-bookmark-end:_Hlk51678703"></span></p>
                </td>
            </tr>
            <tr>
                <td style="width:197.05pt; border-bottom-style:solid; border-bottom-width:0.75pt; padding-right:1.4pt; padding-left:1.4pt; vertical-align:bottom">
                    <p style="margin-top:0pt; margin-bottom:0pt; text-align:center; font-size:11.5pt"><span style="-aw-bookmark-end:_Hlk51678595"></span><span style="">&#xa0;</span></p>
                </td>
            </tr>
            <tr>
                <td style="width:197.05pt; border-top-style:solid; border-top-width:0.75pt; padding-right:1.4pt; padding-left:1.4pt; vertical-align:top">
                    <p style="margin-top:0pt; margin-bottom:0pt; text-align:center; font-size:9.5pt"><span style="">(подпись)</span></p>
                </td>
            </tr>
        </table>
        <p style="margin-top:0pt; margin-bottom:0pt; font-size:1pt"><span style="">&#xa0;</span></p>
        <table cellspacing="0" cellpadding="0" style="border-collapse:collapse">
            <tr>
                <td style="width:7.1pt; padding-right:1.4pt; padding-left:1.4pt; vertical-align:bottom">
                    <p style="margin-top:0pt; margin-bottom:0pt; text-align:right; font-size:11.5pt"><span style="">“</span></p>
                </td>
                <td style="width:19.9pt; border-bottom-style:solid; border-bottom-width:0.75pt; padding-right:1.4pt; padding-left:1.4pt; vertical-align:bottom">
                    <p style="margin-top:0pt; margin-bottom:0pt; text-align:center; font-size:11.5pt"><span style="">{{date('d', strtotime($user_bid->created_at)) ?? '' }}</span></p>
                </td>
                <td style="width:9.95pt; padding-right:1.4pt; padding-left:1.4pt; vertical-align:bottom">
                    <p style="margin-top:0pt; margin-bottom:0pt; font-size:11.5pt"><span style="">”</span></p>
                </td>
                <td style="width:70.9pt; border-bottom-style:solid; border-bottom-width:0.75pt; padding-right:1.4pt; padding-left:1.4pt; vertical-align:bottom">
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
                    <p style="margin-top:0pt; margin-bottom:0pt; text-align:center; font-size:11.5pt"><span style="">{{$result  ?? '' }}</span></p>
                </td>
                <td style="width:15.65pt; padding-right:1.4pt; padding-left:1.4pt; vertical-align:bottom">
                    <p style="margin-top:0pt; margin-bottom:0pt; text-align:right; font-size:11.5pt"><span style=""></span></p>
                </td>
                <td style="width:15.65pt; border-bottom-style:solid; border-bottom-width:0.75pt; padding-right:1.4pt; padding-left:1.4pt; vertical-align:bottom">
                    <p style="margin-top:0pt; margin-bottom:0pt; font-size:11.5pt"><span style="">{{date('Y', strtotime($user_bid->created_at)) ?? '' }}</span></p>
                </td>
                <td style="width:17.05pt; padding-right:1.4pt; padding-left:1.4pt; vertical-align:bottom">
                    <p style="margin-top:0pt; margin-left:2.85pt; margin-bottom:0pt; font-size:11.5pt"><span style="">г.</span></p>
                </td>
            </tr>
        </table>
        <p style="margin-top:0pt; margin-right:240.9pt; margin-bottom:0pt; font-size:11.5pt"><span style="">&#xa0;</span></p>
        <p style="margin-top:0pt; margin-right:240.9pt; margin-bottom:0pt; font-size:11.5pt"><span style="">&#xa0;</span></p>
    </div>

    <div class="info-doc"  style="max-width: 670px; margin: auto">
        <hr style="width:33%; height:1px; text-align:left; -aw-footnote-numberstyle:2; -aw-footnote-startnumber:1; -aw-footnote-type:1" />
    <div id="_edn1" style="-aw-footnote-isauto:0">
        <p style="margin-top:0pt; margin-bottom:0pt; text-indent:28.35pt; text-align:justify; font-size:10pt"><a href="#_ednref1" style="text-decoration:none"><span style=" font-size:8pt; vertical-align:super; color:#000000">[1]</span></a><span style="">&#xa0;</span><span style="">Максимальная мощность не превышает 15 кВт с учетом максимальной мощности ранее присоединенных в данной точке присоединения энергопринимающих устройств.</span></p>
    </div>
    <div id="_edn2" style="-aw-footnote-isauto:0">
        <p style="margin-top:0pt; margin-bottom:0pt; text-indent:28.35pt; text-align:justify; font-size:10pt"><a href="#_ednref2" style="text-decoration:none"><span style=" font-size:8pt; vertical-align:super; color:#000000">[2]</span></a><span style="">&#xa0;</span><span style="">Максимальная мощность указывается равной максимальной мощности присоединяемых энергопринимающих устройств в случае отсутствия максимальной мощности ранее присоединенных энергопринимающих устройств (то есть в пункте 6 и подпункте “а” пункта 6 настоящего приложения величина мощности указывается одинаковая).</span></p>
    </div>
    <div id="_edn3" style="-aw-footnote-isauto:0">
        <p style="margin-top:0pt; margin-bottom:0pt; text-indent:28.35pt; text-align:justify; font-size:10pt"><a href="#_ednref3" style="text-decoration:none"><span style=" font-size:8pt; vertical-align:super; color:#000000">[3]</span></a><span style="">&#xa0;</span><span style="">Классы напряжения (0,4; 6; 10) кВ.</span></p>
    </div>
</div>
</body>
</html>


