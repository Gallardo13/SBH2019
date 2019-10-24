using System.ComponentModel.DataAnnotations;

namespace FSO.SDD.DbModel
{
    /// <summary>
    /// статус задачи в jira
    /// </summary>
    public class JiraTaskState
    {
        /// <summary>
        /// Ид 
        /// </summary>
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}