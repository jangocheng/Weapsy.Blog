using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Weapsy.Blog.Data.EF.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blog",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Theme = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BlogId = table.Column<Guid>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    Excerpt = table.Column<string>(nullable: true),
                    Slug = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    StatusTimeStamp = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blog");

            migrationBuilder.DropTable(
                name: "Post");
        }
    }
}
