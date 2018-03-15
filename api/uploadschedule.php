<?php
/**
 * Created by PhpStorm.
 * User: Wei Hong
 * Date: 15/3/2018
 * Time: 11:42 PM
 */

require_once  $_SERVER['DOCUMENT_ROOT'] . "/vendor/autoload.php";
use MicrosoftAzure\Storage\Blob\BlobRestProxy;

#JSON Response
$jsonResponeArr = array();

#Restricts to POST
if ($_SERVER['REQUEST_METHOD'] != "POST") {
    onError('API only accepts POST request');
}

#Reads Config file
$config = parse_ini_file('blobstorageconfig.ini');

if (!$config) {
    onError('Error reading Blob Storage config file');
}

$accountName = $config['account']['name'];
$accountKey = $config['account']['key'];

#Attemps to retrieve file uploaded
if ($_FILES['schedule']['error'] != UPLOAD_ERR_OK) {
    onError($_FILES['schedule']['error']);
}

$fileName = $_FILES['schedule']['name'];
$file = $_FILES['schedule']['tmp_name'];


#Blob Storage Related
$connectionStr = "DefaultEndpointsProtocol=https;AccountName={$accountName};AccountKey={$accountKey}";
$blobClient = BlobRestProxy::createBlobService($connectionStr);
$containerName = $config['container']['name'];

#Uploading to blob storage
$content = fopen($file, "r");
if (!$content) {
    onError('Error opening file to convert to blob for uploading');
}

$blobClient->createBlockBlob($containerName, $fileName, $content);


function onError($reason) {
    global $jsonResponeArr;

    $jsonResponeArr['status'] = 'error';
    $jsonResponeArr['statusCode'] = '503';
    $jsonResponeArr['reason'] = $reason;

    printJSON();
}

function printJSON() {
    header('Content-type: text/json');
    global $jsonResponseArr;
    print json_encode($jsonResponseArr, JSON_PRETTY_PRINT);
    die();
}