using SP.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Health owner = null;
    [SerializeField] Image fill = null;
    public Gradient gradient;

    private void Start()
    {
        GetComponent<Slider>().maxValue = owner.GetMaxHealth();
    }

    private void Update()
    {
        GetComponent<Slider>().value = owner.GetHealth();
        fill.color = gradient.Evaluate(GetComponent<Slider>().normalizedValue);
        if (owner.tag == "Enemy" && owner.GetHealth() == 0f) SeeYa();
    }

    private void SeeYa()
    {
        Destroy(gameObject);
    }

}
