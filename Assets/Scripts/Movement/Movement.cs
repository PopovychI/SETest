using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    public Action OnStartMove;
    public Action OnStop;

    [SerializeField] private float _maxMoveSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 5f;
    [SerializeField] private float _currentAcceleration = 2f;
    [SerializeField] private float _deccelerationMultiplier = 20f;

    private float _currentSpeed;

    private Direction _direction;
    private Transform _destination;
    private Vector3 _directionVector;

    public Direction Dir => _direction;
    public Transform Destination => _destination;
    public float MaxMoveSpeed => _maxMoveSpeed;

    private void Awake()
    {
        _directionVector = Vector3.zero;
    }
    public void Move()
    {
        if (!_destination) MoveInDirection();
        else MoveToDestination();
    }
    private void MoveInDirection()
    {

        var calculatedSpeed = _currentSpeed;
        if (Vector3.Angle(transform.forward, _directionVector) > 60f)
        {
            calculatedSpeed /= 3f;
            RotateInDirection(_directionVector); //rotates one more time in one frame
        }
        transform.position = Vector3.MoveTowards(transform.position, transform.position + _directionVector, calculatedSpeed * Time.deltaTime);
        if (_direction != Direction.None)
        {
            Accelerate();
            RotateInDirection(_directionVector);
        }
        else Deccelerate();
        SetDirection(Direction.None);
    }
    private void MoveToDestination()
    {
            transform.position = Vector3.MoveTowards(transform.position, _destination.position, _currentSpeed * Time.deltaTime);
            Accelerate();
            RotateInDirection(_destination.position);
    }
    private void Accelerate()
    {
        if (_currentSpeed <= 0) OnStartMove?.Invoke();
            _currentSpeed = Mathf.Clamp(_currentSpeed + _currentAcceleration * Time.deltaTime, 0, _maxMoveSpeed);
    }
    private void Deccelerate()
    {
        _currentSpeed = Mathf.Clamp(_currentSpeed - Time.deltaTime * _deccelerationMultiplier, 0, _maxMoveSpeed);
        if (_currentSpeed <= 0)
        {
            OnStop?.Invoke();
            SetDirection(Direction.None);
        }
    }
    public void SetDirection(Direction dir)
    {
        var directionVector = _directionVector.normalized;
        if (dir == Direction.Forward) directionVector = Vector3.forward;
        if (dir == Direction.Backward) directionVector = -Vector3.forward;
        if (dir == Direction.Left) directionVector = -Vector3.right;
        if (dir == Direction.Right) directionVector = Vector3.right;
        if (dir == Direction.None) directionVector = Vector3.zero;
        _directionVector += directionVector;
        _direction = dir;
        _directionVector.Normalize();
    }

    public void SetDestination(Vector3 destination)
    {
        GameObject point = new();
        point.transform.position = destination;
       var gO = Instantiate(point);
        _destination = gO.transform;
    }
    public void SetDestination(Transform destination)
    {
        _destination = destination;      
    }
    public bool IsPositionNearby(Vector3 position, float range)
    {
        var rangeCheck = Vector3.Distance(transform.position, position);
        if (rangeCheck < range) return true;
        else return false;
    }
    public void ResetDestination()
    {
        _destination = null;
        _directionVector = Vector3.zero;
        FullStop();
    }
    public void ForceRotate(Transform destination)
    {
        transform.rotation = Quaternion.LookRotation(destination.position);
    }
    public void RotateInDirection(Vector3 direction)
    {
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, _rotationSpeed * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
    public void FullStop()
    {
        _currentSpeed = 0;
        Deccelerate();
    }
    private void OnDestroy()
    {
        OnStartMove = null;
        OnStop = null;
    }
}




public enum Direction
{
    Forward,
    Backward,
    Left,
    Right,
    None,
}
