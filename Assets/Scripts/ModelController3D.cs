using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelController3D : MonoBehaviour
{
    private Animator animator;
    private TCPServer server;

    private Quaternion defaultNeckQuaternion = Quaternion.Euler(0, 90, -90);

    private State state = new State();

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();

        server = GetComponent<TCPServer>();
    }

    // Update is called once per frame
    void Update()
    {   
        if ((server.data != null) && (server.data != "")) {
            state.FromData(server.data);
        }

        animator.GetBoneTransform(HumanBodyBones.Neck).rotation = Quaternion.Euler(state.pitch, state.yaw, state.roll) * defaultNeckQuaternion;
    }
}
