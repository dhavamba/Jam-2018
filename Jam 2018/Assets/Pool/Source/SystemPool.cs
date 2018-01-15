using System.Collections.Generic;
using UnityEngine;

public class SystemPool : MonoBehaviour
{
    public float max;
    public GameObject Prefab { private get; set; }

    private List<GameObject> listDisable;
    private List<GameObject> listAble;
    private GameObject aux;

    private void Awake()
    {
        listDisable = new List<GameObject>();
        listAble = new List<GameObject>();
    }

    private void OnDestroy()
    {
        StaticPool.RemovePool(gameObject);
    }

    /// <summary>
    /// Disable GameObject
    /// </summary>
    public void Disable(GameObject obj)
    {
        obj.SetActive(false);
        listDisable.Add(obj);
        listAble.Remove(obj);
    }

    /// <summary>
    /// Disable all GameObject
    /// </summary>
    public void AllDisable()
    {
        foreach (GameObject obj in listAble)
        {
            obj.SetActive(false);
            listDisable.Add(obj);
        }
        listAble.Clear();
    }

    public void CreatePool()
    {
        while (listDisable.Count + listAble.Count < max)
        {
            aux = _IstantiateNewObject(Vector3.zero);
            Disable(aux);
        }
    }

    /// <summary>
    /// Create or Recycle GameObject
    /// </summary>
    public GameObject InstantiateObject(Transform parent, Vector3 position)
    {
        if (parent != null)
        {
            position = parent.position + position;
        }

        CreatePool();

        if (listAble.Count >= max)
        {
            Disable(listAble[0]);
        }
        aux = _RecycleObject();

        _ResetElementAux(position);
        return aux;
    }

    private GameObject _RecycleObject()
    {
        aux = listDisable[0];
        aux.SetActive(true);
        listDisable.Remove(aux);
        listAble.Add(aux);
        return aux;
    }

    private void _ResetElementAux(Vector3 position)
    {
        aux.transform.position = position;
        if (aux.GetComponent<Rigidbody>())
        {
            aux.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        transform.rotation = Quaternion.identity;
    }

    private GameObject _IstantiateNewObject(Vector3 position)
    {
        return GameObject.Instantiate(Prefab, position, Quaternion.identity, transform);
    }

}

