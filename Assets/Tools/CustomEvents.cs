using System;
using UnityEngine;
using UnityEngine.Events;

namespace Tools
{
    [Serializable] public class GameObjectEvent : UnityEvent<GameObject> {}
}