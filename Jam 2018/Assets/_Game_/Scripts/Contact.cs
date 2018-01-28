using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contact : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D cool)
    {
        cool.GetComponent<Animator>().SetTrigger("destroy");
        GameObject.FindObjectOfType<LevelManager>().GameOver();
    }

}
