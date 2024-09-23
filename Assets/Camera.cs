//using Cinemachine;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Camera : MonoBehaviour
//{
//    public CinemachineVirtualCamera virtualCamera; // Tham chiếu đến Virtual Camera
//    private bool cameraAtLimit = false;

//    private void OnTriggerEnter2D(Collider2D other)
//    {
//        if (other.CompareTag("CameraLimit")) // Thẻ của thanh cuối
//        {
//            cameraAtLimit = true;
//            virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_XDamping = 0; // Dừng camera
//            // Bạn có thể thêm logic khác nếu cần
//        }
//    }

//    private void OnTriggerExit2D(Collider2D other)
//    {
//        if (other.CompareTag("CameraLimit"))
//        {
//            cameraAtLimit = false;
//            virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_XDamping = 1; // Khôi phục camera
//            // Hoặc thêm logic khác nếu cần
//        }
//    }
//}
