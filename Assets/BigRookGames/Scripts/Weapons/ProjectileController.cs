using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BigRookGames.Weapons
{
    public class ProjectileController : MonoBehaviour
    {
        // --- Config ---
        public float speed = 100;
        public float explosionRadius = 3f;
        public float explosionForce = 5f;
        public float upwardsModifier = 1f;
        public LayerMask collisionLayerMask;

        // --- Explosion VFX ---
        public GameObject rocketExplosion;

        // --- Projectile Mesh ---
        public MeshRenderer projectileMesh;

        // --- Script Variables ---
        private bool targetHit;

        // --- Audio ---
        public AudioSource inFlightAudioSource;

        // --- VFX ---
        public ParticleSystem disableOnHit;


        private void Update()
        {
            // --- Check to see if the target has been hit. We don't want to update the position if the target was hit ---
            if (targetHit) return;

            // --- moves the game object in the forward direction at the defined speed ---
            transform.position += transform.forward * (speed * Time.deltaTime);
        }


        /// <summary>
        /// Explodes on contact.
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter(Collision collision)
        {
            // --- return if not enabled because OnCollision is still called if compoenent is disabled ---
            if (!enabled) return;

            // --- Explode when hitting an object and disable the projectile mesh ---
            Explode();
            BlowObj();
            projectileMesh.enabled = false;
            targetHit = true;
            inFlightAudioSource.Stop();
            foreach(Collider col in GetComponents<Collider>())
            {
                col.enabled = false;
            }
            disableOnHit.Stop();
           
            // --- Destroy this object after 2 seconds. Using a delay because the particle system needs to finish ---
            Destroy(gameObject, 5f);
        }


        /// <summary>
        /// Instantiates an explode object.
        /// </summary>
        private void Explode()
        {
            // --- Instantiate new explosion option. I would recommend using an object pool ---
            GameObject newExplosion = Instantiate(rocketExplosion, transform.position, rocketExplosion.transform.rotation, null);
        }


        private void BlowObj()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
            
            foreach (Collider col in colliders)
            {
                if (col.gameObject == this.gameObject) continue;
                Rigidbody rb = col.attachedRigidbody;
                if(rb == null)continue;
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, upwardsModifier, ForceMode.Impulse);
                Debug.Log("Hit Explosion" + col.name);
            }    

        }

    }
}