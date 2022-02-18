using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerDiv : MonoBehaviour
{
    public string id;
    private int _createdModels = 0;

    public void DeleteControllerCallback() {
        Debug.Log("Destroying " + id);

        GameObject controller = GameObject.Find("Controller-" + id);

        ModelController modelController = controller.GetComponent<ModelController>();
        modelController.DeleteGameObject();

        DestroyGameObject();
    }

    public void NewModelCallback() {
        var model = Live2DModelLoader.LoadModel();

        ModelController controller = GameObject.Find("Controller-" + id).GetComponent<ModelController>();
        controller.AddLive2DModel(model);
    }

    private void DestroyGameObject() {
        Destroy(gameObject);
    }
}
