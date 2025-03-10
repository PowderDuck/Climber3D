using Climber3D.Climbables;
using Climber3D.Events;
using UnityEngine;

namespace Climber3D.Limbs
{
    public class Limb : MonoBehaviour
    {
        [SerializeField] private GameObject _directorLimb = default!;
        [SerializeField] private GameObject _extenderLimb = default!;

        [SerializeField] private Grabber _grabber = default!;

        [SerializeField] private float _extendability = 3f;
        [SerializeField] private float _extensionRate = 1f;

        private Vector3 _direction = Vector3.zero;

        private Vector3 _initialExtenderPosition = Vector3.zero;

        private float _currentExtension = 0f;
        private bool _reverse = true;

        private bool Condition => _reverse ? _currentExtension > 0 : _currentExtension < _extendability;

        private bool IsExtending => _currentExtension > 0;

        private Climbable? _activeClimbable { get; set; } = null;

        public event Grabber.GrabberEnteredEvent? GrabberEntered;

        private void Awake()
        {
            _grabber.SetLimb(this);
            _grabber.GrabberEntered += OnGrabberEntered;
        }

        private void Update() // TODO : Implement Entity.cs => LimbEntity.cs;
        {
            if (Condition)
            {
                _currentExtension += _extensionRate * Time.deltaTime * (_reverse ? -1f : 1f);
                _currentExtension = Mathf.Min(
                    Mathf.Max(0, _currentExtension),
                    _extendability);

                if (_currentExtension >= _extendability)
                {
                    _reverse = true;
                }

                _extenderLimb.transform.localPosition =
                    _initialExtenderPosition + (Vector3.up * _currentExtension);
            }
        }

        public void InitiateDrag(Vector3 direction)
        {
            if (!IsExtending)
            {
                _direction = direction.normalized;
                _reverse = false;
                _directorLimb.transform.rotation = Quaternion.LookRotation(Vector3.forward, _direction);
                _initialExtenderPosition = _extenderLimb.transform.localPosition;
                _activeClimbable?.Released(_grabber);
            }
        }

        private void OnGrabberEntered(object sender, GrabberEnteredEventArgs eventArgs)
        {
            if (IsExtending)
            {
                Debug.Log($"Entered {eventArgs.Target.name}");

                _reverse = true;
                if (eventArgs.Target.Validate())
                {
                    GrabberEntered?.Invoke(this, new(eventArgs.Target));
                    _activeClimbable = eventArgs.Target;
                    _currentExtension = 0;
                }
            }
        }
    }
}