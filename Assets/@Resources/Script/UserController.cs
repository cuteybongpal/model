using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UserController : MonoBehaviour
{
    public float maxXAngle;
    public float minXAngle;
    //test 용도
    public Material Material;
    void Start()
    {
        UserControlLoop().Forget();
        PreviewAndPlaceBlock();
    }
    //유니티 생명주기 함수
    async UniTaskVoid UserControlLoop()
    {
        while (Application.isPlaying && isActiveAndEnabled)
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
        if (Input.GetKey(KeyCode.Mouse1))
        {
            Cursor.lockState = CursorLockMode.Locked;
            if (rotationDelta.x + transform.localRotation.x - Mathf.Abs(minXAngle) > 0)
                rotationDelta.x = rotationDelta.x + transform.localRotation.x - Mathf.Abs(minXAngle);
            else if (rotationDelta.x + transform.localRotation.x - Mathf.Abs(maxXAngle) > 0)
                rotationDelta.x = rotationDelta.x + transform.localRotation.x - Mathf.Abs(maxXAngle);

            transform.localRotation = Quaternion.Euler(transform.rotation.eulerAngles + rotationDelta);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
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
        if ((Input.GetKey(KeyCode.LeftShift)))
        {
            moveDirection -= Vector3.up * Time.deltaTime;
        }
        if ((Input.GetKey(KeyCode.Space)))
        {
            moveDirection += Vector3.up * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.RightShift)){
            ModelManager.Instance.BuildComplete();
        }
        transform.position += moveDirection.normalized * Time.deltaTime * 10;
    }

    //블럭 설치와 미리보기 기능
    private async void PreviewAndPlaceBlock()
    {

        while (isActiveAndEnabled && Application.isPlaying)
        {
            //마우스 커서가 UI요소에 있을 경우 컨티뉴함
            if (EventSystem.current.IsPointerOverGameObject())
            {
                await UniTask.Yield();
                continue;
            }
            //마우스 커서 위치를 레이로 반환해줌
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(transform.position, ray.direction, 100);
            Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));
            Debug.DrawRay(transform.position, ray.direction * 100, Color.red);
            Vector3 hitPos = Vector3.zero;
            if (hits.Length <= 0)
            {
                await UniTask.Yield();
                continue;
            }

            foreach (RaycastHit hit in hits)
            {
                hitPos = hit.point - ray.direction * .001f;
                break;
            }

            hitPos.x = Mathf.Round(hitPos.x);
            hitPos.y = Mathf.Round(hitPos.y);
            hitPos.z = Mathf.Round(hitPos.z);


            Block _block = ObjectManager.Instance.blockManager.Spawn();
            _block.Material = Material;
            _block.Color = new Color(ModelManager.Instance.CurrentColor.r, ModelManager.Instance.CurrentColor.g, ModelManager.Instance.CurrentColor.b, 0.5f);
            _block.transform.position = hitPos;
            await UniTask.Yield();
            if (Input.GetMouseButtonDown(0))
            {
                Color color = _block.Color;
                color.a = 1f;
                _block.Color = color;
                Vector3Int blockPos = new Vector3Int((int)hitPos.x, (int)hitPos.y, (int)hitPos.z);
                _block.Pos = blockPos;
                ModelManager.Instance.Add(_block);
                _block = null;
                continue;
            }
            ObjectManager.Instance.blockManager.DeSpawn(_block);
        }
    }
}
