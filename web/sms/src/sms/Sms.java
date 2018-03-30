/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package sms;

import okhttp3.*;

/**
 *
 * @author yk
 */
public class Sms {

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        try {
            OkHttpClient client = new OkHttpClient();

            MediaType mediaType = MediaType.parse("application/json");
            RequestBody body = RequestBody.create(mediaType, "{\"from\": \"CLX\",\r\n\"to\": [\"+65 91164808\"],\r\n\"body\": \"Hi this is my first message using CLX's SMS REST API\" }");
            Request request = new Request.Builder()
                    .url("https://api.clxcommunications.com/xms/v1/singaporem42/batches")
                    .post(body)
                    .addHeader("Authorization", "Bearer 1e205ab201fd477dacfcda68a5d5c756")
                    .addHeader("Content-Type", "application/json")
                    .build();

            Response response = client.newCall(request).execute();

        } catch (Exception e) {
            System.out.println(e.toString());
        }
        System.out.println("sent");
    }

}
