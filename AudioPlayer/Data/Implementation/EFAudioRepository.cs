#nullable enable
using System.Data.Entity;
using AudioPlayer.Data.Interfaces;
using AudioPlayer.Models;

namespace AudioPlayer.Data.Implementation;

public class EFAudioRepository : EFGenericRepository<Audio>
{
    public EFAudioRepository(ApplicationContext context) : base(context)
    {
        
    }

    public override Audio Add(Audio audio)
    {
        if (GetAll().Any() is false)
        {
            return _dbSet.Add(audio);
        }

        var lastAudio = GetAll().First(entity => entity.NextAudio is null);
        lastAudio.NextAudio = audio;
        audio.PreviousAudio = lastAudio;
        return _dbSet.Add(audio);
    }

    public override void Delete(Audio audio)
    {
        if (audio.NextAudio is null && audio.PreviousAudio is not null)
        {
            var previousAudio = audio.PreviousAudio;
            previousAudio.NextAudio = null;
        }

        else if (audio.NextAudio is not null && audio.PreviousAudio is null)
        {
            var nextAudio = audio.NextAudio;
            nextAudio.PreviousAudio = null; 
        }
        
        else if (audio.NextAudio is not null && audio.PreviousAudio is not null)
        {
            var previousAudio = audio.PreviousAudio;
            var nextAudio = audio.NextAudio;
            nextAudio.PreviousAudio = previousAudio;
            previousAudio.NextAudio = nextAudio;
        }
        
        if (_dbContext.Entry(audio).State == EntityState.Detached)
        {
            _dbSet.Attach(audio);
        }

        _dbSet.Remove(audio);
    }
}