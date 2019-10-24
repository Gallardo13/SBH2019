using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FSO.SDD.DbModel
{
    /// <summary>
    /// Измерение индикатора
    /// Например: процент выполненных задач, Количество незакрытых задач, длительность согласования пулреквеста, уровень СО2...
    /// </summary>
    public class IndicatorDimension
    {
        /// <summary>
        /// Идентификатор (автоинкремент)
        /// </summary>
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
