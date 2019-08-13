﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

[System.Serializable]
public class Boundary 
{
	public float xMin, xMax, yMin, yMax; //Screen Boundary dimentions
}

public class Player_Script : MonoBehaviour 
{
	public static Player_Script PlayerInstance;
	//Public Var
	public float speed; 			//Player Ship Speed
	public Boundary boundary; 		//make an Object from Class Boundary
	public GameObject Explosion;	//Explosion Prefab
	Vector3 direction;
	Vector3 touchPosition;
	Vector3 lastTouchPosition;
	Rigidbody2D rigidbody2;
	public Transform position;
	[HideInInspector]
	public bool isDead=false;
	float x,y;//calculate touch points
	Camera mCamera;
	//AudioSource audio2;
	// Update is called once per frame
	void OnEnable()
	{
		isDead=false;
	}
	void Start()
	{
		mCamera=Camera.main;
		PlayerInstance=this;
		rigidbody2=GetComponent<Rigidbody2D>();
		//audio2=GetComponent<AudioSource>();
	}
	void FixedUpdate()
	{	
		Move();
	}

	//Called when the Trigger entered 
	void OnTriggerEnter2D(Collider2D other)
	{
		//Excute if the object tag was equal to one of these
		if(other.tag!=PlayerGun.PlayerGunInstance.shotDealDamage && other.tag!="Boundary"&&other.tag!="PowerUp"&&other.tag!="PlayerTracingShot") 
		{
			Debug.Log(other.tag);
			Debug.Log(PlayerGun.PlayerGunInstance.shotDealDamage);
			Instantiate (Explosion, transform.position , transform.rotation); 				//Instantiate Explosion
			SharedValues_Script.gameover = true;
			TakeDamage(1); 											
			//gameObject.SetActive(false); 															//Destroy Player Ship Object
		}
		if(other.tag=="PowerUp")
		{
			if(PlayerGun.PlayerGunInstance.level<2)
			{
				PlayerGun.PlayerGunInstance.level++;
				PlayerGun.PlayerGunInstance.UpgradeGun();
			}
			
		}
	}

	//Moving touch pad only

	Vector3 GetCameraPosition(Vector3 pos)
	{
		return mCamera.ScreenToWorldPoint(pos);
	}

    void Move()
	{
	
		//Debug.Log(EventSystem.current.IsPointerOverGameObject());
		//if click over UI Then block -->Doesnt really work well with touch
		if(EventSystem.current.IsPointerOverGameObject())
		return;

		#if UNITY_EDITOR
		if (Input.GetMouseButton(0))
		{
			Vector3 mousePosition = mCamera.ScreenToWorldPoint(Input.mousePosition); //calculating mouse position in the worldspace
            mousePosition.z = transform.position.z;
			direction=mousePosition-transform.position;
			rigidbody2.velocity=new Vector2(direction.x,direction.y+1) *speed;
            //transform.position = Vector3.MoveTowards(transform.position, mousePosition, 30 * Time.deltaTime);	
			if(Input.GetMouseButtonUp(0))
			rigidbody2.velocity=Vector2.zero;
		}
		#endif

		
		#if UNITY_IOS || UNITY_ANDROID
		if(Input.touchCount>0)
		{
			Touch touch=Input.GetTouch(0);
			touchPosition=GetCameraPosition(touch.position);
			touchPosition.z=0;

			direction=touchPosition-transform.position;
			rigidbody2.velocity=new Vector2(direction.x,direction.y+1) *speed;

			if(touch.phase==TouchPhase.Ended)
			{
				rigidbody2.velocity=Vector2.zero;
			}
		}
		#endif

		rigidbody2.position = new Vector2 
		(
			Mathf.Clamp (rigidbody2.position.x, boundary.xMin, boundary.xMax),  //X
			Mathf.Clamp (rigidbody2.position.y, boundary.yMin, boundary.yMax)	 //Y
		);
	}
	/* 
	void Moving()
	{
		if(EventSystem.current.IsPointerOverGameObject())
		return;
		if(Input.touchCount>0)
		{
			Touch touch=Input.GetTouch(0);
	
			touchPosition=mCamera.ScreenToWorldPoint(touch.position);
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

	void MovingPC()
	{

		if (Input.GetMouseButton(0)) //if mouse button was pressed       
        {
			if(EventSystem.current.IsPointerOverGameObject())
			return;
            Vector3 mousePosition = mCamera.ScreenToWorldPoint(Input.mousePosition); //calculating mouse position in the worldspace
            mousePosition.z = transform.position.z;
			direction=mousePosition-transform.position;
			rigidbody2.velocity=new Vector2(direction.x,direction.y+1) *speed;
            //transform.position = Vector3.MoveTowards(transform.position, mousePosition, 30 * Time.deltaTime);

			if(Input.GetMouseButtonUp(0))
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
	*/
	void TakeDamage(int damage)
	{
		Destruct();
	}

	void Destruct()
	{
		isDead=true;
		//Destroy(gameObject);
		SharedValues_Script.gameover = true;   //Trigger That its a GameOver
		gameObject.SetActive(false);
	}
}
