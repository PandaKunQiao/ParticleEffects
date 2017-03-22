using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class cannonBall : MonoBehaviour
{

	private Rigidbody projectile;

	[SerializeField]
	public float _timeToExplode = 2f;

	public float radius = 5f;
	public float power = 400f;
	public float liftPower = 1f;

	public ParticleSystem explosionFX;




	// Use this for initialization
	IEnumerator Start()
	{
		yield return new WaitForSecondsRealtime(_timeToExplode);

		ParticleSystem instanceFX = Instantiate(explosionFX, transform.position, transform.rotation) as ParticleSystem;

		foreach (Renderer r in GetComponentsInChildren<Renderer>())
			r.enabled = false;
		Explode();

	}


	void Explode()
	{
		Vector3 explosionPos = transform.position;
		Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);

		foreach (Collider hit in colliders)
			if (hit && hit.GetComponent<Rigidbody>())
			{
				hit.GetComponent<Rigidbody>().AddExplosionForce(power, explosionPos, radius, liftPower);
			}
		Destroy(gameObject);
	}

}
