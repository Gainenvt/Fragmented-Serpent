using UnityEngine;

public class Player : MonoBehaviour
{
    public float HP = 100f;
    public bool isSubmerged = false;
    public bool isPlayerHit = false;

    private void Update()
    {
       
    }

    void Start()
    {        
        LockCursor();
        
    }
    // Update is called once per frame
     void  LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;//remove coursor from vieew
    }
    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;//Unhide Cursor 
    }
    
    private void OnTriggerEnter(Collider other)
    {
        
    }

    

}
