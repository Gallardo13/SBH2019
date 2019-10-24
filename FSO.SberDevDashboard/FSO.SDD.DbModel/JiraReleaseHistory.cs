using System.ComponentModel.DataAnnotations;

namespace FSO.SDD.DbModel
{
    /// <summary>
    /// История Релиза
    /// </summary>
    public class JiraReleaseHistory
    {
        /// <summary>
        /// Ид 
        /// </summary>
        [Key]
        public int Id { get; set; }

        public int ReleaseId { get; set; }


        public JiraReleaseState State { get; set; }

        public int StateId { get; set; }

        /// <summary>
        /// Суммарная оценка ёмкости задач, которые включены в релиз
        /// </summary>

        public int TasksEstimationValue { get; set; }

        /// <summary>
        /// Остаточная оценка ёмкости задач, которые включены в релиз
        /// </summary>

        public int TasksRemainderValue { get; set; }
    }
}