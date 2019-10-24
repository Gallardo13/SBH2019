using System.ComponentModel.DataAnnotations;

namespace FSO.SDD.DbModel
{
    /// <summary>
    /// Список ключей сущностей для показетеля индикатора
    /// </summary>
    public class IndicatorValueSource
    {
        /// <summary>
        /// Идентификатор (автоинкремент)
        /// </summary>
        [Key]
        public int Id { get; set; }

        public int IndicatorValueId { get; set; }


        public int IndicatorValueSourceKeyId { get; set; }
    }
}