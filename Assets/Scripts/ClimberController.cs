using System.Collections.Generic;
using Climber3D.Events;
using Climber3D.Limbs;
using UnityEngine;

namespace Climber3D
{
    public class ClimberController : MonoBehaviour
    {
        [SerializeField] private Limb _leftHand = default!;
        [SerializeField] private Limb _rightHand = default!;
        [SerializeField] private Limb _leftLeg = default!;
        [SerializeField] private Limb _rightLeg = default!;

        [SerializeField] private float _activationRadius = 5f;
        [SerializeField] private float _maxWait = 0.5f;

        private IDictionary<LimbType, Limb> _limbs = new Dictionary<LimbType, Limb>();

        private float _currentWaitTime = 0f;
        private bool _isDragging = false;
        private Vector3 _initialDragPosition = Vector3.zero;

        private LimbType? _currentLimb = null;

        private void Start()
        {
            _limbs = new Dictionary<LimbType, Limb>
            {
                { LimbType.LEFT_LEG, _leftLeg },
                { LimbType.RIGHT_LEG, _rightLeg },
                { LimbType.LEFT_HAND, _leftHand },
                { LimbType.RIGHT_HAND, _rightHand }
            };
        }

        private void Update()
        {
            DragControl();
        }

        public void OnDragBegin(object sender, LimbDragBeginEventArgs eventArgs)
        {
            _currentLimb = eventArgs.LimbType;
            _isDragging = true;
            _initialDragPosition = Input.mousePosition;
            _currentWaitTime = _maxWait;
        }

        public void OnDragEnd(object sender, LimbDragEndEventArgs eventArgs)
        {
            var direction = Input.mousePosition - _initialDragPosition;
            InitiateDrag(direction);
        }

        private void DragControl()
        {
            if (_isDragging && _currentWaitTime > 0)
            {
                _currentWaitTime -= Time.deltaTime;
                var direction = Input.mousePosition - _initialDragPosition;

                if (direction.magnitude >= _activationRadius
                    || _currentWaitTime <= 0)
                {
                    InitiateDrag(direction);
                }
            }
        }
        private void InitiateDrag(Vector3 direction)
        {
            if (_currentLimb != null)
            {
                if (!_limbs.TryGetValue(_currentLimb.Value, out var limb))
                {
                    throw new KeyNotFoundException(
                        $"Limb with LimbType {_currentLimb} is Not Found");
                }
                limb.InitiateDrag(direction);

                _isDragging = false;
                _initialDragPosition = Vector3.zero;
                _currentLimb = null;
            }
        }

        private void OnGrabbed(object sender, GrabberEnteredEventArgs eventArgs)
        {
            // TODO : Implement the displacement calculation;
        }
    }
}