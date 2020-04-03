using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Meteor : MonoBehaviour
{
	public ParticleSystem FrontFlames;
	public ParticleSystem BurstFlames;
	public ParticleSystem Trail;
	public ParticleSystem[] ImpactEffects;
	
	public float StartDeg;
	public float StartDist;
	public float Gravity;
	public float MaxFallSpeed;
	public float Speed;
	public float AtmosphereDistance;
	public float Drag;
	public SpriteRenderer Sprite;

	private float _rads;
	private float _dist;
	private Vector2 _position;
	private Vector2 _velocity;
	private bool _inAtmosphere;
	private bool _hit;

	private void Start()
	{
		_rads = Random.Range(0f,360f) * Mathf.Deg2Rad;
		_dist = StartDist;
		_velocity.x = Speed;
		//_velocity.y = Random.Range(-1, 1f);

		_position = new Vector2(
		Mathf.Cos(_rads) * _dist * Random.Range(0.9f,1.1f),
		Mathf.Sin(_rads) * _dist * Random.Range(0.9f, 1.1f)
		);
		transform.position = _position;

		transform.localScale *= Random.Range(0.25f, 1f);
	}

	private void Update()
	{
		if (_hit)
			return;

		if(_velocity.y > -MaxFallSpeed) _velocity.y -= Gravity * Time.deltaTime;

		_rads += (_velocity.x) * Time.deltaTime;
		_dist += _velocity.y * Time.deltaTime;

		_position = new Vector2(
		Mathf.Cos(_rads) * _dist,
		Mathf.Sin(_rads) * _dist
		);
		transform.position = _position;

		if (!_inAtmosphere && Vector2.Distance(transform.position, Vector2.zero) <= AtmosphereDistance)
		{
			EnterAtmosphere();
		}
		else if(_inAtmosphere)
		{
			_velocity.x *= (1 - Time.deltaTime * Drag);
			transform.eulerAngles = new Vector3(0f, 0f, Mathf.Atan2(transform.position.y, transform.position.x) * Mathf.Rad2Deg + 40f);

			if (!_hit && Physics2D.OverlapCircle(transform.position, 0.01f))
				HitPlanet();
		}
	}

	private void EnterAtmosphere()
	{
		_inAtmosphere = true;
		transform.eulerAngles = new Vector3(0f, 0f, Mathf.Atan2(transform.position.y, transform.position.x) * Mathf.Rad2Deg + 40f);
		BurstFlames.Emit(100);
		FrontFlames.Play();
		Trail.Play();
	}

	private void HitPlanet()
	{
		transform.eulerAngles = new Vector3(0f, 0f, ((Mathf.Atan2(transform.position.y, transform.position.x)) * Mathf.Rad2Deg) + 90f);
		_hit = true;
		GameManager.Instance.AddCrater(transform.position);
		foreach (var ps in ImpactEffects)
			ps.Play();
		FrontFlames.Stop();
		Trail.Stop();
		Sprite.enabled = false;
		Destroy(gameObject, 5f);
	}
}
