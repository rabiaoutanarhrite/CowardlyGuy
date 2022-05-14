using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroy : MonoBehaviour
{
    public Transform parent;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && GameManager.instance.enemyIsFlee)
        {
            other.gameObject.GetComponent<PlayerController>().isAttack = true;
            GameManager.instance.liveEnemiesCount--;
            StartCoroutine(DisablePlayerAttack(other));
        }

        if (other.gameObject.CompareTag("Player") && !GameManager.instance.enemyIsFlee)
        {
            parent.GetComponent<EnemyController>().isAttack = true;
            other.gameObject.GetComponent<PlayerController>().health--;
            StartCoroutine(DisableEnemyAttack(other));

        }
    }

    IEnumerator DisablePlayerAttack(Collider other)
    {
        yield return new WaitForSeconds(1f);

        parent.GetComponent<EnemyController>().isLive = false;
        other.gameObject.GetComponent<PlayerController>().isAttack = false;

    }
    IEnumerator DisableEnemyAttack(Collider other)
    {
        yield return new WaitForSeconds(1f);

        parent.GetComponent<EnemyController>().isAttack = false;
    }
}
