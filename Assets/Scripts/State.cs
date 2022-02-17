using System.Collections;
using System.Collections.Generic;

public class State
{
    
    public float roll;
    public float pitch;
    public float yaw;
    public float ear_left;
    public float ear_right;
    public float iris_ratio_x;
    public float iris_ratio_y;
    public float mar;

    public void FromData(string data) {
        string[] splitData = data.Split(' ');

        roll = float.Parse(splitData[0]);
        pitch = float.Parse(splitData[1]);
        yaw = float.Parse(splitData[2]);
        ear_left = float.Parse(splitData[3]);
        ear_right = float.Parse(splitData[4]);
        iris_ratio_x = float.Parse(splitData[5]);
        iris_ratio_y = float.Parse(splitData[6]);
        mar = float.Parse(splitData[9]);
    }
}
