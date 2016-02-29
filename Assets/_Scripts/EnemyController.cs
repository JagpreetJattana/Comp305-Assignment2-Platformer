using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    // PRIVATE INSTANCE VARIABLES
    private Transform _transform;
    private Vector3 _currentPosition;


    // Use this for initialization
    void Start()
    {
        this._transform = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        this._transform.Rotate(0, 0, 3f);
        this._currentPosition = this._transform.position;
        this._currentPosition += new Vector3(-2,4,0);
        this._transform.position = this._currentPosition;

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Death"))
        {

            this._restart();
            
        }
    }

    //Private methods
    private void _restart() {
        this._transform.position = new Vector3(840f,330f,0);

    }
    }
