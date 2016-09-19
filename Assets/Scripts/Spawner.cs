using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject P_Agent;
	public bool autoSpawn = true;
	[Range(0f, 500f)]
	public int NbAgent = 1;

	private float elapsed;
	private float temporize;


	void Start () {
		for (int i = 0; i < NbAgent; i++) {
			Instantiate (P_Agent, transform);
		}

		initTimeOut ();
	}
	

	void Update () {
		if (autoSpawn) {
			elapsed += Time.deltaTime;
			if (elapsed >= temporize) {
				NbAgent++;
				initTimeOut ();
			}
		}

		if (transform.childCount < NbAgent) {
			for (int i = 0; i < NbAgent - transform.childCount; i++) {
				Instantiate (P_Agent, transform);
			}
		} else if (transform.childCount > NbAgent) {
			for (int i = 0; i < transform.childCount - NbAgent; i++) {
				Destroy (transform.GetChild (i).gameObject);
			}
		}
	}


	void initTimeOut () {
		elapsed = 0;
		temporize = Random.Range (2, 5);
	}

}
