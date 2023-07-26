using UnityEngine;
using DG.Tweening;
using CloneEscape.PlayerBehavours;
using CloneEscape.Audio;
using CloneEscape.LevelBase;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Vector3 distance;

    [SerializeField]
    private float moveTime; 
    
    [SerializeField]
    private AudioClip _moveClip;

    private Transform _playerTransform;

    private bool isFollowing;

    private void Start()
    {
        PlayerSelector.Instance.OnNewPlayerSelected.AddListener(SetFollowingPlayer);
        PlayerSelector.Instance.OnNewPlayerSelected.AddListener((SelectablePlayer _) => AudioManager.Instance.AudioSource.PlayOneShot(_moveClip));

        isFollowing = true;
    }

    private void Update()
    {
        if (!isFollowing || _playerTransform == null) return;

        transform.position = GetNewPosition(_playerTransform);
        transform.LookAt(_playerTransform, Vector3.up);
    }

    private void OnDestroy()
    {
        PlayerSelector.Instance.OnNewPlayerSelected.RemoveListener(SetFollowingPlayer);
        PlayerSelector.Instance.OnNewPlayerSelected.RemoveListener((SelectablePlayer _) => AudioManager.Instance.AudioSource.PlayOneShot(_moveClip));
    }

    public void SetFollowingPlayer(SelectablePlayer newPlayer)
    {
        isFollowing = false;

        Transform _newPlayerTransform = newPlayer.transform;
        Vector3 newPosition = GetNewPosition(_newPlayerTransform);

        transform.DOMove(newPosition, moveTime).SetEase(Ease.InOutFlash).OnComplete(() =>
        {
            _playerTransform = _newPlayerTransform;
            isFollowing = true;
        });
    }

    private Vector3 GetNewPosition(Transform _transform) => new(_transform.position.x, transform.position.y, transform.position.z);
}
