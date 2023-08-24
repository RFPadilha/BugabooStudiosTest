using System;
public interface IPlayerController
{
    int score { get; set; }

    event Action<int> OnScoreChanged;

    void IncreaseScore(int value);
}