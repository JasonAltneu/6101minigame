using UnityEngine;

public class ArrowMover_BeatDancer : MonoBehaviour
{

    private int socketNumber = -1;
    private float lifespan = 0f;
    private float moveSpeed = 0f;
    private float elapsedTime = 0f;
    GameObject parent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        parent = transform.parent.gameObject;
        //The arrow needs to move 6.3 units
        lifespan = 1.1f * parent.GetComponent<GameManager_BeatDancer>().getInstantiationInterval();
        moveSpeed = -6.3f / parent.GetComponent<GameManager_BeatDancer>().getInstantiationInterval(); //This is how much it will move in a given second
        Debug.Log(moveSpeed * parent.GetComponent<GameManager_BeatDancer>().getInstantiationInterval());
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        transform.position = new Vector3(transform.position[0], transform.position[1] + moveSpeed * Time.deltaTime);
        if(elapsedTime >= lifespan)
        {
            parent.GetComponent<GameManager_BeatDancer>().incrementScore(2f);
            Destroy(gameObject);
        }
    }

    public void setSocketNumber(int num)
    {
        socketNumber = num;
    }
    
    public int getSocketNum()
    {
        return socketNumber;
    }
}
