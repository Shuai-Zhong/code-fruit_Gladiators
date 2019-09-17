using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingScoreHandler : MonoBehaviour
{
    // These 3 values need to be set here rather than start because of engine bug
    private string value = "10";
    private float lifeTime = 2f;
    private float counter = 0f;

    private float circleSpeed;
    private float circleSize;
    private float circleGrowSpeed;

    void Awake()
    {
        GetComponent<TextMesh>().text = value;

        gameObject.GetComponent<MeshRenderer>().sortingLayerName = "Foreground";
        gameObject.GetComponent<MeshRenderer>().sortingOrder = 10;
    }

    private void Start()
    {
        Invoke("DestroyText", lifeTime);

        circleSpeed = 3f;
        circleSize = 1f;
        circleGrowSpeed = 0.2f;
    }

    private void Update()
    {
        // Doesn't execute if paused
        if(Time.timeScale == 0)
        {
            return;
        }

        counter += Time.deltaTime * circleSpeed;

        float xPos = Mathf.Cos(counter) * circleSize * circleGrowSpeed;
        float yPos = Mathf.Sin(counter) * circleSize * circleGrowSpeed;

        transform.localPosition = new Vector3(xPos, yPos, 0);

        circleSize += circleGrowSpeed;
    }

    // Destroy floating text
    private void DestroyText()
    {
        Destroy(transform.parent.gameObject);
    }
}