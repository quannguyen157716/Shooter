using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maneuver : MonoBehaviour {

	public float dodge;
    public float smoothing;
    public Vector2 startWait;
    public Vector2 maneuverTime;
    public Vector2 maneuverWait;
	public bool maneuver=false;
    private float currentSpeed;
    private float targetManeuver;
    private Rigidbody2D rb;
	void OnEnable()
	{
		rb = GetComponent <Rigidbody2D> ();
        currentSpeed =1;
		if(maneuver)
        StartCoroutine (Evade ());
	}
	
	IEnumerator Evade()
    {
        yield return new WaitForSeconds (Random.Range (startWait.x, startWait.y));

        while (true)
        {
            targetManeuver = Random.Range (1, dodge) * -Mathf.Sign (transform.position.x);
            yield return new WaitForSeconds (Random.Range (maneuverTime.x, maneuverTime.y));
            targetManeuver = 0;
            yield return new WaitForSeconds (Random.Range (maneuverWait.x, maneuverWait.y));
        }
    }
    
	 void FixedUpdate ()
    {
		float newManeuver = Mathf.MoveTowards (rb.velocity.x, targetManeuver, Time.deltaTime*smoothing);
		rb.velocity = new Vector2 (newManeuver, -2);
    }

}
