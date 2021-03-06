﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    float headCheckRad = .1f;
    float feetCheckRad = .01f;
    [SerializeField] LayerMask steppableObj;

    [SerializeField] private GameObject enemy;
    [SerializeField] private List<GameObject> enemyList;
    [SerializeField] private Vector3 enemySpawnPoint = new Vector3(11f, 1f, -7f);
    [SerializeField] private bool gameOn = false;

    private void Start()
    {

        enemyList = new List<GameObject>();
        StartCoroutine(WaitAndSpawnGroup(3, 3, 3));
    }
    private void Update()
    {
        foreach(GameObject enemy in enemyList)
        {
            if (DeathDetected(enemy))
            {
                KillObj(enemy);
            }
        }
    }

    private IEnumerator WaitAndSpawn(
        int spawnCount,
        int waitSeconds
        )
    {
        for (int i = 0; i < spawnCount; ++i)
        {
            SpawnEnemy(enemy, enemySpawnPoint);
            yield return new WaitForSeconds(waitSeconds);
        }
    }

    private IEnumerator WaitAndSpawnGroup(
        int spawnCount,
        int enemyCountInGroup,
        int waitSeconds
        )
    {
        for (int i = 0; i < spawnCount; ++i)
        {
            for (int j = 0; j < enemyCountInGroup; ++j)
            {
                SpawnEnemy(enemy, enemySpawnPoint);
            }
            yield return new WaitForSeconds(waitSeconds);
        }
    }

    private void SpawnEnemy(GameObject prefab, Vector3 position)
    {
        enemyList.Add(Instantiate(prefab, position, Quaternion.identity));
    }

    private void KillObj(GameObject target)
    {
        Destroy(target.gameObject);
    }

    private bool DeathDetected(GameObject target)
    {
        if (target != null)
        {
            bool isStepping = Physics2D.OverlapCircle(
                target.transform.GetChild(0).position,
                feetCheckRad,
                steppableObj
                );
            bool isStomped = Physics2D.OverlapCircle(
                target.transform.GetChild(1).position,
                headCheckRad,
                steppableObj
                );
            return isStepping && isStomped;
        }
        return false;
    }
}
