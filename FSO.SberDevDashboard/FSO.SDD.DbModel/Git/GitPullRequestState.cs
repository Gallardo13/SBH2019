using System.ComponentModel.DataAnnotations;

namespace FSO.SDD.DbModel
{
    /// <summary>
    /// состояние пулреквеста
    /// </summary>
    public class GitPullRequestState
    {
        /// <summary>
        /// Ид 
        /// </summary>
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}