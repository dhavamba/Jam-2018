using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackOrder : MonoBehaviour
{
    private List<OrderEnum> orders;
    private Movement movement;

    [SerializeField]
    private GameObject mine;
    [SerializeField]
    private GameObject wallMine;

    private Transform mineParent;
    private int numberOrder;

    private void Awake()
    {
        orders = new List<OrderEnum>();
        movement = GameObject.FindObjectOfType<Movement>();
    }

    public List<OrderEnum> GetOrders()
    {
        return orders;
    }

    public void Add(params OrderEnum[] order)
    {
        orders.AddRange(order);
        numberOrder++;
        if (numberOrder > GameObject.FindObjectOfType<LevelManager>().getLevel())
        {
            GameObject.FindObjectOfType<Movement>().SetActivate(false);
            CreateMine();
            numberOrder = 0;
        }
    }

    private void CreateMine()
    {
        mineParent = GameObject.Instantiate(wallMine, movement.transform.GetChild(0).position, Quaternion.identity, movement.transform.GetChild(0)).transform;
        int numberMine = 7;
        foreach (OrderEnum o in orders)
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
        mineParent.GetChild(numberMine).gameObject.SetActive(false);
    }

    public List<OrderEnum> Play()
    {
        StartCoroutine(MyCoroutine());
        List<OrderEnum> tmp = new List<OrderEnum>(orders);
        orders = new List<OrderEnum>();
        return tmp;
    }

    IEnumerator MyCoroutine()
    {
        foreach (OrderEnum order in orders)
        {
            //Debug.Log("WEEE");
            switch (order)
            {
                case OrderEnum.Left:
                    movement.Left();
                    break;
                case OrderEnum.Right:
                    movement.Right();
                    break;
            }
            yield return new WaitForSeconds(movement.duration);
        }
    }




    public List<OrderEnum> Play(float time)
    {
        float orderDuration = time / orders.Count;

        foreach (OrderEnum order in orders)
        {
            switch (order)
            {
                case OrderEnum.Left:
                    movement.Left(orderDuration);
                    break;
                case OrderEnum.Right:
                    movement.Right(orderDuration);
                    break;
            }
        }
        List<OrderEnum> tmp = new List<OrderEnum>(orders);
        orders = new List<OrderEnum>();
        return tmp;
    }
}
