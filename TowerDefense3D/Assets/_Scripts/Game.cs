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
            _board.ToggleDestination(tile);
        }
    }
}
