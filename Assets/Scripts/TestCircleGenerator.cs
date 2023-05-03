using UnityEngine;
using System.Threading.Tasks;

public class TestCircleGenerator : MonoBehaviour
{
    //Scale Factor;
    public float widthScale = 0.003f;

    //Declare circlePrefab and refPoints
    public GameObject circlePrefab;
    public Transform SpawnPoint;
    public Transform refPos;

    //Declare Animation parameters
    public float animDuration = 2f;
    private int animDurationmili;
    public int nCircles;
    public float deltaZ;
    public float deltaR;

    //Circle width control
    private float circleWidth;
    public float CircleWidht
    {
        get { return circleWidth; }
        set { circleWidth = value; }
    }

    //Declare newSpawnPosition counter and radius
    private Vector3 newSpawnPosition;
    [SerializeField] private int counter;
    [SerializeField] private float radius = 10f;
    
    //public accesor change animation duration
    public float AnimDuration
    {
        get { return animDuration;}
        set { animDuration = value;} 
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnPoint.position = this.transform.position;
        counter = 0;
        circleWidth = 1f * widthScale;
        newSpawnPosition = SpawnPoint.position;
        animDurationmili = Mathf.CeilToInt(animDuration * 1000);
        DelayedSpawn(animDurationmili,animDuration);
        deltaZ *= -widthScale;
        radius *= widthScale;
    }

    // Update is called once per frame
    void Update()
    {
        //Ref point follows spawn point
        Vector3 refPoint = new Vector3(SpawnPoint.position.x, SpawnPoint.position.y + radius, SpawnPoint.position.z - deltaZ);
        refPos.position = refPoint;

        //When all circles are drawn direction is changed and radius incremented;
        if (counter >= nCircles)
        {
            deltaZ = - deltaZ;
            radius = (radius + (1.25f * widthScale)); 
            counter = 0;
            DelayedSpawn(animDurationmili, animDuration);
        }
    }

    async void DelayedSpawn(int delayT, float animT)
    {
        for (int i = 0; i<nCircles; i++) 
        {
            if (Application.isPlaying)
            {
                await Task.Delay(delayT);
                print("newCircleCalled");

                //Create a new circle Prefab
                GameObject currentCircle = Instantiate(circlePrefab, SpawnPoint.position, Quaternion.identity);

                //Change radius
                currentCircle.GetComponent<DrawAnimatedCircle>().Radius = radius;

                //Change width 
                currentCircle.GetComponent<LineRenderer>().startWidth = circleWidth;
                currentCircle.GetComponent<LineRenderer>().endWidth = circleWidth;

                //Call draw function and animation
                currentCircle.GetComponent<DrawAnimatedCircle>().GenerateCirclePoints();
                currentCircle.GetComponent<DrawAnimatedCircle>().AnimateCircle(animT);

                //Change next circle position
                newSpawnPosition = new Vector3(newSpawnPosition.x, newSpawnPosition.y, newSpawnPosition.z + deltaZ);
                SpawnPoint.position = newSpawnPosition;

                //Increment counter
                counter++;
            }
        }
    }
}
