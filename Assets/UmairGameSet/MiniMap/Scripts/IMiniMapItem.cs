using UnityEngine;

public interface IMiniMapItem
{
    Sprite Icon
    {
        get;
    }
    Color IconColor
    {
        get;
    }
    Vector3 Position
    {
        get;
    }
    float RotationZ
    {
        get;
    }
    bool isBounded
    {
        get;
    }
    void Subscribe ();
}
