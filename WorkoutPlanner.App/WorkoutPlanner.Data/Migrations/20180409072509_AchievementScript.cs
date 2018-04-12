using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WorkoutPlanner.Data.Migrations
{
    public partial class AchievementScript : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"CREATE PROCEDURE LoadAchievementDefaults
                AS
                INSERT INTO Achievements (Description, Type, Value)
                VALUES 
                     ('5k Achievement', 'Accomplishment', 5),
                     ('10k Achievement', 'Accomplishment', 10),
                     ('15k Achievement', 'Accomplishment', 15),
                     ('50k total', 'Distance', 50),
                     ('100k total', 'Distance', 100),
                     ('200k total', 'Distance', 200),
                     ('400k total', 'Distance', 400),
                     ('600k total', 'Distance', 600),
                     ('800k total', 'Distance', 800),
                     ('1000k total', 'Distance', 1000),
                     ('10h total', 'Duration', 10),
                     ('20h total', 'Duration', 20),
                     ('30h total', 'Duration', 30),
                     ('40h total', 'Duration', 40),
                     ('50h total', 'Duration', 50);"
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE LoadAchievementDefaults");
        }
    }
}
