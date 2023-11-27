using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform TF;
    public Transform playerTF;

    [SerializeField] Vector3 offset;

    private void FixedUpdate()
    {
        if (playerTF != null) {
            TF.position = Vector3.Lerp(TF.position, playerTF.position + offset, Time.fixedDeltaTime * 5f);
        }
    }
}
