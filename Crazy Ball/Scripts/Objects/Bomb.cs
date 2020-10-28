using UnityEngine;

public class Bomb : MonoBehaviour
{
    [Header("Вручную")]
    [SerializeField] float powerBomb;
    [SerializeField] float radiusBomb;
    [SerializeField] ParticleSystem firePS;
    ParticleSystem smokePS;
    MeshRenderer mesh;
    BoxCollider _collider;


    

    void Awake()
    {
        smokePS = GetComponent<ParticleSystem>();
        mesh = GetComponent<MeshRenderer>();
        _collider = GetComponent<BoxCollider>();
    }

    void ExplosionBomb()
    {        
        firePS.Play(true);
        smokePS.Play(true);
        mesh.enabled = false;
        _collider.enabled = false;
        Destroy(gameObject, firePS.main.duration / firePS.main.simulationSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Player")) {
            ExplosionBomb();
            Rigidbody rb = collision.collider.GetComponent<Rigidbody>();
            if(rb != null)
                rb.AddExplosionForce(powerBomb, transform.position, radiusBomb, 3.0F);
        }
            
    }

}
