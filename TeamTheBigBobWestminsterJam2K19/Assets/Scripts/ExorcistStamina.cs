using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExorcistStamina : MonoBehaviour
{
    [SerializeField] private Image staminaBar;

    [SerializeField] private float maxStamina, staminaRegen;
    private float stamina;
    public float Stamina
    {
        get { return stamina; }
        set
        {
            stamina = value;
            stamina = Mathf.Clamp(stamina, 0, maxStamina);
            staminaBar.fillAmount = stamina / maxStamina;
        }
    }

    private void Awake()
    {
        stamina = maxStamina;
    }

    private void FixedUpdate()
    {
        Stamina += staminaRegen;
    }
}
