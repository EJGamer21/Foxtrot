using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Foxtrot.Migrations
{
    public partial class CreatedAppointmentStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Appointments",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppointmentStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentStatus", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_StatusId",
                table: "Appointments",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AppointmentStatus_StatusId",
                table: "Appointments",
                column: "StatusId",
                principalTable: "AppointmentStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            
            string emptyGuid = "00000000-0000-0000-0000-000000000000";
            DateTime now = DateTime.Now;

            migrationBuilder.InsertData(
                table: "AppointmentStatus",
                columns: new[]
                {
                    "Name", "CreatedDate", "CreatedBy", "UpdatedDate", "UpdatedBy", "IsDeleted"
                },
                values: new object[]
                {
                    "Opened", now, emptyGuid, now, emptyGuid, false
                });
            
            migrationBuilder.InsertData(
                table: "AppointmentStatus",
                columns: new[]
                {
                    "Name", "CreatedDate", "CreatedBy", "UpdatedDate", "UpdatedBy", "IsDeleted"
                },
                values: new object[]
                {
                    "Pending", now, emptyGuid, now, emptyGuid, false
                });
            
            migrationBuilder.InsertData(
                table: "AppointmentStatus",
                columns: new[]
                {
                    "Name", "CreatedDate", "CreatedBy", "UpdatedDate", "UpdatedBy", "IsDeleted"
                },
                values: new object[]
                {
                    "Closed", now, emptyGuid, now, emptyGuid, false
                });
            
            migrationBuilder.InsertData(
                table: "AppointmentStatus",
                columns: new[]
                {
                    "Name", "CreatedDate", "CreatedBy", "UpdatedDate", "UpdatedBy", "IsDeleted"
                },
                values: new object[]
                {
                    "Cancelled", now, emptyGuid, now, emptyGuid, false
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AppointmentStatus_StatusId",
                table: "Appointments");

            migrationBuilder.DropTable(
                name: "AppointmentStatus");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_StatusId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Appointments");
        }
    }
}
