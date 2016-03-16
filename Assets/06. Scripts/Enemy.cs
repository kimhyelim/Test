using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public enum State { 
		Idle,
		Move,
		Hit,
		Attack
	}

	public string targetTag;
	public NavMeshAgent agent;
	public int hp;

	public CharacterController controller;
	public AttackScope attackScope;
	public float attackDelay; // 공격 딜레이.
	public float attackDamage; // 공격력.

	private GameObject target;
	private Transform targetTrans;

	private Transform trans;

	private State state;

	int hitIdex = 0;

	public IEnumerator hit( AttackHit hit ) {
		state = State.Hit;
		hp -= Mathf.CeilToInt( hit.damage );
		Debug.Log("e hit");
		if ( hp <= 0 ) {
			die();
		}
		else {
			var attackerTrans = hit.go.transform;
			Vector3 attackerPos = attackerTrans.localPosition;
			Vector3 hitDir = ( trans.localPosition - attackerPos ).normalized;

			agent.Stop();

			float backwardTime = 0.5f;
			float t = 0;
			hitIdex++;
			int curHitIdex = hitIdex;
			while ( t < backwardTime && curHitIdex == hitIdex ) {
				t += Time.deltaTime;
				controller.Move( ( backwardTime - t ) * 0.15f * hitDir );
				yield return null;
			}
			if ( curHitIdex == hitIdex )
				StartCoroutine( move() );

		}
	}


	void Start() {
		trans = transform;

		target = GameObject.FindGameObjectWithTag( targetTag );
		targetTrans = target.transform;

		StartCoroutine( move() );
	}

	void Update() {
		var targets = attackScope.getTargets();
		if ( targets.Count != 0 && state != State.Attack ) {
			
			StartCoroutine( attack() );
		}
		else if( state == State.Idle) {
			StartCoroutine( move() );
		}

	}

	IEnumerator attack() {
		state = State.Attack;
		//GUIDebug.Log("Attack : " + gameObject.name);
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


	IEnumerator move() {
		state = State.Move;
		agent.Resume();
		agent.SetDestination( targetTrans.position );

		Vector3 lastPos = targetTrans.position;
		float maxLen = 8.0f, minLen = 3.0f;
		while ( state == State.Move && target != null ) { 
			float len = Vector3.Magnitude( lastPos - trans.position );
			yield return new WaitForSeconds( Mathf.Clamp01( ( len - minLen ) / ( maxLen - minLen ) ) );
			agent.SetDestination( targetTrans.position );
			lastPos = targetTrans.position;
		}
	}

	void die() {
		Debug.Log( "die enemy." );
		DestroyObject( gameObject );
	}


	public void OnDrawGizmos() {
		var origin = Gizmos.color;
		Gizmos.color = Color.red;
		Gizmos.DrawLine( transform.position, ( transform.position + transform.forward * 2f ) );
		Gizmos.color = origin;
	}
}