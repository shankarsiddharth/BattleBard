using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class Checkpoint : MonoBehaviour
{
	public PlayableDirector director;

	private void OnTriggerEnter(Collider other)
	{
		director.Play();
	}

}
