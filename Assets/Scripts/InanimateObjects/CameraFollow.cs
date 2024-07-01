using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Player player;
    public Vector3 offset;
    public Transform bossPOV;
    public bool isBossFight;

    private Vector3 initialOffset;
    private Vector3 initialPos;

    // Start is called before the first frame update
    void OnEnable()
    {
        initialPos = player.transform.position + offset;
        initialOffset = (transform.position - player.transform.position);
        offset = initialOffset * 0.5f;
        bossPOV = player.bossPOV;
    }

    public void OnInit()
    {
        offset = initialOffset * 1.5f;
    }

    public void OnMainMenu()
    {
        offset = initialOffset * 0.5f;
        transform.position = Vector3.Lerp(transform.position, player.transform.position + offset, 0.9f);
    }

    public void OnBossFight()
    {
        offset = initialOffset * 0.5f;
        transform.DOLocalMove(bossPOV.position, 1f);
        transform.DODynamicLookAt(GameController.Instance.boss.transform.position, 1.5f);
    }

    public void AfterBossFight()
    {
        isBossFight = false;
        offset = new Vector3(0, 10, -6);
        transform.DODynamicLookAt(player.transform.position, 1.5f);
        transform.DOLocalMove(player.transform.position + offset, 1f);
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if (isBossFight)
        {
            return;
        }
        if (player != null)
        {
            Vector3 smoothPos = Vector3.Lerp(transform.position, player.transform.position + new Vector3(0, 10, -6), 0.2f);
            transform.position = smoothPos;
        }
    }
}
