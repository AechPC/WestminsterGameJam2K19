using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPowerupManager : MonoBehaviour
{
    public static byte pickupsCollected = 3;

    private void OnLevelWasLoaded(int level)
    {
        pickupsCollected = 3;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "GhostUpgrade")
        {
            switch (pickupsCollected)
            {
                case 0:
                    GetComponent<Collider2D>().isTrigger= true;
                    break;
                case 1:
                    GetComponent<GhostShockwave>().enabled = true;
                    break;
                case 2:
                    GetComponent<GhostShockwave>().upgraded = true;
                    break;
            }

            pickupsCollected++;
            Destroy(other.gameObject);
        }
    }
}
