using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour {
    public GameObject respawnPoint;
    [Header("Sound Settings")]
    [SerializeField]private  AudioClip deathSound;
    // Respawns the player at the respawn point
    public  void Respawn() {
        transform.position = respawnPoint.transform.position;
        SoundManager.instance.PlaySound(deathSound);
    }
}
