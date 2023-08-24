using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform playerOne;
    public Vector3 offset;
    public float smoothSpeed = 1f;
    Vector3 gizmoPos;

    void Start()
    {
        StartCoroutine(CameraStartDelay());
    }


    void FixedUpdate()
    {
        if (playerOne != null && GameManager.instance.playerList.Count < 2)
        {
            Vector3 desiredPosition = playerOne.transform.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        } else if (GameManager.instance.playerList.Count >= 2)
        {
            Vector3 desiredPosition = FindCentroid() + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
            gizmoPos = desiredPosition - offset;
        }
    }
    Vector3 FindCentroid()
    {
        float totalX = 0f;
        float totalY = 0f;
        float totalZ = 0f;

        foreach (var player in GameManager.instance.playerList)
        {
            totalX += player.transform.parent.position.x;
            totalY += player.transform.parent.position.y;
            totalZ += player.transform.parent.position.z;
        }

        float centerX = totalX / GameManager.instance.playerList.Count;
        float centerY = totalY / GameManager.instance.playerList.Count;
        float centerZ = totalZ / GameManager.instance.playerList.Count;

        return new Vector3(centerX, centerY, centerZ);
    }
    IEnumerator CameraStartDelay()
    {
        yield return new WaitForSeconds(.5f);
        playerOne = GameManager.instance.playerList[0].gameObject.transform;
        transform.position = playerOne.transform.position + offset;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(gizmoPos, 1f);
    }
}
