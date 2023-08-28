using System.Collections;
using UnityEngine;

public class ShabbaWaveGenerator : MonoBehaviourBase
{
	[SerializeField] MeshGenerator meshGenerator;
	[SerializeField] private float increaseRate = 0.5f;
	[SerializeField] private float DecreaseRate = 0.1f;
	[SerializeField] private float maxWaveHeight = 0.3f;
	private Coroutine increaseRoutine;
	private Coroutine decreaseRoutine;

	#region material Properties
	private const string _WaveCollisionPoint = "_WaveCollisionPoint";
	private const string _WaveHeight = "_WaveHeight";

	#endregion

	private void OnTriggerEnter2D(Collider2D collision)
	{
		var closestVector = collision.transform.position.GetClosestVector(meshGenerator.surfaceVertices);
		meshGenerator.material.SetFloat(_WaveCollisionPoint, closestVector.x / meshGenerator.width);
		this.DisposeCoroutine(ref increaseRoutine);
		this.DisposeCoroutine(ref decreaseRoutine);
		increaseRoutine = StartCoroutine(IncreaseWave());
	}

	IEnumerator IncreaseWave()
	{
		float height = meshGenerator.material.GetFloat(_WaveHeight);
		float t = height / maxWaveHeight;
		while (height < maxWaveHeight)
		{
			height = Mathf.Lerp(0.1f, maxWaveHeight, t);
			t += Time.deltaTime * increaseRate;
			meshGenerator.material.SetFloat(_WaveHeight, height);
			yield return null;
		}

		meshGenerator.material.SetFloat(_WaveHeight, maxWaveHeight);
		increaseRoutine = null;
		StartCoroutine(DecreaseWave());
	}

	IEnumerator DecreaseWave()
	{
		float height = meshGenerator.material.GetFloat(_WaveHeight);
		float t = 1 - height / maxWaveHeight;
		while (height > 0.1f)
		{
			height = Mathf.Lerp(maxWaveHeight, 0.1f, t);
			t += Time.deltaTime * increaseRate;
			meshGenerator.material.SetFloat(_WaveHeight, height);
			yield return null;
		}

		meshGenerator.material.SetFloat(_WaveHeight, 0.1f);
		decreaseRoutine = null;

	}

	//private void OnCollisionEnter2D(Collision2D collision)
	//{
	//	Vector3 contactPoint = collision.GetContact(0).point;
	//	Debug.Log(contactPoint.GetClosestVector(meshGenerator.surfaceVertices));
	//}

	// internals
	// getClosestVertexToCollision : look into the vertices in the mesh, selecting only those that are interesting (aka the surface line) and get the closest vertex to the collision point. What we need most is the UV of that point
	// setMaterialData : send the relevant data to the shader so it can react 
	// the collision point (as a uv)
	// float surfaceWaveStrength : 1 - polish idea : start a couroutine that is going to interpolate from current surfaceWaveStrength in the shader to 1 in a very short time
	// start a coroutine that diminishes surfaceWaveStrength over time

}

