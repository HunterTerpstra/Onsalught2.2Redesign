using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMenuUI : MonoBehaviour
{
    CanvasVisible canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<CanvasVisible>();
    }

    public void Show() {
        canvas.Show();
    }

    public void Hide() {
        canvas.Hide();
    }
}
