using System.ComponentModel.DataAnnotations;

namespace FSO.SDD.DbModel
{
    /// <summary>
    /// Серия значений индикатора
    /// </summary>
    public class IndicatorValuesSeries
    {
        /// <summary>
        /// Идентификатор (автоинкремент)
        /// </summary>
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}