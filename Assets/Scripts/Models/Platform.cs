using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public class Platform
    {
        public string Name { get; set; }
        public GameObject PlatformGameObject { get; set; }
        public Rigidbody2D PlarformRigidbody2D { get; set; }
        public Collider2D PlatformCollider2D { get; set; }
        public Vector2 Velocity { get; set; }

    }
}
