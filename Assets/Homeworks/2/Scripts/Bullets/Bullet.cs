using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp.Bullets
{
    public sealed class Bullet : MonoBehaviour
    {
        public event Action<Bullet, Collision2D> OnCollisionEntered;
        [NonSerialized] public bool isPlayer;
        [NonSerialized] public int damage;
        private Rigidbody2D _rb2d;
        private SpriteRenderer _spriteRenderer;

        [Inject]
        private void Construct(Rigidbody2D rb2d, SpriteRenderer spriteRenderer)
        {
            this._rb2d = rb2d;
            this._spriteRenderer = spriteRenderer;
        }

        public void Init(BulletSystem.Args args)
        {
            transform.position = args.position;
            _spriteRenderer.color = args.color;
            gameObject.layer = args.physicsLayer;
            damage = args.damage;
            isPlayer = args.isPlayer;
            _rb2d.velocity = args.velocity;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            OnCollisionEntered?.Invoke(this, collision);
        }
    }
}