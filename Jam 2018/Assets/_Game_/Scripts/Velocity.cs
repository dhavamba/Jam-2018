using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Velocity : MonoBehaviour
{

    [SerializeField]
    private float speed;
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(Vector3.down * speed);
        Invoke("Destroy", 8f);
	}

    void Destroy()
    {
        GameObject.Destroy(gameObject);
    }
}
