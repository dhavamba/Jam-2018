using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float timeEvent; //il tempo di durata di un evento
    public int level; //il livello attuale
    public float startDelay;
    private float timerStart;
    //public float timeAnimation; //il tempo di durata dell'animazione
    //public float 

	void Start ()
    {
		timerStart = Time.time;
    }
	
	void Update ()
    {
		//Inizia il livello

	}
}
