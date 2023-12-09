using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController _characterController;
    private CameraFacingSprite _cameraFacingSprite;
    private Vector2 _input;
    private Vector3 _direction;
    private float _gravity = -9.81f;
    [SerializeField] private float _gravityMultiplier = 1.0f;
    private float _velocity;
    [HideInInspector]
    public ObservableClasses.ObservableFloat StepsTaken = new ObservableClasses.ObservableFloat() { Value = 0 };
    [HideInInspector]
    public ObservableClasses.ObservableFloat StepsTakenInGrass = new ObservableClasses.ObservableFloat() { Value = 0 };
    [HideInInspector]
    public bool IsInTallGrass;
    [HideInInspector]
    public List<DialogInteractable> ObjectsInDialogRange = new List<DialogInteractable>();
    public float WalkSpeed = 10;
    public float RunSpeed = 20;
    private float _facing = 180.0f;
    [HideInInspector]
    public bool IsRunning = false;
    [HideInInspector]
    public float PreviousSubmitValue = 0;
    public ObservableClasses.ObservableBoolean SubmitPressedAndReleased = new ObservableClasses.ObservableBoolean() { Value = false };
    public List<Flag> Flags = new List<Flag>();
    public static Party Party = new Party();

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _cameraFacingSprite = GetComponentInChildren<CameraFacingSprite>();
        HandleInteractions();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HandleInput();
        if (GameStateManager.GetState() == GameStateManager.GameStates.ROAMING)
        {
            HandleMovement();
        }
    }

    private void HandleInput()
    {
        float submitValue = Input.GetAxis("Submit");
        var verticalInput = Input.GetAxis("Vertical");
        var horizontalInput = Input.GetAxis("Horizontal");
        _input = new Vector2(horizontalInput, verticalInput);
        _direction = new Vector3(_input.x, 0, _input.y);
        if (submitValue > 0 && PreviousSubmitValue < 1)
        {
            SubmitPressedAndReleased.SetValue(true);
        }
        else
        {
            SubmitPressedAndReleased.SetValue(false);
        }
        PreviousSubmitValue = submitValue;
    }

    private void HandleInteractions()
    {
        SubmitPressedAndReleased.OnChanged += HandleSubmitPressedAndReleased();
    }

    private Action HandleSubmitPressedAndReleased()
    {
        return () =>
        {
            switch (GameStateManager.GetState())
            {
                case GameStateManager.GameStates.ROAMING:
                    {
                        if (SubmitPressedAndReleased.Value == true)
                        {
                            if (ObjectsInDialogRange.Count > 0)
                            {
                                HandleDialog();
                            }
                        }
                    }
                    break;
                case GameStateManager.GameStates.DIALOG:
                    {
                        if (SubmitPressedAndReleased.Value == true)
                        {
                            ServiceLocator.Instance.DialogManager.DisplayNextDialog();
                        }
                    }
                    break;
            }
        };
    }

    private void HandleDialog()
    {
        GameStateManager.SetState(GameStateManager.GameStates.DIALOG);
        DialogInteractable dialogSource = null;
        float minDistance = 1000;
        foreach (DialogInteractable dialogInteractable in ObjectsInDialogRange)
        {
            float distanceX = Math.Abs(dialogInteractable.gameObject.transform.position.x - gameObject.transform.position.x);
            float distanceY = Math.Abs(dialogInteractable.transform.position.z - gameObject.transform.position.z);
            if (distanceX + distanceY < minDistance)
            {
                dialogSource = dialogInteractable;
            }
        }
        Debug.Log("Player is Talking to " + dialogSource);
        dialogSource.StartDialog();
    }

    private void HandleMovement()
    {
        HandleGravity();
        HandleRotation();
        _characterController.Move(_direction * Time.deltaTime * (IsRunning ? RunSpeed : WalkSpeed));
        TrackMovement();
    }

    private void HandleGravity()
    {
        if (_characterController.isGrounded && _velocity < 0.0f)
        {
            _velocity = -1.0f;
        } else
        {
            _velocity += _gravity * _gravityMultiplier * Time.deltaTime;
        }
        _direction.y = _velocity;
    }

    private void HandleRotation()
    {
        if (_input.sqrMagnitude == 0)
        {
            return;
        }

        _facing = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg;
        _cameraFacingSprite.ApplyRotation(_facing);
    }

    private void TrackMovement()
    {
        var oldPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        float moved = Math.Abs(transform.position.x - oldPosition.x) + Math.Abs(transform.position.z - oldPosition.z);
        if (moved > 0)
        {
            if (IsInTallGrass)
            {
                StepsTakenInGrass.SetValue(StepsTakenInGrass.Value + moved);
            }
            StepsTaken.SetValue(StepsTaken.Value + moved);
        }
    }

    public void EnterDialogRange(DialogInteractable gameObject)
    {
        ObjectsInDialogRange.Add(gameObject);
        Debug.Log("Player Entered my Dialog range");
    }

    internal void ExitDialogRange(DialogInteractable dialogInteractable)
    {
        ObjectsInDialogRange.Remove(dialogInteractable);
    }
}
