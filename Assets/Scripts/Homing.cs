using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homing : MonoBehaviour {

    // ==============================================
    // Variables: Objects.
    public GameObject boss;
    public GameObject particle;
    Rigidbody2D _rigidbody2D;

    // ==============================================
    // Vartiables.
    float direction = 0;
    float speed = 10;
    float rotSpeed = 0.01f;

    // ==============================================
    // Functions.
    // Use this for initialization
    void Start () {
        _rigidbody2D = GetComponent<Rigidbody2D>();

        direction = Random.Range(-120, -30);
    }

    public void SetDirection(float d) {
        direction = d;
        direction += Random.Range(-20, 20);
    }

    public void SetVelocity(float degree, float speed) {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        Utils.SetVelocity(_rigidbody2D, degree, speed);
    }

    // Update is called once per frame
    void Update () {
	}

    private void FixedUpdate() {

        // Turn for homing.
        float aim = Utils.GetAim2D(gameObject, boss);
        float d = Mathf.DeltaAngle(direction, aim);
        d *= rotSpeed;
        rotSpeed += 0.01f;
        if(rotSpeed > 1) {
            rotSpeed = 1;
        }
        speed += 0.1f;
        if(speed > 30) {
            speed = 30;
        }
        direction += d;
        Utils.SetVelocity(_rigidbody2D, direction, speed);
    }

    public void Vanish() {
        float deg = Utils.GetDegree(_rigidbody2D);
        deg += 180.0f;
        Particle.Add(particle, transform.position.x, transform.position.y, 4, deg, Color.cyan);
        Destroy(gameObject);
    }
}
