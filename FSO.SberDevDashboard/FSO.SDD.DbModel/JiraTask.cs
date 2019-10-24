using System;
using System.ComponentModel.DataAnnotations;

namespace FSO.SDD.DbModel
{
    public class JiraTask
    {
        /// <summary>
        /// Идентификатор (автоинкремент)
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// уникальный ключ задачи (например mr-666)
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Автор - позволит отслеживать статус созданных мной задач
        /// </summary>
        public User Author { get; set; }

        public int AuthorId { get; set; }

        /// <summary>
        /// На кого задача назначена
        /// </summary>
        public User Owner { get; set; } 

        /// <summary>
        /// проектная область
        /// </summary>
        public string ProjectKey { get; set; }

        /// <summary>
        /// КЭ
        /// </summary>
        public string ConfigurationItem { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

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

        public DateTime CreatedDateTime { get; set; }

        public DateTime UpdatedDateTime { get; set; }

        public JiraTaskState State { get; set; }

        public int StateId { get; set; }
    }
}