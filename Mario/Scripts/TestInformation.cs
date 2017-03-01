using UnityEngine;
using UnityEngine.UI;

public class TestInformation : MonoBehaviour {

    [SerializeField]
    private Text informationText;

    public static string Info = "";

	void Update ()
    {
        informationText.text = Info; // +  "\n разрешение экрана" +Camera.main.aspect;
	}
}
