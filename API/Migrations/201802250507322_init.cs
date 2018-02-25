namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SS_AssociateDepartmentSkillsets",
                c => new
                    {
                        AssociateDepartmentSkillsetID = c.Int(nullable: false, identity: true),
                        AssociateID = c.Int(nullable: false),
                        DepartmentSkillsetID = c.Int(nullable: false),
                        LastWorkedOn = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.AssociateDepartmentSkillsetID);
            
            CreateTable(
                "dbo.SS_Associates",
                c => new
                    {
                        AssociateID = c.Int(nullable: false, identity: true),
                        FullName = c.String(maxLength: 100),
                        UserID = c.String(maxLength: 25),
                        PhoneNumber = c.String(maxLength: 20),
                        VPN = c.Boolean(nullable: false),
                        DepartmentID = c.Int(nullable: false),
                        LocationID = c.Int(nullable: false),
                        UpdatedOn = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AssociateID);
            
            CreateTable(
                "dbo.SS_Departments",
                c => new
                    {
                        DepartmentID = c.Int(nullable: false, identity: true),
                        DepartmentDescr = c.String(nullable: false, maxLength: 30),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.DepartmentID);
            
            CreateTable(
                "dbo.SS_DepartmentSkillsets",
                c => new
                    {
                        DepartmentSkillsetID = c.Int(nullable: false, identity: true),
                        DepartmentID = c.Int(nullable: false),
                        SkillsetID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DepartmentSkillsetID);
            
            CreateTable(
                "dbo.SS_Locations",
                c => new
                    {
                        LocationID = c.Int(nullable: false, identity: true),
                        LocationDescr = c.String(nullable: false, maxLength: 30),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.LocationID);
            
            CreateTable(
                "dbo.set_group_access",
                c => new
                    {
                        grp_mod_id = c.Int(nullable: false, identity: true),
                        grp_id = c.String(maxLength: 25),
                        mod_id = c.String(maxLength: 25),
                        can_view = c.Boolean(nullable: false),
                        can_add = c.Boolean(nullable: false),
                        can_edit = c.Boolean(nullable: false),
                        can_delete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.grp_mod_id);
            
            CreateTable(
                "dbo.set_group",
                c => new
                    {
                        grp_id = c.String(nullable: false, maxLength: 25),
                        grp_name = c.String(maxLength: 50),
                        grp_desc = c.String(maxLength: 255),
                        created_date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.grp_id);
            
            CreateTable(
                "dbo.set_module",
                c => new
                    {
                        mod_id = c.String(nullable: false, maxLength: 25),
                        mod_name = c.String(maxLength: 50),
                        mod_desc = c.String(maxLength: 255),
                        created_date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.mod_id);
            
            CreateTable(
                "dbo.set_user_access",
                c => new
                    {
                        user_grp_id = c.Int(nullable: false, identity: true),
                        user_id = c.String(maxLength: 25),
                        grp_id = c.String(maxLength: 25),
                    })
                .PrimaryKey(t => t.user_grp_id);
            
            CreateTable(
                "dbo.set_user",
                c => new
                    {
                        user_id = c.String(nullable: false, maxLength: 25),
                        user_name = c.String(maxLength: 25),
                        user_last_name = c.String(maxLength: 50),
                        user_first_name = c.String(maxLength: 50),
                        user_middle_name = c.String(maxLength: 50),
                        can_PROD = c.Boolean(nullable: false),
                        can_UAT = c.Boolean(nullable: false),
                        can_PEER = c.Boolean(nullable: false),
                        can_DEV = c.Boolean(nullable: false),
                        created_date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.user_id);
            
            CreateTable(
                "dbo.SS_Skillsets",
                c => new
                    {
                        SkillsetID = c.Int(nullable: false, identity: true),
                        SkillsetDescr = c.String(nullable: false, maxLength: 50),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.SkillsetID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SS_Skillsets");
            DropTable("dbo.set_user");
            DropTable("dbo.set_user_access");
            DropTable("dbo.set_module");
            DropTable("dbo.set_group");
            DropTable("dbo.set_group_access");
            DropTable("dbo.SS_Locations");
            DropTable("dbo.SS_DepartmentSkillsets");
            DropTable("dbo.SS_Departments");
            DropTable("dbo.SS_Associates");
            DropTable("dbo.SS_AssociateDepartmentSkillsets");
        }
    }
}
