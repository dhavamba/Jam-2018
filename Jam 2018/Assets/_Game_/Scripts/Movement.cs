using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float unit;
    [SerializeField]
    private float duration;

    private void Awake()
    {
        
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (GetLeft())
        {
            transform.DOMove(Translate(Vector3.left * unit), duration);
        }
        if (GetRight())
        {
            transform.DOMove(Translate(Vector3.right * unit), duration);
        }
    }

    private Vector3 Translate(Vector3 translate)
    {
        return transform.position + translate;
    }


    private bool GetLeft()
    {
        return InterfaceInput.Instance().Player.GetButtonDown("left");
    }

    private bool GetRight()
    {
        return InterfaceInput.Instance().Player.GetButtonDown("right");
    }
}
