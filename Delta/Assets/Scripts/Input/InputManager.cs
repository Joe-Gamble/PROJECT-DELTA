using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;

    public static InputManager Instance
    {
        get{
            return _instance;
        }
    }
    private PlayerControls playerControls;

    private void Awake() {
        if(_instance != null && _instance != this){
            Destroy(this.gameObject);
        }
        else{
            _instance = this;
        }
        playerControls = new PlayerControls();
    }

    private void OnEnable() {
        playerControls.Enable();
    }

    private void OnDisable() {
        playerControls.Disable();
    }

    ///<summary> Returns the current player movement inputs </summary>
    public Vector2 GetPlayerMovement(){
        return playerControls.Player.Movement.ReadValue<Vector2>();
    }

    ///<summary> Returns the current player mouse inputs </summary>
    public Vector2 GetMouseDelta(){
        return playerControls.Player.Look.ReadValue<Vector2>();
    }

    ///<summary> Returns true on the frame the player jumps </summary>
    public bool Jumping(){
        return playerControls.Player.Jump.triggered;
    }

        ///<summary> Returns true on the frame the player presses the ability 1 key </summary>
    public bool Interact(){
        return playerControls.Player.Ability1.triggered;
    }

    public bool SprintDown(){
        return playerControls.Player.Sprint.triggered;
    }

     ///<summary> Returns the current state of the left mouse button </summary>
    public bool OnLeftClick(){
        return Mouse.current.leftButton.isPressed;
    }

    ///<summary> Returns true on the frame left click is pressed </summary>
    public bool OnLeftClickDown(){
        return Mouse.current.leftButton.wasPressedThisFrame;
    }

    ///<summary> Returns true on the frame left click is released </summary>
    public bool OnLeftClickUp(){
        return Mouse.current.leftButton.wasReleasedThisFrame;
    }

    ///<summary> Returns the current state of the right mouse button </summary>
    public bool OnRightClick(){
        return Mouse.current.rightButton.isPressed;
    }

     ///<summary> Returns true on the frame left click is pressed </summary>
    public bool OnRightClickDown(){
        return Mouse.current.rightButton.wasPressedThisFrame;
    }

    ///<summary> Returns true on the frame left click is released </summary>
    public bool OnRightClickUp(){
        return Mouse.current.rightButton.wasReleasedThisFrame;
    }
}
