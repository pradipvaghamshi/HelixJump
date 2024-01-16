using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Vector3 Startpos, Differntpos;
    [SerializeField]
    GameObject HelixObject;
    [SerializeField]
    float speed;
    [SerializeField]
    GameObject[] LavelPrefb;
    void Start()
    {
        StartLavel();
        //Debug.Log("screen width is =" + Screen.width + "== screen height is = " + Screen.height);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Startpos = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            Differntpos = Startpos - Input.mousePosition;
            HelixObject.transform.Rotate(new Vector3(0, Differntpos.x * speed, 0));
            Startpos = Input.mousePosition;
        }
    }
    public void StartLavel()
    {
        int Lavelirng = PlayerPrefs.GetInt("lavel", 9);
        for (int i = 0; i < Lavelirng; i++)
        {
            GameObject g;
            if (i == 0)
            {
                g = Instantiate(LavelPrefb[0], HelixObject.transform);
            }
            else if (i == Lavelirng - 1)
            {
                g = Instantiate(LavelPrefb[LavelPrefb.Length - 1], HelixObject.transform);
            }
            else
            {
                g = Instantiate(LavelPrefb[Random.Range(1, LavelPrefb.Length-1)], HelixObject.transform);
            }
            g.transform.Translate(new Vector3(0, -i * 4f, 0));
            g.transform.Rotate(0, Random.Range(0, 360), 0);
        }
    }
}
