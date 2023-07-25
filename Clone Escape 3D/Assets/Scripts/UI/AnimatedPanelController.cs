using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;


namespace CloneEscape.UI.Panel
{
    [RequireComponent(typeof(RectTransform))]
    public class AnimatedPanelController : MonoBehaviour
    {
        private static float scaleFactor = 0;
        private Rect rect;

        enum OpenSide
        {
            Up, Down, Left, Right
        }

        [SerializeField]
        private OpenSide openSide = OpenSide.Up;

        [SerializeField]
        [Range(0, 5)]
        private float moveTime = 0.25f;

        private Vector3 openedPos;
        private Vector3 closedPos;

        protected UnityEvent<bool> OnOpenStateChange = new();

        private void Start()
        {
            if (scaleFactor == 0)
            {
                scaleFactor = FindObjectOfType<Canvas>().scaleFactor;
            }

            rect = GetComponent<RectTransform>().rect;
            Vector3 deltaPos = OpenSideToVector3(openSide);

            openedPos = closedPos = transform.position;
            openedPos += deltaPos * scaleFactor;
        }

        private Vector3 OpenSideToVector3(OpenSide os) => os switch
        {
            OpenSide.Up => Vector3.up * rect.height,
            OpenSide.Down => Vector3.down * rect.height,
            OpenSide.Right => Vector3.right * rect.width,
            OpenSide.Left => Vector3.left * rect.width,
            _ => Vector3.zero,
        };

        public void OpenPanel()
        {
            transform.DOMove(openedPos, moveTime).SetUpdate(true).SetEase(Ease.OutFlash);
            OnOpenStateChange?.Invoke(true);
        }

        public void ClosePanel()
        {
            transform.DOMove(closedPos, moveTime).SetUpdate(true).SetEase(Ease.InFlash);
            OnOpenStateChange?.Invoke(false);
        }
    }
}
