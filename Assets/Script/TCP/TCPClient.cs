using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HiSocket;
using HiSocketExample;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using HXXmlSystemSpace;


public class TCPClient : MonoBehaviour
{
    private string IP;
    private int Port;
    private TcpConnection _Tcp;
    private bool _IsConnected;
    private Socket ClinetSocket;
    private int _Counter;
    private string ResultStr;
    // Start is called before the first frame update
    void Start()
    {
        IP=XmlReadAndWrite.LoadXml("TCPServer","IP");
        Port=Convert.ToInt32(XmlReadAndWrite.LoadXml("TCPServer","Port"));
        _Tcp=new TcpConnection(new HXPackage());
        _Tcp.OnConnecting+=OnConnecting;
        _Tcp.OnConnected+=OnConnected;
        _Tcp.OnReceive+=OnReceive;
        _Tcp.Connect(new IPEndPoint(IPAddress.Parse(IP),Port));
    }
    void OnConnecting()
    {
        Debug.Log("<color=green>connecting...</color>");
    }
     void OnConnected()
    {
        Debug.Log("<color=green>connected</color>");
        _IsConnected = true;
    }
    void OnReceive(byte[] bytes){
        if(SocketData.TcpClientMessageCheck(bytes,out ResultStr)){
            Debug.Log("客户端接受的数据为："+ResultStr);
        }else{
            Debug.Log(ResultStr);
        }
    }
    public void SocketSend(string _head,string _body){
        byte[] SendData=Encoding.ASCII.GetBytes(SocketData.TcpClientMessageSend(_head,_body));
        _Tcp.Send(SendData);
    }
    
}
