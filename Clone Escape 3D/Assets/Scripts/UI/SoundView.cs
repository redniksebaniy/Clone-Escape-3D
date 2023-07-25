using UnityEngine;
using UnityEngine.UI;

public class SoundView : MonoBehaviour
{
    [SerializeField]
    private Sprite onSprite;

    [SerializeField]
    private Sprite offSprite;

    private Image image;

    private bool isMuted;

    private void Start()
    {
        image = transform.GetChild(0).GetComponent<Image>();
        isMuted = AudioManager.Instance.IsMuted;
        ChangeView();
    }

    public void ChangeView()
    {
        image.sprite = isMuted ? offSprite : onSprite;
        isMuted = !isMuted;
    }
}
