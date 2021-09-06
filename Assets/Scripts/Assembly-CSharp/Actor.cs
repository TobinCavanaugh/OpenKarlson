using UnityEngine;

public abstract class Actor : MonoBehaviour
{
	public Transform gunPosition;

    	public Transform orientation;
	
    	private float xRotation;
	
    	private Rigidbody rb;
	
    	private float accelerationSpeed = 4500f;
	
    	private float maxSpeed = 20f;
	
    	private bool crouching;
	
    	private bool jumping;
	
    	private bool wallRunning;
	
    	protected float x;
	
    	protected float y;
	
    	private Vector3 wallNormalVector = Vector3.up;
	
    	private bool grounded;
	
    	public Transform groundChecker;
	
    	public LayerMask whatIsGround;
	
    	private bool readyToJump;
	
    	private float jumpCooldown = 0.2f;
	
    	private float jumpForce = 500f;
	
    	protected Actor()
    	{
    	}
	
    	private void Awake()
    	{
    	    this.rb = base.GetComponent<Rigidbody>();
    	    this.OnStart();
    	}
	
    	protected void CounterMovement(float x, float y, Vector2 mag)
    	{
    	    if (!this.grounded || this.crouching)
    	    {
    	        return;
    	    }
    	    float single = 0.2f;
    	    if (x == 0f || mag.x < 0f && x > 0f || mag.x > 0f && x < 0f)
    	    {
    	        this.rb.AddForce(this.accelerationSpeed * single * Time.deltaTime * -mag.x * this.orientation.transform.right);
    	    }
    	    if (y == 0f || mag.y < 0f && y > 0f || mag.y > 0f && y < 0f)
    	    {
    	        this.rb.AddForce(this.accelerationSpeed * single * Time.deltaTime * -mag.y * this.orientation.transform.forward);
    	    }
    	    if (Mathf.Sqrt(Mathf.Pow(this.rb.velocity.x, 2f) + Mathf.Pow(this.rb.velocity.z, 2f)) > 20f)
    	    {
    	        float single1 = this.rb.velocity.y;
    	        Vector3 vector3 = this.rb.velocity.normalized * 20f;
    	        this.rb.velocity = new Vector3(vector3.x, single1, vector3.z);
    	    }
    	}
	
    	protected Vector2 FindVelRelativeToLook()
    	{
    	    float single = this.orientation.transform.eulerAngles.y;
    	    Vector3 vector3 = this.rb.velocity;
    	    float single1 = Mathf.Atan2(vector3.x, vector3.z) * 57.29578f;
    	    float single2 = Mathf.DeltaAngle(single, single1);
    	    float single3 = 90f - single2;
    	    float single4 = this.rb.velocity.magnitude;
    	    float single5 = single4 * Mathf.Cos(single2 * 0.0174532924f);
    	    return new Vector2(single4 * Mathf.Cos(single3 * 0.0174532924f), single5);
    	}
	
    	private void FixedUpdate()
    	{
    	    this.Movement();
    	    this.RotateBody();
    	}
	
    	private void Jump()
    	{
    	    if (this.grounded || this.wallRunning)
    	    {
    	        Vector3 vector3 = this.rb.velocity;
    	        this.rb.velocity = new Vector3(vector3.x, 0f, vector3.z);
    	        this.readyToJump = false;
    	        this.rb.AddForce((Vector2.up * this.jumpForce) * 1.5f);
    	        this.rb.AddForce((this.wallNormalVector * this.jumpForce) * 0.5f);
    	        base.Invoke("ResetJump", this.jumpCooldown);
    	        if (this.wallRunning)
    	        {
    	            this.wallRunning = false;
    	        }
    	    }
    	}
	
    	private void LateUpdate()
    	{
    	    this.Look();
    	}
	
    	protected abstract void Logic();
	
    	protected abstract void Look();
	
    	private void Movement()
    	{
    	    this.grounded = Physics.OverlapSphere(this.groundChecker.position, 0.1f, this.whatIsGround).Length != 0;
    	    Vector2 look = this.FindVelRelativeToLook();
    	    float single = look.x;
    	    float single1 = look.y;
    	    this.CounterMovement(this.x, this.y, look);
    	    if (this.readyToJump && this.jumping)
    	    {
    	        this.Jump();
    	    }
    	    if (this.crouching && this.grounded && this.readyToJump)
    	    {
    	        this.rb.AddForce((Vector3.down * Time.deltaTime) * 2000f);
    	        return;
    	    }
    	    if (this.x > 0f && single > this.maxSpeed)
    	    {
    	        this.x = 0f;
    	    }
    	    if (this.x < 0f && single < -this.maxSpeed)
    	    {
    	        this.x = 0f;
    	    }
    	    if (this.y > 0f && single1 > this.maxSpeed)
    	    {
    	        this.y = 0f;
    	    }
    	    if (this.y < 0f && single1 < -this.maxSpeed)
    	    {
    	        this.y = 0f;
    	    }
    	    this.rb.AddForce(Time.deltaTime * this.y * this.accelerationSpeed * this.orientation.transform.forward);
    	    this.rb.AddForce(Time.deltaTime * this.x * this.accelerationSpeed * this.orientation.transform.right);
    	}
	
    	protected abstract void OnStart();
	
    	private void ResetJump()
    	{
    	    this.readyToJump = true;
    	}
	
    	protected abstract void RotateBody();
	
    	private void Update()
    	{
    	    this.Logic();
    	}
}
