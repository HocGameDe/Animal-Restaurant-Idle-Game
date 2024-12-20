#if USING_CINEMACHINE
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;
    public CinemachineVirtualCamera cam;
    public PlayerLevel playerLevel;
    public float scale = 1;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        cam.m_Lens.FieldOfView = scale * (1 + (playerLevel.SizeRatio - 1) * 0.5f);
    }

    public void AdjustCameraSize()
    {
        float newFOV = scale * (1 + (playerLevel.SizeRatio - 1) * 0.5f);
        float oldFOV = cam.m_Lens.FieldOfView;
        DOVirtual.Float(oldFOV, newFOV, 1f, (float f) =>
        {
            cam.m_Lens.FieldOfView = f;
        }).SetEase(Ease.OutBack);
    }
}
#endif