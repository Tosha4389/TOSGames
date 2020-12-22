using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningScript : MonoBehaviour
{
    [Tooltip("Скорость передвижения молнии")]
    [SerializeField] float speed = 10f;
    [Tooltip("Урон")]
    [SerializeField] float damage = 10f;

    public GameObject SphereLightning { get; set; }
    public GameObject ColliderLightning { get; set; }
    public GameObject RightLightning { get; set; }
    public GameObject LeftLightning { get; set; }

    BoxCollider2D boxCollider;

    float startDamage;
    float halfDamage;
    float damnDamage;

    public float Damage
    {
        get { return damage; }
    }

    private void Awake()
    {
        SphereLightning = transform.GetChild(0).gameObject;
        LeftLightning = transform.GetChild(2).gameObject;
        RightLightning = transform.GetChild(1).gameObject;
        ColliderLightning = transform.GetChild(3).gameObject;
        boxCollider = ColliderLightning.GetComponent<BoxCollider2D>();

        startDamage = damage;
        halfDamage = 0.5f * damage;
        damnDamage = 0.25f * damage;
    }

    public void ActiveLineLightning(bool active)
    {
        if(active) {
            ColliderLightning.SetActive(true);
            LeftLightning.SetActive(true);
            RightLightning.SetActive(true);
            boxCollider.enabled = true;

        } else { 
            ColliderLightning.SetActive(false);
            LeftLightning.SetActive(false);
            RightLightning.SetActive(false);
            boxCollider.enabled = false;
        } 
    }

    public void ActiveSphereLightning(bool active)
    {
        if(active)
            SphereLightning.SetActive(true);
        else SphereLightning.SetActive(false);
    }

    public void MoveLightning(GameObject gameObject, Vector3 position, bool fast)
    {
        Rigidbody2D rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        Vector3 direct = position - gameObject.transform.position;

        if(fast) {            
            if(direct.sqrMagnitude > 0.1f) {
                rigidbody2D.MovePosition(position);
            }
        }
        else {            
            rigidbody2D.velocity = direct.normalized * speed * Time.deltaTime;            

            if(direct.sqrMagnitude < 0.1f) {
                rigidbody2D.velocity = Vector3.zero;
            }
        }
    }

    public void RotateLightning(Vector3 oneTouch, Vector3 twoTouch)
    {
        Vector3 direct = twoTouch - oneTouch;
        transform.right = - direct;
    }

    public void ScaleLightning()
    {
        float dist = Vector3.Distance(LeftLightning.transform.position, RightLightning.transform.position);
        
        if(dist > 2.5f) {
            LeftLightning.transform.localScale = new Vector3(dist / 2.5f, 2.5f / dist, LeftLightning.transform.localScale.z);
            RightLightning.transform.localScale = new Vector3(dist / 2.5f, 2.5f / dist, RightLightning.transform.localScale.z);
            boxCollider.gameObject.transform.localScale = new Vector2(dist, 0.1f);
        }

        if(dist > 2.5f && dist < 5f)
            damage = startDamage;
        else if(dist > 5f && dist < 7.5f)
            damage = halfDamage;
        else if(dist > 7.5f)
            damage = damnDamage;        
    }

    public void ColliderTransform(Vector2 start, Vector2 end)
    {        
        Vector2 direct = end - start;
        boxCollider.gameObject.transform.localScale = new Vector2(direct.magnitude, 0.1f);
    }

}
