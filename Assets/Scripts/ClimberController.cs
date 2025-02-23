using System.Collections.Generic;
using Climber3D.Events;
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

        private IDictionary<Vector2, Limb> _limbs = new Dictionary<Vector2, Limb>();

        private float _currentWaitTime = 0f;
        private bool _isDragging = false;
        private Vector3 _initialDragPosition = Vector3.zero;

        private Vector2 _cameraDimensions = Vector2.zero;

        private void Start()
        {
            _cameraDimensions = new(Camera.main.pixelWidth, Camera.main.pixelHeight);

            _limbs = new Dictionary<Vector2, Limb>
            {
                { new(0, 0), _leftLeg },
                { new(1, 0), _rightLeg },
                { new(0, 1), _leftHand },
                { new(1, 1), _rightHand }
            };
        }

        private void Update()
        {
            DragControl();
        }

        // TODO : substitute _limbs = Dictionary<LimbType, Limb>();
        private void OnDragBegin(object sender, LimbDragBeginEventArgs eventArgs)
        {
            _isDragging = true;
            _initialDragPosition = Input.mousePosition;
            _currentWaitTime = _maxWait;
        }

        private void DragControl()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _isDragging = true;
                _initialDragPosition = Input.mousePosition;
                _currentWaitTime = _maxWait;

                return;
            }

            if (_isDragging && _currentWaitTime > 0)
            {
                _currentWaitTime -= Time.deltaTime;
                var direction = Input.mousePosition - _initialDragPosition;

                if (direction.magnitude >= _activationRadius
                    || _currentWaitTime <= 0
                    || Input.GetMouseButtonUp(0))
                {
                    var screenSector = GetScreenSector(_initialDragPosition);
                    if (!_limbs.TryGetValue(screenSector, out var limb))
                    {
                        throw new KeyNotFoundException($"Limb at Screen Sector {screenSector} is Not Found");
                    }
                    limb.InitiateDrag(direction);

                    _isDragging = false;
                    _initialDragPosition = Vector3.zero;
                }
            }
        }

        private Vector2 GetScreenSector(Vector3 mousePosition)
        {
            return new()
            {
                x = Mathf.Min(
                    Mathf.Floor(mousePosition.x / (_cameraDimensions.x / 2f)), 1),
                y = Mathf.Min(
                    Mathf.Floor(mousePosition.y / (_cameraDimensions.y / 2f)), 1)
            };
        }
    }
}