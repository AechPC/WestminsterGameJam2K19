using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressManager : MonoBehaviour
{
    public void SetAbilityGained(bool forGhost, byte abilityNum)
    {
        transform.GetChild(Convert.ToByte(forGhost) * 3 + 1 + abilityNum).GetComponent<Text>().text = "True";
    }
}
