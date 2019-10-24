using System.ComponentModel.DataAnnotations;

namespace FSO.SDD.DbModel
{

    /// <summary>
    /// Задача, включенная в спринт
    /// </summary>
    public class JiraSprintTask
    {
        /// <summary>
        /// Ид 
        /// </summary>
        [Key]
        public int Id { get; set; }

        public int SprintId { get; set; }

        public int TaskId { get; set; }

    }
}