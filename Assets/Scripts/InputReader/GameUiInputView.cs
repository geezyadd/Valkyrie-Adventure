using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUiInputView :MonoBehaviour, IEtityInputSource
{
    [SerializeField] private Joystick _joystick;
    [SerializeField] private Button _jumpButton;
    [SerializeField] private Button _attackButton;
    [SerializeField] private Button _magicButton;
    [SerializeField] private Button _unknownButton;
    public float _direction => JoysticHorizontalNormalized();
    public bool Jump { get; private set; }
    public bool Attack { get; private set; }
    private void Awake()
    {
        _jumpButton.onClick.AddListener(()=> Jump = true);
        _attackButton.onClick.AddListener(() => Attack = true);
        
    }
    private float JoysticHorizontalNormalized() 
    {
        
        if(_joystick.Horizontal > 0f) 
        {
            return 1f;
        }
        if (_joystick.Horizontal < 0f)
        {
            return -1f;
        }
        else 
        {
            return 0f;
        }
        
    }
    private void OnDestroy()
    {
        _jumpButton.onClick.RemoveAllListeners();
        _attackButton.onClick.RemoveAllListeners();
    }
    public void ResetOneTimeActions()
    {
        Jump = false;
        Attack = false;
    }
}
