using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constantine;

public class EnemySpawn : MonoBehaviour
{

    public EnemyPool _enemyPool;


    private void Awake()
    {
        _enemyPool.Init();
    }

}
