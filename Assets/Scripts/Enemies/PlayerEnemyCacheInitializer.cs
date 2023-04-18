using UnityEngine;

public class PlayerEnemyCacheInitializer : MonoBehaviour
{
    private PlayerEnemyCache playerEnemyCache;

    private void Awake()
    {
        playerEnemyCache = GetComponent<PlayerEnemyCache>();
    }
}
