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
    string ResultStr=null;
    IPEndPoint TcpClientPoint;

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
            }catch{
                break;
            }
            TcpClientPoint=(IPEndPoint)ClientSocket.RemoteEndPoint;
            SocketData.TCPClientDic.Add(TcpClientPoint.Address,ClientSocket);
            Debug.Log("Link Client IP is："+TcpClientPoint.Address+":"+TcpClientPoint.Port.ToString());
            Thread ReceiveThread=new Thread(SocketReceive);
            ReceiveThread.Start(ClientSocket);
        }
    }
    private void SocketReceive(object _clientSocket){
        Socket ClientSocket=_clientSocket as Socket;
        while(true){
            ReceiveData=new byte[1024];
            try{
                ReceiveNumber=ClientSocket.Receive(ReceiveData);
                if (ReceiveNumber>0)
                {
                    byte[] CopyReceiveData=new byte[ReceiveNumber];
                    Array.Copy(ReceiveData,0,CopyReceiveData,0,CopyReceiveData.Length);
                    if(SocketData.TcpClientMessageCheck(CopyReceiveData,out ResultStr)){
                        Debug.Log("服务器接受的数据为："+ResultStr);
                        SocketData.TcpClientMessageQuene.Enqueue(ResultStr);
                        SocketSend(ClientSocket,CopyReceiveData);
                    }else{
                        Debug.Log(ResultStr);
                    }
                }      
            }catch{

            }
        }
    }
    public void SocketSend(Socket _clientSocket,String _head,string _body ){
        byte[] SendData=Encoding.ASCII.GetBytes(SocketData.TcpClientMessageSend(_head,_body));
        _clientSocket.Send(SendData);
    }
    public void SocketSend(Socket _clientSocket,Byte[] _message){
        _clientSocket.Send(_message);
    }
    void Update()
    {
        
    }
}
