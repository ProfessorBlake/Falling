using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEditor;
using UnityEngine.UI;

public class OrbitCalculator : MonoBehaviour
{
	public float StartDeg;
	public float StartDist;
	public float Gravity;
	public float Speed;
	public Slider Slider;

	private float _rads;
	private float _dist;

	private Vector2 _position;
	private Vector2 _velocity;
	private List<PathPos> _pathPos = new List<PathPos>();
	private float _addPathPosDelay = 0f;
	private Color[] cols = new Color[] { Color.blue, Color.cyan, Color.gray, Color.green, Color.magenta, Color.red, Color.white, Color.yellow };
	private Color _pathCol;
	private int _selectedPathPos;

	public struct PathPos
	{
		public Vector2 Position;
		public float Time;
	}

	private void Generate()
	{
		_velocity.x = Speed;
		_velocity.y = 0f;
		_pathPos = new List<PathPos>();
		//_rads = StartDeg * Mathf.Deg2Rad;
		_rads = Random.Range(0f, 360f) * Mathf.Deg2Rad;
		//_dist = StartDist;
		_dist = Random.Range(2f, 5.25f);
		Speed = Random.Range(0.01f, 0.2f);
		_pathCol = cols[Random.Range(0, cols.Length)];

		int attempts = 999;
		bool hit = false;
		float t = 0f;
		float dist = 0f;
		while (attempts > 0 && !hit)
		{
			attempts--;
			t += 1f;

			_velocity.y -= Gravity;

			_rads += (_velocity.x);
			_dist += _velocity.y;

			Vector2 newPos = new Vector2(
			Mathf.Cos(_rads) * _dist,
			Mathf.Sin(_rads) * _dist
			);
			dist += (Vector2.Distance(newPos, _position));
			_position = newPos;

			_pathPos.Add(new PathPos()
			{
				Position = _position,
				Time = _velocity.magnitude * dist
			});

			if (Vector2.Distance(_position, Vector2.zero) < 0.5f)
			{
				Debug.Log("hit");
				hit = true;
			}
		}

		Slider.maxValue = _pathPos.Count;
	}

	public void UpdateSlider(float x)
	{
		
		_selectedPathPos = (int)x;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
			Generate();
	}

	private void OnDrawGizmos()
	{
		//Planet
		Gizmos.color = Color.green;
		Gizmos.DrawSphere(Vector2.zero, 0.5f);

		// Body
		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere(_position, 0.20f);

		//Path
		Gizmos.color = _pathCol;
		GUIStyle style = new GUIStyle();
		style.fontSize = 24;
		style.normal.textColor = Color.white;
		//foreach(PathPos pos in _pathPos)
		//{
		//	Gizmos.DrawSphere(pos.Position, 0.1f);
		//	Handles.Label(pos.Position + (pos.Position * 0.15f), (pos.Time).ToString(), style);
		//}
		Gizmos.DrawSphere(_pathPos[_selectedPathPos].Position, 0.1f);
		Handles.Label(_pathPos[_selectedPathPos].Position + (_pathPos[_selectedPathPos].Position * 0.15f), (_pathPos[_selectedPathPos].Time).ToString(), style);
	}
}
