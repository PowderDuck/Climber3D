using UnityEngine;

namespace Climber3D
{
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] private GameObject _leftHand;
        [SerializeField] private GameObject _rightHand;

        [SerializeField] private float _reachDistance = 2.5f;

        private void Update()
        {

        }
    }
}