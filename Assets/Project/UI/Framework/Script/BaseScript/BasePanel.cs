using System.Collections;
using System.Collections.Generic;
using PUIType;
using UnityEngine;
using UnityEngine.UI;

namespace UIFramework
{
    public class BasePanel
    {
        protected Transform transform; //窗体
        protected string resName; //资源名称
        protected bool resident; //是否常驻
        protected bool visible = false; //是否可见
        protected WindowType selfType; //窗体类型
        protected ScenesType scenesType; //场景类型

        //UI控件 按钮
        protected Button[] buttonList; //按钮列表
        protected Text[] textList; //文字列表
        protected Image[] imageList; //图片列表
        protected InputField[] inputFieldList; //输入列表
        protected Slider[] sliderList; //滑动条列表

        //需要给子类提供的接口:
        //初始化
        protected virtual void Awake()
        {
            //表示隐藏的物体也会查找
            buttonList = transform.GetComponentsInChildren<Button>(true);
            textList = transform.GetComponentsInChildren<Text>(true);
            imageList = transform.GetComponentsInChildren<Image>(true);
            inputFieldList = transform.GetComponentsInChildren<InputField>(true);
            sliderList = transform.GetComponentsInChildren<Slider>(true);
            RegisterUIEvent();
        }

        //UI事件的注册
        protected virtual void RegisterUIEvent()
        {

        }

        //添加监听游戏事件
        protected virtual void OnAddListener()
        {

        }

        //移除游戏事件
        protected virtual void OnRemoveListener()
        {

        }

        //每次打开
        protected virtual void OnEnable()
        {

        }

        //每次关闭
        protected virtual void OnDisable()
        {

        }

        //每帧更新
        public virtual void Update(float deltaTime)
        {

        }

        //---------------WindowManager

        public void Open()
        {
            if (transform == null)
            {
                if (Create())
                {
                    Awake(); //初始化
                }
            }

            if (transform.gameObject.activeSelf == false)
            {
                UIRoot.SetParent(transform, true, selfType == WindowType.TipsWindow);
                transform.gameObject.SetActive(true);
                visible = true;
                OnEnable(); //调用激活时候触发的事件
                OnAddListener(); //添加事件
            }
        }

        public void Close(bool isDestroy = false)
        {
            if (transform.gameObject.activeSelf == true)
            {
                OnRemoveListener(); //移除游戏事件
                OnDisable(); //隐藏时候触发的事件
                if (isDestroy == false)
                {
                    if (resident)
                    {
                        transform.gameObject.SetActive(false);
                        UIRoot.SetParent(transform, false, false);

                    }
                    else
                    {
                        GameObject.Destroy(transform.gameObject);
                        transform = null;
                    }
                }
                else
                {
                    GameObject.Destroy(transform.gameObject);
                    transform = null;
                }

            }
            //不可见的状态
            visible = false;
        }

        public void PreLoad()
        {
            if (transform == null)
            {
                if (Create())
                {
                    Awake();
                }
            }
        }
        /// <summary>
        /// 设置文本显示内容 labelName:文本组件名字 content:文本内容
        /// </summary>
        /// <param name="labelName"></param>
        /// <param name="content"></param>
        public void SetText(string labelName, string content)
        {
            foreach (var text in textList)
            {
                if (text.name == labelName)
                {
                    text.text = content;
                    break;
                }
            }
        }
        /// <summary>
        /// 设置Image组件显示的图片
        /// </summary>
        /// <param name="imgName"></param>
        /// <param name="icon"></param>
        public void SetImage(string imgName, Sprite icon)
        {
            foreach (var img in imageList)
            {
                if (img.name == imgName)
                {
                    img.sprite = icon;
                    break;
                }
            }
        }
        /// <summary>
        /// 启用按钮
        /// </summary>
        /// <param name="name"></param>
        public void EnableButton(string name)
        {
            foreach (var button in buttonList)
            {
                if (button.name == name)
                {
                    button.enabled = true;
                    break;
                }
            }
        }
        /// <summary>
        /// 禁用按钮
        /// </summary>
        /// <param name="name"></param>
        public void UnableButton(string name)
        {
            foreach (var button in buttonList)
            {
                if (button.name == name)
                {
                    button.enabled = false;

                    break;
                }
            }
        }
        //隐藏按钮
        public void HideButton(string buttonName)
        {
            foreach (var button in buttonList)
            {
                if (button.name == buttonName)
                {
                    button.gameObject.SetActive(false);
                    break;
                }
            }
        }
        //显示按钮
        public void ShowButton(string buttonName)
        {
            foreach (var button in buttonList)
            {
                if (button.name == buttonName)
                {
                    button.gameObject.SetActive(true);
                    break;
                }
            }
        }
        //隐藏文本框
        public void HideText(string textName)
        {
            foreach (var text in textList)
            {
                if (text.name == textName)
                {
                    text.gameObject.SetActive(false);
                    break;
                }
            }
        }
        //显示文本框
        public void ShowText(string textName)
        {
            foreach (var text in textList)
            {
                if (text.name == textName)
                {
                    text.gameObject.SetActive(true);
                    break;
                }
            }
        }

        //获取场景类型
        public ScenesType GetScenesType()
        {
            return scenesType;
        }

        //窗体类型
        public WindowType GetWindowType()
        {
            return selfType;
        }

        //获取根节点
        public Transform GetRoot()
        {
            return transform;
        }

        //是否可见
        public bool IsVisible()
        {
            return visible;
        }

        //是否常驻
        public bool IsREsident()
        {
            return resident;
        }

        //------内部----
        private bool Create()
        {
            if (string.IsNullOrEmpty(resName))
            {
                return false;
            }

            if (transform == null)
            {
                var obj = Resources.Load<GameObject>(resName);
                if (obj == null)
                {
                    Debug.LogError($"未找到UI预制件{selfType}");
                    return false;
                }
                transform = GameObject.Instantiate(obj).transform;

                transform.gameObject.SetActive(false);

                UIRoot.SetParent(transform, false, selfType == WindowType.TipsWindow);
                return true;
            }

            return true;
        }
    }

}