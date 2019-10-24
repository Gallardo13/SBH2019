using System;
using System.ComponentModel.DataAnnotations;

namespace FSO.SDD.DbModel
{
    public class GitPullRequest
    {
        /// <summary>
        /// Идентификатор (автоинкремент)
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Автор
        /// </summary>
        public User Author { get; set; }

        public int AuthorId { get; set; }

        /// <summary>
        /// Ветка, из которой делается пулреквест - через коммиты привязываются все задачи их авторы
        /// </summary>
        public GitBranch Branch { get; set; }

        public int BranchId { get; set; }

        public GitPullRequestState State { get; set; }

        public int StateId { get; set; }

        /// <summary>
        /// Дата создание полреквеста
        /// </summary>
        public DateTime CreatedDateTime { get; set; }


        /// <summary>
        /// Дата, когда пулреквест бы одобрен. Этого может не случиться.
        /// </summary>
        public DateTime? MergedDateTime { get; set; }

    }
}