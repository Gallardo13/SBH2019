using System.ComponentModel.DataAnnotations;

namespace FSO.SDD.DbModel
{
    /// <summary>
    /// Задача, вклоюченная в релиз
    /// </summary>
    public class JiraReleaseTask
    {
        /// <summary>
        /// Ид 
        /// </summary>
        [Key]
        public int Id { get; set; }

        public int ReleaseId { get; set; }

        public int TaskId { get; set; }

    }
}