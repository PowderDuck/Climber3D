using UnityEngine;

namespace Climber3D.Entities
{
    public abstract class Entity<TEntity> : MonoBehaviour where TEntity : MonoBehaviour
    {
        public TEntity Target { get; private set; } = default!;

        protected EntityHandler<TEntity> Handler { get; private set; } = default!;

        protected virtual void Awake()
        {
            Target = GetComponent<TEntity>();
            Handler = FindObjectOfType<EntityHandler<TEntity>>();
            Debug.Log($"[!] Found : {Target.gameObject.name}");
        }

        protected virtual void OnDisable()
        {

        }

        public abstract void EntityUpdate();
    }
}
