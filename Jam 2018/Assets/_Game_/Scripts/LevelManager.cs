using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public float timeEvent; //il tempo di durata di un evento
    public int numEventsPerLevel; //numero di eventi per livello
    public int lives;
    //public float timeReduction; //il tempo di cui diminuisce l'evento

    private int level; //il livello attuale
    private float startDelay;
    private float timerStart; //il tempo di start dell'evento
    private bool eventMoment; //il momento dell'evento è attivo?
    private bool correct; //la risposta è corretta?
    private int contEvents; //quanti eventi sono passati dall'inizio del livello
    //public float timeAnimation; //il tempo di durata dell'animazione

    void Start()
    {
        timerStart = Time.time;
        correct = false;
        contEvents = 0;
        level = 1;
    }

    void Update()
    {
        //Inizializza il gioco
        if (Time.time >= timerStart + startDelay)
        {


            //alla fine riattiva l'evento
            timerStart = Time.time;
            eventMoment = true;
        }
        
        //Inizio evento
        if (Time.time >= timerStart + timeEvent && eventMoment)
        {
            //Agisci e setta il *correct
            OrderEnum[] sequence = GetComponent <Capitano>().createSequence(level);

            

            //Aggiorna il contatore di eventi
            contEvents++;
            //Verifica il passaggio di livello
            if (contEvents >= numEventsPerLevel)
            {
                level++;
                //aggiungi una persona al pool
                contEvents = 0;
            }
        }

        //Inizio animazione
        if (!eventMoment)
        {
            //Fai l'animazione


            //alla fine riattiva l'evento
            timerStart = Time.time;
            eventMoment = true;
        }



    }
}
