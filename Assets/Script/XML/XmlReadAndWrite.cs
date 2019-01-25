using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;

namespace HXXmlSystemSpace
{
    public class XmlReadAndWrite : MonoBehaviour{
        static string Path=Application.streamingAssetsPath+"Config.xml";
        static string SoftName="轨道教学";
        static string SoftMessage="软件信息";
        
        public static bool CreatXml(string _company,string _name,string _version,string _key){
            Debug.Log(Path);
            if(File.Exists(Path)){
                return false;
            }else{
                XmlDocument Config=new XmlDocument();
                XmlElement ParentElement=Config.CreateElement(SoftName);
                XmlElement ChildElement=Config.CreateElement(SoftMessage);
                ChildElement.SetAttribute("公司",_company);
                ChildElement.SetAttribute("姓名",_name);
                ChildElement.SetAttribute("版本号",_version);
                ChildElement.SetAttribute("密钥",_key);
                ParentElement.AppendChild(ChildElement);
                Config.AppendChild(ParentElement);
                Config.Save(Path);
                Debug.Log("创建完成");
                return true;
            }
        }
        public static bool AddXml(string _elementName,string[] _keys,string[] _values){
            if(_keys.Length!=_values.Length){
                Debug.Log("写入配置文件数据长度不匹配");
                return false;
            }
            if(File.Exists(Path)){
                XmlDocument Config=new XmlDocument();
                Config.Load(Path);
                XmlNode ParentElement=Config.SelectSingleNode(SoftName);
                XmlElement ChildElement=Config.CreateElement(_elementName);
                for(int i=0;i<_keys.Length;i++){
                    ChildElement.SetAttribute(_keys[i],_values[i]);
                }
                ParentElement.AppendChild(ChildElement);
                Config.AppendChild(ParentElement);
                Config.Save(Path);
                return true;
            }else{
                return false;
            }
        }
        public static string LoadXml(string _elementName,string _key){
            string value=null;
            XmlDocument Config=new XmlDocument();
            if(File.Exists(Path)){
                Config.Load(Path);
                XmlNodeList ConfigList=Config.SelectSingleNode(SoftName).ChildNodes;
                foreach(XmlNode ElementNode in ConfigList){
                    if(ElementNode.Name==_elementName){
                        value = ElementNode.Attributes[_key].Value;
                    }
                }
            }
            return value;
        }
    }
    
}

