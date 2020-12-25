using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIFramework
{
    public class UIRoot
    {
        static Transform transform;
        static Canvas curCanvas;
        static Transform recyclePool;//回收的窗体 隐藏
        static Transform workstation;//前台显示/工作的窗体
        static Transform noticestation;//提示类型的窗体
        static bool isInit = false;
        public static void Init()
        {
            if (transform == null)
            {
                var obj = Resources.Load<GameObject>("MainCanvas");
                transform = Object.Instantiate(obj).transform;
                curCanvas = transform.GetComponent<Canvas>();
            }

            if (recyclePool == null)
            {
                recyclePool = transform.Find("recyclePool");
            }

            if (workstation == null)
            {
                workstation = transform.Find("workstation");
            }

            if (noticestation == null)
            {
                noticestation = transform.Find("noticestation");
            }
            isInit = true;
        }

        public static void SetParent(Transform window, bool isOpen, bool isTipsWindow = false)
        {
            if (isInit == false)
            {
                Init();
            }

            if (isOpen == true)
            {
                if (isTipsWindow)
                {
                    window.SetParent(noticestation, false);
                }
                else
                {
                    window.SetParent(workstation, false);
                }
            }
            else
            {
                window.SetParent(recyclePool, false);
            }
        }

        public static void SetRenderCamera(Camera _uiCamera)
        {
            curCanvas.worldCamera = _uiCamera;
        }
    }
}