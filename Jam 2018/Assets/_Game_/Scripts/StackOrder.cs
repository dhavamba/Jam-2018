using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackOrder : MonoBehaviour
{
    private List<OrderEnum> orders;
    private Movement movement;

    private void Awake()
    {
        orders = new List<OrderEnum>();
        movement = GameObject.FindObjectOfType<Movement>();
    }

    public List<OrderEnum> GetOrders()
    {
        return orders;
    }

    public void Add(OrderEnum order)
    {
        orders.Add(order);
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
