using Cinemachine;
using UnityEngine;

namespace Assets.Scripts.Components.LevelManegment
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class SetFollowComponent : MonoBehaviour
    {
        private CinemachineVirtualCamera vCamera;
        private void Start()
        {
            vCamera = GetComponent<CinemachineVirtualCamera>();
        }

        public void SetFollow(string tag)
        {
            vCamera.Follow = GameObject.FindGameObjectWithTag(tag).transform;
        }
    }
}
