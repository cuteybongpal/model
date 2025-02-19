using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserController : MonoBehaviour
{
    public float maxXAngle;
    public float minXAngle;

    Func<Block> spawnBlock;
    Action<Block> deSpawnBlock;

    Action<Block> addBlock;
    Action buildComplete;
    void Start()
    {
        spawnBlock = Singleton<ObjectManager>.GetFunction<Func<Block>>((int)ObjectManager.MethodNum.BlockSpawn);
        deSpawnBlock = Singleton<ObjectManager>.GetFunction<Action<Block>>((int)ObjectManager.MethodNum.BlockDeSpawn);

        addBlock = Singleton<ModelManager>.GetFunction<Action<Block>>((int)ModelManager.MethodNum.Add);
        buildComplete = Singleton<ModelManager>.GetFunction<Action>((int)ModelManager.MethodNum.CompleteModel);

        if (spawnBlock == null)
            Debug.Log("스폰 널");
        if (deSpawnBlock == null)
            Debug.Log("디스폰 널");

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
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            buildComplete?.Invoke();
        }
        transform.position += moveDirection.normalized * Time.deltaTime * 10;
    }

    //블럭 설치와 미리보기 기능
    private async void PreviewAndPlaceBlock()
    {

        while (isActiveAndEnabled && Application.isPlaying)
        {
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
            

            Block _block = spawnBlock?.Invoke();
            Color _color = _block.Color;
            _color.a = .5f;
            _block.Color = _color;
            _block.transform.position = hitPos;
            await UniTask.Yield();
            if (Input.GetMouseButtonUp(0))
            {
                Color color = _block.Color;
                color.a = 1f;
                _block.Color = color;
                Vector3Int blockPos = new Vector3Int((int)hitPos.x, (int)hitPos.y, (int)hitPos.z);
                Debug.Log(blockPos);
                _block.Pos = blockPos;
                addBlock?.Invoke(_block);
                _block = null;
                continue;
            }
            deSpawnBlock?.Invoke(_block);
        }
    }
}
