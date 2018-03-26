using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WorkoutPlanner.Data.Migrations
{
    public partial class TrackCreatorIsNowInt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tracks_Users_CreatorId",
                table: "Tracks");

            migrationBuilder.DropIndex(
                name: "IX_Tracks_CreatorId",
                table: "Tracks");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Tracks");

            migrationBuilder.AddColumn<int>(
                name: "Creator",
                table: "Tracks",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Creator",
                table: "Tracks");

            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "Tracks",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tracks_CreatorId",
                table: "Tracks",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tracks_Users_CreatorId",
                table: "Tracks",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
