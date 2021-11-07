using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderDamage : MonoBehaviour
{
    [SerializeField] private SpiderEnemyMove spiderEnemyMove;
    [SerializeField] private GameObject bloodObj;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {        
            Vector3 hitPos = other.ClosestPointOnBounds(this.transform.position);
            Instantiate(bloodObj, hitPos, Quaternion.identity);
            spiderEnemyMove.Damage();
            Debug.Log("Damage");
        }
    }
}
