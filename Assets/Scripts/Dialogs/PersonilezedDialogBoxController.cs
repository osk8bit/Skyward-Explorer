using UnityEngine;

namespace Assets.Scripts.Dialogs
{
    public class PersonilezedDialogBoxController : DialogBoxController
    {
        [SerializeField] private DialogContent _right;

        protected override DialogContent CurrentContent => CurrentSentense.Side == Side.Left ? _content : _right;

        protected override void OnStartDialogAnimation()
        {
            _right.gameObject.SetActive(CurrentSentense.Side == Side.Right);
            _content.gameObject.SetActive(CurrentSentense.Side == Side.Left);

            base.OnStartDialogAnimation();
        }

        protected override void OnCloseAnimationComplete()
        {
            _right.gameObject.SetActive(false);
            base.OnCloseAnimationComplete();
        }
    }
}
