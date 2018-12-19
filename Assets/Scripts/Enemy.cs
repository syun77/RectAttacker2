using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    // ==============================================
    // Constants.
    public enum eId {
        Zako,
    };

    // ==============================================
    // Variables: Objects.
    public GameObject bullet;
    public GameObject particle;
    Rigidbody2D _rigidbody2D;
    Player _player;

    // ==============================================
    // Variables.
    eId id = eId.Zako;
    int interval = 0;

    // ==============================================
    public void Init(eId id, float degree, float speed, Player player) {

        this.id = id;
        _player = player;

        _rigidbody2D = GetComponent<Rigidbody2D>();
        Utils.SetVelocity(_rigidbody2D, degree, speed);

        transform.localScale = new Vector3(0.5f, 0.5f, 1);

        Destroy(gameObject, 3);
    }

    // Use this for initialization
    void Start () {
        _rigidbody2D = GetComponent<Rigidbody2D>();
	}

    void DoBullet(float degree, float speed) {
        Vector3 v = new Vector3(transform.position.x, transform.position.y);
        GameObject obj = Instantiate(bullet, v, Quaternion.identity);
        Bullet b = obj.GetComponent<Bullet>();
        b.SetVelocity(degree, speed);
    }
    void DoBulletAim(float speed) {
        float aim = Utils.GetAim2D(this.gameObject, _player.gameObject);
        DoBullet(aim, speed);
    }

	// Update is called once per frame
	void Update () {
	}

    private void FixedUpdate() {
        interval++;
        if(interval%10 == 1) {
            DoBulletAim(5);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        switch(collision.tag) {
        case "Shot":
            {
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
