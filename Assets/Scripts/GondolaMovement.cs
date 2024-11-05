using UnityEngine;

public class GondolaMovement : MonoBehaviour {
    [SerializeField]private Transform posA, posB;
    [SerializeField]private float speed;
    
    Vector2 targetPosition;

    private void Start() {
        targetPosition = posA.position;
    }
    // Moves the gondola between two points
    void Update() {
        if (Vector2.Distance(transform.position, posA.position) < 0.1f) {
            targetPosition = posB.position;
        } 
        if (Vector2.Distance(transform.position, posB.position) < 0.1f) {
            targetPosition = posA.position;
        }
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
    // Moves the player with the gondola
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            other.transform.SetParent(transform);
        }
    }
    // Stops moving the player with the gondola
    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            other.transform.SetParent(null);
        }
    }

}
