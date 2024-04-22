using Newtonsoft.Json;

namespace LEGO4IDControl.Features.AGVEvents;

public record AgvEvent(string status, int x, int y);