using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointScript : MonoBehaviour {
    private BoxCollider2D checkpointCollider;
    [SerializeField]private  AudioClip checkpointSound;

    void Awake() {
        checkpointCollider = GetComponent<BoxCollider2D>();
    }
    // Sets the respawn point to the current checkpoint
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            other.gameObject.GetComponent<PlayerRespawn>().respawnPoint = this.gameObject;;
            checkpointCollider.enabled = false;
            SoundManager.instance.PlaySound(checkpointSound);
        }
    }
}
