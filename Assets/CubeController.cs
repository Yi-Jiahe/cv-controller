using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    FaceData faceData;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RequestLoop());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator RequestLoop()
    {
        while (true)
        {
            
            FaceData newFaceData = SocketClient.GetData();
            
            Debug.Log(newFaceData);

            if (newFaceData != null) {
                gameObject.transform.position = new Vector3(newFaceData.position.x, newFaceData.position.y, newFaceData.position.z);
                gameObject.transform.rotation = Quaternion.Euler(newFaceData.pose.pitch * Mathf.Rad2Deg, newFaceData.pose.yaw * Mathf.Rad2Deg, newFaceData.pose.roll * Mathf.Rad2Deg);
            }
            
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }
}
