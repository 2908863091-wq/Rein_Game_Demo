using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance {  get; private set; } 

    public event EventHandler OnInterAct;
    public event EventHandler TestAct;
    public event EventHandler Rush;
    public event EventHandler Theworld;
    private GameControl gamecontrol;

    private void Awake()
    {
        Instance = this;

        gamecontrol = new GameControl();
        gamecontrol.Player.Enable();

        gamecontrol.Player.InterAct.performed += InterAct_Performed;
        gamecontrol.Player.Test.performed += TestAct_Performed;
        gamecontrol.Player.Rush.performed += Rush_Performed;
        gamecontrol.Player.Theworld.performed += Theworld_performed;
    }
    private void OnDestroy()
    {
        gamecontrol.Player.InterAct.performed -= InterAct_Performed;
        gamecontrol.Player.Test.performed -= TestAct_Performed;
        gamecontrol.Player.Rush.performed -= Rush_Performed;
        gamecontrol.Player.Theworld.performed -= Theworld_performed;

        gamecontrol.Dispose();
    }

    private void Theworld_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Theworld?.Invoke(this, EventArgs.Empty);
    }
    private void InterAct_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInterAct?.Invoke(this,EventArgs.Empty);
    }
    private void TestAct_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        TestAct?.Invoke(this, EventArgs.Empty);
    }
    private void Rush_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Rush?.Invoke(this, EventArgs.Empty);
    }

    public Vector3 GetDirection()
    {
        Vector2 inputvector2 = gamecontrol.Player.Move.ReadValue<Vector2>();

        Vector3 direction = new Vector3(inputvector2.x, 0, inputvector2.y);

        direction = direction.normalized;
        return direction;
    }
}
