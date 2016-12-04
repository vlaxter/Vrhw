namespace Vrhw.Repository.Sql.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class IntialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Diffs",
                c => new
                {
                    Id = c.Int(nullable: false),
                    Left = c.String(),
                    Right = c.String(),
                })
                .PrimaryKey(t => t.Id);
        }

        public override void Down()
        {
            DropTable("dbo.Diffs");
        }
    }
}