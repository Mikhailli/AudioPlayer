namespace AudioPlayer.Models.ApiRequestModels;

public class AudiosSelectParameters : SelectParameters
{
    public AudiosSelectParameters(int start, int length, string searchPattern,
        string? orderColumnName, bool isAscending)
        : base(start, length, searchPattern, orderColumnName, isAscending)
    {
        
    }
}