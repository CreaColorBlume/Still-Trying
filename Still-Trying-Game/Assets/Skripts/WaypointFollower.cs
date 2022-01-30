using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    //[SerializeField] private Transform[] _waypoints;
    [SerializeField] private List<WaypointData> _waypointDataList = new List<WaypointData>();
    private int _currentWaypointIndex = 0;
    private float _targetDistance = 0.1f;

    [SerializeField] private float _speed = 1f;

    private float _timerCur = 0.0f;
    [SerializeField] private float _timerReset = 1.0f;

    [SerializeField] private float _rotationSpeed;

    private void Start()
    {
        _timerCur = _timerReset;
    }
    void Update()
    {
        if (Vector3.Distance(transform.position, _waypointDataList[_currentWaypointIndex].waypointLocation.position) < _targetDistance)
        {
            if (_timerCur >= 0)
            {
                _timerCur -= Time.deltaTime;
            }
            else
            {
               
                _currentWaypointIndex++;

                if (_currentWaypointIndex >= _waypointDataList.Count)
                {
                    _currentWaypointIndex = 0;
                }

                _speed = _waypointDataList[_currentWaypointIndex].speedFromWaypoint;
                _timerCur = _waypointDataList[_currentWaypointIndex].timeToWaitAtWaypoint;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _waypointDataList[_currentWaypointIndex].waypointLocation.position, _speed * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _waypointDataList[_currentWaypointIndex].waypointLocation.rotation, _rotationSpeed * Time.deltaTime);
        }
    }

    [System.Serializable]
    public class WaypointData
    {
        public Transform waypointLocation;
        public float speedFromWaypoint;
        public float timeToWaitAtWaypoint;
    }
}
