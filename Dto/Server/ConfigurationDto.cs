using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaciSynchroni.Common.Dto.Server;
public record ConfigurationDto
{
    public string ServerName { get; set; } = string.Empty;
    public Version ServerVersion { get; set; } = new(0, 0, 0);
    public Uri? HubUri { get; set; } = null;
    public Uri? AuthUri { get; set; } = null;
    public string? DiscordInvite { get; set; } = null;
    public string? ServerRules { get; set; } = null;
    public bool? IsOAuthEnabled { get; set; } = null;
    public bool? BypassVersionCheck { get; set; } = null;
}
