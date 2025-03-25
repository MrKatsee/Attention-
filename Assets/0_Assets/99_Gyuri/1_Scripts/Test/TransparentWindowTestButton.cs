using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransparentWindowTestButton : MonoBehaviour
{
    [SerializeField]
    private Button button;
    [SerializeField]
    private GameObject circle;

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(test);
    }

    void test()
    {
        
        circle.SetActive(!circle.activeSelf);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
