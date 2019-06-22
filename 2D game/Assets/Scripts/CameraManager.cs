using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {
    public Camera mainCamera;
    public GameObject player;
    public Vector3 offset;

    void LateUpdate() {
        mainCamera.transform.position = player.transform.position - offset;
    }
}
