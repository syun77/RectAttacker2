using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflect : MonoBehaviour {

    // ======================================
    // Variables. Objects.
    public GameObject _horming;

    // ======================================
    // Functions.
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetPosition(float x, float y) {
        transform.position = new Vector3(x, y);
    }

    public void SetVisible(bool b) {
        gameObject.SetActive(b);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Bullet") {
            Vector3 v = new Vector3(transform.position.x, transform.position.y);
            GameObject obj = Instantiate(_horming, v, Quaternion.identity);
            Homing h = obj.GetComponent<Homing>();
            Bullet b = collision.gameObject.GetComponent<Bullet>();
            h.SetDirection(Utils.GetDegree(b.GetComponent<Rigidbody2D>()));
            b.Vanish();
        }
    }
}
