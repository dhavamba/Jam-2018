using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StackOrder : MonoBehaviour
{
    private List<OrderEnum> orders;
    private Movement movement;

    [SerializeField]
    private float heighWall;
    [SerializeField]
    private GameObject mine;
    [SerializeField]
    private GameObject wallMine;

    private Transform mineParent;
    private int numberOrder;
    private int numberMine;
    private List<OrderEnum> secureOrders;

    private void Awake()
    {
        orders = new List<OrderEnum>();
        movement = GameObject.FindObjectOfType<Movement>();
    }

    public void SetOrders(List<OrderEnum> lista)
    {
        secureOrders = lista;
    }



    public List<OrderEnum> GetOrders()
    {
        return orders;
    }

    public void Add(params OrderEnum[] order)
    {
        orders.AddRange(order);
        numberOrder++;
        if (numberOrder >= GameObject.FindObjectOfType<LevelManager>().getLevel())
        {
            GameObject.FindObjectOfType<Movement>().SetActivate(false);
            numberOrder = 0;
        }
    }

    public void CreateMine(int danger)
    {
        //0 = normale
        //1 = morte istantanea
        //2 = davanti te
        numberMine = 7;
        mineParent = GameObject.Instantiate(wallMine, movement.transform.position + Vector3.up * heighWall, Quaternion.identity).transform;
        if (danger != 1)
        {
            if (danger == 0)
            {
                foreach (OrderEnum o in secureOrders)
                {
                    switch (o)
                    {
                        case OrderEnum.Left:
                            numberMine--;
                            break;
                        case OrderEnum.Right:
                            numberMine++;
                            break;
                    }
                }
            }
            mineParent.GetChild(numberMine).gameObject.SetActive(false);
        }
    }

    public List<OrderEnum> Play(float time)
    {
        float orderDuration = time / orders.Count;

        if (Enumerable.SequenceEqual(orders.OrderBy(t => t), secureOrders.OrderBy(t => t)))
        {
            transform.FindChild("Win").GetComponent<AudioSource>().Play();
        }
        StartCoroutine(MyCoroutine(orderDuration));
        List<OrderEnum> tmp = new List<OrderEnum>(orders);
        orders = new List<OrderEnum>();
        return tmp;
    }


    IEnumerator MyCoroutine(float duration)
    {
        foreach (OrderEnum order in orders)
        {
            //Debug.Log("WEEE");
            switch (order)
            {
                case OrderEnum.Left:
                    movement.Left(duration);
                    break;
                case OrderEnum.Right:
                    movement.Right(duration);
                    break;
            }
            yield return new WaitForSeconds(duration);
        }
    }






    
}
