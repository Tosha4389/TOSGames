using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class OneTouchEvent : UnityEvent<Vector3>
{

}


[Serializable]
public class TwoTouchEvent : UnityEvent<Vector3, Vector3>
{

}


public class EventsManager
{

}
