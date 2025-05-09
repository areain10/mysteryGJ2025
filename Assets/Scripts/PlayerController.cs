using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public CharacterController cc;
    [SerializeField] private GameObject player;
    [SerializeField] private Camera cam;
    [SerializeField] private float Sensitivity;
    [SerializeField] AudioClip footstep;
    [SerializeField] private float speed, walk, run, crouch;

    private Vector3 crouchScale, normalScale;

    public bool isMoving, isCrouching, isRunning, isJumping;

    private float X, Y;

    private void Start()
    {
        speed = walk;
        crouchScale = new Vector3(1, .70f, 1);
        normalScale = new Vector3(1, 1, 1);
        cc = GetComponent<CharacterController>();
        cc.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cam.gameObject.transform.localPosition = new Vector3(0,1.8f,-0.5f);
    }
    private void Update()
    {
        try
        {
            Sensitivity = 200 * GameObject.FindAnyObjectByType<menus>().sensitivity;
        }
        catch
        {

        }
        if (!gameObject.GetComponent<clueManager>().notepad)
        {


            #region Camera Limitation Calculator
            //Camera limitation variables
            const float MIN_Y = -60.0f;
            const float MAX_Y = 70.0f;
            //Debug.Log(Screen.width / 1980f);
            X += Input.GetAxisRaw("Mouse X") * (Sensitivity * Time.deltaTime * (Screen.width / 1980f));
            Y -= Input.GetAxisRaw("Mouse Y") * (Sensitivity * Time.deltaTime * (Screen.width / 1980f));
            Debug.Log(X + "        " + Y);
            if (Y < MIN_Y)
                Y = MIN_Y;
            else if (Y > MAX_Y)
                Y = MAX_Y;
            #endregion
            cam.transform.eulerAngles = new Vector3(Y, X, .0f);
            transform.eulerAngles = new Vector3(0,X,.0f);
            //transform.localRotation = Quaternion.Euler(0f, X, 0.0f);

            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 forward = transform.forward * vertical;
            Vector3 right = transform.right * horizontal;

            try
            {
                cc.SimpleMove(Vector3.Normalize(forward + right) * speed);
            }
            catch
            {

            }
            
            // Determines if the speed = run or walk
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = run;
                isRunning = true;
            }
           
            //Crouch
            else if (Input.GetKey(KeyCode.LeftControl))
            {
                isCrouching = true;
                isRunning = false;
                speed = crouch;
                player.transform.localScale = crouchScale;
            }
            else
            {
                isRunning = false;
                isCrouching = false;
                speed = walk;
                player.transform.localScale = normalScale;
            }
            // Detects if the player is moving.
            // Useful if you want footstep sounds and or other features in your game.
            isMoving = cc.velocity.sqrMagnitude > 0.0f ? true : false;
        }
        else
        {
            
        }
        if(isMoving && !GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().Play();
        }
        else if(!isMoving && GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().Stop();
        }
    }
}
