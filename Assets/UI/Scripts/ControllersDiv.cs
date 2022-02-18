using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllersDiv : MonoBehaviour
{
    public GameObject controller;
    public GameObject controllerDiv;

    private int _createdControllers = 0;

    private RectTransform _root;

    void Start() {
        _root = GetComponent<RectTransform>();
    }

    public void NewControllerCallback() {
        GameObject controllers = GameObject.Find("Controllers");

        GameObject newController = Instantiate(controller, controllers.transform);
        newController.name = "Controller-" + _createdControllers;
        TCPServer server = newController.GetComponent<TCPServer>();
        server.port = 5066 + _createdControllers;

        GameObject newControllerDiv = Instantiate(controllerDiv, this.transform);
        Debug.Log(_root);
        LayoutRebuilder.MarkLayoutForRebuild(_root);
    
        newControllerDiv.name = "Controller-Div-" + _createdControllers;
        newControllerDiv.GetComponent<ControllerDiv>().id = _createdControllers.ToString();

        _createdControllers += 1;
    }
}
