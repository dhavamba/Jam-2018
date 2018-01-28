using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTerrain : MonoBehaviour
{
    public GameObject[] gameobjects;

    private float time;
    private float initTime;
    public float range;

    private void Awake()
    {
        time = UnityEngine.Random.Range(1, 2);
        Invoke("Random", time);
    }

    public void Random()
    {

        for (int i = 0; i < transform.childCount; i++)
        {
            int index = UnityEngine.Random.Range(0, gameobjects.Length);
            Vector2 t = transform.GetChild(i).position;
            t = new Vector2(t.x + UnityEngine.Random.Range(-range, range), t.y + UnityEngine.Random.Range(-range, range));
            Transform obj = GameObject.Instantiate(gameobjects[index], t, Quaternion.identity).transform;
            float newScale = UnityEngine.Random.Range(0, 2);
            obj.localScale = new Vector3(obj.localScale.x + newScale, obj.localScale.y + newScale, 1);

        }

        time = UnityEngine.Random.Range(3, 4);
        Invoke("Random", time);
    }
}
