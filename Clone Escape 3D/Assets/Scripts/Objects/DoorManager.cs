using UnityEngine;
using DG.Tweening;

public class DoorManager : MonoBehaviour
{
    [SerializeField]
    private float deltaY;

    [SerializeField]
    private float moveTime;

    private uint neededPlateCount = 0;
    private uint currentPlateCount = 0;

    private float closedPosY;
    private float openedPosY;

    [SerializeField]
    private AudioClip _openClip;

    [SerializeField]
    private AudioClip _closeClip;

    private void Start()
    {
        closedPosY = transform.position.y;
        openedPosY = transform.position.y + deltaY;
    }

    public void IncNeededPlateCount() => neededPlateCount++;

    public void IncCurrentPlateCount()
    {
        if (++currentPlateCount == neededPlateCount)
        {
            OpenDoor();
        }
    }

    public void DecCurrentPlateCount()
    {
        if (currentPlateCount-- == neededPlateCount)
        {
            CloseDoor();
        }
    }

    private void OpenDoor()
    {
        transform.DOMoveY(openedPosY, moveTime).SetEase(Ease.InExpo);
        AudioManager.Instance.AudioSource.PlayOneShot(_openClip);
    }

    private void CloseDoor()
    {
        transform.DOMoveY(closedPosY, moveTime).SetEase(Ease.OutBounce);
        AudioManager.Instance.AudioSource.PlayOneShot(_closeClip);
    }
}
