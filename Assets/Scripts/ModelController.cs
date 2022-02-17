using Live2D.Cubism.Core;
using Live2D.Cubism.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelController : MonoBehaviour
{
    private TCPServer server;

    private List<CubismModel> live2Dmodels = new List<CubismModel>();
    private List<Animator> animators = new List<Animator>();

    private State state = new State();

    private float _t;

    private Quaternion defaultNeckQuaternion = Quaternion.Euler(0, 90, -90);

    // Start is called before the first frame update
    void Start()
    {
        server = GetComponent<TCPServer>();

        foreach (Transform transform in this.gameObject.transform){
            CubismModel model = transform.FindCubismModel();
            if (model) {
                live2Dmodels.Add(model);
            } else {
                animators.Add(transform.GetComponentInChildren<Animator>());
            }
        }

        Debug.Log("live2D Models: " + live2Dmodels.Count);
        Debug.Log("Animators:" + animators.Count);
    }

    // Update is called once per frame
    void Update()
    {
        if ((server.data != null) && (server.data != "")) {
            state.FromData(server.data);
        }

        foreach (Animator animator in animators) {
            animator.GetBoneTransform(HumanBodyBones.Neck).rotation = Quaternion.Euler(state.pitch, state.yaw, state.roll) * defaultNeckQuaternion;
        }
    }

    void LateUpdate()
    {
        foreach (CubismModel model in live2Dmodels) {
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
}
