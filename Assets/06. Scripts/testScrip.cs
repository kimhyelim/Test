using UnityEngine;
using System.Collections;

public class testScrip : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.gameObject.GetComponent<CharacterJoint>().connectedBody = transform.parent.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame

}
