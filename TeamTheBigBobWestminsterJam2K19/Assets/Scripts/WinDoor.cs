using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinDoor : MonoBehaviour
{
    [SerializeField] private Sprite openSprite;

    private SpriteRenderer rend;

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Exorcist")
        {
            if (ExorcistPowerupManager.pickupsCollected == 3 && GhostPowerupManager.pickupsCollected == 3)
            {
                SceneManager.LoadScene(3);
                rend.sprite = openSprite;
                Debug.Log("You win. GZ!");

            }
            else
            {
                Debug.Log("You need three powerups for both the exorcist and the ghost");
            }
        }
    }
}
