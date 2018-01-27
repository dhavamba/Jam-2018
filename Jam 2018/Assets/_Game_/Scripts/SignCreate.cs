using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignCreate : MonoBehaviour
{
    private Sprite right;
    private Sprite left;

    public void Add(params OrderEnum[] orders)
    {
        int i = 0;
        foreach (OrderEnum o in orders)
        {
            switch (o)
            {
                case OrderEnum.Left:
                    transform.GetChild(i).GetComponent<Image>().enabled = true;
                    transform.GetChild(i).GetComponent<Image>().sprite = left;
                    break;
                case OrderEnum.Right:
                    transform.GetChild(i).GetComponent<Image>().enabled = true;
                    transform.GetChild(i).GetComponent<Image>().sprite = right;
                    break;
            }
            i++;
        }
    }

    public void Enable(bool t)
    {
        GetComponent<Image>().enabled = t;
    }

    public void DisableSign()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<Image>().enabled = false;
        }
    }


}
