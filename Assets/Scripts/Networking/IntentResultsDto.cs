using System.Collections;
using System.Collections.Generic;

public class IntentResultsDto
{
    public int IdIntent { get; set; }
    public Dictionary<int, bool> Result { get; set; } = new Dictionary<int, bool>();
}
