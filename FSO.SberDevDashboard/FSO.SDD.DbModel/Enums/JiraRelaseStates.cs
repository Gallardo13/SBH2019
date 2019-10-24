namespace FSO.SDD.DbModel.Enums
{
    public enum JiraRelaseStates
    {
        /// <summary>
        /// новый
        /// </summary>
        New = 1,

        /// <summary>
        /// запланирован - есть дата начала и ожидаемого завершения
        /// </summary>
        Planned = 2,

        /// <summary>
        /// Работы начаты, но задачи ещё можно добавлять
        /// </summary>
        InWork = 3,

        /// <summary>
        /// Стабилизация, работа завершается, задачи добавлять по идее нельзя
        /// </summary>
        Stabilization = 4,



        SystemTest = 5,

        IFT = 6,

        AcceptanceTest = 7,

        Production = 8,

        Rejected = 9


    }
}