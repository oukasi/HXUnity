using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using HXEventSystemSpace;
using HXXmlSystemSpace;
using System;

public class ModleClickandRotate : MonoBehaviour
{
    [HideInInspector]
    public float RotateSpeed=0;
    [HideInInspector]
    public float ScaleSpeed=0;
    [HideInInspector]
    public float ScaleLimitMin=0;
    [HideInInspector]
    public float ScaleLimitMax=0;
    [HideInInspector]
    public string ClickRayLayerName;
    public Camera MainCamera;
    private Vector2 OldMousePosition;
    private Vector2 NewMousePosition;
    Ray ClickRay;
    RaycastHit ClickHit;
    LayerMask ClickRayLayer;
    // Start is called before the first frame update
    void Start()
    {
        ClickRayLayer=1<<(LayerMask.GetMask(ClickRayLayerName));
        RotateSpeed= (float)Convert.ToDouble(XmlReadAndWrite.LoadXml("ModleClickandRotates","RotateSpeed"));
        ScaleSpeed= (float)Convert.ToDouble(XmlReadAndWrite.LoadXml("ModleClickandRotates","ScaleSpeed"));
        ScaleLimitMin= (float)Convert.ToDouble(XmlReadAndWrite.LoadXml("ModleClickandRotates","ScaleLimitMin"));
        ScaleLimitMax= (float)Convert.ToDouble(XmlReadAndWrite.LoadXml("ModleClickandRotates","ScaleLimitMax"));
        ScaleLimitMin=ScaleLimitMin*transform.localScale.x;
        ScaleLimitMax=ScaleLimitMax*transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        #if UNITY_STANDALONE_WIN
        if(!EventSystem.current.IsPointerOverGameObject())
        {
            if(Input.GetMouseButtonDown(0)){
                OldMousePosition=Input.mousePosition;
            }
            if(Input.GetMouseButton(0)){
                NewMousePosition=Input.mousePosition;
                ModleRotate(NewMousePosition-OldMousePosition);
                OldMousePosition=NewMousePosition;
            }
            if(Input.GetMouseButtonUp(0)){
                Debug.Log("现在按下的鼠标0");
                ChoseClick(Input.mousePosition);
            }
            if(Input.GetMouseButtonUp(1)){
                Debug.Log("现在按下的鼠标1");
                ChoseClick(Input.mousePosition);
            }
            if(Input.GetMouseButtonUp(2)){
                Debug.Log("现在按下的鼠标2");
            }
            if(Input.GetAxis("Mouse ScrollWheel")!=0){
                Debug.Log("中键滚动量："+Input.mouseScrollDelta.ToString());
                ModleScale(Input.mouseScrollDelta.y);
            }
        }
        #endif
    }
    void ChoseClick(Vector2 _clickposition){
        ClickRay=MainCamera.ScreenPointToRay(_clickposition);
        if(Physics.Raycast(ClickRay,out ClickHit,Mathf.Infinity,ClickRayLayer)){
            HXEventSystem.Click.TriggerEvent(ClickHit.transform,HXEventSystem.ClickActionEnum.LeftClick);
        }
    }
    void FunctionClick(Vector2 _clickposition){
         ClickRay=MainCamera.ScreenPointToRay(_clickposition);
        if(Physics.Raycast(ClickRay,out ClickHit,Mathf.Infinity,ClickRayLayer)){
            HXEventSystem.Click.TriggerEvent(ClickHit.transform,HXEventSystem.ClickActionEnum.RigthClick);
        }else{
            HXEventSystem.Click.TriggerEvent(null,HXEventSystem.ClickActionEnum.RigthClick);
        }
    }
    void ModleRotate(Vector2 _position){
        if(Mathf.Abs(_position.x)>Mathf.Abs(_position.y)){
            transform.Rotate(Vector3.up,-_position.x*RotateSpeed*Time.deltaTime,Space.World);
        }else{
            transform.Rotate(Vector3.right,_position.y*RotateSpeed*Time.deltaTime,Space.World);
        }
    }
    void ModleScale(float _delta){
        if(transform.localScale.x*(1+ScaleSpeed*_delta*Time.deltaTime)>ScaleLimitMin&&transform.localScale.x*(1+ScaleSpeed*_delta*Time.deltaTime)<ScaleLimitMax){
            transform.localScale+=transform.localScale*ScaleSpeed*_delta*Time.deltaTime;
        }
    }
}
