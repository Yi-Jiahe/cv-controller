using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelDiv : MonoBehaviour
{
    public string id;

    public void onClick() {
        GameObject.Find("EventSystem").GetComponent<UI>().ActiveModel = GameObject.Find("Model-" + id);
    }

    public void DeleteModel() {
        string controllerId = id.Split('-')[0];

        GameObject.Find("Controller-" + controllerId).GetComponent<ModelController>().DeleteModel(id);
        Destroy(gameObject);
    }
}
