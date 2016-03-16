using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackScope : MonoBehaviour {
	public Transform pivot;
	public string targetTag;
	public float reach; // 공격 사정거리.
	public float angle; // 공격 영역각도.

	List<GameObject> targets = new List<GameObject>();
	
	List<GameObject> ret = new List<GameObject>();
	List<GameObject> remove = new List<GameObject>();

	//public int count {
	//	get { 
	//		return 
	//	}
	//}

	void OnTriggerEnter( Collider collider ) {
		var go = collider.gameObject;
		if ( go.tag == targetTag ) {
			//GUIDebug.Log( "OnTriggerEnter : " + targetTag );
			targets.Add( go );
		}
	}

	void OnTriggerExit( Collider collider ) {
		var go = collider.gameObject;
		if ( go.tag == targetTag ) {
			targets.Remove( go );
		}
	}


	public List<GameObject> getTargets() {
		ret.Clear();
		remove.Clear();
		foreach ( var e in targets ) {
			if ( e == null ) {
				remove.Add( e );
				continue;
			}
			Transform trans = e.transform;
			Vector3 dir = pivot.forward;
			Vector3 toTargetDir = (trans.position - pivot.position).normalized;
			//Debug.Log( Vector3.Angle( dir, toTargetDir ) );
			//if ( GetComponentsInParent<Enemy>() != null ) {

			//	GUIDebug.Log( "dir : " + dir );
			//	GUIDebug.Log( "toTargetDir : " + toTargetDir );
			//	GUIDebug.Log( "Vector3.Angle() : " + Vector3.Angle( dir, toTargetDir ).ToString() );
			//	GUIDebug.Log( angle.ToString() );
			//}

			if ( Vector3.Angle( dir, toTargetDir ) <= angle) {
				ret.Add( e );
			}
		}
		foreach ( var e in remove )
			targets.Remove(e);

		return ret;
	}

	//public void OnDrawGizmos() {
	//	var origin = Gizmos.color;
	//	Gizmos.color = Color.red;
	//	Vector3 forward = transform.forward;
		
	//	Gizmos.DrawLine( transform.position, ( transform.position + transform.forward * 3f ) );
	//	Gizmos.color = origin;
	//}

}