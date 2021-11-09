using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;
using DG.Tweening;

public class CurrentSceneManager : MonoBehaviour
{
    [SerializeField]
    private Question[] questions;
    [SerializeField]
    private float transitionTime;
    [SerializeField]
    private TextMeshProUGUI questionText;

    [SerializeField]
    private TextMeshProUGUI feedBackTrue;
    [SerializeField]
    private TextMeshProUGUI feedBackFalse;

    [SerializeField]
    private Transform trueButton;
    [SerializeField]
    private Transform falseButton;


    private static List<Question> unasweredQuestions;
    private Question currentQuestion;

    private void Awake()
    {
        if (unasweredQuestions == null || unasweredQuestions.Count == 0)
        {
            unasweredQuestions = questions.ToList<Question>();
        }
        SetRandomQuestion();
        
    }


    private void SetRandomQuestion()
    {
        int randomIndex = Random.Range(0, unasweredQuestions.Count);
        currentQuestion = unasweredQuestions[randomIndex];
        questionText.SetText(currentQuestion.questionText);
        unasweredQuestions.RemoveAt(randomIndex);

        if (currentQuestion.isTrue)
        {
            feedBackTrue.SetText("WRONG");
            feedBackFalse.SetText("CORRECT");
        }
        else
        {
            feedBackFalse.SetText("WRONG");
            feedBackTrue.SetText("CORRECT");
        }


    }

    public void UserSelectTrue()
    {
        falseButton.DOMoveX(falseButton.position.x + 500f, 0.2f);

        StartCoroutine(TransitionToNextQuestion());


    }
    public void UserSelectFalse()
    {
        trueButton.DOMoveX(trueButton.position.x - 500f, 0.2f);

        StartCoroutine(TransitionToNextQuestion());
       
    }

    private IEnumerator TransitionToNextQuestion()
    {
        
        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }

}
