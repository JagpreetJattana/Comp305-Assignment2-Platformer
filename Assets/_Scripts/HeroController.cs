using UnityEngine;
using System.Collections;

//Velocity Range Utility Class
[System.Serializable]
public class VelocityRange
{
    //Public Instance Variables
    public float minimum;
    public float maximum;

    //Constructor
    public VelocityRange(float minimum, float maximum) {
        this.minimum = minimum;
        this.maximum = maximum;
    }

}

public class HeroController : MonoBehaviour {
    //private instance variable
    private Animator _animator;
    private float _move;
   
    private float _jump;
    private bool _facingRight;
    private Transform _transform;
    private Rigidbody2D _rigidBody2d;
    private bool _isGrounded;

    //public instance variable
    public VelocityRange velocityRange;
    public float moveForce;
    public float jumpForce;
    public Transform groundCheck;



    // Use this for initialization
    void Start () {
        //Initialize public instance varibles
        this.velocityRange = new VelocityRange(300f, 25000f);
       // this.moveForce = 50f;
        //this.jumpForce = 500f;
        

        //Initialize private instance variables
        this._transform = gameObject.GetComponent<Transform>();
        this._animator = gameObject.GetComponent<Animator>();
        this._move = 0f;
        this._jump = 0f;
        this._facingRight = true;
        this._rigidBody2d = gameObject.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        this._isGrounded = Physics2D.Linecast(this._transform.position, this.groundCheck.position,1<<LayerMask.NameToLayer("Ground"));
        Debug.DrawLine(this._transform.position, this.groundCheck.position);

     
        float forceX = 0f;
        float forceY = 0f;

        //get absolute value of velocity for our gameobject
        float absVelX = Mathf.Abs(this._rigidBody2d.velocity.x);
        float absVelY = Mathf.Abs(this._rigidBody2d.velocity.y);

       
        //to check that if the player is grounded
        if (this._isGrounded)
        {

            //gets a number between 1 and -1 along horizontal and vertical axis
            this._move = Input.GetAxis("Horizontal");
            this._jump = Input.GetAxis("Vertical");

            if (this._move != 0)
            {
                if (this._move > 0)
                {
                    //movement force here
                    if (absVelX < this.velocityRange.maximum) {
                        forceX = this.moveForce;
                    }
                    this._facingRight = true;
                    this._flip();
                }
                if (this._move < 0)
                {
                    //mmovement force here
                    if (absVelX < this.velocityRange.maximum)
                    {
                        forceX = -this.moveForce;
                    }
                    this._facingRight = false;
                    this._flip();
                }

                // call the walk clip
                this._animator.SetInteger("Anim_State", 1);
            }
            else {

                // set default animation state to "idle"
                this._animator.SetInteger("Anim_State", 0);
            }

            if (this._jump > 0)
            {
                //call the jump force
                // call the "jump" clip
                //  this._animator.SetInteger("Anim_State", 2);
                if (absVelY < this.velocityRange.maximum)
                {
                    forceY = this.jumpForce;
                }
            }

            //applying the forces to the characte
            this._rigidBody2d.AddForce(new Vector2(forceX, forceY));


        }
        else {
          //  Debug.Log("not grounded");
          //call the jump animation
            this._animator.SetInteger("Anim_State", 2);
        }

        //Debug.Log(this._jump);
       

    }

    // PRIVATE METHODS
    private void _flip()
    {
        if (this._facingRight)
        {
            this._transform.localScale = new Vector2(1, 1);
        }
        else {
            this._transform.localScale = new Vector2(-1, 1);
        }
    }

}
