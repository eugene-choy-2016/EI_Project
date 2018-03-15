<?php
/**
 * Created by PhpStorm.
 * User: weihong
 * Date: 15/3/18
 * Time: 4:56 PM
 */
require_once  $_SERVER['DOCUMENT_ROOT'] . "/vendor/autoload.php";
use MicrosoftAzure\Storage\Blob\BlobRestProxy;



$connectionStr = "DefaultEndpointsProtocol=https;AccountName={$blobAccountName};AccountKey={$blobAccountKey}";

$blobClient = BlobRestProxy::createBlobService($connectionStr);

$containerName = 'test';

$file = $_FILES['file']['name'];
echo $file;
$tmpName = $_FILES['file']['tmp_name'];
echo $tmpName;


$content = fopen($tmpName, "r");
$blobClient->createBlockBlob($containerName, $file, $content);

