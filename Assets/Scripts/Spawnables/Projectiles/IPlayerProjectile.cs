using System;

namespace Projectiles
{
    public interface IPlayerProjectile
    {
        public void OnPlayerShoots(object sender, EventArgs e);
    }
}