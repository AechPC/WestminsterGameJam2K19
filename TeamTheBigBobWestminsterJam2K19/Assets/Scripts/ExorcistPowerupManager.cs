using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExorcistPowerupManager : MonoBehaviour
{
    public static byte pickupsCollected = 0;

    private ProgressManager progressManager;

    private void Start()
    {
        progressManager = GameObject.FindWithTag("ProgressManager").GetComponent<ProgressManager>();
    }

    private void OnLevelWasLoaded(int level)
    {
        pickupsCollected = 0;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "ExorcistUpgrade")
        {
            switch (pickupsCollected)
            {
                case 0:
                    GetComponent<ExorcistShockwave>().enabled = true;
                    break;
                case 1:
                    GetComponent<BulletTime>().enabled = true;
                    break;
                case 2:
                    GetComponent<ExorcistShield>().enabled = true;
                    break;
            }

            progressManager.SetAbilityGained(false, pickupsCollected);
            pickupsCollected++;
            Destroy(other.gameObject);
        }
    }
}