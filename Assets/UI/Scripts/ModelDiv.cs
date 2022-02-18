using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelDiv : MonoBehaviour
{
    public string id;

    void OnDestroy() {
        LayoutRebuilder.MarkLayoutForRebuild(GameObject.Find("Controllers Div").transform as RectTransform);
    }

    public void onClick() {
        GameObject.Find("EventSystem").GetComponent<UI>().ActiveModel = GameObject.Find("Model-" + id);
    }

    public void DeleteModel() {
        string controllerId = id.Split('-')[0];

        GameObject.Find("Controller-" + controllerId).GetComponent<ModelController>().DeleteModel(id);

        Destroy(gameObject);
    }
}
