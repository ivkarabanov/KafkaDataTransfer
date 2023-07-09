using FluentMigrator;

namespace WebKafkaTry.Reader.Migrations
{
    [Migration(1)]
    public sealed class Initial : Migration
    {
        public override void Down()
        {
            Delete.Table("Notes");
        }

        public override void Up()
        {
            Create.Table("Notes")
                .WithColumn("NoteId").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Theme").AsString().Nullable()
                .WithColumn("Text").AsString().Nullable()
                .WithColumn("CreatedOn").AsString().NotNullable();

            Create.Table("NoteCategories")
                .WithColumn("NoteId").AsInt32().NotNullable()
                .WithColumn("CategoryId").AsInt32().NotNullable();

            Create.PrimaryKey("PK_NoteCategories_NoteId_CategoryId")
                .OnTable("NoteCategories")
                .Columns("NoteId", "CategoryId");

            Create.Table("Categories")
                .WithColumn("CategoryId").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Name").AsString().NotNullable();

            Insert.IntoTable("Categories").Row(new { Name = "Рабочее" });
            Insert.IntoTable("Categories").Row(new { Name = "Хобби" });
        }
    }
}
