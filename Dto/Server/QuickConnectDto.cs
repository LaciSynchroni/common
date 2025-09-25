using LaciSynchroni.Common.Data.Enum;
using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaciSynchroni.Common.Dto.Server;

[MessagePackObject()]
public record QuickConnectDto
{
    [Key(0)]
    public string ServerName = string.Empty;
    [Key(1)]
    public string ServerURI = string.Empty;
    [Key(2)]
    public string SecretKey = string.Empty;
}
