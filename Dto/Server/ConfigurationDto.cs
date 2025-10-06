﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaciSynchroni.Common.Dto.Server;
public record ConfigurationDto
{
    public string ServerName { get; set; } = string.Empty;
    public Version ServerVersion { get; set; } = new(0, 0, 0);
    public Uri ServerUri { get; set; } = new("wss://noemptyuri");
    public string? DiscordInvite { get; set; } = null;
    public string? ServerRules { get; set; } = null;
    public bool? IsOAuthEnabled { get; set; } = null;
}
