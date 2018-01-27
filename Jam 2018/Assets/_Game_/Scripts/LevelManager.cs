using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public float timeEvent; //il tempo di durata di un evento
    public int numEventsPerLevel; //numero di eventi per livello
    public int lives;
    public float reduceTime;
    //public float timeReduction; //il tempo di cui diminuisce l'evento

    private int level; //il livello attuale
    private float startDelay;
    private float timerStart; //il tempo di start dell'evento
    private bool eventMoment; //il momento dell'evento è attivo?
    private bool animationMoment; //il momento dell'animazione è attivo?
    private bool correct; //la risposta è corretta?
    private int contEvents; //quanti eventi sono passati dall'inizio del livello
    private bool captainSetted; //il capitano è stato deciso?
    private int captainNumber;
    private bool match;
    private List<OrderEnum> sequence1;
    List<OrderEnum> sequence2;
    //public float timeAnimation; //il tempo di durata dell'animazione

    void Start()
    {
        timerStart = Time.time;
        correct = false;
        contEvents = 0;
        level = 1;
        captainSetted = false;
        captainNumber = 1;
        match = false;
        sequence1 = new List<OrderEnum>();
        sequence2 = new List<OrderEnum>();
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
            
            if (!captainSetted)
            {
                setCaptain();
            }

            // TODO : DEVI SETTARE IL MATCH A TRUE

            if (match)
            {//Agisci e setta il *correct
                setMatch();
            }
            // TODO Fai la chiamata all'animazione

        }
        else if (eventMoment)
        {
            //Aggiorna il contatore di eventi
            contEvents++;
            //Verifica il passaggio di livello
            if (contEvents >= numEventsPerLevel)
            {
                level++;
                //aggiungi una persona al pool
                contEvents = 0;
            }
            animationMoment = true;
            eventMoment = false;
            captainSetted = false;
            match = false;
            timeEvent = timeEvent / reduceTime;
        }

        //Inizio animazione
        if (animationMoment)
        {
            //TODO: Fai l'animazione


        }
        else if (!eventMoment)
        {
            //alla fine riattiva l'evento
            timerStart = Time.time;
            eventMoment = true;
            animationMoment = false;
        }
    }

    public void setCaptain()
    {
        int nCaptains = level;
        if (nCaptains > 4)
        {
            nCaptains = 4;
        }
        captainNumber = (int)((Random.value * nCaptains) + 1);
        if (captainNumber == 2)
        {
            timeEvent = timeEvent * reduceTime;
        }
        sequence1 = GetComponent<Capitano>().createSequence(level);

        List<OrderEnum> sequence3 = new List<OrderEnum>(sequence1);
        
        // TODO : INVIARE SCRITTA

        captainSetted = true;
    }

    public void setMatch()
    {
        sequence2 = GameObject.FindObjectOfType<StackOrder>().GetOrders();
        bool correct = true;
        for (int i = 0; i < sequence1.Count; i++)
        {
            switch (captainNumber)
            {
                case 3:
                    if (sequence1[i] == sequence2[i])
                        correct = false;
                    break;
                case 4:
                    if (sequence2.Count > 0)
                        correct = false;
                    break;
                default:
                    if (sequence1[i] != sequence2[i])
                        correct = false;
                    break;
            }
        }
    }








}
