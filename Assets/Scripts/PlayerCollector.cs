using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
	public Text score_text;

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
			other.gameObject.SetActive(false);
			ManagerGame.total_score += other.gameObject.GetComponent<PickupScore>().value;
			ManagerGame.total_score = Mathf.Max(ManagerGame.total_score, 0);
			if (score_text != null)
            {
				score_text.text = ManagerGame.total_score.ToString();
			}
		}
	}

	void OnDestroy()
	{
		ManagerGame.last_level_time = Time.timeSinceLevelLoad;
	}
}
