using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public GameObject controller;
    public GameObject controllerDiv;

    private int _createdControllers = 0;

    public void NewControllerCallback() {
        GameObject controllers = GameObject.Find("Controllers");

        GameObject newController = Instantiate(controller, controllers.transform);
        newController.name = "Controller " + _createdControllers;
        TCPServer server = newController.GetComponent<TCPServer>();
        server.port = 5066 + _createdControllers;

        GameObject controllersDiv = GameObject.Find("Controllers Div");
        GameObject newControllerDiv = Instantiate(controllerDiv, controllersDiv.transform);
        newControllerDiv.name = "Controller Div " + _createdControllers;

        _createdControllers += 1;
    }

    public void DeleteControllerCallBack(int controllerId) {
        Debug.Log(this);
    }
}
