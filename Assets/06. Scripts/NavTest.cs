using UnityEngine;
using System.Collections;

public class NavTest : MonoBehaviour {
	public Vector3 dest;
	
	
	
	IEnumerator Start(){
		NavMeshAgent nma = GetComponent<NavMeshAgent>();
		nma.destination = dest;

		yield return new WaitForSeconds(1.0f);

//		var body = GetComponent<Rigidbody> ();
//		body.AddForce (-50.0f, 100.0f, 0.0f);
	}

}
