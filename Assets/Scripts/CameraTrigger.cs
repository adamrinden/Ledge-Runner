using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {
    [SerializeField] private Transform nextRoom;
    [SerializeField ] private CameraController cam;
    // A trigger that moves the camera to the next room
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            cam.MovetoNewRoom(nextRoom);
        }
    }
}