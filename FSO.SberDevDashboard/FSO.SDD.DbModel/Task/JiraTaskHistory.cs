using System;
using System.ComponentModel.DataAnnotations;

namespace FSO.SDD.DbModel
{
    public class JiraTaskHistory
    {
        /// <summary>
        /// Идентификатор (автоинкремент)
        /// </summary>
        [Key]
        public int Id { get; set; }

        public JiraTask Task { get; set; }

        public int TaskId { get; set; }

        /// <summary>
        /// На кого задача назначена
        /// </summary>
        public User Owner { get; set; }
        public int OwnerId { get; set; }

        /// <summary>
        /// Критичность дефекта (1-5. 0 - не дефект)
        /// </summary>
        public int DefectSeverity { get; set; }

        /// <summary>
        /// оценка остака ёмкости задачи в сторипоинтах
        /// </summary>
        public int Estimation { get; set; }

        /// <summary>
        /// Остаток ёмкости
        /// </summary>
        public int Remainder { get; set; }

        /// <summary>
        /// оценка ёмкости задачи в сторипоинтах до начала работ
        /// </summary>
        public int OriginalEstimation { get; set; }

        public DateTime UpdatedDateTime { get; set; }

        public JiraTaskState State { get; set; }

        public int StateId { get; set; }
    }
}