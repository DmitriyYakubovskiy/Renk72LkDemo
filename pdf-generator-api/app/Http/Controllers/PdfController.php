<?php

namespace App\Http\Controllers;

use App\Http\Controllers\Controller;
use Illuminate\Support\Facades\Log;
use Illuminate\Http\Request;
use PDF;

class PdfController extends Controller
{
    private function createBid1(Request $request, $prefix = '')
    {
        return (object) [
            'surname' => $request->input($prefix . 'surname'),
            'name' => $request->input($prefix . 'name'),
            'patronymic' => $request->input($prefix . 'patronymic'),
            'snils' => $request->input($prefix . 'snils'),
            'date_of_birth' => $request->input($prefix .'dateOfBirth') 
                ? (new \DateTime($request->input($prefix .'dateOfBirth')))->format('d.m.Y')
                : null,
            'place_of_birth' => $request->input($prefix . 'placeOfBirth'),
            'phone' => $request->input($prefix . 'phoneNumber'),
            'email' => $request->input($prefix . 'email'),
            'address_reg' => $request->input($prefix . 'addressRegistration'),
            'address_actual' => $request->input($prefix . 'addressActual'),
            'passport' => $request->input($prefix . 'passportType'),
            'passport_series' => $request->input($prefix . 'passportSeries'),
            'passport_number' => $request->input($prefix . 'passportNumber'),
            'passport_date' => $request->input($prefix .'passportDate') 
                ? (new \DateTime($request->input($prefix .'passportDate')))->format('d.m.Y')
                : null,
            'passport_issued_by' => $request->input($prefix . 'passportIssuedBy'),
        ];
    }

    public function generatePdf(Request $request)
    {
        try {
            Log::info('Входящий запрос на генерацию PDF:', [
                'all_request_data' => $request->all(),
                'user_role' => $request->input('userRole'),
                'service' => $request->input('service'),
                'step1' => $request->input('step1'),
                'step2' => $request->input('step2'),
                'step3' => $request->input('step3'),
                'step4' => $request->input('step4'),
                'step5' => $request->input('step5'),
            ]);
            $bid1 = $this->createBid1($request, 'step1.');

            $bid2 = (object) [
                'surname' => $request->input('step2.surname'),
                'name' => $request->input('step2.name'),
                'patronymic' => $request->input('step2.patronymic'),
                'snils' => $request->input('step2.snils'),
                'act' => $request->input('step2.attorney'),
                'phone' => $request->input('step2.phoneNumber'),
                'email' => $request->input('step2.email'),
                'passport' => $request->input('step2.passportType'),
                'passport_series' => $request->input('step2.passportSeries'),
                'passport_number' => $request->input('step2.passportNumber'),
                'passport_issued_by' => $request->input('step2.passportIssuedBy'),
                'passport_date' => $request->input('step2.passportDate') 
                ? (new \DateTime($request->input('step2.passportDate')))->format('d.m.Y')
                : null,
            ];

            $bid3 = (object) [

                'region' => $request->input('step3.region'),
                'district' => $request->input('step3.district'),
                'address_of_object' => $request->input('step3.addressOfObject'),
                'cadastral_number' => $request->input('step3.cadastralNumber'),
                'reason_for_bid' => $request->input('step3.reasonForBid'),
                'guarant_supplier' => $request->input('step3.guarantySupplier'),
                'type_of_contract' => $request->input('step3.typeOfContract'),
                'voltage_class' => $request->input('step3.voltageClass'),
                'connection_type' => $request->input('step3.connectionType'),
                'power_device' => $request->input('step3.powerDevice'),
                'object_name' => $request->input('step3.objectName'),
            ];


            $step4 = $request->input('step4');

            if (is_object($step4)) {
                $step4 = json_decode(json_encode($step4), true);
            }

            $stages = $step4['stages'] ?? [];
            $points = $step4['points'] ?? [];

            $bid4 = (object) [
                'reliability_category' => $request->input('step4.reliabilityCategory'),
                               
                'old_point_power' => $request->input('step4.oldPointPower'),
                'old_point_volt' => $request->input('step4.oldPointVolt'),

                'count_of_transformers' => $request->input('step4.countOfTransformers'),
                'transformers_power' => $request->input('step4.transformersPower'),
                'generators_power' => $request->input('step4.generatorsPower'),
                'count_of_generators' => $request->input('step4.countOfGenerators'),
                'type_of_load' => $request->input('step4.typeOfLoad'),
                "tech_min" => $request->input('step4.techMin'),
                'justification_min' => $request->input('step4.justificationMin'),
                'payment_order'=> $request->input('step4.paymentOrder'),
            ];

            $bid5 = (object) [
                'filename_1' => $request->input('step5.passportFileId'),
                'filename_2' => null,
                'filename_3' => $request->input('step5.powerDevicesPlanFileId'),
                'filename_4' => $request->input('step5.otherFileId'),
                'filename_5' => null,
                'filename_6' => $request->input('step5.snilsFileId'),
                'filename_7' => $request->input('step5.benefitFileId'),
            ];

            $user_bid = (object)[
                'created_at' => now(),
                'service' => $request->input("service") ?? null,
                'user_role' => $request->input("userRole") ?? null,
            ];
            $pdf = null;

            if($user_bid->user_role == "Физическое лицо")
            {
                if($user_bid->service == "Технологическое присоединение энергопринимающих устройств до 15 кВт") {
                    $pdf = PDF::loadView('Fl.doc', [
                        'bid1' => $bid1,
                        'bid2' => $bid2,
                        'bid3' => $bid3,
                        'bid4' => $bid4,
                        'bid5' => $bid5,
                        'user_bid' => $user_bid,
                        'stages' => $stages,
                        'points' => $points
                    ]);
                } else{
                    $pdf = PDF::loadView('Fl.doc100', [
                        'bid1' => $bid1,
                        'bid2' => $bid2,
                        'bid3' => $bid3,
                        'bid4' => $bid4,
                        'bid5' => $bid5,
                        'user_bid' => $user_bid,
                        'stages' => $stages,
                        'points' => $points
                    ]);
                }
            }
            
            if (!$pdf) {
                throw new \Exception("Не удалось создать PDF. Проверьте параметры user_role и service.");
            }    

            return $pdf->download('prepdf.pdf', 200);

        } catch (\Exception $e) {
            Log::error('Ошибка при генерации PDF: ' . $e->getMessage(), [
                'request' => $request->all(),
                'exception' => $e,
            ]);

            return response()->json([
                'error' => 'Произошла ошибка при генерации PDF: ' . $e->getMessage()
            ], 500);
        }
    }

    public function generateDopInfo(Request $request)
    {
        $bid1 = $this->createBid1($request);

        $pdf = PDF::loadView('Ul.addinfo', [
            'bid1' => $bid1
        ]);

        return $pdf->download('dopinfo.pdf', 200);
    }
}
