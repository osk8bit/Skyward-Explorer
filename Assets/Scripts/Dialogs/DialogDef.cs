using System.IO.Enumeration;
using UnityEngine;

namespace Assets.Scripts.Dialogs
{
    [CreateAssetMenu(menuName = "Defs/Dialog", fileName = "Dialog")]
    public class DialogDef : ScriptableObject
    {
        [SerializeField] private DialogData _data;
        public DialogData Data => _data;
    }
}
