﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public float timeEvent; //il tempo di durata di un evento
    public float timeAnimation; //il tempo di durata dell'animazione
    public float timeAnimationFull; //compreso anche gli ostacoli che scendono
    public int numEventsPerLevel; //numero di eventi per livello
    public int lives;
    public float reduceTime;
    public AudioClip owl;
    public AudioClip dog;
    public AudioClip parrot;
    public AudioClip cat;
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
        if (Time.time <= timerStart + timeAnimationFull && animationMoment)
        {
            
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
            nCaptains = 4;
        if (level > 5)
            level = 5;
        captainNumber = (int)((Random.value * nCaptains) + 1);
        //TODO : RIMETTERE LA RIGA QUI SOPRA ALLA FINE
        //captainNumber = 2;
        if (captainNumber == 2 || captainNumber == 4)
        {
            timeEvent = timeEvent * reduceTime;
        }
        //Debug.Log("NUMERO CAPITANO: "+captainNumber);
        //Debug.Log("TEMPO: "+timeEvent);
        sequenceCaptain = GetComponent<Capitano>().createSequence(level);
        GameObject.FindObjectOfType<Movement>().SetActivate(true);
        GameObject.FindObjectOfType<SignCreate>().Add(sequenceCaptain);
        GameObject.FindObjectOfType<SignCreate>().Enable(true, captainNumber-1);
        GameObject.FindObjectOfType<Bar>().SetTime(timeEvent);
        AudioClip tclip = owl;
        switch (captainNumber)
        {
            case 1:
                tclip = owl;
                break;
            case 2:
                tclip = dog;
                break;
            case 3:
                tclip = parrot;
                break;
            case 4:
                tclip = cat;
                break;
            default:
                tclip = owl;
                break;
        }
        transform.Find("Captain_Sound").GetComponent<AudioSource>().clip = tclip;
        transform.Find("Captain_Sound").GetComponent<AudioSource>().Play();

        List<OrderEnum> sequenceTmp = new List<OrderEnum>(sequenceCaptain);
        if (captainNumber == 3)
        {
            for (int i = 0; i < sequenceTmp.Count; i++)
            {
                if (sequenceCaptain[i] == 0)
                    sequenceTmp[i] = OrderEnum.Right;
                else
                    sequenceTmp[i] = OrderEnum.Left;
            }
        }
        GameObject.FindObjectOfType<StackOrder>().SetOrders(sequenceTmp);

        captainSetted = true;
    }
    
    public void setMatch()
    {
        sequencePlayer = GameObject.FindObjectOfType<StackOrder>().Play(timeAnimation);
        correct = true;
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
        
        //Debug.Log(correct);
        eventMoment = false;
        finalizeEvent = true;
    }

    public void endEvent()
    {
        setMatch();
        if (GameObject.FindObjectOfType<SignCreate>() != null)
            GameObject.FindObjectOfType<SignCreate>().Enable(false, 0);
        //Aggiorna il contatore di eventi
        contEvents++;
        //Verifica il passaggio di livello
        if (contEvents >= numEventsPerLevel)
        {
            level++;
            //aggiungi una persona al pool
            contEvents = 0;
        }
        GameObject.FindObjectOfType<Movement>().SetActivate(false);
        animationMoment = true;
        captainSetted = false;
        match = false;
        finalizeEvent = false;
        if (captainNumber == 2 || captainNumber == 4)
        {
            timeEvent = timeEvent / reduceTime;
        }
        int tmp = 1;
        if (correct)
            tmp = 0;
        if (correct && (captainNumber == 4))
            tmp = 2;
        GameObject.FindObjectOfType<StackOrder>().CreateMine(tmp);
        timerStart = Time.time;
    }

    public int getLevel()
    {
        return level;
    }

    public void GameOver()
    {
        GameObject.Find("submarine").GetComponent<SpriteRenderer>().enabled = false;
        Invoke("_GameOver", 1f);
    }

    private void _GameOver()
    {
        GetComponent<AudioSource>().Play();
        GameObject.Find("UI").transform.FindChild("GameOverPanel").gameObject.active = true;
        if (GameObject.Find("Canvas"))
        {
            GameObject.Find("Canvas").gameObject.active = false;
        }
    }

}
