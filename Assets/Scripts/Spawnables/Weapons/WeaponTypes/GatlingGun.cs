using System.Collections.Generic;
using UnityEngine;

namespace Spawnables.Projectiles.WeaponTypes
{
    public class GatlingGun : Weapon
    {
        public float shiftTime;
        
        private List<IShootable> _gunHeads = new();
        private int _gunHeadIndex = 0;
        private int _gunHeadIncrement = 1;
        
        private float _timeOfLastShot = 0;

        protected override void DetectShootables()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                IShootable shootable = transform.GetChild(i).GetComponent<IShootable>();
                if (shootable == null) continue;
                
                _gunHeads.Add(shootable);
            }
        }
        
        protected override void TryShooting()
        {
            if (Time.time - _timeOfLastShot <= shiftTime) return;

            ShiftGunHeadIndex();
            _gunHeads[_gunHeadIndex].OnShoot();
            Debug.Log((_gunHeads[_gunHeadIndex] as MonoBehaviour).gameObject.name);
            _timeOfLastShot = Time.time;
        }
        
        private void ShiftGunHeadIndex()
        {
            _gunHeadIndex += _gunHeadIncrement;
            
            if (_gunHeadIndex <= 0 || _gunHeadIndex >= _gunHeads.Count - 1)
            {
                _gunHeadIncrement *= -1;
            }
        }
    }
}