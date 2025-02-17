using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserController : MonoBehaviour
{
    public float maxXAngle;
    public float minXAngle;

    void Start()
    {
        UserControlLoop().Forget();
        PreviewAndPlaceBlock();
    }
    //유니티 생명주기 함수
    async UniTaskVoid UserControlLoop()
    {
        while (true)
        {
            RotateView();
            MoveCharacter();
            await UniTask.Yield();
        }
    }
    //화면 회전
    void RotateView()
    {
        float mouseDeltaX = Input.GetAxis("Mouse X");
        float mouseDeltaY = Input.GetAxis("Mouse Y");

        Vector3 rotationDelta = new Vector3(-mouseDeltaY, mouseDeltaX, 0);

        if (rotationDelta.x + transform.localRotation.x - Mathf.Abs(minXAngle) > 0)
            rotationDelta.x = rotationDelta.x + transform.localRotation.x - Mathf.Abs(minXAngle);
        else if (rotationDelta.x + transform.localRotation.x - Mathf.Abs(maxXAngle) > 0)
            rotationDelta.x = rotationDelta.x + transform.localRotation.x - Mathf.Abs(maxXAngle);

        transform.localRotation = Quaternion.Euler(transform.rotation.eulerAngles + rotationDelta);
    }
    //캐릭터 움직이는 함수
    private void MoveCharacter()
    {
        float angleY = transform.localRotation.eulerAngles.y;
        if (angleY > 180)
            angleY -= 360;

        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            moveDirection += new Vector3(Mathf.Sin(angleY * Mathf.Deg2Rad), 0, Mathf.Cos(angleY * Mathf.Deg2Rad));
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection += new Vector3(Mathf.Sin(angleY * Mathf.Deg2Rad + Mathf.PI / 2), 0, Mathf.Cos(angleY * Mathf.Deg2Rad + Mathf.PI / 2));
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDirection += new Vector3(Mathf.Sin(angleY * Mathf.Deg2Rad + Mathf.PI), 0, Mathf.Cos(angleY * Mathf.Deg2Rad + Mathf.PI));
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection += new Vector3(Mathf.Sin(angleY * Mathf.Deg2Rad - Mathf.PI / 2), 0, Mathf.Cos(angleY * Mathf.Deg2Rad - Mathf.PI / 2));
        }

        transform.position += moveDirection.normalized * Time.deltaTime * 10;
    }

    //블럭 설치와 미리보기 기능
    private async void PreviewAndPlaceBlock()
    {
        RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward, 100);
        Vector3 hitPos = Vector3.zero;
        if (hits.Length > 0)
            return;

        foreach (RaycastHit hit in hits)
        {
            hitPos = hit.point;
            break;
        }
        hitPos.x = Mathf.Round(hitPos.x);
        hitPos.y = Mathf.Round(hitPos.y);
        hitPos.z = Mathf.Round(hitPos.z);
        await UniTask.Yield();
    }
}
