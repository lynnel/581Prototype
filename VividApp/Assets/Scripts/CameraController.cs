using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraController : MonoBehaviour
{
	// Objects
	public GameObject plane = null;
	public static WebCamTexture deviceCam = null;
	public bool isPicture = false;
	public Sprite unpressed;
	public Sprite pressed;
	public GameObject menuButton ;

	// Setting up device camera to render to game plane
	public void Start () {
		deviceCam = new WebCamTexture();
		plane = GameObject.Find ("Plane");
		plane.GetComponent<Renderer>().material.mainTexture = deviceCam;
		deviceCam.Play();

		gameObject.GetComponentInChildren<Button>().image.sprite = unpressed;

		StartCoroutine(FixAspectRatio());
	}

	public IEnumerator FixAspectRatio() {
		while (deviceCam.width < 100) {
			yield return null;
		}

		int correctWidth = deviceCam.width;
		int correctHeight = deviceCam.height;
		float newWidth = (float)correctWidth / (float)correctHeight;
#if UNITY_EDITOR
		plane.transform.localScale = new Vector3 (newWidth, 1f, 1f);
#elif UNITY_ANDROID
		plane.transform.localScale = new Vector3 (newWidth, 1f, 1f);
		plane.transform.Rotate (new Vector3(0,90,0));
#endif
	}
		
	// toggles camera on and off
	public void ToggleCamera() {
		if (isPicture) {
			deviceCam.Play();
			isPicture = false;
			gameObject.GetComponentInChildren<Button>().image.sprite = unpressed;
		} else {
			deviceCam.Pause();
			isPicture = true;
			gameObject.GetComponentInChildren<Button>().image.sprite = pressed;
		}
	}

	// function that loads main menu
	public void ReturnToMenu() {
		Application.LoadLevel ("Menu");
	}
	
}


