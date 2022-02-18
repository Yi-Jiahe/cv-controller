using Live2D.Cubism.Core;
using Live2D.Cubism.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelController : MonoBehaviour
{
    private class _Model {
        public enum ModelType
        {
            Live2D,
            ThreeD,
        }

        public ModelType modelType;
        
        public CubismModel cubismModel;
        public Animator animator;
    }

    private TCPServer server;

    private Dictionary<string, _Model> models = new Dictionary<string, _Model>();

    private State state = new State();

    public float ear_min = 0.1f;
    public float ear_max = 0.6f;
    public float iris_ratio_x_min = 0.3333f;
    public float iris_ratio_x_max = 0.6667f;
    public float iris_ratio_y_min = 0.4f;
    public float iris_ratio_y_max = 0.6f;
    public float mar_min = 0f;
    public float mar_max = 0.8f;


    private float _t;

    public float ParamEyeOpen_min = 0f;
    public float ParamEyeOpen_max = 1.2f;
    public float ParamEyeBallX_min = -1f;
    public float ParamEyeBallX_max = 1f;
    public float ParamEyeBallY_min = -1f;
    public float ParamEyeBallY_max = 1f;
    public float ParamMouthOpenY_min = 0f;
    public float ParamMouthOpenY_max = 1f;


    private Quaternion defaultNeckQuaternion = Quaternion.Euler(0, 90, -90);

    // Start is called before the first frame update
    void Start()
    {
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
        _t += Time.deltaTime;
        foreach (KeyValuePair<string, _Model> modelMapping in models) {
            _Model model = modelMapping.Value;
            switch (model.modelType) {
                case _Model.ModelType.Live2D:
                    // Standard Parameter ID list
                    // https://docs.live2d.com/cubism-editor-manual/standard-parametor-list/?locale=en_us

                    model.cubismModel.Parameters.FindById("ParamBreath").Value = 0.5f + 0.5f * Mathf.Sin(_t * 2f);

                    // yaw
                    model.cubismModel.Parameters.FindById("ParamAngleX").Value = -Mathf.Clamp(state.yaw, -30, 30);
                    // pitch
                    model.cubismModel.Parameters.FindById("ParamAngleY").Value = Mathf.Clamp(state.pitch, -30, 30);
                    // roll
                    model.cubismModel.Parameters.FindById("ParamAngleZ").Value = -Mathf.Clamp(state.roll, -30, 30);

                    model.cubismModel.Parameters.FindById("ParamEyeROpen").Value = ClampAndScale(state.ear_left, ear_min, ear_max, ParamEyeOpen_min, ParamEyeOpen_max);
                    model.cubismModel.Parameters.FindById("ParamEyeLOpen").Value = ClampAndScale(state.ear_right, ear_min, ear_max, ParamEyeOpen_min, ParamEyeOpen_max);

                    model.cubismModel.Parameters.FindById("ParamEyeBallX").Value = ClampAndScale(state.iris_ratio_x, iris_ratio_x_min, iris_ratio_x_max, ParamEyeBallX_min, ParamEyeBallX_max);
                    model.cubismModel.Parameters.FindById("ParamEyeBallY").Value = ClampAndScale(state.iris_ratio_y, iris_ratio_y_min, iris_ratio_y_max, ParamEyeBallY_min, ParamEyeBallY_max);

                    model.cubismModel.Parameters.FindById("ParamMouthOpenY").Value = ClampAndScale(state.mar, mar_min, mar_max, ParamMouthOpenY_min, ParamMouthOpenY_max);

                    break;
                case _Model.ModelType.ThreeD:
                    model.animator.GetBoneTransform(HumanBodyBones.Neck).rotation = Quaternion.Euler(state.pitch, state.yaw, state.roll) * defaultNeckQuaternion;

                    break;
            }
        }
    }

    public void AddLive2DModel(string id, CubismModel cubismModel) {
        cubismModel.transform.parent = gameObject.transform;
        cubismModel.transform.localPosition = new Vector3(0, 0, 0);

        _Model newModel = new _Model();
        newModel.modelType = _Model.ModelType.Live2D;
        newModel.cubismModel = cubismModel;
        models.Add(id, newModel);
    }


    public void DeleteModel(string id) {
        models.Remove(id);
        Destroy(GameObject.Find("Model-" + id));
    }

    public void DeleteGameObject() {
        Destroy(gameObject);
    }

    private float ClampAndScale(float input, float minIn, float maxIn, float minOut, float maxOut) {
        float clamped = Mathf.Clamp(input, minIn, maxIn);
        float rangeIn = maxIn - minIn;
        float rangeOut = maxOut - minOut;
        float scale = rangeOut/rangeIn;
        return (clamped - minIn + minOut) * scale;
    }
}
