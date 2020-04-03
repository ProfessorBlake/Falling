using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlanetController : MonoBehaviour
{
	public GameManager GameManager;
	public SpriteRenderer SkySpriteRenderer;

	private void Start()
	{
		GameManager.OnMeteorEnterAtmosphere += HandleMeteorEnterAtmosphere;
	}

	private void OnDisable()
	{
		GameManager.OnMeteorEnterAtmosphere -= HandleMeteorEnterAtmosphere;
	}

	private void HandleMeteorEnterAtmosphere()
	{
		SkySpriteRenderer.DOColor(Color.white, 0.2f).SetLoops(1, LoopType.Yoyo);
	}
}
