using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class OilBar : MonoBehaviour
{
    [SerializeField] Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }
    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
    }

    public void SetHealth(float health)
    {
        slider.value = health;
    }
}
