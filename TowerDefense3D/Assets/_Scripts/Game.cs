using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// start the game and realize the main functional
/// </summary>
public class Game : MonoBehaviour
{
    [SerializeField]
    private Vector2Int _boardSize;

    [SerializeField]
    private GameBoard _board;

    [SerializeField]
    GameTileContentFactory _contentFactory;

    [SerializeField]
    EnemyFactory _enemyFactory;
    [SerializeField, Range(0.1f, 10f)]
    private float _spawnSpeed;

    private float _spawnProgress;

    private EnemyCollection _enemyCollection = new EnemyCollection();

    [SerializeField]
    Camera _camera;

    private Ray TouchRay => _camera.ScreenPointToRay(Input.mousePosition);

    private void Start()
    {
        _board.Initialize(_boardSize, _contentFactory);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleTouche();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            HandleAlternativeTouche();
        }
        _spawnProgress += _spawnSpeed * Time.deltaTime;
        while (_spawnProgress >= 1f)
        {
            _spawnProgress -= 1f;
            SpawnEnemy();
        }
        _enemyCollection.GameUpdate();
    }

    private void SpawnEnemy()
    {
        GameTile spawnPoint = _board.GetSpawnPoint(Random.Range(0, _board.SpawnPointsCount));
        Enemy enemy = _enemyFactory.Get();
        enemy.SpawnOn(spawnPoint);
        _enemyCollection.Add(enemy);
    }

    private void HandleTouche()
    {
        GameTile tile = _board.GetTile(TouchRay);
        if (tile != null)
        {
            _board.ToggleWall(tile);
        }
    }

    private void HandleAlternativeTouche()
    {
        GameTile tile = _board.GetTile(TouchRay);
        if (tile != null)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _board.ToggleDestination(tile);
            }
            else
            {
                _board.ToggleSpawnPoint(tile);
            }
        }
    }
}
