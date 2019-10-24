using System.ComponentModel.DataAnnotations;

namespace FSO.SDD.DbModel
{
    /// <summary>
    /// Список сущностей, по которым можно агрегировать индикатор
    /// </summary>
    public class IndicatorSourceAggregate
    {
        /// <summary>
        /// Идентификатор (автоинкремент)
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Тип источника данных (таблица)
        /// </summary>
        public string SourceType { get; set; }

        /// <summary>
        /// ид индикатора
        /// </summary>
        public int IndicatorId { get; set; }
    }
}