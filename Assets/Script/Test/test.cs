using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HXEventSystemSpace;
using HXXmlSystemSpace;

public class test : MonoBehaviour
{
    public string ElementName;
    public string[] Keys;
    public string[] Values;
    string[] values=new string[4];

    public  TCPClient tCPClient;
    public  TCPClient tCPClient2;
    // Start is called before the first frame update
    void Start()
    {
       
    }
    public void click(){
        tCPClient.SocketSend(Random.Range(10,99).ToString(),"ABCDEFG"+Random.Range(0,999));

    }

    void Update()
    {
        
    }
}
