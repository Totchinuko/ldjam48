using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constantine;

public class HealthController : MonoBehaviour
{
    //public Collider2D _collider;
    //public EnemyPool _enemyPool;
    //public PawnAI _pawn;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile"))
        {
            Debug.Log("touche");
            //_enemyPool.ReleasePawn(_pawn);
            gameObject.SetActive(false);
        }
    }

}
