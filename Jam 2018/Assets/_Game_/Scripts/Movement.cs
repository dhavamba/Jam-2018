using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float unit;

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
            transform.Translate(Vector2.left * unit);
        }
        if (GetRight())
        {
            transform.Translate(Vector2.right * unit);
        }
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
