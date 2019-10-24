using System;
using System.ComponentModel.DataAnnotations;

namespace FSO.SDD.DbModel
{
    /// <summary>
    /// Согласующие пулреквест
    /// </summary>
    public class GitPullRequestApprover
    {
        /// <summary>
        /// Ид 
        /// </summary>
        [Key]
        public int Id { get; set; }

        public int PullRequestId { get; set; }

        public int UserId { get; set; }




    }
}