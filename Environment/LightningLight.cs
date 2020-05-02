using UnityEngine;

public class LightningLight : MonoBehaviour
{
    [SerializeField] Light lightningLight;
    float timer;
    float timer2;
    int flashCounter;
    bool canStrike = false;

    private void Start()
    {
        lightningLight.enabled = false;
    }

    private void Update()
    {
        if (Random.Range(1f, 100f) > 50 && canStrike == false)
        {
            canStrike = true;
            timer = 5f;
        }
        if (timer >= 5 && canStrike)
        {
            if(timer2 >= 5f && flashCounter == 0)
            {
                lightningLight.intensity = Random.Range(0f, .4f);
                lightningLight.enabled = true;
                flashCounter++;
            }
            if (timer2 >= Random.Range(5.01f, 5.09f) && flashCounter == 1)
            {
                lightningLight.enabled = false;
                flashCounter++;
            }
            if (timer2 >= Random.Range(5.10f, 5.19f) && flashCounter == 2)
            {
                lightningLight.intensity = Random.Range(0f, .4f);
                lightningLight.enabled = true;
                flashCounter++;
            }
            if (timer2 >= Random.Range(5.20f, 5.29f) && flashCounter == 3)
            {
                lightningLight.enabled = false;
                flashCounter++;
            }
            if (timer2 >= Random.Range(5.30f, 5.39f) && flashCounter == 4)
            {
                lightningLight.intensity = Random.Range(0f, .4f);
                lightningLight.enabled = true;
                flashCounter++;
            }
            if (timer2 >= Random.Range(5.40f, 5.49f) && flashCounter == 5)
            {
                lightningLight.enabled = false;
                flashCounter++;
            }
            if (timer2 >= Random.Range(5.50f, 5.59f) && flashCounter == 2)
            {
                lightningLight.intensity = Random.Range(0f, .4f);
                lightningLight.enabled = true;
                flashCounter++;
            }
            if (timer2 >= Random.Range(5.60f, 5.69f) && flashCounter == 6)
            {
                lightningLight.enabled = false;
                flashCounter = 0;
                timer = 0f;
                timer2 = 0f;
                canStrike = false;
            }
        }
        timer += Time.deltaTime;
        timer2 += Time.deltaTime;
    }
}