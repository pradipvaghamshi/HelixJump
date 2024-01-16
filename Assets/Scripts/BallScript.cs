using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class BallScript : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField]
    GameObject BallWin, BallLose;
    [SerializeField]
    GameObject SplashObject;
    [SerializeField]
    float power, radius;
    float Force = 5;
    bool flag;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "LastBlock")
        {
            int lavelring = PlayerPrefs.GetInt("level", 9);
            lavelring++;
            PlayerPrefs.SetInt("level", lavelring);
            BallWin.SetActive(true);
        }
        else if (collision.gameObject.tag == "BadBlock")
        {
            Debug.Log("BAD");
            BallLose.SetActive(true);
        }
        else
        { 
            transform.DOMoveY(this.gameObject.transform.position.y+1.2f, 0.5f);
            GameObject sp = Instantiate(SplashObject, new Vector3(gameObject.transform.position.x, this.gameObject.transform.position.y - 0.1f, gameObject.transform.position.z), Quaternion.Euler(90, 0, 0), collision.gameObject.transform);
            Destroy(sp, 0.45f);
            BallEffect();
        }

    }
    private void Update()
    {
        float CameraOffsety = 4.02f;
        if (transform.position.y < Camera.main.transform.position.y - CameraOffsety)
        {
            Vector3 pos = new Vector3(Camera.main.transform.position.x, transform.position.y + CameraOffsety, Camera.main.transform.position.z);
            Camera.main.transform.position = pos;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.childCount != null)
        {
            foreach (Transform child in other.transform)
            {

                child.transform.gameObject.AddComponent<Rigidbody>();
                child.transform.gameObject.GetComponent<Rigidbody>().AddExplosionForce(power, child.transform.position, radius, 0.1f);
                child.transform.gameObject.GetComponent<MeshCollider>().convex = false;
                child.transform.gameObject.GetComponent<MeshCollider>().enabled = false;

                Destroy(child.transform.gameObject, 1f);
            }
        }
    }

    void BallEffect()
    {
        Sequence mySequence = DOTween.Sequence();
        // Add a movement tween at the beginning
        mySequence.Append(transform.DOScaleX(0.5f, 0.1f));
        // Add a rotation tween as soon as the previous one is finished
        mySequence.Append(transform.DOScaleY(0.5f, 0.1f));
        // Delay the whole Sequence by 1 second
        mySequence.PrependInterval(0.1f);
        mySequence.Append(transform.DOScaleX(0.6f, 0.1f));
        // Add a rotation tween as soon as the previous one is finished
        mySequence.Append(transform.DOScaleY(0.6f, 0.1f));

    }

    public static void retry()
    {
        SceneManager.LoadScene(0);
    }
}