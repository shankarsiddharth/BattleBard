using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
struct Wave
{
    public List<Minion> EnemyMinions;
    public List<Minion> AlliedMinions;
}

public class LaneManager : MonoBehaviour
{
    public List<Lane> lanes;

    [SerializeField] 
    List<Wave> waves;


    [Tooltip("The time between wave spawns.")]
    public float wave_interval = 30f;

    private float _time_since_wave = 0;
    private int _cur_wave = 0;

	private void Update()
	{
        _time_since_wave += Time.deltaTime;
        if (_time_since_wave >= wave_interval) {
            SpawnWave();
            _time_since_wave = 0;
        }
	}


    private void SpawnWave()
	{
        foreach (Lane lane in lanes)
		{
            // Get the current wave, wrapping if needed
            Wave current_wave = waves[_cur_wave % waves.Count];

            foreach (Minion m in current_wave.EnemyMinions) {
                Minion min = Instantiate(m);
                min.transform.position = lane.GetLaneCheckpoint(lane.GetLaneCheckpointCount() - 1);
                min.cur_lane = lane;
                min.pointIndex = lane.GetLaneCheckpointCount() - 2;
                lane.enemy_minions.Add(min);
            }

            foreach (Minion m in current_wave.AlliedMinions)
			{
                Minion min = Instantiate(m);
                min.transform.position = lane.GetLaneCheckpoint(0);
                min.cur_lane = lane;
                min.pointIndex = 1;
                lane.allied_minions.Add(min);
            }

            EventManager.RaiseLaneSpawnEvent(_cur_wave);
        }

        // Increment wave
        _cur_wave++;
	}

}
