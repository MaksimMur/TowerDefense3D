using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyFactory OriginFactory { get; set; }

    private GameTile _tileFrom, _tileTo;
    private Vector3 _positionFrom, _positionTo;
    private float _prorgess;

    public void SpawnOn(GameTile tile)
    {
        this.transform.localPosition = tile.transform.localPosition;
        _tileFrom = tile;
        _tileTo = tile.NextTileOnPath;
        _positionFrom = _tileFrom.transform.localPosition;
        _positionTo = _tileTo.ExitPoint;
        transform.localRotation = _tileFrom.PathDirection.GetRotation();
        _prorgess = 0;
    }

    public bool GameUpdate()
    {
        _prorgess += Time.deltaTime;
        while (_prorgess >= 1f)
        {
            _tileFrom = _tileTo;
            _tileTo = _tileTo.NextTileOnPath;
            if (_tileTo == null)
            {
                OriginFactory.Reclaim(this);
                return false;
            }
            _positionFrom = _positionTo;
            _positionTo = _tileTo.ExitPoint;
            transform.localRotation = _tileFrom.PathDirection.GetRotation();
            _prorgess -= 1f;
        }
        transform.localPosition = Vector3.LerpUnclamped(_positionFrom, _positionTo, _prorgess);
        return true;
    }
}

