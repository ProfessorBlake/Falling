using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }

	public Action OnMeteorEnterAtmosphere;

	public GameObject MeteorPrefab;
	public GameObject CraterPrefab;

	public Transform CraterContainer;

	private void Awake()
	{
		Instance = this;
	}

	private void Update()
	{
		if (Input.GetKey(KeyCode.Space))
			Instantiate(MeteorPrefab);
	}

	public void AddCrater(Vector3 position)
	{
		Instantiate(CraterPrefab, position, Quaternion.Euler(new Vector3(0f, 0f, Random.Range(0f, 360f))),CraterContainer);
	}
}
