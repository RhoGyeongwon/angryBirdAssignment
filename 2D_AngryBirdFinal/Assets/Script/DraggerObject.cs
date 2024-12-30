using UnityEngine;
using UnityEngine.EventSystems;

//3) 서로 공격하는 부분은 따로 클래스를 만들어서 관리한다.
//4) prefab의 무기 정보를 다르게 넣는다. 공격력, 또는 스피드, sprite, name 등 다르게 관리해야한다.
//5) 랜덤으로 하늘에서 아이템이 떨어지게 한다.
public class DraggerObject : MonoBehaviour, IPointerUpHandler, IDragHandler, IPointerDownHandler //콜라이더 터치로 인식되는 인터페이스
{//draggerObject가 작동하려면 collider 컴포넌트가 붙어있어야함.
    
    //Position 값
    Vector3 startPosition;
    Vector3 pullPosition;
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
    GameObject throwObject;
    
    private void Awake()
    {
        MainCamera = Camera.main;
    }

    void Start()
    {
        startPosition = transform.position;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position);
    }
    //문제 2 ) 라인 렌더러가 공을 이상하게 따라가는 현상
    public void OnPointerDown(PointerEventData eventData)
    {
        throwObject = Instantiate(prefabObject, transform.position, Quaternion.identity);
        bulletGravityScale = throwObject.GetComponent<Rigidbody2D>().gravityScale;
        throwObject.GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log(throwObject.transform.position);
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position);
        throwObject.GetComponent<Rigidbody2D>().gravityScale = bulletGravityScale;
        Vector3 velocityValue = Vector3.zero - LinePosition1;
        throwObject.GetComponent<Rigidbody2D>().AddForce(velocityValue * 20f, ForceMode2D.Impulse);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Camera.main != null)
        {
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
            
            throwObject.transform.position = LinePosition1;
            
            lineRenderer.SetPosition(1, LinePosition1);
        }
    }
}
