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

    private void Awake()
    {
        orders = new List<OrderEnum>();
        movement = GameObject.FindObjectOfType<Movement>();
        //Add(OrderEnum.Right);
    }

    public List<OrderEnum> GetOrders()
    {
        return orders;
    }

    public void Add(params OrderEnum[] order)
    {
        orders.AddRange(order);
        CreateMine();
    }

    private void CreateMine()
    {
        mineParent = GameObject.Instantiate(wallMine, movement.transform.GetChild(0).position, Quaternion.identity, movement.transform.GetChild(0)).transform;
        Vector2 aux = mineParent.position;
        foreach (OrderEnum o in orders)
        {
            switch (o)
            {
                case OrderEnum.Left:
                    aux -= Vector2.right * (mine.GetComponent<SpriteRenderer>().bounds.size.x + 0.2f);
                    break;
                case OrderEnum.Right:
                    aux += Vector2.right * (mine.GetComponent<SpriteRenderer>().bounds.size.x);
                    break;
            }
        }
        Physics2D.OverlapPoint(aux).gameObject.SetActive(false);
    }

    public void Play()
    {
        foreach (OrderEnum order in orders)
        {
            switch (order)
            {
                case OrderEnum.Left:
                    movement.Left();
                    break;
                case OrderEnum.Right:
                    movement.Right();
                    break;
            }
        }
        orders.Clear();
    }

    public void Play(float time)
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
        orders.Clear();
    }
}
