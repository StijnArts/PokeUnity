using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed;
    private float verticalInput;
    private float horizontalInput;
    [HideInInspector]
    public ObservableClasses.ObservableFloat stepsTaken = new ObservableClasses.ObservableFloat() { Value = 0 };
    [HideInInspector]
    public ObservableClasses.ObservableFloat stepsTakenInGrass = new ObservableClasses.ObservableFloat() { Value = 0 };
    [HideInInspector]
    public bool isInTallGrass;
    [HideInInspector]
    public List<DialogInteractable> objectsInDialogRange = new List<DialogInteractable>();
    public float previousSubmitValue = 0;
    public ObservableClasses.ObservableBoolean submitPressedAndReleased = new ObservableClasses.ObservableBoolean() { Value = false };
    public List<Flag> flags = new List<Flag>();
    public static Party party = new Party();

    // Start is called before the first frame update
    void Start()
    {
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
        if (submitValue > 0 && previousSubmitValue < 1)
        {
            submitPressedAndReleased.SetValue(true);
        }
        else
        {
            submitPressedAndReleased.SetValue(false);
        }
        previousSubmitValue = submitValue;
    }

    private void HandleInteractions()
    {
        submitPressedAndReleased.OnChanged += HandleSubmitPressedAndReleased();
    }

    private Action HandleSubmitPressedAndReleased()
    {
        return () =>
        {
            switch (GameStateManager.GetState())
            {
                case GameStateManager.GameStates.ROAMING:
                    {
                        if (submitPressedAndReleased.Value == true)
                        {
                            if (objectsInDialogRange.Count > 0)
                            {
                                HandleDialog();
                            }
                        }
                    }
                    break;
                case GameStateManager.GameStates.DIALOG:
                    {
                        if (submitPressedAndReleased.Value == true)
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
        foreach (DialogInteractable dialogInteractable in objectsInDialogRange)
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
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        float oldXPosition = transform.position.x;
        float oldZPosition = transform.position.z;
        transform.Translate(Vector3.forward * Time.deltaTime * walkSpeed * verticalInput);
        transform.Translate(Vector3.right * Time.deltaTime * walkSpeed * horizontalInput);
        float moved = Math.Abs(transform.position.x - oldXPosition) + Math.Abs(transform.position.z - oldZPosition);
        if (isInTallGrass)
        {
            stepsTakenInGrass.SetValue(stepsTakenInGrass.Value + moved);
        }
        stepsTaken.SetValue(stepsTaken.Value + moved);
    }

    public void EnterDialogRange(DialogInteractable gameObject)
    {
        objectsInDialogRange.Add(gameObject);
        Debug.Log("Player Entered my Dialog range");
    }

    internal void ExitDialogRange(DialogInteractable dialogInteractable)
    {
        objectsInDialogRange.Remove(dialogInteractable);
    }
}
