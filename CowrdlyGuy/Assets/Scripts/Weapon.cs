using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float rotationSpeed = 1f;
    private float amount;

    // Start is called before the first frame update
    void Start()
    {
        amount = GameManager.instance.weaponAmount;
     }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.instance.totalWeaponsAmount += amount;
            Destroy(this.gameObject);
        }
    }

   
}
