using System.ComponentModel.DataAnnotations;

namespace FSO.SDD.DbModel
{
    /// <summary>
    ///  - система (jira, git, dpm)
    /// </summary>
    public class SourceSystem
    {
        /// <summary>
        /// Идентификатор (автоинкремент)
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

    }
}