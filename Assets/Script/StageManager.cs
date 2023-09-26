using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class StageManager : Singleton<StageManager>
{
    // Stage Management
    [Header("Stages")]
    [SerializeField] int stageNum = 1;
    StageState[] stageStates;

    [SerializeField] public int CurrentStage = 0;
    [SerializeField] public int CurrentPage = 0;
    [SerializeField] MapData Map;
    [SerializeField] PageManager _pageManager;
    [SerializeField] Transform _playerTransform;
    private void Start()
    {
        LoadCurrentStageAndPage();
        // Init Stage States
        stageStates = new StageState[stageNum];
        Array.Fill(stageStates, StageState.NotClear);
    }



    // Stage Management Methods
    public StageState GetStageState(int index)
    {
        return stageStates[index];
    }
    public void LoadCurrentStageAndPage()
    {
        int stageIndex = CurrentStage;
        int pageIndex = CurrentPage;

        LoadPlayerSpawnPosition();
        _pageManager.ResetPages();
        _pageManager.UpdatePagesIndex(pageIndex);
        for (int i = 0; i <= pageIndex; i++)
        {
            var PageSections = Map.Stages[stageIndex].Pages[i].Sections;
            for (int j = 0; j < PageSections.Count; j++)
            {
                if (j == 0)
                    _pageManager.leftPages.Add(PageSections[j]);
                else if (j == 1)
                    _pageManager.middlePages.Add(PageSections[j]);
                else if (j == 2)
                    _pageManager.rightPages.Add(PageSections[j]);
                if (i != pageIndex)
                {
                    PageSections[j].SetActive(false);
                }
                else
                {
                    PageSections[j].SetActive(true);
                }
            }
        }
    }

    public void LoadPlayerSpawnPosition()
    {
        _playerTransform.position = Map.Stages[CurrentStage].Pages[CurrentPage].RespawnPosition;
    }

    public void NextPage()
    {
        CurrentPage++;
        if (CurrentPage >= 3)
        {
            CurrentPage = CurrentPage % 3;
            NextStage();
        }
        LoadCurrentStageAndPage();
    }

    public void NextStage()
    {
        CurrentStage++;
    }
    public enum StageState
    {
        NotClear,
        Clear,
        StarClear,
        None
    }
}
