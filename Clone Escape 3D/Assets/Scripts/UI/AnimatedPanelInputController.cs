using UnityEngine;

namespace CloneEscape.UI.Panel
{
    public class AnimatedPanelInputController : AnimatedPanelController
    {
        [SerializeField]
        private string panelAxis;

        private bool isOpened = false;

        private void Update()
        {
            if (Input.GetButtonDown(panelAxis))
            {
                if (isOpened)
                {
                    ClosePanel();
                }
                else
                {
                    OpenPanel();
                }

                isOpened = !isOpened;
                OnOpenStateChange?.Invoke(isOpened);
            }
        }
    }
}