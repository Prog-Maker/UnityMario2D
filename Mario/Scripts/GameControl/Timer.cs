using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Timer : MonoBehaviour {

    private int TimeLeft;

   // [SerializeField]
    private Text textBlock;

	void Start ()
    {
        textBlock = FindObjectsOfType<Text>().Where(x => x.name == "TimeLeft").FirstOrDefault();

        TimeLeft = 400;

        InvokeRepeating("UpdateTime", 0, 0.33f);
	}
	
	
	void UpdateTime ()
    {
        textBlock.text = (TimeLeft--).ToString();
	}
}
