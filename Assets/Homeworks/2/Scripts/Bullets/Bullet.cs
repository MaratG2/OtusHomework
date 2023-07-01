using ShootEmUp.Pool;
using UnityEngine;
using Zenject;

namespace ShootEmUp.Bullets
{
    public sealed class Bullet : MonoBehaviour
    {
        private int _damage;
        private Rigidbody2D _rb2d;
        private SpriteRenderer _spriteRenderer;
        private PoolObject<Bullet> _poolObject;

        [Inject]
        private void Construct(Rigidbody2D rb2d, SpriteRenderer spriteRenderer)
        {
            this._rb2d = rb2d;
            this._spriteRenderer = spriteRenderer;
        }

        public void Init(BulletSystem.Args args, PoolObject<Bullet> poolObject)
        {
            this._poolObject = poolObject;
            transform.position = args.position;
            _spriteRenderer.color = args.color;
            gameObject.layer = args.physicsLayer;
            _damage = args.damage;
            _rb2d.velocity = args.velocity;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            _poolObject.EnPool(this);
            if (collision.gameObject.TryGetComponent(out HitPointsComponent hitPoints))
                hitPoints.TakeDamage(_damage);
        }
    }
}