using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PUIType;

namespace UIFramework
{
    public class UIManager : MonoSingleton<UIManager>
    {
        Dictionary<WindowType, BasePanel> panelDIC = new Dictionary<WindowType, BasePanel>();
        //构造函数 初始化
        public UIManager()
        {
            //添加窗口
            panelDIC.Add(WindowType.TipsWindow, new TipPanel());
            panelDIC.Add(WindowType.LoadingWindow, new LoadingPanel());
            panelDIC.Add(WindowType.DialogueWindow, new DialoguePanel());
            panelDIC.Add(WindowType.HUDPanel, new HUDPanel());
            panelDIC.Add(WindowType.BeginWindow, new BeginPanel());
        }
        
        public void Update()
        {
            foreach (var window in panelDIC.Values)
            {
                if (window.IsVisible())
                {
                    window.Update(Time.deltaTime);
                }
            }

        }
        //打开窗口
        public BasePanel OpenWindow(WindowType type)
        {
            BasePanel window;
            if (panelDIC.TryGetValue(type, out window))
            {
                window.Open();
                return window;
            }
            else
            {
                Debug.LogError($"Open Error:{type}");
                return null;
            }
        }

        //关闭窗口
        public void CloseWindow(WindowType type)
        {
            BasePanel window;
            if (panelDIC.TryGetValue(type, out window))
            {
                window.Close();
            }
            else
            {
                Debug.LogError($"Open Error:{type}");
            }
        }

        //预加载
        public void PreLoadWindow(ScenesType type)
        {
            foreach (var item in panelDIC.Values)
            {
                if (item.GetScenesType() == type)
                {
                    item.PreLoad();
                }
            }
        }

        //隐藏掉某个类型的所有窗口
        public void HideAllWindow(ScenesType type, bool isDestroy = false)
        {

            foreach (var item in panelDIC.Values)
            {
                if (item.GetScenesType() == type)
                {
                    item.Close(isDestroy);
                }
            }
        }
        /// <summary>
        /// 提示窗口
        /// </summary>
        /// <param name="content"></param>
        public void ShowMessageBox(string content)
        {
            BasePanel panel;
            if (panelDIC.TryGetValue(WindowType.TipsWindow, out panel))
            {
                panel.Open();
                panel.SetText("tipInfo", content);
            }
            else
            {
                Debug.LogError($"Open Error:MessageBox open fail");
            }
        } 

        public void DialogueBox(string content,string name,Sprite header)
        {
            BasePanel panel;
            if (panelDIC.TryGetValue(WindowType.DialogueWindow, out panel))
            {
                panel.Open();
                panel.SetText(GetPanel<DialoguePanel>(WindowType.DialogueWindow).DialogueInfo, content);
                panel.SetText(GetPanel<DialoguePanel>(WindowType.DialogueWindow).Name, name);
                panel.SetImage(GetPanel<DialoguePanel>(WindowType.DialogueWindow).Header, header);
            }
            else
            {
                Debug.LogError($"Open Error:MessageBox open fail");
            }
        }
      
        public T GetPanel<T>(WindowType type) where T : BasePanel
        {
            BasePanel window;
            if (!panelDIC.TryGetValue(type, out window))
            {
                return null;
            }
            return window as T;
        }
    }
}