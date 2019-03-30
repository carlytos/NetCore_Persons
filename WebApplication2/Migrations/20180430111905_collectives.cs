using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FinalProyect.Migrations
{
    public partial class collectives : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Collectives_CollectiveId",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_CollectiveId",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "CollectiveId",
                table: "Persons");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CollectiveId",
                table: "Persons",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persons_CollectiveId",
                table: "Persons",
                column: "CollectiveId");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Collectives_CollectiveId",
                table: "Persons",
                column: "CollectiveId",
                principalTable: "Collectives",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
