using System.ComponentModel.DataAnnotations;

namespace FSO.SDD.DbModel
{
    public class JiraRelease
    {
        /// <summary>
        /// Ид 
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// КЭ
        /// </summary>
        public string ConfigurationItem { get; set; }
    }
}