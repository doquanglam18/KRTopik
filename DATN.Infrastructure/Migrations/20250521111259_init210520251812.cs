using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DATN.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init210520251812 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 759, DateTimeKind.Local).AddTicks(2609));

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 759, DateTimeKind.Local).AddTicks(2623));

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 759, DateTimeKind.Local).AddTicks(2626));

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 759, DateTimeKind.Local).AddTicks(2634));

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 759, DateTimeKind.Local).AddTicks(2636));

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 759, DateTimeKind.Local).AddTicks(2625));

            migrationBuilder.UpdateData(
                table: "KoreaBlog",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 759, DateTimeKind.Local).AddTicks(5461));

            migrationBuilder.UpdateData(
                table: "KoreaBlog",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 759, DateTimeKind.Local).AddTicks(5471));

            migrationBuilder.UpdateData(
                table: "KoreaBlog",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 759, DateTimeKind.Local).AddTicks(5474));

            migrationBuilder.UpdateData(
                table: "KoreaBlog",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 759, DateTimeKind.Local).AddTicks(5476));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 760, DateTimeKind.Local).AddTicks(5862));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 760, DateTimeKind.Local).AddTicks(5869));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 760, DateTimeKind.Local).AddTicks(5870));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 760, DateTimeKind.Local).AddTicks(5871));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 760, DateTimeKind.Local).AddTicks(5872));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 760, DateTimeKind.Local).AddTicks(5874));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 760, DateTimeKind.Local).AddTicks(5875));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 760, DateTimeKind.Local).AddTicks(5881));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 760, DateTimeKind.Local).AddTicks(5883));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 760, DateTimeKind.Local).AddTicks(5884));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 760, DateTimeKind.Local).AddTicks(5885));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 760, DateTimeKind.Local).AddTicks(5886));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 760, DateTimeKind.Local).AddTicks(5888));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 760, DateTimeKind.Local).AddTicks(5889));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 760, DateTimeKind.Local).AddTicks(5890));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 760, DateTimeKind.Local).AddTicks(5891));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 760, DateTimeKind.Local).AddTicks(5892));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 760, DateTimeKind.Local).AddTicks(5894));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 760, DateTimeKind.Local).AddTicks(5895));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 760, DateTimeKind.Local).AddTicks(5898));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 760, DateTimeKind.Local).AddTicks(5911));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 760, DateTimeKind.Local).AddTicks(5913));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 760, DateTimeKind.Local).AddTicks(5914));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 760, DateTimeKind.Local).AddTicks(5916));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 25,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 760, DateTimeKind.Local).AddTicks(5917));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 26,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 760, DateTimeKind.Local).AddTicks(5918));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 27,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 760, DateTimeKind.Local).AddTicks(5919));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 28,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 760, DateTimeKind.Local).AddTicks(5920));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 29,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 760, DateTimeKind.Local).AddTicks(5922));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 30,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 760, DateTimeKind.Local).AddTicks(5923));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 31,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 760, DateTimeKind.Local).AddTicks(5924));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 32,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 760, DateTimeKind.Local).AddTicks(5925));

            migrationBuilder.UpdateData(
                table: "ListeningQuestion",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 11, 12, 57, 760, DateTimeKind.Utc).AddTicks(3511));

            migrationBuilder.UpdateData(
                table: "ListeningQuestion",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 11, 12, 57, 760, DateTimeKind.Utc).AddTicks(3519));

            migrationBuilder.UpdateData(
                table: "ListeningQuestion",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 11, 12, 57, 760, DateTimeKind.Utc).AddTicks(3521));

            migrationBuilder.UpdateData(
                table: "ListeningQuestion",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 11, 12, 57, 760, DateTimeKind.Utc).AddTicks(3524));

            migrationBuilder.UpdateData(
                table: "ListeningQuestion",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 11, 12, 57, 760, DateTimeKind.Utc).AddTicks(3526));

            migrationBuilder.UpdateData(
                table: "ListeningQuestion",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 11, 12, 57, 760, DateTimeKind.Utc).AddTicks(3528));

            migrationBuilder.UpdateData(
                table: "ListeningQuestion",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 11, 12, 57, 760, DateTimeKind.Utc).AddTicks(3530));

            migrationBuilder.UpdateData(
                table: "ListeningQuestion",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 11, 12, 57, 760, DateTimeKind.Utc).AddTicks(3532));

            migrationBuilder.UpdateData(
                table: "RankQuestion",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 11, 12, 57, 759, DateTimeKind.Utc).AddTicks(9252));

            migrationBuilder.UpdateData(
                table: "RankQuestion",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 11, 12, 57, 759, DateTimeKind.Utc).AddTicks(9252));

            migrationBuilder.UpdateData(
                table: "RankQuestion",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 11, 12, 57, 759, DateTimeKind.Utc).AddTicks(9252));

            migrationBuilder.UpdateData(
                table: "RankQuestion",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 11, 12, 57, 759, DateTimeKind.Utc).AddTicks(9252));

            migrationBuilder.UpdateData(
                table: "RankQuestion",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 11, 12, 57, 759, DateTimeKind.Utc).AddTicks(9252));

            migrationBuilder.UpdateData(
                table: "RankQuestion",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 11, 12, 57, 759, DateTimeKind.Utc).AddTicks(9252));

            migrationBuilder.UpdateData(
                table: "RankQuestion",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 11, 12, 57, 759, DateTimeKind.Utc).AddTicks(9252));

            migrationBuilder.UpdateData(
                table: "RankQuestion",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 11, 12, 57, 759, DateTimeKind.Utc).AddTicks(9252));

            migrationBuilder.UpdateData(
                table: "RankQuestion",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 11, 12, 57, 759, DateTimeKind.Utc).AddTicks(9252));

            migrationBuilder.UpdateData(
                table: "RankQuestion",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 11, 12, 57, 759, DateTimeKind.Utc).AddTicks(9252));

            migrationBuilder.UpdateData(
                table: "RankQuestion",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 11, 12, 57, 759, DateTimeKind.Utc).AddTicks(9252));

            migrationBuilder.UpdateData(
                table: "RankQuestion",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 11, 12, 57, 759, DateTimeKind.Utc).AddTicks(9252));

            migrationBuilder.UpdateData(
                table: "RankQuestion",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 11, 12, 57, 759, DateTimeKind.Utc).AddTicks(9252));

            migrationBuilder.UpdateData(
                table: "RankQuestion",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 11, 12, 57, 759, DateTimeKind.Utc).AddTicks(9252));

            migrationBuilder.UpdateData(
                table: "RankQuestion",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 11, 12, 57, 759, DateTimeKind.Utc).AddTicks(9252));

            migrationBuilder.UpdateData(
                table: "RankQuestion",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 11, 12, 57, 759, DateTimeKind.Utc).AddTicks(9252));

            migrationBuilder.UpdateData(
                table: "RankQuestion",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 11, 12, 57, 759, DateTimeKind.Utc).AddTicks(9252));

            migrationBuilder.UpdateData(
                table: "RankQuestion",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 11, 12, 57, 759, DateTimeKind.Utc).AddTicks(9252));

            migrationBuilder.UpdateData(
                table: "RankQuestion",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 11, 12, 57, 759, DateTimeKind.Utc).AddTicks(9252));

            migrationBuilder.UpdateData(
                table: "RankQuestion",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 11, 12, 57, 759, DateTimeKind.Utc).AddTicks(9252));

            migrationBuilder.UpdateData(
                table: "RankQuestion",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 11, 12, 57, 759, DateTimeKind.Utc).AddTicks(9252));

            migrationBuilder.UpdateData(
                table: "RatingBlog",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 761, DateTimeKind.Local).AddTicks(3984));

            migrationBuilder.UpdateData(
                table: "RatingBlog",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 761, DateTimeKind.Local).AddTicks(3993));

            migrationBuilder.UpdateData(
                table: "RatingBlog",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 761, DateTimeKind.Local).AddTicks(3995));

            migrationBuilder.UpdateData(
                table: "RatingBlog",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 761, DateTimeKind.Local).AddTicks(3996));

            migrationBuilder.UpdateData(
                table: "RatingBlog",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 761, DateTimeKind.Local).AddTicks(3999));

            migrationBuilder.UpdateData(
                table: "RatingBlog",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 761, DateTimeKind.Local).AddTicks(4000));

            migrationBuilder.UpdateData(
                table: "RatingBlog",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 761, DateTimeKind.Local).AddTicks(4002));

            migrationBuilder.UpdateData(
                table: "RatingBlog",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 761, DateTimeKind.Local).AddTicks(4004));

            migrationBuilder.UpdateData(
                table: "ReadingAnswer",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 762, DateTimeKind.Local).AddTicks(2899));

            migrationBuilder.UpdateData(
                table: "ReadingAnswer",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 762, DateTimeKind.Local).AddTicks(2910));

            migrationBuilder.UpdateData(
                table: "ReadingAnswer",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 762, DateTimeKind.Local).AddTicks(2911));

            migrationBuilder.UpdateData(
                table: "ReadingAnswer",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 762, DateTimeKind.Local).AddTicks(2912));

            migrationBuilder.UpdateData(
                table: "ReadingAnswer",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 762, DateTimeKind.Local).AddTicks(2914));

            migrationBuilder.UpdateData(
                table: "ReadingAnswer",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 762, DateTimeKind.Local).AddTicks(2915));

            migrationBuilder.UpdateData(
                table: "ReadingAnswer",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 762, DateTimeKind.Local).AddTicks(2916));

            migrationBuilder.UpdateData(
                table: "ReadingAnswer",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 762, DateTimeKind.Local).AddTicks(2917));

            migrationBuilder.UpdateData(
                table: "ReadingAnswer",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 762, DateTimeKind.Local).AddTicks(2919));

            migrationBuilder.UpdateData(
                table: "ReadingAnswer",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 762, DateTimeKind.Local).AddTicks(2920));

            migrationBuilder.UpdateData(
                table: "ReadingAnswer",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 762, DateTimeKind.Local).AddTicks(2921));

            migrationBuilder.UpdateData(
                table: "ReadingAnswer",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 762, DateTimeKind.Local).AddTicks(2922));

            migrationBuilder.UpdateData(
                table: "ReadingAnswer",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 762, DateTimeKind.Local).AddTicks(2923));

            migrationBuilder.UpdateData(
                table: "ReadingAnswer",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 762, DateTimeKind.Local).AddTicks(2925));

            migrationBuilder.UpdateData(
                table: "ReadingAnswer",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 762, DateTimeKind.Local).AddTicks(2926));

            migrationBuilder.UpdateData(
                table: "ReadingAnswer",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 18, 12, 57, 762, DateTimeKind.Local).AddTicks(2927));

            migrationBuilder.UpdateData(
                table: "ReadingQuestion",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 5, 21, 11, 12, 57, 761, DateTimeKind.Utc).AddTicks(9813), new DateTime(2025, 5, 21, 11, 12, 57, 761, DateTimeKind.Utc).AddTicks(9814) });

            migrationBuilder.UpdateData(
                table: "ReadingQuestion",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 5, 21, 11, 12, 57, 761, DateTimeKind.Utc).AddTicks(9818), new DateTime(2025, 5, 21, 11, 12, 57, 761, DateTimeKind.Utc).AddTicks(9818) });

            migrationBuilder.UpdateData(
                table: "ReadingQuestion",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 5, 21, 11, 12, 57, 761, DateTimeKind.Utc).AddTicks(9836), new DateTime(2025, 5, 21, 11, 12, 57, 761, DateTimeKind.Utc).AddTicks(9836) });

            migrationBuilder.UpdateData(
                table: "ReadingQuestion",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 5, 21, 11, 12, 57, 761, DateTimeKind.Utc).AddTicks(9842), new DateTime(2025, 5, 21, 11, 12, 57, 761, DateTimeKind.Utc).AddTicks(9843) });

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 5, 21, 11, 12, 57, 762, DateTimeKind.Utc).AddTicks(4276), new DateTime(2025, 5, 21, 11, 12, 57, 762, DateTimeKind.Utc).AddTicks(4278) });

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 5, 21, 11, 12, 57, 762, DateTimeKind.Utc).AddTicks(4281), new DateTime(2025, 5, 21, 11, 12, 57, 762, DateTimeKind.Utc).AddTicks(4281) });

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 5, 21, 11, 12, 57, 762, DateTimeKind.Utc).AddTicks(4282), new DateTime(2025, 5, 21, 11, 12, 57, 762, DateTimeKind.Utc).AddTicks(4283) });

            migrationBuilder.InsertData(
                table: "SystemLogging",
                columns: new[] { "Id", "ActionName", "CreatedDate", "Details", "IPAddress", "UpdatedDate", "UserId" },
                values: new object[,]
                {
                    { 1, "Logout", new DateTime(2025, 5, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), "Người dùng đã đăng xuất khỏi hệ thống", "::1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ea81763f-6534-448e-aa30-4112123493fb") },
                    { 2, "Logout", new DateTime(2025, 5, 1, 13, 0, 0, 0, DateTimeKind.Unspecified), "Người dùng đã đăng xuất khỏi hệ thống", "::1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("5c0c563b-80d4-4485-9854-f6af58422601") },
                    { 3, "Login - Failed", new DateTime(2025, 5, 2, 11, 0, 0, 0, DateTimeKind.Unspecified), "Email: dolam180903@gmail.com - Lý do: Tài khoản hoặc mật khẩu không chính xác !", "::1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 4, "Login - Failed", new DateTime(2025, 5, 3, 12, 0, 0, 0, DateTimeKind.Unspecified), "Email: admin@gmail.com - Lý do: Tài khoản hoặc mật khẩu không chính xác !", "::1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 5, "Login - Success", new DateTime(2025, 5, 4, 13, 0, 0, 0, DateTimeKind.Unspecified), "User Đỗ Quang Lâm (dolam180903@gmail.com) đã đăng nhập thành công.", "::1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("5c0c563b-80d4-4485-9854-f6af58422601") },
                    { 6, "Login - Success", new DateTime(2025, 5, 5, 14, 0, 0, 0, DateTimeKind.Unspecified), "User System Admin (admin@gmail.com) đã đăng nhập thành công.", "::1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("33333333-3333-3333-3333-333333333333") },
                    { 7, "Logout", new DateTime(2025, 5, 6, 15, 0, 0, 0, DateTimeKind.Unspecified), "Người dùng đã đăng xuất khỏi hệ thống", "::1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ea81763f-6534-448e-aa30-4112123493fb") },
                    { 8, "Login - Failed", new DateTime(2025, 5, 7, 9, 0, 0, 0, DateTimeKind.Unspecified), "Email: admin@gmail.com - Lý do: Tài khoản hoặc mật khẩu không chính xác !", "::1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 9, "Login - Success", new DateTime(2025, 5, 8, 10, 30, 0, 0, DateTimeKind.Unspecified), "User System Admin (admin@gmail.com) đã đăng nhập thành công.", "::1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("33333333-3333-3333-3333-333333333333") },
                    { 10, "Login - Success", new DateTime(2025, 5, 9, 16, 0, 0, 0, DateTimeKind.Unspecified), "User Trần Thị B (b@gmail.com) đã đăng nhập thành công.", "::1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ea81763f-6534-448e-aa30-4112123493fb") },
                    { 11, "Logout", new DateTime(2025, 5, 10, 11, 0, 0, 0, DateTimeKind.Unspecified), "Người dùng đã đăng xuất khỏi hệ thống", "::1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("5c0c563b-80d4-4485-9854-f6af58422601") },
                    { 12, "Login - Success", new DateTime(2025, 5, 11, 13, 0, 0, 0, DateTimeKind.Unspecified), "User Trần Thị B (b@gmail.com) đã đăng nhập thành công.", "::1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ea81763f-6534-448e-aa30-4112123493fb") },
                    { 13, "Login - Failed", new DateTime(2025, 5, 12, 17, 0, 0, 0, DateTimeKind.Unspecified), "Email: a@example.com - Lý do: Tài khoản hoặc mật khẩu không chính xác !", "::1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 14, "Logout", new DateTime(2025, 5, 13, 14, 0, 0, 0, DateTimeKind.Unspecified), "Người dùng đã đăng xuất khỏi hệ thống", "::1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("33333333-3333-3333-3333-333333333333") },
                    { 15, "Login - Success", new DateTime(2025, 5, 14, 13, 0, 0, 0, DateTimeKind.Unspecified), "User Trần Thị B (b@gmail.com) đã đăng nhập thành công.", "::1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ea81763f-6534-448e-aa30-4112123493fb") },
                    { 16, "Login - Success", new DateTime(2025, 5, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), "User Đỗ Quang Lâm (dolam180903@gmail.com) đã đăng nhập thành công.", "::1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("5c0c563b-80d4-4485-9854-f6af58422601") },
                    { 17, "Login - Failed", new DateTime(2025, 5, 16, 8, 0, 0, 0, DateTimeKind.Unspecified), "Email: dolam180903@gmail.com - Lý do: Tài khoản hoặc mật khẩu không chính xác !", "::1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 18, "Login - Success", new DateTime(2025, 5, 17, 15, 0, 0, 0, DateTimeKind.Unspecified), "User System Admin (admin@gmail.com) đã đăng nhập thành công.", "::1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("33333333-3333-3333-3333-333333333333") },
                    { 19, "Logout", new DateTime(2025, 5, 18, 16, 0, 0, 0, DateTimeKind.Unspecified), "Người dùng đã đăng xuất khỏi hệ thống", "::1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("5c0c563b-80d4-4485-9854-f6af58422601") },
                    { 20, "Login - Failed", new DateTime(2025, 5, 19, 10, 0, 0, 0, DateTimeKind.Unspecified), "Email: admin@gmail.com - Lý do: Tài khoản hoặc mật khẩu không chính xác !", "::1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 21, "Login - Success", new DateTime(2025, 5, 20, 14, 0, 0, 0, DateTimeKind.Unspecified), "User Trần Thị B (b@gmail.com) đã đăng nhập thành công.", "::1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ea81763f-6534-448e-aa30-4112123493fb") },
                    { 22, "Logout", new DateTime(2025, 5, 21, 12, 0, 0, 0, DateTimeKind.Unspecified), "Người dùng đã đăng xuất khỏi hệ thống", "::1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("33333333-3333-3333-3333-333333333333") },
                    { 23, "Login - Success", new DateTime(2025, 5, 22, 8, 0, 0, 0, DateTimeKind.Unspecified), "User Đỗ Quang Lâm (dolam180903@gmail.com) đã đăng nhập thành công.", "::1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("5c0c563b-80d4-4485-9854-f6af58422601") },
                    { 24, "Logout", new DateTime(2025, 5, 23, 10, 0, 0, 0, DateTimeKind.Unspecified), "Người dùng đã đăng xuất khỏi hệ thống", "::1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ea81763f-6534-448e-aa30-4112123493fb") },
                    { 25, "Login - Failed", new DateTime(2025, 5, 24, 11, 0, 0, 0, DateTimeKind.Unspecified), "Email: a@example.com - Lý do: Tài khoản hoặc mật khẩu không chính xác !", "::1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 26, "Login - Success", new DateTime(2025, 5, 25, 14, 0, 0, 0, DateTimeKind.Unspecified), "User Đỗ Quang Lâm (dolam180903@gmail.com) đã đăng nhập thành công.", "::1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("5c0c563b-80d4-4485-9854-f6af58422601") },
                    { 27, "Login - Success", new DateTime(2025, 5, 26, 10, 0, 0, 0, DateTimeKind.Unspecified), "User System Admin (admin@gmail.com) đã đăng nhập thành công.", "::1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("33333333-3333-3333-3333-333333333333") },
                    { 28, "Logout", new DateTime(2025, 5, 27, 9, 0, 0, 0, DateTimeKind.Unspecified), "Người dùng đã đăng xuất khỏi hệ thống", "::1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ea81763f-6534-448e-aa30-4112123493fb") },
                    { 29, "Login - Failed", new DateTime(2025, 5, 28, 15, 0, 0, 0, DateTimeKind.Unspecified), "Email: admin@gmail.com - Lý do: Tài khoản hoặc mật khẩu không chính xác !", "::1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 30, "Login - Success", new DateTime(2025, 5, 29, 16, 0, 0, 0, DateTimeKind.Unspecified), "User System Admin (admin@gmail.com) đã đăng nhập thành công.", "::1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("33333333-3333-3333-3333-333333333333") }
                });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "PasswordHash",
                value: "$2a$11$kVid0ZbhlqDB8rJn5FlI.OkTVItVIHzQ5Kh6DSlKaUmVj91zoCf2q");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("5c0c563b-80d4-4485-9854-f6af58422601"),
                column: "PasswordHash",
                value: "$2a$11$b8ABbcJePe0Wbh8ulRaYGOzTmPBh8s6nYtRBJ4PVSEnrLZQ3TNzei");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("ea81763f-6534-448e-aa30-4112123493fb"),
                column: "PasswordHash",
                value: "$2a$11$b8ABbcJePe0Wbh8ulRaYGOzTmPBh8s6nYtRBJ4PVSEnrLZQ3TNzei");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SystemLogging",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SystemLogging",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SystemLogging",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SystemLogging",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "SystemLogging",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "SystemLogging",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "SystemLogging",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "SystemLogging",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "SystemLogging",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "SystemLogging",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "SystemLogging",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "SystemLogging",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "SystemLogging",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "SystemLogging",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "SystemLogging",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "SystemLogging",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "SystemLogging",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "SystemLogging",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "SystemLogging",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "SystemLogging",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "SystemLogging",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "SystemLogging",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "SystemLogging",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "SystemLogging",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "SystemLogging",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "SystemLogging",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "SystemLogging",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "SystemLogging",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "SystemLogging",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "SystemLogging",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 406, DateTimeKind.Local).AddTicks(1162));

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 406, DateTimeKind.Local).AddTicks(1174));

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 406, DateTimeKind.Local).AddTicks(1177));

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 406, DateTimeKind.Local).AddTicks(1179));

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 406, DateTimeKind.Local).AddTicks(1181));

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 406, DateTimeKind.Local).AddTicks(1176));

            migrationBuilder.UpdateData(
                table: "KoreaBlog",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 406, DateTimeKind.Local).AddTicks(4133));

            migrationBuilder.UpdateData(
                table: "KoreaBlog",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 406, DateTimeKind.Local).AddTicks(4143));

            migrationBuilder.UpdateData(
                table: "KoreaBlog",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 406, DateTimeKind.Local).AddTicks(4146));

            migrationBuilder.UpdateData(
                table: "KoreaBlog",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 406, DateTimeKind.Local).AddTicks(4148));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 407, DateTimeKind.Local).AddTicks(5842));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 407, DateTimeKind.Local).AddTicks(5851));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 407, DateTimeKind.Local).AddTicks(5852));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 407, DateTimeKind.Local).AddTicks(5853));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 407, DateTimeKind.Local).AddTicks(5855));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 407, DateTimeKind.Local).AddTicks(5856));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 407, DateTimeKind.Local).AddTicks(5857));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 407, DateTimeKind.Local).AddTicks(5863));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 407, DateTimeKind.Local).AddTicks(5864));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 407, DateTimeKind.Local).AddTicks(5865));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 407, DateTimeKind.Local).AddTicks(5867));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 407, DateTimeKind.Local).AddTicks(5868));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 407, DateTimeKind.Local).AddTicks(5869));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 407, DateTimeKind.Local).AddTicks(5870));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 407, DateTimeKind.Local).AddTicks(5872));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 407, DateTimeKind.Local).AddTicks(5873));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 407, DateTimeKind.Local).AddTicks(5874));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 407, DateTimeKind.Local).AddTicks(5875));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 407, DateTimeKind.Local).AddTicks(5876));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 407, DateTimeKind.Local).AddTicks(5881));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 407, DateTimeKind.Local).AddTicks(5897));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 407, DateTimeKind.Local).AddTicks(5899));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 407, DateTimeKind.Local).AddTicks(5900));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 407, DateTimeKind.Local).AddTicks(5901));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 25,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 407, DateTimeKind.Local).AddTicks(5902));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 26,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 407, DateTimeKind.Local).AddTicks(5904));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 27,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 407, DateTimeKind.Local).AddTicks(5905));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 28,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 407, DateTimeKind.Local).AddTicks(5974));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 29,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 407, DateTimeKind.Local).AddTicks(5975));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 30,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 407, DateTimeKind.Local).AddTicks(5976));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 31,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 407, DateTimeKind.Local).AddTicks(5977));

            migrationBuilder.UpdateData(
                table: "ListeningAnswer",
                keyColumn: "Id",
                keyValue: 32,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 407, DateTimeKind.Local).AddTicks(5979));

            migrationBuilder.UpdateData(
                table: "ListeningQuestion",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 2, 32, 56, 407, DateTimeKind.Utc).AddTicks(3030));

            migrationBuilder.UpdateData(
                table: "ListeningQuestion",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 2, 32, 56, 407, DateTimeKind.Utc).AddTicks(3046));

            migrationBuilder.UpdateData(
                table: "ListeningQuestion",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 2, 32, 56, 407, DateTimeKind.Utc).AddTicks(3048));

            migrationBuilder.UpdateData(
                table: "ListeningQuestion",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 2, 32, 56, 407, DateTimeKind.Utc).AddTicks(3051));

            migrationBuilder.UpdateData(
                table: "ListeningQuestion",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 2, 32, 56, 407, DateTimeKind.Utc).AddTicks(3053));

            migrationBuilder.UpdateData(
                table: "ListeningQuestion",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 2, 32, 56, 407, DateTimeKind.Utc).AddTicks(3055));

            migrationBuilder.UpdateData(
                table: "ListeningQuestion",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 2, 32, 56, 407, DateTimeKind.Utc).AddTicks(3057));

            migrationBuilder.UpdateData(
                table: "ListeningQuestion",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 2, 32, 56, 407, DateTimeKind.Utc).AddTicks(3060));

            migrationBuilder.UpdateData(
                table: "RankQuestion",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 2, 32, 56, 406, DateTimeKind.Utc).AddTicks(8412));

            migrationBuilder.UpdateData(
                table: "RankQuestion",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 2, 32, 56, 406, DateTimeKind.Utc).AddTicks(8412));

            migrationBuilder.UpdateData(
                table: "RankQuestion",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 2, 32, 56, 406, DateTimeKind.Utc).AddTicks(8412));

            migrationBuilder.UpdateData(
                table: "RankQuestion",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 2, 32, 56, 406, DateTimeKind.Utc).AddTicks(8412));

            migrationBuilder.UpdateData(
                table: "RankQuestion",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 2, 32, 56, 406, DateTimeKind.Utc).AddTicks(8412));

            migrationBuilder.UpdateData(
                table: "RankQuestion",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 2, 32, 56, 406, DateTimeKind.Utc).AddTicks(8412));

            migrationBuilder.UpdateData(
                table: "RankQuestion",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 2, 32, 56, 406, DateTimeKind.Utc).AddTicks(8412));

            migrationBuilder.UpdateData(
                table: "RankQuestion",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 2, 32, 56, 406, DateTimeKind.Utc).AddTicks(8412));

            migrationBuilder.UpdateData(
                table: "RankQuestion",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 2, 32, 56, 406, DateTimeKind.Utc).AddTicks(8412));

            migrationBuilder.UpdateData(
                table: "RankQuestion",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 2, 32, 56, 406, DateTimeKind.Utc).AddTicks(8412));

            migrationBuilder.UpdateData(
                table: "RankQuestion",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 2, 32, 56, 406, DateTimeKind.Utc).AddTicks(8412));

            migrationBuilder.UpdateData(
                table: "RankQuestion",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 2, 32, 56, 406, DateTimeKind.Utc).AddTicks(8412));

            migrationBuilder.UpdateData(
                table: "RankQuestion",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 2, 32, 56, 406, DateTimeKind.Utc).AddTicks(8412));

            migrationBuilder.UpdateData(
                table: "RankQuestion",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 2, 32, 56, 406, DateTimeKind.Utc).AddTicks(8412));

            migrationBuilder.UpdateData(
                table: "RankQuestion",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 2, 32, 56, 406, DateTimeKind.Utc).AddTicks(8412));

            migrationBuilder.UpdateData(
                table: "RankQuestion",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 2, 32, 56, 406, DateTimeKind.Utc).AddTicks(8412));

            migrationBuilder.UpdateData(
                table: "RankQuestion",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 2, 32, 56, 406, DateTimeKind.Utc).AddTicks(8412));

            migrationBuilder.UpdateData(
                table: "RankQuestion",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 2, 32, 56, 406, DateTimeKind.Utc).AddTicks(8412));

            migrationBuilder.UpdateData(
                table: "RankQuestion",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 2, 32, 56, 406, DateTimeKind.Utc).AddTicks(8412));

            migrationBuilder.UpdateData(
                table: "RankQuestion",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 2, 32, 56, 406, DateTimeKind.Utc).AddTicks(8412));

            migrationBuilder.UpdateData(
                table: "RankQuestion",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 2, 32, 56, 406, DateTimeKind.Utc).AddTicks(8412));

            migrationBuilder.UpdateData(
                table: "RatingBlog",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 408, DateTimeKind.Local).AddTicks(4311));

            migrationBuilder.UpdateData(
                table: "RatingBlog",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 408, DateTimeKind.Local).AddTicks(4319));

            migrationBuilder.UpdateData(
                table: "RatingBlog",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 408, DateTimeKind.Local).AddTicks(4321));

            migrationBuilder.UpdateData(
                table: "RatingBlog",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 408, DateTimeKind.Local).AddTicks(4322));

            migrationBuilder.UpdateData(
                table: "RatingBlog",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 408, DateTimeKind.Local).AddTicks(4324));

            migrationBuilder.UpdateData(
                table: "RatingBlog",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 408, DateTimeKind.Local).AddTicks(4326));

            migrationBuilder.UpdateData(
                table: "RatingBlog",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 408, DateTimeKind.Local).AddTicks(4328));

            migrationBuilder.UpdateData(
                table: "RatingBlog",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 408, DateTimeKind.Local).AddTicks(4330));

            migrationBuilder.UpdateData(
                table: "ReadingAnswer",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 409, DateTimeKind.Local).AddTicks(1691));

            migrationBuilder.UpdateData(
                table: "ReadingAnswer",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 409, DateTimeKind.Local).AddTicks(1696));

            migrationBuilder.UpdateData(
                table: "ReadingAnswer",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 409, DateTimeKind.Local).AddTicks(1698));

            migrationBuilder.UpdateData(
                table: "ReadingAnswer",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 409, DateTimeKind.Local).AddTicks(1699));

            migrationBuilder.UpdateData(
                table: "ReadingAnswer",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 409, DateTimeKind.Local).AddTicks(1700));

            migrationBuilder.UpdateData(
                table: "ReadingAnswer",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 409, DateTimeKind.Local).AddTicks(1701));

            migrationBuilder.UpdateData(
                table: "ReadingAnswer",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 409, DateTimeKind.Local).AddTicks(1702));

            migrationBuilder.UpdateData(
                table: "ReadingAnswer",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 409, DateTimeKind.Local).AddTicks(1704));

            migrationBuilder.UpdateData(
                table: "ReadingAnswer",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 409, DateTimeKind.Local).AddTicks(1705));

            migrationBuilder.UpdateData(
                table: "ReadingAnswer",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 409, DateTimeKind.Local).AddTicks(1706));

            migrationBuilder.UpdateData(
                table: "ReadingAnswer",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 409, DateTimeKind.Local).AddTicks(1707));

            migrationBuilder.UpdateData(
                table: "ReadingAnswer",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 409, DateTimeKind.Local).AddTicks(1708));

            migrationBuilder.UpdateData(
                table: "ReadingAnswer",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 409, DateTimeKind.Local).AddTicks(1710));

            migrationBuilder.UpdateData(
                table: "ReadingAnswer",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 409, DateTimeKind.Local).AddTicks(1711));

            migrationBuilder.UpdateData(
                table: "ReadingAnswer",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 409, DateTimeKind.Local).AddTicks(1712));

            migrationBuilder.UpdateData(
                table: "ReadingAnswer",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 21, 9, 32, 56, 409, DateTimeKind.Local).AddTicks(1713));

            migrationBuilder.UpdateData(
                table: "ReadingQuestion",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 5, 21, 2, 32, 56, 408, DateTimeKind.Utc).AddTicks(8984), new DateTime(2025, 5, 21, 2, 32, 56, 408, DateTimeKind.Utc).AddTicks(8984) });

            migrationBuilder.UpdateData(
                table: "ReadingQuestion",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 5, 21, 2, 32, 56, 408, DateTimeKind.Utc).AddTicks(8997), new DateTime(2025, 5, 21, 2, 32, 56, 408, DateTimeKind.Utc).AddTicks(8997) });

            migrationBuilder.UpdateData(
                table: "ReadingQuestion",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 5, 21, 2, 32, 56, 408, DateTimeKind.Utc).AddTicks(9000), new DateTime(2025, 5, 21, 2, 32, 56, 408, DateTimeKind.Utc).AddTicks(9001) });

            migrationBuilder.UpdateData(
                table: "ReadingQuestion",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 5, 21, 2, 32, 56, 408, DateTimeKind.Utc).AddTicks(9003), new DateTime(2025, 5, 21, 2, 32, 56, 408, DateTimeKind.Utc).AddTicks(9004) });

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 5, 21, 2, 32, 56, 409, DateTimeKind.Utc).AddTicks(2755), new DateTime(2025, 5, 21, 2, 32, 56, 409, DateTimeKind.Utc).AddTicks(2755) });

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 5, 21, 2, 32, 56, 409, DateTimeKind.Utc).AddTicks(2757), new DateTime(2025, 5, 21, 2, 32, 56, 409, DateTimeKind.Utc).AddTicks(2758) });

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 5, 21, 2, 32, 56, 409, DateTimeKind.Utc).AddTicks(2759), new DateTime(2025, 5, 21, 2, 32, 56, 409, DateTimeKind.Utc).AddTicks(2759) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "PasswordHash",
                value: "$2a$11$a2uE8IplQPkm3uww43PZOu7l2LmL6fwx9W7z30ikerkvK0sB1Cxya");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("5c0c563b-80d4-4485-9854-f6af58422601"),
                column: "PasswordHash",
                value: "$2a$11$caghMadkRv2IDoauKNSfSOUPcNKtj79nAmtzMIA21ymo1lpnbDZna");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("ea81763f-6534-448e-aa30-4112123493fb"),
                column: "PasswordHash",
                value: "$2a$11$caghMadkRv2IDoauKNSfSOUPcNKtj79nAmtzMIA21ymo1lpnbDZna");
        }
    }
}
