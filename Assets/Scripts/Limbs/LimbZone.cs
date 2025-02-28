using Climber3D.Events;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Climber3D.Limbs
{
    public class LimbZone : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField] private LimbType _limbType = default!;

        public delegate void LimbDragBeginEvent(
            object sender, LimbDragBeginEventArgs eventArgs);
        public event LimbDragBeginEvent? LimbDragBegin;

        public delegate void LimbDragEndEvent(object sender, LimbDragEndEventArgs eventArgs);
        public event LimbDragEndEvent? LimbDragEnd;

        public void OnBeginDrag(PointerEventData eventData)
        {
            LimbDragBegin?.Invoke(this, new(_limbType));
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            LimbDragEnd?.Invoke(this, new());
        }

        public void OnDrag(PointerEventData eventData) { }

    }
}
