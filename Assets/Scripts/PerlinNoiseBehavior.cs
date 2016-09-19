using UnityEngine;
using System.Collections;

public class PerlinNoiseBehavior : MonoBehaviour {

	public bool randomizeSeeds = true;
	public float seedX;
	public float seedY;
	public float seedZ;

	public float speedFactor = 1;
	public float MinMapRange = -5f;
	public float MaxMapRange = 5f;

	private Vector3 offset;

	private float _stepX;
	private float _stepY;
	private float _stepZ;


	void Awake () {
		if (randomizeSeeds) {
			seedX = Random.Range(0, 1000000);
			seedY = Random.Range(0, 1000000);
			seedZ = Random.Range(0, 1000000);
		}

		offset = Random.insideUnitSphere * MaxMapRange * 0.8f;
		RandomPerlinMove ();
	}


	void FixedUpdate () {
		RandomPerlinMove ();
	}


	/// <summary>
	/// Randomize the position on the three axes with basic randomness.
	/// </summary>
	void RandomMove () {
		Vector3 p = transform.position;
		float step_x = p.x + Random.Range (-1f, 1f);
		float step_y = p.y + Random.Range (-1f, 1f);
		float step_z = p.z + Random.Range (-1f, 1f);

		transform.position = new Vector3 (step_x, step_y, step_z);
	}


	/// <summary>
	/// Randomize the position with Perlin noise.
	/// </summary>
	void RandomPerlinMove () {
		_stepX = mapRange (0, 1, MinMapRange, MaxMapRange, Mathf.PerlinNoise (Time.time * speedFactor, seedX));
		_stepY = mapRange (0, 1, MinMapRange, MaxMapRange, Mathf.PerlinNoise (Time.time * speedFactor, seedY));
		_stepZ = mapRange (0, 1, MinMapRange, MaxMapRange, Mathf.PerlinNoise (Time.time * speedFactor, seedZ));

		transform.position = offset + new Vector3 (_stepX, _stepY, _stepZ);
	}


	/// <summary>
	/// Map a value from a range, A to another, B.
	/// </summary>
	/// <returns>The range.</returns>
	/// <param name="a1">A1.</param>
	/// <param name="a2">A2.</param>
	/// <param name="b1">B1.</param>
	/// <param name="b2">B2.</param>
	/// <param name="s">The value.</param>
	float mapRange (float a1, float a2, float b1 ,float b2, float s) {
		return b1 + (s - a1) * (b2 - b1) / ( a2 - a1);
	}
}
