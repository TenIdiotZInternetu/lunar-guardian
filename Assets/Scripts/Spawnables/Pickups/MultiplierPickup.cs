using System;
using PlayerScripts;
using UnityEngine;

namespace Spawnables.Collectibles
{
    public class MultiplierPickup : Pickup
    {
        protected void OnTriggerExit2D(Collider2D other)
        {
            Debug.Log(other.ToString()); 
            if (!other.gameObject.CompareTag("PlayfieldBorder")) return;
            PlayerStatus.ResetScoreMultiplier();
        }
    }
}