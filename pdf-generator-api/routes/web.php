<?php

use Illuminate\Support\Facades\Route;
use App\Http\Controllers\PdfController;


Route::post('/api/generate-pdf', [PdfController::class, 'generatePdf']);
Route::post('/api/generate-dopinfo', [PdfController::class, 'generateDopInfo']);
