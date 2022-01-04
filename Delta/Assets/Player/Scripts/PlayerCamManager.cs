using UnityEngine;
using System.Collections;
using Cinemachine;

public class PlayerCamManager : MonoBehaviour
{
    public enum CamStates
    {
        FIRST_PERSON, THIRD_PERSON
    };

    private static PlayerCamManager _instance;

    public static PlayerCamManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private InputManager inputManager;
    private CamStates cam_state = CamStates.FIRST_PERSON;

    public GameObject player;
    private float xRotation = 0;
    private GameObject current_cam;

    //Camera
    public Camera player_cam;

    //First Person 
    public CinemachineVirtualCamera FP_Cam;

    //Third Person
    public CinemachineFreeLook TP_Cam;

    [SerializeField]
    private float FP_horizontalSpeed = 10.0f;
    [SerializeField]
    private float FP_verticalSpeed = 10.0f;
    [SerializeField]
    private float FP_clampAngle = 80f;



    // Start is called before the first frame update
    void Start()
    {
        inputManager = InputManager.Instance;
        SetCamToFP();
        player_cam.cullingMask &= ~(1 << 6);
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    void SetCamToTP()
    {
        cam_state = CamStates.THIRD_PERSON;
        TP_Cam.m_Priority = 1;
        FP_Cam.m_Priority = 0;
        current_cam = TP_Cam.gameObject;
    }

    void SetCamToFP()
    {
        cam_state = CamStates.FIRST_PERSON;
        FP_Cam.m_Priority = 1;
        TP_Cam.m_Priority = 0;
        current_cam = FP_Cam.gameObject;
    }

    public IEnumerator SetMasks()
    {
        if (cam_state == CamStates.FIRST_PERSON)
        {
            // Switch off layer 14, leave others as-is
            yield return new WaitForSeconds(1);
            player_cam.cullingMask &= ~(1 << 6);

        }
        else if (cam_state == CamStates.THIRD_PERSON)
        {
            // Switch on layer 14, leave others as-is
            player_cam.cullingMask |= (1 << 6);
        }
    }

    public GameObject GetCurrentCam()
    {
        return current_cam;
    }

    public CamStates GetCamState()
    {
        return cam_state;
    }

    public Camera GetRawCam()
    {
        return player_cam;
    }

    // Update is called once per frame
    void Update()
    {
        if (inputManager.Interact())
        {
            if (cam_state == CamStates.FIRST_PERSON)
            {
                SetCamToTP();
            }
            else
            {
                SetCamToFP();
            }
            StartCoroutine(SetMasks());
        }
    }

    private void LateUpdate()
    {
        if (cam_state == CamStates.FIRST_PERSON)
        {
            Vector2 deltaInput = inputManager.GetMouseDelta();

            float mouse_x = deltaInput.x * FP_verticalSpeed * Time.deltaTime;
            float mouse_y = deltaInput.y * FP_horizontalSpeed * Time.deltaTime;

            xRotation -= mouse_y;
            xRotation = Mathf.Clamp(xRotation, -FP_clampAngle, FP_clampAngle);

            transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
            player.transform.Rotate(Vector3.up * mouse_x);
        }
    }
}
