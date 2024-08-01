using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    
    [Header("Desctop Parametrs")]
    [SerializeField] CharacterController characterController;


    [Header("Mobile Parametrs")]

    [Header("Model Parametrs")]
    [SerializeField] Transform model;
    [SerializeField] float speedRotation;

    public static string platform;
    
    private Vector3 _startCursorePosition;
    private Vector3 _direction;
    private Quaternion _rotation;
    private Joystick _joystick;

    private void Awake()
    {
        _joystick = GameObject.FindGameObjectWithTag("MobileController").GetComponent<Joystick>();

        //CheckDevice(platform);
        platform = "Desctop";
    }

    private void CheckDevice(string platform)
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            if (SystemInfo.deviceType == DeviceType.Handheld)
            {
                platform = "Mobile";
                return;
            }
            else
            {
                platform = "Desctop";
                return;
            }
        }
        else
        {
            Debug.LogWarning("»гра не запущена в WebGL.");
        }
    }


    private void Update()
    {
        if (platform == "Desctop")
            DesctopPlayerController();
        else
            MobilePlayerController();
    }


    private void DesctopPlayerController()
    {
        _direction.x = -Input.GetAxisRaw("Horizontal");
        _direction.z = -Input.GetAxisRaw("Vertical");

        _direction = _direction.normalized;
        characterController.Move(_direction * speed * Time.deltaTime);

        RotateModel();
        //Debug.Log(_direction);
    }

    private void MobilePlayerController()
    {
        _direction.x = -_joystick.GetDirection().x;
        _direction.z = -_joystick.GetDirection().y;

        _direction = _direction.normalized;
        characterController.Move(_direction * speed * Time.deltaTime);

        RotateModel();
    }

    private void RotateModel()
    {
        if (_direction != Vector3.zero)
        {
            _rotation = Quaternion.LookRotation(_direction);
        }
        model.rotation = Quaternion.Lerp(model.rotation, _rotation, speedRotation * Time.deltaTime);
    }
}
