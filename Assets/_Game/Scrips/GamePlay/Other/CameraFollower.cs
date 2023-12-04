using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFollower : MonoBehaviour
{
    public Transform TF;
   // public Transform playerTF;

    [SerializeField] Vector3 offset;
    public float speed = 20;

    private void Start()
    {
        TF=FindObjectOfType<PlayerController>()?.transform;
    }
    private void FixedUpdate()
    {
        //if (playerTF != null) {
        //    TF.position = Vector3.Lerp(TF.position, playerTF.position + offset, Time.fixedDeltaTime * 5f);
        //}
        if (TF == null)
        {
            return;
        }
        transform.position = Vector3.Lerp(transform.position, TF.position + offset, Time.deltaTime * speed);
    }
}
