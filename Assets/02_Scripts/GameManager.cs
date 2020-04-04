using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    float headCheckRad = .1f;
    float feetCheckRad = .01f;
    [SerializeField] LayerMask steppableObj;

    [SerializeField] private GameObject enemy;
    private List<GameObject> enemyList;
    [SerializeField] private Vector3 enemySpawnPoint = new Vector3(11f, 1f, -7f);

    private void Start()
    {
        enemyList = new List<GameObject>();
        enemyList.Add(SpawnObj(enemy, enemySpawnPoint));
    }
    private void Update()
    {
        foreach (GameObject enemy in enemyList)
        {
            if (DeathDetected(enemy))
            {
                KillObj(enemy);
            }
        }
    }

    private GameObject SpawnObj(GameObject prefab, Vector3 position)
    {
        return Instantiate(prefab, position, Quaternion.identity);
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
