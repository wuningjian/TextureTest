using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ParticleTestScript : MonoBehaviour {

    private ParticleSystem ps;
    private ParticleSystemRenderer psr;
    public ParticleSystemRenderMode renderMode = ParticleSystemRenderMode.Billboard;
    public float cameraScale = 0.0f;
    public float lengthScale = 0.0f;
    public float velocityScale = 1.0f;

	// Use this for initialization
	void Start () {
        ps = GetComponent<ParticleSystem>();
        psr = GetComponent<ParticleSystemRenderer>();

        psr.mesh = Resources.GetBuiltinResource<Mesh>("Capsule.fbx");

        var main = ps.main;
        main.startSpeed = new ParticleSystem.MinMaxCurve(0.5f, 1.5f);
        main.startSize = new ParticleSystem.MinMaxCurve(0.1f, 0.8f);
	}
	
	// Update is called once per frame
	void Update () {
        psr.renderMode = renderMode;
        psr.cameraVelocityScale = cameraScale;
        psr.lengthScale = lengthScale;
        psr.velocityScale = velocityScale;

        if (renderMode == ParticleSystemRenderMode.Stretch)
        {
            Camera.main.transform.position = new Vector3(Mathf.Sin(Time.time) * 4.0f, 0.0f, -10.0f);
        }
	}

    private void OnGUI()
    {
        renderMode = (ParticleSystemRenderMode)GUI.SelectionGrid(new Rect(25, 25, 900, 30), (int)renderMode, new GUIContent[] {
            new GUIContent("Billboard"),
            new GUIContent("Stretch"),
            new GUIContent("HorizontalBillboard"),
            new GUIContent("VerticalBillboard"),
            new GUIContent("Mesh"),
            new GUIContent("None")
        }, 6);

        //Debug.Log(renderMode);

        if (renderMode == ParticleSystemRenderMode.Stretch)
        {
            GUI.Label(new Rect(25, 80, 100, 30), "Camera Scale");
            GUI.Label(new Rect(25, 120, 100, 30), "Length Scale");
            GUI.Label(new Rect(25, 160, 100, 30), "Velocity Scale");

            cameraScale = GUI.HorizontalSlider(new Rect(125, 85, 100, 30), cameraScale, 0.0f, 10.0f);
            lengthScale = GUI.HorizontalSlider(new Rect(125, 125, 100, 30), lengthScale, 0.0f, 10.0f);
            velocityScale = GUI.HorizontalSlider(new Rect(125, 165, 100, 30), velocityScale, 0.0f, 10.0f);
        }
    }
}
