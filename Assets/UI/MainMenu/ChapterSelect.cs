using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChapterSelect : MonoBehaviour
{
    public void Chapter(int SceneIndex)
    {
        SceneManager.LoadScene(SceneIndex);
    }
}
