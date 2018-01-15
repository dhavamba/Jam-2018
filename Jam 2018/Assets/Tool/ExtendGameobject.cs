using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtendGameObject
{
    public static GameObject Instantiate(string name, GameObject prefab = null, bool pool = false, float max = 10)
    {
        return Instantiate(name, Vector3.zero, Quaternion.identity, null, prefab, pool, max);
    }

    public static GameObject Instantiate(string name, Transform parent = null, GameObject prefab = null, bool pool = false, float max = 10)
    {
        return Instantiate(name, Vector3.zero, Quaternion.identity, parent, prefab, pool, max);
    }

    public static GameObject Instantiate(string name, Vector3 position, Transform parent = null, GameObject prefab = null, bool pool = false, float max = 10)
    {
        return Instantiate(name, position, Quaternion.identity, parent, prefab, pool, max);
    }

    public static GameObject Instantiate(Transform parent = null, GameObject prefab = null, bool pool = false, float max = 10)
    {
        return Instantiate("GameObject", Vector3.zero, Quaternion.identity, parent, prefab, pool, max);
    }

    public static GameObject Instantiate(Vector3 position, Transform parent = null, GameObject prefab = null, bool pool = false, float max = 10)
    {
        return Instantiate("GameObject", position, Quaternion.identity, parent, prefab, pool, max);
    }

    public static GameObject Instantiate(string name, Vector3 position, Quaternion quat, Transform parent = null, GameObject prefab = null, bool pool = false, float max = 10)
    {
        GameObject aux;
        if (!pool)
        {
            aux = GameObject.Instantiate(prefab, position, quat, parent);
            aux.name = name;
        }
        else
        {
            aux = StaticPool.Instantiate(prefab, position, parent, max);
            aux.AddOnlyOneComponent<MultiTags>();
            aux.AddTags("pool");
        }
        return aux;
    }

    public static void Destroy(GameObject obj)
    {
        if (obj.HaveTags("pool"))
        {
            StaticPool.Destroy(obj);
        }
        else
        {
            GameObject.Destroy(obj);
        }
    }

    public static T[] FindOtherObjectsOfType<T>(this GameObject obj) where T : MonoBehaviour
    {
        T[] names = GameObject.FindObjectsOfType<T>();
        List<T> list = new List<T>(names);
        list.Remove(list.Find(x => x.gameObject.GetInstanceID() == obj.GetInstanceID()));
        return list.ToArray();
    }

    public static T FindComponentInGameObject<T>(string name) where T : Component
    {
        return GameObject.Find(name)?.GetComponent<T>();
    }


    public static GameObject FindGameObjectWithTags(bool only = false, params string[] tags)
    {
        return MultiTags.FindGameObjectWithMultiTags(only, tags);
    }

    public static GameObject[] FindGameObjectsWithTags(bool only = false, params string[] tags)
    {
        return MultiTags.FindGameObjectsWithMultiTags(only, tags);
    }

    public static T FindObjectOfType<T>() where T : Component
    {
        return GameObject.FindObjectOfType<T>();
    }

    public static T[] FindObjectsOfType<T>() where T : Component
    {
        return GameObject.FindObjectsOfType<T>();
    }

    public static GameObject Find(string name)
    {
        return GameObject.Find(name);
    }

    public static T AddOnlyOneComponent<T>(this GameObject obj) where T : Component
    {
        if (!obj.GetComponent<T>())
        {
            obj.AddComponent<T>();
        }
        return obj.GetComponent<T>();
    }
}
