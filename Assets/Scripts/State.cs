using System.Collections;
using System.Collections.Generic;

public class State
{
    
    public float roll;
    public float pitch;
    public float yaw;
    public float ear_left;
    public float ear_right;
    public float mar;

    public void FromData(string data) {
        string[] splitData = data.Split(' ');

        roll = float.Parse(splitData[0]);
        pitch = float.Parse(splitData[1]);
        yaw = float.Parse(splitData[2]);
        ear_left = float.Parse(splitData[3]);
        ear_right = float.Parse(splitData[4]);
        mar = float.Parse(splitData[9]);
    }
}
