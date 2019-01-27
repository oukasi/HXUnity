using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SocketDataRead : MonoBehaviour
{
    string TcpMessageReadStr;
    string TcpMessageHeadStr;
    string TcpMessageLengthStr;
    string TcpMsaageBodyStr;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(SocketData.TcpClientMessageQuene.Count!=0){
            TcpMessageReadStr=SocketData.TcpClientMessageQuene.Dequeue();
            TcpMessageHeadStr=TcpMessageReadStr.Substring(0,2);
            TcpMessageLengthStr=TcpMessageReadStr.Substring(2,4);
            TcpMsaageBodyStr=TcpMessageReadStr.Substring(6,Convert.ToInt32(TcpMessageLengthStr));
            Debug.Log("读取的数据为:"+TcpMessageReadStr);
            Debug.Log("读取的数据头为:"+TcpMessageHeadStr);
            Debug.Log("读取的数据长度为:"+TcpMessageLengthStr);
            Debug.Log("读取的数据消息为:"+TcpMsaageBodyStr);
        }
    }
}
