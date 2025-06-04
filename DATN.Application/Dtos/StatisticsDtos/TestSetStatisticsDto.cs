using System;
using System.Collections.Generic;

namespace DATN.Application.Dtos.StatisticsDtos
{
    public class TestSetStatisticsDto
    {
        public int RankQuestionId { get; set; }
        public string RankQuestionName { get; set; }
        public int TotalAttempts { get; set; }
        public int TotalUsers { get; set; }
        public double AverageScore { get; set; }
        public DateTime Date { get; set; }
    }

    public class TestSetStatisticsRequestDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public StatisticsTimeUnit TimeUnit { get; set; }
    }

    public enum StatisticsTimeUnit
    {
        Day,
        Week,
        Month,
        Year
    }
} 