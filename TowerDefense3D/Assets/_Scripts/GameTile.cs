using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTile : MonoBehaviour
{
    [SerializeField]
    private Transform _arrow;

    //neighbors of tiles
    // _nextOnPath help to enemies find the tile, that enemies dont constantly find path
    private GameTile _north, _east, _south, _west, _nextOnPath;

    //keep amount of tiles to needed place of assigment
    private int _distance;

    public bool HasPath => _distance != int.MaxValue;
    public bool IsAlternative {get;set;}



    private Quaternion _northRotation = Quaternion.Euler(90f, 0f, 0f);
    private Quaternion _eastRotation = Quaternion.Euler(90f,90f, 0f);
    private Quaternion _southRotation = Quaternion.Euler(90f, 180f, 0f);
    private Quaternion _westRotation = Quaternion.Euler(90f, 270f, 0f);

    public static void MakeEastWestNeighbors(GameTile east, GameTile west)
    {
        west._east = east;
        east._west = west;
    }

    public static void MakeNorthSouthNeighbors(GameTile north, GameTile south)
    {
        north._south = south;
        south._north = north;
    }

    /// <summary>
    /// make a realoding of condition of tile
    /// </summary>
    public void ClearPath()
    {
        _distance = int.MaxValue;
        _nextOnPath = null;
    }

    /// <summary>
    /// mark tile as place of assigment
    /// </summary>
    public void BecomeDestination()
    {
        _distance = 0;
        _nextOnPath = null;
    }

    /// <summary>
    /// called for tiles that have own distance (distance !=int.MaxValue)
    /// </summary>
    /// <param name="neighbor"></param>
    /// <returns></returns>
    private GameTile GrowPathTo(GameTile neighbor)
    {
        if (!HasPath || neighbor == null || neighbor.HasPath)
        {
            return null;
        }

        neighbor._distance = _distance + 1;
        neighbor._nextOnPath = this;
        return neighbor;
    }

    public GameTile GrowPathNorth() => GrowPathTo(_north);
    public GameTile GrowPathEast() => GrowPathTo(_east);
    public GameTile GrowPathSouth() => GrowPathTo(_south);
    public GameTile GrowPathWest() => GrowPathTo(_west);

    public void ShowPath()
    {
        if (_distance == 0)
        {
            _arrow.gameObject.SetActive(false);
            return;
        }
        _arrow.gameObject.SetActive(true);
        _arrow.localRotation =
            _nextOnPath == _north ? _northRotation :
            _nextOnPath == _east ? _eastRotation :
            _nextOnPath == _south ? _southRotation :
            _westRotation;
        print(_arrow.eulerAngles);
    }
}
