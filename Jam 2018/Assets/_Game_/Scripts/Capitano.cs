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

    public int[] createSequence(int size)
    {
        //0 è sinistra, 1 è destra
        int[] sequence = new int[size];
        for (int i=0; i<size; i++)
        {
            sequence[i] = (int)(Random.value * 2);
        }
        return sequence;
    }



}
