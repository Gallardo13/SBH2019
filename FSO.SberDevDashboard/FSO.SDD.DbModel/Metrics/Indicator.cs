using System.ComponentModel.DataAnnotations;

namespace FSO.SDD.DbModel
{
    /// <summary>
    /// Индикатор (то, что мы умеем считать)
    /// </summary>
    public class Indicator
    {
        /// <summary>
        /// Идентификатор (автоинкремент)
        /// </summary>
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}