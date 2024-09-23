using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBound : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    private bool cameraAtLimit = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        print(other.tag);

        if (other.CompareTag("CameraLimit")) // Thẻ của thanh cuối
        {
            print("dfgdf ");
            cameraAtLimit = true;
            virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_XDamping = 0; 
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        print(other.ToString());


        if (other.CompareTag("CameraLimit"))
        {
            print(other.tag);

            cameraAtLimit = false;
            virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_XDamping = 1; 
        }
    }
}
