using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HXXmlSystemSpace;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;

public class TCPServer : MonoBehaviour
{
    private string IP;
    private int Port;
    private int ConnectionRequestCount;
    private bool IsOn=true;
    private Socket ServerSocket;
    Thread ConnectThread;
    private byte[] ReceiveData;
    int ReceiveNumber=0;
    string ReceiveStr;

    // Start is called before the first frame update
    void Start()
    {
        IP=XmlReadAndWrite.LoadXml("TCPServer","IP");
        Port=Convert.ToInt32(XmlReadAndWrite.LoadXml("TCPServer","Port"));
        ConnectionRequestCount=Convert.ToInt32(XmlReadAndWrite.LoadXml("TCPServer","ConnectionRequestCount"));
        ServerSocket=new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
        ServerSocket.SetSocketOption(SocketOptionLevel.Socket,SocketOptionName.ReuseAddress,true);
        ServerSocket.Bind(new IPEndPoint(IPAddress.Parse(IP),Port));
        ServerSocket.Listen(ConnectionRequestCount);
        ServerSocket.NoDelay=true;
        ConnectThread=new Thread(SocketConnet);
        ConnectThread.Start();
    }

    // Update is called once per frame
    private void SocketConnet(){
        Socket ClientSocket=null;
        while(IsOn){
            try{
                ClientSocket=ServerSocket.Accept();
                Thread ReceiveThread=new Thread(SocketReceive);
                ReceiveThread.Start(ClientSocket);
            }catch{
                break;
            }
        }
    }
    private void SocketReceive(object _clientSocket){
        Socket ClientSocket=_clientSocket as Socket;
        Debug.Log("开始接受数据");
        while(true){
            ReceiveData=new byte[1024];
            try{
                ReceiveNumber=ClientSocket.Receive(ReceiveData);
                if (ReceiveNumber>0)
                {
                    String temp=Encoding.ASCII.GetString(ReceiveData);
                    Debug.Log("服务器接收数据长度："+ReceiveNumber);
                    Debug.Log("服务器接收"+temp);
                    ClientSocket.Send(ReceiveData);
                }      
            }catch{

            }
        }
    }
    void Update()
    {
        
    }
}
