using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{

    #region Public Variables

    public Player Player;
    public CompositeCollider2D[] bounds;
    public CinemachineVirtualCamera[] cameras;
    public Vector3 currentCameraPosition;
    public GameObject CMCameras;
    public GameObject Sky;

    #endregion

    public int newCameraIndex;


    // Start is called before the first frame update
    void Start()
    {

        newCameraIndex = 0;

    }

    // Update is called once per frame
    void Update()
    {
    
        for (int i = 0; i < bounds.Length; i++) {
            if(bounds[i].bounds.Contains(Player.transform.position)) {
                newCameraIndex = i;
            }
        }
        
        currentCameraPosition = cameras[newCameraIndex].gameObject.transform.position;
        Sky.gameObject.transform.position = new Vector3(cameras[newCameraIndex].gameObject.transform.position.x, cameras[newCameraIndex].gameObject.transform.position.y, Sky.gameObject.transform.position.z);
        ChangeCamera(newCameraIndex);
    }

    public IEnumerator ShakeTheCamera (float duration, float magnitude) 
    {

       float elapsed = 0.0f;

       while (elapsed < duration) 
       {
           float x = Random.Range(-1f, 1f) * magnitude;
           float y = Random.Range(-1f, 1f) * magnitude;

           CMCameras.transform.position = new Vector3(x, y, 0f);

            elapsed += Time.deltaTime;

           yield return null;
       }

       CMCameras.transform.position = new Vector3(0f, 0f, 0f);
    }

    public void ChangeCamera(int index) {

        for (int i = 0; i < cameras.Length; i++) {
            cameras[i].gameObject.SetActive(false);
        }
        cameras[index].gameObject.SetActive(true);
    }

}
