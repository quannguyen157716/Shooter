/// <summary>
/// 2D Space Shooter Example
/// By Bug Games www.Bug-Games.net
/// Programmer: Danar Kayfi - Twitter: @DanarKayfi
/// Special Thanks to Kenney for the CC0 Graphic Assets: www.kenney.nl
/// 
/// This is the Player Ship Script:
/// - Player Ship Movement
/// - Fire Control
/// - Screen Boundary Control
/// - Explosion/Game Over Trigger
/// 
/// </summary>

using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary 
{
	public float xMin, xMax, yMin, yMax; //Screen Boundary dimentions
}

public class Player_Script : MonoBehaviour 
{
	//Public Var
	public float speed; 			//Player Ship Speed
	public Boundary boundary; 		//make an Object from Class Boundary
	GameObject shot;			//Fire Prefab
	public Transform shotSpawn;		//Where the Fire Spawn
	public float fireRate = 0.5F;	//Fire Rate between Shots
	public GameObject Explosion;	//Explosion Prefab

	//Private Var
	private float nextFire = 0.0F;	//First fire & Next fire Time
	Vector3 direction;
	Vector3 touchPosition;
	Vector3 lastTouchPosition;
	Rigidbody2D rigidbody2;
	AudioSource audio2;
	// Update is called once per frame
	void Start()
	{
		rigidbody2=GetComponent<Rigidbody2D>();
		audio2=GetComponent<AudioSource>();
	}
	void Update () 
	{
		Fire2();
	}

	void Fire()
	{
		//Excute When the Current Time is bigger than the nextFire time
		if (Time.time > nextFire) 
		{
			nextFire = Time.time + fireRate; 								//Increment nextFire time with the current system time + fireRate
			Instantiate (shot , shotSpawn.position ,shotSpawn.rotation); 	//Instantiate fire shot 
			audio2.Play (); 													//Play Fire sound
		}
	}

	void Fire2()
	{
		if(Time.time >nextFire)
		{
			nextFire = Time.time + fireRate;//fire after ''firerate'' time from the time of last frame
			shot = BulletPooler.SharedBulletPool.GetPooledObject("PlayerLaser"); 
  			if (shot != null) 
			{
   			shot.transform.position = shotSpawn.transform.position;
    		shot.transform.rotation = shotSpawn.transform.rotation;
    		shot.SetActive(true);
			}
		}
	}
	// FixedUpdate is called one per specific time
	/* void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal"); 				//Get if Any Horizontal Keys pressed
		float moveVertical = Input.GetAxis ("Vertical");					//Get if Any Vertical Keys pressed
		Vector2 movement = new Vector2 (moveHorizontal, moveVertical); 		//Put them in a Vector2 Variable (x,y)
		rigidbody2.velocity = movement * speed; 							//Add Velocity to the player ship rigidbody

		//Lock the position in the screen by putting a boundaries
		rigidbody2.position = new Vector2 
			(
				Mathf.Clamp (rigidbody2.position.x, boundary.xMin, boundary.xMax),  //X
				Mathf.Clamp (rigidbody2.position.y, boundary.yMin, boundary.yMax)	 //Y
			);
		
	}*/

	void FixedUpdate()
	{	
		if(Input.touchCount>0)
		{
			Touch touch=Input.GetTouch(0);
			
			touchPosition=Camera.main.ScreenToWorldPoint(touch.position);
			touchPosition.z=0;
	
			direction=touchPosition-transform.position;
			rigidbody2.velocity=new Vector2(direction.x,direction.y+1) *speed;
			
			if(touch.phase==TouchPhase.Ended)
			{
				rigidbody2.velocity=Vector2.zero;
			}
		}

		rigidbody2.position = new Vector2 
		(
			Mathf.Clamp (rigidbody2.position.x, boundary.xMin, boundary.xMax),  //X
			Mathf.Clamp (rigidbody2.position.y, boundary.yMin, boundary.yMax)	 //Y
		);
	}
	//Called when the Trigger entered hate 
	void OnTriggerEnter2D(Collider2D other)
	{
		//Excute if the object tag was equal to one of these
		if(other.tag == "Enemy" || other.tag == "Asteroid" || other.tag == "EnemyShot" ||other.tag=="EnemyBlue") 
		{
			Instantiate (Explosion, transform.position , transform.rotation); 				//Instantiate Explosion
			SharedValues_Script.gameover = true; 											//Trigger That its a GameOver
			gameObject.SetActive(false); 															//Destroy Player Ship Object
		}
	}
}
