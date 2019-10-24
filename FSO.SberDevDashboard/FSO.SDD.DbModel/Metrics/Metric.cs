﻿using System.ComponentModel.DataAnnotations;

namespace FSO.SDD.DbModel
{
    /// <summary>
    /// Описание метрики
    /// </summary>
    /// <remarks>
    /// Метрика - это норматив индикатора
    /// Индикатор - это некая цифра или набор цифр (серии данных)
    /// Значение индикатора вычисляется для набора сущностей-источников
    /// Значение может быть вычислено по одному или нескольким измерениям/разрезам.
    /// У метрики может быть несколько допустимых аггрегатов - сущностей, по которым можно считать значение.
    /// Например, диаграмма сгорания:
    /// - строится по дням (измерение),
    /// - вычисляется актуальный процент выполненных задач и оптимальный процент (серии данных)
    /// - значение вычисляется для релиза, спринта, эпика (сущности) и задачи как независимых ключей, т.е. для каждой сущности вычисляется своё значение.
    /// - аггрегировать можно по релизау, спринту, эпику (таким образом можно по одной метрики нарисовать три диаграммы)
    /// Значение метрики тоже может быть источником данных для других индикаторов.
    /// Например, посчитанное отклонение реального процента исполнения от оптимального, можно вывести как индикатор качества планирования, который считать по спринтам
    /// </remarks>
    public class Metric
    {
        /// <summary>
        /// Идентификатор (автоинкремент)
        /// </summary>
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public Indicator Indicator { get; set; }

        public int IndicatorId { get; set; }

        /// <summary>
        /// значение индикатора за рамками допустимого (больше)
        /// Например, процент выполненных задач в спринте не может быть больше 100
        /// </summary>
        public decimal MaxValue { get; set; }

        /// <summary>
        /// значение индикатора за рамками допустимого (меньшне) 
        /// Например, количество  задач без пулреквеста не может быть меньше 0
        /// </summary>
        public decimal MinValue { get; set; }

        /// <summary>
        /// значение индикатора в норме (нижняя граница)
        /// Например, отклонение общей мкости задач в спринте не ниже 80%
        /// </summary>
        public decimal OptimalValueLow { get; set; }

        /// <summary>
        /// значение индикатора в норме (верхняя граница)
        /// Например, отклонение общей мкости задач в спринте не выше 120%
        /// </summary>
        public decimal OptimalValueHigh { get; set; }

    }
}