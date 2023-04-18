using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class DirectionExtensions
{
    private static Quaternion[] _rotations =
    {
        Quaternion.identity,
        Quaternion.Euler(0f,90f,0f),
        Quaternion.Euler(0f,180f,0f),
        Quaternion.Euler(0f,270,0f)
    };

    public static Quaternion GetRotation(this Direction direction)
    {
        return _rotations[(int)direction];
    }
}
public enum Direction
{
    North,
    East,
    South,
    West
}