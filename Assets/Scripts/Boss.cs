using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

    // ==============================================
    // Variables: Objects.
    public GameObject player;
    public GameObject bullet;
    public GameObject particle;

    // ==============================================
    // Variables.
    int interval = 0;

    // ==============================================
    // Functions.
    // Use this for initialization
    void Start () {
    }

    void DoBullet(float degree, float speed) {
        Vector3 v = new Vector3(transform.position.x, transform.position.y);
        GameObject obj = Instantiate(bullet, v, Quaternion.identity);
        Bullet b = obj.GetComponent<Bullet>();
        b.SetVelocity(degree, speed);
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void FixedUpdate() {
        interval++;
        DoBullet(interval * 3, 5);
        DoBullet(interval * -3, 5);
        DoBullet(interval * 1, 5);
        DoBullet(interval * -1, 5);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        switch (collision.tag) {
        case "Shot": {
                Shot shot = collision.gameObject.GetComponent<Shot>();
                shot.Vanish();
            }
            break;

        case "Horming": {
                Homing homing = collision.gameObject.GetComponent<Homing>();
                homing.Vanish();
            }
            break;
        }
    }
}
