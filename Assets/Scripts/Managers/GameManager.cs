using System.Collections.Generic;
using Climber3D.Limbs;
using UnityEngine;

namespace Climber3D.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private ClimberController _climberController = default!;
        [SerializeField] private List<LimbZone> _limbZones = default!;

        private void Awake()
        {
            _limbZones
                .ForEach(limbZone =>
                {
                    limbZone.LimbDragBegin += _climberController.OnDragBegin;
                    limbZone.LimbDragEnd += _climberController.OnDragEnd;
                });
        }
    }
}
