using System.ComponentModel.DataAnnotations;

namespace FSO.SDD.DbModel
{
    /// <summary>
    /// Задача, включенная в эпик
    /// </summary>
    public class JiraEpicTask
    {
        /// <summary>
        /// Ид 
        /// </summary>
        [Key]
        public int Id { get; set; }

        public int EpicId { get; set; }

        public int TaskId { get; set; }

    }
}