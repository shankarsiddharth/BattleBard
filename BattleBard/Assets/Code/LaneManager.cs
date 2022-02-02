using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
struct WaveMinion
{
    public Minion minion;
    public int count;
}

[Serializable]
struct Wave
{
    public List<WaveMinion> EnemyMinions;
    public List<WaveMinion> AlliedMinions;
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

            foreach (WaveMinion wm in current_wave.EnemyMinions) {
                for (int i = 0; i < wm.count; i++)
                {
                    Minion min = Instantiate(wm.minion);
                    min.transform.position = lane.GetLaneCheckpoint(lane.GetLaneCheckpointCount() - 1);
                    min.cur_lane = lane;
                    min.pointIndex = lane.GetLaneCheckpointCount() - 2;
                    lane.enemy_minions.Add(min);
                }
            }

            foreach (WaveMinion wm in current_wave.AlliedMinions)
            {
                for (int i = 0; i < wm.count; i++)
                {
                    Minion min = Instantiate(wm.minion);

                    // Generate a random starting position for them
                    float xoffset = Random.Range(0, 2f);
                    float zoffset = Random.Range(0, 2f);

                    min.transform.position = lane.GetLaneCheckpoint(0) + new Vector3(xoffset, 0, zoffset);
                    min.cur_lane = lane;
                    min.pointIndex = 1;
                    lane.allied_minions.Add(min);
                }
            }

            EventManager.RaiseLaneSpawnEvent(_cur_wave);
        }

        // Increment wave
        _cur_wave++;
	}

}
