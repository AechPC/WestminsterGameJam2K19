using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExorcistGrenade : MonoBehaviour
{
    [SerializeField] private GameObject grenade;

    [SerializeField] private Transform grenadeSpawnPos;

    [SerializeField] private float throwAngle, throwDis;

    private Vector2 throwDir;

    [SerializeField] private int grenadeCount;

    private void Awake()
    {
        throwDir = new Vector2(Mathf.Cos(throwAngle * Mathf.Deg2Rad), Mathf.Sin(throwAngle * Mathf.Deg2Rad));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && grenadeCount > 0)
        {
            grenadeCount--;
            Rigidbody2D rb = Instantiate(grenade, grenadeSpawnPos.position, Quaternion.identity).GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(transform.rotation.eulerAngles.y < 90 ? throwDir.x : -throwDir.x, throwDir.y) * throwDis);
        }
    }

    public void GetGrenades(int grenades)
    {
        grenadeCount += grenades;
    }
}
