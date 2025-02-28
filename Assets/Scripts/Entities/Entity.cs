using UnityEngine;

namespace Climber3D.Entities
{
    public abstract class Entity<TEntity> : MonoBehaviour where TEntity : MonoBehaviour
    {
        public TEntity Target { get; private set; } = default!;

        private void Awake()
        {
            Target = GetComponent<TEntity>();
            Debug.Log($"[!] Found : {Target.gameObject.name}");
        }

        public abstract void EntityUpdate();
    }
}
