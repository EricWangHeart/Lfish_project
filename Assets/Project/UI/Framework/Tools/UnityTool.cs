using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnityTool 
{
   public static GameObject FindGameObject(string GameObjectName)
    {
        GameObject TmpGameObj = GameObject.Find(GameObjectName);
        if(TmpGameObj==null)
        {
            Debug.LogWarning("未找到"+GameObjectName+"对象");
            return null;
        }

        return TmpGameObj;
    }

   

    public static GameObject FindChildGameObject(GameObject Container,string gameObjectName)

    {
        if(Container==null)
        {
            Debug.LogWarning("Container=NULL");

            return null;
        }

        Transform GameObjectTF = null;

        if (Container.name == gameObjectName) 
        {
            GameObjectTF = Container.transform;
        }
        else
        {
            Transform[] allChildren = Container.transform.GetComponentsInChildren<Transform>();
            foreach(Transform child in allChildren)
            {
                if(child.name == gameObjectName)
                {
                    if (GameObjectTF == null)
                        GameObjectTF = child;
                    else
                        Debug.LogWarning("组件名重复");
                }

            }
        }

        if(GameObjectTF==null)
        {
            Debug.LogWarning("找不到该子组件");
            return null;
        }

        return GameObjectTF.gameObject;
    }
}
