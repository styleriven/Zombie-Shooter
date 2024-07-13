using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerMouse : MonoBehaviour {
    private static ManagerMouse instance;

    public static ManagerMouse Instance {
        get => instance;
        set => instance = value;
    }
    [SerializeField]
    public Texture2D cursorTexture; 
    [SerializeField]
    public Vector2 hotSpot = Vector2.zero; 

    void Awake() {
        if(ManagerMouse.Instance != null) {
            Debug.LogError("Only 1 ManagerMouse instance is allowed");
            return;
        }
        ManagerMouse.Instance = this;

        DontDestroyOnLoad(gameObject); 
    }

    void Start() {
        SetCustomCursor();
    }

    void Update() {
        
        if(Input.GetKeyDown(KeyCode.C)) {
            ManagerMouse.Instance.SetCustomCursor();
        }

        if(Input.GetKeyDown(KeyCode.D)) {
            ManagerMouse.Instance.SetDefaultCursor();
        }
    }

    public void SetCustomCursor() {
        Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
    }

    public void SetDefaultCursor() {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto); 
    }
}
