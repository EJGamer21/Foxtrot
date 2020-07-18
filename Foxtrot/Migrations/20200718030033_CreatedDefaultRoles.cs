using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Foxtrot.Migrations
{
    public partial class CreatedDefaultRoles : Migration
    {
        private readonly Guid _standardUserId = new Guid("795C0B8F-A244-4BBA-B17E-48FB117D04A6");
        private readonly Guid _providerUserId = new Guid("2AB97C60-FB38-4FC8-AA63-6D39E4101CB6");
        private readonly string _emptyGuid = "00000000-0000-0000-0000-000000000000";
        private readonly DateTime _now = DateTime.Now;
        
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[]
                {
                    "Id", "Name", "Description", "CreatedDate", "CreatedBy", 
                    "UpdatedDate", "UpdatedBy"
                },
                values: new object[]
                {
                    _standardUserId, "User", "I'm just a normal guy",
                    _now, _emptyGuid, _now, _emptyGuid
                });
            
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[]
                {
                    "Id", "Name", "Description", "CreatedDate", "CreatedBy", 
                    "UpdatedDate", "UpdatedBy"
                },
                values: new object[]
                {
                    _providerUserId, "Provider", "I provide services to the users",
                    _now, _emptyGuid, _now, _emptyGuid
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: _standardUserId);
            
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: _providerUserId);

        }
    }
}
