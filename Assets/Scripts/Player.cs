using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Protocol;
using Google.Protobuf;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Protocol.LoginRequest req = new Protocol.LoginRequest();

        req.Email = "test@gmail.com";
        req.Password = "testPassword";

        System.IO.MemoryStream reqStream = new System.IO.MemoryStream();
        byte[] byteArr = req.ToByteArray();


        // deserialize
        MessageParser<Protocol.LoginRequest> parser = Protocol.LoginRequest.Parser;
        Protocol.LoginRequest receivedRequest = parser.ParseFrom(byteArr);

        Debug.Log(receivedRequest.Email);
        Debug.Log(receivedRequest.Password);
    }
}
