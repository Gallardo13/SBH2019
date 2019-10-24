using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FSO.SDD.DbModel
{
    public class GitBranch
    {
        /// <summary>
        /// Идентификатор (автоинкремент)
        /// </summary>
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// Таска для которой ветка была создана
        /// </summary>
        public string JiraTaskKey { get; set; }
    }
}
