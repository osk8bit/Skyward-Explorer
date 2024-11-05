using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.GoBased
{
    public class DestroyObjectComponent : MonoBehaviour
    {
        [SerializeField] private GameObject _objectToDedstroy;

        public void DestroyObject()
        {
            Destroy(_objectToDedstroy);
        }
    }
}
