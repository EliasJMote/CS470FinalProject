﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogicScript : MonoBehaviour
{
    // Visualizers for each instrument
    public Instantiate512cubes trumpetVisualizer;
    public Instantiate512cubes drumVisualizer;
    public Instantiate512cubes pianoVisualizer;
    public Instantiate512cubes harpVisualizer;
    public Instantiate512cubes violinVisualizer;

    // Visualizer objects for each instrument
    public GameObject trumpetVisualizerObj;
    public GameObject drumVisualizerObj;
    public GameObject pianoVisualizerObj;
    public GameObject harpVisualizerObj;
    public GameObject violinVisualizerObj;

    public int _gameScore = 0;
    public int initialBPM = 150;
    int _bpm;
    float _bpmScale; // bpm * 1min/60s
    public float conductorTolerance = 1.0f/4.0f; // fraction of a beat
    public double _inputDelay = 25.0/1000.0; // in seconds
    public Text scoreObject;
    public GameObject beatObject;
    public GameObject selector;
    private selector _selectorScript;
    public AudioSource trumpetMusic;
    public AudioSource drumMusic;
    public AudioSource pianoMusic;
    public AudioSource harpMusic;
    public AudioSource violinMusic;
    private float timer;

    // public variable that can be set to LTouch or RTouch in the Unity Inspector
    //public Controller controller;

    // Use this for initialization
    void Start()
    {
        SetBPM(initialBPM);
        _selectorScript = selector.GetComponent<selector>();
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        OVRInput.Update();
        float lTriggerVal = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch);
        //if (OVRInput.GetDown(OVRInput.Button.One))
        //if(OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch) == 1.0f);
        Debug.Log(_selectorScript.instSelection);
        if(lTriggerVal > 0.5f)
            UnityEngine.XR.InputTracking.Recenter();
        timer += Time.deltaTime;
        double keyTime = AudioSettings.dspTime - _inputDelay;

        if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickRight, OVRInput.Controller.LTouch))
        {
            switch (_selectorScript.instSelection)
            {
                case 0: _selectorScript.instSelection = 2; break;
                case 1: _selectorScript.instSelection = 4; break;
                case 2: _selectorScript.instSelection = 1; break;
                case 3: _selectorScript.instSelection = 0; break;
                case 4: _selectorScript.instSelection = 3; break;
                default: _selectorScript.instSelection = 3; break;
            }
            _selectorScript.MoveSelector(_selectorScript.instSelection);
        }
        else if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickLeft, OVRInput.Controller.LTouch))
        {
            switch (_selectorScript.instSelection)
            {
                case 0: _selectorScript.instSelection = 3; break;
                case 1: _selectorScript.instSelection = 2; break;
                case 2: _selectorScript.instSelection = 0; break;
                case 3: _selectorScript.instSelection = 4; break;
                case 4: _selectorScript.instSelection = 1; break;
                default: _selectorScript.instSelection = 3; break;
            }
            _selectorScript.MoveSelector(_selectorScript.instSelection);
        }

        // Display/hide ui element
        //beatObject.SetActive(KeyTimeGoodEnough(keyTime));

        /*if ( Input.GetKeyDown(KeyCode.Space) )
        {
            if (KeyTimeGoodEnough( keyTime ))
                _gameScore++;
            else
                _gameScore--;
        }*/

        //Debug.Log(timer);

        /*
         * Timer events for requiring music to be adjusted
         */

        // 10 second mark
        if (timer >= 10f && timer <= 10.2f)
        {
            pianoMusic.volume = 0.8f;
            pianoVisualizer.volscale = 8f;
            pianoVisualizerObj.SetActive(true);
        }

        // 20 second mark
        if (timer >= 20f && timer <= 20.2f)
        {
            pianoMusic.volume = 0.2f;
            pianoVisualizer.volscale = 2f;
            pianoVisualizerObj.SetActive(true);
        }

        // 30 second mark

        // 40 second mark

        // 50 second mark

        // 1 minute mark

        // 1 minute 10 second mark

        // 1 minute 20 second mark


        // Decrease volume with downward swipe
        if (OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.RTouch) && OVRInput.GetLocalControllerAcceleration(OVRInput.Controller.RTouch).y > 2)
        {
            switch(_selectorScript.instSelection)
            {
                case 0:
                    if(trumpetMusic.volume > 0.5f)
                    {
                        trumpetMusic.volume = 0.5f;
                        trumpetVisualizer.volscale = 5f;
                        trumpetVisualizerObj.SetActive(false);
                        _gameScore += 100;
                    }
                    break;
                case 1:
                    if (drumMusic.volume > 0.5f)
                    {
                        drumMusic.volume = 0.5f;
                        drumVisualizer.volscale = 5f;
                        drumVisualizerObj.SetActive(false);
                        _gameScore += 100;
                    }
                    break;
                case 2:
                    if (pianoMusic.volume > 0.5f)
                    {
                        pianoMusic.volume = 0.5f;
                        pianoVisualizer.volscale = 5f;
                        pianoVisualizerObj.SetActive(false);
                        _gameScore += 100;
                    }
                    break;
                case 3:
                    if (harpMusic.volume > 0.5f)
                    {
                        harpMusic.volume = 0.5f;
                        harpVisualizer.volscale = 5f;
                        harpVisualizerObj.SetActive(false);
                        _gameScore += 100;
                    }
                    break;
                case 4:
                    if (violinMusic.volume > 0.5f)
                    {
                        violinMusic.volume = 0.5f;
                        violinVisualizer.volscale = 5f;
                        violinVisualizerObj.SetActive(false);
                        _gameScore += 100;
                    }
                    break;
            }
        }

        // Increase volume with upward swipe
        if (OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.RTouch) && OVRInput.GetLocalControllerAcceleration(OVRInput.Controller.RTouch).y < -2)
        {
            switch (_selectorScript.instSelection)
            {
                case 0:
                    if (trumpetMusic.volume < 0.5f)
                    {
                        trumpetMusic.volume = 0.5f;
                        trumpetVisualizer.volscale = 5f;
                        trumpetVisualizerObj.SetActive(false);
                        _gameScore += 100;
                    }
                    break;
                case 1:
                    if (drumMusic.volume < 0.5f)
                    {
                        drumMusic.volume = 0.5f;
                        drumVisualizer.volscale = 5f;
                        drumVisualizerObj.SetActive(false);
                        _gameScore += 100;
                    }
                    break;
                case 2:
                    if (pianoMusic.volume < 0.5f)
                    {
                        pianoMusic.volume = 0.5f;
                        pianoVisualizer.volscale = 5f;
                        pianoVisualizerObj.SetActive(false);
                        _gameScore += 100;
                    }
                    break;
                case 3:
                    if (pianoMusic.volume < 0.5f)
                    {
                        harpMusic.volume = 0.5f;
                        harpVisualizer.volscale = 5f;
                        harpVisualizerObj.SetActive(false);
                        _gameScore += 100;
                    }
                    break;
                case 4:
                    if (violinMusic.volume < 0.5f)
                    {
                        violinMusic.volume = 0.5f;
                        violinVisualizer.volscale = 5f;
                        violinVisualizerObj.SetActive(false);
                        _gameScore += 100;
                    }
                    break;
            }

        }
        scoreObject.text = "Score: " + _gameScore;
    }

    private bool KeyTimeGoodEnough( double t )
    {
        float beat = _bpmScale * (float)t;
        float nearestBeat = Mathf.Round(beat);
        float beatDifference = Mathf.Abs(beat - nearestBeat);

        return beatDifference < conductorTolerance;
    }

    public void SetBPM(int bpm)
    {
        _bpm = bpm;
        _bpmScale = _bpm / 60.0f;
    }

    public int GetBPM()
    {
        return _bpm;
    }

    void FixedUpdate()
    {
        OVRInput.FixedUpdate();
    }
}
