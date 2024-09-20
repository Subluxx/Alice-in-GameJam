using UnityEngine;
using System.Collections.Generic;

public class TransformationGrid : MonoBehaviour {

	public Transform prefab;

	public int gridResolution = 10;

	Transform[] grid;
    Matrix4x4 transformation;
    List<Transformation> transformations;
    public ScaleTransformation scaleTransformation;

	void Awake () {
		grid = new Transform[gridResolution * gridResolution * gridResolution];
        for (int i = 0, y = 0; y < gridResolution; y++) {
            for (int x = 0; x < gridResolution; x++, i++) {
                grid[i] = CreateGridPoint(x, y);
            }
        }
        transformations = new List<Transformation>();
        
	}
    void Update () {
		UpdateTransformation();
        GetComponents<Transformation>(transformations);
        for (int i = 0, y = 0; y < gridResolution; y++) {
            for (int x = 0; x < gridResolution; x++, i++) {
                grid[i].localPosition = TransformPoint(x, y);
                grid[i].localScale = TransformScale(x,y);
            }
        }
	}
    Transform CreateGridPoint (int x, int y) {
		Transform point = Instantiate<Transform>(prefab);
		point.localPosition = GetCoordinates(x, y);
		point.GetComponent<MeshRenderer>().material.color = new Color(
			(float)x / gridResolution,
			(float)y / gridResolution,
            0
			//(float)z / gridResolution
		);
		return point;
	}
    Vector3 GetCoordinates (int x, int y) {
		return new Vector3(
			x - (gridResolution - 1) * 0.5f,
			y - (gridResolution - 1) * 0.5f
			//z - (gridResolution - 1) * 0.5f
		);
	}
    Vector3 TransformPoint (int x, int y) {
		Vector3 coordinates = GetCoordinates(x, y);
		return transformation.MultiplyPoint(coordinates);
	}
    Vector3 TransformScale (int x, int y) {
        //Debug.Log(scale);
        return scaleTransformation.scale;
    }
    void UpdateTransformation () {
		GetComponents<Transformation>(transformations);
		if (transformations.Count > 0) {
			transformation = transformations[0].Matrix;
			for (int i = 1; i < transformations.Count; i++) {
				transformation = transformations[i].Matrix * transformation;
			}
		}
	}
}
