using Explorer.Encounters.API.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Dtos;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(EncounterDto), typeDiscriminator: (int)EncounterType.Misc)]
[JsonDerivedType(typeof(SocialEncounterDto), typeDiscriminator: (int)EncounterType.Social)]
[JsonDerivedType(typeof(HiddenLocationEncounterDto), typeDiscriminator: (int)EncounterType.Locaion)]
public class EncounterDto
{
    [JsonPropertyName("type")]
    public EncounterType Type { get; set; }
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public LocationDto Location { get; set; }
    public int XP { get; set; }
    public EncounterStatus Status { get; set; }
    public long CreatorId { get; set; }
}
