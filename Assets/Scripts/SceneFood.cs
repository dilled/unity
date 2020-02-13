using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneFood : MonoBehaviour
{
    
    public void EatFood(GameObject food)
    {
        food.SetActive(false);
        StartCoroutine(Food(food));
    }
    IEnumerator Food(GameObject food)
    {
        yield return new WaitForSeconds(120);
        food.SetActive(true);
    }
}
