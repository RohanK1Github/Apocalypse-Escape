using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class resultsController : MonoBehaviour
{

    public waveController wc;
    public points p;
    public EnemyAI e;
    public bossEnemyAI be;

    public TextMeshProUGUI totalPoints;
    public TextMeshProUGUI wavesSurvived;



    public int endPoints;





    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        endPoints = p.totalPoints;
        totalPoints.text = "Total score earned: " + endPoints.ToString();
        wavesSurvived.text = "Waves survived: " + wc.waveNumber.ToString();

        
    }
}
