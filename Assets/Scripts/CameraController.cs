using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField] private float speed;
    private float currentPosY = -1.0f;
    private Vector3 velocity = Vector3.zero;
    // Positions Camera to the new room with the help of a trigger
    private void Update(){
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(transform.position.x, currentPosY, transform.position.z), ref velocity, speed);
    }

    public void MovetoNewRoom(Transform _newRoom){
        currentPosY = _newRoom.position.y;
    }
    
}
