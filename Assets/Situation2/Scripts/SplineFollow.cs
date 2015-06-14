using UnityEngine;

public class SplineFollow : MonoBehaviour {

	public BezierSpline spline;

	public float duration;

	public SplineWalkerMode mode;

	public float late;

	private float progress;
	private bool goingForward = true;
	private MovementManager mover;
	private float speed = 1.0f;
	private float slowDownDistance = 2.0f;
	private float stopDistance = 0.1f;

	// Use of factory design pattern
	public static SplineFollow CreateSplineFollow ( GameObject go, BezierSpline spline, float duration, 
	                                               SplineWalkerMode mode, float late) {

		SplineFollow this_obj = go.AddComponent<SplineFollow>();

		//calls Start() on the object and initializes it.
		this_obj.spline = spline;
		this_obj.duration = duration;
		this_obj.mode = mode;
		this_obj.late = late;
		
		return this_obj;
	}

	private void Start() {
		mover = GetComponent<MovementManager>();
	}

	private void Update () {

		if (late > 0) {
			late -= Time.deltaTime;
			return;
		}
		if (goingForward) {
			progress += (Time.deltaTime * speed / duration);
			if (progress > 1f) {
				if (mode == SplineWalkerMode.Once) {
					progress = 1f;
				}
				else if (mode == SplineWalkerMode.Loop) {
					progress -= 1f;
				}
				else {
					progress = 2f - progress;
					goingForward = false;
				}
			}
		}
		else {
			progress -= (Time.deltaTime / duration) * speed;
			if (progress < 0f) {
				progress = -progress;
				goingForward = true;
			}
		}

		Vector3 direction = spline.GetDirection(progress);
		direction.Normalize ();
		Vector2 direction2D = new Vector2 (direction.x, direction.z);

		mover.Move (
			InputManager.ControlType.THIRD_PERSON,
			direction2D,
			new Vector2(0, 0),
			true
		);

		Vector3 position = spline.GetPoint(progress);
		transform.localPosition = position;

		int i = spline.GetNextImportantPointIndex(progress);

		if (spline.GetImportantPointMode(i) == BezierControlPointMode.Aligned) {
			Vector3 nextPosition = spline.GetImportantPoint(i);
			float distance = Vector3.Distance(position, nextPosition);
			if (distance < slowDownDistance) {
				if (distance > stopDistance) {
					speed = distance  / (slowDownDistance - stopDistance);
					return;
				}
			}
		}

		speed = 1.0f;



	}
}