using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Shoot : MonoBehaviour
{
    // Instantiate a prefab with an attached Missile script
    public Rigidbody missilePrefab;
    public float bulletSpeed = 100, fireRate = .2f, clipCount = 30, totalAmmo = 100;
    public bool autoFire = false;
    Text textClipCount, textTotalAmmo;
    public AudioClip gunshot, reload, getAmmo, clipEmpty;
    AudioSource aud;

    void Start() {
        textClipCount = GameObject.Find("TxtClipCount").GetComponent<Text>();
        textTotalAmmo = GameObject.Find("TxtTotalAmmo").GetComponent<Text>();
        textClipCount.text = clipCount.ToString();
        textTotalAmmo.text = totalAmmo.ToString();
        aud = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0)) { 
            autoFire = true;
            StartCoroutine(Fire());
        }
        if(Input.GetKeyUp(KeyCode.Mouse0)) {
            autoFire = false;
        }
        if(Input.GetKeyDown(KeyCode.R)) {
            if(totalAmmo >= 30) {
                totalAmmo += clipCount;
                clipCount = 30;
                totalAmmo -= 30;
            } else {
                totalAmmo += clipCount;
                clipCount = totalAmmo;
                totalAmmo = 0;
            }
            textClipCount.text = clipCount.ToString();
            textTotalAmmo.text = totalAmmo.ToString();
            aud.PlayOneShot(reload);
        }
    }

    IEnumerator Fire() {
        if(clipCount > 0) {
            while(autoFire && clipCount > 0) {
                // Instantiate the missile at the position and rotation of this object's transform
                Rigidbody clone = (Rigidbody)Instantiate(missilePrefab, transform.position, transform.rotation);
                clone.gameObject.transform.Translate(0,0,1);
                clone.AddRelativeForce(Vector3.forward * bulletSpeed * 10);
                clipCount--;
                textClipCount.text = clipCount.ToString();
                aud.PlayOneShot(gunshot);
                print("Pow!");
                yield return new WaitForSeconds(fireRate);
            }
        } else {
            aud.PlayOneShot(clipEmpty);
        }
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "AmmoPickup") {
            totalAmmo += 60;
            textTotalAmmo.text = totalAmmo.ToString();
            aud.PlayOneShot(getAmmo);
            print("You've picked up more ammo!");
        }
    }
}
