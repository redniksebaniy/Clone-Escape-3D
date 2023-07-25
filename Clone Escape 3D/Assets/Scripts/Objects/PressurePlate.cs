using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Collider))]
public class PressurePlate : MonoBehaviour
{
    [SerializeField]
    private float deltaY;

    [SerializeField]
    private float moveTime;

    [SerializeField]
    private DoorManager[] doors;

    private Transform plate;
    private float unpressedPosY;
    private float pressedPosY;

    
    private void Start()
    {
        foreach(DoorManager currentDoor in doors)
        {
            currentDoor.IncNeededPlateCount();
        }

        plate = transform.GetChild(0);
        unpressedPosY = plate.position.y;
        pressedPosY = plate.position.y - deltaY;
    }

    private void OnTriggerEnter()
    {
        foreach (DoorManager currentDoor in doors)
        {
            currentDoor.IncCurrentPlateCount();
        }

        plate.DOMoveY(pressedPosY, moveTime);
    }

    private void OnTriggerExit()
    {
        foreach (DoorManager currentDoor in doors)
        {
            currentDoor.DecCurrentPlateCount();
        }

        plate.DOMoveY(unpressedPosY, moveTime);
    }
}
