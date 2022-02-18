using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerDiv : MonoBehaviour
{
    public string id;

    public void DeleteControllerCallback() {
        Debug.Log("Destroying " + id);

        GameObject controller = GameObject.Find("Controller " + id);

        ModelController modelController = controller.GetComponent<ModelController>();
        modelController.DeleteGameObject();

        DestroyGameObject();
    }

    private void DestroyGameObject() {
        Destroy(gameObject);
    }
}
