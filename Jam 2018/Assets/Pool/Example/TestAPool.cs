using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAPool : MonoBehaviour
{
    public GameObject obj;
    [Range(0,1)]
    public float time;
    public int numberMax;

	// Use this for initialization
	void Start ()
    {
        Create();
	}

    void Create()
    {
        StaticPool.Instantiate(obj, Vector3.down, gameObject.transform, numberMax);
        Invoke("Put", time);
    }

    void Put()
    {
        StaticPool.Instantiate(obj, Vector3.down, gameObject.transform);
        Invoke("Put", time);
    }
}
