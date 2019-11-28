using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step {

    public enum Side { Left = -1, Right = 1, Unknown = 0 }

    public ulong pos;
    public float t;
    public Vector2 localMinPosition;
    public float length;
    public float leftDiff;
    public Side side;
    public Vector2 walkingDirection;

    public Step(ulong pos, float t, Vector2 localMinPosition, float length, float leftDiff, Side side = Side.Unknown)
    {
        this.pos = pos;
        this.t = t;
        this.localMinPosition = localMinPosition;
        this.length = length;
        this.leftDiff = leftDiff;
        this.side = side;
    }
}
