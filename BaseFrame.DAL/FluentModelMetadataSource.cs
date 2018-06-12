using BaseFrame.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;
using Telerik.OpenAccess.Metadata.Fluent;

namespace BaseFrame.DAL
{
    public partial class FluentModelMetadataSource : FluentMetadataSource
    {

        protected override IList<MappingConfiguration> PrepareMapping()
        {
            List<MappingConfiguration> mappingConfigurations = new List<MappingConfiguration>();

            mappingConfigurations.Add(GetSuncereUserMappingConfiguration());
            mappingConfigurations.Add(GetSuncereRoleMappingConfiguration());
            mappingConfigurations.Add(GetSuncerePermissionMappingConfiguration());
            mappingConfigurations.Add(GetSuncereAuditLogMappingConfiguration());

            return mappingConfigurations;
        }

        protected override MetadataContainer CreateModel()
        {
            MetadataContainer container = base.CreateModel();
            container.NameGenerator.RemoveCamelCase = false;
            container.NameGenerator.ResolveReservedWords = false;
            container.NameGenerator.SourceStrategy = NamingSourceStrategy.Property;
            return container;
        }

        public MappingConfiguration<SuncereUser> GetSuncereUserMappingConfiguration()
        {
            MappingConfiguration<SuncereUser> configuration = new MappingConfiguration<SuncereUser>();
            configuration.MapType().ToTable("SuncereUser");

            configuration.HasProperty(x => x.Id).IsIdentity(KeyGenerator.Autoinc);
            configuration.HasProperty(x => x.UserName).IsNotNullable().HasColumnType("nvarchar").HasLength(128);
            configuration.HasProperty(x => x.Password).IsNotNullable().HasColumnType("nvarchar").HasLength(256);
            configuration.HasProperty(x => x.EmailAddress).HasColumnType("nvarchar").HasLength(256);
            configuration.HasProperty(x => x.PhoneNumber).HasColumnType("nvarchar").HasLength(16);
            configuration.HasProperty(x => x.LastLoginHostAddress).HasColumnType("nvarchar").HasLength(256);
            configuration.HasProperty(x => x.Status).HasColumnType("bit");
            configuration.HasProperty(x => x.IsStatic).HasColumnType("bit");
            configuration.HasProperty(x => x.Remark).HasColumnType("nvarchar").HasLength(256);
            configuration.HasProperty(x => x.CreationTime).IsCalculatedOn(DateTimeAutosetMode.Insert);
            configuration.HasProperty(x => x.LastModificationTime).IsCalculatedOn(DateTimeAutosetMode.InsertAndUpdate);

            configuration.HasAssociation(x => x.SuncereRoles).HasFieldName("_suncereRoles").WithOpposite(x => x.SuncereUsers).WithDataAccessKind(DataAccessKind.ReadWrite);

            return configuration;
        }

        public MappingConfiguration<SuncereRole> GetSuncereRoleMappingConfiguration()
        {
            MappingConfiguration<SuncereRole> configuration = new MappingConfiguration<SuncereRole>();
            configuration.MapType().ToTable("SuncereRole");

            configuration.HasProperty(x => x.Id).IsIdentity(KeyGenerator.Autoinc);
            configuration.HasProperty(x => x.Name).IsNotNullable().HasColumnType("nvarchar").HasLength(128);
            configuration.HasProperty(x => x.Status).HasColumnType("bit");
            configuration.HasProperty(x => x.IsStatic).HasColumnType("bit");
            configuration.HasProperty(x => x.Remark).HasColumnType("nvarchar").HasLength(256);
            configuration.HasProperty(x => x.CreationTime).IsCalculatedOn(DateTimeAutosetMode.Insert);
            configuration.HasProperty(x => x.LastModificationTime).IsCalculatedOn(DateTimeAutosetMode.InsertAndUpdate);

            configuration.HasAssociation(x => x.SuncereUsers).HasFieldName("_suncereUsers").WithOpposite(x => x.SuncereRoles).WithDataAccessKind(DataAccessKind.ReadWrite).MapJoinTable("SuncereUserRole", (x, y) => new { SuncereRole_Id = x.Id, SuncereUser_Id = y.Id }).CreatePrimaryKeyFromForeignKeys();
            configuration.HasAssociation(x => x.SuncerePermissions).HasFieldName("_suncerePermissions").WithOpposite(x => x.SuncereRoles).WithDataAccessKind(DataAccessKind.ReadWrite);

            return configuration;
        }

        public MappingConfiguration<SuncerePermission> GetSuncerePermissionMappingConfiguration()
        {
            MappingConfiguration<SuncerePermission> configuration = new MappingConfiguration<SuncerePermission>();
            configuration.MapType().ToTable("SuncerePermission");

            configuration.HasProperty(x => x.Id).IsIdentity(KeyGenerator.Autoinc);
            configuration.HasProperty(x => x.Name).IsNotNullable().HasColumnType("nvarchar").HasLength(128);
            configuration.HasProperty(x => x.Controller).HasColumnType("nvarchar").HasLength(128);
            configuration.HasProperty(x => x.Action).HasColumnType("nvarchar").HasLength(128);
            configuration.HasProperty(x => x.Icon).HasColumnType("nvarchar").HasLength(128);
            configuration.HasProperty(x => x.Status).HasColumnType("bit");
            configuration.HasProperty(x => x.IsStatic).HasColumnType("bit");
            configuration.HasProperty(x => x.Remark).HasColumnType("nvarchar").HasLength(256);
            configuration.HasProperty(x => x.CreationTime).IsCalculatedOn(DateTimeAutosetMode.Insert);
            configuration.HasProperty(x => x.LastModificationTime).IsCalculatedOn(DateTimeAutosetMode.InsertAndUpdate);

            configuration.HasAssociation(x => x.SuncereRoles).HasFieldName("_suncereRoles").WithOpposite(x => x.SuncerePermissions).WithDataAccessKind(DataAccessKind.ReadWrite).MapJoinTable("SuncereRolePermission", (x, y) => new { SuncerePermission_Id = x.Id, SuncereRole_Id = y.Id }).CreatePrimaryKeyFromForeignKeys();

            return configuration;
        }

        public MappingConfiguration<SuncereAuditLog> GetSuncereAuditLogMappingConfiguration()
        {
            MappingConfiguration<SuncereAuditLog> configuration = new MappingConfiguration<SuncereAuditLog>();
            configuration.MapType().ToTable("SuncereAuditLog");

            configuration.HasProperty(x => x.Id).IsIdentity(KeyGenerator.Autoinc);
            configuration.HasProperty(x => x.UserName).IsNotNullable().HasColumnType("nvarchar").HasLength(128);
            configuration.HasProperty(x => x.Url).IsNotNullable().HasColumnType("nvarchar").HasLength(256);
            configuration.HasProperty(x => x.Referrer).IsNotNullable().HasColumnType("nvarchar").HasLength(256);
            configuration.HasProperty(x => x.HostName).IsNotNullable().HasColumnType("nvarchar").HasLength(256);
            configuration.HasProperty(x => x.HostAddress).IsNotNullable().HasColumnType("nvarchar").HasLength(256);
            configuration.HasProperty(x => x.CreationTime).IsCalculatedOn(DateTimeAutosetMode.Insert);

            return configuration;
        }

    }
}
