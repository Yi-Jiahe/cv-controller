using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public GameObject ActiveModel;
    public float mouseSensitivity = 5f;

    private Vector3 _initialMousePosition;
    private Vector3 _initalModelPosition;

    void Update () {
        if (ActiveModel) {
            if (Input.GetMouseButtonDown(0))
            {
                _initialMousePosition = Input.mousePosition;
                _initalModelPosition = ActiveModel.transform.position;
                // Debug.Log(_initialMousePosition);
            }

            if (Input.GetMouseButton(0))
            {
                Resolution currentResolution = Screen.currentResolution;

                ActiveModel.transform.position = _initalModelPosition + Vector3.Scale(Input.mousePosition - _initialMousePosition, new Vector3 (mouseSensitivity/currentResolution.width , mouseSensitivity/currentResolution.height , 0));
            }
        }
    }

    void FixedUpdate()
    {
      


    }
}
