using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
	public Text score_text;

	[HideInInspector]
	public int level_score = 0;

	void Start()
    {
		if (score_text != null)
        {
			score_text.text = ManagerGame.total_score.ToString();
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Pickup"))
		{
			int score = other.gameObject.GetComponent<PickupScore>().value;
			level_score += score;
			ManagerGame.total_score += score;
			ManagerGame.total_score = Mathf.Max(ManagerGame.total_score, 0);
			if (score_text != null)
            {
				score_text.text = ManagerGame.total_score.ToString();
			}
			Destroy(other.gameObject);
		}
	}
}
