using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu]
public class EnemyFactory : GameObjectFactory
{
    [SerializeField]
    private Enemy _enemyPrefab;

    public Enemy Get()
    {
        Enemy instance = CreateGameObjectInstance(_enemyPrefab);
        instance.OriginFactory = this;
        return instance;
    }

    public void Reclaim(Enemy enemy)
    {
        Destroy(enemy.gameObject);
    }
}
