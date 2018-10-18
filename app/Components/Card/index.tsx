import * as React from 'react';
import { withStyles, Theme, createStyles } from '@material-ui/core/styles';
import Card from '@material-ui/core/Card';
import CardContent from '@material-ui/core/CardContent';
import CardMedia from '@material-ui/core/CardMedia';
import IconButton from '@material-ui/core/IconButton';
import Typography from '@material-ui/core/Typography';
import PlayArrowIcon from '@material-ui/icons/PlayArrow';
import ThumbsUp from '@material-ui/icons/ThumbUp';
const styles = (theme: Theme) =>
  createStyles({
    card: {
      display: 'flex',
      marginBottom: '5px',
      marginTop: '5px',
      width: '100%',
    },
    details: {
      display: 'flex',
      justifyContent: 'space-between',
      width: 'calc(100% - 350px)',
    },
    content: {
      flex: '1 0 auto',
      width: '100%',
    },
    cover: {
      height: 151,
      marginLeft: 'auto',
      width: 151,
    },
    controls: {
      display: 'flex',
      alignItems: 'center',
      paddingLeft: theme.spacing.unit,
      paddingBottom: theme.spacing.unit,
    },
    playIcon: {
      alignItems: 'left',
      height: 38,
      width: 38,
    },
  });

function MediaControlCard(props) {
  const {
    classes,
    theme,
    title,
    artist,
    votes,
    albumArt,
    voteClick,
    canVote,
    displayVoting = true,
  } = props;

  return (
    <Card className={classes.card}>
      <div className={classes.details}>
        <CardContent className={classes.content}>
          <Typography variant="headline">{title}</Typography>
          <Typography variant="subheading" color="textSecondary">
            {artist}
          </Typography>
        </CardContent>
        {displayVoting &&
          <div className={classes.controls}>
            <IconButton
              aria-label="thumbs up"
              onClick={e => {
                e.preventDefault();
                voteClick();
              }}
            >
              <ThumbsUp />
            </IconButton>

            <Typography variant="subheading" color="textSecondary">
              Votes: {votes}
            </Typography>
          </div>
        }
        {!displayVoting &&
          <div className={classes.controls}>
            <IconButton aria-label="Play/pause">
            <PlayArrowIcon className={classes.playIcon} />
          </IconButton>
          </div>
        }
      </div>
      <CardMedia className={classes.cover} image={albumArt} />
    </Card>
  );
}

export default withStyles(styles, { withTheme: true })(MediaControlCard);
