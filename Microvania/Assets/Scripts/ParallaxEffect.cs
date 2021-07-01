using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
	private float length, startPos;
	public GameObject cam;
	public float parallaxFactor;

	void Start()
	{
		startPos = transform.position.x;
		length = GetComponent<SpriteRenderer>().bounds.size.x;
	}

	void LateUpdate()
	{
		float temp = (cam.transform.position.x * (1 - parallaxFactor));
		float dist = (cam.transform.position.x * parallaxFactor);

		transform.position = new Vector3(startPos + dist - length, transform.position.y, transform.position.z);

		if (temp > startPos + length/2) startPos += length;
		else if (temp < startPos - length/2) startPos -= length;
	}
}
