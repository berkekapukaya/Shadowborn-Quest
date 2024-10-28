using System;
using UnityEngine;

namespace Player
{
    public class SelfDestroy : MonoBehaviour
    {
        private ParticleSystem ps;
        
        private void Awake()
        {
            ps = GetComponent<ParticleSystem>();
        }

        private void Update()
        {
            if (ps != null && !ps.IsAlive())
            {
                DestroySelf();
            }
        }

        public void DestroySelf()
        {
            Destroy(gameObject);
        }
    }
}
