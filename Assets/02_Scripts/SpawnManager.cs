using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public void SpawnObj(GameObject prefab, Vector2 position)
    {
        Instantiate(prefab, position, Quaternion.identity);
    }
}
