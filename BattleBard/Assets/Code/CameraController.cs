using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public List<Lane> lanes;
    public Lane cur_lane;

    [Header("Config")]
    [Tooltip("The duration that it takes this camera to move to the next position unit after the current one dies.")]
    public float switch_duration = 0.75f;

    private Minion _following;
    private float _following_delay;
    private float _cam_delay;

    private readonly static float CAM_REFRESH_RATE = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.OnMinionDeath += OnMinionDeath;
    }

    // Update is called once per frame
    void Update()
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
            transform.position = Vector3.Lerp(transform.position, _following.transform.position, Mathf.Min(1, _following_delay/switch_duration));
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
}
