using System.ComponentModel.DataAnnotations;

namespace FSO.SDD.DbModel
{
    /// <summary>
    /// Задача, ассоциированная с коммитом
    /// </summary>
    public class GitCommitTask
    {
        /// <summary>
        /// Ид 
        /// </summary>
        [Key]
        public int Id { get; set; }

        public int CommitId { get; set; }

        public int TaskId { get; set; }


    }
}