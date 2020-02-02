using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StackPanel : MonoBehaviour {

    private int max = 6;
    private Queue<string> queue;

    public Sprite Vide;
    public Sprite Left;
    public Sprite Right;
    public Sprite Jump;
    public Sprite ForreuseOn;
    public Sprite ForreuseOff;
    public Sprite AnalyserOn;
    public Sprite AnalyserOff;

	// Use this for initialization
	void Start () {
        queue = new Queue<string>();
    }

    public bool AddAction(string action)
    {
        if(queue.Count<max)
        {
            queue.Enqueue(action);
            UpdateActions();
            return true;
        }
        return false;
    }

    public void ConsumeAction()
    {
        if (queue.Count > 0)
        {
            queue.Dequeue();
            UpdateActions();
        }
    }
    public string GetAction()
    {
        if (queue.Count == 0) return null;
        return queue.Peek();
    }

    private void UpdateAction(int i, string action)
    {
        Image image = transform.GetChild(i).GetComponent<Image>();
        if (action == "left") image.sprite = Left;
        else if (action == "right") image.sprite = Right;
        else if (action == "jump") image.sprite = Jump;
        else if (action == "forreuse-on") image.sprite = ForreuseOn;
        else if (action == "forreuse-off") image.sprite = ForreuseOff;
        else if (action == "analyser-on") image.sprite = AnalyserOn;
        else if (action == "analyser-off") image.sprite = AnalyserOff;
        else image.sprite = Vide;
    }

    // Update is called once per frame
    private void UpdateActions () {
        int i = 0;
		foreach(string action in queue)
        {
            UpdateAction(i++, action);
        }
        while(i<max)
        {
            UpdateAction(i++, null);
        }
    }
}
