using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField] public AudioSource click;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnClick()
    {
        click.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
