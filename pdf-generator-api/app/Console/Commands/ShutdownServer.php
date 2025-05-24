<?php

namespace App\Console\Commands;

use Illuminate\Console\Command;

class ShutdownServer extends Command
{
    protected $signature = 'server:shutdown';
    protected $description = 'Завершает работу сервера';

    public function handle()
    {
        $port = env('REVERB_SERVER_PORT', '8080');
        $this->info('Завершение работы сервера...');
        exec("kill $(lsof -t -i:8000)");
        return 0;
    }
}
