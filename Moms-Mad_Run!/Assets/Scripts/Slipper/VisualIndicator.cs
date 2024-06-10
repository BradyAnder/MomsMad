using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualIndicator : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public int resolution = 30;
    public float timeStep = 0.1f;

    public void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private Vector3 CalculatePoint(Vector3 originPoint, Vector3 velocity, float time)
    {
        Vector3 position = origin + velocity * time + 0.5f * Physics.gravity * (time * time);
        return position;
    }

    public void resetTrajectory()
    {
        lineRenderer.positionCount = 0;
    }

    public void showTrajectory(Vector3 originPoint, Vector3 velocity, float maxTime)
    {
        lineRenderer.positionCount = resolution;

        List<Vector3> points = new List<Vector3>();

        for (int i = 0; i < resolution; i++) {
            float time = i * timeStep;
            if (maxTime < time) {
                break;
            }
            Vector3 point = CalculatePoint(originPoint, velocity, time);
            points.Add(point);
        }
        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.toArray());
    }
}