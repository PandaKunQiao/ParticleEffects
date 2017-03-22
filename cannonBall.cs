using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class cannonBall : MonoBehaviour
{

    private Rigidbody projectile;
	private Quaternion startRotation = new Quaternion(0f, 0f, 0f, 0f);

    [SerializeField]
    public float _timeToBomb = 1.5f;
	public float _timeToShine = 3f;
	public float _timeToPhysics = 1f;

    public float radius = 5f;
    public float power = 400f;
    public float liftPower = 1f;

    public ParticleSystem explosionFX;
	public ParticleSystem fireFX;
	public ParticleSystem shiningFX;

    


    // Use this for initialization
    IEnumerator Start()
    {
        yield return new WaitForSecondsRealtime(_timeToShine);

		ParticleSystem instanceShiningFX = Instantiate(shiningFX, transform.position, transform.rotation) as ParticleSystem;



		yield return new WaitForSecondsRealtime(_timeToBomb);
		ParticleSystem instanceExplosionFX = Instantiate(explosionFX, transform.position, transform.rotation) as ParticleSystem;
		yield return new WaitForSecondsRealtime(_timeToPhysics);
		print (startRotation);
		if (transform.position.y <= 0.2 && transform.position.y >= -0.2){
			ParticleSystem instanceFireFX = Instantiate(fireFX, transform.position, startRotation) as ParticleSystem;
		}
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
