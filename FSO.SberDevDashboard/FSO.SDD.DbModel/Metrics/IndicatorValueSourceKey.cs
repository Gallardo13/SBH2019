using System.ComponentModel.DataAnnotations;

namespace FSO.SDD.DbModel
{
    /// <summary>
    /// Связка значения индикатора с ключами сущностей, для которых он вычислен
    /// </summary>
    public class IndicatorValueSourceKey
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
        /// ид в таблицее источника
        /// </summary>
        public int SourceItemId { get; set; }
    }
}