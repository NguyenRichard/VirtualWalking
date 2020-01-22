using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class QuestDebug : MonoBehaviour
{

    bool inMenu;
    Text logText;
    public static QuestDebug Instance;

    //private Text sliderText;

    //private float sliderValue;
    private Canvas UICanvas;

    private GameObject feedbackManager;
    private HeadInObstacles headInObstacles;
    private HandInObstacle handInObstacle;

    private CalculateCollisions calculateCollisions;
    private GameObject collisionDetector;
    private GameObject rHand;
    private GameObject lHand;
    private GameObject menu;


    private string path_woFeedback_collisions;
    private string path_woFeedback_coordinates;
    private string path_wFeedback_collisions;
    private string path_wFeedback_coordinates;

    private float time;
    [SerializeField]
    private float refresh_coordinate_time = 0.3f;


    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        UICanvas = GetComponent<Canvas>();
        var rt = DebugUIBuilder.instance.AddLabel("Debug");
        logText = rt.GetComponent<Text>();

        DebugUIBuilder.instance.AddToggle("Feedback On/Off",switchFeedback);
        DebugUIBuilder.instance.AddDivider();
        DebugUIBuilder.instance.AddButton("Stop", resetTimer);
        DebugUIBuilder.instance.AddButton("Start", startRecording);

        feedbackManager = GameObject.Find("FeedbackManager");
        headInObstacles = feedbackManager.GetComponent<HeadInObstacles>();
        handInObstacle = feedbackManager.GetComponent<HandInObstacle>();

        var prefabCollisionDetector = Resources.Load<GameObject>("Prefabs/CollisionDetector");
        collisionDetector = Instantiate(prefabCollisionDetector, Vector3.zero, Quaternion.identity);

        collisionDetector.transform.SetParent(GameObject.Find("CenterEyeAnchor").transform);
        calculateCollisions = collisionDetector.GetComponentInChildren<CalculateCollisions>();

        rHand = GameObject.Find("RightHandAnchor");
        lHand = GameObject.Find("LeftHandAnchor");


        time = Time.time;
        menu = GameObject.Find("CanvasWithDebug");
        if (!Application.isEditor)
        {
            path_woFeedback_collisions = Path.Combine(Application.persistentDataPath, "Log" + "_" + "CoordinatewoFeedback.txt");
            path_wFeedback_collisions = Path.Combine(Application.persistentDataPath, "Log" + "_" + "CoordinatewFeedback.txt");
        }
        

        //var sliderPrefab = DebugUIBuilder.instance.AddSlider("Slider", 50f, 100.0f, SliderPressed, true);
        //var textElementsInSlider = sliderPrefab.GetComponentsInChildren<Text>();
        //Assert.AreEqual(textElementsInSlider.Length, 2, "Slider prefab format requires 2 text components (label + value)");
        //sliderText = textElementsInSlider[1];
        //Assert.IsNotNull(sliderText, "No text component on slider prefab");
        //sliderText.text = sliderPrefab.GetComponentInChildren<Slider>().value.ToString();
    }

    private void startRecording()
    {
        calculateCollisions.reset();
        DateTime now = DateTime.Now;
        if (headInObstacles.IsActive == false)
        {
            path_woFeedback_coordinates = Path.Combine(Application.persistentDataPath, "Log" + "_" + now.Day.ToString("00") + "_" + now.Month.ToString("00") + "_" + now.Year + "_" +
now.Hour.ToString("00") + "_" + now.Minute.ToString("00") + "_" + now.Second.ToString("00") + "_" + "CoordinatewoFeedback.txt");
            File.WriteAllText(path_woFeedback_coordinates, "x_head;y_head;z_head;x_handR;y_handR;z_handR;x_handL;y_handL;z_handL" +
Environment.NewLine);
        }
        else
        {
            path_wFeedback_coordinates = Path.Combine(Application.persistentDataPath, "Log" + "_" + now.Day.ToString("00") + "_" + now.Month.ToString("00") + "_" + now.Year + "_" +
now.Hour.ToString("00") + "_" + now.Minute.ToString("00") + "_" + now.Second.ToString("00") + "_" + "CoordinatewFeedback.txt");
            File.WriteAllText(path_wFeedback_coordinates, "x_head;y_head;z_head;x_handR;y_handR;z_handR;x_handL;y_handL;z_handL" +
Environment.NewLine);
        }


    }

    private void resetTimer()
    {
        DateTime now = DateTime.Now;
        if (headInObstacles.IsActive == false)
        {
            File.AppendAllText(path_woFeedback_collisions, now.Day.ToString("00") + "_" + now.Month.ToString("00") + "_" + now.Year + "_" +
             now.Hour.ToString("00") + "_" + now.Minute.ToString("00") + "_" + now.Second.ToString("00") + ";" + calculateCollisions.TimeInWall
             + ";" + calculateCollisions.NumberOfCollision
             + Environment.NewLine);
        }
        else
        {
            File.AppendAllText(path_wFeedback_collisions, now.Day.ToString("00") + "_" + now.Month.ToString("00") + "_" + now.Year + "_" +
             now.Hour.ToString("00") + "_" + now.Minute.ToString("00") + "_" + now.Second.ToString("00") + ";" + calculateCollisions.TimeInWall
             + ";" + calculateCollisions.NumberOfCollision
             + Environment.NewLine);
        }

    }

    private void switchFeedback(Toggle t)
    {
        handInObstacle.IsActive = t.isOn;
        headInObstacles.IsActive = t.isOn;
    }

    //Copied from DebugUISample.cs script
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Start))
        {
            if (inMenu)
            {
                inMenu = false;
                DebugUIBuilder.instance.Hide();
            }
            else
            {
                inMenu = true;
                DebugUIBuilder.instance.Show();
            }
            // inMenu = !inMenu;
            
            //DebugUIBuilder.instance.Show();
            /*
            if (inMenu)
            {
                menu.SetActive(false);
            }
            else
            {
                menu.SetActive(true);
            }
            inMenu = !inMenu;*/

        }

        if (!Application.isEditor)
        {
            if (Time.time >= time)
            {
                DateTime now = DateTime.Now;
                if (headInObstacles.IsActive == false)
                {
                    File.AppendAllText(path_woFeedback_coordinates, collisionDetector.transform.position.x
                    + ";" + collisionDetector.transform.position.y
                    + ";" + collisionDetector.transform.position.z
                    + ";" + rHand.transform.position.x
                    + ";" + rHand.transform.position.y
                    + ";" + rHand.transform.position.z
                    + ";" + lHand.transform.position.x
                    + ";" + lHand.transform.position.y
                    + ";" + lHand.transform.position.z
                    + Environment.NewLine);
                }
                else
                {
                    File.AppendAllText(path_wFeedback_coordinates, collisionDetector.transform.position.x
                    + ";" + collisionDetector.transform.position.y
                    + ";" + collisionDetector.transform.position.z
                    + ";" + rHand.transform.position.x
                    + ";" + rHand.transform.position.y
                    + ";" + rHand.transform.position.z
                    + ";" + lHand.transform.position.x
                    + ";" + lHand.transform.position.y
                    + ";" + lHand.transform.position.z
                    + Environment.NewLine);
                }
                time = Time.time + refresh_coordinate_time;
            }
        }
      

    }

    public void Log(string msg)
    {
        logText.text = msg;
    }

    /*public void SliderPressed(float f)
    {
        Debug.Log("Slider: " + f);
        sliderText.text = f.ToString();
    }

    public float GetSliderValue()
    {        
        return float.Parse(sliderText.text) / 100.0f;
    }*/
}
