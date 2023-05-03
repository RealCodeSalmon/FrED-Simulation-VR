using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FilamentDiamControl : MonoBehaviour
{
    //Scale Factor;
    public float scale =  0.003f;

    //Slider
    public Slider diamSlider;
    public TMP_Text dispText;
    public TMP_Text percentage;

    //Diameter min and max 
    public float minDiam = 0.2f;   // 0.2mm
    public float maxDiam = 3f;  // 3mm 

    //Diameter value
    [SerializeField] private float diameter;

    //NEW LINE OBJECT REFERENCE 
    public GameObject LineObject;

    // Start is called before the first frame update
    void Start()
    {
        maxDiam = maxDiam * scale;
        minDiam = minDiam * scale;
        diamSlider.value = 33f;
        diameter = minDiam;
    }

    // Update is called once per frame
    void Update()
    {
        diameter = diamSlider.value * (maxDiam / 100);
        percentage.text = diamSlider.value.ToString("0.0") + "%";
    }

    //Done on EDITOR Slider.OnValueChanged
    public void FillTextBox()
    {
        
        //Set textbox to diameter current value
        dispText.text = diameter.ToString("0.0"); //Format to decimal

        //NEW IMPLEMENTATION 
        LineObject.GetComponent<NewAnimateLine>().EndW = diameter/scale;

    }

    //Done on EDITOR ipText.OnDeselect
    //public void UpdateSlider()
    //{
    //    //Set diameter to value set on TextBox
    //    float slideval = float.Parse(dispText.text, CultureInfo.InvariantCulture.NumberFormat);
    //    slideval = Mathf.Clamp(slideval, minDiam, maxDiam);
    //    diameter = slideval;
        
    //    //Set diameter slider to corresponding position
    //    diamSlider.SetValueWithoutNotify((slideval / maxDiam) * 100f);

    //    //NEW IMPLEMENTATION 
    //    LineObject.GetComponent<NewAnimateLine>().EndW = diameter;
    //}
}