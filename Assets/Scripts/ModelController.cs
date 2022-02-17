using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelController : MonoBehaviour
{
    private Animator animator;
    private TCPServer server;

    private Quaternion defaultNeckQuaternion = Quaternion.Euler(0, 90, -90);

    private State state = new State();

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        Debug.Log("Animator", animator);
        server = GetComponent<TCPServer>();
        Debug.Log("Server", server);
    }

    // Update is called once per frame
    void Update()
    {   
        if ((server.data != null) && (server.data != "")) {
            Debug.Log(server.data);
            state.FromData(server.data);

            animator.GetBoneTransform(HumanBodyBones.Neck).rotation = Quaternion.Euler(state.pitch, state.yaw, state.roll) * defaultNeckQuaternion;


        }
    }
}
