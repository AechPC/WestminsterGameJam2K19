using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Progress : MonoBehaviour
{
    [SerializeField] private Text[] abilities = new Text[6];

    public void GainAbility(int ability) //abilities 1-3 are exorcists abilities chronologically, 4-6 are ghosts abilities chronologically.
    {
        if (abilities[ability - 1].text == "False")
        {
            abilities[ability - 1].text = "True";
        }
    }
}