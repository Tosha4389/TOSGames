using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMove : MonoBehaviour
{
	[SerializeField] float speed = 200f;

	bool facingRight = true;
	Animator anim;
	Rigidbody2D rigidb;
	Transform tran;
	Camera cam;
	SpriteRenderer spriteRenderer;
	Vector3 targetPos;

	public float Speed
	{
		get { return speed; }
		set { speed = value; }
	}

	void Awake()
	{
		anim = GetComponent<Animator>();
		rigidb = GetComponent<Rigidbody2D>();
		tran = GetComponent<Transform>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		cam = Camera.main;
		targetPos = tran.position;
	}

	public void Move()
	{
		Vector3 direction = targetPos - tran.position;
		if(direction.sqrMagnitude < 0.5f) {
			targetPos = new Vector3(Random.Range(-cam.orthographicSize * cam.aspect - 2f, cam.orthographicSize * cam.aspect + 2f), Random.Range(-cam.orthographicSize - 2f, cam.orthographicSize + 2f), tran.position.z);
		}

		rigidb.velocity = direction.normalized * speed * Time.deltaTime;

		anim.SetFloat("HSpeed", speed);
		anim.SetFloat("vSpeed", rigidb.velocity.y);

		if(targetPos.x > tran.position.x && !facingRight)
			Flip();
		else if(targetPos.x < tran.position.x && facingRight)
			Flip();
	}

	void Flip()
	{
		if(facingRight)
			spriteRenderer.flipX = true;
		else spriteRenderer.flipX = false;

		facingRight = !facingRight;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		StartCoroutine(MoveAround());
	}

	IEnumerator MoveAround()
	{
		if(Random.value > 0.5f) rigidb.velocity = Vector2.up * speed * Time.deltaTime;
		else rigidb.velocity = Vector2.down * speed * Time.deltaTime;
		yield return new WaitForSeconds(0.5f);
		targetPos = transform.position;
	}

}
