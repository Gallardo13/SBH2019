namespace FSO.SDD.DbModel.Enums
{
    public enum JiraTaskStates
    {
        /// <summary>
        /// Создана
        /// </summary>
        Created = 1,

        /// <summary>
        /// Оценена и запланирована
        /// </summary>
        Planned = 2,

        /// <summary>
        /// Включена в спринт
        /// </summary>
        Open = 3,

        /// <summary>
        /// взята в работу
        /// </summary>
        InWork = 4,

        /// <summary>
        /// Заавершена
        /// </summary>
        Done = 5,

        /// <summary>
        /// Отклонена
        /// </summary>
        Rejected = 6
    }
}