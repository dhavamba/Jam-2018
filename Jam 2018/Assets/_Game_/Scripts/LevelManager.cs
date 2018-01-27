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
    private List<OrderEnum> sequenceCaptain;
    List<OrderEnum> sequencePlayer;
    private bool finalizeEvent;
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
        sequenceCaptain = new List<OrderEnum>();
        sequencePlayer = new List<OrderEnum>();
        eventMoment = true;
        finalizeEvent = false;
    }

    void Update()
    {
        //Inizializza il gioco
        if (Time.time >= timerStart + startDelay)
        {
            //alla fine riattiva l'evento

        }
        
        //Inizio evento
        if (Time.time <= timerStart + timeEvent && eventMoment)
        {
            
            if (!captainSetted)
            {
                setCaptain();
            }
        }
        else if (eventMoment)
        {
            eventMoment = false;
            finalizeEvent = true;
        }
        if (finalizeEvent)
        {
            endEvent();
        }

        //Inizio animazione
        if (animationMoment)
        {
            //TODO: Fai l'animazione

            //TODO: alla fine riattiva l'evento
            animationMoment = false;
        }
        else if (!eventMoment)
        {
            
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
        
        sequenceCaptain = GetComponent<Capitano>().createSequence(level);
        
        List<OrderEnum> sequence3 = new List<OrderEnum>(sequenceCaptain);

        // TODO : INVIARE SCRITTA

        captainSetted = true;
    }

    public void setMatch()
    {
        sequencePlayer = GameObject.FindObjectOfType<StackOrder>().Play();
        //Debug.Log("SEQUENCE_PLAYER:" + sequencePlayer[0]);
        bool correct = true;
        //Debug.Log("lunghezza player: " + sequencePlayer.Count);
        if (sequencePlayer.Count == sequenceCaptain.Count)
        {
            
            for (int i = 0; i < sequenceCaptain.Count; i++)
            {
                
                switch (captainNumber)
                {
                    case 3:
                        if (sequenceCaptain[i] == sequencePlayer[i])
                            correct = false;
                        break;
                    default:
                        if (sequenceCaptain[i] != sequencePlayer[i])
                            correct = false;
                        break;
                }
            }
        }
        else if ( ((captainNumber == 4) && (sequencePlayer.Count!=0)) || (!(captainNumber == 4)))
        {
            correct = false;
        }
        
        Debug.Log(correct);
        eventMoment = false;
        finalizeEvent = true;
    }

    public void endEvent()
    {
        //if (match)
        //{//Agisci e setta il *correct
        setMatch();
        //}

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
        captainSetted = false;
        match = false;
        finalizeEvent = false;
        timeEvent = timeEvent / reduceTime;
    }





}
