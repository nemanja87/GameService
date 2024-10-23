using System;

public interface IGameService
{
    GameResult Play(GameChoice playerChoice, GameChoice computerChoice);
}
