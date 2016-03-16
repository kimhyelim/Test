using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackHit {
	public GameObject go;
	public float damage;
	public float spasticityTime;
}

public class PlayerController : MonoBehaviour {
	public enum State {
		Idle,
		Attack,
		Hit
	}

	public float rotationScale = 1.0f;

	public float speed = 6.0F;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;

	private Vector3 moveDirection = Vector3.zero;
	private CharacterController controller;

	public AttackScope attackScope;
	public float attackDelay; // 공격 딜레이.
	public float attackDamage; // 공격력.
							   //public float spasticityTime; // 피격시 경직 시간.

	public int hp;

	private Transform trans;
	private State state;

	Vector3 lastMousePos;


	int hitIdex = 0;

	public IEnumerator hit( AttackHit hit ) {
		//state = State.Hit;
		hp -= Mathf.CeilToInt( hit.damage );
		//GUIDebug.Log( "hit : " + gameObject.name );
		Debug.Log( "hit" );
		if ( hp <= 0 ) {
			die();
		}
		else {
			var attackerTrans = hit.go.transform;
			Vector3 attackerPos = attackerTrans.localPosition;
			Vector3 hitDir = ( trans.localPosition - attackerPos ).normalized;
			
			float backwardTime = 0.5f;
			float t = 0;
			hitIdex++;
			int curHitIdex = hitIdex;
			while ( t < backwardTime && curHitIdex == hitIdex ) {
				t += Time.deltaTime;
				controller.Move( ( backwardTime - t ) * 0.2f * hitDir );
				yield return null;
			}
		}
	}

	void die() {
		Debug.Log( "die player." );
		trans.GetComponentInChildren<Renderer>().enabled = false;
	}

	void Awake() {
		trans = transform;
		controller = GetComponent<CharacterController>();
	}

	void Start() {
		state = State.Idle;
		lastMousePos = Input.mousePosition;
	}


	void Update() {

		float dir = ( Input.mousePosition - lastMousePos ).x;
		trans.Rotate( Vector3.up, dir * rotationScale );


		lastMousePos = Input.mousePosition;

		if ( controller.isGrounded ) {
			moveDirection = new Vector3( Input.GetAxis( "Horizontal" ), 0, Input.GetAxis( "Vertical" ) );
			moveDirection = trans.TransformDirection( moveDirection );
			moveDirection *= speed;
			//if (Input.GetButton("Jump"))
			//	moveDirection.y = jumpSpeed;

		}
		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move( moveDirection * Time.deltaTime );

		//if ( moveDirection.x > 0f || moveDirection.z > 0f )
		//	trans.forward = moveDirection.normalized;
		//trans.LookAt();

		if ( Input.GetMouseButtonDown( 0 ) ) {
			StartCoroutine( attack() );
		}
	}

	IEnumerator attack() {
		yield return new WaitForSeconds( attackDelay );

		var targets = attackScope.getTargets();

		AttackHit ag = new AttackHit();
		ag.go = gameObject;
		ag.damage = attackDamage;
		ag.spasticityTime = 0.5f;

		foreach ( var e in targets ) {
			e.SendMessage( "hit", ag );
		}

		state = State.Idle;
	}

	//void startState( State state ) {
	////	Debug.Log( state );
	//	this.state = state;
	//	switch ( state ) {
	//		case State.Idle:
	//			break;
	//		case State.Attack:
	//			StartCoroutine( attack() );
	//			break;
	//	}
	//}

	public void OnDrawGizmos() {
		var origin = Gizmos.color;
		Gizmos.color = Color.black;
		Gizmos.DrawLine( transform.position, ( transform.position + transform.forward * 3f ) );
		Gizmos.color = origin;
	}
}