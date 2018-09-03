using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullPlatform : MonoBehaviour, IHookable {

    Transform _wayPoint;
    Transform _startPoint;
    Transform _platform;
    Transform _body;
    bool _isActive;
    bool _isWaiting;
    Transform _nextPoint;
    float _timer;

    public float waypointWaitTime;
    public float platformSpeed;

	// Use this for initialization
	void Start () {
        _body = transform.parent;
        _platform = _body.transform.parent;
        _wayPoint = _platform.Find("waypoint");
        if(_wayPoint == null)
        {
            Debug.LogError("Missing waypoint!");
        }
        _startPoint = _platform.Find("startpoint");
        if (_wayPoint == null)
        {
            Debug.LogError("Missing startpoint!");
        }
    }
	
	// Update is called once per frame
	void Update () {
		if (_isActive)
        {
            if (!_isWaiting)
            {
                // Go to nextpoit
                _body.Translate(
                    (_nextPoint.position - _body.transform.position).normalized * platformSpeed * Time.deltaTime
                );
                // se chegou
                if (Vector2.Distance(_nextPoint.position, _body.transform.position) <= 0.1)
                {
                    if (_nextPoint == _wayPoint)
                    {
                        _isWaiting = true;
                        _timer = waypointWaitTime;
                        _nextPoint = _startPoint;
                    }
                    else
                    {
                        _isActive = false;
                    }
                }
            }
            else
            {
                if (_timer <= 0)
                {
                    _isWaiting = false;
                }
                else
                {
                    _timer -= Time.deltaTime;
                }
            }
        }
	}

    public void HookAction()
    {
        Debug.Log("Hook Action!");
        if (!_isActive)
        {
            _isActive = true;
            _nextPoint = _wayPoint;

        }
    }

    public string GetTag()
    {
        return gameObject.tag;
    }
}
