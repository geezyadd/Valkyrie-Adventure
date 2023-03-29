using Player;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Player
{
    public class ExternalDevicesInputReader : IEtityInputSource
    {
        public float _direction => Input.GetAxisRaw("Horizontal"); 
        public bool Jump { get; private set; }
        public bool Attack { get; private set; }
        public void OnUpdate()
        {
            if (Input.GetButtonDown("Jump"))
            {
                Jump = true;
            }
            if (IsPointerOverUi() && Input.GetKeyDown(KeyCode.E))
            {
                Attack = true;
            }
        }
        private bool IsPointerOverUi() => EventSystem.current.IsPointerOverGameObject();
        public void ResetOneTimeActions() 
        {
            Jump = false;
            Attack = false;
        }
    }
}