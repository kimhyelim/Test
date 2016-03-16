using UnityEngine;
using System.Collections.Generic;

public class GUIDebug : MonoBehaviour {
	static Queue<string> buffer = new Queue<string>();

	public static void log( string str ) {
		buffer.Enqueue( str );

		if ( buffer.Count > 20 )
			buffer.Dequeue();
	}


	void OnGUI() {
		foreach ( var e in buffer )
			GUILayout.Label( e );
	}
}
