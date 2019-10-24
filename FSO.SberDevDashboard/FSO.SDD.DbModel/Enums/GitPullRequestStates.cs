namespace FSO.SDD.DbModel
{
    /// <summary>
    /// состояние пулреквеста
    /// </summary>
    public enum GitPullRequestStates
    {
        Opened = 1,
        NeedWork = 2,
        Approved = 3,
        Merged = 4

    }
}