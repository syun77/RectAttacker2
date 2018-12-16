using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour {

    // ===============================
    // Static Functions.
    public static void Add(GameObject obj, float x, float y, int count, float degree, Color color) {
        Vector3 v = new Vector3(x, y);
        for (int i = 0; i < count; i++) {
            GameObject o = Instantiate(obj, v, Quaternion.identity);
            Particle p = o.GetComponent<Particle>();
            p.SetDegree(degree);
            p.SetColor(color);
        }
    }

    // ===============================
    // Variables: Objects.
    Rigidbody2D _rigidbody2D;

    // ===============================
    // Variables.
    float _life;
    float _degree = 0;
    Color _color = Color.white;

    // ===============================
    // Functions.

    void SetDegree(float degree) {
        _degree = degree;
    }
    void SetColor(Color color) {
        _color = color;
    }

    // Use this for initialization
    void Start () {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        float degree = _degree + Random.Range(-30, 30);
        if(_degree <= 0) {
            degree = Random.Range(75, 135);
        }
        float speed = Random.Range(1, 3);
        Utils.SetVelocity(_rigidbody2D, degree, speed);

        _life = 1;

        SpriteRenderer render = GetComponent<SpriteRenderer>();
        render.color = _color;
	}
	
	// Update is called once per frame
	void Update () {
        _life -= Time.deltaTime;
        if(_life < 0) {
            Destroy(gameObject);
        }
	}

    private void FixedUpdate() {

        float friction = 0.95f;
        _rigidbody2D.velocity *= friction;

        if(_life < 0.25f) {
            SpriteRenderer render = GetComponent<SpriteRenderer>();
            if(render.enabled) {
                render.enabled = false;
            }
            else {
                render.enabled = true;
            }
        }
    }
}
