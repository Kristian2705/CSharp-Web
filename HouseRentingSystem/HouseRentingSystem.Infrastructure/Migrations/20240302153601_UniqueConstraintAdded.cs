using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRentingSystem.Infrastructure.Migrations
{
    public partial class UniqueConstraintAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "56e80787-8b93-4a88-ab63-f30dec35fda0", "AQAAAAEAACcQAAAAEFdTJLfdcYWYITPhhrLnhfN4/cULTV9XCMWweKajkXkBOVvY/o4CG6pRvubzDdJ33A==", "2a816299-d25f-4cd5-9271-0f4145a1d8ee" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2bf8d63b-260e-4675-b1ac-e0ecfd3eff77", "AQAAAAEAACcQAAAAEDS5yfEe3A5Ixxr3woyxgLuzuuZwLmxacqp4lzKpsqg5mNpmb6adFNEJ65o9eTNUyg==", "f5477ccf-887e-4488-be16-f8ab6c739142" });

            migrationBuilder.CreateIndex(
                name: "IX_Agents_PhoneNumber",
                table: "Agents",
                column: "PhoneNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Agents_PhoneNumber",
                table: "Agents");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2f6efa90-85e4-444a-9b05-eca20082ec19", "AQAAAAEAACcQAAAAEBceWRq+8fspBCI37uewJzAjcoGX7tHCeCQWog749UV/BN1bVsa/YJ/GiRjnMGOd2Q==", "7aa2d641-0748-411a-bf9c-96319292a3d5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a1bbe18a-5dfb-426a-aaf2-0a40ff44fef7", "AQAAAAEAACcQAAAAEEWhQMlozhA2lBMiJ5A7c77clmw/y4xO0GQIkWLNLd/L6ZAMI66ngPowXjREeseTfA==", "429f2dda-58d3-49bd-b26c-6f63ce0394f7" });
        }
    }
}
