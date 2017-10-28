using System;
using System.Collections.Generic;
using Assets.Code.Structure;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Code
{
    /// <summary>
    /// Bullet manager for spawning and tracking all of the game's bullets
    /// </summary>
    public class BulletManager : ISaveLoad
    {
        private readonly Transform _holder;

        /// <summary>
        /// Bullet prefab. Use GameObject.Instantiate with this to make a new bullet.
        /// </summary>
        private readonly Object _bullet;

        public BulletManager (Transform holder) {
            _holder = holder;
            _bullet = Resources.Load("Bullet");
        }

        // TODO fill me in
        public void ForceSpawn (Vector2 pos, Quaternion rotation, Vector2 velocity, float deathtime)
        {
            var bulletClone = (GameObject) Object.Instantiate(_bullet, pos, rotation);
            bulletClone.transform.SetParent(_holder);
            bulletClone.GetComponent<Bullet>().Initialize(velocity, Time.time + Bullet.Lifetime);
        }

        #region saveload

        // TODO fill me in
        public GameData OnSave () {
            BulletsData bullets = new BulletsData();
            
            Bullet[] list = (Bullet[]) Object.FindObjectsOfType(typeof(Bullet));
            foreach (Bullet obj in list)
            {
                BulletData bullet = new BulletData(); 
                bullet.Pos = obj.gameObject.GetComponent<Rigidbody2D>().position;
                bullet.Velocity = obj.gameObject.GetComponent<Rigidbody2D>().velocity;
                bullet.Rotation = obj.gameObject.GetComponent<Rigidbody2D>().rotation;
                bullets.Bullets.Add(bullet);
            }
            return bullets;
        }

        // TODO fill me in
        public void OnLoad (GameData data) {
            throw new NotImplementedException();
        }

        #endregion

    }

    /// <summary>
    /// Save data for all bullets in game
    /// </summary>
    public class BulletsData : GameData
    {
        public List<BulletData> Bullets;
    }

    /// <summary>
    /// Save data for a single bullet
    /// </summary>
    public class BulletData
    {
        public Vector2 Pos;
        public Vector2 Velocity;
        public float Rotation;
    }
}