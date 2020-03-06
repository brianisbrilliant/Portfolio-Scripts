using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AnimateOnTrigger : MonoBehaviour
{
    [Tooltip("The SteamVR Player prefab")]
    [SerializeField] Transform player;
    [Tooltip("The animator that you want to affect")]
    [SerializeField] Animator anim;
    [Tooltip("The name of the trigger you want to set")]
    [SerializeField] string trigger = "Start";
    [Tooltip("The distance (in meters) you need the player to be before triggering the animation")]
    [Range(1f,55f)]
    [SerializeField] float distance = 5f;
    [Tooltip("The delay (in seconds) before the animation is triggered again")]
    [SerializeField] float delay = 5f;
    [SerializeField] float dist;

    [SerializeField] AudioClip clip;
    private AudioSource aud;
    
    private bool triggered = false;

    void Start() {
        if(player == null) {
            player = GameObject.Find("Player").transform;
        }
        aud = this.GetComponent<AudioSource>();
    }

    void Update() {
        dist = Vector3.Distance(this.transform.position,player.position);
        if(!triggered) {
            if(dist < distance) {
                StartCoroutine(StartAnimation());
            }
        }
    }

    IEnumerator StartAnimation() {
        triggered = true;
        anim.SetTrigger(trigger);
        if(clip != null)
        {
            aud.PlayOneShot(clip);
        }
        yield return new WaitForSeconds(delay);
        triggered = false;
    }

    // void OnTriggerEnter(Collider other) {
    //     if(other.gameObject.name == "Player") {
    //         anim.SetTrigger(trigger);
    //     }
    // }
}
