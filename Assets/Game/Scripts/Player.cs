using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
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
}
