﻿using System.ComponentModel.DataAnnotations;

namespace FSO.SDD.DbModel
{
    public class JiraReleaseState
    {
        /// <summary>
        /// Ид 
        /// </summary>
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}