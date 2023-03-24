using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;

public class ThridPersonShooterController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private Transform projectile;
    [SerializeField] private Transform spawnProjectilePosition;
    [SerializeField] private Canvas TutorialCanvas;
    [SerializeField] private Canvas QuestCanvas;
    [SerializeField] private Transform QuestAccepted;
    public Animator animator;

    private StarterAssetsInputs starterAssetsInputs;
    private ThirdPersonController thirdPersonController;
    private void Awake()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        thirdPersonController= GetComponent<ThirdPersonController>();

        StartCoroutine(StartCountdown());
        StartCoroutine(StartCountdownQuest());


    }

    private void Update()
    {
        Vector3 mouseWorldPosition = Vector3.zero; ;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);

        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            mouseWorldPosition = raycastHit.point;
            
        }

        if (starterAssetsInputs.aim)
        {
            aimVirtualCamera.gameObject.SetActive(true);
            thirdPersonController.SetRotateOnMove(false);

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        }
        else
        {
            aimVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.SetRotateOnMove(true);
        }

        if(starterAssetsInputs.shoot)
        {
            animator.SetTrigger("Attack");
            Vector3 aimDir = (mouseWorldPosition - spawnProjectilePosition.position).normalized;
            Instantiate(projectile, spawnProjectilePosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
            starterAssetsInputs.shoot = false;
        }



    }

    public IEnumerator StartCountdown(float countdownValue = 10)
    {
        yield return new WaitForSeconds(10.0f);
        TutorialCanvas.gameObject.SetActive(false);
        QuestCanvas.gameObject.SetActive(true);
        QuestAccepted.gameObject.SetActive(false);
    }

    public IEnumerator StartCountdownQuest(float countdownValue = 10)
    {
        yield return new WaitForSeconds(0.01f);
        QuestCanvas.gameObject.SetActive(false);

    }
}
