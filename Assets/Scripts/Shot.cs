using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour {

    // ==============================================
    // Constatns.
    public float MOVE_SPEED;

    // ==============================================
    // Variables: Objects.
    public GameObject particle;
    Rigidbody2D _rigidbody2D;

    // ==============================================
    // Functions.
    public void SetVelocity(float degree, float speed) {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        Utils.SetVelocity(_rigidbody2D, degree, speed);
    }


	// Use this for initialization
	void Start () {
        _rigidbody2D = GetComponent<Rigidbody2D>();	
	}
	
	// Update is called once per frame
	void Update () {
        if(Utils.OutOfScreen(transform.position)) {
            Vanish();
        }
	}

    public void Vanish() {
        float deg = Utils.GetDegree(_rigidbody2D);
        deg += 180.0f;
        Particle.Add(particle, transform.position.x, transform.position.y, 2, deg, Color.cyan);
        Destroy(gameObject);
    }
}
