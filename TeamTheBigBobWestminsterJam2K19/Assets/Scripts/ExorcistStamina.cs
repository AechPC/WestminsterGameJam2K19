using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExorcistStamina : MonoBehaviour
{
    [SerializeField] private Image staminaBar;

    [SerializeField] private float maxStamina;
    private float stamina;
    public float Stamina
    {
        get { return stamina; }
        set
        {
            stamina = value;

            staminaBar.fillAmount = stamina / maxStamina;
        }
    }

    private void Awake()
    {
        stamina = maxStamina;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
