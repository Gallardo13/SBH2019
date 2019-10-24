using System.ComponentModel.DataAnnotations;

namespace FSO.SDD.DbModel
{
    /// <summary>
    /// Значение в индикаторе
    /// </summary>
    public class IndicatorValue
    {
        /// <summary>
        /// Идентификатор (автоинкремент)
        /// </summary>
        [Key]
        public int Id { get; set; }


        public Indicator Indicator { get; set; }
        /// <summary>
        /// Индикатор
        /// </summary>
        public int IndicatorId { get; set; }


        public IndicatorDimension Dimension { get; set; }
        /// <summary>
        /// измерение
        /// </summary>
        public int DimensionId { get; set; }

        /// <summary>
        /// Серия значений
        /// </summary>

        public IndicatorValuesSeries IndicatorSeries { get; set; }
        public int IndicatorSeriesId { get; set; }

        /// <summary>
        /// Значение показателя 
        /// </summary>
        public decimal Value { get; set; }
    }
}