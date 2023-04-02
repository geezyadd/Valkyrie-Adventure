using Player;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Player
{
    public class ExternalDevicesInputReader : IEtityInputSource, IDisposable
    {
        public float _direction => Input.GetAxisRaw("Horizontal"); 
        public bool Jump { get; private set; }
        public bool Attack { get; private set; }
        public ExternalDevicesInputReader() 
        {
            ProjectUpdater.Instance.UpdateCalled += OnUpdate;
        }
        public void ResetOneTimeActions() 
        {
            Jump = false;
            Attack = false;
        }
        public void Dispose() => ProjectUpdater.Instance.UpdateCalled -= OnUpdate;  
        private void OnUpdate()
        {
            if (Input.GetButtonDown("Jump"))
            {
                Jump = true;
            }
            if ( Input.GetButtonDown("Fire2"))
            {
                Attack = true;
            }
        }
        private bool IsPointerOverUi() => EventSystem.current.IsPointerOverGameObject();
    }
}