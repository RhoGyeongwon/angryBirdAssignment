using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggerObject : MonoBehaviour, IPointerUpHandler, IDragHandler, IPointerDownHandler //콜라이더 터치로 인식되는 인터페이스
{//draggerObject가 작동하려면 collider 컴포넌트가 붙어있어야함.
    
    //Position 값
    Vector3 startPosition;
    Vector3 LinePosition1 = Vector3.zero;
    
    //중력값
    float bulletGravityScale;
    Camera MainCamera;
    
    [Header ("Line")]
    [SerializeField] LineRenderer lineRenderer;
    
    [Header ("MaxDistance")]
    [SerializeField] float maxPullDistance;
    
    [Header ("Bullet")]
    [SerializeField] GameObject prefabObject;

    [Header("TextUI")]
    [SerializeField] TextMeshPro powerUIText;
    float powerPercent;
    
    GameObject throwObject;
    [SerializeField] GameObject triangleSprite;
    [SerializeField] Animator animator;
    float speed = 120f;
    float coolTimeValue = 2f;
    
    [SerializeField] BoxCollider2D box;
    
    public bool isDragging { get; private set; } = false;
    
    void Awake()
    {
        MainCamera = Camera.main;
    }

    void Start()
    {
        powerUIText.color = Color.white;
        powerUIText.text = "0%";
        Application.targetFrameRate = 60;
        startPosition = transform.position;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position);
    }

    void Update()
    {
        if (!isDragging)
        {
            animator.SetBool("IsDragger", false);
        }
        else
        {
            triangleSprite.transform.Rotate(0f, 0f, 100f * Time.deltaTime);
            animator.SetBool("IsDragger", true);
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
        powerUIText.color = Color.yellow;
        
        throwObject = Instantiate(prefabObject, transform.position, Quaternion.identity);
        bulletGravityScale = throwObject.GetComponent<Rigidbody2D>().gravityScale;
        throwObject.GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
        powerUIText.color = Color.white;
        powerPercent = 0f;
        powerUIText.text = string.Format("{0}%",powerPercent);
        
        if (throwObject == null)
        {
            return;
        }
        
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position);
        
        Vector3 velocityValue = Vector3.zero - LinePosition1;
        throwObject.GetComponent<Rigidbody2D>().gravityScale = bulletGravityScale;
        throwObject.GetComponent<Rigidbody2D>().AddForce(velocityValue * speed * Time.deltaTime, ForceMode2D.Impulse);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (throwObject == null)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position);
            return;
        }
        
        Vector3 mouseWorldPos = MainCamera.ScreenToWorldPoint(
        new Vector3(eventData.position.x, 
        eventData.position.y, 
        MainCamera.WorldToScreenPoint(throwObject.transform.position).z));
       
        Vector3 pullDirection = startPosition - mouseWorldPos;
        
        if (pullDirection.magnitude > maxPullDistance)
        {
            pullDirection = pullDirection.normalized * maxPullDistance;
            LinePosition1 = startPosition - pullDirection;
        }
        else
        {
            LinePosition1 = mouseWorldPos;
        }

        powerPercent = (pullDirection.magnitude / maxPullDistance) * 100f;
        powerUIText.text = string.Format("{0:F0}%",powerPercent);
        
        throwObject.transform.position = LinePosition1;
        lineRenderer.SetPosition(1, LinePosition1);
    }

    void OnCollisionEnter2D(Collision2D other) //따로 컴포넌트 만들어서 관리하려 했으나 안됨.
    {
        StartCoroutine(NotTouchCoroutine());
    }

    IEnumerator NotTouchCoroutine() //★ 근데 잡아당긴 상태에서 있으면 안사라짐. 이건 나중에 버그 수정하자.
    {
        box.enabled = false;
        yield return new WaitForSeconds(2f);
        box.enabled = true;
    }
}
