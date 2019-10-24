using System;
using System.ComponentModel.DataAnnotations;

namespace FSO.SDD.DbModel
{
    public class GitCommit
    {
        /// <summary>
        /// Идентификатор (автоинкремент)
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Уникальный ключ коммита
        /// </summary>
        public string ComminHash { get; set; }

        public string Comment { get; set; }

        /// <summary>
        /// Ветка в которой сделан коммит
        /// </summary>
        public GitBranch Branch { get; set; }

        public int BranchId { get; set; }

        /// <summary>
        /// Автор коммита
        /// </summary>
        public User Author { get; set; }

        public int AuthorId { get; set; }

        public DateTime CreatedDateTime { get; set; }

    }
}