import * as React from 'react';
import { withStyles, Theme, createStyles } from '@material-ui/core/styles';
import Card from '@material-ui/core/Card';
import CardContent from '@material-ui/core/CardContent';
import CardMedia from '@material-ui/core/CardMedia';
import IconButton from '@material-ui/core/IconButton';
import Typography from '@material-ui/core/Typography';
import SkipPreviousIcon from '@material-ui/icons/SkipPrevious';
import PlayArrowIcon from '@material-ui/icons/PlayArrow';
import ThumbsUp from '@material-ui/icons/ThumbUp';
import ThumbsDown from '@material-ui/icons/ThumbDown';
const styles = (theme: Theme) =>
  createStyles({
    card: {
      display: 'flex',
    },
    details: {
      display: 'flex',
    },
    content: {
      flex: '1 0 auto',
      width: '100%',
    },
    cover: {
      width: 151,
      height: 151,
    },
    controls: {
      display: 'flex',
      alignItems: 'center',
      paddingLeft: theme.spacing.unit,
      paddingBottom: theme.spacing.unit,
    },
    playIcon: {
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
        <CardContent className={classes.content}>
          <CardMedia className={classes.cover} image={albumArt} />
        </CardContent>
      </div>
    </Card>
  );
}

export default withStyles(styles, { withTheme: true })(MediaControlCard);
