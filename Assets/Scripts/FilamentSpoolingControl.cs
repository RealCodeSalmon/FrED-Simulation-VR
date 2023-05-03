using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FilamentSpoolingControl : MonoBehaviour
{
    //Slider
    public Slider velSlider;
    public TMP_Text dispText;
    public TMP_Text percentage;

    //Indicator Text
    public TMP_Text linealSpeedText;
    public TMP_Text extFilamentText;
    public TMP_Text coilText;

    //Game Object for CircleGenerator Animation
    public GameObject CircleGenerator;

    //Rotation speed is measured in deg/second
    //6 deg/s  = 1 rpm
    //360 deg/s = 1 rps
    public float minVelocity = 0f;
    public float maxVelocity = 90f; //Max vel = 15 rpm

    //Rotation
    [SerializeField] private float rVelocity;
    public float RVelocity { get { return rVelocity; } }

    //Tangential velocity
    [SerializeField]private float tanVelocity;

    public float TanVelocity { get { return tanVelocity; } }
    
    //Extracted filament
    [SerializeField] private float extFilament;

    //Degrees spun and coil turns
    [SerializeField]private float degreesSpun;
    private int coilTurns;

    //Extrusion time and segment time

    //Inverse rotation
    [SerializeField]private float sec4Rotation; //Inverse rotation is used to determine circle animation duration in seconds 
    public float corrFactor; //Correctionfactor for syncronization with lineal guide.

    // Start is called before the first frame update
    void Start()
    {
        velSlider.value = 10f;
        extFilament = 0f;
        degreesSpun = 0f;
        coilTurns = 0;
    }

    // Update is called once per frame
    void Update()
    {
        rVelocity = velSlider.value * (maxVelocity/100);

        //Degrees spun counter
        degreesSpun += rVelocity * Time.deltaTime;

        //Coil Turns indicator
        coilTurns = Mathf.FloorToInt(degreesSpun / 360);
        coilText.text = coilTurns.ToString();
        
        //Spool Tangential Velocity Indicator
        tanVelocity = Mathf.Deg2Rad * rVelocity * 10; //Tan vel = w * radius
        linealSpeedText.text = tanVelocity.ToString("0.0");

        //Extracted Filament indicator
        extFilament += tanVelocity * Time.deltaTime;
        extFilamentText.text = extFilament.ToString("0.0");

        sec4Rotation = 360 / rVelocity * corrFactor;
        CircleGenerator.GetComponent<TestCircleGenerator>().AnimDuration= sec4Rotation;

        percentage.text = velSlider.value.ToString("0.0") + "%";
    }

    //Done on EDITOR Slider.OnValueChanged
    public void FillTextBox()
    {
        //Convert deg/s to rpm (deg/s / 6)
        float rpmVelocity = rVelocity / 6f;
        dispText.text = rpmVelocity.ToString("0.0");
    }

    //Done on EDITOR ipText.OnDeselect
    //public void UpdateSlider()
    //{
    //    //Convert rpm to deg/s  (rpm * 6)
    //    float slideval = float.Parse(dispText.text, CultureInfo.InvariantCulture.NumberFormat) * 6f;
    //    slideval = Mathf.Clamp(slideval, minVelocity, maxVelocity);
    //    rVelocity = slideval;
    //    velSlider.SetValueWithoutNotify((slideval / maxVelocity) * 100f);
    //}
}
