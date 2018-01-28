using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class Bar : MonoBehaviour
{
    private RectTransform rect;
    private float maxSixe;

    private float time;
    private float oldTime;

    protected virtual void Awake()
    {
        rect = GetComponent<RectTransform>();
        maxSixe = rect.sizeDelta.x;
        oldTime = time;
    }

    private void Update()
    {
        if (time != 0)
        {
            if (oldTime > 0)
            {
                SetBar(oldTime / time);
                oldTime -= Time.deltaTime;
            }
            else
            {
                FinishTime();
            }
        }
    }

    public void SetTime(float t)
    {
        time = t;
        oldTime = time;
    }

    private void FinishTime()
    {
        SetBar(0);
        time = 0;
    }

    protected void SetBar(float actualValue)
    {
        GetComponent<Image>().fillAmount = actualValue;
    }
}
