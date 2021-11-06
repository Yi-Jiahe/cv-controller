using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class FaceData
{
    public Pose pose;
    public Position position;
}

[System.Serializable]
public class Pose
{
    public float roll, pitch, yaw;
}

[System.Serializable]
public class Position
{
    public float x, y, z;
}