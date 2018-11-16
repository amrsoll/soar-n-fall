using UnityEngine;


public class CameraController : MonoBehaviour {

	public const byte LEFT = 1;
	public const byte RIGHT = 2;
	private static readonly float BIOME_CAM_SIZE = (float)Biome.BlockSize*5;
	//load from JSON
	
	private const float PLAYER_CAM_SIZE = 2.7f;

	public Camera thisCam;
	public GameObject player;       //Public variable to store a reference to the player game object
	public BiomeManager biomeManager;

	private readonly Vector3 BiomeCenterOffset = new Vector3((float) Biome.XSize / 2,
															 (float) Biome.YSize / 2,
															 (float) Biome.ZSize / 2);

	private Vector3 prevCamOffset;         //Private variable to store the offset distance between the player and camera
	private Vector3 curCamOffset;         //Private variable to store the offset distance between the player and camera
	private Vector3 nextCamOffset;         //Private variable to store the offset distance between the player and camera
	private Vector3 camTarget;
	private float camSize;
	private Vector3Int _currentBiomePos;
	// private float delta;
	//testing
	private bool isDoneRotating = true;
	private byte rotationDirection = 0;
	private bool followThePlayer = false;

	// returns true if the camera is already centered on the position v
	private bool Follow(Vector3 v, float size, float strength = 2f) {
		//TODO emulate distance with the size of the viewfield
		thisCam.orthographicSize = Mathf.Lerp(thisCam.orthographicSize, size, strength*Time.deltaTime );
		transform.position = Vector3.Slerp(transform.position, v+curCamOffset, strength*Time.deltaTime);
		Quaternion _w = transform.rotation;
		transform.LookAt(camTarget, Vector3.up);
		transform.rotation = Quaternion.Lerp(_w, transform.rotation, strength*Time.deltaTime);
		return transform.position == v+curCamOffset;
	}

	private Vector3 RotateBy90Up(Vector3 v, int direction) {
		Quaternion q;
		if(direction >= 0) {
			q = Quaternion.AngleAxis( 90, Vector3.up);
		} else {
			q = Quaternion.AngleAxis(-90, Vector3.up);
		}
		return q * v;
	}

	// direction should either be LEFT or RIGHT (but can take any positive or negative value with the same result)
	// Example :
	// 	if(!isDoneRotating)
	// 		isDoneRotating = rotateCamera(RIGHT, 1.2f);
	public void rotateCamera(byte direction = 1) {
		int dir = 0;
		if(direction == LEFT) dir = 1;
		if(direction == RIGHT) dir = -1;
		curCamOffset = RotateBy90Up(curCamOffset, dir);
	}

	public void SwitchView() {
		followThePlayer = !followThePlayer;
		if(followThePlayer) {
			camTarget = player.transform.position;
			camSize = PLAYER_CAM_SIZE;
		} else {
			camTarget = _currentBiomePos + BiomeCenterOffset;
			camSize = BIOME_CAM_SIZE;
		}
	}

	// Use this for initialization
	void Start () {
		_currentBiomePos = new Vector3Int(0,0,0);
		prevCamOffset = Biome.BlockSize * new Vector3(Biome.XSize, Biome.YSize, Biome.ZSize);
		curCamOffset = prevCamOffset;
		followThePlayer = false;
		SwitchView();
		thisCam.orthographicSize = camSize;
		transform.position = camTarget + curCamOffset;
		transform.LookAt(camTarget, Vector3.up);
		
		
	}

	void Update() {
    	if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.JoystickButton5)){
			rotateCamera(RIGHT);
        }
		if(Input.GetKeyDown(KeyCode.JoystickButton4)) {
			rotateCamera(LEFT);
		}
		if(Input.GetKeyDown(KeyCode.V)) {
			SwitchView();
		}
        if (followThePlayer)
        {
            camTarget = player.transform.position; //needs to be refreshed
        } else
        {
            _currentBiomePos = BiomeManager.WorldToBiomePos(player.transform.position);
            camTarget = BiomeManager.BiomeToWorldPos(_currentBiomePos) + BiomeCenterOffset;
        }
        Follow(camTarget, camSize);
	}
}
