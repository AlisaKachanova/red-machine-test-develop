using System;
using System.Collections;
using UnityEngine;

public class Camera_Handler : MonoBehaviour
{
    public static Action<bool> Change;
    private bool Ok;
    private void Awake()
    {
        Change += ChangeStateOfCamera;
        Ok = true;
    }
    private void OnDestroy()
    {
        Change -= ChangeStateOfCamera;
    }
    private void Update()
    {
        if (Ok && Input.GetKey(KeyCode.Mouse0))
        {
            StopAllCoroutines();
            StartCoroutine(Move(Input.mousePosition));
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
            StopAllCoroutines();
    }
    private IEnumerator Move(Vector3 newPos)
    {
        newPos = UnityEngine.Camera.main.ScreenToWorldPoint(newPos);
        Vector3 pos;
        while (Ok && (transform.position != newPos))
        {
            pos = transform.position;
            transform.position = Vector3.MoveTowards
            (pos, new Vector3(newPos.x, newPos.y, pos.z), Time.deltaTime / (newPos.sqrMagnitude * 0.003f));
            yield return null;
        }
        Ok = false;
    }

    private void ChangeStateOfCamera(bool ok) => Ok = ok;
}
