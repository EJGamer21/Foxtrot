using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Foxtrot.Migrations
{
    public partial class CreatedAdminUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            Guid adminId = new Guid("9F917B76-5B52-4DF2-9A9E-D99875777AC4");
            Guid adminRoleId = new Guid("DC00B091-C6C6-449F-9F0C-2B55AF3E0D34");
            string emptyGuid = "00000000-0000-0000-0000-000000000000";
            DateTime now = DateTime.Now;

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[]
                {
                    "Id", "Name", "Description", "CreatedDate", "CreatedBy", 
                    "UpdatedDate", "UpdatedBy"
                },
                values: new object[]
                {
                    adminRoleId, "Admin", "I'm the creator of everything",
                    now, emptyGuid, now, emptyGuid
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[]
                {
                    "Id", "FullName", "Email", "Address", "Dni", "RoleId",
                    "CreatedDate", "CreatedBy", "UpdatedDate", "UpdatedBy"
                    
                },
                values: new object[] {
                    adminId, "Enger Jimenez", "ejimenezr21@gmail.com", 
                    "South Carolina, Townville, 455  Mill Street", "62464787F", adminRoleId,
                    now, emptyGuid, now, emptyGuid,
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
