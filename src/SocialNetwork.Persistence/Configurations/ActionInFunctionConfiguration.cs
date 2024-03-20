using SocialNetwork.Domain.Entities.Identity;
using SocialNetwork.Persistence.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace SocialNetwork.Persistence.Configurations
{
    internal sealed class ActionInFunctionConfiguration : IEntityTypeConfiguration<ActionInFunction>
    {
        public void Configure(EntityTypeBuilder<ActionInFunction> builder)
        {
            builder.ToTable(TableNames.ActionInFunctions);

            builder.HasKey(x => new { x.ActionId, x.FunctionId });
        }
    }
}


