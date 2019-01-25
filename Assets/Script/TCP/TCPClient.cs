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
        String temp=Encoding.ASCII.GetString(bytes);
        Debug.Log("客户端接收的数据长度为："+bytes.Length);
        Debug.Log("客户端接收的数据为："+temp);
    }
    public void SocketSend(){
        Debug.Log("客户端发送数据");
        byte[] SendData=new byte[1024];
        SendData=Encoding.ASCII.GetBytes("ABCD");
        Debug.Log("客户端发送数据长度为："+SendData.Length);
        _Tcp.Send(SendData);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
