using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuClose : MonoBehaviour
{
    Button button;
    public CanvasVisible canvas;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Close);
    }

    void Close()
    {
        canvas.Hide();
    }
}
