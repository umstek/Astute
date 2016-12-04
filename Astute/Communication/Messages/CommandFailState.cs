namespace Astute.Communication.Messages
{
    public enum CommandFailState
    {
        Obstacle,
        CellOccupied,
        Dead,
        TooQuick,
        InvalidCell,
        GameHasFinished,
        GameNotStartedYet,
        NotAValidContestant
    }
}