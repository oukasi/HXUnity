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
    // Start is called before the first frame update
    void Start()
    {
        /**
        XmlReadAndWrite.CreatXml("HX","oukasi","1.0.0","891114");
        while(true){
            if(XmlReadAndWrite.AddXml("ModleClickandRotates",Keys,Values)){
                break;
            }
        }
        Debug.Log("写入完成");
        **/
        if(XmlReadAndWrite.AddXml(ElementName,Keys,Values)){
            Debug.Log("写入完成");
        }
       
    }
    public void click(){

    }

    void Update()
    {
        
    }
}
