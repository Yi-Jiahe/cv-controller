using Live2D.Cubism.Core;
using Live2D.Cubism.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelControllerLive2D : MonoBehaviour
{
    private CubismModel model;
    private TCPServer server;

    private State state = new State();

    private float _t;

    // Start is called before the first frame update
    void Start()
    {
        model = this.gameObject.transform.GetChild(0).FindCubismModel();
        Debug.Log("Model", model);

        server = GetComponent<TCPServer>();
    }

    // Update is called once per frame
    void Update()
    {
         if ((server.data != null) && (server.data != "")) {
            state.FromData(server.data);
        }
    }

    void LateUpdate()
    {
        // Standard Parameter ID list
        // https://docs.live2d.com/cubism-editor-manual/standard-parametor-list/?locale=en_us

        _t += Time.deltaTime;
        model.Parameters.FindById("ParamBreath").Value = 0.5f + 0.5f * Mathf.Sin(_t * 2f);

        // yaw
        model.Parameters.FindById("ParamAngleX").Value = -Mathf.Clamp(state.yaw, -30, 30);
        // pitch
        model.Parameters.FindById("ParamAngleY").Value = Mathf.Clamp(state.pitch, -30, 30);
        // roll
        model.Parameters.FindById("ParamAngleZ").Value = -Mathf.Clamp(state.roll, -30, 30);

        model.Parameters.FindById("ParamEyeROpen").Value = state.ear_left;
        model.Parameters.FindById("ParamEyeLOpen").Value = state.ear_right;

        model.Parameters.FindById("ParamEyeBallX").Value = state.iris_ratio_x;
        model.Parameters.FindById("ParamEyeBallY").Value = state.iris_ratio_y;

        model.Parameters.FindById("ParamMouthOpenY").Value = state.mar;
    }
}
