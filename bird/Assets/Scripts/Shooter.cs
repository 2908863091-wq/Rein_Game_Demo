using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public static Shooter Instance {  get; private set; }

    private LineRenderer leftLineRender;
    private LineRenderer rightLineRender;

    private Transform leftpoint;
    private Transform rightpoint;
    private Transform middlepoint;
    private Transform birdTransform;

    private bool isDrawing = false;

    private void Awake()
    {
        Instance = this;

        leftLineRender = transform.Find("line_left").GetComponent<LineRenderer>();
        rightLineRender = transform.Find("line_right").GetComponent<LineRenderer>();

        leftpoint = transform.Find("left_point");
        rightpoint = transform.Find("right_point");
        middlepoint = transform.Find("middle_point");
    }

    // Start is called before the first frame update
    void Start()
    {
        hideLine();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDrawing)
        {
            Drawing();
        }
    }

    public void StartDraw(Transform birdTransform)
    {
        isDrawing = true;
        this.birdTransform = birdTransform;
        showLine();
    }
    public void EndDraw()
    {
        isDrawing=false;
        hideLine();
    }

    public void Drawing()
    {
        Vector3 birdPosition = birdTransform.position;

        birdPosition = (birdPosition - middlepoint.position).normalized * 0.4f + birdPosition;

        leftLineRender.SetPosition(0,birdPosition);
        leftLineRender.SetPosition(1, leftpoint.position);

        rightLineRender.SetPosition(0,birdPosition);
        rightLineRender.SetPosition(1, rightpoint.position);
    }
    public Vector3 getCenterPosition()
    {
        return middlepoint.transform.position;
    }
    private void hideLine()
    {
        leftLineRender.enabled = false;
        rightLineRender.enabled = false;
    }
    private void showLine()
    {
        leftLineRender.enabled = true;
        rightLineRender.enabled = true;
    }
}
