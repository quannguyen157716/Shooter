﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoweUp : MonoBehaviour 
{
	public Rigidbody2D rb;
	void Start()
	{
		rb.velocity= -1*transform.up*1.5f;
		Destroy(gameObject,10);
	}
}
