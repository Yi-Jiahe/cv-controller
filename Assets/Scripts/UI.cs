using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public GameObject controller;
    public GameObject controllerDiv;
    public void NewController() {
        GameObject controllers = GameObject.Find("Controllers");

        GameObject newController = Instantiate(controller, controllers.transform);

        GameObject controllersDiv = GameObject.Find("Controllers Div");
        GameObject newControllerDiv = Instantiate(controllerDiv, controllersDiv.transform);
    }
}
