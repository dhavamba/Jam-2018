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

    private bool activate;

    private StackOrder stack;

    private void Awake()
    {
        stack = GameObject.FindObjectOfType<StackOrder>();
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (GetLeft() && activate)
        {
            stack.Add(OrderEnum.Left);
        }
        if (GetRight() && activate)
        {
            stack.Add(OrderEnum.Right);
        }
    }

    public void SetActivate(bool b)
    {
        activate = b;
    }

    public void Left(float orderDuration)
    {
        transform.DOMove(Translate(Vector3.left * unit), orderDuration);
    }

    public void Left()
    {
        transform.DOMove(Translate(Vector3.left * unit), duration);
    }

    public void Right(float orderDuration)
    {
        transform.DOMove(Translate(Vector3.right * unit), orderDuration);
    }

    public void Right()
    {
        transform.DOMove(Translate(Vector3.right * unit), duration);
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
