using UnityEngine;

namespace _SecondGameJam.Scripts.Core
{
    [RequireComponent(typeof(Camera))]
    public class SetResolution : MonoBehaviour
    {
        private const float TARGET_WIDTH = 1920f;
        private const float TARGET_HEIGHT = 1080f;
        private const float TARGET_ASPECT_RATIO = TARGET_WIDTH / TARGET_HEIGHT;

        private void Start()
        {
            var mainCamera = gameObject.GetComponent<Camera>();

            float ratio = (float)Screen.width / Screen.height;
            if (ratio >= TARGET_ASPECT_RATIO)
            {
                mainCamera.orthographicSize = TARGET_HEIGHT / 200f;
            }
            else
            {
                float scaledHeight = TARGET_WIDTH / ratio;
                mainCamera.orthographicSize = scaledHeight / 200f;
            }
        }
    }
}