using System;
using GameStates;
using UnityEngine;
using UnityEngine.Events;

namespace Tools
{
    [Serializable] public class GameObjectEvent : UnityEvent<GameObject> {}
    [Serializable] public class GameStateEvent : UnityEvent<GameState> {}
}