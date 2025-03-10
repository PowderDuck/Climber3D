using System.Collections.Generic;
using UnityEngine;

namespace Climber3D.Entities
{
    public abstract class EntityHandler<TEntity> : MonoBehaviour where TEntity : MonoBehaviour
    {
        protected ICollection<TEntity> Entities { get; private set; } = new List<TEntity>();
    }
}
