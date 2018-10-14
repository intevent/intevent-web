import { Song } from '../../models/song';

export interface SongsUpdated {
  currentlyPlaying: Readonly<Song>,
  votableSongs: ReadonlyArray<Song>,
};
