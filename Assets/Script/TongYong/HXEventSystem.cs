using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HXEventSystemSpace
{
    public class HXEventSystem:MonoBehaviour{
        // 点击事件
        public enum ClickActionEnum
        {
            LeftClick,
            RigthClick,
            TouchClick,
            HoldClick
        }
        public  delegate void CLickDel(Transform _transform,ClickActionEnum _action);
        public static event CLickDel ClickEvent;
        public struct ClickStruct
        {
            public void AddListener(CLickDel _del){
                HXEventSystem.ClickEvent+=_del;
            }
            public void RemoveListener(CLickDel _del){
                HXEventSystem.ClickEvent-=_del;
            }
            public void TriggerEvent(Transform _transform,ClickActionEnum _action){
                ClickEvent(_transform,_action);
            }
        }
        public static ClickStruct Click;
        // 动画动作事件
        public enum AnimationActionEnum
        {
            // 动画播放
            Play,
            // 动画停止
            Stop,
            // 动画暂停
            Pause,
            // 动画完成
            Finsh
        }
        public delegate void AnimationActionDel(Transform _transform, AnimationActionEnum _action);
        public static event AnimationActionDel AnimationActionEvent;
        public struct AnimationActionStruct
        {
            public void AddListener(AnimationActionDel _action){
                HXEventSystem.AnimationActionEvent+=_action;
            }
            public void RemoveListener(AnimationActionDel _action){
                HXEventSystem.AnimationActionEvent-=_action;
            }
            public void TriggerEvent(Transform _transform, AnimationActionEnum _action){
                AnimationActionEvent(_transform,_action);
            }
        }
        public static AnimationActionStruct AnimationAction;
        
    }
    
}
    

