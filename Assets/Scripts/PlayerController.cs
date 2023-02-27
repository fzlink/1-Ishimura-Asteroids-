using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D RB;
    [SerializeField] private Transform PlayerForward;
    [SerializeField] private Transform PlayerCenter;
    [SerializeField] private float Speed;
    [SerializeField] private float MaxVelocity;
    [SerializeField] private float RotationSpeed;
    [SerializeField] private Animator Animator;
    [SerializeField] private Transform Root;
    [SerializeField] private CollisionHandler CollisionHandler;
    [SerializeField] private ShipPartController ShipPartController;
    [SerializeField] private BlinkController BlinkController;
    [SerializeField] private Shooter Shooter;
    
    [SerializeField] private int _healthPoint = 3;

    public event Action<int> OnDestroyed;
    public event Action OnRespawned;
    
    private bool _boost;
    private Vector2 _directionVector;


    private void Awake()
    {
        CollisionHandler.OnCollided += OnCollided;
    }

    public int GetHealthPoint()
    {
        return _healthPoint;
    }
    
    private void OnCollided(object sender, CollisionEventArgs eventArgs)
    {
        if (eventArgs.type == typeof(Asteroid))
        {
            LoseHealth();
        }
    }

    private void LoseHealth()
    {
        _healthPoint--;
        OnDestroyed?.Invoke(_healthPoint);
        Shatter();
    }

    private void Shatter()
    {
        Root.gameObject.SetActive(false);
        ShipPartController.gameObject.SetActive(true);
        ShipPartController.Distribute();
        Shooter.enabled = false;
        if (_healthPoint > 0)
        {
            StartCoroutine(Respawn());
        }
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(2f);
        
        RB.velocity = Vector2.zero;
        gameObject.transform.position = Vector3.zero;
        Root.gameObject.SetActive(true);
        ShipPartController.Respawn();
        Shooter.enabled = true;
        CollisionHandler.gameObject.SetActive(false);
        Action OnBlinkEnd = () =>
        {
            CollisionHandler.gameObject.SetActive(true);
            OnRespawned?.Invoke();
        };
        BlinkController.Blink(2f,OnBlinkEnd);
    }

    // Update is called once per frame
    void Update()
    {
        _boost = Input.GetKey(KeyCode.UpArrow);
        Animator.SetBool("Boost", _boost);
        
        int direction = 0;
        if (Input.GetKey(KeyCode.RightArrow))
            direction = -1;
        else if (Input.GetKey(KeyCode.LeftArrow))
            direction = 1;
        Root.Rotate(Vector3.forward * RotationSpeed * direction);
    }

    private void FixedUpdate()
    {
        if (_boost)
        {
            Vector2 forwardDir = PlayerForward.position - PlayerCenter.position;
            RB.AddForce(forwardDir * Speed, ForceMode2D.Force);
        }

        ClampVelocity();
    }

    private void ClampVelocity()
    {
        float sqrMagnitude = RB.velocity.sqrMagnitude;
        if (sqrMagnitude > MaxVelocity * MaxVelocity)
        {
            RB.velocity = RB.velocity.normalized * MaxVelocity;
        }
    }
}
