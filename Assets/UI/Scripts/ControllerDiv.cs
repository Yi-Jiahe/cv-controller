using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControllerDiv : MonoBehaviour
{
    public string id;

    public GameObject modelDiv;

    private int _createdModels = 0;

    private TCPServer _server;

    private TMP_InputField _hostInput;
    private TMP_InputField _portInput;
    private Toggle _serverToggle;

    private RectTransform _root;

    void Start() {
        _root = GameObject.Find("Controllers Div").GetComponent<RectTransform>();

        _server = GameObject.Find("Controller-" + id).GetComponent<TCPServer>();

        Transform addressInputTransform = gameObject.transform.transform.Find("Address Input");
        Transform toggleTransform = addressInputTransform.Find("ServerToggle");
        _serverToggle = toggleTransform.gameObject.GetComponent<Toggle>();
        Transform hostInputTransform = addressInputTransform.Find("HostIp");
        _hostInput = hostInputTransform.gameObject.GetComponent<TMP_InputField>();
        Transform portInputTransform = addressInputTransform.Find("Port");
        _portInput = portInputTransform.gameObject.GetComponent<TMP_InputField>();

        _hostInput.text = _server.hostIp;
        _portInput.text = _server.port.ToString();
    }

    public void DeleteControllerCallback() {
        Debug.Log("Destroying " + id);

        GameObject controller = GameObject.Find("Controller-" + id);

        ModelController modelController = controller.GetComponent<ModelController>();
        modelController.DeleteGameObject();

        DestroyGameObject();
    }

    public void ServerToggleCallback() {
        if (_serverToggle.isOn) {
            _server.hostIp = _hostInput.text;
            _server.port = Int32.Parse(_portInput.text);
            _server.StartServer();
        } else {
            _server.StopServer();
        }
    }

    void OnDestroy() {
        Debug.Log(_root);
        LayoutRebuilder.MarkLayoutForRebuild(_root);
    }

    public void NewModelCallback() {

        string path = EditorUtility.OpenFilePanel("Select file", Application.streamingAssetsPath, "json");
        if (path.Length == 0) {
            return;
        }

        var model = Live2DModelLoader.LoadModel(path);


        string modelId = id + "-" +_createdModels.ToString();

        model.name = "Model-" + modelId;

        GameObject.Find("Controller-" + id).GetComponent<ModelController>().AddLive2DModel(modelId, model);

        GameObject newModelDiv = Instantiate(modelDiv, this.transform);

        Debug.Log(_root);

        LayoutRebuilder.MarkLayoutForRebuild(_root);
        newModelDiv.name = "Model-Div-" + modelId;
        newModelDiv.GetComponent<ModelDiv>().id = modelId;

        _createdModels += 1;
    }

    public void onClick() {
        GameObject.Find("EventSystem").GetComponent<UI>().ActiveModel = GameObject.Find("Controller-" + id);
    }

    private void DestroyGameObject() {
        Destroy(gameObject);
    }
}
