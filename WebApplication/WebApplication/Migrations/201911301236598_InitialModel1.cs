namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel1 : DbMigration
    {
        public override void Up()
        {
            Sql(@"
            INSERT INTO [dbo].[AspNetUsers] ([Id], [Name], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'8601291b-5d55-4ce9-8fb5-65c5b6583b4b', N'admin', N'admin@gmail.com', 0, N'AI3gI+WFW4XPwu33BT2YsRml+LkRgt0LLlZy/hZg69uSrQBLiqtLm4ViA6F0HnesFw==', N'425c4374-e267-414f-bf96-e4ca4b805e54', NULL, 0, 0, NULL, 1, 0, N'admin@gmail.com')
            INSERT INTO [dbo].[AspNetUsers] ([Id], [Name], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'b451baa6-65a4-4805-8cc6-ae220ce831ff', N'guest', N'guest@gmail.com', 0, N'AJN0kyNg6hJ6apNXnTxb3xkg70QbNwfC2dzR9d23RzGLIA/6Qo27a2brMePzWSEudg==', N'52da49a2-3670-4245-bf8c-6c84e1514f9d', NULL, 0, 0, NULL, 1, 0, N'guest@gmail.com')
            
            INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'8971bc16-f34a-458c-ac91-d6ec54ea7565', N'Admin')
            INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'8601291b-5d55-4ce9-8fb5-65c5b6583b4b', N'8971bc16-f34a-458c-ac91-d6ec54ea7565')
    
            SET IDENTITY_INSERT [dbo].[CategoryTypes] ON
            INSERT INTO [dbo].[CategoryTypes] ([Id], [Name]) VALUES (2, N'Veg')
            SET IDENTITY_INSERT [dbo].[CategoryTypes] OFF

      

            ");

        }

        public override void Down()
        {
        }
    }
}
