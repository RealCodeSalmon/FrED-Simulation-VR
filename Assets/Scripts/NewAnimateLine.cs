using System;
using System.Threading.Tasks;
using UnityEngine;

public class NewAnimateLine : MonoBehaviour
{
    //Original LineReference
    public GameObject ogLine;

    //CircleGenerator Reference;
    public GameObject cirGen;

    //ControlsManagerReference
    public GameObject ControlsManager;
    private float tanVel;

    //Line Positions
    public Transform[] LinePoints;
    
    //Line Renderers
    public LineRenderer[] LineSegments;

    //Line Segment Distances
    [SerializeField] private int[] SegmDelays = new int[12];

    //Line Parameters
    public float startWidth = 0.1f;
    public float endWidth = 0.5f;
    public float endW = 1f;
    public int incrementCounts = 10;
    public float corrFactor = 0.2f;

    public float EndW
    {
        get { return endW; }
        set { endW = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        ogLine.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLinePosition();

        tanVel = ControlsManager.GetComponent<FilamentSpoolingControl>().TanVelocity;
        FindSegmentDelay(tanVel);

        AnimKeyPress();
    }

    private async void AnimateLine(float startW, float endW)
    {
        for (int i = 0; i < LineSegments.Length; i++)
        {
            await MoveStartWidth(LineSegments[i], incrementCounts, startW, endW, SegmDelays[i]);
            await MoveEndWidth(LineSegments[i], incrementCounts, startW, endW, SegmDelays[i]);
        }
        cirGen.GetComponent<TestCircleGenerator>().CircleWidht = endW;
    }

    private async Task MoveStartWidth(LineRenderer line, int counts, float startW, float endW, int delay)
    {
        float newWidth;
        for (int i = 0; i <= counts-1; i++)
        {
            newWidth = startW + (i * (endW - startW) / counts);
            line.startWidth = newWidth;
            print("i = " + i + " start width = " + newWidth);
            await Task.Delay(delay);
        }
    }

    private async Task MoveEndWidth(LineRenderer line, int counts, float startW, float endW, int delay)
    {
        float newWidth;
        for (int i = 0; i <= counts - 1; i++)
        {
            newWidth = startW + (i * (endW - startW) / counts);
            line.endWidth = newWidth;
            print("i = " + i + " end width = " + newWidth);
            await Task.Delay(delay);
        }
    }

    private void UpdateLinePosition()
    {
        for (int i = 0; i< LineSegments.Length; i++)
        {
            LineSegments[i].SetPosition(0, LinePoints[i].position);
            LineSegments[i].SetPosition(1, LinePoints[i+1].position);
        }
    } 

    private void FindSegmentDelay(float vel)
    {
        float currDelay;
        double totalDistance = 483.1686;
        float totalTime = Convert.ToSingle(totalDistance / vel);

        for (int i = 0; i < SegmDelays.Length; i++)
        {
            currDelay = Convert.ToSingle(Vector3.Distance(LinePoints[i].position, LinePoints[i+1].position)/totalDistance);
            SegmDelays[i] = Mathf.CeilToInt((currDelay/(incrementCounts - 1)) * totalTime * corrFactor * 1000);
        }
    }

    private void AnimKeyPress()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            print("KeyPressed");
            startWidth = endWidth;
            endWidth = endW;
            AnimateLine(startWidth, endW);
        }
    }

    public void AnimButtonPress()
    {
        print("ButtonPressed");
        startWidth = endWidth;
        endWidth = endW;
        AnimateLine(startWidth, endW);
    }

}
