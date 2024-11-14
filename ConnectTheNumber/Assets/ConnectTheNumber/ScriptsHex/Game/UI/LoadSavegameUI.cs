using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Ilumisoft.Hex
{
    public class LoadSavegameUI : MonoBehaviour
    {
        enum State
        {
            Waiting,
            Confimred,
            Cancelled
        }
        
        State state;

        public Coroutine Execute(UnityAction okAction, UnityAction refuseAction)
        {
            return StartCoroutine(ProcessUserInput(okAction, refuseAction));
        }

        public IEnumerator ProcessUserInput(UnityAction okAction, UnityAction refuseAction)
        {
            state = State.Waiting;

            while (state == State.Waiting)
            {
                yield return null;
            }

            switch(state)
            {
                case State.Confimred:
                    okAction?.Invoke();      
                    break;
                case State.Cancelled:   
                    refuseAction?.Invoke(); 
                    break;
            }
        }

        public void Confirm()
        {
            ColorConnectAudioController.Instance.PlaySound(ColorConnectAudioController.Instance.click);
            state = State.Confimred;
        }

        public void Cancel()
        {
            ColorConnectAudioController.Instance.PlaySound(ColorConnectAudioController.Instance.click);

            state = State.Cancelled;
        }
    }
}