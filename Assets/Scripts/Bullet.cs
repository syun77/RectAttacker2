using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    // ==============================================
    // Variables: Objects.
    public GameObject particle;
    Rigidbody2D _rigidbody2D;

    // ==============================================
    // Functions.
    // Use this for initialization
    public void SetVelocity(float degree, float speed) {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        Utils.SetVelocity(_rigidbody2D, degree, speed);
    }

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
        Particle.Add(particle, transform.position.x, transform.position.y, 4, -1, Color.red);
        Destroy(gameObject);
    }
}
