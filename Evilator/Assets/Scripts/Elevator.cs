using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour {

    [SerializeField]
    private GameObject[] sector1Elements;

    [SerializeField]
    private GameObject[] sector2Elements;

    [SerializeField]
    private GameObject[] sector3Elements;

    [SerializeField]
    private int visibleSegments = 10;

    [SerializeField]
    private float moveSpeed = 10;

    private LinkedList<Transform> activeSegments;

    private float maxHeight;

    private float fallTime = 0.0f;

    private float distance = 1.0f;


    // Use this for initialization
    void Start () {
        activeSegments = new LinkedList<Transform>();
        maxHeight = transform.position.y + distance * (visibleSegments-1);

        distance = sector1Elements[0].transform.TransformPoint( sector1Elements[0].gameObject.GetComponent<MeshFilter>().mesh.bounds.max).y - sector1Elements[0].transform.TransformPoint(sector1Elements[0].gameObject.GetComponent<MeshFilter>().mesh.bounds.min).y;

        FillElevator();
	}

    private void FillElevator()
    {
        for(int i = visibleSegments-1; i >= 0; i--)
        {
            AddSegment(ProvideSegment());  
        }
    }

	
	void Update () {
        fallTime += Time.deltaTime;
        
		foreach(Transform t in activeSegments)
        {
            t.Translate(Vector3.up*Time.deltaTime*moveSpeed);
        }
        CheckSegmentHeight();
	}

    private void CheckSegmentHeight()
    {
        if(activeSegments.First.Value.position.y > maxHeight)
        {
            GameObject gameObject = activeSegments.First.Value.gameObject;
            activeSegments.RemoveFirst();
            Destroy(gameObject);

            AddSegment(ProvideSegment());
        }
    }

    private GameObject ProvideSegment()
    {
        float random =  Random.Range(0.0f,1.0f);
        if (random < 0.3f)
        {
            return sector1Elements[0];
        }
        return sector2Elements[0];

    }


    private void AddSegment(GameObject segment)
    {
        GameObject gameObject;
        if (activeSegments.Count > 0)
        {
            gameObject = Instantiate(segment,  activeSegments.Last.Value.position - Vector3.up * distance, Quaternion.identity) as GameObject;
        }else
        {
            gameObject = Instantiate(segment, transform.position, Quaternion.identity) as GameObject;
        }
        gameObject.transform.parent = transform;
        activeSegments.AddLast(gameObject.transform);
    }
}
