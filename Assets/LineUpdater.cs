using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class LineUpdater : MonoBehaviour
{
    GameObject origin;
    GameObject target;
    LineRenderer lineRenderer;
    BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        origin = gameObject.transform.Find("Origin").gameObject;
        target = gameObject.transform.Find("Target").gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        UpdateLinePositions();
    }

    void UpdateLinePositions()
    {
        if (lineRenderer == null || origin == null || target == null) { return; }

        lineRenderer.SetPosition(0, origin.transform.position);
        lineRenderer.SetPosition(1, target.transform.position);

    }
}
