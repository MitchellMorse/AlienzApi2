namespace AlienzApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AwardReasons", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Powerups", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Powerups", "Name", c => c.String(nullable: false, maxLength: 1));
            AlterColumn("dbo.AwardReasons", "Name", c => c.String(nullable: false, maxLength: 1));
        }
    }
}
