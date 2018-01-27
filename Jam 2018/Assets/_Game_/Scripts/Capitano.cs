using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capitano : MonoBehaviour
{
    /*
     * 1: normale
     * 2: meno tempo
     * 3: comandi invertiti
     * 4: non fare niente
    */
    private int type; 

	void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}

    public int getType()
    {
        return type;
    }

    public List<OrderEnum> createSequence(int size)
    {
        //0 è sinistra, 1 è destra
        List<OrderEnum> sequence = new List<OrderEnum>();
        int num = 0;
        for (int i=0; i<size; i++)
        {
            num = (int)(Random.value * 2);
            if (num==0)
            {
                sequence.Add(OrderEnum.Left);
            }
            else
            {
                sequence.Add(OrderEnum.Right);
            }
            
        }
        return sequence;
    }



}
