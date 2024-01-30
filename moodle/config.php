<?php  // Moodle configuration file

unset($CFG);
global $CFG;
$CFG = new stdClass();

$CFG->dbtype    = 'mysqli';
$CFG->dblibrary = 'native';
$CFG->dbhost    = 'HOST';
$CFG->dbname    = 'NOME_DO_BANCO';
$CFG->dbuser    = 'USER_DB';
$CFG->dbpass    = 'USER_PASS';
$CFG->prefix    = 'mdl_';
$CFG->dboptions = array (
  'dbpersist' => 0,
  'dbport' => '',
  'dbsocket' => '',
  'dbcollation' => 'utf8_general_ci',
  'readonly' => [
    'instance' => 'INSTANCIA'
  ]
);

$CFG->sslproxy = 1;
$CFG->wwwroot   = 'URL';
$CFG->dataroot  = 'moodledata';
$CFG->admin     = '_NAME';

$CFG->session_handler_class = '\core\session\redis';
$CFG->session_redis_host = 'HOST_CACHE';
$CFG->session_redis_port = 9999;  // Optional.
$CFG->session_redis_database = 9999;  // Optional, default is db 0.
$CFG->session_redis_auth = ''; // Optional, default is don't set one.
$CFG->session_redis_acquire_lock_timeout = 9999;
$CFG->session_redis_acquire_lock_retry = 9999; // Optional, default is 100ms (from 3.9)
$CFG->session_redis_lock_expire = 60 * 60 * 8; // 28800 8h
$CFG->session_redis_serializer_use_igbinary = false; // Optional, default is PHP builtin serializer.




$CFG->directorypermissions = 0000;

// Force a debugging mode regardless the settings in the site administration
//@ini_set('display_startup_errors', '1');
//@error_reporting(E_ALL | E_STRICT); // NOT FOR PRODUCTION SERVERS!
//@ini_set('display_errors', '1');    // NOT FOR PRODUCTION SERVERS!
//$CFG->debug = (E_ALL | E_STRICT);   // === DEBUG_DEVELOPER - NOT FOR PRODUCTION SERVERS!
//$CFG->debugdisplay = 1;             // NOT FOR PRODUCTION SERVERS!

require_once(__DIR__ . '/lib/setup.php');

// There is no php closing tag in this file,
// it is intentional because it prevents trailing whitespace problems!
