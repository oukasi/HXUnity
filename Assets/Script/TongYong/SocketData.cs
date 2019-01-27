using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System;

public static class SocketData 
{
    public static Dictionary<IPAddress,Socket> TCPClientDic=new Dictionary<IPAddress, Socket>();
    public static Queue<string> TcpClientMessageQuene=new Queue<string> ();
    public static bool TcpClientMessageCheck(byte[] _message,out string _result){
        string MessageHead=Encoding.ASCII.GetString(_message,0,2);
        string MessageLength=Encoding.ASCII.GetString(_message,2,4);
        string MessageResult=Encoding.ASCII.GetString(_message,6,_message.Length-6);
        if(MessageResult.Length==Convert.ToInt32(MessageLength)){
            _result=MessageHead+MessageLength+MessageResult;
            return true;
        }else{
            _result="Message length is wrong";
            return false;
        }
    }
    public static string TcpClientMessageSend(string _head,string _body){
        string MessageSend;
        string MessageLength=Convert.ToString(_body.Length);
        MessageLength=MessageLength.PadLeft(4,'0');
        return MessageSend=_head+MessageLength+_body;
    }
}
