using UnityEngine;

public class CameraOrbit : MonoBehaviour {
    protected Transform _Xform_Camera;
    protected Transform _Xform_Parent;

    protected Vector3 _localRotation;
    protected float _CameraDistance = 40f;

    public float MouseSensitivity = 4f;
    public float ScrollSensitivity = 2f;
    public float OrbitDampening = 10f;
    public float ScrollDampening = 6f;

    public bool CameraDisabled = false;
    // Use this for initialization
    void Start () {
        this._Xform_Camera = this.transform;
        this._Xform_Parent = this.transform.parent;
	}
	
	// Update is called once per frame, after update() on every game object in the scene
	void LateUpdate () {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            CameraDisabled = !CameraDisabled;

        if (!CameraDisabled)
        {
            //Rotate camera based on Mouse Coordinates
            if(Input.GetAxis("Mouse X")!=0 && Input.GetMouseButton(1) || 
                Input.GetAxis("Mouse Y") != 0 && Input.GetMouseButton(1))
            {
                _localRotation.x += Input.GetAxis("Mouse X") * MouseSensitivity;
                _localRotation.y -= Input.GetAxis("Mouse Y") * MouseSensitivity;

                //clamp the y rotation to horizon and not flipping over at the top
                _localRotation.y = Mathf.Clamp(_localRotation.y, -90f, 90f);
            }

            if(Input.GetAxis("Mouse ScrollWheel") != 0f)
            {
                float ScrollAmount = Input.GetAxis("Mouse ScrollWheel") * ScrollSensitivity;
                //farer scroll faster, nearer slower
                ScrollAmount *= (this._CameraDistance*0.3f);

                this._CameraDistance += ScrollAmount * -1f;

                //camera no closer than 1 meter, no further than 100meters
                this._CameraDistance = Mathf.Clamp(this._CameraDistance, 30f, 50f);
            }

            Quaternion QT = Quaternion.Euler(_localRotation.y, _localRotation.x, 0);
            this._Xform_Parent.rotation = Quaternion.Lerp(this._Xform_Parent.rotation,QT, Time.deltaTime * OrbitDampening);

            if(this._Xform_Camera.localPosition.z != this._CameraDistance * -1f)
            {
                this._Xform_Camera.localPosition = new Vector3(0f, 0f, Mathf.Lerp(this._Xform_Camera.localPosition.z, this._CameraDistance *-1f, Time.deltaTime*ScrollDampening));
            }
        }
	}
}
