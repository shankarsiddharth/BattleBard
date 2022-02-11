using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public LaneManager lane_manager;
    public Lane cur_lane;

    [Header("Config")]
    [Tooltip("The duration that it takes this camera to move to the next position unit after the current one dies.")]
    public float switch_duration = 0.75f;
    [Tooltip("The maximum time between unit noises.")]
    public float max_bark_cooldown = 10.0f;

    private float _audio_delay;

    private Minion _following;
    private float _following_delay;
    private float _cam_delay;

    private readonly static float CAM_REFRESH_RATE = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.OnMinionDeath += OnMinionDeath;
        EventManager.OnForceCameraMovement += OnForceCameraMove;
        EventManager.OnDrumPlayed += OnDrumPlayed;

        Camera.main.TryGetComponent(out AudioListener _audioListener);
        if (_audioListener == null)
            print("The main camera should have an Audio Listener attached.");

        _audio_delay = UnityEngine.Random.Range(1.5f, max_bark_cooldown);
    }

    // Update is called once per frame
    void Update()
    {
        CheckAndFollowMinion();
        AudioUpdate();
    }

    // Probably should be moved outside of Camera someday... Probably into a coroutine
    void AudioUpdate()
	{
        _audio_delay -= Time.deltaTime;
        if (_audio_delay <= 0f)
		{
            // Get a random lane
            float laneRand = UnityEngine.Random.Range(0f,1f);
            List<Minion> laneMinions = (laneRand > 0.5f) ? cur_lane.allied_minions : cur_lane.enemy_minions;

            // Get a random minion in that lane
            int minionRand = UnityEngine.Random.Range(0, laneMinions.Count);
            if (laneMinions.Count > 0)
            {
                Minion m = laneMinions[minionRand];

                // Get a random bark from that minion
                if (m.barks != null)
                {
                    int barkRand = UnityEngine.Random.Range(0, m.barks.Count);

                    m.audioSource.clip = m.barks[barkRand];
                    m.audioSource.Play();
                }
            }

            // Reset delay
            _audio_delay = UnityEngine.Random.Range(1.5f, max_bark_cooldown);
        }

    }

    void CheckAndFollowMinion()
	{
        // If we don't have a minion to follow, get a new one
        _cam_delay += Time.deltaTime;
        if (_following == null && _cam_delay > CAM_REFRESH_RATE)
        {
            _following = cur_lane.GetFurthestMinion();
            _cam_delay = 0f;
            _following_delay = 0f;
        }

        _following_delay += Time.deltaTime;

        // If we have a minion, move the camera towards it
        if (_following != null)
            transform.position = Vector3.Lerp(transform.position, _following.transform.position, Mathf.Min(1, _following_delay / switch_duration));
    }

    // When a minion dies, we need to move to the next
    void OnMinionDeath(Minion m)
	{
        if (_following == m)
		{
            _following = cur_lane.GetFurthestMinion();
            _following_delay = 0f;
        }
	}
    void OnForceCameraMove(int lane)
    {
        cur_lane = lane_manager.lanes[lane%lane_manager.lanes.Count];
        _following = null;
        EventManager.RaiseCameraMovedEvent(lane);
    }
    void OnDrumPlayed(EventManager.Drum drum)
	{
        if (drum == EventManager.Drum.Pedal)
		{
            EventManager.RaiseForceCameraMovement(lane_manager.lanes.IndexOf(cur_lane)+1);
		}
	}
}
