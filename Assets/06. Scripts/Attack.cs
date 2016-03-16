using UnityEngine;
using System.Collections;


//public class Attack : MonoBehaviour {

//	public AttackScope attackScope;
//	public float attackDelay; // 공격 딜레이.
//	//public float attackReach; // 공격 사정거리.
//	//public float attackAngle; // 공격 영역각도.
//	public float attackDamage; // 공격력.
//	//public float spasticityTime; // 피격시 경직 시간.

//	private bool play ; // 공격 중이니?
	
//	public bool isPlay {
//		get {
//			return play;
//		}
//	}

//	// Use this for initialization
//	void Start () {
	
//	}

//	IEnumerator attack() {
//		if ( play ) yield break;

//		play = true;
//		yield return new WaitForSeconds( attackDelay );

//		var targets = attackScope.getTargets();
		
//		AttackHit ag = new AttackHit();		
//		ag.go = gameObject;
//		ag.damage = attackDamage;
//		ag.spasticityTime = 0.5f;

//		foreach ( var e in targets ) {
//			e.SendMessage( "hit", ag );
//		}

//		play = false;
//	}

//}
